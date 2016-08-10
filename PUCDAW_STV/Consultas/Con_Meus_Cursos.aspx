<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Con_Meus_Cursos.aspx.vb" Inherits="Consultas_Con_Meus_Cursos" %>


<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid">
        <h1 id="Titulo_Page" runat="server">Meus Cursos
        </h1>
        <br />
        <hr />

        <div class="form-group">
            <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                <asp:label id="L_Erro" runat="server" skinid="Skin_label_error" text=""></asp:label>
            </div>
            <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                <asp:label id="L_Aviso" runat="server" text=""></asp:label>
            </div>

            <div class="row rr">
                <div class="col-md-5">
                    <asp:label id="L_Titulo" runat="server" text="Título:"></asp:label>
                    <asp:textbox id="TB_Titulo" runat="server" class="form-control" tooltip="Busca por descrição do curso"></asp:textbox>
                </div>

            </div>
            <div class="row rr">
                <div class="col-md-5">
                    <asp:label id="L_Usuario" runat="server" text="Instrutor:"></asp:label>
                    <asp:dropdownlist id="DDL_Usuario" runat="server" datavaluefield="Cod_Usuario" datatextfield="Nome" class="form-control">
                    </asp:dropdownlist>
                </div>
                <asp:button id="B_Filtrar_Aluno" text="BUSCAR" runat="server" class="btn btn-primary" tooltip="Buscar Cursos" visible="false" />
            </div>
            <div class="row rr" id="filtro_inativo" runat="server">
                <div class="col-md-5">
                    <asp:button id="B_Filtrar" text="BUSCAR" runat="server" class="btn btn-primary pull-right" tooltip="Buscar Cursos" />
                </div>
            </div>
        </div>

        <div class="panel panel-primary table-responsive">
            <div class="panel-heading">Cursos</div>
            <div class="panel-body">
                <asp:updatepanel id="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GV_Curso" runat="server" AutoGenerateColumns="False" GridLines="None"
                            AllowPaging="true" PagerStyle-CssClass="pagination" CssClass="table table-striped table-hover table-condensed" AlternatingRowStyle-CssClass="alt"
                            PageSize="10">
                            <Columns>
                                <asp:TemplateField ControlStyle-Width="15px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HL_Visualizar" runat="server">
                                     <asp:Image ImageUrl="~/Images/visualizar.png" runat="server" ToolTip="Visualizar Conteúdo" />
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Cod_Curso" HeaderText="Código" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Titulo" HeaderText="Titulo" ItemStyle-VerticalAlign="Middle" />
                                <asp:BoundField DataField="Instrutor" HeaderText="Instrutor" ItemStyle-VerticalAlign="Middle" />
                                <asp:BoundField DataField="Dt_Inicio" HeaderText="Data Início" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Dt_Termino" HeaderText="Data Término" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:CheckBoxField HeaderText="Inativo" DataField="Curso_Inativo" SortExpression="Inativo" />
                            </Columns>
                            <RowStyle CssClass="cursor-pointer" />
                            <EmptyDataTemplate>
                                <label>
                                    Nenhum registro encontrado!
                                </label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:updatepanel>
            </div>

            <!--Modal Confirmãção-->
            <div class="modal fade" id="myModalMC" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <asp:updatepanel id="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="L_Titulo_Modal_MC">Confirmar Inscrição:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row rr">
                                        <div class="col-md-12"> 
                                            <asp:Label ID="L_Titulo_U" runat="server" Text="Olá"></asp:Label><br />
                                            <p>
                                             <asp:Label ID="Label2" runat="server" Text="Por favor, confirme sua inscrição para participar do curso"></asp:Label>
                                            <asp:Label ID="L_Titulo_C" runat="server" Text="" style="font-weight:bold"></asp:Label>
                                            </p>
                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="B_Fecha_Inscricao" Text="Cancelar" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Cancelar Inscrição em Curso" />
                                    <asp:Button ID="B_Confirma_Inscricao" Text="Confirmar Minha Inscrição" runat="server" class="btn btn-primary" ToolTip="Confirmar Inscrição no Curso" />
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


