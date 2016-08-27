Imports STV
Imports STV.Entidades
Imports System.Web.Services
Partial Class Cadastros_Cad_Atividade : Inherits STV.Base.Page

    Dim _Atividade As Atividade
    Private ReadOnly Property Atividade As Atividade
        Get
            If IsNothing(_Atividade) Then _
                _Atividade = New Atividade

            Return _Atividade
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

                If Not IsNothing(Cod_Atividade) Then
                    Cad_Questões.Visible = True
                    Monta_Dados()

                    Carrega_Questoes(Cod_Atividade)
                End If
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try


        'If TB_Alternativa_A.Text = "" Then
        '    Throw New ApplicationException("Alternativa A não pode ser vazia")
        'End If

        'Catch ex2 As ApplicationException
        '    L_Erro.Text = ex2.Message
        '    D_Erro.Visible = True
        'Catch ex As Exception
        '    L_Erro.Text = ex.Message
        '    D_Erro.Visible = True
        '    'Throw
        'End Try
    End Sub

    Protected Sub Carrega_Questoes(Cod_Atividade As Integer)
        Try
            rptQuestoes.DataSource = Atividade.Carrega_Questoes(Cod_Atividade)
            rptQuestoes.DataBind()
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub Monta_Dados()
        Try
            Dim Dado = Atividade.Carrega_Atividade(Cod_Atividade)
            Unidade.Text = Dado.Unidade
            Curso.Text = Dado.Curso

            Titulo.Text = Dado.Titulo
            Dt_Abertura.Text = Dado.Dt_Abertura.ToString("dd/MM/yyyy")
            Dt_Encerramento.Text = Dado.Dt_Fechamento.ToString("dd/MM/yyyy")
            Valor.Text = Dado.Valor
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub Monta_Questao(Cod_Questao As Integer)
        Try
            Dim Dado = Atividade.Carrega_Questao(Cod_Questao)

            TB_Enunciado.Text = Dado.Enunciado
            TB_Alternativa_A.Text = Dado.Alternativa_A
            TB_Alternativa_B.Text = Dado.Alternativa_B
            TB_Alternativa_C.Text = Dado.Alternativa_C
            TB_Alternativa_D.Text = Dado.Alternativa_D
            RBL_Alternativa_Correta.SelectedValue = Dado.Alternativa_Correta
            TB_Justificativa.Text = Dado.Justificativa
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub B_Add_Questao_Click(sender As Object, e As EventArgs) Handles B_Add_Questao.Click
        Try
            Me.ViewState("Questao_Selecionada") = Nothing
            Limpar_Cadastro()
            RegistrarScript("$('#myModal').modal('show')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub


    Protected Sub B_Salvar_Questao_Click(sender As Object, e As EventArgs) Handles B_Salvar_Questao.Click
        Try
            Dim Dado As New Atividade.Dados

            Dado.Enunciado = TB_Enunciado.Text
            Dado.Alternativa_A = TB_Alternativa_A.Text
            Dado.Alternativa_B = TB_Alternativa_B.Text
            Dado.Alternativa_C = TB_Alternativa_C.Text
            Dado.Alternativa_D = TB_Alternativa_D.Text
            Dado.Alternativa_Correta = RBL_Alternativa_Correta.SelectedValue
            Dado.Justificativa = TB_Justificativa.Text

            If Me.ViewState("Questao_Selecionada") Is Nothing Then
                Dado.Cod_Atividade = Cod_Atividade
                Atividade.Inserir_Questao(Dado)
            Else
                Dado.Cod_Questao = CInt(Me.ViewState("Questao_Selecionada"))
                Atividade.Alterar_Questao(Dado)
                Me.ViewState("Questao_Selecionada") = Nothing
            End If

            Limpar_Cadastro()
            Carrega_Questoes(Cod_Atividade)
            RegistrarScript("$('#myModal').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub Limpar_Cadastro()
        Try
            TB_Enunciado.Text = ""
            TB_Alternativa_A.Text = ""
            TB_Alternativa_B.Text = ""
            TB_Alternativa_C.Text = ""
            TB_Alternativa_D.Text = ""
            RBL_Alternativa_Correta.SelectedValue = "A"
            TB_Justificativa.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub Carrega_Modal(sender As Object, e As CommandEventArgs)
        Try
            Monta_Questao(e.CommandArgument.ToString)
            Me.ViewState("Questao_Selecionada") = e.CommandArgument.ToString
            RegistrarScript("$('#myModal').modal('show')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub Carrega_Modal_Exclusao(sender As Object, e As CommandEventArgs)
        Try
            Me.ViewState("Questao_Selecionada") = e.CommandArgument.ToString
            RegistrarScript("$('#myModalE').modal('show')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_Fecha_Exclusao_Click(sender As Object, e As EventArgs) Handles B_Fecha_Exclusao.Click
        Try
            Me.ViewState("Questao_Selecionada") = Nothing
            RegistrarScript("$('#myModalE').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try

    End Sub

    Protected Sub B_Fechar_Click(sender As Object, e As EventArgs) Handles B_Fechar.Click
        Try
            RegistrarScript("$('#myModal').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Confirma_Exclusao_Click(sender As Object, e As EventArgs) Handles B_Confirma_Exclusao.Click
        Try
            Dim Dado As New Atividade.Dados
            Dado.Cod_Questao = CInt(Me.ViewState("Questao_Selecionada"))
            Atividade.Excluir_Questao(Dado)

            Carrega_Questoes(Cod_Atividade)
            RegistrarScript("$('#myModalE').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Voltar_Click(sender As Object, e As EventArgs) Handles B_Voltar.Click
        Try
            Response.Redirect("../Consultas/Con_Conteudo_Unidade.aspx?Unit=" & Cod_Unidade)
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub
End Class
