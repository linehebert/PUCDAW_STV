﻿Imports System.Data

Namespace STV.Entidades

    Public Class Atividade : Inherits STV.Base.ClasseBase

        Public Class Dados
            Public Cod_Atividade As Integer
            Public Cod_Curso As Integer
            Public Titulo As String
            ' Public Dt_Abertura As Date
            Public Dt_Fechamento As Date
            Public Valor As Double
            Public Cod_Unidade As Integer
            Public Unidade As String
            Public Curso As String
            Public Enunciado As String
            Public Alternativa_A As String
            Public Alternativa_B As String
            Public Alternativa_C As String
            Public Alternativa_D As String
            Public Alternativa_Correta As String
            Public Justificativa As String
            Public Cod_Questao As Integer
            Public Cod_Usuario As Integer
            Public Resposta As String
            Public Correta As Integer
            Public Pontos As Double

        End Class

        Public Function Carrega_Atividade(Cod_Atividade As Integer) As Dados

            Dim Retorno As New Dados
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT A.Cod_Atividade, A.Titulo, A.Dt_Fechamento, A.Valor, A.Cod_Unidade, U.titulo AS Unidade, C.titulo AS Curso, C.Cod_Curso")
            Sql.AppendLine("FROM Atividade AS A")
            Sql.AppendLine("LEFT JOIN Unidade AS U ON U.Cod_Unidade = A.Cod_Unidade")
            Sql.AppendLine("LEFT JOIN Curso AS C ON C.cod_curso = U.Cod_Curso")
            Sql.AppendLine(" WHERE Cod_Atividade= " + Util.CString(Cod_Atividade))

            Dim Query = Biblio.Executar_Query(Sql.ToString)
            If Query.Read() Then
                Retorno.Cod_Atividade = Util.CInteger(Query("Cod_Atividade"))
                Retorno.Titulo = Util.CString(Query("Titulo"))
                'Retorno.Dt_Abertura = Util.CString(Query("Dt_Abertura"))
                Retorno.Dt_Fechamento = Util.CString(Query("Dt_Fechamento"))
                Retorno.Valor = Util.CString(Query("Valor"))
                Retorno.Cod_Unidade = Util.CString(Query("Cod_Unidade"))
                Retorno.Unidade = Util.CString(Query("Unidade"))
                Retorno.Curso = Util.CString(Query("Curso"))
                Retorno.Cod_Curso = Util.CString(Query("Cod_Curso"))
            End If
            Biblio.FechaConexao()

            Return Retorno
        End Function

        Public Function Carrega_Atividades(Cod_Unidade As Integer, Publica As Boolean) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Atividade, Titulo, Dt_Fechamento, Valor, Cod_Unidade, Publica")
            Sql.AppendLine("FROM Atividade")
            Sql.AppendLine("WHERE 0 = 0")
            If Cod_Unidade <> 0 Then Sql.AppendLine("AND Cod_Unidade = " + Util.CString(Cod_Unidade))
            If Publica = True Then Sql.AppendLine("AND Publica = 1")
            Sql.AppendLine("ORDER BY Cod_Unidade ASC")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function


        'Carrega a atividade resolvida e finalizada pelo aluno
        Public Function Carrega_Atividade_Aluno(Cod_Atividade As Integer) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT  q.cod_questao, q.Enunciado, q.Alternativa_A, q.Alternativa_B, q.Alternativa_C, q.Alternativa_D, q.Justificativa, q.Alternativa_Correta, ur.resposta ")
            Sql.AppendLine(" FROM Questao as q")
            Sql.AppendLine("left join USUARIOxRESPOSTAS as UR on q.Cod_Questao = ur.Cod_Questao")
            Sql.AppendLine(" WHERE 0 = 0")
            If Cod_Atividade <> 0 Then Sql.AppendLine("AND q.Cod_Atividade = " + Util.CString(Cod_Atividade))
            Sql.AppendLine(" ORDER BY q.Cod_Questao ASC")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Function Carrega_Questoes(Cod_Atividade As Integer) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT  Cod_Atividade, Cod_Questao, Enunciado, Alternativa_A, Alternativa_B, Alternativa_C, Alternativa_D, Alternativa_Correta, Justificativa")
            Sql.AppendLine(" FROM Questao")
            Sql.AppendLine(" WHERE 0 = 0")
            If Cod_Atividade <> 0 Then Sql.AppendLine("AND Cod_Atividade = " + Util.CString(Cod_Atividade))
            Sql.AppendLine(" ORDER BY Cod_Questao ASC")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Function Carrega_Questao_Inicial(Cod_Atividade As Integer) As Dados
            Dim Retorno As New Dados
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT TOP 1")
            Sql.AppendLine(" Cod_Atividade, Cod_Questao, Enunciado, Alternativa_A, Alternativa_B, Alternativa_C, Alternativa_D, Alternativa_Correta, Justificativa")
            Sql.AppendLine(" FROM Questao")
            Sql.AppendLine(" WHERE 0 = 0")
            If Cod_Atividade <> 0 Then Sql.AppendLine("AND Cod_Atividade = " + Util.CString(Cod_Atividade))
            Sql.AppendLine(" ORDER BY Cod_Questao ASC")

            Dim Query = Biblio.Executar_Query(Sql.ToString)
            If Query.Read() Then
                Retorno.Cod_Atividade = Util.CInteger(Query("Cod_Atividade"))
                Retorno.Cod_Questao = Util.CInteger(Query("Cod_Questao"))
                Retorno.Enunciado = Util.CString(Query("Enunciado"))
                Retorno.Alternativa_A = Util.CString(Query("Alternativa_A"))
                Retorno.Alternativa_B = Util.CString(Query("Alternativa_B"))
                Retorno.Alternativa_C = Util.CString(Query("Alternativa_C"))
                Retorno.Alternativa_D = Util.CString(Query("Alternativa_D"))
                Retorno.Alternativa_Correta = Util.CString(Query("Alternativa_Correta"))
                Retorno.Justificativa = Util.CString(Query("Justificativa"))
            End If
            Biblio.FechaConexao()

            Return Retorno
        End Function

        Public Function Carrega_Questao(Cod_Questao As Integer) As Dados

            Dim Retorno As New Dados
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT Cod_Atividade, Cod_Questao, Enunciado, Alternativa_A, Alternativa_B, Alternativa_C, Alternativa_D, Alternativa_Correta, Justificativa")
            Sql.AppendLine("FROM Questao")
            Sql.AppendLine(" WHERE Cod_Questao = " + Util.CString(Cod_Questao))

            Dim Query = Biblio.Executar_Query(Sql.ToString)
            If Query.Read() Then
                Retorno.Cod_Atividade = Util.CInteger(Query("Cod_Atividade"))
                Retorno.Cod_Questao = Util.CInteger(Query("Cod_Questao"))
                Retorno.Enunciado = Util.CString(Query("Enunciado"))
                Retorno.Alternativa_A = Util.CString(Query("Alternativa_A"))
                Retorno.Alternativa_B = Util.CString(Query("Alternativa_B"))
                Retorno.Alternativa_C = Util.CString(Query("Alternativa_C"))
                Retorno.Alternativa_D = Util.CString(Query("Alternativa_D"))
                Retorno.Alternativa_Correta = Util.CString(Query("Alternativa_Correta"))
                Retorno.Justificativa = Util.CString(Query("Justificativa"))
            End If
            Biblio.FechaConexao()

            Return Retorno
        End Function

        Public Function Proxima_Questao(Cod_Atividade As Integer, Cod_Questao As Integer, Ordem As String) As Dados

            Dim Retorno As New Dados
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT TOP 1 Cod_Atividade, Cod_Questao, Enunciado, Alternativa_A, Alternativa_B, Alternativa_C, Alternativa_D, Alternativa_Correta, Justificativa")
            Sql.AppendLine("FROM Questao")
            Sql.AppendLine(" WHERE Cod_Atividade = " + Util.CString(Cod_Atividade))
            If Not String.IsNullOrEmpty(Ordem) And Ordem = "ASC" Then
                Sql.AppendLine(" AND Cod_Questao > " + Util.CString(Cod_Questao))
                Sql.AppendLine(" ORDER BY Cod_Questao ASC")
            ElseIf Not String.IsNullOrEmpty(Ordem) And Ordem = "DESC" Then
                Sql.AppendLine(" AND Cod_Questao < " + Util.CString(Cod_Questao))
                Sql.AppendLine(" ORDER BY Cod_Questao DESC")
            End If


            Dim Query = Biblio.Executar_Query(Sql.ToString)
            If Query.Read() Then
                Retorno.Cod_Atividade = Util.CInteger(Query("Cod_Atividade"))
                Retorno.Cod_Questao = Util.CInteger(Query("Cod_Questao"))
                Retorno.Enunciado = Util.CString(Query("Enunciado"))
                Retorno.Alternativa_A = Util.CString(Query("Alternativa_A"))
                Retorno.Alternativa_B = Util.CString(Query("Alternativa_B"))
                Retorno.Alternativa_C = Util.CString(Query("Alternativa_C"))
                Retorno.Alternativa_D = Util.CString(Query("Alternativa_D"))
                Retorno.Alternativa_Correta = Util.CString(Query("Alternativa_Correta"))
                Retorno.Justificativa = Util.CString(Query("Justificativa"))
            End If
            Biblio.FechaConexao()

            Return Retorno
        End Function

        Public Sub Alterar_Atividade(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Atividade SET")
            Sql.AppendLine("Titulo = " + Util.Sql_String(Registro.Titulo))
            ' Sql.AppendLine(", Dt_Abertura= " + Util.Sql_String(Registro.Dt_Abertura))
            Sql.AppendLine(", Dt_Fechamento= " + Util.Sql_String(Registro.Dt_Fechamento))
            Sql.AppendLine(", Valor= " + Util.Sql_String(Registro.Valor))
            Sql.AppendLine("WHERE Cod_Atividade = " + Util.Sql_String(Registro.Cod_Atividade))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Publicar_Atividade(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Atividade SET Publica = 1 ")
            Sql.AppendLine("WHERE Cod_Atividade = " + Util.Sql_String(Registro.Cod_Atividade))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Inserir_Atividade(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO Atividade (titulo,  dt_fechamento, valor, cod_unidade)")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Registro.Titulo))
            'Sql.AppendLine("," + Util.Sql_String(Registro.Dt_Abertura))
            Sql.AppendLine("," + Util.Sql_String(Registro.Dt_Fechamento))
            Sql.AppendLine("," + Util.Sql_Numero(Registro.Valor))
            Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Unidade))
            Sql.AppendLine(")")

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Inserir_Questao(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO Questao (cod_atividade, Enunciado, Alternativa_A, Alternativa_B, Alternativa_C, Alternativa_D, Alternativa_Correta, Justificativa)")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Registro.Cod_Atividade))
            Sql.AppendLine("," + Util.Sql_String(Registro.Enunciado))
            Sql.AppendLine("," + Util.Sql_String(Registro.Alternativa_A))
            Sql.AppendLine("," + Util.Sql_String(Registro.Alternativa_B))
            Sql.AppendLine("," + Util.Sql_String(Registro.Alternativa_C))
            Sql.AppendLine("," + Util.Sql_String(Registro.Alternativa_D))
            Sql.AppendLine("," + Util.Sql_String(Registro.Alternativa_Correta))
            Sql.AppendLine("," + Util.Sql_String(Registro.Justificativa))
            Sql.AppendLine(")")

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Alterar_Questao(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Questao SET")
            Sql.AppendLine("Enunciado = " + Util.Sql_String(Registro.Enunciado))
            Sql.AppendLine(", Alternativa_A= " + Util.Sql_String(Registro.Alternativa_A))
            Sql.AppendLine(", Alternativa_B= " + Util.Sql_String(Registro.Alternativa_B))
            Sql.AppendLine(", Alternativa_C= " + Util.Sql_String(Registro.Alternativa_C))
            Sql.AppendLine(", Alternativa_D= " + Util.Sql_String(Registro.Alternativa_D))
            Sql.AppendLine(", Alternativa_Correta = " + Util.Sql_String(Registro.Alternativa_Correta))
            Sql.AppendLine(", Justificativa = " + Util.Sql_String(Registro.Justificativa))
            Sql.AppendLine("WHERE Cod_Questao = " + Util.Sql_String(Registro.Cod_Questao))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Excluir_Questao(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("DELETE FROM Questao")
            Sql.AppendLine("WHERE Cod_Questao = " + Util.Sql_String(Registro.Cod_Questao))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Excluir_Conteudo_Atividade(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("DELETE FROM Questao")
            Sql.AppendLine("WHERE Cod_Atividade = " + Util.Sql_String(Registro.Cod_Atividade))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Excluir_Atividade(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("DELETE FROM Atividade")
            Sql.AppendLine("WHERE Cod_Atividade = " + Util.Sql_String(Registro.Cod_Atividade))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub


        Public Sub Alterar_Resposta(Registro As Dados)

            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Usuarioxrespostas SET")
            Sql.AppendLine("cod_usuario = " + Util.Sql_String(Registro.Cod_Usuario))
            Sql.AppendLine(",cod_atividade = " + Util.Sql_String(Registro.Cod_Atividade))
            Sql.AppendLine(",cod_questao = " + Util.Sql_String(Registro.Cod_Questao))
            Sql.AppendLine(",resposta = " + Util.Sql_String(Registro.Resposta))
            Sql.AppendLine(",correta = " + Util.Sql_String(Registro.Correta))
            Sql.AppendLine("WHERE Cod_Usuario = " + Util.Sql_String(Registro.Cod_Usuario))
            Sql.AppendLine("AND Cod_Questao = " + Util.Sql_String(Registro.Cod_Questao))
            Sql.AppendLine("AND Cod_Atividade = " + Util.Sql_String(Registro.Cod_Atividade))

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Inserir_Resposta(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO Usuarioxrespostas (cod_usuario, cod_atividade, cod_questao, resposta, correta)")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Registro.Cod_Usuario))
            Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Atividade))
            Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Questao))
            Sql.AppendLine("," + Util.Sql_String(Registro.Resposta))
            Sql.AppendLine("," + Util.Sql_String(Registro.Correta))
            Sql.AppendLine(")")

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Inserir_Notas(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO NOTAS (Cod_Atividade, Cod_Usuario, Pontos)")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Registro.Cod_Atividade))
            Sql.AppendLine("," + Util.Sql_String(Registro.Cod_Usuario))
            Sql.AppendLine("," + Util.Sql_Numero(Registro.Pontos))
            Sql.AppendLine(")")

            Biblio.Executar_Sql(Sql.ToString())
        End Sub

    End Class

End Namespace
