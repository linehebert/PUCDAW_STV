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
            <br /><br />
            <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                <center><asp:Label ID="L_Erro" runat="server" SkinID="Skin_label_error" Text=""></asp:Label></center>
            </div>
            <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Aviso" runat="server" Text=""></asp:Label>
            </div>
            <br />
        </div>
        <div class="table-responsive">
            <center>
            <rsweb:ReportViewer ID="RV" runat="server" Font-Names="Arial" Font-Size="12pt" SizeToReportContent="True"
                AsyncRendering="False" Visible="false">
                <LocalReport ReportPath="Relatorios\Relatorio_Usuarios.rdlc" EnableHyperlinks="True" EnableExternalImages="True"></LocalReport>
            </rsweb:ReportViewer>
            </center>
            <br /><br />
        </div>
        <fieldset>
            <asp:Button ID="B_Voltar" Text="VOLTAR" runat="server" class="btn btn-default pull-left" ToolTip="Voltar" />
        </fieldset>
        <br /><br />
    </div>


</asp:Content>

