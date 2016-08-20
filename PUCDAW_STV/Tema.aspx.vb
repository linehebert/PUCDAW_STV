Imports System.Data
Imports System.IO
Imports STV.Entidades
Imports STV.Seguranca

Partial Class Tema : Inherits STV.Base.Page

    Dim _Layout As Layout
    Private ReadOnly Property Layout As Layout
        Get
            If IsNothing(_Layout) Then _
                _Layout = New Layout
            Return _Layout
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Preenche_DDL_Tema()
            Monta_Dados()
        End If
    End Sub

    Private Sub Monta_Dados()
        Dim Logo As String = Request.PhysicalApplicationPath & "\Images\Logo_Novo.jpg"
        If File.Exists(Logo) Then
            I_Logo.ImageUrl = "~/Images/Logo_Novo.jpg"
        Else
            I_Logo.ImageUrl = "~/Images/Logo_Default.png"
        End If

        'Dim Dado = Layout.Carrega_Tema()
        'DDL_Tema.SelectedValue = Dado.Cod_Tema

    End Sub

    Private Sub Salvar_Logotipo()
        D_Erro.Visible = False
        D_Aviso.Visible = False
        If FU_Logo.HasFile Then
            Dim Extensao As String = Path.GetExtension(FU_Logo.FileName).Replace(".", "").ToLower()
            Dim Caminho As String = ""

            If Extensao = "jpg" Or Extensao = "jpeg" Or Extensao = "gif" Or Extensao = "png" Then

                Caminho = Request.PhysicalApplicationPath + "images"
                Try
                    Dim Arquivo As String = (Caminho + "\Logo_Novo.jpg")

                    FU_Logo.SaveAs(Arquivo)

                    I_Logo.ImageUrl = ResolveUrl("~/Images/Logo_Novo.jpg")
                    I_Logo.Visible = True

                    D_Aviso.Visible = True
                    L_Aviso.Visible = True
                    L_Aviso.Text = "Alterações realizadas com sucesso!"

                    'Response.Redirect("Tema.aspx")
                Catch ex As Exception
                    D_Erro.Visible = True
                    L_Erro.Visible = True
                    L_Erro.Text = "Não foi possível salvar o arquivo. Tente novamente."
                End Try
            Else
                D_Erro.Visible = True
                L_Erro.Visible = True
                L_Erro.Text = "A extensão do arquivo é inválida."
            End If
        End If
    End Sub

    Protected Sub IB_Logo_Excluir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IB_Logo_Excluir.Click

        Dim Logo As String = Request.PhysicalApplicationPath & "\Images\Logo_Novo.jpg"
        If File.Exists(Logo) Then File.Delete(Logo)

        I_Logo.ImageUrl = ResolveUrl("~/Images/Logo_Default.jpg")
        I_Logo.Visible = True

        Response.Redirect("Tema.aspx")

        D_Aviso.Visible = True
        L_Aviso.Visible = True
        L_Aviso.Text = "O logo padrão do sistema foi definido com sucesso!"

    End Sub


    Protected Sub Preenche_DDL_Tema()
        Try
            Dim Tema As New Layout
            DDL_Tema.DataSource = Tema.Carrega_Temas("")
            DDL_Tema.DataBind()

            Dim item As New ListItem
            item.Text = "Selecione um tema"
            item.Value = 0
            DDL_Tema.Items.Insert(0, item)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click
        Salvar_Logotipo()

        L_Tema.Text = DDL_Tema.SelectedValue

        If DDL_Tema.SelectedValue > 0 Then
            Layout.Exclui_Tema_Antigo()

            Dim Dados As New Layout.Dados
            Dados.Cod_Tema = DDL_Tema.SelectedValue

            Layout.Alterar(Dados)
        End If

        Response.Redirect("Tema.aspx")

    End Sub

    Private Sub B_Cancelar_Click(sender As Object, e As EventArgs) Handles B_Cancelar.Click
        Response.Redirect("Default.aspx")
    End Sub
End Class
