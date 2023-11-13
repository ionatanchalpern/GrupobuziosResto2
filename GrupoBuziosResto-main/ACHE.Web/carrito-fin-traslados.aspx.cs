using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;

public partial class carrito_fin_traslados : WebBasePage
{

    #region Properties

    private int ultimoPedidoId;

    private string Mode;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblOk.Visible = true;
        lblOk.Text = "Se ha enviado un mail al proveedor con los datos de la reserva.<br /><br />";
        
        if (!IsPostBack)
        {
            if (CurrentUser == null || CurrentUser.TipoUsuario != "O")
                Response.Redirect("login-traslados.aspx");
            hdnID.Value = string.IsNullOrEmpty(Request.QueryString["Id"]) ? "" : Request.QueryString["Id"].ToString();

        }
    }

    //protected void btnEnviarMail_Click(object sender, EventArgs e) {
    //    EnviarMailPedidoTraslado(ultimoPedidoId, CurrentUser.Nombre, CurrentUser.Email);
    //}

    protected void btnImprimirCupones_Click(object sender, EventArgs e)
    {
        imprimirCupones();
    }

    private void imprimirCupones()
    {
        Response.Redirect("pedidoPrintTraslado.aspx?Id=" + hdnID.Value);
    }

    private void EnviarMailPedidoTraslado(int idPedido, string nombreUsuario, string emailUsuario)
    {
        try
        {
            using (var dbContext = new ACHEEntities())
            {
                var pedido = dbContext.PedidosTraslado
                    .Include("Usuarios")
                    .Include("LugaresTrasladosOrigen")
                    .Include("LugaresTrasladosDestino")
                    .Include("Servicios").Include("Servicios.Proveedores")
                    .Include("PasajerosPorPedidoTraslado")
                    .Where(x => x.IDPedidoTraslado == idPedido).FirstOrDefault();
                if (pedido != null)
                {
                    ListDictionary replacements = new ListDictionary();

                    string idPedidoEncriptado = Cryptography.Encrypt(idPedido);
                    string idProveedorEncriptado = Cryptography.Encrypt(pedido.Servicios.IDProveedor);

                    replacements.Add("<FECHAGENERACION>", pedido.FechaAlta.ToString("D", new CultureInfo("es-ES")));

                    replacements.Add("<ID>", idPedido);
                    replacements.Add("<IDPEDIDO>", idPedidoEncriptado);
                    replacements.Add("<IDPROVEEDOR>", idProveedorEncriptado);
                    replacements.Add("<EMPRESA>", pedido.Usuarios.Empresa);
          //          replacements.Add("<PASAJERO>", pedido.Pasajero + " - DNI: " + pedido.Dni);
                    replacements.Add("<USUARIOOPERADOR>", nombreUsuario);
                    replacements.Add("<EMAILOPERADOR>", pedido.Usuarios.Email);
                    replacements.Add("<FECHARESERVA>", DateTime.Now.ToString("dd/MM/yyyy"));
                    replacements.Add("<PROVEEDOR>", pedido.Servicios.Proveedores.Nombre);
                    replacements.Add("<LOGO>", pedido.Servicios.Proveedores.Logo);
                    replacements.Add("<SERVICIO>", pedido.Servicios.Nombre);

                    if (pedido.Servicios.SubTipos.Tipo == "R")
                        replacements.Add("<LABELIDAVUELTA>", "Ida/Vuelta");
                    else if (pedido.Servicios.SubTipos.Tipo == "H")
                        replacements.Add("<LABELIDAVUELTA>", "Hotel a Hotel");
                    else if (pedido.FechaIda.HasValue)
                        replacements.Add("<LABELIDAVUELTA>", "Ida");
                    else
                        replacements.Add("<LABELIDAVUELTA>", "Vuelta");

                    if (pedido.Servicios.SubTipos.Tipo == "R")
                        replacements.Add("<IDAVUELTA>", pedido.LugaresTrasladosOrigen.Nombre + " / " + pedido.LugaresTrasladosDestino.Nombre + " / " + pedido.LugaresTrasladosOrigen.Nombre);
                    else if (pedido.Servicios.SubTipos.Tipo == "T")
                        replacements.Add("<IDAVUELTA>", pedido.LugaresTrasladosOrigen.Nombre + " / " + pedido.LugaresTrasladosDestino.Nombre);
                    else
                    {
                        replacements.Add("<IDAVUELTA>", pedido.LugaresTrasladosOrigen.Nombre + " / " + pedido.LugaresTrasladosDestino.Nombre);
                    }

                    if (pedido.Servicios.SubTipos.Tipo != "R")
                    {
             //           replacements.Add("<HOTELDESTINO>", pedido.HotelEstadia);
                        replacements.Add("<HOTELORIGEN>", pedido.Hotel4);
                    }
                    else
                    {
                        replacements.Add("<HOTELORIGEN>", "");
             //           replacements.Add("<HOTELDESTINO>", pedido.HotelEstadia);
                    }

                    replacements.Add("<OBSERVACIONESORIGEN>", pedido.Observaciones);
             //       replacements.Add("<OBSERVACIONESDESTINO>", pedido.ObservacionesDestino);

                    //if (pedido.FechaVuelta != null)
                    //    replacements.Add("<IDAVUELTA>", pedido.LugaresTrasladosOrigen.Nombre + " / " + pedido.LugaresTrasladosDestino.Nombre + " / " + pedido.LugaresTrasladosOrigen.Nombre);
                    //else
                    //    replacements.Add("<IDAVUELTA>", pedido.LugaresTrasladosOrigen.Nombre + " / " + pedido.LugaresTrasladosDestino.Nombre);
                    replacements.Add("<TIPOSERVICIO>", pedido.TipoServicio == "R" ? "Regular" : "Privado");

                    replacements.Add("<ORIGEN>", pedido.LugaresTrasladosOrigen.Nombre);
                    if (pedido.FechaIda.HasValue)
                        replacements.Add("<FECHAARRIBO>", pedido.FechaIda.Value.ToString("dd/MM/yyyy"));
                    replacements.Add("<HORAARRIBO>", pedido.HoraArribo);
                    replacements.Add("<AEROLINEAARRIBO>", pedido.AerolineaArribo);
                    replacements.Add("<NUMEROVUELOARRIBO>", pedido.VueloArribo);

                    replacements.Add("<DESTINO>", pedido.LugaresTrasladosDestino.Nombre);
                    string fechaVuelta = string.Empty;
                    if (pedido.FechaVuelta.HasValue)
                        fechaVuelta = pedido.FechaVuelta.Value.ToString("dd/MM/yyyy");
                    replacements.Add("<FECHAPARTIDA>", fechaVuelta);
                    replacements.Add("<HORAPARTIDA>", pedido.HoraPartida);
                    replacements.Add("<AEROLINEAPARTIDA>", pedido.AerolineaPartida);
                    replacements.Add("<NUMEROVUELOPARTIDA>", pedido.VueloPartida);

                    replacements.Add("<CANTADULTOS>", pedido.CantAdultos);
                    replacements.Add("<CANTMENORES1>", pedido.CantMenoresAsiento);
                    replacements.Add("<CANTMENORES2>", pedido.CantMenoresGratis);

                    var htmlinfoPasajeros = "<br><table> <tr><td style='width:150px'><b>Nombre</b></td><td style='width:150px'><b>DNI</b></td></tr>";
                    foreach (var pasajero in pedido.PasajerosPorPedidoTraslado)
                    {
                        htmlinfoPasajeros += "<tr>";
                        htmlinfoPasajeros += "<td style='width:150px'>" + pasajero.Nombre + "</td>";
                        htmlinfoPasajeros += "<td style='width:150px'>" + pasajero.DNI + "</td>";
                        htmlinfoPasajeros += "</tr>";

                    }
                    htmlinfoPasajeros += "</table>";
                    replacements.Add("<INFOPASAJEROS>", htmlinfoPasajeros);

                    replacements.Add("<OBSERVACIONES>", pedido.Observaciones);

                    bool send = false;
                    if (Mode == "M")
                    {
                        if (pedido.Servicios.SubTipos.Tipo == "H")
                            send = EmailHelper.SendMessage(EmailTemplate.ModificacionTransporteInternoProv, replacements, pedido.Servicios.Proveedores.Email, "TRASLADOS RED: Modificación Reserva #" + idPedido);
                        else
                            send = EmailHelper.SendMessage(EmailTemplate.ModificacionTransporteProv, replacements, pedido.Servicios.Proveedores.Email, "TRASLADOS RED: Modificación Reserva #" + idPedido);
                    }
                    else
                    {
                        if (pedido.Servicios.SubTipos.Tipo == "H")
                            send = EmailHelper.SendMessage(EmailTemplate.ReservaTransporteInternoProv, replacements, pedido.Servicios.Proveedores.Email, "TRASLADOS RED: Nueva Reserva #" + idPedido);
                        else
                            send = EmailHelper.SendMessage(EmailTemplate.ReservaTransporteProv, replacements, pedido.Servicios.Proveedores.Email, "TRASLADOS RED: Nueva Reserva #" + idPedido);
                    }
                    if (!send)
                        throw new Exception("Ha ocurrido un error al enviar el email, por favor intente nuevamente.<br /><br />");
                    else
                    {
                        lblError.Visible = false;
                        lblOk.Visible = true;
                        lblOk.Text = "Se ha enviado un mail al proveedor con los datos de la reserva.<br /><br />";
                    }
                }
                else
                    throw new Exception("No se ha encontrado la reserva");
            }
        }
        catch (Exception e)
        {
            showError(e.Message);
        }
    }

    private void showError(string exception)
    {
        lblError.Visible = true;
        lblError.Text = exception;
    }
}