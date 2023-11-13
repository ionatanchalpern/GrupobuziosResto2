using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Text;
using System.Net;
using Pechkin;
using System.IO;
using System.Globalization;

public partial class pedidoPrintTraslado : WebBasePage
{

    public string texto;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var session = string.IsNullOrEmpty(Request.QueryString["session"]) ? true : false;
            if (CurrentUser != null || !session)
            {
                int idPedido = string.IsNullOrEmpty(Request.QueryString["Id"]) ? 0 : int.Parse(Request.QueryString["Id"]);
                if (idPedido != 0)
                    generarDetallePedido(idPedido, session);
                else
                {
                    if (Request.UrlReferrer != null)
                        Response.Redirect(Request.UrlReferrer.ToString());
                    else
                        Response.Redirect("traslados-paso1.aspx");
                }
            }
            else
                Response.Redirect("login-traslados.aspx");
        }
    }

    private void generarDetallePedido(int id, bool session)
    {
        if (!session)
            btnDescargar.Visible = false;
        using (var dbContext = new ACHEEntities())
        {
            var pedido = dbContext.PedidosTraslado
                .Include("Servicios").Include("Servicios.Proveedores")
                .Where(x => x.IDPedidoTraslado == id).FirstOrDefault();
            if (pedido != null)
            {
                string url = "http://www.grupobuziosresto.com/files/logos/";
                litLogo.Text = "<img src='" + url + pedido.Servicios.Proveedores.Logo + "' width='200' />";
                litFechaGeneracion.Text = pedido.FechaAlta.ToString("D", new CultureInfo("es-ES"));
                if (File.Exists(Server.MapPath("~/files/proveedores/" + pedido.Usuarios.IDUsuario + ".jpg")))
                    litLogo2.Text = "<img src='/files/proveedores/" + pedido.Usuarios.IDUsuario + ".jpg' width='200' />";
                litId.Text = pedido.IDPedidoTraslado.ToString();
                litEmpresa.Text = litEmpresa2.Text = pedido.Usuarios.Empresa;
                litOperador.Text = pedido.Usuarios.Contacto;
                litEmailOperador.Text = pedido.Usuarios.Email;
                litProveedor.Text = pedido.Servicios.Proveedores.Nombre;
                litServicio.Text = reemplazarAcentos(pedido.Servicios.Nombre);

                //litIdaVuelta.Text = pedido.LugaresTrasladosOrigen.Nombre + " / " + pedido.LugaresTrasladosDestino.Nombre;
                if (pedido.Servicios.SubTipos.Tipo == "R")
                {
                    litTipo.Text = "ida y vuelta ";
                    string IdaVuelta = "";
                    if (pedido.LugaresTrasladosOrigen != null)
                        IdaVuelta = pedido.LugaresTrasladosOrigen.Nombre + " / ";
                    if (pedido.LugaresTrasladosDestino != null)
                        IdaVuelta += pedido.LugaresTrasladosDestino.Nombre;

                    //litIdaVuelta.Text = IdaVuelta;
                    if (pedido.LugaresTrasladosOrigen != null)
                    {
                        litaeropuertoida.Text = "Aeropuerto ida: " + pedido.LugaresTrasladosOrigen.Nombre;
                        litcompaniaida.Text = "Compa&ntilde;ia a&eacute;rea  ida: " + pedido.AerolineaArribo;
                        litnrovueloida.Text = "Nro de vuelo ida: " + pedido.VueloArribo;
                        lithoraida.Text = "Hora de llegada del a&eacute;reo: " + pedido.HoraArribo;
                    }
                    if (pedido.LugaresTrasladosDestino != null)
                    {
                        litaeropuertoVuelta.Text = "Aeropuerto vuelta: " + pedido.LugaresTrasladosDestino.Nombre;
                        litCompaniaVuelta.Text = "Compa&ntilde;ia a&eacute;rea partida: " + pedido.AerolineaPartida;
                        litNroVuelta.Text = "Nro de vuelo vuelta: " + pedido.VueloPartida;
                        litHoraVuelta.Text = "Hora de salida del a&eacute;reo: " + pedido.HoraPartida;

                    }
                }
                else
                {
                    if (pedido.LugaresTrasladosOrigen != null)
                    {
                        // litIdaVuelta.Text = "Aeropuerto ida: " + pedido.LugaresTrasladosOrigen.Nombre;
                        litaeropuertoida.Text = "Aeropuerto ida: " + pedido.LugaresTrasladosOrigen.Nombre;
                        litcompaniaida.Text = "Compa&ntilde;ia a&eacute;rea  ida: " + pedido.AerolineaArribo;
                        litnrovueloida.Text = "Nro de vuelo ida: " + pedido.VueloArribo;
                        lithoraida.Text = "Hora de llegada del a&eacute;reo: " + pedido.HoraArribo;
                    }
                    else if (pedido.LugaresTrasladosDestino != null)
                    {
                        //litIdaVuelta.Text = "Aeropuerto vuelta: " + pedido.LugaresTrasladosDestino.Nombre;
                        litaeropuertoVuelta.Text = "Aeropuerto vuelta: " + pedido.LugaresTrasladosDestino.Nombre;
                        litCompaniaVuelta.Text = "Compa&ntilde;ia a&eacute;rea vuelta: " + pedido.AerolineaPartida;
                        litNroVuelta.Text = "Nro de vuelo vuelta: " + pedido.VueloPartida;
                        litHoraVuelta.Text = "Hora de salida del a&eacute;reo: " + pedido.HoraPartida;

                    } if (pedido.FechaVuelta.HasValue)
                        litTipo.Text = "solo vuelta";
                    else
                        litTipo.Text = "solo ida";
                }
                //if (pedido.FechaVuelta.HasValue)
                //    litIdaVuelta.Text = pedido.LugaresTrasladosOrigen.Nombre + " / " + pedido.LugaresTrasladosDestino.Nombre + " / " + pedido.LugaresTrasladosOrigen.Nombre;

                litTipoServicio.Text = pedido.TipoServicio == "R" ? "Round trip" : "One way";

                ///   litOrigen.Text = litOrigen2.Text = pedido.LugaresTrasladosOrigen.Nombre;
                if (pedido.FechaIda.HasValue)
                    litfechaida.Text = "Fecha ida: " + pedido.FechaIda.Value.ToString("dd/MM/yyyy");

                if (pedido.FechaVuelta.HasValue)
                    litFechaVuelta.Text = "Fecha vuelta: " + pedido.FechaVuelta.Value.ToString("dd/MM/yyyy");
                if (pedido.Hotel2 != null && pedido.Hotel2 != "")
                {
                    if (pedido.Hotel1 != null && pedido.Hotel1 != "")
                    {
                        litHotelOrigen.Text = "Desde hotel: " + pedido.Hotel1;
                        ////litDirehotel1.Text = "Direcci&oacute;n hotel ida: " + reemplazarAcentos(pedido.DireccionHotel1);
                    }
                    litHotelOrigen2.Text = "Al hotel: " + pedido.Hotel2;
                    ////litDireHotelOrigen2.Text = "Direcci&oacute;n hotel ida 2: " + reemplazarAcentos(pedido.DireccionHotel2);
                }
                else
                {
                    litHotelOrigen.Text = "Hotel ida: " + pedido.Hotel1;
                    ////litDirehotel1.Text = "Direcci&oacute;n hotel ida: " + reemplazarAcentos(pedido.DireccionHotel1);

                }



                if (pedido.Hotel4 != null && pedido.Hotel4 != "")
                {
                    if (pedido.Hotel3 != null && pedido.Hotel3 != "")
                    {
                        litHotelVuelta1.Text = "Desde hotel: " + pedido.Hotel3;
                        ////litDireHotelVuelta1.Text = "Direcci&oacute;n hotel vuelta: " + reemplazarAcentos(pedido.DireccionHotel3);
                    }
                    litHotelVuelta2.Text = "Al hotel: " + pedido.Hotel4;
                    ////litDireHotelVuelta2.Text = "Direcci&oacute;n hotel vuelta 2: " + reemplazarAcentos(pedido.DireccionHotel4);
                }
                else
                {
                    litHotelVuelta1.Text = "Hotel vuelta: " + pedido.Hotel3;
                    ////litDireHotelVuelta1.Text = "Direcci&oacute;n hotel vuelta: " + reemplazarAcentos(pedido.DireccionHotel3);
                }


                litCantAdultos.Text = pedido.CantAdultos.ToString();
                litCantMenores1.Text = pedido.CantMenoresAsiento.ToString();
                litCantMenores2.Text = pedido.CantMenoresGratis.ToString();
                litObservaciones.Text = reemplazarAcentos(pedido.Observaciones);
                var htmlinfoPasajeros = "<table> <tr><td style='width:150px'><b>Nombre</b></td><td><b>DNI</b></td></tr>";
                foreach (var pasajero in pedido.PasajerosPorPedidoTraslado)
                {
                    htmlinfoPasajeros += "<tr>";
                    htmlinfoPasajeros += "<td style='width:150px'>" + reemplazarAcentos(pasajero.Nombre) + "</td>";
                    htmlinfoPasajeros += "<td style='width:150px'>" + pasajero.DNI + "</td>";
                    htmlinfoPasajeros += "</tr>";

                }
                htmlinfoPasajeros += "</table>";
                spnInfoPasajeros.InnerHtml = htmlinfoPasajeros;
                //    litNombrePasajero.Text = pedido.Pasajero + " - DNI: " + pedido.Dni;

                var parametros = dbContext.Parametros.FirstOrDefault();
                if (parametros != null)
                    litTextoTraslados.Text = reemplazarAcentos(parametros.TextoTraslados);
                
                if (pedido.FechaIda == null)
                    pnlIda.Visible = false;
                else if (pedido.FechaVuelta == null)
                    pnlVuelta.Visible = false;
                
            }
        }
    }

    private string reemplazarAcentos(string palabra)
    {
        if (palabra == null)
            return "";

        palabra = palabra.ReplaceAll("ó", "&oacute;").ReplaceAll("í", "&iacute;").ReplaceAll("é", "&eacute;").ReplaceAll("ú", "&uacute;").ReplaceAll("ñ", "&ntilde;").ReplaceAll("á", "&aacute;");

        return palabra;
    }

    protected void Descargar(object sender, EventArgs e)
    {
        string html;

        #region Read the HTML content

        using (var client = new WebClient())
        {
            //client.Headers[HttpRequestHeader.Cookie] =System.Web.HttpContext.Current.Request.Headers["Cookie"];
            html = client.DownloadString(@"http://localhost:56329/pedidoPrintTraslado.aspx?session=false&Id=" + Request.QueryString["Id"]);
        }

        #endregion

        #region Transform the HTML into PDF

        var pechkin = Factory.Create(new GlobalConfig());
        var pdf = pechkin.Convert(new ObjectConfig()
                                .SetLoadImages(true).SetZoomFactor(1.0)
                                .SetPrintBackground(true)
                                .SetScreenMediaType(true)
                                .SetIntelligentShrinking(true)
                                .SetAllowLocalContent(true)
                                .SetCreateForms(false)
                                .SetCreateExternalLinks(true), html);

        #endregion

        #region Return the pdf file

        Response.Clear();

        Response.ClearContent();
        Response.ClearHeaders();

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Voucher-" + Request.QueryString["Id"] + ".pdf; size={0}", pdf.Length));
        Response.BinaryWrite(pdf);

        Response.Flush();
        Response.End();

        #endregion
    }
}