Imports STV
Imports STV.Entidades
Partial Class Cadastros_Cad_Departamento : Inherits STV.Base.Page

    Dim _Departamento As Departamento
    Private ReadOnly Property Departamento As Departamento
        Get
            If IsNothing(_Departamento) Then _
                _Departamento = New Departamento

            Return _Departamento
        End Get
    End Property

    Private ReadOnly Property Cod_Departamento As Integer
        Get
            Return Request("Codigo")
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If Request("Codigo") <> "" Then
                Monta_Dados()
            Else
                TB_Codigo.Visible = False
                L_Codigo.Visible = False
            End If

        End If
    End Sub

    Private Sub Monta_Dados()

        Dim Dado = Departamento.Carrega_Departamento(Cod_Departamento)

        TB_Codigo.Text = Dado.Cod_Departamento
        TB_Descr.Text = Dado.Descricao
        CB_Inativos.Checked = Dado.Departamento_Inativo

    End Sub

    Protected Sub B_Salvar_Click(sender As Object, e As EventArgs) Handles B_Salvar.Click

        If Request("Codigo") <> "" Then
            Dim Dados As New Departamento.Dados

            Dados.Cod_Departamento = TB_Codigo.Text
            Dados.Descricao = TB_Descr.Text.ToUpper()
            Dados.Departamento_Inativo = CB_Inativos.Checked

            Departamento.Alterar(Dados)
        Else

            Dim Dados As New Departamento.Dados

            Dados.Descricao = TB_Descr.Text.ToUpper()
            Dados.Departamento_Inativo = CB_Inativos.Checked

            Departamento.Inserir(Dados)
        End If

        Voltar()
    End Sub
    Protected Sub B_Cancelar_Click(sender As Object, e As System.EventArgs) Handles B_Cancelar.Click
        Voltar()
    End Sub
    Protected Sub Voltar()
        Response.Redirect("../Consultas/Con_Departamento.aspx")
    End Sub

End Class
