Imports System.Data
Imports STV.Entidades
Imports STV.Seguranca
Partial Class Consultas_Con_Meus_Cursos : Inherits STV.Base.Page

    Dim _Curso As Curso
    Private ReadOnly Property Curso As Curso
        Get
            If IsNothing(_Curso) Then _
                _Curso = New Curso
            Return _Curso
        End Get
    End Property

    Dim _Usuario As Usuario
    Private ReadOnly Property Usuario As Usuario
        Get
            If IsNothing(_Usuario) Then _
                _Usuario = New Usuario
            Return _Usuario
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

    Dim _Cursos_Usuario As DataTable
    Private ReadOnly Property Cursos_Usuario As DataTable
        Get
            If IsNothing(_Cursos_Usuario) Then
                _Cursos_Usuario = CType(Me.ViewState("Cursos_Usuario"), DataTable)
            End If
            Return _Cursos_Usuario
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack() Then
                Preenche_DDL_Usuario()
                Carrega_Inscricoes()

                Carrega_Grid(0, "", Usuario_Logado.Cod_Usuario)

            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub
    Private Sub Carrega_Inscricoes()
        Try
            Me.ViewState("Cursos_Usuario") = Curso.Carrega_Cursos_Usuario(Usuario_Logado.Cod_Usuario)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub B_Filtrar_Click(sender As Object, e As System.EventArgs) Handles B_Filtrar.Click
        Try
            Carrega_Grid(DDL_Usuario.SelectedValue, TB_Titulo.Text, Usuario_Logado.Cod_Usuario)
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub
    Private Sub Carrega_Grid(Instrutor As Integer, Titulo As String, Cod_Usuario As Integer)
        Try
            GV_Curso.DataSource = Curso.Carrega_Meus_Cursos(Instrutor, Titulo, Cod_Usuario)
            GV_Curso.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub GV_Curso_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Curso.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                CType(e.Row.FindControl("HL_Visualizar"), HyperLink).NavigateUrl = "../Consultas/Conteudo.aspx?Cod=" + Criptografia.Encryptdata(Util.CInteger(e.Row.DataItem("Cod_Curso")).ToString)
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

#Region "Carrega DDLs"
    Protected Sub Preenche_DDL_Usuario()
        Try
            Dim Usuario As New Usuario
            DDL_Usuario.DataSource = Usuario.Carrega_Usuarios("", False, 0, Usuario_Logado.Cod_Usuario, False)
            DDL_Usuario.DataBind()

            Dim item As New ListItem
            item.Text = "Todos"
            item.Value = 0
            DDL_Usuario.Items.Insert(0, item)
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region
End Class
