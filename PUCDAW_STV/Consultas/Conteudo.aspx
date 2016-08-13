<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Conteudo.aspx.vb" Inherits="Consultas_Conteudo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>

<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid">
        <h1>Visualizar Unidade</h1>
        <hr />
        <br />
        <div class="form-group">
            <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Erro" runat="server" SkinID="Skin_label_error" Text=""></asp:Label>
            </div>
            <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Aviso" runat="server" Text=""></asp:Label>
            </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon glyphicon-info-sign" aria-hidden="true" style="margin-right: 20px;"></span>
                    Curso
                </div>
                <div class="row rr">
                    <div class="col-md-10 col-md-offset-1" style="text-align: center">
                        <asp:Label ID="L_Curso_Unidade" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="UP_Unidades" runat="server">
                <ContentTemplate>
                    <asp:Repeater ID="rptUnidades" runat="server">
                        <ItemTemplate>
                            <div class="panel panel-default">
                                <div class="panel-heading expandir" id="Atividades">
                                    <span class="glyphicon glyphicon glyphicon-th-list" aria-hidden="true" style="margin-right: 20px;"></span>
                                    <%# Container.DataItem("Titulo").ToString.ToUpper %>
                                    <span class="pull-right">
                                        <a data-toggle="collapse" data-parent="#panel-quote-group" href="#AtividadesPanel">
                                            <span class="toggle-icon glyphicon glyphicon-minus"></span>
                                        </a>
                                    </span>
                                </div>
                                <asp:UpdatePanel ID="UP_Unidades" runat="server">
                                    <ContentTemplate>
                                        <asp:Repeater ID="rptAtividades" runat="server">
                                            <HeaderTemplate>
                                                <div class="row">
                                                    <div class="col-md-8 col-md-offset-2 form-inline">
                                                        <br />
                                                        <div class="pull-right">
                                                            <asp:Label ID="L_Title" runat="server" Text="ATIVIDADES"></asp:Label>
                                                            <asp:Image ImageUrl="~/Images/atividades.png" runat="server" ToolTip="Editar Cadastro"/>
                                                        </div><br />
                                                        <hr />
                                                    </div>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="AtividadesPanel" class="panel-collapse collapse in">
                                                    <div class="row rr form-inline">
                                                        <div class="col-md-8 col-md-offset-2">
                                                            <a href="../Cadastros/Atividade.aspx?Atv=<%# Container.DataItem("Cod_Atividade") %>">
                                                                <%# Container.DataItem("Titulo").ToString.ToUpper %>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="row rr" style="text-align: center">
                                                    <asp:Label ID="L_Empty" runat="server" Text="Nenhuma atividade disponível para esta unidade" Visible="false"></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script src="../js/jsstyle.js"></script>
</asp:Content>
