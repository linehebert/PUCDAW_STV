Imports STV.Entidades
Imports STV.Seguranca
Partial Class Consultas_Con_Unidade : Inherits STV.Base.Page

    Dim _Unidade As Unidade
    Private ReadOnly Property Unidade As Unidade
        Get
            If IsNothing(_Unidade) Then _
                _Unidade = New Unidade

            Return _Unidade
        End Get
    End Property

    Dim _Curso As Curso
    Private ReadOnly Property Curso As Curso
        Get
            If IsNothing(_Curso) Then _
                _Curso = New Curso

            Return _Curso
        End Get
    End Property
    Private ReadOnly Property Cod_Curso As Integer
        Get
            Return Request("Cod")
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
                    B_Nova_Unidade.Visible = False
                End If
                Monta_Dados_Curso()

                Dim qntd_unidade As String = Biblio.Pega_Valor("SELECT Cod_Unidade FROM Unidade WHERE Cod_Curso=" + Util.Sql_String(Cod_Curso), "Cod_Unidade")
                If qntd_unidade = "" Then
                    Nenhuma_Unidade.Visible = True
                Else
                    Nenhuma_Unidade.Visible = False
                    Carrega_Unidades(Cod_Curso)
                End If
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub Carrega_Unidades(Cod_Curso As Integer)
        Try
            rptUnidades.DataSource = Unidade.Carrega_Unidades(Cod_Curso)
            rptUnidades.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub Monta_Dados_Curso()
        Try
            Dim Dado = Curso.Carrega_Curso(Cod_Curso)

            L_Titulo.Text = Dado.Titulo
            L_Dt_Inicio.Text = Dado.Dt_Inicio.ToString("dd/MM/yyyy")
            L_Dt_Termino.Text = Dado.Dt_Termino.ToString("dd/MM/yyyy")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub B_Voltar_Click(sender As Object, e As System.EventArgs) Handles B_Voltar.Click
        Try
            If Request("INST") = "S" Then
                Response.Redirect("../Consultas/Con_Curso.aspx?INST=S")
            Else
                Response.Redirect("../Consultas/Con_Curso.aspx")
            End If

        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try

    End Sub

    Protected Sub B_Nova_Unidade_Click(sender As Object, e As EventArgs) Handles B_Nova_Unidade.Click
        Try
            L_TItulo_Modal.InnerText = "Nova Unidade:"
            TB_Titulo.Text = ""
            Me.ViewState("Unidade_Selecionada") = Nothing
            RegistrarScript("$('#myModalI').modal('show')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try

    End Sub

    Protected Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click
        Try
            Dim Dados As New Unidade.Dados
            Dados.Titulo = TB_Titulo.Text

            If Me.ViewState("Unidade_Selecionada") Is Nothing Then
                Dados.Cod_Curso = Cod_Curso
                Unidade.Inserir(Dados)
            Else
                Dados.Cod_Unidade = CInt(Me.ViewState("Unidade_Selecionada"))
                Unidade.Alterar(Dados)
                Me.ViewState("Unidade_Selecionada") = Nothing
            End If

            TB_Titulo.Text = ""

            Nenhuma_Unidade.Visible = False
            Carrega_Unidades(Cod_Curso)
            RegistrarScript("$('#myModalI').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub Carrega_Modal_Alteracao(sender As Object, e As CommandEventArgs)
        Try
            Dim Dado = Unidade.Carrega_Unidade(e.CommandArgument.ToString)
            TB_Titulo.Text = Dado.Titulo
            L_TItulo_Modal.InnerText = "Renomear Unidade:"

            Me.ViewState("Unidade_Selecionada") = e.CommandArgument.ToString()
            RegistrarScript("$('#myModalI').modal('show')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub Carrega_Modal_Exclusao(sender As Object, e As CommandEventArgs)
        Try
            Me.ViewState("Unidade_Selecionada") = e.CommandArgument.ToString()
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
            Dim Dado As New Unidade.Dados
            Dado.Cod_Unidade = CInt(Me.ViewState("Unidade_Selecionada"))
            Unidade.Excluir_Unidade(Dado)

            Dim qntd_unidade As String = Biblio.Pega_Valor("SELECT Cod_Unidade FROM Unidade WHERE Cod_Curso=" + Util.Sql_String(Cod_Curso), "Cod_Unidade")
            If qntd_unidade = "" Then
                Nenhuma_Unidade.Visible = True
            End If

            Carrega_Unidades(Cod_Curso)
            RegistrarScript("$('#myModalE').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try

    End Sub
End Class
