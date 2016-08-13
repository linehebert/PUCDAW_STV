Imports STV.Entidades
Imports STV.Seguranca

Partial Class Consultas_Conteudo : Inherits STV.Base.Page

    Dim _Curso As Curso
    Private ReadOnly Property Curso As Curso
        Get
            If IsNothing(_Curso) Then _
                _Curso = New Curso
            Return _Curso
        End Get
    End Property

    Dim _Unidade As Unidade
    Private ReadOnly Property Unidade As Unidade
        Get
            If IsNothing(_Unidade) Then _
                _Unidade = New Unidade

            Return _Unidade
        End Get
    End Property

    Private ReadOnly Property Cod_Curso As String
        Get
            Return Criptografia.Decryptdata(Request("Cod"))
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

    Dim _Atividade As Atividade
    Private ReadOnly Property Atividade As Atividade
        Get
            If IsNothing(_Atividade) Then _
                _Atividade = New Atividade
            Return _Atividade
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
            If Not Page.IsPostBack() Then
                Monta_Dados()
                Carrega_Unidades(Cod_Curso)


            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub Monta_Dados()
        Try
            Dim Dado = Curso.Carrega_Curso(Cod_Curso)
            L_Curso_Unidade.Text = Dado.Titulo
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub Carrega_Unidades(Cod_Curso As Integer)
        Try
            rptUnidades.DataSource = Unidade.Carrega_Unidades(Cod_Curso)
            rptUnidades.DataBind()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub Carrega_Atividades(Cod_Unidade As Integer)
        Try
            rptUnidades.DataSource = Atividade.Carrega_Atividades(Cod_Unidade, True)
            rptUnidades.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub rptUnidades_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptUnidades.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rptAtividades As Repeater = CType(e.Item.FindControl("rptAtividades"), Repeater)
            Dim Dt As Data.DataTable = Atividade.Carrega_Atividades(CInt(e.Item.DataItem("Cod_Unidade")), True)
            rptAtividades.DataSource = Dt
            rptAtividades.DataBind()

            If Dt.Rows.Count = 0 Then
                Dim Footer As Control = rptAtividades.Controls(rptAtividades.Controls.Count - 1).Controls(0)
                CType(Footer.FindControl("L_Empty"), Label).Visible = True
            End If


        End If
    End Sub
End Class
