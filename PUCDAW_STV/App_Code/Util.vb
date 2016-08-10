Imports Microsoft.VisualBasic

Namespace STV

    Public Class Util

        Public Function Sql_String(S As String) As String
            If String.IsNullOrEmpty(S) Then _
                Return "NULL"

            Return "'" + S + "'"
        End Function
        Public Function Sql_Data_Hora(D As Date, Inicio As Boolean) As String
            If Inicio Then
                Return "'" + D.Year.ToString("0000") + "-" + D.Month.ToString("00") + "-" + D.Day.ToString("00") + " 00:00:00'"
            Else
                Return "'" + D.Year.ToString("0000") + "-" + D.Month.ToString("00") + "-" + D.Day.ToString("00") + " 23:59:59'"
            End If
        End Function
        Public Function Sql_Numero(Numero As Double) As String
            Return Numero.ToString().Replace(",", ".")
        End Function
        Public Function Sql_Numero_Null(Numero As Double) As String
            If Not Numero > 0 Then
                Return "NULL"
            Else
                Return Sql_Numero(Numero)
            End If
        End Function
        Public Function Sql_Boolean(Booleano As Boolean) As String
            If Booleano Then
                Return "1"
            Else
                Return "0"
            End If
        End Function

        Public Function CString(S As Object) As String
            Try
                Return CStr(S)
            Catch
                Return String.Empty
            End Try
        End Function
        Public Function CInteger(Numero As Object) As Integer
            If IsDBNull(Numero) Then _
                Return 0

            Dim Valor As Integer = 0

            Integer.TryParse(Numero, Valor)

            Return Valor
        End Function
        Public Function CDateTime(Data As Object) As DateTime
            Dim Valor As DateTime

            DateTime.TryParse(Data, Valor)

            Return Valor
        End Function
        Public Function CDouble(Numero As Object) As Double
            If IsDBNull(Numero) Then _
                Return 0

            Dim Valor As Double = 0

            Double.TryParse(Numero, Valor)

            Return Valor
        End Function
        Public Function CBoolean(Numero As Object) As Double
            If IsDBNull(Numero) Then _
                Return 0

            Dim Valor As Boolean = 0

            Boolean.TryParse(Numero, Valor)

            Return Valor
        End Function
    End Class

End Namespace
