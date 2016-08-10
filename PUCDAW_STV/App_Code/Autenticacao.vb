Imports Microsoft.VisualBasic
Imports STV.Entidades

Namespace STV.Seguranca
    Public Class Autenticacao : Inherits Base.ClasseBase

        Dim _Usuario As Usuario
        Private ReadOnly Property Usuario As Usuario
            Get
                If IsNothing(_Usuario) Then _
                _Usuario = New Usuario

                Return _Usuario
            End Get
        End Property
        Public Function Login_Usuario(Login As String, Senha As String) As String
            Dim Retorno As String = String.Empty

            Dim CPF_Logado As String = Biblio.Pega_Valor("SELECT CPF FROM Usuario WHERE CPF = " + Util.Sql_String(Login), "CPF")
            If Not String.IsNullOrEmpty(CPF_Logado) Then
                Dim Senha_Banco As String = Criptografia.Decryptdata(Biblio.Pega_Valor("SELECT Senha FROM Usuario WHERE CPF = " + Util.Sql_String(CPF_Logado), "Senha"))
                If Senha_Banco <> Senha Then
                    Retorno = "Senha inválida!"
                Else
                    'FormsAuthentication.SetAuthCookie(CPF_Logado, True)
                    Dim Cod_User As String = Biblio.Pega_Valor("SELECT Cod_Usuario FROM Usuario WHERE CPF = " + Util.Sql_String(CPF_Logado), "Cod_Usuario")
                    Dim User = Usuario.Carrega_Usuario(Cod_User)

                    Dim Dados As String = Newtonsoft.Json.JsonConvert.SerializeObject(User)
                    Dim ticket As New FormsAuthenticationTicket(1, User.Nome, DateTime.Now, DateTime.Now.AddHours(3), False, Dados)
                    Dim dadoscookie = FormsAuthentication.Encrypt(ticket)
                    Dim Cookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, dadoscookie)
                    Cookie.HttpOnly = True
                    Cookie.Expires = ticket.Expiration
                    HttpContext.Current.Response.Cookies.Add(Cookie)
                End If
            Else
                Retorno = "CPF inválido ou não cadastrado!"
            End If
            Return Retorno
        End Function


        Public Function Obter_User_Logado() As Usuario.Dados
            Dim Cookie As HttpCookie = HttpContext.Current.Request.Cookies(FormsAuthentication.FormsCookieName)
            Dim User As New Usuario.Dados
            If Not Cookie Is Nothing Then
                Dim ticket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(Cookie.Value)
                User = Newtonsoft.Json.JsonConvert.DeserializeObject(ticket.UserData, GetType(Usuario.Dados))
            End If
            Return User
        End Function

    End Class

End Namespace
