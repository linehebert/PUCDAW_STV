<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>


<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="conteudo rr">

        <div class="container droppedHover">
            <div class="form-signin" role="form" runat="server">
                <center>INSIRA SEUS DADOS DE ACESSO</center><br />
                <br />
                <div id="Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                    <asp:Label ID="L_Erro" runat="server" SkinID="Skin_label_error" Text=""></asp:Label>
                </div>

                <asp:Label ID="L_CPF" runat="server" Text="CPF:"></asp:Label>
                <asp:TextBox ID="TB_CPF" runat="server" class="form-control" ToolTip="Informe o CPF para acesso ao sistema"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFV_TTB_CPF" runat="server" ControlToValidate="TB_CPF"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                    class="validation">* Informe o CPF</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="TB_CPF" class="validation" Text="* CPF incorreto" ValidationGroup="A"
                                     ValidationExpression="^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$"  runat="server"></asp:RegularExpressionValidator>
                <br />
                <asp:Label ID="L_senha" runat="server" Text="Senha:"></asp:Label>
                <asp:TextBox ID="TB_Senha" runat="server" class="form-control" TextMode="Password" ToolTip="Informe o CPF para acesso ao sistema"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF_TB_Senha" runat="server" ControlToValidate="TB_Senha"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                    class="validation">* Informe a senha</asp:RequiredFieldValidator>

                <asp:Button ID="B_Login" runat="server" class="btn btn-lg btn-primary btn-block" Text="Entrar" SkinID="Skin_Button_Tabela"
                    CausesValidation="true" ValidationGroup="A"/>
            </div>
        </div>

    </div>
</asp:Content>

