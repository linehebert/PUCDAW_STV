<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Con_Usuario.aspx.vb" Inherits="Consultas_Con_Usuario" %>

<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">


    <div class="grid">
        <br />
        <h2 class="text-primary">Gerenciar Usuários</h2>
        <hr />
        <asp:ImageButton ID="B_Novo" ImageUrl="~/Images/add.png" runat="server" ImageAlign="Right" ToolTip="Adicionar Usuário" />
        <br />

        <div class="form-group">
            <div class="row rr">
                <div class="col-md-5">
                    <asp:Label ID="L_Descricao" runat="server" class="control-label" Text="Nome:"></asp:Label>
                    <asp:TextBox ID="TB_Descr" runat="server" class="form-control" ToolTip="Busca por nome do usuário"></asp:TextBox>
                </div>
            </div>
            <div class="row rr">
                <div class="col-md-5">
                    <asp:Label ID="L_Departamento" runat="server" class="control-label" Text="Departamento:"></asp:Label>
                    <asp:DropDownList ID="DDL_Departamento" class="form-control" runat="server" DataValueField="Cod_Departamento" DataTextField="Descricao">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row rr">
                <div class="col-md-5">
                    <div class="checkbox-inline">
                        <asp:CheckBox ID="CB_Inativos" runat="server" Text="Exibe Inativos" ToolTip="Status do Registro" />
                    </div>
                    <asp:Button ID="B_Filtrar" Text="BUSCAR" runat="server" class="btn btn-primary pull-right" ToolTip="Buscar Usuários" />
                </div>
            </div>
            <br />
            <br />
            <div class="panel panel-primary  table-responsive">
                <div class="panel-heading">
                    Usuários
                </div>
                <div class="panel-body">
                    <asp:GridView ID="GV_Usuario" runat="server" AutoGenerateColumns="False" GridLines="None"
                        AllowPaging="true" PagerStyle-CssClass="pagination" CssClass="table table-striped table-hover table-condensed" AlternatingRowStyle-CssClass="alt"
                        PageSize="20">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HL_Relatorio" runat="server">
                                     <asp:Image ImageUrl="~/Images/relatorio.png" runat="server" ToolTip="Gerar Relatório de Acompanhamento"/>
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Cod_Usuario" HeaderText="Código" ItemStyle-VerticalAlign="Middle" Visible="false" />
                            <asp:TemplateField>
                                <HeaderTemplate>Nome Completo</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HL_Alterar" runat="server" Text='<%# Container.DataItem("Nome").ToString.ToUpper %>' data-toggle="ToolTip" title="Alterar Registro"> 
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CPF" HeaderText="CPF" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Email" HeaderText="E-mail" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Cod_Departamento" HeaderText="Cod_Departamento" ItemStyle-VerticalAlign="Middle" Visible="false" />
                            <asp:BoundField DataField="Departamento" HeaderText="Departamento" ItemStyle-VerticalAlign="Middle" />
                            <asp:CheckBoxField HeaderText="Inativo" DataField="Usuario_Inativo" SortExpression="Inativo" />
                        </Columns>
                        <RowStyle CssClass="cursor-pointer" />
                        <EmptyDataTemplate>
                            <label>
                                Nenhum registro encontrado!
                            </label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


