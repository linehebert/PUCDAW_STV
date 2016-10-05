<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cad_Curso.aspx.vb" Inherits="Cadastros_Cad_Curso" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid"><br />
        <h2 class="text-primary" id="cad_Curso" runat="server">Cadastro de Cursos</h2>
        <hr /><br />

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

                    <fieldset class="rr" runat="server">
                        
                        <div class="row rr">
                            <div class="col-md-6 col-md-offset-3">
                                <asp:Label ID="L_Titulo" runat="server" Text="Título:"></asp:Label>
                                <asp:TextBox ID="TB_Titulo" runat="server" class="form-control" ToolTip="Título do Curso"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV_TB_Titulo" runat="server" ControlToValidate="TB_Titulo"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                    class="validation">* Informe um título para este curso</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-3 col-md-offset-3">
                                <asp:Label ID="L_Dt_Inicio" runat="server" Text="Data de Início:"></asp:Label>
                                <asp:TextBox ID="TB_Dt_Inicio" runat="server" class="form-control" ToolTip="INforme a data de início do curso" type="Date" name="dtinicio"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV_TB_Dt_Inicio" runat="server" ControlToValidate="TB_Dt_Inicio"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                    class="validation">* Campo Obrigatório</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="L_Dt_Termino" runat="server" Text="Data de Término:"></asp:Label>
                                <asp:TextBox ID="TB_Dt_Termino" runat="server" class="form-control" ToolTip="Informe a data de término do curso" type="Date"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV_TB_Dt_Termino" runat="server" ControlToValidate="TB_Dt_Termino"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                    class="validation">* Campo Obrigatório</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-6 col-md-offset-3">
                                <asp:Label ID="L_Usuario" runat="server" Text="Instrutor:"></asp:Label>
                                <asp:DropDownList ID="DDL_Usuario" runat="server" DataValueField="Cod_Usuario" class="form-control" DataTextField="Nome">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_DDL_Usuario" runat="server" ControlToValidate="DDL_Usuario"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" InitialValue="0" ValidationGroup="A"
                                    Class="validation">* Informe qual o instrutor deste curso</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-6 col-md-offset-3">
                                <asp:Label ID="L_Categoria" runat="server" Text="Categoria:"></asp:Label>
                                <asp:DropDownList ID="DDL_Categoria" runat="server" DataValueField="Cod_Categoria" class="form-control" DataTextField="Descricao" Value="0">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_DDL_Categoria" runat="server" ControlToValidate="DDL_Categoria"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" InitialValue="0" ValidationGroup="A"
                                    Class="validation">* Informe qual a categoria deste curso</asp:RequiredFieldValidator>
                                
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-6 col-md-offset-3">
                                <asp:Label ID="L_palavra_chave" runat="server" Text="Palavras-Chaves:"></asp:Label>
                                <asp:TextBox ID="TB_palavra_chave" runat="server" TextMode="MultiLine" ToolTip="Palavras-Chaves" Columns="50" Rows="5" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV_Pavras_Chave" runat="server" ControlToValidate="TB_palavra_chave"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                    class="validation">* Informe as palavras-chave desse curso.</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-6 col-md-offset-3">
                                <div class="checkbox-inline">
                                    <asp:CheckBox ID="CB_Inativos" runat="server" Text="Inativo" ToolTip="Status do Registro" CssClass="pull-right" />
                                </div>
                            </div>
                        </div>
                    </fieldset>

                </div>
            </div>

            <div class="col-xs-12" id="Visibilidade" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon glyphicon-eye-open" aria-hidden="true" style="margin-right: 20px;"></span>
                        Definir Visibilidade
                    </div>
                    <asp:UpdatePanel ID="UP_Visibilidade" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <fieldset class="rr">
                                <div class="col-md-5">
                                    <div class="row rr">
                                        <div class="col-md-12" style="text-align: center;">
                                            <asp:Label ID="L_N_Incluidos" runat="server" Text="Departamentos Sem Visibilidade"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row rr">
                                        <div class="col-md-12">
                                            <asp:ListBox ID="LB_NIncluidos" runat="server" CssClass="form-control" SelectionMode="Multiple" Rows="15"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2" style="margin-top: 5%">
                                    <div class="row rr">
                                        <asp:Button ID="B_Incluir_Todos" Text=">>" runat="server" class="btn btn-default form-control" ToolTip="Selecionar Todos os Departamento" />
                                        <asp:Button ID="B_Incluir" Text=">" runat="server" class="btn btn-default form-control" ToolTip="Selecionar Departamento" />
                                        <asp:Button ID="B_NIncluir" Text="<" runat="server" class="btn btn-default form-control" ToolTip="Desmarcar Departamento" />
                                        <asp:Button ID="B_NIncluir_Todos" Text="<<" runat="server" class="btn btn-default form-control" ToolTip="Desmarcar Todos os Departamentos Selecionados" />
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="row rr">
                                        <div class="col-md-12" style="text-align: center;">
                                            <asp:Label ID="Label1" runat="server" Text="Departamentos Com Visibilidade"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row rr">
                                        <div class="col-md-12">
                                            <asp:ListBox ID="LB_Incluidos" runat="server" CssClass="form-control" SelectionMode="Multiple" Rows="15"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <fieldset class="rr">
                <div class="row rr">
                    <div class="col-md-12  col-md-offset-9">
                        <asp:Button ID="B_Cancelar" Text="CANCELAR" runat="server" class="btn btn-danger" ToolTip="Cancelar" />
                        <asp:Button ID="B_Salvar" Text="SALVAR" runat="server" class="btn btn-primary" ToolTip="Salvar Registro" CausesValidation="true" ValidationGroup="A" />
                    </div>
                </div>
            </fieldset>

            <!--Modal Informações-->
            <div class="modal fade" id="myModalInfo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <h4>Atenção!
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="L_Info" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="B_Continuar" Text="CONTINUAR" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Continuar cadastro do curso" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
