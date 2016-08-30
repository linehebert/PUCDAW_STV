<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Dados_Acesso.aspx.vb" Inherits="Dados_Acesso" %>

<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">
    <div class="grid">
        <h1>Cadastro de Usuários</h1>
        <hr />

        <div class="col-xs-12">
            <div class="form-group">
                <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                    <asp:Label ID="L_Erro" runat="server" Text=""></asp:Label>
                </div>
                <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                    <asp:Label ID="L_Aviso" runat="server" Text=""></asp:Label>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon glyphicon-info-sign" aria-hidden="true" style="margin-right: 20px;"></span>
                        Dados de Acesso
                    </div>
                    <fieldset class="rr" runat="server" id="F_CPF">
                        <div class="row rr">
                            <div class="col-md-4 col-md-offset-4">
                                <asp:Label ID="Label1" runat="server" Text="CPF:"></asp:Label>
                                <div id="Campo_CPF" runat="server" class="form-inline">
                                    <asp:TextBox ID="TB_CPF" runat="server" class="form-control" ToolTip="CPF do Usuário Logado" ReadOnly="true"></asp:TextBox>
                                    <asp:Image ID="Ok" ImageUrl="~/Images/ok.png" runat="server" Visible="True" />
                                </div>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-4 col-md-offset-4">
                                <asp:Label ID="L_Senha_Atual" runat="server" Text="Senha Atual:"></asp:Label>
                                <asp:TextBox ID="TB_Senha_Atual" runat="server" class="form-control" ToolTip="Senha de Acesso Atual" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RF_TB_Senha_Atual" runat="server" ControlToValidate="TB_Senha_Atual"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                    class="validation">* Informe a senha de acesso atual</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <br />
                        <div class="row rr">
                            <div class="col-md-4 col-md-offset-4">
                                <div id="Campo_Senha" runat="server">
                                    <asp:Label ID="L_Senha" runat="server" Text="Senha:"></asp:Label>
                                    <asp:TextBox ID="TB_Senha" runat="server" class="form-control" ToolTip="Senha de Acesso" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RF_TB_Senha" runat="server" ControlToValidate="TB_Senha"
                                        Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                        class="validation">* Informe nova senha para acesso</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-4 col-md-offset-4">
                                <div id="Campo_Confirma_Senha" runat="server">
                                    <asp:Label ID="L_Confirma_Senha" runat="server" Text="Confirme a Senha:"></asp:Label>
                                    <asp:TextBox ID="TB_Confirma_Senha" runat="server" class="form-control" ToolTip="Confirmar Senha" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RF_TB_Confirma_Senha" runat="server" ControlToValidate="TB_Confirma_Senha"
                                        Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                        class="validation">* Confirme a nova senha informada</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
        <fieldset class="rr">
            <div class="row">
                <div class="col-md-2">
                    <asp:Button ID="B_Voltar" Text="VOLTAR" runat="server" class="btn btn-default pull-left" ToolTip="Voltar" />
                </div>
                <div class="col-md-4  col-md-offset-6">
                    <asp:Button ID="B_Salvar" Text="SALVAR" runat="server" class="btn btn-primary pull-right" ToolTip="Salvar Registro" CausesValidation="true" ValidationGroup="A" />
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>



