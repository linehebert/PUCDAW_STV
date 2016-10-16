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

                'Verifica se a atividade já foi finalizada pelo aluno
                Dim Finalizada As String = Biblio.Pega_Valor("SELECT PONTOS FROM NOTAS WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Atividade=" + Util.Sql_String(Cod_Atividade), "PONTOS")

                If Finalizada <> Nothing And CDate(Dt_Encerramento.Text) < Date.Today() Then
                    'Exibe a revisão da atividade
                    Aviso_Encerramento.Visible = False
                    Realizar_Atividade.Visible = False
                    Atividade_Completa.Visible = True
                    L_nota_L.Visible = True
                    L_Nota.Visible = True
                ElseIf Finalizada <> Nothing And CDate(Dt_Encerramento.Text) >= Date.Today() Then
                    'Atividade realizada mas ainda não encerrada. Aguarde para revisão!
                    Realizar_Atividade.Visible = False
                    Atividade_Completa.Visible = False
                    L_Aviso_Encerramento.Text = "Atividade Realizada! Aguarde o encerramento da atividade para visualizar a revisão."

                ElseIf Finalizada = Nothing And CDate(Dt_Encerramento.Text) < Date.Today() Then
                    'Atividade nao realizada mas encerrada.
                    Realizar_Atividade.Visible = False
                    Atividade_Completa.Visible = False
                    L_Aviso_Encerramento.Text = "Atividade Não Realizada! Não é possível realizar esta atividade pois se encontra encerrada."

                ElseIf Finalizada = Nothing And CDate(Dt_Encerramento.Text) >= Date.Today() Then
                    'Libera para realizar a atividade.
                    Aviso_Encerramento.Visible = False
                    Realizar_Atividade.Visible = True
                    Atividade_Completa.Visible = False
                End If

                Dim qntd_questao As String = Biblio.Pega_Valor("SELECT Cod_Questao FROM Questao WHERE Cod_Atividade=" + Util.Sql_String(Cod_Atividade), "Cod_Questao")
                If qntd_questao = "" Then
                    Nenhuma_Questao.Visible = True
                Else
                    Nenhuma_Questao.Visible = False
                    Carrega_Questoes(Cod_Atividade)
                End If
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
            Dt_Encerramento.Text = Dado.Dt_Fechamento.ToString("dd/MM/yyyy")
            Valor.Text = Dado.Valor

            Dim Nota As Double = Biblio.Pega_Valor_Double("SELECT PONTOS FROM NOTAS WHERE Cod_Usuario=" + Util.CString(Usuario_Logado.Cod_Usuario) + " AND Cod_Atividade = " + Util.CString(Dado.Cod_Atividade), "PONTOS")
            If Nota <> 0 Then L_Nota.Text = Nota.ToString("F")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub Monta_Questionario()
        Try
            Dim Dado = Atividade.Carrega_Questao_Inicial(Cod_Atividade)

            Me.ViewState("Cod_Questao_Origem") = Dado.Cod_Questao
            L_Questao.Text = Dado.Enunciado
            L_A.Text = "A: " & Dado.Alternativa_A
            L_B.Text = "B: " & Dado.Alternativa_B
            L_C.Text = "C: " & Dado.Alternativa_C
            L_D.Text = "D: " & Dado.Alternativa_D
            L_Justificativa.Text = "Justificativa: " & Dado.Justificativa

            Dim Resposta As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao_Origem")), "Resposta")
            If Resposta = "A" Then RB_A.Checked = True
            If Resposta = "B" Then RB_B.Checked = True
            If Resposta = "C" Then RB_C.Checked = True
            If Resposta = "D" Then RB_D.Checked = True

            'Verifica se é a ultima questao
            Dim Proxima = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao_Origem")), "ASC")
            If Proxima.Cod_Questao = Nothing Then
                B_Proximo.Visible = False
                B_Finalizar.Visible = True
            Else
                Me.ViewState("Cod_Questao_Destino") = Proxima.Cod_Questao
            End If

            B_Anterior.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub Carrega_Questoes(Cod_Atividade As Integer)
        Try
            rptQuestoes.DataSource = Atividade.Carrega_Atividade_Aluno(Cod_Atividade)
            rptQuestoes.DataBind()
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

                Dim Resposta As String = Biblio.Pega_Valor("SELECT Alternativa_Correta FROM Questao WHERE Cod_Questao=" + Util.Sql_String(CInt(Me.ViewState("Cod_Questao_Origem"))), "Alternativa_Correta")
                Dim Resposta_Usuario As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao_Origem")), "Resposta")

                'Guarda os dados para inserir na tabela de usuarioxrespostas
                Dim Registro As New Atividade.Dados
                Registro.Cod_Usuario = Usuario_Logado.Cod_Usuario
                Registro.Cod_Atividade = Cod_Atividade
                Registro.Cod_Questao = CInt(Me.ViewState("Cod_Questao_Origem"))

                If RB_A.Checked Then Registro.Resposta = "A"
                If RB_B.Checked Then Registro.Resposta = "B"
                If RB_C.Checked Then Registro.Resposta = "C"
                If RB_D.Checked Then Registro.Resposta = "D"

                If Registro.Resposta = Resposta Then
                    Registro.Correta = 1
                Else
                    Registro.Correta = 0
                End If

                'verifica se é insert ou alteração
                If Resposta_Usuario = Nothing Then
                    Atividade.Inserir_Resposta(Registro)
                Else
                    Atividade.Alterar_Resposta(Registro)
                End If

                'Verifica se é a ultima questao
                Dim Proxima = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao_Origem")), "ASC")
                If Proxima.Cod_Questao = Nothing Then
                    B_Proximo.Visible = False
                    B_Finalizar.Visible = True
                    Me.ViewState("Cod_Questao_Destino") = Me.ViewState("Cod_Questao_Origem")
                Else
                    'Carregar a próxima questão
                    Me.ViewState("Cod_Questao_Origem") = Me.ViewState("Cod_Questao_Destino")
                    Me.ViewState("Cod_Questao_Destino") = Proxima.Cod_Questao
                    L_Questao.Text = Proxima.Enunciado
                    L_A.Text = "A: " & Proxima.Alternativa_A
                    L_B.Text = "B: " & Proxima.Alternativa_B
                    L_C.Text = "C: " & Proxima.Alternativa_C
                    L_D.Text = "D: " & Proxima.Alternativa_D

                    'Verifica se já existe resposta cadastrada para a questão atual
                    Dim Resposta_Usu As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao_Destino")), "Resposta")
                    RB_A.Checked = False
                    RB_B.Checked = False
                    RB_C.Checked = False
                    RB_D.Checked = False
                    If Resposta_Usu IsNot Nothing Then
                        If Resposta_Usu = "A" Then RB_A.Checked = True
                        If Resposta_Usu = "B" Then RB_B.Checked = True
                        If Resposta_Usu = "C" Then RB_C.Checked = True
                        If Resposta_Usu = "D" Then RB_D.Checked = True
                    End If

                    'Verifica se é a ultima questao
                    Proxima = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao_Destino")), "ASC")
                    If Proxima.Cod_Questao = Nothing Then
                        B_Proximo.Visible = False
                        B_Finalizar.Visible = True
                        Me.ViewState("Cod_Questao_Origem") = Me.ViewState("Cod_Questao_Destino")
                    Else
                        Me.ViewState("Cod_Questao_Origem") = Me.ViewState("Cod_Questao_Destino")
                        Me.ViewState("Cod_Questao_Destino") = Proxima.Cod_Questao
                    End If

                End If


                B_Anterior.Visible = True
                UP_Atividade.Update()
            Else
                L_Modal_Info.InnerText = "Esta questão ainda não foi respondida. Selecione a alternativa que julga correta para esta questão."
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

                Dim Resposta_Correta As String = Biblio.Pega_Valor("SELECT Alternativa_Correta FROM Questao WHERE Cod_Questao=" + Util.Sql_String(CInt(Me.ViewState("Cod_Questao_Origem"))), "Alternativa_Correta")
                Dim Resposta_Usuario As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao_Origem")), "Resposta")

                'Guarda os dados para inserir na tabela de usuarioxrespostas
                Dim Registro As New Atividade.Dados
                Registro.Cod_Usuario = Usuario_Logado.Cod_Usuario
                Registro.Cod_Atividade = Cod_Atividade
                Registro.Cod_Questao = CInt(Me.ViewState("Cod_Questao_Origem"))

                If RB_A.Checked Then Registro.Resposta = "A"
                If RB_B.Checked Then Registro.Resposta = "B"
                If RB_C.Checked Then Registro.Resposta = "C"
                If RB_D.Checked Then Registro.Resposta = "D"

                If Registro.Resposta = Resposta_Correta Then
                    Registro.Correta = 1
                Else
                    Registro.Correta = 0
                End If

                'verifica se é insert ou alteração
                If Resposta_Usuario = Nothing Then
                    Atividade.Inserir_Resposta(Registro)
                Else
                    Atividade.Alterar_Resposta(Registro)
                End If

                'Verifica qual é a próxima questão
                Dim Proxima = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao_Origem")), "DESC")
                If Proxima.Cod_Questao = Nothing Then
                    B_Anterior.Visible = False
                    Me.ViewState("Cod_Questao_Origem") = Me.ViewState("Cod_Questao_Destino")
                Else
                    'Me.ViewState("Cod_Questao_Destino") = Me.ViewState("Cod_Questao_Origem")
                    Me.ViewState("Cod_Questao_Destino") = Proxima.Cod_Questao
                    L_Questao.Text = Proxima.Enunciado
                    L_A.Text = "A: " & Proxima.Alternativa_A
                    L_B.Text = "B: " & Proxima.Alternativa_B
                    L_C.Text = "C: " & Proxima.Alternativa_C
                    L_D.Text = "D: " & Proxima.Alternativa_D

                    'Verifica se já existe resposta cadastrada para a questão atual
                    Dim Resposta As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao_Destino")), "Resposta")
                    RB_A.Checked = False
                    RB_B.Checked = False
                    RB_C.Checked = False
                    RB_D.Checked = False
                    If Resposta IsNot Nothing Then
                        If Resposta = "A" Then
                            RB_A.Checked = True
                        ElseIf Resposta = "B" Then
                            RB_B.Checked = True
                        ElseIf Resposta = "C" Then
                            RB_C.Checked = True
                        ElseIf Resposta = "D" Then
                            RB_D.Checked = True
                        End If
                    End If

                    Proxima = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao_Destino")), "DESC")
                    If Proxima.Cod_Questao = Nothing Then
                        B_Anterior.Visible = False
                        Me.ViewState("Cod_Questao_Origem") = Me.ViewState("Cod_Questao_Destino")
                    Else
                        Me.ViewState("Cod_Questao_Origem") = Me.ViewState("Cod_Questao_Destino")
                        Me.ViewState("Cod_Questao_Destino") = Proxima.Cod_Questao
                    End If
                End If

                B_Proximo.Visible = True
                B_Finalizar.Visible = False


                UP_Atividade.Update()
            Else
                L_Modal_Info.InnerText = "Esta questão ainda não foi respondida. Selecione a alternativa que julga correta para esta questão."
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

    Private Sub B_Finalizar_Click(sender As Object, e As EventArgs) Handles B_Finalizar.Click
        Try
            'Verifica se pelo menos uma alternativa foi selecionada
            If RB_A.Checked OrElse RB_B.Checked OrElse RB_C.Checked OrElse RB_D.Checked Then

                'Verifica se já existe resposta cadastrada para a questão atual
                'Dim Resposta As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao")), "Resposta")
                'If Resposta = "A" Then RB_A.Checked = True
                'If Resposta = "B" Then RB_B.Checked = True
                'If Resposta = "C" Then RB_C.Checked = True
                'If Resposta = "D" Then RB_D.Checked = True

                Dim Resposta As String = Biblio.Pega_Valor("SELECT Alternativa_Correta FROM Questao WHERE Cod_Questao=" + Util.Sql_String(CInt(Me.ViewState("Cod_Questao_Origem"))), "Alternativa_Correta")
                Dim Resposta_Usuario As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao_Origem")), "Resposta")

                'Guarda os dados para inserir na tabela de usuarioxrespostas
                Dim Registro As New Atividade.Dados
                Registro.Cod_Usuario = Usuario_Logado.Cod_Usuario
                Registro.Cod_Atividade = Cod_Atividade
                Registro.Cod_Questao = CInt(Me.ViewState("Cod_Questao_Origem"))

                If RB_A.Checked Then Registro.Resposta = "A"
                If RB_B.Checked Then Registro.Resposta = "B"
                If RB_C.Checked Then Registro.Resposta = "C"
                If RB_D.Checked Then Registro.Resposta = "D"

                If Registro.Resposta = Resposta Then
                    Registro.Correta = 1
                Else
                    Registro.Correta = 0
                End If

                'verifica se é insert ou alteração
                If Resposta_Usuario = Nothing Then
                    Atividade.Inserir_Resposta(Registro)
                Else
                    Atividade.Alterar_Resposta(Registro)
                End If

                'Abre modal para confirmar a finalização da atividade e calcular a nota
                L_Modal_Info.InnerText = "Ao finalizar não será mais possível realizar alterações. Tem certeza que deseja finalizar esta atividade?"
                B_Confirmar.Visible = True
                B_Cancelar_Finalizar.Visible = True
                B_Responder.Visible = False

                RegistrarScript("$('#myModalInfo').modal('show')")
            Else
                L_Modal_Info.InnerText = "Esta questão ainda não foi respondida. Selecione a alternativa que julga correta para esta questão."
                RegistrarScript("$('#myModalInfo').modal('show')")
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Confirmar_Click(sender As Object, e As EventArgs) Handles B_Confirmar.Click
        Try
            'Verifica a quantidade de questões da atividade
            Dim Quantidade As Integer = Biblio.Pega_Valor_Integer("SELECT COUNT(*) AS TOTAL FROM QUESTAO WHERE Cod_Atividade=" + Util.Sql_Numero(Cod_Atividade), "TOTAL")

            'Verifica o valor da atividade
            Dim Valor As Double = Biblio.Pega_Valor("SELECT VALOR FROM ATIVIDADE WHERE Cod_Atividade=" + Util.Sql_Numero(Cod_Atividade), "VALOR")

            'Verifica o valor de cada questão
            Dim Valor_Questao As Double = Valor / Quantidade

            'Verifica quantos acertos o usuário teve
            Dim Acertos As Integer = Biblio.Pega_Valor("SELECT COUNT(Cod_Questao) AS ACERTOS FROM USUARIOxRESPOSTAS WHERE Cod_Atividade=" + Util.Sql_Numero(Cod_Atividade) + " AND Cod_Usuario=" + Util.Sql_Numero(Usuario_Logado.Cod_Usuario) + " AND Correta=1", "ACERTOS")

            'Calcula a nota
            Dim Nota As Double = Acertos * Valor_Questao

            'Grava no Banco
            Dim Registro_Nota As New Atividade.Dados
            Registro_Nota.Cod_Usuario = Usuario_Logado.Cod_Usuario
            Registro_Nota.Cod_Atividade = Cod_Atividade
            Registro_Nota.Pontos = Nota

            Atividade.Inserir_Notas(Registro_Nota)

            RegistrarScript("$('#myModalInfo').modal('hide')")
            Response.Redirect("../Cadastros/Atividade.aspx?Atv=" & Cod_Atividade)
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Cancelar_Finalizar_Click(sender As Object, e As EventArgs) Handles B_Cancelar_Finalizar.Click
        RegistrarScript("$('#myModalInfo').modal('hide')")
    End Sub

    Private Sub B_Voltar_Click(sender As Object, e As EventArgs) Handles B_Voltar.Click
        Dim Dado = Atividade.Carrega_Atividade(Cod_Atividade)
        Response.Redirect("../Consultas/Conteudo.aspx?Cod=" + Criptografia.Encryptdata(Util.CInteger(Dado.Cod_Curso)))
    End Sub



#End Region
End Class
