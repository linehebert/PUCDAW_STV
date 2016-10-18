Imports System.IO
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

    Dim _Usuario As Usuario
    Private ReadOnly Property Usuario As Usuario
        Get
            If IsNothing(_Usuario) Then _
                _Usuario = New Usuario

            Return _Usuario
        End Get
    End Property
    Private Sub MasterPage_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim Logo As String = Request.PhysicalApplicationPath & "\Images\Logo_Novo.jpg"
        If File.Exists(Logo) Then
            Img_Logo.ImageUrl = "~/Images/Logo_Novo.jpg"
        Else
            Img_Logo.ImageUrl = "~/Images/Logo_Default.png"
        End If


        Dim Usuario_Logado As Usuario.Dados = Autenticacao.Obter_User_Logado()
        'Identifica usuário logado no cabeçalho
        If Usuario_Logado.Nome <> "" Then
            'RegistrarScript("$('#BT_Login').hide();")
            'L_Usuario_Logado.Visible = True
            L_Usuario_Logado.Text = "Olá " + Usuario_Logado.Nome
            dados_acesso.Visible = True

            'Identifica qual o tipo de usuário logado e libera as páginas de acordo com as permissões
            If Usuario_Logado.ADM = True Then
                'Libera todas as páginas
                departamentos.Visible = True
                usuarios.Visible = True
                cursos_instrutor.Visible = False
                meus_cursos.Visible = False
                categorias.Visible = True
                cursos.InnerText = "CURSOS"

                If Usuario.Verifica_Responsabilidade(Usuario_Logado.Cod_Usuario) = True Then
                    cursos_instrutor.Visible = True
                    cursos_instrutor.InnerText = "GERENCIAR CURSOS"
                End If
            Else
                tema.Visible = False
                cursos.InnerText = "INSCRIÇÕES"
                If Usuario.Verifica_Responsabilidade(Usuario_Logado.Cod_Usuario) = True Then
                    'Libera acesso de instrutor
                    departamentos.Visible = False
                    usuarios.Visible = False
                    categorias.Visible = False
                    cursos_instrutor.InnerText = "GERENCIAR CURSOS"
                Else
                    'Libera acesso somente de usuário/aluno
                    departamentos.Visible = False
                    usuarios.Visible = False
                    cursos_instrutor.Visible = False
                    categorias.Visible = False
                End If
            End If
        Else
            L_Usuario_Logado.Text = "Você ainda não está logado"

            home.visible = False
            tema.Visible = False
            departamentos.Visible = False
            usuarios.Visible = False
            cursos.Visible = False
            cursos_instrutor.Visible = False
            meus_cursos.Visible = False
            LB_Sair.Visible = False
        End If



    End Sub

    Protected Sub LB_Sair_Click(sender As Object, e As System.EventArgs) Handles LB_Sair.Click
        FormsAuthentication.SignOut()
        Response.Redirect("~/Login.aspx")
    End Sub
End Class

