Imports STV.Entidades
Imports STV.Seguranca

Partial Class Dados_Acesso : Inherits STV.Base.Page

    Dim _Usuario As Usuario
    Private ReadOnly Property Usuario As Usuario
        Get
            If IsNothing(_Usuario) Then _
                _Usuario = New Usuario
            Return _Usuario
        End Get
    End Property
    Private ReadOnly Property Cod_Usuario As Integer
        Get
            Return Request("Codigo")
        End Get
    End Property
    Dim _Autenticacao As Autenticacao
    Private ReadOnly Property Autenticacao As Autenticacao
        Get
            If IsNothing(_Autenticacao) Then _
                _Autenticacao = New Autenticacao
            Return _Autenticacao
        End Get
    End Property
    Dim _Usuario_Logado As Usuario.Dados
    Private ReadOnly Property Usuario_Logado As Usuario.Dados
        Get
            If IsNothing(_Usuario_Logado) Then
                _Usuario_Logado = New Usuario.Dados
                _Usuario_Logado = Autenticacao.Obter_User_Logado()
            End If

            Return _Usuario_Logado
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                TB_CPF.Text = Usuario_Logado.CPF
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

#Region "Botões e Eventos"

    Private Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click
        Try
            Dim senha_atual As String = Biblio.Pega_Valor("SELECT Senha FROM USUARIO WHERE Cod_Usuario=" + Util.CString(Usuario_Logado.Cod_Usuario), "Senha")
            Dim Senha_Banco As String = Criptografia.Decryptdata(senha_atual)
            If Senha_Banco = TB_Senha_Atual.Text Then

                If TB_Senha.Text = TB_Confirma_Senha.Text Then
                    Dim Dados As New Usuario.Dados

                    Dados.Senha = Criptografia.Encryptdata(TB_Senha.Text)
                    Dados.Cod_Usuario = Usuario_Logado.Cod_Usuario

                    Usuario.Alterar_Senha(Dados)

                    D_Erro.Visible = False
                    D_Aviso.Visible = True
                    L_Aviso.Text = "Nova senha definida com sucesso!"
                Else
                    D_Erro.Visible = True
                    D_Aviso.Visible = False
                    L_Erro.Text = "Nova senha informada não é igual a confirmação da nova senha. Verifique e tente novamente!"


                    TB_Senha.Focus()
                End If
            Else
                D_Erro.Visible = True
                D_Aviso.Visible = False
                L_Erro.Text = "Senha Atual Incorreta. Por favor, tente novamente!"

                TB_Senha_Atual.Focus()
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Voltar_Click(sender As Object, e As EventArgs) Handles B_Voltar.Click
        Response.Redirect("Default.aspx")
    End Sub

#End Region
End Class
