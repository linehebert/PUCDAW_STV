Imports STV
Imports STV.Entidades
Partial Class Cadastros_Cad_Departamento : Inherits STV.Base.Page

    Dim _Departamento As Departamento
    Private ReadOnly Property Departamento As Departamento
        Get
            If IsNothing(_Departamento) Then _
                _Departamento = New Departamento

            Return _Departamento
        End Get
    End Property

    Private ReadOnly Property Cod_Departamento As Integer
        Get
            Return Request("Codigo")
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Request("Codigo") <> "" Then
                    Monta_Dados()
                Else
                    L_Codigo.Visible = False
                    TB_Codigo.Visible = False
                End If
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

#Region "Funções e Rotinas"
    Private Sub Monta_Dados()
        Try
            Dim Dado = Departamento.Carrega_Departamento(Cod_Departamento)

            TB_Codigo.Text = Dado.Cod_Departamento
            TB_Descr.Text = Dado.Descricao
            CB_Inativos.Checked = Dado.Departamento_Inativo
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub Voltar()
        Response.Redirect("../Consultas/Con_Departamento.aspx")
    End Sub
#End Region

#Region "Botões e Eventos"

    Protected Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click
        Try
            If Request("Codigo") <> "" Then
                Dim Dados As New Departamento.Dados
                Dados.Cod_Departamento = TB_Codigo.Text
                Dados.Descricao = TB_Descr.Text.ToUpper()
                Dados.Departamento_Inativo = CB_Inativos.Checked

                Departamento.Alterar(Dados)
            Else
                Dim Dados As New Departamento.Dados
                Dados.Descricao = TB_Descr.Text.ToUpper()
                Dados.Departamento_Inativo = CB_Inativos.Checked

                Departamento.Inserir(Dados)
            End If
            Voltar()
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub
    Protected Sub B_Cancelar_Click(sender As Object, e As System.EventArgs) Handles B_Cancelar.Click
        Voltar()
    End Sub

#End Region
End Class
