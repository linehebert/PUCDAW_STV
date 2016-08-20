<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Tema.aspx.vb" Inherits="Tema" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server"></asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="conteudo">
        <div class="grid">
            <h1>Alteração de Tema/Logotipo</h1>
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
                        <div class="row rr">
                            <div class="col-md-6 col-md-offset-3">
                                <div class="row rr">
                                    <asp:Label ID="L_Tema" runat="server" Text="Tema:" ></asp:Label>
                                    <asp:DropDownList ID="DDL_Tema" runat="server" DataValueField="Cod_Tema" DataTextField="Cor" class="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-6 col-md-offset-3">
                                <div class="row rr">
                                    <asp:Label ID="Label2" runat="server" SkinID="Skin_label" Text="Logotipo:"></asp:Label>
                                    <asp:FileUpload ID="FU_Logo" runat="server" class="form-control" />
                                </div>
                                <br />
                                <div class="row rr">
                                    <asp:Label ID="L_Logo_Atualizar" runat="server" SkinID="Skin_label" Text="Logo Atual:"></asp:Label>
                                    <div class="pull-right">
                                        <asp:Label ID="Label3" runat="server" SkinID="Skin_label" Text="Excluir"></asp:Label>
                                        <asp:ImageButton ID="IB_Logo_Excluir" runat="server" ImageUrl="~/images/delete.png"
                                            Style="vertical-align: middle;" />
                                        <cc2:ConfirmButtonExtender ID="IB_Logo_Excluir_ConfirmButtonExtender" runat="server"
                                            TargetControlID="IB_Logo_Excluir" ConfirmText="Tem certeza que deseja excluir?" />
                                    </div>
                                </div>

                                <div class="row rr">
                                    <center> <asp:Image ID="I_Logo" runat="server" Width="100%" Height="100%" /> </center>
                                </div>

                            </div>
                        </div>

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

