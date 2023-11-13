<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="restaurant.aspx.cs" Inherits="restaurant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/lightbox.css" rel="stylesheet" />
    <script src="/js/lightbox.min.js"></script>
    <style type="text/css">
    .lightbox
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <h3>
                    <asp:Literal runat="server" ID="litNombreBc" />:
                    <asp:Literal runat="server" ID="litObservacionesBc" />
                </h3>
                <ol class="breadcrumb">
                    <li><a href="default.aspx">Home</a></li>
                    <li class="active"><asp:Literal runat="server" ID="litNombreBc2" /></li>
                </ol>
            </div>
        </div>
    </div>

    <div class="container content-padding">
        <div class="row">
            <div class="col-xs-12 col-sm-4 col-md-3 col-lg-3 place-info">
                <asp:Image runat="server" ID="imgLogo" CssClass="place-logo" />

                <div class="more-info">
                    <div style="min-height: 150px;">
                        <h3>
                            <asp:Literal runat="server" ID="litNombre" />
					    </h3>
                        <p>
                            <asp:Literal runat="server" ID="litObservaciones" />
                        </p>
                        <p>
                            <asp:Literal runat="server" ID="litDireccion" />
                        </p>
                        <p>
                            <asp:Literal runat="server" ID="litCiudad" />
                        </p>
                    </div>
                    <p class="time" style="padding-top: 10px;">
                        Horario de atención
                        <br />
                        <asp:Literal runat="server" ID="litAtencion" />
                        <br />
                        Tel: <asp:Literal runat="server" ID="litTelefono" />
                    </p>
                    
                </div>

                <asp:Literal runat="server" ID="litImageMapa"></asp:Literal>

                <%--<a class="example-image-link" href="img/demopage/image-1.jpg" data-lightbox="imageMapa">
                    <asp:Image runat="server" ID="imgMapa" CssClass="mapa-logo" />
                </a>--%>

                
            </div>

            <div class="col-xs-12 col-sm-5 col-md-6 col-lg-9 col-sm-offset-1 col-md-offset-2 col-lg-offset-0 place-menus">
                               <div class="menu clasico" id="divMenuClasico" runat="server">
                    <div class="first">
                        <h3>ENTRADA</h3>
                        <p>
                            <asp:Literal runat="server" ID="litEntradaClasico" />
                        </p>
                    </div>

                    <div class="main">
                        <h3>PLATO PRINCIPAL</h3>
                        <p>
                            <asp:Literal runat="server" ID="litPlatoPrincipalClasico" /></p>
                    </div>

                    <div class="desert">
                        <h3>POSTRE</h3>
                        <p>
                            <asp:Literal runat="server" ID="litPostreClasico" />
                        </p>
                    </div>

                    <div class="drinks">
                        <asp:Panel runat="server" ID="pnlSiBebidaClasico" Visible="false">
                            <h3>BEBIDA</h3>
                            <p>
                                <asp:Literal runat="server" ID="litBebidaClasico" />
                            </p>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlNoBebidaClasico" Visible="false">
                            <h3></h3>
                            <span class="no-drink">NO Incluye Bebida</span>
                        </asp:Panel>
                    </div>

                    <asp:Panel runat="server" ID="pnlClasicoDet" Visible="false" CssClass="detail">
                        <asp:HyperLink runat="server" ID="lnkMenuClasico" data-lightbox='imageMenuTurista' style="cursor:pointer; text-decoration:none ">
                            <p>Ver + detalle del menú Clasico > <img src="/imgs/icono_detalle_menu-04.png" /></p>
                        </asp:HyperLink>
                    </asp:Panel>
                </div>
                <div class="menu turist" id="divMenuTurista" runat="server">
                    <div class="first">
                        <h3>ENTRADA</h3>
                        <p>
                            <asp:Literal runat="server" ID="litEntradaTurista" />
                        </p>
                    </div>

                    <div class="main">
                        <h3>PLATO PRINCIPAL</h3>
                        <p>
                            <asp:Literal runat="server" ID="litPrincipalTurista" /></p>
                    </div>

                    <div class="desert">
                        <h3>POSTRE</h3>
                        <p>
                            <asp:Literal runat="server" ID="litPostreTurista" />
                        </p>
                    </div>

                    <div class="drinks">
                        <asp:Panel runat="server" ID="pnlSiBebidaTurista" Visible="false">
                            <h3>BEBIDA</h3>
                            <p>
                                <asp:Literal runat="server" ID="litBebidaTurista" />
                            </p>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlNoBebidaTurista" Visible="false">
                            <h3></h3>
                            <span class="no-drink">NO Incluye Bebida</span>
                        </asp:Panel>
                    </div>

                    <asp:Panel runat="server" ID="pnlTuristaDet" Visible="false" CssClass="detail">
                        <asp:HyperLink runat="server" ID="lnkMenuTurista" data-lightbox='imageMenuTurista' style="cursor:pointer; text-decoration:none ">
                            <p>Ver + detalle del menú Turista > <img src="/imgs/icono_detalle_menu-04.png" /></p>
                        </asp:HyperLink>
                    </asp:Panel>
                </div>
               
                <!--
                <div class="menu premium" id="divMenuPremium" runat="server">
                    <div class="first">
                        <h3>ENTRADA</h3>
                        <p>
                            <asp:Literal runat="server" ID="litEntradaPremium" />
                        </p>
                    </div>

                    <div class="main">
                        <h3>PLATO PRINCIPAL</h3>
                        <p>
                            <asp:Literal runat="server" ID="litPrincipalPremium" />
                        </p>
                    </div>

                    <div class="desert">
                        <h3>POSTRE</h3>
                        <p>
                            <asp:Literal runat="server" ID="litPostrePremium" />
                        </p>
                    </div>

                    <div class="drinks">
                        
                        <asp:Panel runat="server" ID="pnlSiBebidaPremium" Visible="false">
                            <h3>BEBIDA</h3>
                            <p>
                                <asp:Literal runat="server" ID="litBebidaPremium" />
                            </p>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlNoBebidaPremium" Visible="false">
                            <h3></h3>
                            <span class="no-drink">NO Incluye Bebida</span>
                        </asp:Panel>
                        
                    </div>

                    <asp:Panel runat="server" ID="pnlPremiumDet" Visible="false" CssClass="detail">
                        <asp:HyperLink runat="server" ID="lnkMenuPremium" data-lightbox='imageMenuPremiun' style="cursor:pointer; text-decoration:none ">
                            <p>Ver + detalle del menú Premium > <img src="/imgs/icono_detalle_menu-04.png" /></p>
                        </asp:HyperLink>
                    </asp:Panel>
                </div>
                 <div class="menu playa" id="divMenuPlaya" runat="server">
                    <div class="first">
                        <h3>ENTRADAPLAYA</h3>
                        <p>
                            <asp:Literal runat="server" ID="litEntradaPlaya" />
                        </p>
                    </div>

                    <div class="main">
                        <h3>PLATO PRINCIPAL</h3>
                        <p>
                            <asp:Literal runat="server" ID="litPrincipalPlaya" />
                        </p>
                    </div>

                    <div class="desert">
                        <h3>POSTRE</h3>
                        <p>
                            <asp:Literal runat="server" ID="litPostrePlaya" />
                        </p>
                    </div>

                    <div class="drinks">
                        
                        <asp:Panel runat="server" ID="pnlSiBebidaPlaya" Visible="false">
                            <h3>BEBIDA</h3>
                            <p>
                                <asp:Literal runat="server" ID="litBebidaPlaya" />
                            </p>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlNoBebidaPlaya" Visible="false">
                            <h3></h3>
                            <span class="no-drink">NO Incluye Bebida</span>
                        </asp:Panel>
                        
                    </div>

                    <asp:Panel runat="server" ID="pnlPlayaDet" Visible="false" CssClass="detail">
                        <asp:HyperLink runat="server" ID="lnkMenuPlaya" data-lightbox='imageMenuPlaya' style="cursor:pointer; text-decoration:none ">
                            <p>Ver + detalle del menú Playa > <img src="/imgs/icono_detalle_menu-04.png" /></p>
                        </asp:HyperLink>
                    </asp:Panel>
                </div>

                <div class="clearfix"></div>
                
                -->
                  <div class="menu buffetlibre" id="divMenuBuffetLibre" runat="server">
                    <div class="first">
                        <h3>ENTRADA</h3>
                        <p>
                            <asp:Literal runat="server" ID="litEntradaBuffetLibre" />
                        </p>
                    </div>

                    <div class="main">
                        <h3>PLATO PRINCIPAL</h3>
                        <p>
                            <asp:Literal runat="server" ID="litPrincipalBuffetLibre" /></p>
                    </div>

                    <div class="desert">
                        <h3>POSTRE</h3>
                        <p>
                            <asp:Literal runat="server" ID="litPostreBuffetLibre" />
                        </p>
                    </div>

                    <div class="drinks">
                        <asp:Panel runat="server" ID="pnlSiBebidaBuffetLibre" Visible="false">
                            <h3>BEBIDA</h3>
                            <p>
                                <asp:Literal runat="server" ID="litBebidaBuffetLibre" />
                            </p>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlNoBebidaBuffetLibre" Visible="false">
                            <h3></h3>
                            <span class="no-drink">NO Incluye Bebida</span>
                        </asp:Panel>
                    </div>

                    <asp:Panel runat="server" ID="pnlBuffetLibreDet" Visible="false" CssClass="detail">
                        <asp:HyperLink runat="server" ID="lnkMenuBuffetLibre" data-lightbox='imageMenuBuffetLibre' style="cursor:pointer; text-decoration:none ">
                            <p>Ver + detalle del menú Buffet> <img src="/imgs/icono_detalle_menu-04.png" /></p>
                        </asp:HyperLink>
                    </asp:Panel>
                </div>
                <div class="row" id="divMenuKids" runat="server">
                    <div class="col-sx-12 col-sm-12 col-md-12 col-lg-12 kids-menu-wrapper">
                        <div class="kids-menu-details">
                            <div class="info">
                                <h2>Menu Kids <span class="small">(3 a 10 años) <br />Menores de 2 años no abonan.</span></h2>
                                <%--<h3>ENTRADA</h3>
                                <p>Entradas Cocina</p>
                                <p>
                                    <asp:Literal runat="server" ID="litEntradaKids" />
                                </p>--%>
                                <h3 class="bottom-separator">PLATO PRINCIPAL</h3>
                                <p>
                                    <asp:Literal runat="server" ID="litPrincipalKids" />
                                </p>

                                <%--<p class="bottom-separator">Pastas.</p>--%>
                                <h3 class="bottom-separator">Postre</h3>
                                <p><asp:Literal runat="server" ID="litPostreKids" /></p>

                                <asp:Panel runat="server" ID="pnlSiBebidaKids" Visible="false">
                                    <h3 class="bottom-separator">BEBIDA</h3>
                                    <p><asp:Literal runat="server" ID="litBebidaKids" /></p>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlNoBebidaKids" Visible="false">
                                    <p class="bottom-separator"></p>
                                    <span class="no-drink-tag">NO Incluye Bebida</span>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>

        <asp:Panel runat="server" ID="pnlImagenes">
            <div class="row">
                <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9 place-pictures pull-right">
                    <div class="wrapper">
                        <asp:Literal runat="server" ID="litImagen1"></asp:Literal>

                        <asp:Literal runat="server" ID="litImagen2"></asp:Literal>

                        <asp:Literal runat="server" ID="litImagen3"></asp:Literal>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

