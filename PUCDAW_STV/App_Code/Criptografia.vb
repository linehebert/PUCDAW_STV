Imports Microsoft.VisualBasic

Namespace STV.Seguranca

    Public Class Criptografia

        Public Shared Function Encryptdata(password As String) As String
            If String.IsNullOrEmpty(password) Then _
                Return String.Empty

            Dim strmsg As String = String.Empty

            Try
                Dim encode As Byte() = New Byte(password.Length - 1) {}
                encode = Encoding.UTF8.GetBytes(password)
                strmsg = Convert.ToBase64String(encode)
            Catch : End Try

            Return strmsg
        End Function

        Public Shared Function Decryptdata(encryptpwd As String) As String
            If String.IsNullOrEmpty(encryptpwd) Then _
                Return String.Empty

            Dim decryptpwd As String = String.Empty

            Try
                Dim encodepwd As New UTF8Encoding()
                Dim Decode As Decoder = encodepwd.GetDecoder()
                Dim todecode_byte As Byte() = Convert.FromBase64String(encryptpwd)
                Dim charCount As Integer = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length)
                Dim decoded_char As Char() = New Char(charCount - 1) {}
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0)
                decryptpwd = New [String](decoded_char)
            Catch : End Try

            Return decryptpwd
        End Function

    End Class

End Namespace
