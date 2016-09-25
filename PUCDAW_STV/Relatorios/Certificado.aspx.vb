Imports STV.Entidades
Imports System.IO
Imports STV.Seguranca
Imports Microsoft.Reporting.WebForms
Partial Class Relatorios_Certificado : Inherits STV.Base.Page

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
    Private ReadOnly Property Cod_Curso As Integer
        Get
            Return Criptografia.Decryptdata(Request("Curso"))
        End Get
    End Property
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Monta_Relatorio()
        End If
    End Sub

    Protected Function Retorna_SQL() As String
        Dim SQL As New StringBuilder
        With SQL
            .Clear()
            .AppendLine("SELECT C.Titulo, C.Dt_Inicio, C.Dt_Termino, U.Nome AS Instrutor, Us.Nome as Aluno")
            .AppendLine(" FROM Curso C")
            .AppendLine(" INNER JOIN USUARIO U ON U.Cod_Usuario = C.Instrutor")
            .AppendLine(" INNER JOIN CURSOxUSUARIO CU ON CU.Cod_Curso = C.Cod_Curso")
            .AppendLine(" INNER JOIN USUARIO Us ON Us.Cod_Usuario = CU.Cod_Usuario")
            .AppendLine(" WHERE 0 = 0")
            .AppendLine(" AND C.Cod_Curso =" + Util.Sql_Numero(Cod_Curso))
            .AppendLine(" AND CU.Cod_Usuario =" + Util.Sql_Numero(Usuario_Logado.Cod_Usuario))
        End With

        Return SQL.ToString
    End Function

    Private Sub Monta_Relatorio()
        Dim ParamList As New List(Of ReportParameter)
        Dim DT = Biblio.Retorna_DataTable(Retorna_SQL)

        With RV
            .LocalReport.DataSources.Clear()
            .LocalReport.DataSources.Add(New ReportDataSource("Lista", DT))
            .LocalReport.SetParameters(ParamList)
            .LocalReport.Refresh()
            .Visible = True
        End With
    End Sub

End Class