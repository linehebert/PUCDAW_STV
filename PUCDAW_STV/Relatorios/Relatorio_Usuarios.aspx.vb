Imports STV.Entidades
Imports Microsoft.Reporting.WebForms
Imports STV.Seguranca

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
    Private ReadOnly Property Cod_Usuario As String
        Get
            Return Criptografia.Decryptdata(Request("Rel_User"))
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            'Verifica se o usuário está cadastrado em algum curso.
            Dim matriculas As String = Biblio.Pega_Valor("SELECT Cod_Curso FROM CURSOxUSUARIO WHERE Cod_Usuario=" + Cod_Usuario, "Cod_Curso")
            If matriculas <> "" Then
                D_Erro.Visible = False
                L_Erro.Visible = False
                Monta_Relatorio()
            Else
                D_Erro.Visible = True
                L_Erro.Visible = True
                L_Erro.Text = "Este usuário não possui relatório de acompanhamento pois não se encontra cadastrado em nenhum curso!"
            End If
        End If
    End Sub

    Protected Function Retorna_SQL() As String
        Dim SQL As New StringBuilder
        With SQL
            .Clear()
            .AppendLine("Select UN.Cod_Unidade, U.Cod_Usuario, U.NOME As NOME,C.Dt_Termino, C.Cod_Curso, C.Titulo As CURSO, '' AS APROVACAO, 0.0 AS VIDEOS, 0 AS VIDEOUNIDADETOTAL, 0 AS VIDEOUNIDADEASSISTIDO, 0.0 AS NOTAMAXIMA, 0.0 AS TOTALNOTA, 0.0 AS NOTAALUNO, UN.Titulo AS UNIDADE, A.Valor, A.Cod_Atividade, A.Titulo AS ATIVIDADE, N.Pontos AS NOTA FROM USUARIO AS U")
            .AppendLine(" LEFT JOIN CURSOxUSUARIO AS CU ON CU.Cod_Usuario = U.Cod_Usuario")
            .AppendLine(" LEFT JOIN CURSO AS C ON C.Cod_Curso = CU.Cod_Curso")
            .AppendLine(" LEFT JOIN UNIDADE AS UN ON UN.Cod_Curso = C.Cod_Curso")
            .AppendLine(" LEFT JOIN ATIVIDADE AS A ON A.Cod_Unidade = UN.Cod_Unidade")
            .AppendLine(" LEFT JOIN NOTAS AS N ON N.Cod_Atividade = A.Cod_Atividade")
            .AppendLine("WHERE 0 = 0 ")
            .AppendLine(" AND U.Cod_Usuario = " + Cod_Usuario)
        End With

        Return SQL.ToString
    End Function

    Private Sub Monta_Relatorio()
        Dim ParamList As New List(Of ReportParameter)
        Dim DT = Biblio.Retorna_DataTable(Retorna_SQL)

        For i As Integer = 0 To DT.Rows.Count - 1

            DT.Rows(i)("VIDEOUNIDADEASSISTIDO") = Curso.Quantidade_Visualizados(Util.CInteger(DT.Rows(i)("Cod_Curso")), Util.CInteger(DT.Rows(i)("Cod_Usuario")), Util.CInteger(DT.Rows(i)("Cod_Unidade")))
            DT.Rows(i)("VIDEOUNIDADETOTAL") = Curso.Quantidade_Materiais(Util.CInteger(DT.Rows(i)("Cod_Curso")), Util.CInteger(DT.Rows(i)("Cod_Unidade")))

            DT.Rows(i)("Videos") = Videos_Assistidos(Util.CInteger(DT.Rows(i)("Cod_Curso")), Util.CInteger(DT.Rows(i)("Cod_Usuario")))
            DT.Rows(i)("NOTAMAXIMA") = Curso.Nota_Maxima(Util.CInteger(DT.Rows(i)("Cod_Curso")))
            DT.Rows(i)("NOTAALUNO") = Curso.Nota_Aluno(Util.CInteger(DT.Rows(i)("Cod_Curso")), Util.CInteger(DT.Rows(i)("Cod_Usuario")))

            If Util.CDouble(DT.Rows(i)("NOTAMAXIMA")) > 0 Then DT.Rows(i)("TOTALNOTA") = (Util.CDouble(DT.Rows(i)("NOTAALUNO")) * 100) / Util.CDouble(DT.Rows(i)("NOTAMAXIMA"))

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
        Dim TotalMateriais As Integer = Curso.Quantidade_Materiais(Cod_Curso, 0)
        Dim TotalVisualizados As Integer = Curso.Quantidade_Visualizados(Cod_Curso, Cod_Usuario, 0)

        If TotalMateriais = 0 Then
            Return 0
        Else
            If TotalMateriais = TotalVisualizados Then
                Return 100
            Else
                Dim porcentagem As Double = (TotalVisualizados * 100) / TotalMateriais
                Return porcentagem
            End If
        End If
    End Function

    Private Sub B_Voltar_Click(sender As Object, e As EventArgs) Handles B_Voltar.Click
        Response.Redirect("../Consultas/Con_Usuario.aspx")
    End Sub
End Class
