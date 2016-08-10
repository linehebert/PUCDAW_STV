Imports STV.Entidades

Partial Class Consultas_Con_Departamento : Inherits STV.Base.Page

    Dim _Departamento As Departamento

    Private ReadOnly Property Departamento As Departamento
        Get
            If IsNothing(_Departamento) Then _
                _Departamento = New Departamento

            Return _Departamento
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Carrega_Grid("", False)
        End If
    End Sub

    Private Sub Carrega_Grid(Cod_Departamento As Integer)
        GV_Departamento.DataSource = Departamento.Carrega_Departamento(Cod_Departamento)
        GV_Departamento.DataBind()
    End Sub
    Private Sub Carrega_Grid(Descricao As String, Inativo As Boolean)
        GV_Departamento.DataSource = Departamento.Carrega_Departamentos(Descricao, Inativo)
        GV_Departamento.DataBind()
    End Sub

    Protected Sub GV_Departamento_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Departamento.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("HL_Alterar"), HyperLink).NavigateUrl = "../cadastros/Cad_Departamento.aspx?Codigo=" + Util.CInteger(e.Row.DataItem("Cod_Departamento")).ToString()
        End If
    End Sub

    Protected Sub B_Filtrar_Click(sender As Object, e As System.EventArgs) Handles B_Filtrar.Click
        Carrega_Grid(TB_Descr.Text, CB_Inativos.Checked)
    End Sub

    Protected Sub B_Novo_Click(sender As Object, e As System.EventArgs) Handles B_Novo.Click
        Response.Redirect("../Cadastros/Cad_Departamento.aspx")
    End Sub

    Protected Sub GV_Departamento_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_Departamento.PageIndexChanging
        GV_Departamento.PageIndex = e.NewPageIndex
        Carrega_Grid(TB_Descr.Text, CB_Inativos.Checked)
    End Sub

End Class
