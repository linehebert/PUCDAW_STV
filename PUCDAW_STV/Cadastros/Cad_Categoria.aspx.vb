Imports STV
Imports STV.Entidades
Partial Class Cadastros_Cad_Categoria : Inherits STV.Base.Page

    Dim _Categoria As Categoria
    Private ReadOnly Property Categoria As Categoria
        Get
            If IsNothing(_Categoria) Then _
                _Categoria = New Categoria

            Return _Categoria
        End Get
    End Property

    Private ReadOnly Property Cod_Categoria As Integer
        Get
            Return Request("Codigo")
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Request("Codigo") <> "" Then
                    Monta_Dados()
                    cad_categoria.InnerText = "Alteração de Cadastro"
                Else
                    L_Codigo.Visible = False
                    TB_Codigo.Visible = False
                    cad_categoria.InnerText = "Nova Categoria"
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
            Dim Dado = Categoria.Carrega_Categoria(Cod_Categoria)

            TB_Codigo.Text = Dado.Cod_Categoria
            TB_Descr.Text = Dado.Descricao
            CB_Inativos.Checked = Dado.Categoria_Inativo
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub Voltar()
        Response.Redirect("../Consultas/Con_Categoria.aspx")
    End Sub
#End Region

#Region "Botões e Eventos"

    Protected Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click
        Try
            If Request("Codigo") <> "" Then
                Dim Dados As New Categoria.Dados
                Dados.Cod_Categoria = TB_Codigo.Text
                Dados.Descricao = TB_Descr.Text.ToUpper()
                Dados.Categoria_Inativo = CB_Inativos.Checked

                Categoria.Alterar(Dados)
            Else
                Dim Dados As New Categoria.Dados
                Dados.Descricao = TB_Descr.Text.ToUpper()
                Dados.Categoria_Inativo = CB_Inativos.Checked

                Categoria.Inserir(Dados)
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
