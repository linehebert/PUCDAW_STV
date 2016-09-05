Imports Microsoft.VisualBasic
Imports System.Data

Namespace STV.Entidades

    Public Class Curso : Inherits Base.ClasseBase

        Public Class Dados
            Public Cod_Curso As Integer
            Public Titulo As String
            Public Dt_Inicio As Date
            Public Dt_Termino As Date
            Public Categoria As Integer
            Public Palavras_Chave As String
            Public Instrutor As Integer
            Public Departamento As Integer
            Public Curso_Inativo As Boolean
        End Class

        Public Function Carrega_Curso(Cod_Curso As String) As Dados

            Dim Retorno As New Dados
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT C.cod_curso, C.titulo, C.dt_inicio, C.dt_termino, C.palavras_chave,")
            Sql.AppendLine("C.instrutor as cod_user, U.nome as Instrutor, C.departamento, C.categoria as cod_categoria, Cat.descricao as categoria, C.curso_inativo ")
            Sql.AppendLine("FROM curso AS C ")
            Sql.AppendLine("LEFT JOIN Usuario AS U ON U.cod_usuario = C.instrutor ")
            Sql.AppendLine("LEFT JOIN Categoria AS Cat ON Cat.cod_categoria = C.categoria ")
            Sql.AppendLine("WHERE Cod_Curso=" + Util.CString(Cod_Curso))

            Dim Query = Biblio.Executar_Query(Sql.ToString)

            If Query.Read() Then
                Retorno.Cod_Curso = Util.CInteger(Query("Cod_Curso"))
                Retorno.Titulo = Util.CString(Query("Titulo"))
                Retorno.Dt_Inicio = Util.CDateTime(Query("Dt_Inicio"))
                Retorno.Dt_Termino = Util.CDateTime(Query("Dt_Termino"))
                Retorno.Categoria = Util.CInteger(Query("Cod_Categoria"))
                Retorno.Palavras_Chave = Util.CString(Query("Palavras_Chave"))
                Retorno.Instrutor = Util.CInteger(Query("Cod_User"))
                Retorno.Curso_Inativo = Util.CBoolean(Query("Curso_Inativo"))
            End If

            Biblio.FechaConexao()

            Return Retorno
        End Function

        Public Function Carrega_Cursos(Titulo As String, Departamento As Integer, Instrutor As Integer, Inativo As Boolean, Outros As Boolean) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT C.cod_curso, C.titulo, C.dt_inicio, C.dt_termino, C.palavras_chave,")
            Sql.AppendLine("C.instrutor as cod_user, U.nome as Instrutor, C.departamento, C.categoria as cod_categoria, Cat.descricao as categoria, C.curso_inativo ")
            Sql.AppendLine("FROM curso AS C ")
            Sql.AppendLine("LEFT JOIN Usuario AS U ON U.cod_usuario = C.instrutor ")
            Sql.AppendLine("LEFT JOIN Categoria AS Cat ON Cat.cod_categoria = C.categoria ")
            Sql.AppendLine("WHERE 0 = 0")
            If Not String.IsNullOrEmpty(Titulo) Then Sql.AppendLine("AND C.Titulo LIKE " + Util.Sql_String("%" + Titulo + "%"))
            If Departamento <> 0 Then Sql.AppendLine("AND C.Departamento = " + Util.Sql_String(Departamento))
            If Instrutor <> 0 And Outros = False Then Sql.AppendLine("AND Instrutor = " + Util.Sql_String(Instrutor))
            If Instrutor <> 0 And Outros = True Then Sql.AppendLine("AND Instrutor <> " + Util.Sql_String(Instrutor))
            If Inativo = False Then Sql.AppendLine("AND C.Curso_Inativo = 0 ")
            Sql.AppendLine("ORDER BY Cod_Curso")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Function Carrega_Cursos_Aluno(Departamento As Integer, Instrutor As Integer, Titulo As String) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT CD.Cod_Departamento, CD.Cod_Curso, C.titulo, C.Cod_Curso, C.dt_inicio, ")
            Sql.AppendLine("C.dt_termino, Cat.descricao AS Categoria, U.nome AS Instrutor, C.Curso_Inativo ")
            Sql.AppendLine("FROM cursoXdepartamento AS CD ")
            Sql.AppendLine("LEFT JOIN Curso AS C ON C.cod_curso = CD.Cod_Curso")
            Sql.AppendLine("LEFT JOIN Usuario AS U ON U.cod_usuario = C.instrutor ")
            Sql.AppendLine("LEFT JOIN Categoria AS Cat ON Cat.cod_categoria = C.categoria ")
            Sql.AppendLine("WHERE 0 = 0")
            If Not String.IsNullOrEmpty(Titulo) Then Sql.AppendLine("AND C.Titulo LIKE " + Util.Sql_String("%" + Titulo + "%"))
            If Departamento <> 0 Then Sql.AppendLine("AND CD.Cod_Departamento = " + Util.Sql_String(Departamento))
            If Instrutor <> 0 Then Sql.AppendLine("AND C.Instrutor = " + Util.Sql_String(Instrutor))
            Sql.AppendLine("AND C.Curso_Inativo = 0 ")
            Sql.AppendLine("ORDER BY C.Cod_Curso DESC")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Function Carrega_Meus_Cursos(Instrutor As Integer, Titulo As String, Cod_Usuario As Integer) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT C.Titulo, C.Instrutor as Cod_Instrutor, C.Dt_Inicio, C.Dt_Termino, C.Cod_Curso, U.nome AS Instrutor, U.cod_usuario, C.CUrso_Inativo ")
            Sql.AppendLine("FROM Curso AS C ")
            Sql.AppendLine("LEFT JOIN Usuario AS U ON U.cod_usuario = C.instrutor ")
            Sql.AppendLine("WHERE 0 = 0 ")
            Sql.AppendLine("AND C.Cod_Curso IN (select cursoXusuario.cod_curso from cursoXusuario where cursoXusuario.cod_usuario=" + Util.Sql_String(Cod_Usuario) + ")")
            If Instrutor <> 0 Then Sql.AppendLine("AND C.Instrutor = " + Util.Sql_String(Instrutor))
            If Not String.IsNullOrEmpty(Titulo) Then Sql.AppendLine("AND C.Titulo LIKE " + Util.Sql_String("%" + Titulo + "%"))
            Sql.AppendLine("AND C.Curso_Inativo = 0 ")
            Sql.AppendLine("ORDER BY C.Cod_Curso DESC")

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Function Carrega_Conteudo_Completo(Cod_Curso As Integer) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT U.Titulo as Unidade, A.Titulo as Atividade, U.Cod_Unidade, A.Cod_Atividade ")
            Sql.AppendLine("FROM UNIDADE AS U")
            Sql.AppendLine("LEFT JOIN Atividade as A ON A.Cod_Unidade = U.Cod_Unidade")
            Sql.AppendLine("WHERE 0 = 0")
            If Cod_Curso <> 0 Then Sql.AppendLine("AND Cod_Curso = " + Util.CString(Cod_Curso))


            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Sub Delete(Cod_Curso As Integer)
            Dim Sql As New StringBuilder
            Sql.AppendLine("DELETE FROM cursoXdepartamento")
            Sql.AppendLine("WHERE Cod_Curso = " + Util.CString(Cod_Curso))

            Biblio.Executar_Query(Sql.ToString())
        End Sub
        Public Sub Alterar(Registro As Dados)
            Dim Sql As New StringBuilder
            Sql.AppendLine("UPDATE Curso SET")
            Sql.AppendLine("Titulo = " + Util.Sql_String(Registro.Titulo))
            Sql.AppendLine(",Dt_Inicio = " + Util.Sql_String(Registro.Dt_Inicio))
            Sql.AppendLine(",Dt_Termino = " + Util.Sql_String(Registro.Dt_Termino))
            Sql.AppendLine(",Palavras_Chave = " + Util.Sql_String(Registro.Palavras_Chave))
            Sql.AppendLine(",Instrutor = " + Util.Sql_String(Registro.Instrutor))
            Sql.AppendLine(",Categoria = " + Util.Sql_String(Registro.Categoria))
            Sql.AppendLine(",Curso_Inativo = " + Util.Sql_String(Registro.Curso_Inativo))
            Sql.AppendLine("WHERE Cod_Curso = " + Util.Sql_String(Registro.Cod_Curso))

            Biblio.Executar_Query(Sql.ToString())
        End Sub

        Public Function Inserir(Registro As Dados) As Integer
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO CURSO(titulo, dt_inicio, dt_termino, palavras_chave, instrutor, categoria, curso_inativo)")
            Sql.AppendLine(" Output Inserted.Cod_Curso")
            Sql.AppendLine(" VALUES(")
            Sql.AppendLine(Util.Sql_String(Registro.Titulo))
            Sql.AppendLine("," + Util.Sql_String(Registro.Dt_Inicio))
            Sql.AppendLine("," + Util.Sql_String(Registro.Dt_Termino))
            Sql.AppendLine("," + Util.Sql_String(Registro.Palavras_Chave))
            Sql.AppendLine("," + Util.Sql_String(Registro.Instrutor))
            Sql.AppendLine("," + Util.Sql_String(Registro.Categoria))
            Sql.AppendLine("," + Util.Sql_String(Registro.Curso_Inativo))
            Sql.AppendLine(")")

            Dim dt As DataTable = Biblio.Retorna_DataTable(Sql.ToString())

            Return Util.CInteger(dt.Rows(0).Item("Cod_Curso"))
        End Function

        Public Sub Inserir_Visibilidade(Departamento As Integer, Curso As Integer)
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO cursoXdepartamento(Cod_Departamento, Cod_Curso)")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Departamento))
            Sql.AppendLine("," + Util.Sql_String(Curso))
            Sql.AppendLine(")")

            Biblio.FechaConexao()
            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Sub Inscrever_Usuario(Usuario As Integer, Curso As Integer)
            Dim Sql As New StringBuilder
            Sql.AppendLine("INSERT INTO cursoXusuario(Cod_Usuario, Cod_Curso)")
            Sql.AppendLine("VALUES(")
            Sql.AppendLine(Util.Sql_String(Usuario))
            Sql.AppendLine("," + Util.Sql_String(Curso))
            Sql.AppendLine(")")

            Biblio.FechaConexao()
            Biblio.Executar_Sql(Sql.ToString())
        End Sub

        Public Function Carrega_Visibilidade(Cod_Curso As String) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("select cd.Cod_Departamento, d.descricao,  cd.cod_curso  ")
            Sql.AppendLine("from cursoXdepartamento as cd")
            Sql.AppendLine("left join Departamento AS D ON D.Cod_Departamento = cd.Cod_Departamento")
            Sql.AppendLine("where cod_curso=" + Util.CString(Cod_Curso))

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Function Carrega_Cursos_Usuario(Cod_Usuario As String) As DataTable
            Dim Sql As New StringBuilder
            Sql.AppendLine("select cod_curso ")
            Sql.AppendLine("from cursoXusuario ")
            Sql.AppendLine("where cod_usuario=" + Util.CString(Cod_Usuario))

            Return Biblio.Retorna_DataTable(Sql.ToString())
        End Function

        Public Function Verifica_Inscritos(Curso As Integer, Departamento As Integer) As Boolean
            Dim Sql As New StringBuilder
            Sql.AppendLine("SELECT CU.Cod_Curso FROM cursoXusuario AS CU ")
            Sql.AppendLine("inner join USUARIO as U ON U.cod_usuario = CU.Cod_Usuario ")
            Sql.AppendLine("WHERE 0 = 0 ")
            If Curso <> 0 Then Sql.AppendLine("AND CU.Cod_Curso = " + Util.Sql_String(Curso))
            If Departamento <> 0 Then Sql.AppendLine("AND U.departamento = " + Util.Sql_String(Departamento))

            Dim Query = Biblio.Executar_Query(Sql.ToString)
            Return Query.Read()
        End Function
    End Class

End Namespace
