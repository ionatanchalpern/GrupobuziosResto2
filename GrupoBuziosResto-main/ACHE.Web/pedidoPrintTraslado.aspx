<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pedidoPrintTraslado.aspx.cs" Inherits="pedidoPrintTraslado" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <title>Grupo B&uacute;zios Rest&oacute;</title>
    <link rel="stylesheet" href="http://www.grupobuziosresto.com/css/cupon/styles.css">

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
        <div class="no-print">
            <asp:Button runat="server" ID="btnDescargar" Text="Descargar en PDF" OnClick="Descargar" />
        </div>
        <br />
        <div id="content">
            <div style="max-width: 1000px; font-family: Calibri;">
                <asp:panel style="border: 1px solid black;">
                    <div style="padding: 10px;">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <%--<asp:Image runat="server" ID="imgLogo" Width="200" />--%>
                                    <asp:Literal runat="server" ID="litLogo" />
                                </td>
                                <td style="vertical-align: bottom;">
                                    <h3>DETALLES DEL SERVICIO</h3>
                                </td>
                                <td style="vertical-align: bottom;text-align: right">
                                    <asp:Literal runat="server" ID="litLogo2" />
                                    <p>
                                        Generado el d&iacute;a:
                                    </p>
                                    <p>
                                        <asp:Label runat="server" ID="litFechaGeneracion" ForeColor="GrayText" />
                                    </p>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <hr style="border: 2px solid #4b1534;" />
                    </div>
                    <div style="padding: 10px;">
                        <table>
                            <tbody>
                                <tr>
                                    <td style="width: 800px;">
                                        <p>
                                            C&oacute;digo de la reserva:
                                            <asp:Label runat="server" ID="litId" ForeColor="GrayText" />
                                        </p>
                                        <br />
                                        <p>
                                            Operador Responsable:
                                            <asp:Label runat="server" ID="litEmpresa" ForeColor="GrayText" />
                                        </p>
                                        <br />
                                        <p>
                                            Operadora:
                                            <asp:Label runat="server" ID="litEmpresa2" ForeColor="GrayText" />
                                        </p>
                                        <br />
                                        <p>
                                            Usuario:
                                            <asp:Label runat="server" ID="litOperador" ForeColor="GrayText" />
                                        </p>
                                        <br />
                                        <p>
                                            Email:
                                            <asp:Label runat="server" ID="litEmailOperador" ForeColor="GrayText" />
                                        </p>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div style="padding: 10px;">
                        <h3>TRASLADOS</h3>
                        <hr style="border: 2px solid #4b1534;" />
                        <br />
                        <br />
                        <p>
                            Transportista:
                            <asp:Label runat="server" ID="litProveedor" ForeColor="GrayText" />
                        </p>
                        <br />
                        <p>
                            Servicio de Traslado:
                            <asp:Label runat="server" ID="litServicio" ForeColor="GrayText" />
                        </p>
                        <br />
                        <p>
                            Tipo de traslado:
                            <asp:Label runat="server" ID="litTipo" ForeColor="GrayText" />
                        </p>
                        <br />
                        <p>
                            Servicio:
                            <asp:Label runat="server" ID="litTipoServicio" ForeColor="GrayText" />
                        </p>
                        <br />
                    </div>
                    <asp:panel runat="server" ID="pnlIda" style="padding: 10px;">
                        <h3>IDA</h3>
                        <hr style="border: 2px solid #4b1534;" />
                        <br />
                        <br />
                        <table>
                            <tbody>
                                <tr>
                                    <td style="width: 800px;">
                                    
                                        <p>
                                            
                                            <asp:Label runat="server" ID="litaeropuertoida"  />
                                        </p>
                                        <br />
                                        <p>
                                            <asp:Label runat="server" ID="litcompaniaida"  />
                                        </p>
                                        <br />
                                        <p>
                                            <asp:Label runat="server" ID="litnrovueloida"  />
                                        </p>
                                        <br />
                                         <p>
                                            <asp:Label runat="server" ID="litfechaida"  />
                                        </p>  <br />
                                         <p>
                                            <asp:Label runat="server" ID="lithoraida" />
                                        </p>  <br />
                                        <p>
                                            <asp:Label runat="server" ID="litHotelOrigen" />
                                        </p><br/>
                                         <p>
                                            <asp:Label runat="server" ID="litDirehotel1" />
                                        </p><br/>
                                         <p>
                                            <asp:Label runat="server" ID="litHotelOrigen2" />
                                        </p>
                                        <br /> 
                                        <p>
                                            <asp:Label runat="server" ID="litDireHotelOrigen2" />
                                        </p>
                                       
                                        <br />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:panel>
                    <asp:panel runat="server" ID="pnlVuelta" style="padding: 10px;">
                        <h3>VUELTA</h3>
                        <hr style="border: 2px solid #4b1534;" />
                        <br />
                        <br />
                        <table>
                            <tbody>
                                <tr>
                                    <td style="width: 800px;">
                                        <p>
                                            <asp:Label runat="server" ID="litaeropuertoVuelta" />
                                        </p>
                                        <br />
                                        <p>
                                            <asp:Label runat="server" ID="litCompaniaVuelta" />
                                        </p>
                                        <br />
                                        <p>
                                            <asp:Label runat="server" ID="litNroVuelta" />
                                        </p>
                                        <br />
                                         <p>
                                            <asp:Label runat="server" ID="litFechaVuelta" />
                                        </p>  <br />
                                         <p>
                                            <asp:Label runat="server" ID="litHoraVuelta" />
                                        </p>  <br />
                                        <p>
                                            <asp:Label runat="server" ID="litHotelVuelta1" />
                                        </p><br/>
                                         <p>
                                            <asp:Label runat="server" ID="litDireHotelVuelta1" />
                                        </p><br/>
                                         <p>
                                            <asp:Label runat="server" ID="litHotelVuelta2" />
                                        </p>
                                        <br /> 
                                        <p>
                                            <asp:Label runat="server" ID="litDireHotelVuelta2" />
                                        </p>
                                       
                                        <br />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:panel>
                    <div style="padding: 10px;">
                        <h3>DATOS DEL PASAJERO</h3>
                        <hr style="border: 2px solid #4b1534;" />
                        <br />
                        <br />
                        <p>
                            Cantidad de Adultos:
                            <asp:Label runat="server" ID="litCantAdultos" ForeColor="GrayText" />
                        </p>
                        <br />
                        <p>
                            Cantidad de Menores que ocupan Asiento:
                            <asp:Label runat="server" ID="litCantMenores1" ForeColor="GrayText" />
                        </p>
                        <br />
                        <p>
                            Cantidad de Menores gratis hasta 3 a&ntilde;os:
                            <asp:Label runat="server" ID="litCantMenores2" ForeColor="GrayText" />
                        </p>
                        <br />
                        <p>
                            Información pasajeros:
                            <br /><br />
                            <span runat="server" ID="spnInfoPasajeros" ForeColor="GrayText" />
                        </p>
                        <br/>
                        <p>
                            Observaciones:<br />
                            <asp:Label runat="server" ID="litObservaciones" ForeColor="GrayText" />
                        </p>
                        <br />
                    </div>
                </div>
            </div>
            <br />
            <div style="max-width: 1000px; font-family: Calibri;">
                <div style="border: 1px solid black;">
                    <div style="padding: 10px;">
                        <br />
                        <asp:Label runat="server" ID="litTextoTraslados"/>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>




