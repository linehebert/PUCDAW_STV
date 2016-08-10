Imports STV.Entidades
Imports STV.Seguranca
Partial Class MasterPage : Inherits STV.Base.MasterPage

    Dim _Autenticacao As Autenticacao
    Private ReadOnly Property Autenticacao As Autenticacao
        Get
            If IsNothing(_Autenticacao) Then _
                _Autenticacao = New Autenticacao

            Return _Autenticacao
        End Get
    End Property
    Private Sub MasterPage_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Dim teste As String = Page.User.Identity.Name -- método que faz requisição toda vez no banco
        Dim Usuario_Logado As Usuario.Dados = Autenticacao.Obter_User_Logado()
        'If Usuario_Logado.Nome = "Aline" Then... codigo 
    End Sub

    Protected Sub LB_Sair_Click(sender As Object, e As System.EventArgs) Handles LB_Sair.Click
        FormsAuthentication.SignOut()
        Response.Redirect("~/Login.aspx")
    End Sub
End Class

