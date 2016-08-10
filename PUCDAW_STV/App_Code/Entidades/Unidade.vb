Imports Microsoft.VisualBasic
Imports System.Data

Namespace STV.Entidades

    Public Class Unidade : Inherits STV.Base.ClasseBase

        Public Class Dados
            Public Cod_Unidade As Integer
            Public Titulo As String
            Public Cod_Curso As Integer
            Public Curso As String
        End Class

        Public Function Carrega_Unidade(Cod_Unidade As Integer) As Dados

            Dim Retorno As New Dados
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT U.Cod_Unidade, U.Titulo, U.Cod_Curso, C.titulo AS Curso")
            Sql.AppendLine("FROM Unidade AS U")
            Sql.AppendLine("LEFT JOIN Curso as C ON C.cod_curso = U.Cod_Curso")
            Sql.AppendLine(" WHERE Cod_Unidade= " + Util.CString(Cod_Unidade))
            Sql.AppendLine(" ORDER BY Cod_Unidade ASC")

            Dim Query = Biblio.Executar_Query(Sql.ToString)
            If Query.Read() Then
                Retorno.Cod_Unidade = Util.CInteger(Query("Cod_Unidade"))
                Retorno.Titulo = Util.CString(Query("Titulo"))
                Retorno.Cod_Curso = Util.CInteger(Query("Cod_Curso"))
                Retorno.Curso = Util.CString(Query("Curso"))
            End If
            Biblio.FechaConexao()

            Return Retorno
        End Function

        Public Function Carrega_Unidades(Cod_Curso As Integer) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Unidade, Titulo, Cod_Curso")
            Sql.AppendLine("FROM Unidade")
            Sql.AppendLine("WHERE 0 = 0")
            If Cod_Curso <> 0 Then Sql.AppendLine("AND Cod_Curso = " + Util.CString(Cod_Curso))
            Sql.AppendLine("ORDER BY Cod_Unidade ASC")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Sub Inserir(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO Unidade (Titulo, Cod_Curso)")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Registro.Titulo))
            Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Curso))
            Sql.AppendLine(")")

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Excluir_Unidade(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("DELETE FROM Unidade")
            Sql.AppendLine("WHERE Cod_Unidade = " + Util.Sql_String(Registro.Cod_Unidade))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Alterar(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Unidade SET")
            Sql.AppendLine("Titulo = " + Util.Sql_String(Registro.Titulo))
            Sql.AppendLine("WHERE Cod_Unidade = " + Util.Sql_String(Registro.Cod_Unidade))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub




    End Class

End Namespace
