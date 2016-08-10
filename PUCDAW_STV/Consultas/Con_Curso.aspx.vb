Imports System.Data
Imports STV.Entidades
Imports STV.Seguranca
Partial Class Consultas_Con_Curso : Inherits STV.Base.Page

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
                If Usuario.Verifica_Responsabilidade(Usuario_Logado.Cod_Usuario) Then
                    'Instrutor
                    Titulo_Page.InnerText = "Consulta de Cursos"
                    Preenche_DDL_Departamento()
                    Carrega_Grid("", 0, False, 0)

                Else
                    'Somente Aluno
                    Titulo_Page.InnerText = "Cursos Disponíveis"
                    B_Novo.Visible = False
                    Carrega_Grid(Usuario_Logado.Cod_Departamento, 0, "")

                    'Define a visibilidade da página
                    B_Filtrar_Aluno.Visible = True
                    filtro_departamento.Visible = False
                    filtro_inativo.Visible = False
                    GV_Curso.Columns(0).Visible = False
                    GV_Curso.Columns(1).Visible = False
                    GV_Curso.Columns(3).Visible = False
                    GV_Curso.Columns(9).Visible = False
                End If
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

    Private Sub Carrega_Grid(Cod_Curso As Integer)
        Try
            GV_Curso.DataSource = Curso.Carrega_Curso(Cod_Curso)
            GV_Curso.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub Carrega_Grid(Titulo As String, Departamento As Integer, Instrutor As Integer, Inativo As Boolean)
        Try
            GV_Curso.DataSource = Curso.Carrega_Cursos(Titulo, Departamento, Instrutor, Inativo)
            GV_Curso.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub Carrega_Grid(Departamento As Integer, Instrutor As Integer, Titulo As String)
        Try
            GV_Curso.DataSource = Curso.Carrega_Cursos_Aluno(Departamento, Instrutor, Titulo)
            GV_Curso.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub GV_Curso_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV_Curso.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                CType(e.Row.FindControl("HL_Alterar"), HyperLink).NavigateUrl = "../Cadastros/Cad_Curso.aspx?Cod=" + Util.CInteger(e.Row.DataItem("Cod_Curso")).ToString()
                CType(e.Row.FindControl("HL_Visualizar"), HyperLink).NavigateUrl = "../Consultas/Con_Unidade.aspx?Cod=" + Util.CInteger(e.Row.DataItem("Cod_Curso")).ToString()

                If Cursos_Usuario.Select("Cod_Curso = " & e.Row.DataItem("Cod_Curso")).Length > 0 Then
                    CType(e.Row.FindControl("B_Inscrito"), Button).Visible = True
                    CType(e.Row.FindControl("B_Inscrever"), Button).Visible = False
                End If

            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_Filtrar_Click(sender As Object, e As System.EventArgs) Handles B_Filtrar.Click
        Try
            Carrega_Grid(TB_Titulo.Text, DDL_Departamento.SelectedValue, DDL_Usuario.SelectedValue, CB_Inativos.Checked)
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_Filtrar_Aluno_Click(sender As Object, e As System.EventArgs) Handles B_Filtrar_Aluno.Click
        Try
            Carrega_Grid(Usuario_Logado.Cod_Departamento, DDL_Usuario.SelectedValue, TB_Titulo.Text)
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub
    Protected Sub B_Novo_Click(sender As Object, e As System.EventArgs) Handles B_Novo.Click
        Try
            Response.Redirect("../Cadastros/Cad_Curso.aspx")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub GV_Curso_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GV_Curso.PageIndexChanging
        Try
            GV_Curso.PageIndex = e.NewPageIndex
            Carrega_Grid(TB_Titulo.Text, DDL_Departamento.SelectedValue, DDL_Usuario.SelectedValue, CB_Inativos.Checked)
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

#Region "Carrega DDLs"

    Protected Sub Preenche_DDL_Departamento()
        Try
            Dim departamento As New Departamento
            DDL_Departamento.DataSource = departamento.Carrega_Departamentos("", False)
            DDL_Departamento.DataBind()

            Dim item As New ListItem
            item.Text = "Todos"
            item.Value = 0
            DDL_Departamento.Items.Insert(0, item)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub Preenche_DDL_Usuario()
        Try
            Dim Usuario As New Usuario
            DDL_Usuario.DataSource = Usuario.Carrega_Usuarios("", False, 0)
            DDL_Usuario.DataBind()

            Dim item As New ListItem
            item.Text = "Todos"
            item.Value = 0
            DDL_Usuario.Items.Insert(0, item)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub GV_Curso_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GV_Curso.RowCommand
        Try
            If e.CommandName = "B_Inscrever" Then
                L_Titulo_U.Text = "Olá " + Usuario_Logado.Nome + ","
                L_Titulo_C.Text = Biblio.Pega_Valor("SELECT Titulo FROM Curso WHERE Cod_Curso =" + e.CommandArgument.ToString, "Titulo")
                Me.ViewState("Curso_Selecionado") = e.CommandArgument.ToString
                RegistrarScript("$('#myModalC').modal('show')")
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Fecha_Inscricao_Click(sender As Object, e As EventArgs) Handles B_Fecha_Inscricao.Click
        Try
            RegistrarScript("$('#myModalC').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Confirma_Inscricao_Click(sender As Object, e As EventArgs) Handles B_Confirma_Inscricao.Click
        Try
            Curso.Inscrever_Usuario(Usuario_Logado.Cod_Usuario, CInt(Me.ViewState("Curso_Selecionado")))
            RegistrarScript("$('#myModalC').modal('hide')")
            Carrega_Inscricoes()
            If Usuario.Verifica_Responsabilidade(Usuario_Logado.Cod_Usuario) Then
                Preenche_DDL_Departamento()
                Carrega_Grid("", 0, False, 0)
            Else
                Carrega_Grid(Usuario_Logado.Cod_Departamento, 0, "")
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

#End Region

End Class