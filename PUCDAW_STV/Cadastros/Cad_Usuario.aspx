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
                            </div>
                        </div>
                    </div>
                    <div class="row rr">
                        <div class="col-md-6 col-md-offset-3">
                            <div id="Campo_Departamento" runat="server">
                                <asp:Label ID="L_Departamento" runat="server" Text="Departamento:"></asp:Label>
                                <asp:DropDownList ID="DDL_Departamento" class="form-control" runat="server" DataValueField="Cod_Departamento" DataTextField="Descricao">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row rr">
                        <div class="col-md-6 col-md-offset-3">
                            <div id="Campo_Email" runat="server">
                                <asp:Label ID="L_email" runat="server" Text="E-mail:"></asp:Label>
                                <asp:TextBox ID="TB_Email" runat="server" class="form-control" ToolTip="E-mail"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row rr">
                        <div class="col-md-3 col-md-offset-3">
                            <div id="Campo_Senha" runat="server">
                                <asp:Label ID="L_Senha" runat="server" Text="Senha:"></asp:Label>
                                <asp:TextBox ID="TB_Senha" runat="server" class="form-control" ToolTip="Senha de Acesso" TextMode="Password" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div id="Campo_Confirma_Senha" runat="server">
                                <asp:Label ID="L_Confirma_Senha" runat="server" Text="Confirme a Senha:"></asp:Label>
                                <asp:TextBox ID="TB_Confirma_Senha" runat="server" class="form-control" ToolTip="Confirmar Senha" TextMode="Password" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row rr">
                        <div class="col-md-6 col-md-offset-3">
                            <div id="Campo_Inativo" runat="server">
                                <div class="checkbox-inline">
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
                        <asp:Button ID="B_Salvar" Text="SALVAR" runat="server" class="btn btn-primary" ToolTip="Salvar Registro" />
                        <asp:Button ID="B_Continuar" Text="CONTINUAR" runat="server" class="btn btn-primary pull-right" ToolTip="Validar CPF" />
                    </div>
                </div>
            </fieldset>
        </div>

    </div>
</asp:Content>

