<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Con_Departamento.aspx.vb" Inherits="Consultas_Con_Departamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server"></asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="server">

    <div class="conteudo">

        <div class="grid">
            <br />
            <h2 class="text-primary">Gerenciar Departamentos</h2>
            <hr />
            <asp:ImageButton ID="B_Novo" ImageUrl="~/Images/add.png" runat="server" ImageAlign="Right" ToolTip="Adicionar Departamento" />
            <br />
            <div class="form-group">
                <div class="row rr">
                    <div class="col-md-5">
                        <asp:Label ID="L_Descricao" runat="server" Text="Descrição:"></asp:Label>
                        <asp:TextBox ID="TB_Descr" runat="server" class="form-control" ToolTip="Busca por descrição do departamento"></asp:TextBox>
                    </div>
                </div>
                <div class="row rr">
                    <div class="col-md-5">
                        <div class="checkbox-inline">
                            <asp:CheckBox ID="CB_Inativos" runat="server" Text="Exibe Inativos" ToolTip="Status do Registro" />
                        </div>
                        <asp:Button ID="B_Filtrar" Text="BUSCAR" runat="server" class="btn btn-primary pull-right" ToolTip="Buscar Departamentos" />
                    </div>
                </div>
                <br />
                <br />
                <div class="panel panel-primary table-responsive">
                    <div class="panel-heading">Departamentos </div>
                    <div class="panel-body">
                        <asp:GridView ID="GV_Departamento" runat="server" AutoGenerateColumns="False" GridLines="None"
                            AllowPaging="true" PagerStyle-CssClass="pagination" CssClass="table table-striped table-hover table-condensed" AlternatingRowStyle-CssClass="alt"
                            PageSize="20">
                            <Columns>
                                <asp:TemplateField ControlStyle-Width="15px">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Cod_Departamento" HeaderText="Código" ItemStyle-VerticalAlign="Middle" Visible="false" />
                                <asp:TemplateField ControlStyle-Width="50px">
                                    <HeaderTemplate>Descricao</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HL_Alterar" runat="server" Text='<%# Container.DataItem("Descricao").ToString.ToUpper %>' data-toggle="ToolTip" title="Alterar Registro"> 
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CheckBoxField HeaderText="Inativo" DataField="Departamento_Inativo" SortExpression="Inativo" />
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
    </div>

</asp:Content>

