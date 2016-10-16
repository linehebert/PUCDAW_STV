<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="C_Conteudo" ContentPlaceHolderID="CPH_Conteudo" runat="Server">


    <div class="grid">
        <br />
        <asp:Label ID="Default" runat="server" Text=""></asp:Label>
        <h2 class="text-primary">Bem-Vindo ao STV Web! </h2>
        <hr />
        <br />
        <div class="col-xs-12">
            <div class="form-group">
                <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
<%--                <div class="col-md-10 col-md-offset-1">
                    <center><h4>
                    O STV Web é um sistema de treinamento em vídeo online que destina-se a empresas que buscam a qualificação de seus colaboradores de forma prática e objetiva!
                    Os cursos são oferecidos, personalizados, gerenciados e administrados por sua empresa de forma a atender todas as necessidades que englobam tempo, espaço físico e negociações com terceiros.
                </h4></center>
                </div>--%>

                    <!-- Indicators -->
                    <ol class="carousel-indicators">
                        <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                        <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                        <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                    </ol>

                    <!-- Wrapper for slides -->
                    <div class="carousel-inner" role="listbox">
                        <div class="item active">
                            <img src="Images/apresentacao.jpg" alt="..." width="100%" height="60%">
                            <div class="carousel-caption">
                                ...
                            </div>
                        </div>
                        <div class="item">
                            <img src="Images/personalizavel.jpg" alt="..." width="100%" height="60%">
                            <div class="carousel-caption">
                                ...
                            </div>
                        </div>
                        <div class="item">
                            <img src="Images/acessibilidade.jpg" alt="..." width="100%" height="60%">
                            <div class="carousel-caption">
                                ...
                            </div>
                        </div>
                    </div>

                    <!-- Controls -->
                    <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                        <span class="sr-only">Anterior</span>
                    </a>
                    <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                        <span class="sr-only">Próximo</span>
                    </a>
                </div>
                <br /><br />
            </div>
        </div>
    </div>
</asp:Content>
