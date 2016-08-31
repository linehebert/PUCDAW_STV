<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cad_Atividade.aspx.vb" Inherits="Cadastros_Cad_Atividade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">
    <div class="grid">
        <h1>Cadastro de Atividades</h1>
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
                    <fieldset id="Info_Atividade" runat="server">
                        <div class="row rr">
                            <div class="col-md-2 col-md-offset-1" style="text-align:right;">
                                <asp:Label ID="Label1" runat="server" Text="Curso:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Curso" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-2 col-md-offset-1" style="text-align:right;">
                                <asp:Label ID="Label7" runat="server" Text="Unidade:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Unidade" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-2 col-md-offset-1" style="text-align:right;">
                                <asp:Label ID="L_Titulo" runat="server" Text="Atividade:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Titulo" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-3" style="text-align:right;">
                                <asp:Label ID="L_Dt_Abertura" runat="server" Text="Data de Abertura:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Dt_Abertura" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-3" style="text-align:right;">
                                <asp:Label ID="L_Dt_Encerramento" runat="server" Text="Data de Encerramento:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Dt_Encerramento" runat="server" Text=""></asp:Label>
                            </div>

                        </div>
                        <div class="row rr">
                            <div class="col-md-2 col-md-offset-1" style="text-align:right;">
                                <asp:Label ID="L_Valor" runat="server" Text="Valor:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Valor" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>

            <div class="col-xs-12" id="Cad_Questões" runat="server">
                <div class="panel panel-default table-responsive">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon glyphicon-eye-open" aria-hidden="true" style="margin-right: 20px;"></span>
                        Conteúdo da Atividade
                    </div>
                    <div id="D_Info" class="alert alert-info rr" role="alert" runat="server" visible="false">
                        <center> <asp:Label ID="L_Info" runat="server" Text=""></asp:Label></center>
                    </div>
                    <asp:UpdatePanel ID="UP_Atividade" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <fieldset id="Info_Questoes" runat="server">
                                <div class="col-md-12">
                                    <div class="row rr">
                                        <div class="col-md-3 col-md-offset-9" style="text-align: center">
                                            <asp:Button ID="B_Add_Questao" runat="server" class="btn btn-primary" Text="Adicionar Questão" ToolTip="Nova Questão" />
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="row rr">
                                                <div class="col-md-12">
                                                    <asp:Repeater ID="rptQuestoes" runat="server">
                                                        <ItemTemplate>
                                                            <hr />
                                                            <div class="row rr form-inline">
                                                                <div class="col-md-1 col-md-offset-10">
                                                                    <asp:ImageButton ID="Editar" ImageUrl="~/Images/edit.png" OnCommand="Carrega_Modal" CommandArgument='<%# Container.DataItem("Cod_Questao").ToString.ToUpper %>' runat="server" ImageAlign="Left" ToolTip="Editar Questão" />
                                                                </div>
                                                                <div class="col-md-1">
                                                                    <asp:ImageButton ID="Excuir_Questao" ImageUrl="~/Images/delete.png" OnCommand="Carrega_Modal_Exclusao" CommandArgument='<%# Container.DataItem("Cod_Questao").ToString.ToUpper %>' runat="server" ImageAlign="Left" ToolTip="Excluir Questão" />
                                                                </div>
                                                            </div>
                                                            <div class="row rr form-inline">
                                                                <div class="col-md-10 col-md-offset-1">
                                                                    <asp:Label ID="Label8" runat="server" Text="Questão:"></asp:Label>
                                                                    <%# Container.DataItem("Enunciado").ToString.ToUpper %>
                                                                </div>
                                                            </div>
                                                            <div class="row rr form-inline">
                                                                <div class="col-md-9 col-md-offset-2">
                                                                    <asp:RadioButton ID="RB_A" GroupName="Alternativa" runat="server" Checked='<%# IIf(Container.DataItem("Alternativa_Correta").ToString.ToUpper = "A", True, False) %>' Enabled="false" />
                                                                    <asp:Label ID="L_A" runat="server" Text="Alternativa A:"></asp:Label>
                                                                    <%# Container.DataItem("Alternativa_A").ToString.ToUpper %>
                                                                </div>
                                                            </div>
                                                            <div class="row rr form-inline">
                                                                <div class="col-md-9 col-md-offset-2">
                                                                    <asp:RadioButton ID="RB_B" GroupName="Alternativa" runat="server" Checked='<%# IIf(Container.DataItem("Alternativa_Correta").ToString.ToUpper = "B", True, False) %>' Enabled="false" />
                                                                    <asp:Label ID="L_B" runat="server" Text="Alternativa B:"></asp:Label>
                                                                    <%# Container.DataItem("Alternativa_B").ToString.ToUpper %>
                                                                </div>
                                                            </div>
                                                            <div class="row rr form-inline">
                                                                <div class="col-md-9 col-md-offset-2">
                                                                    <asp:RadioButton ID="RB_C" GroupName="Alternativa" runat="server" Checked='<%# IIf(Container.DataItem("Alternativa_Correta").ToString.ToUpper = "C", True, False) %>' Enabled="false" />
                                                                    <asp:Label ID="L_C" runat="server" Text="Alternativa C:"></asp:Label>
                                                                    <%# Container.DataItem("Alternativa_C").ToString.ToUpper %>
                                                                </div>
                                                            </div>
                                                            <div class="row rr form-inline">
                                                                <div class="col-md-9 col-md-offset-2">
                                                                    <asp:RadioButton ID="RB_D" GroupName="Alternativa" runat="server" Checked='<%# IIf(Container.DataItem("Alternativa_Correta").ToString.ToUpper = "D", True, False) %>' Enabled="false" />
                                                                    <asp:Label ID="L_D" runat="server" Text="Alternativa D:"></asp:Label>
                                                                    <%# Container.DataItem("Alternativa_D").ToString.ToUpper %>
                                                                </div>
                                                            </div>
                                                            <div class="row rr form-inline">
                                                                <div class="col-md-10 col-md-offset-1">
                                                                    <asp:Label ID="L_Justificativa" runat="server" Text="Justificativa:"></asp:Label>
                                                                    <%# Container.DataItem("Justificativa").ToString.ToUpper %>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <fieldset class="rr">
                <div class="row rr">
                    <div class="col-md-12">
                        <asp:Button ID="B_Voltar" Text="VOLTAR" runat="server" class="btn btn-default" ToolTip="Voltar" />
                        <asp:Button ID="B_Publicar" Text="PUBLICAR" runat="server" class="btn btn-primary" ToolTip="Liberar Atividade Aos Alunos" />
                    </div>
                </div>
            </fieldset>

            <!-- Modal -->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UP_Modal" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="myModalLabel">Nova Questão:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row rr">
                                        <div class="col-md-12">
                                            <asp:Label ID="L_Questao" runat="server" Text="Enunciado:"></asp:Label>
                                            <asp:TextBox ID="TB_Enunciado" runat="server" class="form-control" TextMode="MultiLine" ToolTip="Enunciado da Questão" Columns="50" Rows="6"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV_TB_Enunciado" runat="server" ControlToValidate="TB_Enunciado"
                                                Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                                class="validation">* Informe o enunciado desta questão</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row rr">
                                        <div class="col-md-11">
                                            <asp:Label ID="Label3" runat="server" Text="Alternativa A:"></asp:Label>
                                            <asp:TextBox ID="TB_Alternativa_A" runat="server" class="form-control" TextMode="MultiLine" ToolTip="Alternativa A" Columns="50" Rows="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV_TB_Alternativa_A" runat="server" ControlToValidate="TB_Alternativa_A"
                                                Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                                class="validation">* Todas as alternativas são obrigatórias</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row rr">
                                        <div class="col-md-11">
                                            <asp:Label ID="Label2" runat="server" Text="Alternativa B:"></asp:Label>
                                            <asp:TextBox ID="TB_Alternativa_B" runat="server" class="form-control" TextMode="MultiLine" ToolTip="Alternativa B" Columns="50" Rows="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV_TB_Alternativa_B" runat="server" ControlToValidate="TB_Alternativa_B"
                                                Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                                class="validation">* Todas as alternativas são obrigatórias</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row rr">
                                        <div class="col-md-11">
                                            <asp:Label ID="Label4" runat="server" Text="Alternativa C:"></asp:Label>
                                            <asp:TextBox ID="TB_Alternativa_C" runat="server" class="form-control" TextMode="MultiLine" ToolTip="Alternativa C" Columns="50" Rows="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV_TB_Alternativa_C" runat="server" ControlToValidate="TB_Alternativa_C"
                                                Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                                class="validation">* Todas as alternativas são obrigatórias</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row rr">
                                        <div class="col-md-11">
                                            <asp:Label ID="Label5" runat="server" Text="Alternativa D:"></asp:Label>
                                            <asp:TextBox ID="TB_Alternativa_D" runat="server" class="form-control" TextMode="MultiLine" ToolTip="Alternativa D" Columns="50" Rows="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV_TB_Alternativa_D" runat="server" ControlToValidate="TB_Alternativa_D"
                                                Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                                class="validation">* Todas as alternativas são obrigatórias</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row rr">
                                        <div class="col-md-11">
                                            <asp:Label ID="Label6" runat="server" Text="Alternativa Correta:"></asp:Label>
                                            <div class="checkbox-inline">
                                                <asp:RadioButtonList ID="RBL_Alternativa_Correta" CellPadding="5" CellSpacing="5" CssClass="Checkbox-inline" runat="server"
                                                    RepeatLayout="Flow" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="A" Selected="True">&nbsp; A &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="B">&nbsp;  B&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="C">&nbsp;  C&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="D">&nbsp;  D&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row rr">
                                        <div class="col-md-11">
                                            <asp:Label ID="L_Justiftv" runat="server" Text="Justificativa:"></asp:Label>
                                            <asp:TextBox ID="TB_Justificativa" runat="server" class="form-control" TextMode="MultiLine" ToolTip="Alternativa D" Columns="50" Rows="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV_TB_Justificativa" runat="server" ControlToValidate="TB_Justificativa"
                                                Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                                class="validation">* Informe a justificativa para a resposta desta questão</asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="modal-footer">
                                        <asp:Button ID="B_Fechar" Text="Fechar" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Cancelar Cadastro de Questão" />
                                        <asp:Button ID="B_Salvar_Questao" Text="Salvar" runat="server" class="btn btn-success" ToolTip="Salvar Nova Questão" CausesValidation="true" ValidationGroup="A" />
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
                                    <h4 class="modal-title" id="myModalLabelE">Tem certeza que deseja excluir esta questão bem como todas as suas informações?</h4>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="B_Fecha_Exclusao" Text="Fechar" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Cancelar Exclusão da Questão" />
                                    <asp:Button ID="B_Confirma_Exclusao" Text="Confirmar Exclusão da Questão" runat="server" class="btn btn-primary" ToolTip="Excluir Questão" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <!--Modal Confirmação-->
            <div class="modal fade" id="myModalConfirm" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <h4>Atenção!
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="L_1" runat="server" Text="Ao publicar a atividade ela torna-se disponível a todos os alunos e consequentemente não poderá mais sofrer alterações em suas questões."></asp:Label><br />
                                    <br />
                                    <asp:Label ID="L_2" runat="server" Text="Tem certeza que deseja publicar esta atividade bem como todas as suas informações?"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="B_Fechar_Confir" Text="Cancelar" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Cancelar Publicação" />
                                    <asp:Button ID="B_Confirma_Publicar" Text="Confirmar Publicação" runat="server" class="btn btn-primary" ToolTip="Confirmar a publicação desta atividade" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
