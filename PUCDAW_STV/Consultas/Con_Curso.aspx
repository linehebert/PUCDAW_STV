<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Con_Curso.aspx.vb" Inherits="Consultas_Con_Curso" %>

<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid">
        <br />
        <h2 id="Titulo_Page" runat="server" class="text-primary">Consulta de Cursos </h2>
        <hr />
        <asp:ImageButton ID="B_Novo" ImageUrl="~/Images/add.png" runat="server" ImageAlign="Right" ToolTip="Adicionar Curso" />
        <br />


        <div class="form-group">
            <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Erro" runat="server" SkinID="Skin_label_error" Text=""></asp:Label>
            </div>
            <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Aviso" runat="server" Text=""></asp:Label>
            </div>

            <div class="row rr">
                <div class="col-md-5">
                    <asp:Label ID="L_Titulo" runat="server" Text="Título:"></asp:Label>
                    <asp:TextBox ID="TB_Titulo" runat="server" class="form-control" ToolTip="Busca por descrição do curso"></asp:TextBox>
                </div>

            </div>
            <div class="row rr" id="filtro_departamento" runat="server">
                <div class="col-md-5">
                    <asp:Label ID="L_Departamento" runat="server" Text="Departamento:"></asp:Label>
                    <asp:DropDownList ID="DDL_Departamento" runat="server" DataValueField="Cod_Departamento" DataTextField="Descricao" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row rr">
                <div class="col-md-5">
                    <asp:Label ID="L_Usuario" runat="server" Text="Instrutor:"></asp:Label>
                    <asp:DropDownList ID="DDL_Usuario" runat="server" DataValueField="Cod_Usuario" DataTextField="Nome" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row rr">
                <div class="col-md-1">
                    <asp:Button ID="B_Filtrar_Aluno" Text="BUSCAR" runat="server" class="btn btn-primary" ToolTip="Buscar Cursos" Visible="false" />
                </div>
            </div>
            <div class="row rr" id="filtro_inativo" runat="server">
                <div class="col-md-5">
                    <div class="checkbox-inline">
                        <asp:CheckBox ID="CB_Inativos" runat="server" Text="Exibe Inativos" ToolTip="Status do Registro" />
                    </div>
                    <asp:Button ID="B_Filtrar" Text="BUSCAR" runat="server" class="btn btn-primary pull-right" ToolTip="Buscar Cursos" />
                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="panel panel-primary table-responsive">
            <div class="panel-heading">Cursos</div>
            <div class="panel-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GV_Curso" runat="server" AutoGenerateColumns="False" GridLines="None"
                            AllowPaging="true" PagerStyle-CssClass="pagination" CssClass="table table-striped table-hover table-condensed" AlternatingRowStyle-CssClass="alt"
                            PageSize="20">
                            <Columns>
                                <asp:TemplateField ControlStyle-Width="90px" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Button ID="B_Inscrever" Text="Inscreva-se!" CommandName="B_Inscrever" CommandArgument='<%# Container.DataItem("Cod_Curso") %>' class="inscricao" runat="server" ToolTip="Inscrever-se neste curso" Visible="true" />
                                        <asp:Button ID="B_Inscrito" Text="Inscrito!" class="inscrito" runat="server" Enabled="false" ToolTip="Curso em que estou inscrito!" Visible="False" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HL_Avaliacoes" runat="server">
                                     <asp:Image ImageUrl="~/Images/avaliacao.png" runat="server" ToolTip="Avaliacoes do Curso"/>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HL_Alterar" runat="server">
                                     <asp:Image ImageUrl="~/Images/edit.png" runat="server" ToolTip="Alterar Registro"/>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Cod_Curso" HeaderText="Código" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField>
                                    <HeaderTemplate>Curso</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HL_Visualizar" runat="server" Text='<%# Container.DataItem("Titulo").ToString.ToUpper %>' data-toggle="ToolTip" title="Visualizar Curso"> 
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Titulo" HeaderText="Titulo" ItemStyle-VerticalAlign="Middle" />
                                <asp:BoundField DataField="Instrutor" HeaderText="Instrutor" ItemStyle-VerticalAlign="Middle" />
                                <asp:BoundField DataField="Categoria" HeaderText="Categoria" ItemStyle-VerticalAlign="Middle" />
                                <asp:BoundField DataField="Dt_Inicio" HeaderText="Data Início" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Dt_Termino" HeaderText="Data Término" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:CheckBoxField HeaderText="Inativo" DataField="Curso_Inativo" SortExpression="Inativo" />
                            </Columns>
                            <RowStyle CssClass="cursor-pointer" />
                            <EmptyDataTemplate>
                                <label id="msg" runat="server">
                                    Nenhum registro encontrado!
                                </label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <!--Modal Confirmãção-->
            <div class="modal fade" id="myModalC" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="L_Titulo_Modal_C">Confirmar Inscrição:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row rr">
                                        <div class="col-md-12">
                                            <asp:Label ID="L_Titulo_U" runat="server" Text="Olá"></asp:Label><br />
                                            <p>
                                                <asp:Label ID="Label2" runat="server" Text="Por favor, confirme sua inscrição para participar do curso"></asp:Label>
                                                <asp:Label ID="L_Titulo_C" runat="server" Text="" Style="font-weight: bold"></asp:Label>
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

