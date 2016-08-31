Imports STV.Seguranca
Imports STV.Entidades
Partial Class Cadastros_Atividade : Inherits STV.Base.Page

    Dim _Atividade As Atividade
    Private ReadOnly Property Atividade As Atividade
        Get
            If IsNothing(_Atividade) Then _
                _Atividade = New Atividade

            Return _Atividade
        End Get
    End Property
    Dim _Autenticacao As Autenticacao
    Private ReadOnly Property Autenticacao As Autenticacao
        Get
            If IsNothing(_Autenticacao) Then _
                _Autenticacao = New Autenticacao
            Return _Autenticacao
        End Get
    End Property

    Dim _Usuario_Logado As Usuario.Dados
    Private ReadOnly Property Usuario_Logado As Usuario.Dados
        Get
            If IsNothing(_Usuario_Logado) Then
                _Usuario_Logado = New Usuario.Dados
                _Usuario_Logado = Autenticacao.Obter_User_Logado()
            End If

            Return _Usuario_Logado
        End Get
    End Property
    Private ReadOnly Property Cod_Atividade As Integer
        Get
            Return Request("Atv")
        End Get
    End Property
    Private ReadOnly Property Cod_Unidade As Integer
        Get
            Return Request("Unit")
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Monta_Dados()
                Monta_Questionario()
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

#Region "Funções e Rotinas"
    Private Sub Monta_Dados()
        Try
            Dim Dado = Atividade.Carrega_Atividade(Cod_Atividade)
            Unidade.Text = Dado.Unidade
            Curso.Text = Dado.Curso

            Titulo.Text = Dado.Titulo
            Dt_Abertura.Text = Dado.Dt_Abertura.ToString("yyyy-MM-dd")
            Dt_Encerramento.Text = Dado.Dt_Fechamento.ToString("yyyy-MM-dd")
            Valor.Text = Dado.Valor
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub Monta_Questionario()
        Try
            Dim Dado = Atividade.Carrega_Questao_Inicial(Cod_Atividade)

            Me.ViewState("Cod_Questao") = Dado.Cod_Questao
            L_Questao.Text = Dado.Enunciado
            L_A.Text = Dado.Alternativa_A
            L_B.Text = Dado.Alternativa_B
            L_C.Text = Dado.Alternativa_C
            L_D.Text = Dado.Alternativa_D
            L_Justificativa.Text = Dado.Justificativa

            Dim Resposta As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao")), "Resposta")
            If Resposta = "A" Then RB_A.Checked = True
            If Resposta = "B" Then RB_B.Checked = True
            If Resposta = "C" Then RB_C.Checked = True
            If Resposta = "D" Then RB_D.Checked = True

            B_Anterior.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "Botões e Eventos"

    Private Sub B_Proximo_Click(sender As Object, e As EventArgs) Handles B_Proximo.Click
        Try
            'Verifica se pelo menos uma alternativa foi selecionada
            If RB_A.Checked OrElse RB_B.Checked OrElse RB_C.Checked OrElse RB_D.Checked Then

                'Verifica se já existe resposta cadastrada para a questão atual
                Dim Resposta As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao")), "Resposta")
                If Resposta = "A" Then RB_A.Checked = True
                If Resposta = "B" Then RB_B.Checked = True
                If Resposta = "C" Then RB_C.Checked = True
                If Resposta = "D" Then RB_D.Checked = True
                If Resposta = Nothing Then
                    RB_A.Checked = False
                    RB_B.Checked = False
                    RB_C.Checked = False
                    RB_D.Checked = False
                End If

                'Guarda os dados para inserir na tabela de usuarioxrespostas
                Dim Registro As New Atividade.Dados
                Registro.Cod_Usuario = Usuario_Logado.Cod_Usuario
                Registro.Cod_Atividade = Cod_Atividade
                Registro.Cod_Questao = CInt(Me.ViewState("Cod_Questao"))

                If RB_A.Checked Then Registro.Resposta = "A"
                If RB_B.Checked Then Registro.Resposta = "B"
                If RB_C.Checked Then Registro.Resposta = "C"
                If RB_D.Checked Then Registro.Resposta = "D"

                'verifica se é insert ou alteração
                If Resposta = Nothing Then
                    Atividade.Inserir_Resposta(Registro)
                Else
                    Atividade.Alterar_Resposta(Registro)
                End If

                'Carregar a próxima questão
                Dim Dado = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao")), "ASC")

                Me.ViewState("Cod_Questao") = Dado.Cod_Questao
                L_Questao.Text = Dado.Enunciado
                L_A.Text = Dado.Alternativa_A
                L_B.Text = Dado.Alternativa_B
                L_C.Text = Dado.Alternativa_C
                L_D.Text = Dado.Alternativa_D

                'verifica se é a ultima questao
                Dim Proxima = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao")), "ASC")
                If Proxima.Cod_Questao = Nothing Then
                    B_Proximo.Visible = False
                    B_Finalizar.Visible = True
                End If
                B_Anterior.Visible = True

                UP_Atividade.Update()
            Else
                RegistrarScript("$('#myModalInfo').modal('show')")
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Anterior_Click(sender As Object, e As EventArgs) Handles B_Anterior.Click
        Try
            'Verifica se pelo menos uma alternativa foi selecionada
            If RB_A.Checked OrElse RB_B.Checked OrElse RB_C.Checked OrElse RB_D.Checked Then
                'Verifica se já existe resposta cadastrada para a questão atual
                Dim Resposta As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao")), "Resposta")
                If Resposta = "A" Then RB_A.Checked = True
                If Resposta = "B" Then RB_B.Checked = True
                If Resposta = "C" Then RB_C.Checked = True
                If Resposta = "D" Then RB_D.Checked = True
                If Resposta = Nothing Then
                    RB_A.Checked = False
                    RB_B.Checked = False
                    RB_C.Checked = False
                    RB_D.Checked = False
                End If

                'Guarda os dados para inserir na tabela de usuarioxrespostas
                Dim Registro As New Atividade.Dados
                Registro.Cod_Usuario = Usuario_Logado.Cod_Usuario
                Registro.Cod_Atividade = Cod_Atividade
                Registro.Cod_Questao = CInt(Me.ViewState("Cod_Questao"))

                If RB_A.Checked Then Registro.Resposta = "A"
                If RB_B.Checked Then Registro.Resposta = "B"
                If RB_C.Checked Then Registro.Resposta = "C"
                If RB_D.Checked Then Registro.Resposta = "D"

                'verifica se é insert ou alteração
                If Resposta = Nothing Then
                    Atividade.Inserir_Resposta(Registro)
                Else
                    Atividade.Alterar_Resposta(Registro)
                End If

                Dim Dado = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao")), "DESC")

                Me.ViewState("Cod_Questao") = Dado.Cod_Questao
                L_Questao.Text = Dado.Enunciado
                L_A.Text = Dado.Alternativa_A
                L_B.Text = Dado.Alternativa_B
                L_C.Text = Dado.Alternativa_C
                L_D.Text = Dado.Alternativa_D

                Dim Proxima = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao")), "DESC")
                If Proxima.Cod_Questao = Nothing Then
                    B_Anterior.Visible = False
                End If
                B_Proximo.Visible = True
                B_Finalizar.Visible = False


                UP_Atividade.Update()
            Else
                RegistrarScript("$('#myModalInfo').modal('show')")

            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Responder_Click(sender As Object, e As EventArgs) Handles B_Responder.Click
        RegistrarScript("$('#myModalInfo').modal('hide')")
    End Sub

#End Region
End Class
