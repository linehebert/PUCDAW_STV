Imports STV.Entidades
Imports Microsoft.Reporting.WebForms

Partial Class Relatorios_Relatorio_Usuarios : Inherits STV.Base.Page
    Dim _Usuario As Usuario
    Private ReadOnly Property Usuario As Usuario
        Get
            If IsNothing(_Usuario) Then _
                _Usuario = New Usuario

            Return _Usuario
        End Get
    End Property

    Dim _Curso As Curso
    Private ReadOnly Property Curso As Curso
        Get
            If IsNothing(_Curso) Then _
                _Curso = New Curso
            Return _Curso
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Preenche_DDL_Usuarios()
        End If
    End Sub

    Protected Function Retorna_SQL() As String
        Dim SQL As New StringBuilder
        With SQL
            .Clear()
            .AppendLine("SELECT U.Cod_Usuario, U.NOME AS NOME,C.Dt_Termino, C.Cod_Curso, C.Titulo AS CURSO, '' AS APROVACAO, 0.0 AS VIDEOS, 0.0 AS NOTAMAXIMA, 0.0 AS TOTALNOTA, 0.0 AS NOTAALUNO, UN.Titulo AS UNIDADE, A.Valor, A.Cod_Atividade, A.Titulo AS ATIVIDADE, N.Pontos AS NOTA FROM USUARIO AS U")
            .AppendLine(" LEFT JOIN CURSOxUSUARIO AS CU ON CU.Cod_Usuario = U.Cod_Usuario")
            .AppendLine(" LEFT JOIN CURSO AS C ON C.Cod_Curso = CU.Cod_Curso")
            .AppendLine(" LEFT JOIN UNIDADE AS UN ON UN.Cod_Curso = C.Cod_Curso")
            .AppendLine(" LEFT JOIN ATIVIDADE AS A ON A.Cod_Unidade = UN.Cod_Unidade")
            .AppendLine(" LEFT JOIN NOTAS AS N ON N.Cod_Atividade = A.Cod_Atividade")
            .AppendLine("WHERE 0 = 0 ")
            .AppendLine(" AND U.Cod_Usuario = " + DDL_Usuario.SelectedValue)
        End With

        Return SQL.ToString
    End Function

    Private Sub Monta_Relatorio()
        Dim ParamList As New List(Of ReportParameter)
        Dim DT = Biblio.Retorna_DataTable(Retorna_SQL)

        For i As Integer = 0 To DT.Rows.Count - 1

            DT.Rows(i)("Videos") = Videos_Assistidos(Util.CInteger(DT.Rows(i)("Cod_Curso")), Util.CInteger(DT.Rows(i)("Cod_Usuario")))
            DT.Rows(i)("NOTAMAXIMA") = Curso.Nota_Maxima(Util.CInteger(DT.Rows(i)("Cod_Curso")))
            DT.Rows(i)("NOTAALUNO") = Curso.Nota_Aluno(Util.CInteger(DT.Rows(i)("Cod_Curso")), Util.CInteger(DT.Rows(i)("Cod_Usuario")))
            DT.Rows(i)("TOTALNOTA") = (Util.CDouble(DT.Rows(i)("NOTAALUNO")) * 100) / Util.CDouble(DT.Rows(i)("NOTAMAXIMA"))
            If DT.Rows(i)("TOTALNOTA") >= 70 Then
                DT.Rows(i)("APROVACAO") = "APROVADO"
            Else
                DT.Rows(i)("APROVACAO") = "REPROVADO"
            End If
        Next
        DT.AcceptChanges()

        With RV
            .LocalReport.DataSources.Clear()
            .LocalReport.DataSources.Add(New ReportDataSource("Lista", DT))
            .LocalReport.SetParameters(ParamList)
            .LocalReport.Refresh()
            .Visible = True
        End With
    End Sub

    Public Function Videos_Assistidos(Cod_Curso As Integer, Cod_Usuario As Integer) As Double
        'Verificar se visualizou todos os materiais
        Dim TotalMateriais As Integer = Curso.Quantidade_Materiais(Cod_Curso)
        Dim TotalVisualizados As Integer = Curso.Quantidade_Visualizados(Cod_Curso, Cod_Usuario)

        If TotalMateriais = TotalVisualizados Then
            Return 100
        Else
            Dim porcentagem As Double = (TotalVisualizados * 100) / TotalMateriais
            Return porcentagem
        End If
    End Function

    Private Sub B_Filtrar_Aluno_Click(sender As Object, e As EventArgs) Handles B_Filtrar_Aluno.Click
        D_Erro.Visible = False
        L_Erro.Text = ""
        L_Erro.Visible = False
        Monta_Relatorio()
        'If DDL_Usuario.SelectedValue <> 0 Then

        'Else
        '    D_Erro.Visible = True
        '    L_Erro.Visible = True
        '    L_Erro.Text = "Selecione um usuário para emitir o relatório"
        'End If


    End Sub

    Protected Sub Preenche_DDL_Usuarios()
        Try
            Dim usuario As New Usuario
            DDL_Usuario.DataSource = usuario.Carrega_User_Relatorios(0)
            DDL_Usuario.DataBind()

            Dim item As New ListItem
            item.Text = "Selecione o usuário"
            item.Value = 0
            DDL_Usuario.Items.Insert(0, item)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
