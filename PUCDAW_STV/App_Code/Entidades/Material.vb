Imports System.Data

Namespace STV.Entidades

    Public Class Material : Inherits STV.Base.ClasseBase

        Public Class Dados
            Public Cod_Unidade As Integer
            Public Cod_Tipo As Integer
            Public Cod_Material As Integer
            Public Material As String
            Public Titulo As String
            Public Cod_Usuario As Integer

        End Class
        Public Function Carrega_Materiais(Cod_Unidade As Integer) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Unidade, Cod_Tipo, Cod_Material, Material, Titulo ")
            Sql.AppendLine("FROM Materiais")
            Sql.AppendLine("WHERE 0 = 0")
            If Cod_Unidade <> 0 Then Sql.AppendLine("AND Cod_Unidade = " + Util.CString(Cod_Unidade))
            Sql.AppendLine("ORDER BY Cod_Material ASC")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Function Carrega_Material(Cod_Material As Integer) As Dados

            Dim Retorno As New Dados
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Unidade, Cod_Tipo, Cod_Material, Material, Titulo")
            Sql.AppendLine("FROM Materiais")
            Sql.AppendLine("WHERE Cod_Material= " + Util.CString(Cod_Material))

            Dim Query = Biblio.Executar_Query(Sql.ToString)
            If Query.Read() Then
                Retorno.Cod_Unidade = Util.CInteger(Query("Cod_Unidade"))
                Retorno.Cod_Tipo = Util.CInteger(Query("Cod_Tipo"))
                Retorno.Cod_Material = Util.CInteger(Query("Cod_Material"))
                Retorno.Material = Util.CString(Query("Material"))
                Retorno.Titulo = Util.CString(Query("Titulo"))
            End If

            Biblio.FechaConexao()

            Return Retorno
        End Function

        Public Function Carrega_Tipo() As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Tipo, Descricao")
            Sql.AppendLine("FROM Tipo_Material")
            Sql.AppendLine("WHERE 0 = 0")
            Sql.AppendLine("ORDER BY Cod_Tipo ASC")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Function Inserir_Material(Registro As Dados) As Integer
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO Materiais (Cod_Unidade, Cod_Tipo, Titulo, Material)")
            Sql.AppendLine(" Output Inserted.Cod_Material")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Registro.Cod_Unidade))
            Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Tipo))
            Sql.AppendLine("," + Util.Sql_String(Registro.Titulo))
            Sql.AppendLine("," + Util.Sql_String(Registro.Material))
            Sql.AppendLine(")")

            Dim dt As DataTable = Biblio.Retorna_DataTable(Sql.ToString())
            Return Util.CInteger(dt.Rows(0).Item("Cod_Material"))
        End Function

        Public Sub Visualiza_Material(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO MATERIAISxUSUARIO (Cod_Usuario, Cod_Material) ")
            Sql.AppendLine("VALUES(" + Util.Sql_String(Registro.Cod_Usuario))
            Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Material))
            Sql.AppendLine(")")

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Alterar_Material(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Materiais SET")
            Sql.AppendLine(" Material = " + Util.Sql_String(Registro.Material))
            Sql.AppendLine("WHERE Cod_Material = " + Util.Sql_String(Registro.Cod_Material))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Excluir_Material(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("DELETE FROM MATERIAISxUSUARIO ")
            Sql.AppendLine("WHERE Cod_Material = " + Util.Sql_String(Registro.Cod_Material))

            Sql.AppendLine("DELETE FROM Materiais ")
            Sql.AppendLine("WHERE Cod_Material = " + Util.Sql_String(Registro.Cod_Material))
            Biblio.Executar_Sql(Sql.ToString())
        End Sub

    End Class

End Namespace
