<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Avaliacoes.aspx.vb" Inherits="Consultas_Avaliacoes" %>

<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid">
        <br />
        <br />
        <br />
        <div class="form-group">
            <asp:Image ImageUrl="~/Images/encerrado.png" runat="server" CssClass="pull-right" ID="Div_Finalizado" Width="100px" Visible="false" /> <br /><br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon glyphicon-info-sign" aria-hidden="true" style="margin-right: 20px;"></span>
                    Avaliações do Curso
                    
                </div>
                <center><h2 class="text-primary" id="titulo_curso" runat="server"></h2></center>
                <hr />
                <div class="row rr">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UP_Materiais" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:Repeater ID="rptAvaliacoes" runat="server">
                                    <ItemTemplate>
                                        <div id="MateriaisPanel" class="panel-collapse collapse in">
                                            <div class="row rr form-inline">
                                                <div class="col-md-2 col-md-offset-1">
                                                    <b>
                                                        <asp:Label ID="Label3" runat="server" Text="Usuário:" /></b>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:Label ID="Label2" runat="server">
                                                        <%# Container.DataItem("Nome").ToString.ToUpper %>
                                                    </asp:Label>
                                                </div>
                                            </div>
                                            <div class="row rr form-inline">
                                                <div class="col-md-2 col-md-offset-1">
                                                    <b>
                                                        <asp:Label ID="Label4" runat="server" Text="Nota:" /></b>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:Label ID="avaliacao" runat="server" Visible="false">
                                                        <%# Container.DataItem("Avaliacao").ToString.ToUpper %>
                                                    </asp:Label>
                                                    <asp:Image ImageUrl="~/Images/1.png" runat="server"  ID="Image1" Visible="false" />
                                                    <asp:Image ImageUrl="~/Images/2.png" runat="server"  ID="Image2" Visible="false" />
                                                    <asp:Image ImageUrl="~/Images/3.png" runat="server"  ID="Image3" Visible="false" />
                                                    <asp:Image ImageUrl="~/Images/4.png" runat="server"  ID="Image4" Visible="false" />
                                                    <asp:Image ImageUrl="~/Images/5.png" runat="server"  ID="Image5" Visible="false" />
                                                </div>
                                            </div>
                                            <div class="row rr form-inline">
                                                <div class="col-md-2 col-md-offset-1">
                                                    <b>
                                                        <asp:Label ID="Label5" runat="server" Text="Comentário:" /></b>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:Label ID="Label1" runat="server">
                                                        <%# Container.DataItem("Comentario").ToString.ToUpper %>
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="row rr form-inline">
                                    <div class="col-md-10 col-md-offset-1"><br />
                                      <center> <asp:Label ID="Nenhum_Comentario" runat="server" Text="Nenhuma avaliação cadastrada para este curso" Visible="false"></asp:Label></center> 
                                    <br /></div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row ">
                <fieldset class="rr">
                    <asp:Button ID="B_Voltar" Text="VOLTAR" runat="server" class="btn btn-default pull-left" ToolTip="Voltar" />
                </fieldset>
            </div>
        </div>
        <br />
        <br />
    </div>


</asp:Content>
