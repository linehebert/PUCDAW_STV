<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Con_Unidade.aspx.vb" Inherits="Consultas_Con_Unidade" %>



<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid">
        <h1>Consulta de Unidades</h1>
        <hr />

        <div class="form-group">
            <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Erro" runat="server" SkinID="Skin_label_error" Text=""></asp:Label>
            </div>
            <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Aviso" runat="server" Text=""></asp:Label>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon glyphicon-info-sign" aria-hidden="true" style="margin-right: 20px;"></span>
                    Informações Gerais
                </div>
                <div class="row rr">
                    <div class="col-md-10 col-md-offset-1 table-responsive" style="text-align: center">
                        <asp:Label ID="L_Titulo" runat="server" Text="Curso SS" CssClass="Titulo_Curso"></asp:Label>
                    </div>
                </div>
                <div class="row rr form-inline">
                    <div class="col-md-10 col-md-offset-1" style="text-align: center; margin-top: 15px">
                        <asp:Label ID="Dt_Inicio" runat="server" Text="Data de Início:"></asp:Label>
                        <asp:Label ID="L_Dt_Inicio" runat="server" Text="10/10/2010"></asp:Label>
                        &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Dt_Termino" runat="server" Text="Data de Término:"></asp:Label>
                        <asp:Label ID="L_Dt_Termino" runat="server" Text="10/10/2020"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon glyphicon-th-list" aria-hidden="true" style="margin-right: 20px;"></span>Unidades deste Curso
                </div>
                <asp:UpdatePanel ID="UP_Unidade" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div id="collapseExample">
                            <div class="row rr form-inline">
                                <div class="col-md-2 col-md-offset-10">
                                    <asp:ImageButton ID="B_Nova_Unidade" ImageUrl="~/Images/add.png" runat="server" ImageAlign="Right" ToolTip="Adicionar Nova Unidade" />
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row rr" id="Nenhuma_Unidade" visible="false" runat="server">
                                        <div class="col-md-10 col-md-offset-1" style="text-align: center; margin-top: 30px; margin-bottom: 30px;">
                                            <asp:Label ID="Label1" runat="server" Text="Não há unidades cadastradas para este curso."></asp:Label>
                                        </div>
                                    </div>
                                    <asp:Repeater ID="rptUnidades" runat="server">
                                        <ItemTemplate>
                                            <div class="row rr form-inline">
                                                <div class="col-md-10 col-md-offset-1" style="padding: 10px; margin-top: 15px;">
                                                    <asp:ImageButton ID="Editar" ImageUrl="~/Images/edit.png" OnCommand="Carrega_Modal_Alteracao" CommandArgument='<%# Container.DataItem("Cod_Unidade").ToString.ToUpper %>' runat="server" ToolTip="Renomear Unidade" />
                                                    <asp:ImageButton ID="Excuir_Unidade" ImageUrl="~/Images/delete.png" OnCommand="Carrega_Modal_Exclusao" CommandArgument='<%# Container.DataItem("Cod_Unidade").ToString.ToUpper %>' runat="server" ToolTip="Excluir Unidade" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <a href="../Consultas/Con_Conteudo_Unidade.aspx?Unit=<%# Container.DataItem("Cod_Unidade") %>" style="font-size:large">
                                                        <%# Container.DataItem("Titulo").ToString.ToUpper %>
                                                    </a>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <fieldset>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Button ID="B_Voltar" Text="VOLTAR" runat="server" class="btn btn-default" ToolTip="Voltar" />
                    </div>
                </div>
            </fieldset>

            <!--Modal de Inclusão-->
            <div class="modal fade" id="myModalI" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UP_Modal" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="L_TItulo_Modal" runat="server">Nova Unidade:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row rr">
                                        <div class="col-md-12">
                                            <asp:Label ID="L_Titulo_Unidade" runat="server" Text="Título:"></asp:Label>
                                            <asp:TextBox ID="TB_Titulo" runat="server" class="form-control" ToolTip="Título da Unidade"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV_TB_Titulo" runat="server" ControlToValidate="TB_Titulo"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                    class="validation">* Informe um título para esta unidade</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="B_Fechar" Text="CANCELAR" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Cancelar" />
                                    <asp:Button ID="B_Salvar" Text="SALVAR" runat="server" class="btn btn-primary" ToolTip="Salvar Questão" CausesValidation="true" ValidationGroup="A" />
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <!--Modal Exclusão-->
            <div class="modal fade" id="myModalE" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="L_Titulo_MOdal_E">Excluir Unidade:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row rr">
                                        <div class="col-md-12">
                                            <h4 class="modal-title" id="L_Titulo_E">Tem certeza que deseja excluir esta unidade bem como todo o seu conteúdo?</h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="B_Fecha_Exclusao" Text="Fechar" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Cancelar Exclusão da Unidade" />
                                    <asp:Button ID="B_Confirma_Exclusao" Text="Confirmar Exclusão da Unidade" runat="server" class="btn btn-primary" ToolTip="Excluir Unidade" />
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
