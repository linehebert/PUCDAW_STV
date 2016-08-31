Imports System.Data
Imports STV.Entidades
Imports STV.Seguranca

Partial Class Cadastros_Cad_Curso : Inherits STV.Base.Page
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
    Dim _Curso As Curso
    Private ReadOnly Property Curso As Curso
        Get
            If IsNothing(_Curso) Then _
                _Curso = New Curso

            Return _Curso
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

    Dim _Categoria As Categoria
    Private ReadOnly Property Categoria As Categoria
        Get
            If IsNothing(_Categoria) Then _
                _Categoria = New Categoria

            Return _Categoria
        End Get
    End Property

    Dim _Departamento As Departamento
    Private ReadOnly Property Departamento As Departamento
        Get
            If IsNothing(_Departamento) Then _
                _Departamento = New Departamento

            Return _Departamento
        End Get
    End Property
    Private ReadOnly Property Cod_Curso As Integer
        Get
            Return Request("Cod")
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Preenche_DDL_Usuarios()
                Preenche_DDL_Categorias()

                If Cod_Curso <> 0 Then
                    Monta_Dados()
                    Carrega_Lista("", False)
                    Preenche_Listas()
                    UP_Visibilidade.Update()
                Else
                    Carrega_Lista("", False)
                    UP_Visibilidade.Update()
                End If
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub
#Region "Fuñções e Rotinas"
    Private Sub Monta_Dados()
        Try
            Dim Dado = Curso.Carrega_Curso(Cod_Curso)

            TB_Titulo.Text = Dado.Titulo
            TB_Dt_Inicio.Text = Dado.Dt_Inicio.ToString("yyyy-MM-dd")
            TB_Dt_Termino.Text = Dado.Dt_Termino.ToString("yyyy-MM-dd")
            TB_palavra_chave.Text = Dado.Palavras_Chave
            DDL_Usuario.SelectedValue = Dado.Instrutor
            DDL_Categoria.SelectedValue = Dado.Categoria
            CB_Inativos.Checked = Dado.Curso_Inativo
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub Voltar()
        Response.Redirect("../Consultas/Con_Curso.aspx")
    End Sub
#End Region

#Region "Botões e Eventos"

    Protected Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click
        Try
            If LB_Incluidos.Items.Count > 0 Then
                If TB_Dt_Termino.Text >= Today Then
                    If Cod_Curso <> 0 Then
                        'Alterar Informações do Curso
                        Dim Dados As New Curso.Dados

                        Dados.Cod_Curso = Cod_Curso
                        Dados.Titulo = TB_Titulo.Text
                        Dados.Dt_Inicio = TB_Dt_Inicio.Text
                        Dados.Dt_Termino = TB_Dt_Termino.Text
                        Dados.Palavras_Chave = TB_palavra_chave.Text
                        Dados.Instrutor = DDL_Usuario.SelectedValue
                        Dados.Categoria = DDL_Categoria.SelectedValue
                        Dados.Curso_Inativo = CB_Inativos.Checked

                        Curso.Alterar(Dados)

                        'Excluir Visibilidades Anteriores
                        Curso.Delete(Cod_Curso)

                        'Insere novamente a visibilidade correta
                        Dim listaSelecionados As New ListItemCollection
                        For Each item As ListItem In LB_Incluidos.Items
                            listaSelecionados.Add(item)
                        Next

                        For Each item As ListItem In listaSelecionados
                            Curso.Inserir_Visibilidade(item.Value, Cod_Curso)
                        Next
                        UP_Visibilidade.Update()
                        Voltar()

                    Else
                        'INserir informações do curso
                        Dim Dados As New Curso.Dados

                        Dados.Titulo = TB_Titulo.Text.ToUpper()
                        Dados.Dt_Inicio = TB_Dt_Inicio.Text
                        Dados.Dt_Termino = TB_Dt_Termino.Text
                        Dados.Palavras_Chave = TB_palavra_chave.Text
                        Dados.Instrutor = DDL_Usuario.SelectedValue
                        Dados.Categoria = DDL_Categoria.SelectedValue
                        Dados.Curso_Inativo = CB_Inativos.Checked

                        Dim codigo_curso As Integer = Curso.Inserir(Dados)

                        'Inserir visibilidade
                        Dim listaSelecionados As New ListItemCollection
                        For Each item As ListItem In LB_Incluidos.Items
                            listaSelecionados.Add(item)
                        Next

                        For Each item As ListItem In listaSelecionados
                            Curso.Inserir_Visibilidade(item.Value, codigo_curso)
                        Next
                        UP_Visibilidade.Update()
                        Voltar()
                    End If
                Else
                    L_Info.Text = "A data de término do curso não pode ser inferior a data atual, informe uma nova data de término."
                    RegistrarScript("$('#myModalInfo').modal('show')")
                End If
            Else
                L_Info.Text = "É preciso definir a visibilidade deste curso para concluir, selecione quais os departamentos terão acesso a este curso."
                RegistrarScript("$('#myModalInfo').modal('show')")
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Continuar_Click(sender As Object, e As EventArgs) Handles B_Continuar.Click
        RegistrarScript("$('#myModalInfo').modal('hide')")
    End Sub
    Protected Sub B_Cancelar_Click(sender As Object, e As System.EventArgs) Handles B_Cancelar.Click
        Voltar()
    End Sub

#End Region

#Region "Definir Visibilidade"
    Protected Sub Carrega_Lista(Descricao As String, Inativo As Boolean)
        Try
            Dim DT_LIsta As DataTable = Departamento.Carrega_Departamentos("", False)
            Dim item As ListItem

            LB_NIncluidos.Items.Clear()
            For Each linha As DataRow In DT_LIsta.Rows
                item = New ListItem
                item.Value = linha("Cod_Departamento")
                item.Text = linha("Descricao")
                LB_NIncluidos.Items.Add(item)
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub B_Incluir_Todos_Click(sender As Object, e As System.EventArgs) Handles B_Incluir_Todos.Click
        Try
            'Percorre os selecionados da primeira lista
            Dim listaSelecionados As New ListItemCollection
            For Each item As ListItem In LB_NIncluidos.Items
                listaSelecionados.Add(item)
            Next

            'Adiciona na segunda lista
            For Each item As ListItem In listaSelecionados
                LB_Incluidos.Items.Add(item)
                LB_NIncluidos.Items.Remove(item)
            Next
            UP_Visibilidade.Update()
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_Incluir_Click(sender As Object, e As System.EventArgs) Handles B_Incluir.Click
        Try
            'Percorre os selecionados da primeira lista
            Dim listaSelecionados As New ListItemCollection
            For Each item As ListItem In LB_NIncluidos.Items
                If item.Selected Then listaSelecionados.Add(item)
            Next
            'Adiciona na segunda lista
            For Each item As ListItem In listaSelecionados
                LB_Incluidos.Items.Add(item)
                LB_NIncluidos.Items.Remove(item)
                LB_Incluidos.SelectedItem.Selected = False
            Next
            UP_Visibilidade.Update()
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_NIncluir_Todos_Click(sender As Object, e As System.EventArgs) Handles B_NIncluir_Todos.Click
        Try
            'Percorre os selecionados da primeira lista
            Dim listaSelecionados As New ListItemCollection
            For Each item As ListItem In LB_Incluidos.Items
                listaSelecionados.Add(item)
            Next
            'Adiciona na segunda lista
            For Each item As ListItem In listaSelecionados
                If Curso.Verifica_Inscritos(Cod_Curso, item.Value) Then
                    RegistrarScript("alert('Já existe aluno deste departamento inscrito neste curso. " + item.Text + "');")
                    Exit For
                Else
                    LB_NIncluidos.Items.Add(item)
                    LB_Incluidos.Items.Remove(item)
                End If
            Next
            UP_Visibilidade.Update()
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_NIncluir_Click(sender As Object, e As System.EventArgs) Handles B_NIncluir.Click
        Try
            'Percorre os selecionados da primeira lista
            Dim listaSelecionados As New ListItemCollection
            For Each item As ListItem In LB_Incluidos.Items
                If item.Selected Then
                    If Curso.Verifica_Inscritos(Cod_Curso, item.Value) Then
                        RegistrarScript("alert('Já existe aluno deste departamento inscrito neste curso. " + item.Text + "');")
                        Exit For
                    Else
                        listaSelecionados.Add(item)
                    End If
                End If
            Next
            'Adiciona na segunda lista
            For Each item As ListItem In listaSelecionados
                LB_NIncluidos.Items.Add(item)
                LB_Incluidos.Items.Remove(item)
                LB_NIncluidos.SelectedItem.Selected = False
            Next
            UP_Visibilidade.Update()
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

#End Region
#Region "Carregar Campos/DDLs"
    Protected Sub Preenche_Listas()
        Try
            Dim DT_LIsta_Visibilidade As DataTable = Curso.Carrega_Visibilidade(Cod_Curso)

            Dim item As ListItem

            LB_Incluidos.Items.Clear()
            For Each linha As DataRow In DT_LIsta_Visibilidade.Rows
                item = New ListItem
                item.Value = linha("Cod_Departamento")
                item.Text = linha("Descricao")
                LB_Incluidos.Items.Add(item)
                LB_NIncluidos.Items.Remove(item)
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub Preenche_DDL_Usuarios()
        Try
            Dim usuario As New Usuario
            DDL_Usuario.DataSource = usuario.Carrega_Usuarios("", False, 0, Usuario_Logado.Cod_Usuario, False)
            DDL_Usuario.DataBind()

            Dim item As New ListItem
            item.Text = "Selecione o instrutor"
            item.Value = 0
            DDL_Usuario.Items.Insert(0, item)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub Preenche_DDL_Categorias()
        Try
            Dim categoria As New Categoria
            DDL_Categoria.DataSource = categoria.Carrega_Categorias("", False)
            DDL_Categoria.DataBind()

            Dim item As New ListItem
            item.Text = "Selecione a categoria"
            item.Value = 0
            DDL_Categoria.Items.Insert(0, item)
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region
End Class
