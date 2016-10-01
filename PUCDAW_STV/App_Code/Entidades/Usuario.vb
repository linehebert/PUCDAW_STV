Imports Microsoft.VisualBasic
Imports System.Data

Namespace STV.Entidades
    Public Class Usuario : Inherits STV.Base.ClasseBase

        Public Class Dados
            Public Cod_Usuario As Integer
            Public Nome As String
            Public Senha As String
            Public Departamento As Integer
            Public CPF As String
            Public Usuario_Inativo As Boolean
            Public Email As String
            Public Cod_Departamento As Integer
            Public Resposta As String
            Public Cod_Atividade As Integer
            Public Cod_Questao As Integer
            Public ADM As Boolean
        End Class

        Public Function Carrega_Usuario(Cod_Usuario As Integer) As Dados

            Dim Retorno As New Dados
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT cod_usuario, nome, senha, usuario_inativo, D.descricao AS Departamento, cpf, email, U.Departamento AS Cod_Departamento, ADM")
            Sql.AppendLine(" FROM Usuario as U")
            Sql.AppendLine(" LEFT JOIN Departamento AS D ON U.departamento = D.Cod_Departamento")
            Sql.AppendLine(" WHERE Cod_Usuario= " + Util.CString(Cod_Usuario))
            Sql.AppendLine(" ORDER BY Cod_Usuario")


            Dim Query = Biblio.Executar_Query(Sql.ToString)

            If Query.Read() Then
                Retorno.Cod_Usuario = Util.CInteger(Query("Cod_Usuario"))
                Retorno.Nome = Util.CString(Query("Nome"))
                Retorno.Senha = Util.CString(Query("Senha"))
                Retorno.Usuario_Inativo = Util.CBoolean(Query("Usuario_Inativo"))
                Retorno.Departamento = Util.CInteger(Query("Departamento"))
                Retorno.CPF = Util.CString(Query("CPF"))
                Retorno.Email = Util.CString(Query("Email"))
                Retorno.Cod_Departamento = Util.CInteger(Query("Cod_Departamento"))
                Retorno.ADM = Util.CString(Query("ADM"))
            End If

            Biblio.FechaConexao()

            Return Retorno
        End Function

        Public Function Carrega_Usuarios(Nome As String, Inativo As Boolean, Departamento As Integer, UserLogado As Integer, CarregarAdm As Boolean) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT cod_usuario, nome, senha, usuario_inativo,U.Departamento AS Cod_Departamento, D.descricao AS Departamento, cpf, email, ADM")
            Sql.AppendLine(" FROM Usuario as U")
            Sql.AppendLine(" LEFT JOIN Departamento AS D ON U.departamento = D.Cod_Departamento ")
            Sql.AppendLine("WHERE 0 = 0 AND U.Cod_Usuario <> " + Util.Sql_String(UserLogado))
            If Not String.IsNullOrEmpty(Nome) Then Sql.AppendLine(" AND Nome LIKE " + Util.Sql_String("%" + Nome + "%"))
            If Inativo = False Then Sql.AppendLine(" AND Usuario_Inativo = 0 ")
            If Departamento <> 0 Then Sql.AppendLine(" AND Departamento = " + Util.Sql_String(Departamento))
            If CarregarAdm = False Then Sql.AppendLine(" AND U.ADM = 0 ")
            Sql.AppendLine(" ORDER BY Cod_Usuario")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Function Carrega_User_Relatorios(Cod_Usuario As Integer) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT DISTINCT CU.Cod_Usuario, U.Nome FROM CURSOxUSUARIO AS CU")
            Sql.AppendLine(" LEFT JOIN USUARIO AS U ON U.Cod_Usuario = CU.Cod_Usuario")
            Sql.AppendLine("WHERE 0 = 0 ")
            If Cod_Usuario <> 0 Then Sql.AppendLine("And U.Cod_Usuario = " + Util.Sql_String(Cod_Usuario))
            Sql.AppendLine(" ORDER BY U.Nome")
            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Sub Alterar(Registro As Dados)

            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Usuario SET")
            Sql.AppendLine("CPF = " + Util.Sql_String(Registro.CPF))
            Sql.AppendLine(",Nome = " + Util.Sql_String(Registro.Nome))
            'Sql.AppendLine(",Senha = " + Util.Sql_String(Registro.Senha))
            Sql.AppendLine(",Usuario_Inativo = " + Util.Sql_String(Registro.Usuario_Inativo))
            Sql.AppendLine(",Departamento = " + Util.Sql_String(Registro.Cod_Departamento))
            Sql.AppendLine(",Email = " + Util.Sql_String(Registro.Email))
            Sql.AppendLine(",ADM = " + Util.Sql_String(Registro.ADM))
            Sql.AppendLine("WHERE Cod_Usuario = " + Util.Sql_String(Registro.Cod_Usuario))

            Biblio.FechaConexao()
            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Alterar_Senha(Registro As Dados)

            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Usuario SET")
            Sql.AppendLine(" Senha = " + Util.Sql_String(Registro.Senha))
            Sql.AppendLine("WHERE Cod_Usuario = " + Util.Sql_String(Registro.Cod_Usuario))

            Biblio.FechaConexao()
            Biblio.Executar_Sql(Sql.ToString())
        End Sub


        Public Function Inserir(Registro As Dados) As Integer
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO Usuario (nome, senha, usuario_inativo, departamento, CPF, EMAIL, ADM)")
            Sql.AppendLine(" Output Inserted.Cod_Usuario")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Registro.Nome))
            Sql.AppendLine("," + Util.Sql_String(Registro.Senha))
            Sql.AppendLine("," + Util.Sql_String(Registro.Usuario_Inativo))
            Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Departamento))
            Sql.AppendLine("," + Util.Sql_String(Registro.CPF))
            Sql.AppendLine("," + Util.Sql_String(Registro.Email))
            Sql.AppendLine("," + Util.Sql_String(Registro.ADM))
            Sql.AppendLine(")")

            Dim dt As DataTable = Biblio.Retorna_DataTable(Sql.ToString())

            Return Util.CInteger(dt.Rows(0).Item("Cod_Usuario"))
        End Function


        Public Function Existe_CPF(cpf_informado As String) As Boolean
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT * FROM Usuario ")
            Sql.AppendLine("WHERE CPF='" + cpf_informado + "'")

            Dim Query = Biblio.Executar_Query(Sql.ToString)
            Return Query.Read()
        End Function

        Public Function Verifica_Responsabilidade(Cod_Usuario As String) As Boolean
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT * FROM Curso ")
            Sql.AppendLine("WHERE Instrutor=" + Cod_Usuario)

            Dim Query = Biblio.Executar_Query(Sql.ToString)
            Return Query.Read()
        End Function

        Public Function Validar_Cpf(cpf As String) As Boolean
            Dim multiplicador1 As Integer() = New Integer(8) {10, 9, 8, 7, 6, 5, 4, 3, 2}
            Dim multiplicador2 As Integer() = New Integer(9) {11, 10, 9, 8, 7, 6, 5, 4, 3, 2}
            Dim tempCpf As String
            Dim digito As String
            Dim soma As Integer
            Dim resto As Integer
            cpf = cpf.Trim()
            cpf = cpf.Replace(".", "").Replace("-", "")
            If cpf.Length <> 11 Then
                Return False
            End If
            tempCpf = cpf.Substring(0, 9)
            soma = 0

            For i As Integer = 0 To 8
                soma += Integer.Parse(tempCpf(i).ToString()) * multiplicador1(i)
            Next
            resto = soma Mod 11
            If resto < 2 Then
                resto = 0
            Else
                resto = 11 - resto
            End If
            digito = resto.ToString()
            tempCpf = tempCpf & digito
            soma = 0
            For i As Integer = 0 To 9
                soma += Integer.Parse(tempCpf(i).ToString()) * multiplicador2(i)
            Next
            resto = soma Mod 11
            If resto < 2 Then
                resto = 0
            Else
                resto = 11 - resto
            End If
            digito = digito & resto.ToString()
            Return cpf.EndsWith(digito)
        End Function

    End Class

End Namespace
