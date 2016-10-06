Imports STV.Seguranca
Imports STV.Entidades

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
        Erro.Visible = False

        Dim CPF As String = TB_CPF.Text
        Dim Senha As String = TB_Senha.Text

        Dim Autenticou As String = Autenticacao.Login_Usuario(CPF, Senha)

        If String.IsNullOrEmpty(Autenticou) Then
            If IsNothing(Request("ReturnUrl")) Then
                Response.Redirect("Default.aspx")
            Else
                Response.Redirect(Util.CString(Request("ReturnUrl")))
            End If
        Else
            If Autenticou = "Senha inválida!" Then
                TB_Senha.Focus()
            End If

            If Autenticou = "CPF inválido ou não cadastrado!" Then
                TB_CPF.Focus()
            End If
            L_Erro.Text = Autenticou
            Erro.Visible = True
        End If
    End Sub


End Class