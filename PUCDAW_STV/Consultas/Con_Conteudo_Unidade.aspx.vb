Imports STV.Entidades
Imports STV.Seguranca
Partial Class Consultas_Con_Conteudo_Unidade : Inherits STV.Base.Page

    Dim _Unidade As Unidade
    Private ReadOnly Property Unidade As Unidade
        Get
            If IsNothing(_Unidade) Then _
                _Unidade = New Unidade

            Return _Unidade
        End Get
    End Property

    Dim _Atividade As Atividade
    Private ReadOnly Property Atividade As Atividade
        Get
            If IsNothing(_Atividade) Then _
                _Atividade = New Atividade

            Return _Atividade
        End Get
    End Property
    Private ReadOnly Property Cod_Unidade As Integer
        Get
            Return Request("Unit")
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
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack() Then
                If Usuario_Logado.ADM = True Then
                    B_Nova_Atividade.Visible = False
                    B_Novo_Material.Visible = False
                End If
                Monta_Dados_Unidade()

                Dim qntd_unidade As String = Biblio.Pega_Valor("SELECT Cod_Atividade FROM Atividade WHERE Cod_Unidade=" + Util.Sql_String(Cod_Unidade), "Cod_Atividade")
                If qntd_unidade = "" Then
                    Nenhuma_Atividade.Visible = True
                Else
                    Nenhuma_Atividade.Visible = False
                    Carrega_Atividades(Cod_Unidade)
                End If
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub Carrega_Atividades(Cod_Unidade As Integer)
        Try
            rptAtividades.DataSource = Atividade.Carrega_Atividades(Cod_Unidade, False)
            rptAtividades.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub Monta_Dados_Unidade()
        Try
            Dim Dado = Unidade.Carrega_Unidade(Cod_Unidade)
            L_Titulo.Text = Dado.Titulo
            L_Curso_Unidade.Text = Dado.Curso
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub Limpa_Dados_Modal()
        Try
            TB_Titulo.Text = ""
            TB_Dt_Abertura.Text = ""
            TB_Dt_Encerramento.Text = ""
            TB_Valor.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub B_Voltar_Click(sender As Object, e As System.EventArgs) Handles B_Voltar.Click
        Try
            Response.Redirect("../Consultas/Con_Curso.aspx")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try

    End Sub
    Private Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click
        Try
            If TB_Dt_Encerramento.Text >= Today Then
                Dim Dados As New Atividade.Dados
                Dados.Titulo = TB_Titulo.Text
                Dados.Dt_Abertura = TB_Dt_Abertura.Text
                Dados.Dt_Fechamento = TB_Dt_Encerramento.Text
                Dados.Valor = TB_Valor.Text

                If Me.ViewState("Atividade_Selecionada") Is Nothing Then
                    Dados.Cod_Unidade = Cod_Unidade
                    Atividade.Inserir_Atividade(Dados)
                Else
                    Dados.Cod_Atividade = CInt(Me.ViewState("Atividade_Selecionada"))
                    Atividade.Alterar_Atividade(Dados)
                End If

                Limpa_Dados_Modal()
                Carrega_Atividades(Cod_Unidade)
                RegistrarScript("$('#myModalI').modal('hide')")
            Else
                L_Info.Text = "A data de término do curso não pode ser inferior a data atual, informe uma nova data de término."
            End If

        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub
    Protected Sub B_Nova_Atividade_Click(sender As Object, e As System.EventArgs) Handles B_Nova_Atividade.Click
        Try
            Limpa_Dados_Modal()
            L_TItulo_Modal.InnerText = "Nova Atividade:"
            Me.ViewState("Atividade_Selecionada") = Nothing
            RegistrarScript("$('#myModalI').modal('show')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub Carrega_Modal_Alteracao(sender As Object, e As CommandEventArgs)
        Try
            Dim Dado = Atividade.Carrega_Atividade(e.CommandArgument.ToString)
            TB_Titulo.Text = Dado.Titulo
            TB_Dt_Abertura.Text = Dado.Dt_Abertura.ToString("yyyy-MM-dd")
            TB_Dt_Encerramento.Text = Dado.Dt_Fechamento.ToString("yyyy-MM-dd")
            TB_Valor.Text = Dado.Valor
            L_TItulo_Modal.InnerText = "Alterar Atividade:"

            Me.ViewState("Atividade_Selecionada") = e.CommandArgument.ToString()
            RegistrarScript("$('#myModalI').modal('show')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub Carrega_Modal_Exclusao(sender As Object, e As CommandEventArgs)
        Try
            Me.ViewState("Atividade_Selecionada") = e.CommandArgument.ToString()
            RegistrarScript("$('#myModalE').modal('show')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_Fecha_Exclusao_Click(sender As Object, e As EventArgs) Handles B_Fecha_Exclusao.Click
        Try
            RegistrarScript("$('#myModalE').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_Fechar_Click(sender As Object, e As EventArgs) Handles B_Fechar.Click
        Try
            RegistrarScript("$('#myModalI').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Confirma_Exclusao_Click(sender As Object, e As EventArgs) Handles B_Confirma_Exclusao.Click
        Try
            Dim Dado As New Atividade.Dados
            Dado.Cod_Atividade = CInt(Me.ViewState("Atividade_Selecionada"))
            Atividade.Excluir_Conteudo_Atividade(Dado)
            Atividade.Excluir_Atividade(Dado)

            Carrega_Atividades(Cod_Unidade)
            RegistrarScript("$('#myModalE').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub


End Class
