

Imports Microsoft.Reporting.WebForms

Partial Class Relatorios_Relatorio_Usuarios : Inherits STV.Base.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
        End If
    End Sub

    Protected Function Retorna_SQL() As String
        Dim SQL As New StringBuilder
        With SQL
            .Clear()
            .AppendLine("SELECT cod_usuario, nome, cpf, email FROM USUARIO")
            .AppendLine("WHERE 0 = 0")
        End With

        Return SQL.ToString
    End Function

    Private Sub Monta_Relatorio()
        Dim ParamList As New List(Of ReportParameter)
        Dim DT = Biblio.Retorna_DataTable(Retorna_SQL)
        'With ParamList
        '    .Add(New ReportParameter("Nome", Biblio.Pega_Conteudo("SELECT Nome FROM Sys_Dados_Empresa", "Nome")))
        '    .Add(New ReportParameter("AnoMes", "Ano / Mês:          " + TB_Ano.Text + "      / " + TB_Mes.Text))
        'End With
        With RV
            .LocalReport.DataSources.Clear()
            .LocalReport.DataSources.Add(New ReportDataSource("Lista", DT))
            .LocalReport.SetParameters(ParamList)
            .LocalReport.Refresh()
            .Visible = True
        End With
    End Sub

    Private Sub B_Filtrar_Aluno_Click(sender As Object, e As EventArgs) Handles B_Filtrar_Aluno.Click
        Monta_Relatorio()
    End Sub
End Class
