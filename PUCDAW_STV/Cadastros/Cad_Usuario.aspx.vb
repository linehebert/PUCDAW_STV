Imports STV
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

    Private ReadOnly Property Cod_Usuario As Integer
        Get
            Return Request("Codigo")
        End Get
    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Preenche_DDL_Departamento()
            If Request("Codigo") <> "" Then
                F_CPF.Disabled = True
                Ok.Visible = True
                B_Continuar.Visible = False

                Complemento.Visible = True
                Monta_Dados()
            ElseIf Request("Codigo") = "" Then
                Complemento.Visible = False
                B_Cancelar.Visible = False
                B_Salvar.Visible = False
            End If

        End If
    End Sub

    Private Sub Monta_Dados()
        Dim Dado = Usuario.Carrega_Usuario(Cod_Usuario)

        TB_CPF.Text = Dado.CPF
        TB_Nome.Text = Dado.Nome
        DDL_Departamento.SelectedValue = Dado.Cod_Departamento
        TB_Email.Text = Dado.Email
        CB_Inativos.Checked = Dado.Usuario_Inativo

    End Sub

    Protected Sub B_Continuar_Click(sender As Object, e As EventArgs) Handles B_Continuar.Click
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
                    Campo_CPF.Attributes.Add("class", "form-inline has-error")
                    TB_CPF.Focus()

                Else
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
                TB_CPF.Focus()
                Campo_CPF.Attributes.Add("class", "form-inline has-error")
            End If
        End If


    End Sub
    Protected Sub B_Cancelar_Click(sender As Object, e As EventArgs) Handles B_Cancelar.Click
        Response.Redirect("../Default.aspx")
    End Sub

    Protected Sub B_Voltar_Click(sender As Object, e As EventArgs) Handles B_Voltar.Click
        Voltar()
    End Sub
    Protected Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click
        If TB_Nome.Text <> "" And TB_Email.Text <> "" And TB_Senha.Text <> "" And TB_Confirma_Senha.Text <> "" Then
            If Cod_Usuario Then
                If Usuario.Verifica_Responsabilidade(Cod_Usuario) = True And CB_Inativos.Checked Then
                    D_Erro.Visible = True
                    L_Erro.Text = "Este usuário tem cursos sob sua responsabilidade, não é possível inativá-lo!"
                    Campo_Inativo.Attributes.Add("class", "has-error")
                    Monta_Dados()
                Else
                    Dim Dados As New Usuario.Dados

                    Dados.Cod_Usuario = Cod_Usuario
                    Dados.CPF = TB_CPF.Text
                    Dados.Nome = TB_Nome.Text.ToUpper()
                    Dados.Cod_Departamento = DDL_Departamento.SelectedValue
                    Dados.Email = TB_Email.Text
                    Dados.Senha = Criptografia.Encryptdata(TB_Senha.Text)
                    Dados.Usuario_Inativo = CB_Inativos.Checked

                    Usuario.Alterar(Dados)

                    D_Erro.Visible = False
                    Campo_Inativo.Attributes.Add("class", "")
                    D_Aviso.Visible = True
                    L_Aviso.Text = "Registro atualizado com sucesso!"
                End If
            Else
                Dim Dados As New Usuario.Dados

                Dados.CPF = TB_CPF.Text
                Dados.Nome = TB_Nome.Text.ToUpper()
                Dados.Cod_Departamento = DDL_Departamento.SelectedValue
                Dados.Email = TB_Email.Text
                Dados.Senha = Criptografia.Encryptdata(TB_Senha.Text)
                Dados.Usuario_Inativo = CB_Inativos.Checked

                Usuario.Inserir(Dados)

                D_Erro.Visible = False
                D_Aviso.Visible = True
                L_Aviso.Text = "Cadastro realizado com sucesso!"
                Campo_Nome.Attributes.Add("class", "")
                Campo_Email.Attributes.Add("class", "")
                Campo_Senha.Attributes.Add("class", "")
                Campo_Confirma_Senha.Attributes.Add("class", "")
                Campo_Departamento.Attributes.Add("class", "")
                Monta_Dados()

            End If
        Else
            D_Erro.Visible = True
            L_Erro.Text = "Preencha todos os campos obrigatórios (*) "
            Campo_Nome.Attributes.Add("class", "has-error")
            Campo_Email.Attributes.Add("class", "has-error")
            Campo_Senha.Attributes.Add("class", "has-error")
            Campo_Confirma_Senha.Attributes.Add("class", "has-error")
            Campo_Departamento.Attributes.Add("class", "has-error")
            Monta_Dados()
        End If
    End Sub

    Protected Sub Voltar()
        Response.Redirect("../Consultas/Con_Usuario.aspx")
    End Sub

    Protected Sub Preenche_DDL_Departamento()
        Dim departamento As New Departamento
        DDL_Departamento.DataSource = departamento.Carrega_Departamentos("", False)
        DDL_Departamento.DataBind()

        Dim item As New ListItem
        item.Text = "SELECIONE UM DEPARTAMENTO"
        item.Value = 0
        DDL_Departamento.Items.Insert(0, item)
    End Sub
End Class
