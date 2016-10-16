Imports STV.Entidades
Imports STV.Seguranca
Partial Class Cadastros_Cad_Usuario : Inherits STV.Base.Page

    Dim _Usuario As Usuario
    Private ReadOnly Property Usuario As Usuario
        Get
            If IsNothing(_Usuario) Then _
                _Usuario = New Usuario

            Return _Usuario
        End Get
    End Property

    Private ReadOnly Property Cod_Usuario As String
        Get
            Return Criptografia.Decryptdata(Request("UserCad"))
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                B_Salvar.Attributes.Remove("disabled")
                Preenche_DDL_Departamento()
                If Cod_Usuario <> "" Then
                    'Alteração de Cadastro
                    Monta_Dados()
                    TB_CPF.ReadOnly = True
                    Ok.Visible = True
                    B_Continuar.Visible = False
                    Complemento.Visible = True
                    cad_usuario.InnerText = "Alteração de Cadastro"
                    reset_senha.Visible = True

                ElseIf Cod_Usuario = "" Then
                    'Novo Cadastro
                    Complemento.Visible = False
                    B_Cancelar.Visible = False
                    B_Salvar.Visible = False
                    cad_usuario.InnerText = "Novo Usuário"
                    reset_senha.Visible = False
                End If
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

#Region "Funções e Rotinas"

    Private Sub Monta_Dados(Optional Dado As Usuario.Dados = Nothing)
        Try
            If Dado Is Nothing Then Dado = Usuario.Carrega_Usuario(Cod_Usuario)

            TB_CPF.Text = Dado.CPF
            TB_Nome.Text = Dado.Nome
            DDL_Departamento.SelectedValue = Dado.Cod_Departamento
            TB_Email.Text = Dado.Email
            CB_Inativos.Checked = Dado.Usuario_Inativo
            If Dado.ADM = True Then RBL_Tipo_Usuario.SelectedValue = "1"
            If Dado.ADM = False Then RBL_Tipo_Usuario.SelectedValue = "0"
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub Voltar()
        Response.Redirect("../Consultas/Con_Usuario.aspx")
    End Sub

#End Region

#Region "Botões e Eventos"
    Protected Sub B_Continuar_Click(sender As Object, e As EventArgs) Handles B_Continuar.Click
        Try
            If TB_CPF.Text = "" Then
                D_Erro.Visible = True
                L_Erro.Text = "Informe o CPF do novo usuário!"
                Campo_CPF.Attributes.Add("class", "form-inline has-error")
                TB_CPF.Focus()
            Else
                Dim CPF As String = TB_CPF.Text
                If Usuario.Validar_Cpf(CPF) Then
                    If Usuario.Existe_CPF(CPF) Then
                        D_Erro.Visible = True
                        L_Erro.Text = "CPF já cadastrado!"
                        TB_CPF.Text = ""
                        Campo_CPF.Attributes.Add("class", "form-inline has-error")
                        TB_CPF.Focus()
                    Else
                        B_Cancelar.Visible = True
                        Complemento.Visible = True
                        B_Continuar.Visible = False
                        Ok.Visible = True
                        TB_CPF.ReadOnly = True
                        D_Erro.Visible = False
                        L_Erro.Text = ""
                        B_Cancelar.Visible = True
                        B_Salvar.Visible = True
                        Campo_CPF.Attributes.Add("class", "form-inline")
                    End If
                Else
                    D_Erro.Visible = True
                    L_Erro.Text = "CPF inválido!"
                    TB_CPF.Text = ""
                    TB_CPF.Focus()
                    Campo_CPF.Attributes.Add("class", "form-inline has-error")
                End If
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub
    Protected Sub B_Cancelar_Click(sender As Object, e As EventArgs) Handles B_Cancelar.Click
        Response.Redirect("../Default.aspx")
    End Sub

    Protected Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click
        Try
            If TB_Nome.Text <> "" And TB_Email.Text <> "" Then
                If Cod_Usuario <> Nothing Then
                    If Usuario.Verifica_Responsabilidade(Cod_Usuario) = True And CB_Inativos.Checked Then
                        D_Erro.Visible = True
                        L_Erro.Text = "Este usuário tem cursos sob sua responsabilidade, não é possível inativá-lo!"
                        Monta_Dados()
                    Else
                        Dim Dados As New Usuario.Dados

                        Dados.Cod_Usuario = Cod_Usuario
                        Dados.CPF = TB_CPF.Text
                        Dados.Nome = TB_Nome.Text.ToUpper()
                        Dados.Cod_Departamento = DDL_Departamento.SelectedValue
                        Dados.Email = TB_Email.Text
                        Dados.Usuario_Inativo = CB_Inativos.Checked

                        'Verifica se tipo adm foi selecionado
                        If RBL_Tipo_Usuario.SelectedValue = 1 Then
                            'Verifica se está inscrito em cursos
                            Dim Aluno As String = Biblio.Pega_Valor("SELECT Cod_Curso FROM CURSOxUSUARIO WHERE Cod_Usuario=" + Cod_Usuario, "Cod_Curso")
                            If Aluno <> "" Then
                                D_Erro.Visible = True
                                L_Erro.Text = "Não é possível dar permissão de administrador para alunos."
                            Else
                                Dados.ADM = RBL_Tipo_Usuario.SelectedValue

                                Usuario.Alterar(Dados)

                                D_Erro.Visible = False
                                D_Aviso.Visible = True
                                L_Aviso.Text = "Registro atualizado com sucesso!"
                                B_Cancelar.Visible = False
                                Monta_Dados(Dados)
                            End If
                        Else
                            Dados.ADM = RBL_Tipo_Usuario.SelectedValue

                            Usuario.Alterar(Dados)

                            D_Erro.Visible = False
                            D_Aviso.Visible = True
                            L_Aviso.Text = "Registro atualizado com sucesso!"
                            B_Cancelar.Visible = False
                            Monta_Dados(Dados)
                        End If

                    End If
                Else
                    Dim Dados As New Usuario.Dados

                    Dados.CPF = TB_CPF.Text
                    Dados.Nome = TB_Nome.Text.ToUpper()
                    Dados.Cod_Departamento = DDL_Departamento.SelectedValue
                    Dados.Email = TB_Email.Text
                    Dados.Senha = Criptografia.Encryptdata("123")
                    Dados.Usuario_Inativo = CB_Inativos.Checked
                    Dados.ADM = RBL_Tipo_Usuario.SelectedValue

                    Dados.Cod_Usuario = Usuario.Inserir(Dados)

                    D_Erro.Visible = False
                    D_Aviso.Visible = True
                    L_Aviso.Text = "Cadastro realizado com sucesso!"

                    B_Cancelar.Visible = False
                    Monta_Dados(Dados)
                    B_Salvar.Attributes.Add("disabled", "disabled")
                End If
            Else
                Monta_Dados()
            End If
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Private Sub B_Resetar_Click(sender As Object, e As EventArgs) Handles B_Resetar.Click
        Try
            Dim Dados As New Usuario.Dados

            Dados.Senha = Criptografia.Encryptdata("123")
            Dados.Cod_Usuario = Cod_Usuario

            Usuario.Alterar_Senha(Dados)

            D_Erro.Visible = False
            D_Aviso.Visible = True
            L_Aviso.Text = "Senha padrão '123' definida para acesso desse usuário!"

            B_Cancelar.Visible = False
            Monta_Dados()
        Catch ex As Exception
            L_Erro.Text = ex.Message
            D_Erro.Visible = True
        End Try
    End Sub

    Protected Sub B_Voltar_Click(sender As Object, e As EventArgs) Handles B_Voltar.Click
        Voltar()
    End Sub

#End Region

#Region "Preenche DDL's"
    Protected Sub Preenche_DDL_Departamento()
        Dim departamento As New Departamento
        DDL_Departamento.DataSource = departamento.Carrega_Departamentos("", False)
        DDL_Departamento.DataBind()

        Dim item As New ListItem
        item.Text = "SELECIONE UM DEPARTAMENTO"
        item.Value = 0
        DDL_Departamento.Items.Insert(0, item)
    End Sub

#End Region
End Class
