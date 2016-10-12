<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Conteudo.aspx.vb" Inherits="Consultas_Conteudo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>

<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">

    <div class="grid">
        <br />
        <br />
        <br />

        <div class="form-group">
            <div id="D_Erro" class="alert alert-danger" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Erro" runat="server" SkinID="Skin_label_error" Text=""></asp:Label>
            </div>
            <div id="D_Aviso" class="alert alert-success" role="alert" runat="server" visible="false">
                <asp:Label ID="L_Aviso" runat="server" Text=""></asp:Label>
            </div>

            <div class="panel panel-primary" id="Certificado" runat="server">
                <div class="row rr form-inline">
                    <div class="col-md-8 col-md-offset-1">
                        <h4><b>
                            <asp:Label class="text-primary" runat="server" Text="PARABÉNS!"></asp:Label></b></h4>
                        <asp:Label class="text-primary" runat="server" Text="O curso foi finalizado e você atingiu a média de aproveitamento."></asp:Label><br />
                        <asp:Label class="text-primary" runat="server" Text="Para gerar seu certificado clique no ícone."></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <center><asp:ImageButton ID="B_Gerar_Certificado"  ImageUrl="~/Images/icon_certify.png" runat="server" Width="60px" Height="60px" ToolTip="Gerar Certificado" /></center>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon glyphicon-info-sign" aria-hidden="true" style="margin-right: 20px;"></span>
                    Curso
                    <asp:Image ImageUrl="~/Images/encerrado.png" runat="server" CssClass="pull-right" ID="Div_Finalizado" Width="95px" />
                </div>
                <div class="row rr">
                    <div class="col-md-10 col-md-offset-1 table-responsive" style="text-align: center">
                        <asp:Label CssClass="Titulo_Curso" ID="L_Curso_Unidade" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row rr form-inline">
                    <div class="col-md-10 col-md-offset-1" style="text-align: center; margin-top: 15px">
                        <asp:Label ID="Dt_Inicio" runat="server" Text="Início:"></asp:Label>
                        <asp:Label ID="L_Dt_Inicio" runat="server" Text="1"></asp:Label>|
                        <asp:Label ID="Dt_Termino" runat="server" Text="Término:"></asp:Label>
                        <asp:Label ID="L_Dt_Termino" runat="server" Text=""></asp:Label>
                    </div>
                </div>
               
            </div>

             

            <div class="panel panel-default" id="Nenhuma_Unidade" visible="false" runat="server">
                <div class="row rr">
                    <div class="col-md-10 col-md-offset-1" style="text-align: center; margin-top: 30px; margin-bottom: 30px;">
                        <asp:Label ID="Label1" runat="server" Text="Nenhuma unidade foi disponibilidade para este curso no momento."></asp:Label>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="UP_Unidades" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Repeater ID="rptUnidades" runat="server">
                        <ItemTemplate>
                            <div class="panel panel-default">
                                <div class="panel-heading" id="Atividades">
                                    <span class="glyphicon glyphicon glyphicon-th-list" aria-hidden="true" style="margin-right: 20px;"></span>
                                    UNIDADE: <%# Container.DataItem("Titulo").ToString.ToUpper %>
                                </div>
                                <asp:UpdatePanel ID="UP_Materiais" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Repeater ID="rptMateriais" runat="server" OnItemCommand="rptMateriais_ItemCommand">
                                            <HeaderTemplate>
                                                <div class="row">
                                                    <div class="col-md-8 col-md-offset-2 form-inline">
                                                        <br />
                                                        <div class="pull-right">
                                                            <asp:Label ID="L_Title" runat="server" Text="MATERIAIS"></asp:Label>
                                                            <asp:Image ImageUrl="~/Images/materiais.png" runat="server" />
                                                        </div>
                                                        <br />
                                                        <hr />
                                                    </div>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="MateriaisPanel" class="panel-collapse collapse in">
                                                    <div class="row rr form-inline">
                                                        <div class="col-md-8 col-md-offset-2">
                                                            <asp:LinkButton ID="lnk_Material" runat="server" data-toggle="ToolTip" title="Visualizar Material" CommandArgument='<%# Container.DataItem("Cod_Tipo").ToString() + "," + Container.DataItem("Cod_Material").ToString() %>' CommandName="ExibirMaterial">
                                                        <%# Container.DataItem("Titulo").ToString.ToUpper %>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="row rr" style="text-align: center">
                                                    <asp:Label ID="L_Emptym" runat="server" Text="Nenhum material disponível para esta unidade" Visible="false"></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UP_Atividades" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Repeater ID="rptAtividades" runat="server">
                                            <HeaderTemplate>
                                                <div class="row">
                                                    <div class="col-md-8 col-md-offset-2 form-inline">
                                                        <br />
                                                        <div class="pull-right">
                                                            <asp:Label ID="L_Title" runat="server" Text="ATIVIDADES"></asp:Label>
                                                            <asp:Image ImageUrl="~/Images/atividades.png" runat="server" />
                                                        </div>
                                                        <br />
                                                        <hr />
                                                    </div>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="AtividadesPanel" class="panel-collapse collapse in">
                                                    <div class="row rr form-inline">
                                                        <div class="col-md-8 col-md-offset-2">
                                                            <a href="../Cadastros/Atividade.aspx?Atv=<%# Container.DataItem("Cod_Atividade") %>" data-toggle="ToolTip" title="Visualizar Atividade">
                                                                <%# Container.DataItem("Titulo").ToString.ToUpper %>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="row rr" style="text-align: center">
                                                    <asp:Label ID="L_Empty" runat="server" Text="Nenhuma atividade disponível para esta unidade" Visible="false"></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <br />
                                <br />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
            <fieldset>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Button ID="B_Voltar" Text="VOLTAR" runat="server" class="btn btn-default" ToolTip="Voltar" />
                        <asp:Button ID="B_Avaliar" Text="AVALIAR CURSO" runat="server" class="btn btn-primary pull-right" ToolTip="Avaliar Curso" />
                    </div>
                </div>
            </fieldset>
            <!--Modal Exibição-->
            <div class="modal fade bs-example-modal-lg" id="myModalExibicao" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="L_Cabecalho">Conteúdo do Material</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Literal ID="LIT_Video" runat="server"></asp:Literal>
                                            <asp:Label Visible="false" ID="LB_Download" runat="server" Text='Este material contém um arquivo para download:'></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label Visible="false" ID="LB_Material_Download" runat="server" Text=''></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer">
                            <asp:Button ID="B_Fechar_Exibicao" Text="Fechar" runat="server" data-dismiss="modal" class="btn btn-default" ToolTip="Fechar Exibição" />
                            <asp:Button ID="B_Download" Text="Download" runat="server" class="btn btn-primary" ToolTip="Baixar Arquivo" />
                            <asp:Button ID="B_Abrir" Text="Abrir" runat="server" class="btn btn-primary" ToolTip="Abrir Arquivo" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>


            <!--Modal Avaliação -->
            <div class="modal fade" id="myModalAv" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="L_Titulo_MOdal_Av">Avaliar Curso:</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row rr">
                                        <div class="col-md-12">
                                            Nota do Curso:
                                            <div class="vote">
                                                <label>
                                                    <input type="radio" name="fb" value="1" id="um" runat="server" />
                                                    <i class="fa"></i>
                                                </label>
                                                <label>
                                                    <input type="radio" name="fb" value="2" id="dois" runat="server"/>
                                                    <i class="fa"></i>
                                                </label>
                                                <label>
                                                    <input type="radio" name="fb" value="3" id="tres" runat="server" />
                                                    <i class="fa"></i>
                                                </label>
                                                <label>
                                                    <input type="radio" name="fb" value="4" id="quatro" runat="server" />
                                                    <i class="fa"></i>
                                                </label>
                                                <label>
                                                    <input type="radio" name="fb" value="5" id="cinco" runat="server" />
                                                    <i class="fa"></i>
                                                </label>
                                            </div>
                                            <asp:Label ID="voto" runat="server" Text="" ClientIDMode="Static" style="color:white"></asp:Label>
                                            <br />
                                            Comentário:
                                            <br />
                                            <asp:TextBox ID="TB_Comentario" runat="server" class="form-control" ToolTip="Comentário sobre a avaliação" TextMode="MultiLine" Columns="50" Rows="4"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RFV_TB_Comentario" runat="server" ControlToValidate="TB_Comentario"
                                                Display="Dynamic" ErrorMessage="Campo Obrigatório;" SetFocusOnError="True" ValidationGroup="A"
                                                class="validation">* Informe um título para este curso</asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="B_Fecha_Avaliacao" Text="Fechar" runat="server" data-dismiss="modal" class="btn btn-default" />
                                    <asp:Button ID="B_Confirma_Avaliacao" Text="Enviar Avaliação" runat="server" class="btn btn-primary" ToolTip="Avaliar Curso" />
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <script src="../js/jsstyle.js"></script>
        <script>
            function PintarEstrelas(notaSelecionada) {
                $('.vote label i.fa').each(function () {
                    /* checar de o valor clicado é menor ou igual do input atual
                    *  se sim, adicionar classe active
                    */
                    var $input = $(this).prev('input');
                    if ($input.val() <= notaSelecionada) {
                        $(this).addClass('active');
                    }
                });
            };

            $('.vote label i.fa').on('click mouseover', function () {
                // remove classe ativa de todas as estrelas
                $('.vote label i.fa').removeClass('active');
                // pegar o valor do input da estrela clicada
                var val = $(this).prev('input').val();
                //percorrer todas as estrelas
                $('.vote label i.fa').each(function () {
                    /* checar de o valor clicado é menor ou igual do input atual
                    *  se sim, adicionar classe active
                    */
                    var $input = $(this).prev('input');
                    if ($input.val() <= val) {
                        $(this).addClass('active');
                    }
                });
                $("#voto").html(val); // somente para teste
            });
            //Ao sair da div vote
            $('.vote').mouseleave(function () {
                //pegar o valor clicado
                var val = $(this).find('input:checked').val();
                //se nenhum foi clicado remover classe de todos
                if (val == undefined) {
                    $('.vote label i.fa').removeClass('active');
                } else {
                    //percorrer todas as estrelas
                    $('.vote label i.fa').each(function () {
                        /* Testar o input atual do laço com o valor clicado
                        *  se maior, remover classe, senão adicionar classe
                        */
                        var $input = $(this).prev('input');
                        if ($input.val() > val) {
                            $(this).removeClass('active');
                        } else {
                            $(this).addClass('active');
                        }
                    });
                }
                $("#voto").html(val); // somente para teste
            });
        </script>
</asp:Content>
