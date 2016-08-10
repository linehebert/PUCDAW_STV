Imports STV
Imports STV.Entidades
Partial Class Cadastros_Cad_Unidade : Inherits STV.Base.Page

    Dim _Unidade As Unidade
    Private ReadOnly Property Unidade As Unidade
        Get
            If IsNothing(_Unidade) Then _
                _Unidade = New Unidade

            Return _Unidade
        End Get
    End Property

    Private ReadOnly Property Cod_Curso As Integer
        Get
            Return Request("Cod")
        End Get
    End Property

    Private ReadOnly Property Cod_Unidade As Integer
        Get
            Return Request("Unit")
        End Get
    End Property
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            'If Request("Codigo") <> "" Then
            '    Monta_Dados()
            'Else
            '    TB_Codigo.Visible = False
            '    L_Codigo.Visible = False
            'End If

        End If
    End Sub

    Private Sub Monta_Dados()

        Dim Dado = Unidade.Carrega_Unidade(Cod_Unidade)

        TB_Titulo.Text = Dado.Titulo

    End Sub

    'Protected Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click

    '    If Request("Codigo") <> "" Then
    '        Dim Dados As New Departamento.Dados

    '        Dados.Cod_Departamento = TB_Codigo.Text
    '        Dados.Descricao = TB_Descr.Text.ToUpper()
    '        Dados.Departamento_Inativo = CB_Inativos.Checked

    '        Departamento.Alterar(Dados)
    '    Else

    '        Dim Dados As New Departamento.Dados

    '        Dados.Descricao = TB_Descr.Text.ToUpper()
    '        Dados.Departamento_Inativo = CB_Inativos.Checked

    '        Departamento.Inserir(Dados)
    '    End If

    '    Voltar()
    'End Sub

    Protected Sub B_Cancelar_Click(sender As Object, e As System.EventArgs) Handles B_Cancelar.Click
        Voltar()
    End Sub

    Protected Sub Voltar()
        Response.Redirect("../Consultas/Con_Unidade.aspx?Cod=" & Cod_Curso)
    End Sub

End Class