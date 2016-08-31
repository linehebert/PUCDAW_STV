<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Relatorio_Usuarios.aspx.vb" Inherits="Relatorios_Relatorio_Usuarios" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="C_Head" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid"> <br />
        <h2 class="text-primary">Relatório de Usuarios</h2>
        <hr /><br />

        <div class="form-group">
            <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Erro" runat="server" SkinID="Skin_label_error" Text=""></asp:Label>
            </div>
            <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Aviso" runat="server" Text=""></asp:Label>
            </div>

            <div class="row rr">
                <div class="col-md-10">
                    <asp:Label ID="L_Nome" runat="server" Text="Nome:"></asp:Label>
                    <div class="form-inline">
                        <asp:TextBox ID="TB_Nome" runat="server" class="form-control" Width="250px" ToolTip="Busca por nome do usuario"></asp:TextBox>
                        <asp:Button ID="B_Filtrar_Aluno" Text="BUSCAR" runat="server" class="btn btn-primary" ToolTip="Buscar Cursos" />
                    </div>
                </div>
            </div>
        </div>
        <rsweb:ReportViewer ID="RV" runat="server" Font-Names="Arial" Font-Size="12pt" SizeToReportContent="True"
            AsyncRendering="False" Visible="false">
            <localreport reportpath="Relatorios\Relatorio_Usuarios.rdlc" enablehyperlinks="True" enableexternalimages="True"></localreport>
        </rsweb:ReportViewer>
    </div>
    </div>


</asp:Content>

