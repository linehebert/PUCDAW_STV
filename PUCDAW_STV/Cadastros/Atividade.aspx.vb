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

                If Finalizada <> Nothing Then
                    Realizar_Atividade.Visible = False
                    Atividade_Completa.Visible = True
                Else
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
            'Dt_Abertura.Text = Dado.Dt_Abertura.ToString("dd/MM/yyyy")
            Dt_Encerramento.Text = Dado.Dt_Fechamento.ToString("dd/MM/yyyy")
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
            L_A.Text = "A: " & Dado.Alternativa_A
            L_B.Text = "B: " & Dado.Alternativa_B
            L_C.Text = "C: " & Dado.Alternativa_C
            L_D.Text = "D: " & Dado.Alternativa_D
            L_Justificativa.Text = "Justificativa: " & Dado.Justificativa

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

                'Verifica se já existe resposta cadastrada para a questão atual
                'Dim Resposta As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao")), "Resposta")
                'If Resposta = "A" Then RB_A.Checked = True
                'If Resposta = "B" Then RB_B.Checked = True
                'If Resposta = "C" Then RB_C.Checked = True
                'If Resposta = "D" Then RB_D.Checked = True
                Dim Resposta As String = Biblio.Pega_Valor("SELECT Alternativa_Correta FROM Questao WHERE Cod_Questao=" + Util.Sql_String(CInt(Me.ViewState("Cod_Questao"))), "Alternativa_Correta")
                Dim Resposta_Usuario As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao")), "Resposta")

                'Guarda os dados para inserir na tabela de usuarioxrespostas
                Dim Registro As New Atividade.Dados
                Registro.Cod_Usuario = Usuario_Logado.Cod_Usuario
                Registro.Cod_Atividade = Cod_Atividade
                Registro.Cod_Questao = CInt(Me.ViewState("Cod_Questao"))

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

                'Carregar a próxima questão
                Dim Dado = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao")), "ASC")

                Me.ViewState("Cod_Questao") = Dado.Cod_Questao
                L_Questao.Text = Dado.Enunciado
                L_A.Text = "A: " & Dado.Alternativa_A
                L_B.Text = "B: " & Dado.Alternativa_B
                L_C.Text = "C: " & Dado.Alternativa_C
                L_D.Text = "D: " & Dado.Alternativa_D

                'verifica se é a ultima questao
                Dim Proxima = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao")), "ASC")
                If Proxima.Cod_Questao = Nothing Then
                    B_Proximo.Visible = False
                    B_Finalizar.Visible = True
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

                'Verifica se já existe resposta cadastrada para a questão atual
                'Dim Resposta As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao")), "Resposta")
                'If Resposta = "A" Then RB_A.Checked = True
                'If Resposta = "B" Then RB_B.Checked = True
                'If Resposta = "C" Then RB_C.Checked = True
                'If Resposta = "D" Then RB_D.Checked = True
                'If Resposta = Nothing Then
                '    RB_A.Checked = False
                '    RB_B.Checked = False
                '    RB_C.Checked = False
                '    RB_D.Checked = False
                'End If
                Dim Resposta As String = Biblio.Pega_Valor("SELECT Alternativa_Correta FROM Questao WHERE Cod_Questao=" + Util.Sql_String(CInt(Me.ViewState("Cod_Questao"))), "Alternativa_Correta")
                Dim Resposta_Usuario As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao")), "Resposta")

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
                If Resposta_Usuario = Nothing Then
                    Atividade.Inserir_Resposta(Registro)
                Else
                    Atividade.Alterar_Resposta(Registro)
                End If

                Dim Dado = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao")), "DESC")

                Me.ViewState("Cod_Questao") = Dado.Cod_Questao
                L_Questao.Text = Dado.Enunciado
                L_A.Text = "A: " & Dado.Alternativa_A
                L_B.Text = "B: " & Dado.Alternativa_B
                L_C.Text = "C: " & Dado.Alternativa_C
                L_D.Text = "D: " & Dado.Alternativa_D

                Dim Proxima = Atividade.Proxima_Questao(Cod_Atividade, CInt(Me.ViewState("Cod_Questao")), "DESC")
                If Proxima.Cod_Questao = Nothing Then
                    B_Anterior.Visible = False
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

                Dim Resposta As String = Biblio.Pega_Valor("SELECT Alternativa_Correta FROM Questao WHERE Cod_Questao=" + Util.Sql_String(CInt(Me.ViewState("Cod_Questao"))), "Alternativa_Correta")
                Dim Resposta_Usuario As String = Biblio.Pega_Valor("SELECT Resposta FROM usuarioxrespostas WHERE Cod_Usuario=" + Util.Sql_String(Usuario_Logado.Cod_Usuario) + " AND Cod_Questao=" + Util.Sql_String(Me.ViewState("Cod_Questao")), "Resposta")

                'Guarda os dados para inserir na tabela de usuarioxrespostas
                Dim Registro As New Atividade.Dados
                Registro.Cod_Usuario = Usuario_Logado.Cod_Usuario
                Registro.Cod_Atividade = Cod_Atividade
                Registro.Cod_Questao = CInt(Me.ViewState("Cod_Questao"))

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



#End Region
End Class
