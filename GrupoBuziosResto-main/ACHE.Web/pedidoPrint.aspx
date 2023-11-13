<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pedidoPrint.aspx.cs" Inherits="pedidoPrint" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <title>Grupo B&uacute;zios Rest&oacute;</title>
    <%--<link rel="stylesheet" href="<%= ResolveUrl("~/css/cupon/styles.css") %>">--%>
    <link rel="stylesheet" href="http://www.grupobuziosresto.com/css/cupon/styles.css?v=1">

    <!--[if lt IE 9]>
  <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
  <![endif]-->
    <style type="text/css">
        @media only print {
            .no-print {
                display: none;
            }
        }

        b {
            font-weight: bold;
        }
    </style>
</head>

<body>
    <form id="Form1" runat="server">
        <div align="center" class="no-print">
            <asp:Button runat="server" ID="btnDescargar" Text="Descargar en PDF" OnClick="Descargar" />
        </div>
        <div id="content">
            <asp:Repeater runat="server" ID="rptPedidos">
                <ItemTemplate>
                    <div class="coupon" style=" page-break-inside: avoid;height:240px">
                        <img class="scissors" src="http://www.grupobuziosresto.com/css/cupon/imgs/scissors.png">
                        <div class="logo"></div>
                        <div class="main-text">
                            <p class="number"><strong>Cupon N&deg;: <%# Eval("CodigoString") %></strong></p>
                            <p class="name"><strong>Nombre: <%# Eval("Nombre") %></strong></p>
                            <p class="dni"><strong>DNI: <%# Eval("NroDocumento") %></strong></p>
                            <p class="validity">V&aacute;lido desde <%# Eval("FechaDesdeString") %> hasta <%# Eval("FechaHastaString") %></p>
                            <p class="menu-type">
                                <strong>MEN&Uacute; <%# Eval("TipoMenu") %></strong>
								<%# Eval("Entrada") %>
                                <%--<span>(3 a 10 a&ntilde;os) Menores de 2 a&ntilde;os no abonan.</span>--%>
                            </p>
                            <p class="menu-validity"><strong>V&Aacute;LIDO PARA: 1 (UN) ALMUERZO O CENA</strong></p>
                            <p class="menu-description" style="margin-right: 30px;
">
                                <%# Eval("Texto") %>
                        <%# Eval("HorarioAtencion") %>       
                               Este cup&oacute;n <strong>No incluye propina</strong>. Queda a voluntad del cliente.
                            </p>
                        </div>
                        <div class="secondary-text">
                              <p>
                                CONSULTE RESTAURANTE ADHERIDOS EN : 
					<a href="http://www.grupobuziosresto.com/" >
						www.grupobuziosresto.com
                            	</a>

                            </p>
                        </div>
                        <h3 class="bottom-banner">Visite nuestro listado de <strong>Restaurantes Asociados</strong>
                            <img src="http://www.grupobuziosresto.com/css/cupon/imgs/utensils.png">
                            <strong>www.grupobuziosresto.com</strong></h3>
                        <div class="tertiary-text">
                            <h3 class="title">Datos &Uacute;tiles</h3>
                            <p>
                                El pasajero titular deber&aacute; presentar el o los <u>vouchers impresos</u> en el restaurante que haya seleccionado para consumir.<br>
                                Este cup&oacute;n <strong><u>No incluye propina</u></strong>. Queda a voluntad del cliente.<br/><br/>
                                <strong>UBICACI&Oacute;N DE RESTAURANTES</strong>:<br>
                              <%# Eval("Ubicacion") %> <br>
                                <br> <%# Eval("HorarioAtencionDatosUtiles") %>
                                                                <%# Eval("Excepciones") %>
                                <br/>  <br/>
                                Ante cualquier inconveniente podr&aacute; comunicarse de Lunes a Viernes al Tel. +5411-5275-0075 y a nuestro servicio de guarda los fines de semana: Cel. B&uacute;zios y  whatsapp: +005-22-992465598.<br>

                            </p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <br />
            <div class="benefits" style="height:500px;">
                <img src="http://www.grupobuziosresto.com/imgs/Icono_beneficios-02.png" />
                <br>
                <br />
                <asp:Literal runat="server" ID="litBeneficios" />
                <%--<span runat="server" id="litBeneficios"></span>--%>
            </div>
        </div>
    </form>
</body>
</html>




