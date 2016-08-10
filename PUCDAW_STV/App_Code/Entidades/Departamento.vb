Imports Microsoft.VisualBasic
Imports System.Data

Namespace STV.Entidades

    Public Class Departamento : Inherits STV.Base.ClasseBase

        Public Class Dados
            Public Cod_Departamento As Integer
            Public Descricao As String
            Public Departamento_Inativo As Boolean
        End Class

        Public Function Carrega_Departamento(Cod_Departamento As Integer) As Dados

            Dim Retorno As New Dados
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Departamento, Descricao, Departamento_Inativo")
            Sql.AppendLine(" FROM Departamento")
            Sql.AppendLine(" WHERE Cod_Departamento= " + Util.CString(Cod_Departamento))
            Sql.AppendLine(" ORDER BY Cod_departamento")


            Dim Query = Biblio.Executar_Query(Sql.ToString)

            If Query.Read() Then
                Retorno.Cod_Departamento = Util.CInteger(Query("Cod_Departamento"))
                Retorno.Descricao = Util.CString(Query("Descricao"))
                Retorno.Departamento_Inativo = Util.CBoolean(Query("Departamento_Inativo"))
            End If

            Biblio.FechaConexao()

            Return Retorno
        End Function

        Public Function Carrega_Departamentos(Descricao As String, Inativo As Boolean) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Departamento, Descricao, Departamento_Inativo")
            Sql.AppendLine("FROM Departamento")
            Sql.AppendLine("WHERE 0 = 0")
            If Not String.IsNullOrEmpty(Descricao) Then Sql.AppendLine("AND Descricao LIKE " + Util.Sql_String("%" + Descricao + "%"))
            If Inativo = False Then Sql.AppendLine("AND Departamento_Inativo = 0 ")
            Sql.AppendLine("ORDER BY Cod_departamento")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Sub Alterar(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Departamento SET")
            Sql.AppendLine("Descricao = " + Util.Sql_String(Registro.Descricao))
            Sql.AppendLine(",Departamento_Inativo = " + Util.Sql_String(Registro.Departamento_Inativo))
            Sql.AppendLine("WHERE Cod_Departamento = " + Util.Sql_String(Registro.Cod_Departamento))

            Biblio.Executar_Query(Sql.ToString())
        End Sub

        Public Sub Inserir(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO Departamento (Descricao, Departamento_Inativo)")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Registro.Descricao))
            Sql.AppendLine("," + Util.Sql_String(Registro.Departamento_Inativo))
            Sql.AppendLine(")")

            Biblio.Executar_Sql(Sql.ToString())
        End Sub


    End Class

End Namespace
