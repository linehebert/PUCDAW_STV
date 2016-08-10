<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cad_Unidade.aspx.vb" Inherits="Cadastros_Cad_Unidade" %>

<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server"></asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="conteudo">
        <div class="grid">
            <h1>Cadastro de Unidade</h1>
            <br />
            <hr />


            <div class="form-group">

                <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                    <asp:Label ID="L_Erro" runat="server" SkinID="Skin_label_error" Text=""></asp:Label>
                </div>
                <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                    <asp:Label ID="L_Aviso" runat="server" Text=""></asp:Label>
                </div>

                <div class="col-xs-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <span class="glyphicon glyphicon glyphicon-info-sign" aria-hidden="true" style="margin-right: 20px;"></span>
                            Informações Gerais
                        </div>

                        <fieldset runat="server">
                            <div class="row rr">
                                <div class="col-md-6 col-md-offset-3">
                                    <asp:Label ID="L_Titulo" runat="server" Text="Título:"></asp:Label>
                                    <asp:TextBox ID="TB_Titulo" runat="server" class="form-control" ToolTip="Título da Unidade"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row rr">
                                <div class="col-md-6 col-md-offset-3">
                                    <div class="checkbox-inline pull-right">
                                        <asp:CheckBox ID="CB_Inativos" runat="server" Text="Inativo" ToolTip="Status do Registro" />
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <fieldset class="rr">
                    <div class="row rr">
                        <div class="col-md-12  col-md-offset-9">
                            <asp:Button ID="B_Cancelar" Text="CANCELAR" runat="server" class="btn btn-danger" ToolTip="Cancelar" />
                            <asp:Button ID="B_Salvar" Text="SALVAR" runat="server" class="btn btn-primary" ToolTip="Salvar Registro" />
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>

</asp:Content>

