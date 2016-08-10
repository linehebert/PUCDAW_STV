Imports System.Data
Imports System.Data.SqlClient

Namespace STV

    Public Class Biblio

        Dim _Util As Util
        Private ReadOnly Property Util As Util
            Get
                If IsNothing(_Util) Then _
                    _Util = New Util

                Return _Util
            End Get
        End Property

        Sub New(ConnectionString As String)
            Me.ConnectionString = ConnectionString
        End Sub

        Dim _ConnectionString As String
        Private Property ConnectionString As String
            Get
                Return _ConnectionString
            End Get
            Set(value As String)
                _ConnectionString = value
            End Set
        End Property

        Dim _Conexao As SqlConnection
        Private ReadOnly Property Conexao() As SqlConnection
            Get
                If IsNothing(_Conexao) Then _Conexao = New SqlConnection(ConnectionString)
                Return _Conexao
            End Get
        End Property


        Public Function Executar_Query(Sql As String) As IDataReader
            FechaConexao()
            Dim Query As New SqlCommand(Sql, Conexao)
            Conexao.Open()
            Return Query.ExecuteReader()
        End Function
        Public Sub Executar_Sql(Sql As String)
            Dim Comando = New SqlCommand(Sql, Conexao)

            Comando.CommandType = CommandType.Text

            Conexao.Open()

            Comando.ExecuteNonQuery()

            Comando.Dispose()
            Comando = Nothing
            FechaConexao()
        End Sub
        Public Function Retorna_DataTable(Sql As String) As DataTable
            Dim Query As New SqlDataAdapter(Sql, Conexao)
            Dim Dt As New DataTable()

            Query.SelectCommand.CommandTimeout = 999999999

            Conexao.Open()

            Query.Fill(Dt)

            FechaConexao()

            Return Dt
        End Function
        Public Function Existe_Registro(Sql As String) As Boolean
            Dim Retorno As Boolean = False
            Dim Query As New SqlCommand(Sql, Conexao)
            Conexao.Open()
            Dim Reader = Query.ExecuteReader()

            If Reader.HasRows Then Retorno = True

            Reader.Close()
            FechaConexao()

            Return Retorno
        End Function
        Public Function Pega_Valor(Sql As String, Campo As String) As String
            Dim Retorno As String = String.Empty
            Dim Query As New SqlCommand(Sql, Conexao)
            Conexao.Open()
            Dim Reader = Query.ExecuteReader()

            Try

                If Reader.Read() Then _
                    Retorno = CStr(Reader(Campo))

            Catch : End Try

            Reader.Close()
            FechaConexao()

            Return Retorno
        End Function
        Public Function Pega_Valor_Integer(Sql As String, Campo As String) As Integer
            Return Util.CInteger(Pega_Valor(Sql, Campo))
        End Function
        Public Function Pega_Valor_Double(Sql As String, Campo As String) As Integer
            Return Util.CDouble(Pega_Valor(Sql, Campo))
        End Function
        Public Function Pega_Valor_Boolean(Sql As String, Campo As String) As Boolean
            Return Util.CBoolean(Pega_Valor(Sql, Campo))
        End Function


        Public Sub FechaConexao()
            Conexao.Close()
        End Sub

    End Class

End Namespace
