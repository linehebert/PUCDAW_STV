<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cad_Material.aspx.vb" Inherits="Cadastros_Cad_Material" %>

<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid">
        <h1>Cadastro de Materiais</h1>
        <hr />

        <div class="form-group">
            <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                <asp:label id="L_Erro" runat="server" text=""></asp:label>
            </div>
            <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                <asp:label id="L_Aviso" runat="server" text=""></asp:label>
            </div>
            <div class="col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon glyphicon-info-sign" aria-hidden="true" style="margin-right: 20px;"></span>
                        Identificação
                    </div>
                    <fieldset id="Video" runat="server">

                        <div class="row rr">
                            <div class="col-md-12">
                                <video width="400" controls>
                                    <source src="mov_bbb2.mp4" type="video/mp4">
                                    <%--<source src="mov_bbb.ogg" type="video/ogg">--%>
                                    Your browser does not support HTML5 video.
                                </video>
                                <p>
                                    Video courtesy of
                                    <a href="http://www.bigbuckbunny.org/" target="_blank">Big Buck Bunny</a>.
                                </p>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
