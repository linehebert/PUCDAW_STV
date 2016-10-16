Imports STV.Entidades
Imports STV.Seguranca
Partial Class Consultas_Avaliacoes : Inherits STV.Base.Page
    Dim _Curso As Curso
    Private ReadOnly Property Curso As Curso
        Get
            If IsNothing(_Curso) Then _
                _Curso = New Curso
            Return _Curso
        End Get
    End Property
    Private ReadOnly Property Cod_Curso As String
        Get
            Return Criptografia.Decryptdata(Request("AvCurso"))
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then

            Dim Dado = Curso.Carrega_Curso(Cod_Curso)
            titulo_curso.InnerText = Dado.Titulo

            'verifica curso encerrado
            If Dado.Dt_Termino < Date.Today Then
                Div_Finalizado.Visible = True
            Else
                Div_Finalizado.Visible = False
            End If

            Dim qntd_avaliacao As String = Biblio.Pega_Valor("SELECT Comentario FROM Avaliacao WHERE Cod_Curso=" + Cod_Curso, "Comentario")
            If qntd_avaliacao = Nothing Then
                Nenhum_Comentario.Visible = True
            Else
                Nenhum_Comentario.visible = False
            End If

            Carrega_Avaliacoes(Cod_Curso)
            End If
    End Sub

    Private Sub Carrega_Avaliacoes(Cod_Curso As Integer)
        Try
            rptAvaliacoes.DataSource = Curso.Carrega_Comentarios(Cod_Curso)
            rptAvaliacoes.DataBind()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub B_Voltar_Click(sender As Object, e As EventArgs) Handles B_Voltar.Click
        Response.Redirect("../Consultas/Con_Curso.aspx")
    End Sub

    Private Sub rptAvaliacoes_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptAvaliacoes.ItemDataBound
        If e.Item.DataItem("Avaliacao") = 1 Then CType(e.Item.FindControl("Image1"), Image).Visible = True
        If e.Item.DataItem("Avaliacao") = 2 Then CType(e.Item.FindControl("Image2"), Image).Visible = True
        If e.Item.DataItem("Avaliacao") = 3 Then CType(e.Item.FindControl("Image3"), Image).Visible = True
        If e.Item.DataItem("Avaliacao") = 4 Then CType(e.Item.FindControl("Image4"), Image).Visible = True
        If e.Item.DataItem("Avaliacao") = 5 Then CType(e.Item.FindControl("Image5"), Image).Visible = True
    End Sub
End Class
