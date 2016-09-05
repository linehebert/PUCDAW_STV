Imports STV.Entidades

Partial Class Consultas_Con_Categoria : Inherits STV.Base.Page

    Dim _Categoria As Categoria
    Private ReadOnly Property Categoria As Categoria
        Get
            If IsNothing(_Categoria) Then _
                _Categoria = New Categoria

            Return _Categoria
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Carrega_Grid("", False)
        End If
    End Sub

    Private Sub Carrega_Grid(Cod_Categoria As Integer)
        GV_Categoria.DataSource = Categoria.Carrega_Categoria(Cod_Categoria)
        GV_Categoria.DataBind()
    End Sub
    Private Sub Carrega_Grid(Descricao As String, Inativo As Boolean)
        GV_Categoria.DataSource = Categoria.Carrega_Categorias(Descricao, Inativo)
        GV_Categoria.DataBind()
    End Sub

    Protected Sub GV_Categoria_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Categoria.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("HL_Alterar"), HyperLink).NavigateUrl = "../cadastros/Cad_Categoria.aspx?Codigo=" + Util.CInteger(e.Row.DataItem("Cod_Categoria")).ToString()
        End If
    End Sub

    Protected Sub B_Filtrar_Click(sender As Object, e As System.EventArgs) Handles B_Filtrar.Click
        Carrega_Grid(TB_Descr.Text, CB_Inativos.Checked)
    End Sub

    Protected Sub B_Novo_Click(sender As Object, e As System.EventArgs) Handles B_Novo.Click
        Response.Redirect("../Cadastros/Cad_Categoria.aspx")
    End Sub

    Protected Sub GV_Categoria_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_Categoria.PageIndexChanging
        GV_Categoria.PageIndex = e.NewPageIndex
        Carrega_Grid(TB_Descr.Text, CB_Inativos.Checked)
    End Sub

End Class
