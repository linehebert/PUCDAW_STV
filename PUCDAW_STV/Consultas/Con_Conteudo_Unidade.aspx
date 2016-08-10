<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Con_Conteudo_Unidade.aspx.vb" Inherits="Consultas_Con_Conteudo_Unidade" %>



<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>

<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid">
        <h1>Visualizar Unidade</h1>
        <hr />
        <div>
            <asp:Label ID="curso" runat="server" Text="Curso:"></asp:Label>
            <asp:Label ID="L_Curso_Unidade" runat="server" Text=""></asp:Label>
        </div>
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
                    Unidade
                </div>
                <div class="row rr">
                    <div class="col-md-10 col-md-offset-1" style="text-align: center">
                        <asp:Label ID="L_Titulo" runat="server" Text="Unidade" CssClass="Titulo_Curso"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading expandir" id="Materiais">
                    <span class="glyphicon glyphicon glyphicon-th-list" aria-hidden="true" style="margin-right: 20px;"></span>Materiais Didáticos
                    <span class="pull-right">
                        <a data-toggle="collapse" data-parent="#panel-quote-group" href="#MateriaisPanel">
                            <span class="toggle-icon glyphicon glyphicon-minus"></span>
                        </a>
                    </span>
                </div>
                <div id="MateriaisPanel" class="panel-collapse collapse in">
                    <div class="row rr form-inline">
                        <div class="col-md-2 col-md-offset-10">
                            <asp:ImageButton ID="B_Novo_Material" ImageUrl="~/Images/add.png" runat="server" ImageAlign="Right" ToolTip="Adicionar Novo Material Didático" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading expandir" id="Atividades">
                    <span class="glyphicon glyphicon glyphicon-th-list" aria-hidden="true" style="margin-right: 20px;"></span>Atividades Avaliativas
                    <span class="pull-right">
                        <a data-toggle="collapse" data-parent="#panel-quote-group" href="#AtividadesPanel">
                            <span class="toggle-icon glyphicon glyphicon-minus"></span>
                        </a>
                    </span>
                </div>
                <asp:UpdatePanel ID="UP_Atividade" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div id="AtividadesPanel" class="panel-collapse collapse in">
                            <div class="row rr form-inline">
                                <div class="col-md-2 col-md-offset-10">
                                    <asp:ImageButton ID="B_Nova_Atividade" ImageUrl="~/Images/add.png" runat="server" ImageAlign="Right" ToolTip="Adicionar Nova Atividade" />
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Repeater ID="rptAtividades" runat="server">
                                        <ItemTemplate>
                                            <div class="row rr form-inline">

                                                <div class="col-md-1  col-md-offset-3">
                                                    <asp:ImageButton ID="Editar" ImageUrl="~/Images/edit.png" OnCommand="Carrega_Modal_Alteracao" CommandArgument='<%# Container.DataItem("Cod_Atividade").ToString.ToUpper %>' runat="server" ImageAlign="Left" ToolTip="Alterar Atividade" />
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:ImageButton ID="Excuir_Unidade" ImageUrl="~/Images/delete.png" OnCommand="Carrega_Modal_Exclusao" CommandArgument='<%# Container.DataItem("Cod_Atividade").ToString.ToUpper %>' runat="server" ImageAlign="Left" ToolTip="Excluir Atividade" />
                                                </div>
                                                <div class="col-md-7">
                                                    <a href="../Cadastros/Cad_Atividade.aspx?Unit=<%# Container.DataItem("Cod_Unidade") %>&Atv=<%# Container.DataItem("Cod_Atividade") %>">
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

            <!--Modal de Inclusão-->
            <div class="modal fade" id="myModalI" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UP_Modal" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="L_TItulo_Modal" runat="server">Nova Atividade:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row rr">
                                        <div class="col-md-12">
                                            <asp:Label ID="Label1" runat="server" Text="Título:"></asp:Label>
                                            <asp:TextBox ID="TB_Titulo" runat="server" class="form-control" ToolTip="Título da Atividade"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row rr">
                                        <div class="col-md-6">
                                            <asp:Label ID="L_Dt_Abertura" runat="server" Text="Data de Abertura:"></asp:Label>
                                            <asp:TextBox ID="TB_Dt_Abertura" runat="server" class="form-control" ToolTip="Data de Abertura da Atividade" type="Date" name="dtinicio"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="L_Dt_Encerramento" runat="server" Text="Data de Encerramento:"></asp:Label>
                                            <asp:TextBox ID="TB_Dt_Encerramento" runat="server" class="form-control" ToolTip="Data de Fechamento da Atividade" type="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row rr">
                                        <div class="col-md-6">
                                            <asp:Label ID="L_Valor" runat="server" Text="Valor:"></asp:Label>
                                            <asp:TextBox ID="TB_Valor" runat="server" class="form-control" ToolTip="Valor Total da Atividade"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="B_Fechar" Text="CANCELAR" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Cancelar" />
                                    <asp:Button ID="B_Salvar" Text="SALVAR" runat="server" class="btn btn-primary" ToolTip="Salvar Atividade" />
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
                                    <h4 class="modal-title" id="L_Titulo_MOdal_E">Excluir Atividade:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row rr">
                                        <div class="col-md-12">
                                            <h4 class="modal-title" id="L_Titulo_E">Tem certeza que deseja excluir esta atividade bem como todo o seu conteúdo?</h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="B_Fecha_Exclusao" Text="Fechar" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Cancelar Exclusão da Questão" />
                                    <asp:Button ID="B_Confirma_Exclusao" Text="Confirmar Exclusão da Questão" runat="server" class="btn btn-primary" ToolTip="Excluir Questão" />
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script src="../js/jsstyle.js"></script>
</asp:Content>

