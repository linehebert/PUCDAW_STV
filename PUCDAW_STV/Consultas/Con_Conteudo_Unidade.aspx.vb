Imports STV.Entidades
Imports System.IO
Imports STV.Seguranca
Partial Class Consultas_Con_Conteudo_Unidade : Inherits STV.Base.Page
    Dim _Material As Material
    Private ReadOnly Property Material As Material
        Get
            If IsNothing(_Material) Then _
                _Material = New Material

            Return _Material
        End Get
    End Property
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
    Dim _Usuario As Usuario
    Private ReadOnly Property Usuario As Usuario
        Get
            If IsNothing(_Usuario) Then _
                _Usuario = New Usuario
            Return _Usuario
        End Get
    End Property
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Div_Info_Modal.Visible = False
            L_Info.Visible = False

            B_Abrir.Visible = False
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
            If Not Page.IsPostBack() Then
                Monta_Dados_Unidade()

                'Libera inclusão somente se o usuário for ADM ou Instrutor
                If Usuario_Logado.ADM = True Or Usuario.Verifica_Responsabilidade(Usuario_Logado.Cod_Usuario) Then
                    B_Nova_Atividade.Visible = True
                    B_Novo_Material.Visible = True
                Else
                    B_Nova_Atividade.Visible = False
                    B_Novo_Material.Visible = False
                End If

                'Traz todas as Atividades
                Dim qntd_atividade As String = Biblio.Pega_Valor("SELECT Cod_Atividade FROM Atividade WHERE Cod_Unidade=" + Util.Sql_String(Cod_Unidade), "Cod_Atividade")
                If qntd_atividade = "" Then
                    Nenhuma_Atividade.Visible = True
                Else
                    Nenhuma_Atividade.Visible = False
                    Carrega_Atividades(Cod_Unidade)
                End If

                ' Traz todos os materiais
                Dim qntd_material As String = Biblio.Pega_Valor("SELECT Cod_Material FROM Materiais WHERE Cod_Unidade=" + Util.Sql_String(Cod_Unidade), "Cod_Material")
                If qntd_material = "" Then
                    Nenhum_Material.Visible = True
                Else
                    Nenhum_Material.Visible = False
                    Carrega_Materiais(Cod_Unidade)
                End If
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

#Region "Funções da Página"
    Private Sub Monta_Dados_Unidade()
        Try
            Dim Dado = Unidade.Carrega_Unidade(Cod_Unidade)
            L_Titulo.Text = Dado.Titulo

            If Dado.Dt_Termino < Date.Today() Then
                L_Curso_Unidade.InnerText = "Curso: " & Dado.Curso
                Div_Finalizado.Visible = True
                B_Nova_Atividade.Enabled = False
                B_Novo_Material.Enabled = False
                Me.ViewState("Enable") = False
                Me.ViewState("EditarAT") = "Não é possível editar atividades de um curso encerrado!"
                Me.ViewState("ExcluirAT") = "Não é possível excluir atividades de um curso encerrado!"
                Me.ViewState("ExcluirM") = "Não é possível excluir materiais de um curso encerrado!"
            Else
                L_Curso_Unidade.InnerText = "Curso: " & Dado.Curso
                Div_Finalizado.Visible = False
                B_Nova_Atividade.Enabled = True
                B_Novo_Material.Enabled = True
                Me.ViewState("Enable") = True
                Me.ViewState("EditarAT") = "Editar esta atividade"
                Me.ViewState("ExcluirAT") = "Excluir esta atividade"
                Me.ViewState("ExcluirM") = "Excluir este material"
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub B_Voltar_Click(sender As Object, e As System.EventArgs) Handles B_Voltar.Click
        Try
            Dim Cod_Curso As String = Biblio.Pega_Valor("SELECT Cod_Curso FROM Unidade WHERE Cod_Unidade= " + Util.CString(Cod_Unidade), "Cod_Curso")
            Response.Redirect("../Consultas/Con_Unidade.aspx?INST=S&Cod=" & Util.CString(Cod_Curso))
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try

    End Sub
#End Region

#Region "Atividade"
    Private Sub Carrega_Atividades(Cod_Unidade As Integer)
        Try
            rptAtividades.DataSource = Atividade.Carrega_Atividades(Cod_Unidade, False)
            rptAtividades.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub Limpa_Dados_Modal()
        Try
            TB_Titulo.Text = ""
            'TB_Dt_Abertura.Text = ""
            TB_Dt_Encerramento.Text = ""
            TB_Valor.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click
        Try
            Dim cod_curso As Integer = Biblio.Pega_Valor_Integer("SELECT Cod_Curso FROM UNIDADE Where Cod_Unidade = " + Util.CString(Cod_Unidade), "Cod_Curso")
            Dim dt_encerra_curso As String = Biblio.Pega_Valor("SELECT Dt_Termino FROM CURSO WHERE Cod_Curso = " + Util.CString(cod_curso), "Dt_Termino")
            If CDate(TB_Dt_Encerramento.Text) < CDate(dt_encerra_curso) And CDate(TB_Dt_Encerramento.Text) >= Date.Today() Then
                Dim Dados As New Atividade.Dados
                Dados.Titulo = TB_Titulo.Text
                'Dados.Dt_Abertura = TB_Dt_Abertura.Text
                Dados.Dt_Fechamento = TB_Dt_Encerramento.Text
                Dados.Valor = TB_Valor.Text

                If Me.ViewState("Atividade_Selecionada") Is Nothing Then
                    Dados.Cod_Unidade = Cod_Unidade
                    Atividade.Inserir_Atividade(Dados)

                    Nenhuma_Atividade.Visible = False
                    D_Aviso.Visible = True
                    L_Aviso.Visible = True
                    L_Aviso.Text = "Atividade cadastrada com sucesso!"
                Else
                    Dados.Cod_Atividade = CInt(Me.ViewState("Atividade_Selecionada"))
                    Atividade.Alterar_Atividade(Dados)

                    Nenhuma_Atividade.Visible = False
                    D_Aviso.Visible = True
                    L_Aviso.Visible = True
                    L_Aviso.Text = "Alterações realizadas com sucesso!"
                End If

                Limpa_Dados_Modal()
                Carrega_Atividades(Cod_Unidade)
                RegistrarScript("$('#myModalI').modal('hide')")
            Else
                If CDate(TB_Dt_Encerramento.Text) > CDate(dt_encerra_curso) Then

                    RegistrarScript("$('#myModalI').modal('show')")
                    Div_Info_Modal.Visible = True
                    L_Info.Visible = True
                    L_Info.Text = "A data de fechamento da atividade deve ser inferior a data de encerramento do curso"

                ElseIf CDate(TB_Dt_Encerramento.Text) < Date.Today() Then

                    RegistrarScript("$('#myModalI').modal('show')")
                    Div_Info_Modal.Visible = True
                    L_Info.Visible = True
                    L_Info.Text = "A data de fechamento da atividade deve ser igual ou superior a data atual"

                End If



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
            'TB_Dt_Abertura.Text = Dado.Dt_Abertura.ToString("yyyy-MM-dd")
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
            Dim atvpublicada As Boolean = Biblio.Pega_Valor_Boolean("SELECT Publica FROM Atividade WHERE Cod_Atividade = " + Util.Sql_String(CInt(Me.ViewState("Atividade_Selecionada"))), "Publica")
            If atvpublicada = True Then
                D_Aviso.Visible = False
                L_Aviso.Visible = False

                L_Titulo_Modal_E.InnerText = "Atenção!"
                L_Titulo_E.InnerText = "Não é possível excluir uma atividade publicada!"
                B_Confirma_Exclusao.Visible = False
                B_Confirma_Exclusao_Mat.Visible = False

                'Me.ViewState("Atividade_Selecionada") = e.CommandArgument.ToString()
                RegistrarScript("$('#myModalE').modal('show')")
            Else
                D_Aviso.Visible = False
                L_Aviso.Visible = False

                L_Titulo_Modal_E.InnerText = "Excluir Atividade:"
                L_Titulo_E.InnerText = "Tem certeza que deseja excluir esta atividade, bem como todo o seu conteúdo?"
                B_Confirma_Exclusao.Visible = True
                B_Confirma_Exclusao_Mat.Visible = False

                'Me.ViewState("Atividade_Selecionada") = e.CommandArgument.ToString()
                RegistrarScript("$('#myModalE').modal('show')")
            End If
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


            Dim qnt_atv As String = Biblio.Pega_Valor("SELECT Cod_Atividade FROM Atividade WHERE Cod_Unidade =" + Util.Sql_String(Cod_Unidade), "Cod_Atividade")
            If qnt_atv = "" Then
                Nenhuma_Atividade.Visible = True
            Else
                Nenhuma_Atividade.Visible = False
            End If

            Carrega_Atividades(Cod_Unidade)
            RegistrarScript("$('#myModalE').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub
#End Region

#Region "Materiais"
    Private Sub Carrega_Materiais(Cod_Unidade As Integer)
        Try
            rptMateriais.DataSource = Material.Carrega_Materiais(Cod_Unidade)
            rptMateriais.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub Limpa_Dados_Modal_Material()
        Try
            DDL_Tipo.SelectedValue = 0
            TB_Descricao.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub Carrega_Modal_Exclusao_Mat(sender As Object, e As CommandEventArgs)
        Try
            D_Aviso.Visible = False
            L_Aviso.Visible = False

            L_Titulo_Modal_E.InnerText = "Excluir Material:"
            L_Titulo_E.InnerText = "Tem certeza que deseja excluir este material?"
            B_Confirma_Exclusao.Visible = False
            B_Confirma_Exclusao_Mat.Visible = True

            Me.ViewState("Material_Selecionado") = e.CommandArgument.ToString()
            RegistrarScript("$('#myModalE').modal('show')")

        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub
    Private Sub B_Confirma_Exclusao_Mat_Click(sender As Object, e As EventArgs) Handles B_Confirma_Exclusao_Mat.Click
        Try
            Dim Dado As New Material.Dados
            Dado.Cod_Material = CInt(Me.ViewState("Material_Selecionado"))
            Material.Excluir_Material(Dado)

            Dim qnt_mat As String = Biblio.Pega_Valor("SELECT Cod_Material FROM Materiais WHERE Cod_Unidade =" + Util.Sql_String(Cod_Unidade), "Cod_Material")
            If qnt_mat = "" Then
                Nenhum_Material.Visible = True
            Else
                Nenhum_Material.Visible = False

            End If

            Carrega_Materiais(Cod_Unidade)
            RegistrarScript("$('#myModalE').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_Novo_Material_Click(sender As Object, e As System.EventArgs) Handles B_Novo_Material.Click
        Try
            Preenche_DDL_Tipo_Material()
            Limpa_Dados_Modal_Material()

            RegistrarScript("$('#myModalMat').modal('show')")
            L_TItulo_Modal_Mat.InnerText = "Novo Material:"
            D_Alerta.Visible = False
            Link.Visible = False
            Arquivo.Visible = False

        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_Salvar_Material_Click(sender As Object, e As EventArgs) Handles B_Salvar_Material.Click
        Try
            Dim Dados As New Material.Dados
            Dados.Titulo = TB_Descricao.Text
            Dados.Cod_Tipo = DDL_Tipo.SelectedValue
            Dados.Cod_Unidade = Cod_Unidade

            If DDL_Tipo.SelectedValue = 0 Then
                'Mensagem para selecionar um arquivo
                Throw New Exception("Selecione um tipo de material")
            ElseIf DDL_Tipo.SelectedValue = 1 Or DDL_Tipo.SelectedValue = 2 Then
                Dados.Material = TB_Link.Text
                Material.Inserir_Material(Dados)

                Nenhum_Material.Visible = False
                D_Aviso.Visible = True
                L_Aviso.Visible = True
                L_Aviso.Text = "Material cadastrado com sucesso!"
            Else
                'Salvar arquivo
                Salvar_Material(Dados)
                Nenhum_Material.Visible = False
            End If

            Limpa_Dados_Modal_Material()
            Carrega_Materiais(Cod_Unidade)
            RegistrarScript("$('#myModalMat').modal('hide')")

        Catch ex As Exception
            D_Alerta.Visible = True
            L_Alerta.Text = ex.Message
            RegistrarScript("$('#myModalMat').modal('show')")
        End Try
    End Sub


    Private Sub Salvar_Material(ByVal Dados As Material.Dados)
        Try
            D_Erro.Visible = False
            D_Aviso.Visible = False
            If FU_Arquivo.HasFile Then

                Dim Extensao As String = Path.GetExtension(FU_Arquivo.FileName).Replace(".", "").ToLower()
                Dim Caminho As String = ""

                'Verifica se a extensão esta de acordo com o tipo selecionado
                Select Case DDL_Tipo.SelectedValue
                    Case 3
                        If Extensao <> "mp4" And Extensao <> "webm" Then
                            Throw New Exception("Extensão Inválida")
                        End If
                    Case 4
                        If Extensao <> "pdf" Then
                            Throw New Exception("Extensão Inválida")
                        End If
                    Case 5
                        If Extensao <> "txt" And Extensao <> "docx" And Extensao <> "xls" And Extensao <> "doc" And Extensao <> "xlsx" And Extensao <> "zip" And Extensao <> "rar" And Extensao <> "ppt" And Extensao <> "pptx" Then
                            Throw New Exception("Extensão Inválida")
                        End If
                    Case 6
                        If Extensao <> "jpg" And Extensao <> "jpeg" And Extensao <> "png" And Extensao <> "gif" Then
                            Throw New Exception("Extensão Inválida")
                        End If
                    Case Else
                        Throw New Exception("Selecione um tipo de material")
                End Select

                'Salvar no banco
                Dados.Material = ""
                Dim Cod_Material = Material.Inserir_Material(Dados)

                'Atualizar Nome do Arquivo com Codigo do Material
                Dim Arquivo As String = ("/Anexos/" + Cod_Material.ToString() + "_" + TB_Descricao.Text + "." + Extensao)
                Dados.Material = "/Anexos/" + FU_Arquivo.FileName
                Dados.Cod_Material = Cod_Material
                Material.Alterar_Material(Dados)

                'Salva o Arquivo
                Caminho = Request.PhysicalApplicationPath
                FU_Arquivo.SaveAs(Caminho + "\Anexos\" + FU_Arquivo.FileName)

                Nenhum_Material.Visible = False
                D_Aviso.Visible = True
                L_Aviso.Visible = True
                L_Aviso.Text = "Material cadastrado com sucesso!"
            Else
                D_Erro.Visible = True
                L_Erro.Visible = True
                L_Erro.Text = "A extensão do arquivo é inválida."
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub DDL_Tipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDL_Tipo.SelectedIndexChanged
        If DDL_Tipo.SelectedValue = 1 Or DDL_Tipo.SelectedValue = 2 Then
            Link.Visible = True
            Arquivo.Visible = False
        ElseIf DDL_Tipo.SelectedValue > 2 Then
            Link.Visible = False
            Arquivo.Visible = True
        Else
            Link.Visible = False
            Arquivo.Visible = False
        End If

    End Sub

#End Region

#Region "Carrega DDL's"
    Protected Sub Preenche_DDL_Tipo_Material()
        Dim Tipo As New Material
        DDL_Tipo.DataSource = Material.Carrega_Tipo()
        DDL_Tipo.DataBind()

        Dim item As New ListItem
        item.Text = "Selecione um tipo de material"
        item.Value = 0
        DDL_Tipo.Items.Insert(0, item)
    End Sub

    Private Sub rptMateriais_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptMateriais.ItemCommand
        Try
            If e.CommandName = "ExibirMaterial" Then
                Dim Argument() As String = e.CommandArgument.Split(",")
                Dim Cod_Tipo = Argument(0)
                Dim Cod_Material = Argument(1)
                Dim Conteudo_Material As String = Biblio.Pega_Valor("SELECT Material FROM Materiais WHERE Cod_Material =" + Cod_Material, "Material")
                Dim URL As String = HttpContext.Current.Request.Url.Authority + Conteudo_Material

                Select Case Cod_Tipo
                    Case "1"
                        'Exibir vídeo com URL de terceiros
                        LIT_Video.Visible = True
                        LB_Download.Visible = False
                        LB_Material_Download.Visible = False

                        'Dim SB As New StringBuilder
                        'SB.Append("<iframe width = '868px' height='568px'")
                        'SB.Append("src = '" + Conteudo_Material + "' >")
                        'SB.Append("</iframe>")

                        Dim SB As New StringBuilder
                        SB.Append("<a class='embedly-card'  href='" + Conteudo_Material + "'> </a>")
                        SB.Append("<script Async src='//cdn.embedly.com/widgets/platform.js' charset='UTF-8'></script>")

                        LIT_Video.Text = SB.ToString
                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        B_Download.Visible = False
                        B_Abrir.Visible = False
                    Case "2"
                        'Abrir link para outros sites
                        LB_Download.Visible = False
                        LB_Material_Download.Visible = False
                        LIT_Video.Visible = True

                        Dim SB As New StringBuilder
                        SB.Append("<br /><center><h4>Este material contém um link de acesso a site de terceiros.<br />")
                        SB.Append("Para continuar e ser redirecionado a este link ")
                        SB.Append("<a target='_blank' href='" + Conteudo_Material + "'> clique aqui </a></h4></center>")

                        LIT_Video.Text = SB.ToString
                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        B_Download.Visible = False
                        B_Abrir.Visible = False
                    Case "3"
                        'Abrir modal para mostrar o vídeo
                        LIT_Video.Visible = True
                        LB_Download.Visible = False
                        LB_Material_Download.Visible = False

                        Dim SB As New StringBuilder
                        SB.Append("<video width='100%' height='100%' controls>")
                        SB.Append("<source src='http://" + URL + "' type='video/mp4'>")
                        SB.Append("<source src='http://" + URL + "' type='video/webm'>")
                        SB.Append(" Seu navegador não suporte HTML5 ")
                        SB.Append("</video>")

                        LIT_Video.Text = SB.ToString
                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        B_Download.Visible = True
                        B_Abrir.Visible = False

                    Case "4"
                        'Abrir pdf em nova guia
                        LIT_Video.Visible = False

                        Me.ViewState("Material_Selecionado") = Cod_Material
                        LB_Download.Visible = True
                        LB_Material_Download.Visible = True
                        LB_Material_Download.Text = Conteudo_Material.Replace("/Anexos/", "")

                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        B_Download.Visible = True
                        B_Abrir.Visible = True
                    Case "5"
                        'Fazer download do arquivo
                        LIT_Video.Visible = False

                        Me.ViewState("Material_Selecionado") = Cod_Material
                        LB_Download.Visible = True
                        LB_Material_Download.Visible = True
                        LB_Material_Download.Text = Conteudo_Material.Replace("/Anexos/", "")

                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        B_Download.Visible = True
                        B_Abrir.Visible = False
                    Case "6"
                        'Abrir modal para mostrar a imagem
                        LIT_Video.Visible = True
                        LB_Download.Visible = False

                        Dim SB As New StringBuilder
                        SB.Append("<img src='.." + Conteudo_Material + "' width='868px'  />")

                        LIT_Video.Text = SB.ToString
                        LB_Download.Visible = False
                        LB_Material_Download.Visible = False
                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        Me.ViewState("Material_Selecionado") = Cod_Material

                        B_Download.Visible = True
                        B_Abrir.Visible = False
                    Case Else

                End Select
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Download_Click(sender As Object, e As EventArgs) Handles B_Download.Click
        Try
            If Not Me.ViewState("Material_Selecionado") Is Nothing Then
                Dim mt As Material.Dados = Material.Carrega_Material(CInt(Me.ViewState("Material_Selecionado")))

                'Dim mt As Material.Dados = CType(Me.ViewState("Material_Dados"), Material.Dados)
                Dim Path As String = Mid(Request.PhysicalApplicationPath, 1, Request.PhysicalApplicationPath.Length - 1) + mt.Material.Replace("/", "\")
                Dim arquivo As FileInfo = New FileInfo(Path)

                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=" + arquivo.Name)
                Response.AddHeader("Content-Length", arquivo.Length.ToString())
                Select Case arquivo.Extension
                    Case "xls"
                        Response.ContentType = "application/vnd.ms-excel"
                    Case "xlsx"
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Case "doc"
                        Response.ContentType = "application/vnd.ms-word"
                    Case "docx"
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    Case "ppt"
                        Response.ContentType = "application/vnd.ms-powerpoint"
                    Case "pptx"
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation"
                    Case "png"
                        Response.ContentType = "image/png"
                    Case "jpg"
                        Response.ContentType = "image/jpg"
                    Case "jpeg"
                        Response.ContentType = "image/jpeg"
                    Case "gif"
                        Response.ContentType = "image/gif"
                    Case Else
                        Response.ContentType = "application/octet-stream"
                End Select

                'Response.WriteFile(arquivo.FullName)
                Response.TransmitFile(arquivo.FullName)
                'Response.Flush()
                'Response.End()

            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub Cancelar_Material_Click(sender As Object, e As EventArgs) Handles Cancelar_Material.Click
        D_Alerta.Visible = False
        Limpa_Dados_Modal_Material()
        Link.Visible = False
        Arquivo.Visible = False
    End Sub

    Private Sub B_Abrir_Click(sender As Object, e As EventArgs) Handles B_Abrir.Click
        Try
            If Not Me.ViewState("Material_Selecionado") Is Nothing Then
                Dim mt As Material.Dados = Material.Carrega_Material(CInt(Me.ViewState("Material_Selecionado")))

                'Dim mt As Material.Dados = CType(Me.ViewState("Material_Dados"), Material.Dados)
                Dim Path As String = Mid(Request.PhysicalApplicationPath, 1, Request.PhysicalApplicationPath.Length - 1) + mt.Material.Replace("/", "\")
                Dim arquivo As FileInfo = New FileInfo(Path)

                Session("ArquivoPDF") = arquivo
                RegistrarScript(String.Format("window.open('{0}','_blank')", ResolveUrl("ExibirMaterial.aspx")))
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    'Desabilita o botão de excluir materiais caso o curso esteja encerrado.
    Private Sub rptMateriais_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptMateriais.ItemDataBound
        Try
            Dim Excluir_Material As ImageButton = CType(e.Item.FindControl("Excluir_Material"), ImageButton)
            Excluir_Material.Enabled = Me.ViewState("Enable")
            Excluir_Material.ToolTip = Me.ViewState("ExcluirM")

        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    'Desabilita os botões de alterar/excluir as atividades caso o curso esteja encerrado.
    Private Sub rptAtividades_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptAtividades.ItemDataBound
        Try
            Dim Editar As ImageButton = CType(e.Item.FindControl("Editar"), ImageButton)
            Dim Excluir_Atividade As ImageButton = CType(e.Item.FindControl("Excluir_Atividade"), ImageButton)
            Editar.Enabled = Me.ViewState("Enable")
            Excluir_Atividade.Enabled = Me.ViewState("Enable")

            Editar.ToolTip = Me.ViewState("EditarAT")
            Excluir_Atividade.ToolTip = Me.ViewState("ExcluirAT")

        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub


#End Region

End Class
