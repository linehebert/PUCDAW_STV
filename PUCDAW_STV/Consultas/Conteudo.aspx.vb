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

    Private Sub Monta_Dados()
        Try
            Dim Dado = Curso.Carrega_Curso(Cod_Curso)
            L_Curso_Unidade.Text = Dado.Titulo
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
    Private Sub Carrega_Atividades(Cod_Unidade As Integer)
        Try
            rptUnidades.DataSource = Atividade.Carrega_Atividades(Cod_Unidade, True)
            rptUnidades.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub Carrega_Materiais(Cod_Unidade As Integer)
        Try
            rptMateriais.DataSource = Material.Carrega_Materiais(Cod_Unidade)
            rptMateriais.DataBind()
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
                        Dim SB As New StringBuilder
                        SB.Append("<iframe width = '420' height='315'")
                        SB.Append("src = '" + Conteudo_Material + "' >")
                        SB.Append("</iframe>")

                        LIT_Video.Text = SB.ToString
                        RegistrarScript("$('#myModalExibicao').modal('show')")

                    Case "2"
                        'Abrir link para outros sites

                    Case "3"
                        'Abrir modal para mostrar o vídeo
                        Dim SB As New StringBuilder
                        SB.Append("<video width='320' height='240' controls>")
                        SB.Append("<source src='http://" + URL + "' type='video/mp4'>")
                        SB.Append("<source src='http://" + URL + "' type='video/webm'>")
                        SB.Append(" Seu navegador não suporte HTML5 ")
                        SB.Append("</video>")

                        LIT_Video.Text = SB.ToString
                        RegistrarScript("$('#myModalExibicao').modal('show')")

                    Case "4"
                        'Abrir pdf em nova guia
                    Case "5"
                        'Fazer download do arquivo
                        Me.ViewState("Material_Selecionado") = Cod_Material
                        LB_Download.Visible = True
                        LB_Material_Download.Visible = True
                        LB_Material_Download.Text = Conteudo_Material.Replace("/Anexos/", "")

                        RegistrarScript("$('#myModalExibicao').modal('show')")

                    Case "6"
                        'Abrir modal para mostrar a imagem
                        Dim SB As New StringBuilder
                        SB.Append("<img src='.." + Conteudo_Material + "' width='868px'  />")

                        LIT_Video.Text = SB.ToString
                        LB_Download.Visible = False
                        LB_Material_Download.Visible = False
                        RegistrarScript("$('#myModalExibicao').modal('show')")

                        Me.ViewState("Material_Selecionado") = Cod_Material
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

    'Private Sub Cancelar_Material_Click(sender As Object, e As EventArgs) Handles Cancelar_Material.Click
    '    'D_Alerta.Visible = False
    '    'Limpa_Dados_Modal_Material()
    '    'Link.Visible = False
    '    'Arquivo.Visible = False
    'End Sub
#End Region
End Class
