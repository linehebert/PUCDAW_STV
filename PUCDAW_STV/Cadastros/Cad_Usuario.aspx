<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cad_Usuario.aspx.vb" Inherits="Cadastros_Cad_Usuario" %>


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
                        Identificação
                    </div>

                    <fieldset class="rr" runat="server" id="F_CPF">
                        <div class="row rr">
                            <div class="col-md-6 col-md-offset-3">
                                <asp:Label ID="L_CPF" runat="server" Text="CPF:"></asp:Label>
                                <div id="Campo_CPF" runat="server" class="form-inline">
                                    <asp:TextBox ID="TB_CPF" runat="server" class="form-control" ToolTip="CPF do Usuário"></asp:TextBox>
                                    <asp:Image ID="Ok" ImageUrl="~/Images/ok.png" runat="server" Visible="false" />
                                    <asp:RequiredFieldValidator ID="RFV_TB_CPF" runat="server" ControlToValidate="TB_CPF"
                                        Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                        class="validation">* Informe o CPF do usuário</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="col-md-3  col-md-offset-9">
                </div>
            </div>

            <div class="panel panel-default" id="Complemento" runat="server">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon glyphicon-info-sign" aria-hidden="true" style="margin-right: 20px;"></span>
                    Dados Cadastrais
                </div>

                <fieldset runat="server" class="rr">
                    <div class="row rr">
                        <div class="col-md-6 col-md-offset-3">
                            <div id="Campo_Nome" runat="server">
                                <asp:Label ID="L_Nome" runat="server" Text="Nome Completo:"></asp:Label>
                                <asp:TextBox ID="TB_Nome" runat="server" class="form-control" ToolTip="Nome do Usuário"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RF_TB_Nome" runat="server" ControlToValidate="TB_Nome"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="B"
                                    class="validation">* Informe o nome completo do usuário</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row rr">
                        <div class="col-md-6 col-md-offset-3">
                            <div id="Campo_Departamento" runat="server">
                                <asp:Label ID="L_Departamento" runat="server" Text="Departamento:"></asp:Label>
                                <asp:DropDownList ID="DDL_Departamento" class="form-control" runat="server" DataValueField="Cod_Departamento" DataTextField="Descricao">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RF_DDL_Departamento" runat="server" ControlToValidate="DDL_Departamento"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="B"
                                    class="validation">* Informe o departamento deste usuário</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row rr">
                        <div class="col-md-6 col-md-offset-3">
                            <div id="Campo_Email" runat="server">
                                <asp:Label ID="L_email" runat="server" Text="E-mail:"></asp:Label>
                                <asp:TextBox ID="TB_Email" runat="server" class="form-control" ToolTip="E-mail"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RF_TB_Email" runat="server" ControlToValidate="TB_Email"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="B"
                                    class="validation">* Informe o e-mail deste usuário</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row rr">
                        <div class="col-md-3 col-md-offset-3">
                            <div id="Campo_Senha" runat="server">
                                <asp:Label ID="L_Senha" runat="server" Text="Senha:"></asp:Label>
                                <asp:TextBox ID="TB_Senha" runat="server" class="form-control" ToolTip="Senha de Acesso" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RF_TB_Senha" runat="server" ControlToValidate="TB_Senha"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="B"
                                    class="validation">* Informe uma senha para acesso deste usuário</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div id="Campo_Confirma_Senha" runat="server">
                                <asp:Label ID="L_Confirma_Senha" runat="server" Text="Confirme a Senha:"></asp:Label>
                                <asp:TextBox ID="TB_Confirma_Senha" runat="server" class="form-control" ToolTip="Confirmar Senha" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RF_TB_Confirma_Senha" runat="server" ControlToValidate="TB_Confirma_Senha"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="B"
                                    class="validation">* Confirme a senha informada</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row rr">
                        <div class="col-md-5 col-md-offset-3">
                            Tipo do Usuário:
                            </div>
                        </div>
                    <div class="row rr">
                        <div class="col-md-5 col-md-offset-3">
                            <div id="radio" runat="server">
                                <div>
                                    <asp:RadioButtonList ID="RBL_Tipo_Usuario" runat="server" RepeatDirection="Horizontal"
                                        CellPadding="5" CellSpacing="5" RepeatLayout="Flow" CssClass="radio-inline" ToolTip="Selecione o tipo de usuário">
                                        <asp:ListItem Value="0" Selected="True"> Comum &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="1"> Administrador </asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div id="Div1" runat="server">
                                <div class="checkbox-inline pull-right">
                                    <asp:CheckBox ID="CB_Inativos" runat="server" Text="Inativo" ToolTip="Status do Registro" />
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
            <fieldset class="rr">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Button ID="B_Voltar" Text="VOLTAR" runat="server" class="btn btn-default pull-left" ToolTip="Voltar" />
                    </div>
                    <div class="col-md-4  col-md-offset-6">
                        <asp:Button ID="B_Cancelar" Text="CANCELAR" runat="server" class="btn btn-danger" ToolTip="Cancelar" />
                        <asp:Button ID="B_Salvar" Text="SALVAR" runat="server" class="btn btn-primary" ToolTip="Salvar Registro" CausesValidation="true" ValidationGroup="B" />
                        <asp:Button ID="B_Continuar" Text="CONTINUAR" runat="server" class="btn btn-primary pull-right" ToolTip="Validar CPF" CausesValidation="true" ValidationGroup="A" />
                    </div>
                </div>
            </fieldset>
        </div>

    </div>
</asp:Content>

