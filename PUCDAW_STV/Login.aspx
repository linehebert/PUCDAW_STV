<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>


<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="conteudo">

            <div class="container droppedHover">
                <div class="form-signin" role="form" runat="server">
                    INSIRA SEUS DADOS DE ACESSO<br /><br />
                    <div id="Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                        <asp:Label ID="L_Erro" runat="server" SkinID="Skin_label_error" Text=""></asp:Label>
                    </div>
                    <label>CPF:</label>
                    <input name="TB_CPF" type="text" class="form-control" placeholder="000.000.000-00" required="" contenteditable="false">
                    <label>Senha de acesso:</label>
                    <input name="TB_Senha" type="password" class="form-control" placeholder="Senha" required="" contenteditable="false">
                    
                    <div class="checkbox">
                        <label class="checkbox">
                        <input type="checkbox" value="remember-me">Remember me
                    </label>
                    </div>
                    <asp:Button ID="B_Login" runat="server" class="btn btn-lg btn-primary btn-block" Text="Sign in" SkinID="Skin_Button_Tabela"
                        CausesValidation="true" />
                </div>
            </div>
        
    </div>
</asp:Content>

