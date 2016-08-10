Imports STV.Seguranca

Partial Class Login : Inherits STV.Base.Page

    Dim _Autenticacao As Autenticacao
    Private ReadOnly Property Autenticacao As Autenticacao
        Get
            If IsNothing(_Autenticacao) Then _
                _Autenticacao = New Autenticacao

            Return _Autenticacao
        End Get
    End Property

    Protected Sub B_Login_Click(sender As Object, e As System.EventArgs) Handles B_Login.Click
        Dim CPF As String = (Request("TB_CPF").Replace(".", "")).Replace("-", "")
        Dim Senha As String = Request("TB_Senha")

        Dim Autenticou As String = Autenticacao.Login_Usuario(CPF, Senha)
        L_Erro.Text = Autenticou


        If String.IsNullOrEmpty(Autenticou) Then
            If IsNothing(Request("ReturnUrl")) Then
                Response.Redirect("Default.aspx")
            Else
                Response.Redirect(Util.CString(Request("ReturnUrl")))
                Erro.Visible = True
            End If
        End If
    End Sub


End Class