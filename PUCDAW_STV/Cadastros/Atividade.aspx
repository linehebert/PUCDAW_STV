<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Atividade.aspx.vb" MasterPageFile="~/MasterPage.master" Inherits="Cadastros_Atividade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">
    <div class="grid">
        <br />
        <h2 class="text-primary">Realizar Atividade</h2>
        <hr />
        <br />

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
                            <div class="col-md-2 col-md-offset-1" style="text-align: right;">
                                <asp:Label ID="Label1" runat="server" Text="Curso:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Curso" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-2 col-md-offset-1" style="text-align: right;">
                                <asp:Label ID="Label7" runat="server" Text="Unidade:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Unidade" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-2 col-md-offset-1" style="text-align: right;">
                                <asp:Label ID="L_Titulo" runat="server" Text="Atividade:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Titulo" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-3" style="text-align: right;">
                                <asp:Label ID="L_Dt_Abertura" runat="server" Text="Data de Abertura:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Dt_Abertura" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row rr">
                            <div class="col-md-3" style="text-align: right;">
                                <asp:Label ID="L_Dt_Encerramento" runat="server" Text="Data de Encerramento:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Dt_Encerramento" runat="server" Text=""></asp:Label>
                            </div>

                        </div>
                        <div class="row rr">
                            <div class="col-md-2 col-md-offset-1" style="text-align: right;">
                                <asp:Label ID="L_Valor" runat="server" Text="Valor:" CssClass="formlabel"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Valor" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            <br />
            <div class="col-xs-12" id="Realizar_Atividade" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon glyphicon-eye-open" aria-hidden="true" style="margin-right: 20px;"></span>
                        Questões
                    </div>
                    <div id="D_Info" class="alert alert-info rr" role="alert" runat="server" visible="false">
                        <center> <asp:Label ID="L_Info" runat="server" Text=""></asp:Label></center>
                    </div><br /><br />
                    <fieldset id="Info_Questoes" runat="server">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UP_Atividade" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row rr form-inline">
                                        <div class="col-md-10 col-md-offset-1">
                                            <asp:Label ID="L_Questao" runat="server" Text="Questão:"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row rr form-inline">
                                        <div class="col-md-9 col-md-offset-2">
                                            <asp:RadioButton ID="RB_A" GroupName="Alternativa" runat="server" />
                                            <asp:Label ID="L_A" runat="server" Text="Alternativa A:"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row rr form-inline">
                                        <div class="col-md-9 col-md-offset-2">
                                            <asp:RadioButton ID="RB_B" GroupName="Alternativa" runat="server" />
                                            <asp:Label ID="L_B" runat="server" Text="Alternativa B:"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row rr form-inline">
                                        <div class="col-md-9 col-md-offset-2">
                                            <asp:RadioButton ID="RB_C" GroupName="Alternativa" runat="server" />
                                            <asp:Label ID="L_C" runat="server" Text="Alternativa C:"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row rr form-inline">
                                        <div class="col-md-9 col-md-offset-2">
                                            <asp:RadioButton ID="RB_D" GroupName="Alternativa" runat="server" />
                                            <asp:Label ID="L_D" runat="server" Text="Alternativa D:"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row rr form-inline">
                                        <div class="col-md-10 col-md-offset-1">
                                            <asp:Label ID="Label2" runat="server" Text="Justificativa:" Visible="false"></asp:Label>
                                            <asp:Label ID="L_Justificativa" runat="server" Text="" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <center>
                                <asp:Button ID="B_Anterior" Text="<<" runat="server" class="btn btn-default" ToolTip="Questão Anterior" />
                                <asp:Button ID="B_Proximo" Text=">>" runat="server" class="btn btn-default" ToolTip="Próxima Questão" />
                                <asp:Button ID="B_Finalizar" Text="Finalizar" runat="server" class="btn btn-default" ToolTip="FInalizar Atividade" visible="false" />
                                    </center>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </fieldset>
                </div>
            </div>
            <fieldset class="rr">
                <div class="row rr">
                    <div class="col-md-12 form-inline">
                        <asp:Button ID="B_Voltar" Text="VOLTAR" runat="server" class="btn btn-default" ToolTip="Voltar" />
                    </div>
                </div>
            </fieldset>

            <!--Modal Exclusão-->
            <div class="modal fade" id="myModalInfo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                </div>
                                <div class="modal-body">
                                    <h4 class="modal-title" id="myModalLabelInfo">Esta questão ainda não foi respondida. Selecione a alternativa que julga correta para esta questão.</h4>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="B_Responder" Text="RESPONDER" runat="server" class="btn btn-primary" ToolTip="Retornar a questão para responder." />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

