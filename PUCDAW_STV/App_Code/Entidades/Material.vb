Imports System.Data

Namespace STV.Entidades

    Public Class Material : Inherits STV.Base.ClasseBase

        Public Class Dados
            Public Cod_Unidade As Integer
            Public Cod_Tipo As Integer
            Public Cod_Material As Integer
            Public Material As String
            Public Titulo As String


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

        Public Sub Alterar_Material(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Materiais SET")
            Sql.AppendLine(" Material = " + Util.Sql_String(Registro.Material))
            Sql.AppendLine("WHERE Cod_Material = " + Util.Sql_String(Registro.Cod_Material))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        'Public Function Carrega_Atividade(Cod_Atividade As Integer) As Dados

        '    Dim Retorno As New Dados
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("SELECT A.Cod_Atividade, A.Titulo, A.Dt_Abertura, A.Dt_Fechamento, A.Valor, A.Cod_Unidade, U.titulo AS Unidade, C.titulo AS Curso")
        '    Sql.AppendLine("FROM Atividade AS A")
        '    Sql.AppendLine("LEFT JOIN Unidade AS U ON U.Cod_Unidade = A.Cod_Unidade")
        '    Sql.AppendLine("LEFT JOIN Curso AS C ON C.cod_curso = U.Cod_Curso")
        '    Sql.AppendLine(" WHERE Cod_Atividade= " + Util.CString(Cod_Atividade))

        '    Dim Query = Biblio.Executar_Query(Sql.ToString)
        '    If Query.Read() Then
        '        Retorno.Cod_Atividade = Util.CInteger(Query("Cod_Atividade"))
        '        Retorno.Titulo = Util.CString(Query("Titulo"))
        '        Retorno.Dt_Abertura = Util.CString(Query("Dt_Abertura"))
        '        Retorno.Dt_Fechamento = Util.CString(Query("Dt_Fechamento"))
        '        Retorno.Valor = Util.CString(Query("Valor"))
        '        Retorno.Cod_Unidade = Util.CString(Query("Cod_Unidade"))
        '        Retorno.Unidade = Util.CString(Query("Unidade"))
        '        Retorno.Curso = Util.CString(Query("Curso"))
        '    End If
        '    Biblio.FechaConexao()

        '    Return Retorno
        'End Function

        'Public Function Carrega_Atividades(Cod_Unidade As Integer, Publica As Boolean) As DataTable
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("SELECT Cod_Atividade, Titulo, Dt_Abertura, Dt_Fechamento, Valor, Cod_Unidade, Publica")
        '    Sql.AppendLine("FROM Atividade")
        '    Sql.AppendLine("WHERE 0 = 0")
        '    If Cod_Unidade <> 0 Then Sql.AppendLine("AND Cod_Unidade = " + Util.CString(Cod_Unidade))
        '    If Publica = True Then Sql.AppendLine("AND Publica = 1")
        '    Sql.AppendLine("ORDER BY Cod_Unidade ASC")

        '    Return Biblio.Retorna_DataTable(Sql.ToString())
        'End Function

        'Public Function Carrega_Materiais(Cod_Unidade As Integer) As DataTable
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("SELECT Cod_Unidade, Cod_Tipo, Cod_Material, Material ")
        '    Sql.AppendLine("FROM Materiais")
        '    Sql.AppendLine("WHERE 0 = 0")
        '    If Cod_Unidade <> 0 Then Sql.AppendLine("AND Cod_Unidade = " + Util.CString(Cod_Unidade))
        '    Sql.AppendLine("ORDER BY Cod_Material ASC")

        '    Return Biblio.Retorna_DataTable(Sql.ToString())
        'End Function

        'Public Function Carrega_Questoes(Cod_Atividade As Integer) As DataTable
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("SELECT  Cod_Atividade, Cod_Questao, Enunciado, Alternativa_A, Alternativa_B, Alternativa_C, Alternativa_D, Alternativa_Correta, Justificativa")
        '    Sql.AppendLine(" FROM Questao")
        '    Sql.AppendLine(" WHERE 0 = 0")
        '    If Cod_Atividade <> 0 Then Sql.AppendLine("AND Cod_Atividade = " + Util.CString(Cod_Atividade))
        '    Sql.AppendLine(" ORDER BY Cod_Questao ASC")

        '    Return Biblio.Retorna_DataTable(Sql.ToString())
        'End Function

        'Public Function Carrega_Questao_Inicial(Cod_Atividade As Integer) As Dados
        '    Dim Retorno As New Dados
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("SELECT TOP 1")
        '    Sql.AppendLine(" Cod_Atividade, Cod_Questao, Enunciado, Alternativa_A, Alternativa_B, Alternativa_C, Alternativa_D, Alternativa_Correta, Justificativa")
        '    Sql.AppendLine(" FROM Questao")
        '    Sql.AppendLine(" WHERE 0 = 0")
        '    If Cod_Atividade <> 0 Then Sql.AppendLine("AND Cod_Atividade = " + Util.CString(Cod_Atividade))
        '    Sql.AppendLine(" ORDER BY Cod_Questao ASC")

        '    Dim Query = Biblio.Executar_Query(Sql.ToString)
        '    If Query.Read() Then
        '        Retorno.Cod_Atividade = Util.CInteger(Query("Cod_Atividade"))
        '        Retorno.Cod_Questao = Util.CInteger(Query("Cod_Questao"))
        '        Retorno.Enunciado = Util.CString(Query("Enunciado"))
        '        Retorno.Alternativa_A = Util.CString(Query("Alternativa_A"))
        '        Retorno.Alternativa_B = Util.CString(Query("Alternativa_B"))
        '        Retorno.Alternativa_C = Util.CString(Query("Alternativa_C"))
        '        Retorno.Alternativa_D = Util.CString(Query("Alternativa_D"))
        '        Retorno.Alternativa_Correta = Util.CString(Query("Alternativa_Correta"))
        '        Retorno.Justificativa = Util.CString(Query("Justificativa"))
        '    End If
        '    Biblio.FechaConexao()

        '    Return Retorno
        'End Function

        'Public Function Carrega_Questao(Cod_Questao As Integer) As Dados

        '    Dim Retorno As New Dados
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("SELECT Cod_Atividade, Cod_Questao, Enunciado, Alternativa_A, Alternativa_B, Alternativa_C, Alternativa_D, Alternativa_Correta, Justificativa")
        '    Sql.AppendLine("FROM Questao")
        '    Sql.AppendLine(" WHERE Cod_Questao = " + Util.CString(Cod_Questao))

        '    Dim Query = Biblio.Executar_Query(Sql.ToString)
        '    If Query.Read() Then
        '        Retorno.Cod_Atividade = Util.CInteger(Query("Cod_Atividade"))
        '        Retorno.Cod_Questao = Util.CInteger(Query("Cod_Questao"))
        '        Retorno.Enunciado = Util.CString(Query("Enunciado"))
        '        Retorno.Alternativa_A = Util.CString(Query("Alternativa_A"))
        '        Retorno.Alternativa_B = Util.CString(Query("Alternativa_B"))
        '        Retorno.Alternativa_C = Util.CString(Query("Alternativa_C"))
        '        Retorno.Alternativa_D = Util.CString(Query("Alternativa_D"))
        '        Retorno.Alternativa_Correta = Util.CString(Query("Alternativa_Correta"))
        '        Retorno.Justificativa = Util.CString(Query("Justificativa"))
        '    End If
        '    Biblio.FechaConexao()

        '    Return Retorno
        'End Function

        'Public Function Proxima_Questao(Cod_Atividade As Integer, Cod_Questao As Integer, Ordem As String) As Dados

        '    Dim Retorno As New Dados
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("SELECT TOP 1 Cod_Atividade, Cod_Questao, Enunciado, Alternativa_A, Alternativa_B, Alternativa_C, Alternativa_D, Alternativa_Correta, Justificativa")
        '    Sql.AppendLine("FROM Questao")
        '    Sql.AppendLine(" WHERE Cod_Atividade = " + Util.CString(Cod_Atividade))
        '    If Not String.IsNullOrEmpty(Ordem) And Ordem = "ASC" Then
        '        Sql.AppendLine(" AND Cod_Questao > " + Util.CString(Cod_Questao))
        '        Sql.AppendLine(" ORDER BY Cod_Questao ASC")
        '    ElseIf Not String.IsNullOrEmpty(Ordem) And Ordem = "DESC" Then
        '        Sql.AppendLine(" AND Cod_Questao < " + Util.CString(Cod_Questao))
        '        Sql.AppendLine(" ORDER BY Cod_Questao DESC")
        '    End If


        '    Dim Query = Biblio.Executar_Query(Sql.ToString)
        '    If Query.Read() Then
        '        Retorno.Cod_Atividade = Util.CInteger(Query("Cod_Atividade"))
        '        Retorno.Cod_Questao = Util.CInteger(Query("Cod_Questao"))
        '        Retorno.Enunciado = Util.CString(Query("Enunciado"))
        '        Retorno.Alternativa_A = Util.CString(Query("Alternativa_A"))
        '        Retorno.Alternativa_B = Util.CString(Query("Alternativa_B"))
        '        Retorno.Alternativa_C = Util.CString(Query("Alternativa_C"))
        '        Retorno.Alternativa_D = Util.CString(Query("Alternativa_D"))
        '        Retorno.Alternativa_Correta = Util.CString(Query("Alternativa_Correta"))
        '        Retorno.Justificativa = Util.CString(Query("Justificativa"))
        '    End If
        '    Biblio.FechaConexao()

        '    Return Retorno
        'End Function

        'Public Sub Alterar_Atividade(Registro As Dados)
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("UPDATE Atividade SET")
        '    Sql.AppendLine("Titulo = " + Util.Sql_String(Registro.Titulo))
        '    Sql.AppendLine(", Dt_Abertura= " + Util.Sql_String(Registro.Dt_Abertura))
        '    Sql.AppendLine(", Dt_Fechamento= " + Util.Sql_String(Registro.Dt_Fechamento))
        '    Sql.AppendLine(", Valor= " + Util.Sql_String(Registro.Valor))
        '    Sql.AppendLine("WHERE Cod_Atividade = " + Util.Sql_String(Registro.Cod_Atividade))

        '    Biblio.Executar_Sql(Sql.ToString())
        'End Sub

        'Public Sub Inserir_Atividade(Registro As Dados)
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("INSERT INTO Atividade (titulo, dt_abertura, dt_fechamento, valor, cod_unidade)")
        '    Sql.AppendLine("VALUES(")
        '    Sql.AppendLine(Util.Sql_String(Registro.Titulo))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Dt_Abertura))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Dt_Fechamento))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Valor))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Unidade))
        '    Sql.AppendLine(")")

        '    Biblio.Executar_Sql(Sql.ToString())
        'End Sub

        'Public Sub Inserir_Questao(Registro As Dados)
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("INSERT INTO Questao (cod_atividade, Enunciado, Alternativa_A, Alternativa_B, Alternativa_C, Alternativa_D, Alternativa_Correta, Justificativa)")
        '    Sql.AppendLine("VALUES(")
        '    Sql.AppendLine(Util.Sql_String(Registro.Cod_Atividade))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Enunciado))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Alternativa_A))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Alternativa_B))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Alternativa_C))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Alternativa_D))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Alternativa_Correta))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Justificativa))
        '    Sql.AppendLine(")")

        '    Biblio.Executar_Sql(Sql.ToString())
        'End Sub

        'Public Sub Alterar_Questao(Registro As Dados)
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("UPDATE Questao SET")
        '    Sql.AppendLine("Enunciado = " + Util.Sql_String(Registro.Enunciado))
        '    Sql.AppendLine(", Alternativa_A= " + Util.Sql_String(Registro.Alternativa_A))
        '    Sql.AppendLine(", Alternativa_B= " + Util.Sql_String(Registro.Alternativa_B))
        '    Sql.AppendLine(", Alternativa_C= " + Util.Sql_String(Registro.Alternativa_C))
        '    Sql.AppendLine(", Alternativa_D= " + Util.Sql_String(Registro.Alternativa_D))
        '    Sql.AppendLine(", Alternativa_Correta = " + Util.Sql_String(Registro.Alternativa_Correta))
        '    Sql.AppendLine(", Justificativa = " + Util.Sql_String(Registro.Justificativa))
        '    Sql.AppendLine("WHERE Cod_Questao = " + Util.Sql_String(Registro.Cod_Questao))

        '    Biblio.Executar_Sql(Sql.ToString())
        'End Sub

        'Public Sub Excluir_Questao(Registro As Dados)
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("DELETE FROM Questao")
        '    Sql.AppendLine("WHERE Cod_Questao = " + Util.Sql_String(Registro.Cod_Questao))

        '    Biblio.Executar_Sql(Sql.ToString())
        'End Sub

        'Public Sub Excluir_Conteudo_Atividade(Registro As Dados)
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("DELETE FROM Questao")
        '    Sql.AppendLine("WHERE Cod_Atividade = " + Util.Sql_String(Registro.Cod_Atividade))

        '    Biblio.Executar_Sql(Sql.ToString())
        'End Sub

        'Public Sub Excluir_Atividade(Registro As Dados)
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("DELETE FROM Atividade")
        '    Sql.AppendLine("WHERE Cod_Atividade = " + Util.Sql_String(Registro.Cod_Atividade))

        '    Biblio.Executar_Sql(Sql.ToString())
        'End Sub



        'Public Sub Alterar_Resposta(Registro As Dados)

        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("UPDATE Usuarioxrespostas SET")
        '    Sql.AppendLine("cod_usuario = " + Util.Sql_String(Registro.Cod_Usuario))
        '    Sql.AppendLine(",cod_atividade = " + Util.Sql_String(Registro.Cod_Atividade))
        '    Sql.AppendLine(",cod_questao = " + Util.Sql_String(Registro.Cod_Questao))
        '    Sql.AppendLine(",resposta = " + Util.Sql_String(Registro.Resposta))
        '    Sql.AppendLine("WHERE Cod_Usuario = " + Util.Sql_String(Registro.Cod_Usuario))
        '    Sql.AppendLine("AND Cod_Questao = " + Util.Sql_String(Registro.Cod_Questao))
        '    Sql.AppendLine("AND Cod_Atividade = " + Util.Sql_String(Registro.Cod_Atividade))

        '    Biblio.Executar_Sql(Sql.ToString())
        'End Sub

        'Public Sub Inserir_Resposta(Registro As Dados)
        '    Dim Sql As New StringBuilder
        '    Sql.AppendLine("INSERT INTO Usuarioxrespostas (cod_usuario, cod_atividade, cod_questao, resposta)")
        '    Sql.AppendLine("VALUES(")
        '    Sql.AppendLine(Util.Sql_String(Registro.Cod_Usuario))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Atividade))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Questao))
        '    Sql.AppendLine("," + Util.Sql_String(Registro.Resposta))
        '    Sql.AppendLine(")")

        '    Biblio.Executar_Sql(Sql.ToString())
        'End Sub
    End Class

End Namespace
