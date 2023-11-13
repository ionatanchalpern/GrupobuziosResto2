<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="container-fluid main-slider">
        <div id="carousel-example-generic" class="carousel slide" data-ride="carousel" data-interval="2000">
            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                <li data-target="#carousel-example-generic" data-slide-to="3"></li>
                <li data-target="#carousel-example-generic" data-slide-to="4"></li>
            </ol>

            <!-- Wrapper for slides -->
            <div class="carousel-inner" role="listbox">
                <div class="item active" style="background-image: url(imgs/slider/1.jpg);">
                    <img src="imgs/slider/transparent.png" alt="" />
                </div>
                <div class="item" style="background-image: url(imgs/slider/2.jpg);">
                    <img src="imgs/slider/transparent.png" alt="" />
                </div>
                <div class="item" style="background-image: url(imgs/slider/3.jpg);">
                    <img src="imgs/slider/transparent.png" alt="" />
                </div>
                <div class="item" style="background-image: url(imgs/slider/4.jpg);">
                    <img src="imgs/slider/transparent.png" alt="" />
                </div>
                <div class="item" style="background-image: url(imgs/slider/5.jpg);">
                    <img src="imgs/slider/transparent.png" alt="" />
                </div>
            </div>

            <!-- Controls -->
            <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                <span class="sr-only">Anterior</span>
            </a>
            <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                <span class="sr-only">Siguiente</span>
            </a>
        </div>
    </div>

    <div class="container content-padding">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 cupons">
                <asp:Repeater runat="server" ID="rptRestaurantes" ClientIDMode="Static">
                    <ItemTemplate>
                        <div class="cupon">
                            <a href="restaurant.aspx?Id=<%# Eval("IDRestaurant") %>">
                              <span class="overlay" style="z-index: 1000"><img src="imgs/search-index.png" style="z-index: 1000" /></span>
              
                                <h3><%# Eval("Nombre") %></h3>
                                <img height="150" src="<%# Eval("Logo") %>" 
								    style="filter: grayscale(100%);-webkit-filter: grayscale(100%);-moz-filter: grayscale(100%);-ms-filter: grayscale(100%);-o-filter: grayscale(100%);filter: url(/assets/svg/desaturate.svg#greyscale);filter: gray;-webkit-filter: grayscale(1);" />
                                <p><%# Eval("Observaciones") %></p>
                                <p class="small"><%# Eval("Direccion") %></p>
                                <%# Eval("IDRestaurant").ToString() == "1" ? "<span class='link' style='FLOAT: LEFT;BACKGROUND-COLOR: #E84C47;TEXT-TRANSFORM: inherit;COLOR: #FFF;PADDING: 2PX;'>sólo ALMUERZOS</span><span class='link' style='LEFT: 45PX;'>Ver Detalles</span>" : "<span class='link'>Ver Detalles</span>" %>
                                                                
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

