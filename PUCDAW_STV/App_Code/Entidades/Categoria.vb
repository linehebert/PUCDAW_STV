Imports Microsoft.VisualBasic
Imports System.Data

Namespace STV.Entidades

    Public Class Categoria : Inherits STV.Base.ClasseBase

        Public Class Dados
            Public Cod_Categoria As Integer
            Public Descricao As String
            Public Categoria_Inativo As Boolean
        End Class

        Public Function Carrega_Categoria(Cod_Categoria As Integer) As Dados

            Dim Retorno As New Dados
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Categoria, Descricao, Categoria_Inativo")
            Sql.AppendLine(" FROM Categoria")
            Sql.AppendLine(" WHERE Cod_Categoria= " + Util.CString(Cod_Categoria))
            Sql.AppendLine(" ORDER BY Cod_Categoria")


            Dim Query = Biblio.Executar_Query(Sql.ToString)

            If Query.Read() Then
                Retorno.Cod_Categoria = Util.CInteger(Query("Cod_Categoria"))
                Retorno.Descricao = Util.CString(Query("Descricao"))
                Retorno.Categoria_Inativo = Util.CBoolean(Query("Categoria_Inativo"))
            End If

            Biblio.FechaConexao()

            Return Retorno
        End Function

        Public Function Carrega_Categorias(Descricao As String, Inativo As Boolean) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Categoria, Descricao, Categoria_Inativo")
            Sql.AppendLine("FROM Categoria")
            Sql.AppendLine("WHERE 0 = 0")
            If Not String.IsNullOrEmpty(Descricao) Then Sql.AppendLine("AND Descricao LIKE " + Util.Sql_String("%" + Descricao + "%"))
            If Inativo = False Then Sql.AppendLine("AND Categoria_Inativo = 0 ")
            Sql.AppendLine("ORDER BY Cod_Categoria")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Sub Alterar(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Categoria SET")
            Sql.AppendLine("Descricao = " + Util.Sql_String(Registro.Descricao))
            Sql.AppendLine(",Categoria_Inativo = " + Util.Sql_String(Registro.Categoria_Inativo))
            Sql.AppendLine("WHERE Cod_Categoria = " + Util.Sql_String(Registro.Cod_Categoria))

            Biblio.Executar_Query(Sql.ToString())
        End Sub

        Public Sub Inserir(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO Categoria (Descricao, Categoria_Inativo)")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Registro.Descricao))
            Sql.AppendLine("," + Util.Sql_String(Registro.Categoria_Inativo))
            Sql.AppendLine(")")

            Biblio.Executar_Sql(Sql.ToString())
        End Sub


    End Class

End Namespace
