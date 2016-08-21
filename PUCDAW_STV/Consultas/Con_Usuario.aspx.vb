Imports STV.Entidades


Partial Class Consultas_Con_Usuario : Inherits STV.Base.Page

    Dim _Usuario As Usuario
    Private ReadOnly Property Usuario As Usuario
        Get
            If IsNothing(_Usuario) Then _
                _Usuario = New Usuario

            Return _Usuario
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Carrega_Grid("", False, 0, 0, True)
            Preenche_DDL_Departamento()
        End If
    End Sub

    Private Sub Carrega_Grid(Cod_Usuario As Integer)
        GV_Usuario.DataSource = Usuario.Carrega_Usuario(Cod_Usuario)
        GV_Usuario.DataBind()
    End Sub
    Private Sub Carrega_Grid(Descricao As String, Inativo As Boolean, Departamento As Integer, UserLogado As Integer, CarregaAdm As Boolean)
        GV_Usuario.DataSource = Usuario.Carrega_Usuarios(Descricao, Inativo, Departamento, UserLogado, CarregaAdm)
        GV_Usuario.DataBind()
    End Sub

    Protected Sub GV_Usuario_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Usuario.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("HL_Alterar"), HyperLink).NavigateUrl = "../cadastros/Cad_Usuario.aspx?Codigo=" + Util.CInteger(e.Row.DataItem("Cod_Usuario")).ToString()
        End If
    End Sub

    Protected Sub B_Filtrar_Click(sender As Object, e As System.EventArgs) Handles B_Filtrar.Click
        Carrega_Grid(TB_Descr.Text, CB_Inativos.Checked, DDL_Departamento.SelectedValue, 0, True)
    End Sub

    Protected Sub B_Novo_Click(sender As Object, e As System.EventArgs) Handles B_Novo.Click
        Response.Redirect("../Cadastros/Cad_Usuario.aspx")
    End Sub

    Protected Sub GV_Usuario_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_Usuario.PageIndexChanging
        GV_Usuario.PageIndex = e.NewPageIndex
        Carrega_Grid(TB_Descr.Text, CB_Inativos.Checked, DDL_Departamento.SelectedValue, 0, True)
    End Sub

#Region "Carrega DDLs"

    Protected Sub Preenche_DDL_Departamento()
        Dim departamento As New Departamento
        DDL_Departamento.DataSource = departamento.Carrega_Departamentos("", False)
        DDL_Departamento.DataBind()

        Dim item As New ListItem
        item.Text = "TODOS OS DEPARTAMENTOS"
        item.Value = 0
        DDL_Departamento.Items.Insert(0, item)
    End Sub

#End Region
End Class
