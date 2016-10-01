<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Relatorio_Usuarios.aspx.vb" Inherits="Relatorios_Relatorio_Usuarios" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid">
        <br />
        <center><h2 class="text-primary">Relatório de Usuarios</h2></center>
        <hr />


        <div class="form-group">
            <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Erro" runat="server" SkinID="Skin_label_error" Text=""></asp:Label>
            </div>
            <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Aviso" runat="server" Text=""></asp:Label>
            </div>

            <div class="row rr">
                <div class="col-md-10">
                    <asp:Label ID="L_Usuario" runat="server" Text="Usuário:"></asp:Label>
                    <div class="form-inline">
                                <asp:DropDownList ID="DDL_Usuario" runat="server" DataValueField="Cod_Usuario" class="form-control" DataTextField="Nome" >
                                </asp:DropDownList>
                        <asp:Button ID="B_Filtrar_Aluno" Text="BUSCAR" runat="server" class="btn btn-primary" ToolTip="Buscar Cursos" />
                    </div>
                </div>
            </div>
            <br /></div>
            <div class="table-responsive">
                <center>
            
            <rsweb:ReportViewer ID="RV" runat="server" Font-Names="Arial" Font-Size="12pt" SizeToReportContent="True"
                AsyncRendering="False" Visible="false">
                <LocalReport ReportPath="Relatorios\Relatorio_Usuarios.rdlc" EnableHyperlinks="True" EnableExternalImages="True"></LocalReport>
            </rsweb:ReportViewer></center></div>
        
    </div>


</asp:Content>

