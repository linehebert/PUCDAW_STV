Imports STV.Entidades
Imports System.IO
Imports STV.Seguranca

Partial Class Consultas_Conteudo : Inherits STV.Base.Page

    Dim _Curso As Curso
    Private ReadOnly Property Curso As Curso
        Get
            If IsNothing(_Curso) Then _
                _Curso = New Curso
            Return _Curso
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

    Private ReadOnly Property Cod_Curso As String
        Get
            Return Criptografia.Decryptdata(Request("Cod"))
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

    Dim _Atividade As Atividade
    Private ReadOnly Property Atividade As Atividade
        Get
            If IsNothing(_Atividade) Then _
                _Atividade = New Atividade
            Return _Atividade
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
    Dim _Material As Material
    Private ReadOnly Property Material As Material
        Get
            If IsNothing(_Material) Then _
                _Material = New Material

            Return _Material
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
            If Not Page.IsPostBack() Then

                If Verifica_Aprovacao(Cod_Curso) = True Then
                    B_Gerar_Certificado.Visible = True
                    Certificado.Visible = True
                Else
                    B_Gerar_Certificado.Visible = False
                    Certificado.Visible = False
                End If
                Monta_Dados()

                Dim qntd_unidade As String = Biblio.Pega_Valor("SELECT Cod_Unidade FROM Unidade WHERE Cod_Curso=" + Cod_Curso, "Cod_Unidade")
                If qntd_unidade = Nothing Then
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

    Private Function Verifica_Aprovacao(Cod_Curso As Integer) As Boolean
        'Verifica se o curso já encerrou
        Dim Dado = Curso.Carrega_Curso(Cod_Curso)
        If Dado.Dt_Termino < Date.Today Then
            Dim NotaMaxima As Double = Curso.Nota_Maxima(Cod_Curso)
            Dim NotaAluno As Double = Curso.Nota_Aluno(Cod_Curso, Usuario_Logado.Cod_Usuario)

            If (NotaAluno * 100) / NotaMaxima < 70 Then
                Return False
            Else
                'Verificar se visualizou todos os materiais
                Dim TotalMateriais As Integer = Curso.Quantidade_Materiais(Cod_Curso, 0)
                Dim TotalVisualizados As Integer = Curso.Quantidade_Visualizados(Cod_Curso, Usuario_Logado.Cod_Usuario, 0)

                If TotalMateriais = TotalVisualizados Then
                    Return True
                Else
                    Return False
                End If
            End If
        Else
            'Curso nao encerrado
            Return False
        End If



    End Function

    Private Sub Monta_Dados()
        Try
            Dim Dado = Curso.Carrega_Curso(Cod_Curso)
            L_Curso_Unidade.Text = Dado.Titulo

            L_Dt_Inicio.Text = Dado.Dt_Inicio.ToString("dd/MM/yyyy")
            L_Dt_Termino.Text = Dado.Dt_Termino.ToString("dd/MM/yyyy")

            If Dado.Dt_Termino < Date.Today Then
                Div_Finalizado.Visible = True
            Else
                Div_Finalizado.Visible = False
            End If

        Catch ex As Exception
            Throw
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


    Private Sub rptUnidades_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptUnidades.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rptAtividades As Repeater = CType(e.Item.FindControl("rptAtividades"), Repeater)
            Dim Dt As Data.DataTable = Atividade.Carrega_Atividades(CInt(e.Item.DataItem("Cod_Unidade")), True)
            rptAtividades.DataSource = Dt
            rptAtividades.DataBind()

            If Dt.Rows.Count = 0 Then
                Dim Footer As Control = rptAtividades.Controls(rptAtividades.Controls.Count - 1).Controls(0)
                CType(Footer.FindControl("L_Empty"), Label).Visible = True
            End If

            Dim rptMateriais As Repeater = CType(e.Item.FindControl("rptMateriais"), Repeater)
            Dim Dtm As Data.DataTable = Material.Carrega_Materiais(CInt(e.Item.DataItem("Cod_Unidade")))
            rptMateriais.DataSource = Dtm
            rptMateriais.DataBind()

            If Dtm.Rows.Count = 0 Then
                Dim Footer As Control = rptMateriais.Controls(rptMateriais.Controls.Count - 1).Controls(0)
                CType(Footer.FindControl("L_Emptym"), Label).Visible = True
            End If

        End If
    End Sub

#Region "Materiais"

    Private Sub B_Download_Click(sender As Object, e As EventArgs) Handles B_Download.Click
        Try
            If Not Me.ViewState("Material_Selecionado") Is Nothing Then
                Dim mt As Material.Dados = Material.Carrega_Material(CInt(Me.ViewState("Material_Selecionado")))

                'Dim mt As Material.Dados = CType(Me.ViewState("Material_Dados"), Material.Dados)
                Dim Path As String = Mid(Request.PhysicalApplicationPath, 1, Request.PhysicalApplicationPath.Length - 1) + mt.Material.Replace("/", "\")
                Dim arquivo As FileInfo = New FileInfo(Path)

                'Verifica se o curso já encerrou
                Dim Dado = Curso.Carrega_Curso(Cod_Curso)
                If Dado.Dt_Termino > Date.Today Then
                    'Grava a visualização do material
                    Dim existe As String = Biblio.Pega_Valor("SELECT Cod_Material FROM MATERIAISxUSUARIO WHERE Cod_Usuario=" + Util.Sql_Numero(Usuario_Logado.Cod_Usuario) + "AND Cod_Material=" + Util.Sql_Numero(Me.ViewState("Material_Selecionado")), "Cod_Material")
                    If existe = Nothing Then
                        Dim Dados As New Material.Dados
                        Dados.Cod_Usuario = Usuario_Logado.Cod_Usuario
                        Dados.Cod_Material = CInt(Me.ViewState("Material_Selecionado"))
                        Material.Visualiza_Material(Dados)
                    End If
                End If

                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=" + arquivo.Name)
                Response.AddHeader("Content-Length", arquivo.Length.ToString())
                Select Case (Replace(arquivo.Extension, ".", ""))
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

    Public Sub rptMateriais_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "ExibirMaterial" Then
                Dim Argument() As String = e.CommandArgument.Split(",")
                Dim Cod_Tipo = Argument(0)
                Dim Cod_Material = Argument(1)
                Dim Conteudo_Material As String = Biblio.Pega_Valor("SELECT Material FROM Materiais WHERE Cod_Material =" + Cod_Material, "Material")
                Dim URL As String = HttpContext.Current.Request.Url.Authority + Conteudo_Material

                'Verifica se o curso já encerrou
                Dim Dado = Curso.Carrega_Curso(Cod_Curso)

                Select Case Cod_Tipo
                    Case "1"
                        'Exibir vídeo com URL de terceiros
                        LIT_Video.Visible = True
                        LB_Download.Visible = False

                        Dim SB As New StringBuilder
                        SB.Append("<iframe width = '868px' height='568px'")
                        SB.Append("src = '" + Conteudo_Material + "' >")
                        SB.Append("</iframe>")

                        LIT_Video.Text = SB.ToString
                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        B_Download.Visible = False

                        If Dado.Dt_Termino > Date.Today Then
                            'Grava a visualização do material
                            Dim existe As String = Biblio.Pega_Valor("SELECT Cod_Material FROM MATERIAISxUSUARIO WHERE Cod_Usuario=" + Util.Sql_Numero(Usuario_Logado.Cod_Usuario) + "AND Cod_Material=" + Util.Sql_Numero(Cod_Material), "Cod_Material")
                            If existe = Nothing Then
                                Dim Dados As New Material.Dados
                                Dados.Cod_Usuario = Usuario_Logado.Cod_Usuario
                                Dados.Cod_Material = Cod_Material
                                Material.Visualiza_Material(Dados)
                            End If
                        End If
                    Case "2"
                        'Abrir link para outros sites
                        LB_Download.Visible = False
                        LIT_Video.Visible = True
                        LB_Material_Download.Visible = False

                        Dim SB As New StringBuilder
                        SB.Append("<br /><center><h4>Este material contém um link de acesso a site de terceiros.<br />")
                        SB.Append("Para continuar e ser redirecionado a este link ")
                        SB.Append("<a target='_blank' href='" + Conteudo_Material + "'> clique aqui </a></h4></center>")

                        LIT_Video.Text = SB.ToString
                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        B_Download.Visible = False

                        If Dado.Dt_Termino > Date.Today Then
                            'Grava a visualização do material
                            Dim existe As String = Biblio.Pega_Valor("SELECT Cod_Material FROM MATERIAISxUSUARIO WHERE Cod_Usuario=" + Util.Sql_Numero(Usuario_Logado.Cod_Usuario) + "AND Cod_Material=" + Util.Sql_Numero(Cod_Material), "Cod_Material")
                            If existe = Nothing Then
                                Dim Dados As New Material.Dados
                                Dados.Cod_Usuario = Usuario_Logado.Cod_Usuario
                                Dados.Cod_Material = Cod_Material
                                Material.Visualiza_Material(Dados)
                            End If
                        End If
                    Case "3"
                        'Abrir modal para mostrar o vídeo
                        LIT_Video.Visible = True
                        LB_Download.Visible = False

                        Dim SB As New StringBuilder
                        SB.Append("<video width='868px' height='568px' controls>")
                        SB.Append("<source src='http://" + URL + "' type='video/mp4'>")
                        SB.Append("<source src='http://" + URL + "' type='video/webm'>")
                        SB.Append(" Seu navegador não suporte HTML5 ")
                        SB.Append("</video>")

                        LIT_Video.Text = SB.ToString
                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        B_Download.Visible = True

                        If Dado.Dt_Termino > Date.Today Then
                            'Grava a visualização do material
                            Dim existe As String = Biblio.Pega_Valor("SELECT Cod_Material FROM MATERIAISxUSUARIO WHERE Cod_Usuario=" + Util.Sql_Numero(Usuario_Logado.Cod_Usuario) + "AND Cod_Material=" + Util.Sql_Numero(Cod_Material), "Cod_Material")
                            If existe = Nothing Then
                                Dim Dados As New Material.Dados
                                Dados.Cod_Usuario = Usuario_Logado.Cod_Usuario
                                Dados.Cod_Material = Cod_Material
                                Material.Visualiza_Material(Dados)
                            End If
                        End If
                    Case "4"
                        'Abrir pdf em nova guia
                        B_Abrir.Visible = True
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
                        B_Abrir.Visible = False
                        LIT_Video.Visible = False

                        Me.ViewState("Material_Selecionado") = Cod_Material
                        LB_Download.Visible = True
                        LB_Material_Download.Visible = True
                        LB_Material_Download.Text = Conteudo_Material.Replace("/Anexos/", "")

                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        B_Download.Visible = True

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


                        If Dado.Dt_Termino > Date.Today Then

                            'Grava a visualização do material
                            Dim existe As String = Biblio.Pega_Valor("SELECT Cod_Material FROM MATERIAISxUSUARIO WHERE Cod_Usuario=" + Util.Sql_Numero(Usuario_Logado.Cod_Usuario) + "AND Cod_Material=" + Util.Sql_Numero(Cod_Material), "Cod_Material")
                            If existe = Nothing Then
                                Dim Dados As New Material.Dados
                                Dados.Cod_Usuario = Usuario_Logado.Cod_Usuario
                                Dados.Cod_Material = Cod_Material
                                Material.Visualiza_Material(Dados)
                            End If

                        End If
                    Case Else

                End Select
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Abrir_Click(sender As Object, e As EventArgs) Handles B_Abrir.Click
        Try
            If Not Me.ViewState("Material_Selecionado") Is Nothing Then

                'Verifica se o curso já encerrou
                Dim Dado = Curso.Carrega_Curso(Cod_Curso)
                If Dado.Dt_Termino > Date.Today Then
                    'Grava a visualização do material
                    Dim existe As String = Biblio.Pega_Valor("SELECT Cod_Material FROM MATERIAISxUSUARIO WHERE Cod_Usuario=" + Util.Sql_Numero(Usuario_Logado.Cod_Usuario) + "AND Cod_Material=" + Util.Sql_Numero(Me.ViewState("Material_Selecionado")), "Cod_Material")
                    If existe = Nothing Then
                        Dim Dados As New Material.Dados
                        Dados.Cod_Usuario = Usuario_Logado.Cod_Usuario
                        Dados.Cod_Material = CInt(Me.ViewState("Material_Selecionado"))
                        Material.Visualiza_Material(Dados)
                    End If
                End If

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

    Private Sub B_Gerar_Certificado_Click(sender As Object, e As ImageClickEventArgs) Handles B_Gerar_Certificado.Click
        Response.Redirect("../Relatorios/Certificado.aspx?Curso=" & Criptografia.Encryptdata(Cod_Curso))
    End Sub

    Private Sub B_Voltar_Click(sender As Object, e As EventArgs) Handles B_Voltar.Click
        Response.Redirect("../Consultas/Con_Meus_Cursos.aspx")
    End Sub

    'Avaliar Curso
    Private Sub B_Avaliar_Click(sender As Object, e As EventArgs) Handles B_Avaliar.Click

        Dim Dado = Curso.Carrega_Avaliacao(Cod_Curso, Usuario_Logado.Cod_Usuario)
        If Dado.Avaliacao <> 0 Then
            RegistrarScript("PintarEstrelas(" + Dado.Avaliacao.ToString() + ");")
            TB_Comentario.Text = Dado.Comentario
        Else
            um.Checked = False
            dois.Checked = False
            tres.Checked = False
            quatro.Checked = False
            cinco.Checked = False
            TB_Comentario.Text = ""
        End If
        RegistrarScript("$('#myModalAv').modal('show')")
    End Sub
    'Salvar Avaliação
    Private Sub B_Confirma_Avaliacao_Click(sender As Object, e As EventArgs) Handles B_Confirma_Avaliacao.Click
        Try
            Dim Dados As New Curso.Dados
            Dados.Cod_Curso = Cod_Curso
            Dados.Cod_Usuario = Usuario_Logado.Cod_Usuario
            Dados.Comentario = TB_Comentario.Text

            If um.Checked = True Then
                Dados.Avaliacao = 1
            ElseIf dois.Checked = True Then
                Dados.Avaliacao = 2
            ElseIf tres.Checked = True Then
                Dados.Avaliacao = 3
            ElseIf quatro.Checked = True Then
                Dados.Avaliacao = 4
            ElseIf cinco.Checked = True Then
                Dados.Avaliacao = 5
            End If

            'Verificar se já tem avaliação e dar update ************** IF e Criar Função de update.
            Dim Dado = Curso.Carrega_Avaliacao(Cod_Curso, Usuario_Logado.Cod_Usuario)
            If Dado.Avaliacao <> 0 Then
                Curso.Alterar_Avaliacao(Dados)
            Else
                Curso.Inserir_Avaliacao(Dados)
            End If

            TB_Comentario.Text = ""
            RegistrarScript("$('#myModalAv').modal('hide')")
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub


#End Region
End Class
