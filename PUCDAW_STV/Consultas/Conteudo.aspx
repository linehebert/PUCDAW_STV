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
                    <div class="col-md-10 col-md-offset-1 table-responsive" style="text-align: center">
                        <asp:Label CssClass="Titulo_Curso" ID="L_Curso_Unidade" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="panel panel-default" id="Nenhuma_Unidade" visible="false" runat="server">
                <div class="row rr">
                    <div class="col-md-10 col-md-offset-1" style="text-align: center; margin-top: 30px; margin-bottom: 30px;">
                        <asp:Label ID="Label1" runat="server" Text="Nenhuma unidade foi disponibilidade para este curso no momento."></asp:Label>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="UP_Unidades" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Repeater ID="rptUnidades" runat="server">
                        <ItemTemplate>
                            <div class="panel panel-default">
                                <div class="panel-heading expandir" id="Atividades">
                                    <span class="glyphicon glyphicon glyphicon-th-list" aria-hidden="true" style="margin-right: 20px;"></span>
                                    UNIDADE: <%# Container.DataItem("Titulo").ToString.ToUpper %>
                                    <span class="pull-right">
                                        <a data-toggle="collapse" data-parent="#panel-quote-group" href="#AtividadesPanel">
                                            <span class="toggle-icon glyphicon glyphicon-minus"></span>
                                        </a>
                                    </span>
                                </div>
                                <asp:UpdatePanel ID="UP_Materiais" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Repeater ID="rptMateriais" runat="server" OnItemCommand="rptMateriais_ItemCommand">
                                            <HeaderTemplate>
                                                <div class="row">
                                                    <div class="col-md-8 col-md-offset-2 form-inline">
                                                        <br />
                                                        <div class="pull-right">
                                                            <asp:Label ID="L_Title" runat="server" Text="MATERIAIS"></asp:Label>
                                                            <asp:Image ImageUrl="~/Images/materiais.png" runat="server" />
                                                        </div>
                                                        <br />
                                                        <hr />
                                                    </div>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="MateriaisPanel" class="panel-collapse collapse in">
                                                    <div class="row rr form-inline">
                                                        <div class="col-md-8 col-md-offset-2">
                                                            <asp:LinkButton ID="lnk_Material" runat="server" CommandArgument='<%# Container.DataItem("Cod_Tipo").ToString() + "," + Container.DataItem("Cod_Material").ToString() %>' CommandName="ExibirMaterial">
                                                        <%# Container.DataItem("Titulo").ToString.ToUpper %>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="row rr" style="text-align: center">
                                                    <asp:Label ID="L_Emptym" runat="server" Text="Nenhum material disponível para esta unidade" Visible="false"></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UP_Atividades" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Repeater ID="rptAtividades" runat="server">
                                            <HeaderTemplate>
                                                <div class="row">
                                                    <div class="col-md-8 col-md-offset-2 form-inline">
                                                        <br />
                                                        <div class="pull-right">
                                                            <asp:Label ID="L_Title" runat="server" Text="ATIVIDADES"></asp:Label>
                                                            <asp:Image ImageUrl="~/Images/atividades.png" runat="server" />
                                                        </div>
                                                        <br />
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
                                </asp:UpdatePanel><br /><br />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>

            <!--Modal Exibição-->
            <div class="modal fade bs-example-modal-lg" id="myModalExibicao" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="L_Cabecalho">Conteúdo do Material</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Literal ID="LIT_Video" runat="server"></asp:Literal>
                                            <asp:Label Visible="false" ID="LB_Download" runat="server" Text='Este material contém um arquivo para download:'></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label Visible="false" ID="LB_Material_Download" runat="server" Text=''></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer">
                            <asp:Button ID="B_Fechar_Exibicao" Text="Fechar" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Fechar Exibição" />
                            <asp:Button ID="B_Download" Text="Download" runat="server" class="btn btn-primary" ToolTip="Baixar Arquivo" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="../js/jsstyle.js"></script>
</asp:Content>
