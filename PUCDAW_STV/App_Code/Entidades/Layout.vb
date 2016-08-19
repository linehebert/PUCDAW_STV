Imports Microsoft.VisualBasic
Imports System.Data


Namespace STV.Entidades
    Public Class Layout : Inherits STV.Base.ClasseBase

        Public Class Dados
            Public Cod_Tema As Integer
            Public Cor As String
            Public Atual As Boolean
        End Class

        Public Function Carrega_Tema() As DataTable
            Dim Retorno As New Dados
            Dim Sql As New StringBuilder

            Sql.AppendLine("SELECT Cod_Tema, Cor, Atual")
            Sql.AppendLine(" FROM Layout")
            Sql.AppendLine(" WHERE 0 = 0")

            Dim Query = Biblio.Executar_Query(Sql.ToString)
            If Query.Read() Then
                Retorno.Cod_Tema = Util.CInteger(Query("Cod_Tema"))
                Retorno.Cor = Util.CString(Query("Cor"))
                Retorno.Atual = Util.CBoolean(Query("Atual"))
            End If

            Biblio.FechaConexao()
            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function


        Public Function Carrega_Temas(Cor As String) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Tema, Cor, Atual")
            Sql.AppendLine(" FROM Layout")
            Sql.AppendLine(" WHERE 0 = 0")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function


#Region "Tema"
        Public Sub Exclui_Tema_Antigo()

            Dim Sqli As New StringBuilder
            Sqli.AppendLine("UPDATE Layout SET")
            Sqli.AppendLine("Atual = 0")

            Biblio.FechaConexao()
            Biblio.Executar_Sql(Sqli.ToString())

        End Sub

        Public Sub Alterar(Registro As Dados)

            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Layout SET")
            Sql.AppendLine("Atual = 1")
            Sql.AppendLine("WHERE Cod_Tema = " + Util.Sql_String(Registro.Cod_Tema))

            Biblio.FechaConexao()
            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Function Retorna_Tema() As String
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cor FROM Layout ")
            Sql.AppendLine("WHERE Atual=1")

            Return Biblio.Pega_Valor(Sql.ToString, "Cor")
        End Function

        'Public Function Retorna_Tema(ByVal Cod_Tema As Integer) As String

        '    Dim Tema As String = "Padrao"
        '    Select Case Cod_Tema
        '        Case "1" : Tema = "Amarelo"
        '        Case "2" : Tema = "Vermelho"
        '        Case Else : Tema = "Padrao"
        '    End Select


        '    Return Tema
        'End Function
        'Public Function Retorna_Tema() As String
        '    Return Retorna_Tema(False)
        'End Function
#End Region

    End Class
End Namespace
