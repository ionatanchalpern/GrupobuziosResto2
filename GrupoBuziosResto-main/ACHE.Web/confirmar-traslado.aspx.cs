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

public partial class confirmar_traslado : WebBasePage
{

    #region Properties

    private int idPedido, idProveedor;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        string idPedidoEnc = Request.QueryString["IdPedido"];
        string idProvEnc = Request.QueryString["IdProveedor"];
        if (!IsPostBack)
        {
           string aux1 = Cryptography.Decrypt(idPedidoEnc);
            string aux2 = Cryptography.Decrypt(idProvEnc);

            if (int.TryParse(aux1.ToString(), out idPedido) && int.TryParse(aux2.ToString(), out idProveedor))
            {
                if (idPedido > 0 && idProveedor > 0)
                {
                    if (cambiarEstado(idPedido, idProveedor))
                    {
                        enviarMailConfirmado(idPedido, idProveedor);
                        litId.Text = idPedido.ToString();
                    }
                    else
                        hConfirmada.Visible = false;
                }
                else
                    Response.Redirect("traslados-paso1.aspx");
            }
        }
    }

    private bool cambiarEstado(int idPedido, int idProveedor)
    {
        bool result = false;
        using (var dbContext = new ACHEEntities())
        {
            try
            {
                var pedido = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == idPedido && x.Servicios.IDProveedor == idProveedor).FirstOrDefault();
                if (pedido != null)
                {
                    if (pedido.Estado== "Pendiente")
                    {
                        pedido.Estado = "Aceptado";
                        dbContext.SaveChanges();
                        result = true;
                    }
                    else
                        throw new Exception("La reserva ya estaba confirmada o cancelada");
                }
                else
                    throw new Exception("No se ha encontrado la reserva");
            }
            catch (Exception ex)
            {
                showError(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        return result;
    }

    private void enviarMailConfirmado(int idPedido, int idProveedor)
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
                    .Where(x => x.IDPedidoTraslado == idPedido && x.Servicios.IDProveedor == idProveedor).FirstOrDefault();
                if (pedido != null)
                {
                    ListDictionary replacements = new ListDictionary();

                    string textoTraslados = string.Empty;
                    var parametros = dbContext.Parametros.FirstOrDefault();
                    if (parametros != null)
                        textoTraslados = parametros.TextoTraslados;

                    replacements.Add("<FECHAGENERACION>", DateTime.Now.ToString("dd/MM/yyyy"));
                    replacements.Add("<ID>", pedido.IDPedidoTraslado);              
                    replacements.Add("<EMPRESA>", pedido.Usuarios.Empresa);
                    replacements.Add("<USUARIOOPERADOR>", pedido.Usuarios.Empresa);
                    replacements.Add("<EMAILOPERADOR>", pedido.Usuarios.Email);
                    replacements.Add("<FECHARESERVA>", pedido.FechaAlta.ToString("dd/MM/yyyy"));
                    replacements.Add("<PROVEEDOR>", pedido.Servicios.Proveedores.Nombre);
                    replacements.Add("<LOGO>", pedido.Servicios.Proveedores.Logo);
                    replacements.Add("<SERVICIO>", pedido.Servicios.Nombre);
                    replacements.Add("<NROFILE>", pedido.NroFile);
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
                    replacements.Add("<TOTAL>", pedido.Total.ToString("N2"));

                    if (pedido.Servicios.SubTipos.Tipo == "R")
                    {
                        replacements.Add("<TIPOTRASLADO>", "Ida/Vuelta");
                        replacements.Add("<TIPOSERVICIO>", "Round Trip");
                        replacements.Add("<FECHAIDA>", "Fecha ida: " + pedido.FechaIda.Value.ToString("dd/MM/yyyy"));
                        replacements.Add("<FECHAVUELTA>", "Fecha vuelta: " + pedido.FechaVuelta.Value.ToString("dd/MM/yyyy"));
                        replacements.Add("<TITULOIDA>", "IDA");
                        replacements.Add("<TITULOVUELTA>", "VUELTA");

                    }
                    else if (pedido.FechaIda.HasValue)
                    {
                        replacements.Add("<TIPOTRASLADO>", "solo Ida");
                        replacements.Add("<TIPOSERVICIO>", "one way");
                        replacements.Add("<FECHAIDA>", "Fecha ida: " + pedido.FechaIda.Value.ToString("dd/MM/yyyy"));
                        replacements.Add("<TITULOIDA>","IDA");
                    }
                    else
                    {
                        replacements.Add("<TIPOTRASLADO>", "solo Vuelta");
                        replacements.Add("<TIPOSERVICIO>", "one way");
                        replacements.Add("<FECHAVUELTA>", "Fecha vuelta: " + pedido.FechaVuelta.Value.ToString("dd/MM/yyyy"));
                        replacements.Add("<TITULOVUELTA>", "VUELTA");

                    }
                    if (pedido.IDLugarOrigen != null && pedido.IDLugarOrigen > 0)
                    {
                        replacements.Add("<AEROPUERTOIDA>", "Aeropuerto ida: " + pedido.LugaresTrasladosOrigen.Nombre);
                        replacements.Add("<COMPANIAIDA>", "Compañia aérea  ida: " + pedido.AerolineaArribo);
                        replacements.Add("<NROVUELOIDA>", "Nro de vuelo ida: " + pedido.VueloArribo);
                        replacements.Add("<HORAIDA>", "Hora de llegada del aéreo: " + pedido.HoraArribo);

                    }
                    if (pedido.IDLugarDestino != null && pedido.IDLugarDestino > 0)
                    {
                        replacements.Add("<AEROPUERTOVUELTA>", "Aeropuerto vuelta: " + pedido.LugaresTrasladosDestino.Nombre);
                        replacements.Add("<COMPANIAVUELTA>", "Compañia aérea partida: " + pedido.AerolineaPartida);
                        replacements.Add("<NROVUELOVUELTA>", "Nro de vuelo vuelta: " + pedido.VueloPartida);
                        replacements.Add("<HORAVUELTA>", "Hora de salida del aéreo: " + pedido.HoraPartida);

                    }
                    if (pedido.Hotel2 != null && pedido.Hotel2 != "")
                    {
                        if (pedido.Hotel1 != null && pedido.Hotel1 != "")
                        {
                            replacements.Add("<HOTELIDA1>", "Desde hotel: " + pedido.Hotel1);
                            replacements.Add("<DIRECCIONIDA1>", "Direccion hotel ida: " + pedido.DireccionHotel1);

                        }
                        replacements.Add("<HOTELIDA2>", "Al hotel: " + pedido.Hotel2);
                        replacements.Add("<DIRECCIONIDA2>", "Direccion hotel ida 2: " + pedido.DireccionHotel2);
                    }
                    else
                    {
                        if (pedido.Hotel1 != null && pedido.Hotel1 != "")
                        {
                            replacements.Add("<HOTELIDA1>", "Hotel ida: " + pedido.Hotel1);
                            replacements.Add("<DIRECCIONIDA1>", "Direccion hotel ida: " + pedido.DireccionHotel1);
                        }
                    }

                    if (pedido.Hotel4 != null && pedido.Hotel4 != "")
                    {
                        if (pedido.Hotel3 != null && pedido.Hotel3 != "")
                        {
                            replacements.Add("<HOTELVUELTA1>", "Desde hotel: " + pedido.Hotel3);
                            replacements.Add("<DIRECCIONVUELTA1>", "Direccion hotel vuelta: " + pedido.DireccionHotel3);

                        }
                        replacements.Add("<HOTELVUELTA2>", "Al hotel: " + pedido.Hotel4);
                        replacements.Add("<DIRECCIONVUELTA2>", "Direccion hotel vuelta 2: " + pedido.DireccionHotel4);
                    }
                    else
                    {
                        if (pedido.Hotel3 != null && pedido.Hotel3 != "")
                        {
                            replacements.Add("<DIRECCIONVUELTA1>", "Direccion hotel vuelta: " + pedido.DireccionHotel3);

                            replacements.Add("<HOTELVUELTA1>", "Hotel vuelta: " + pedido.Hotel3);
                        }
                    }

                    bool emailOperador = false;
                    //bool emailOperador2 = false;
                  
                    emailOperador = EmailHelper.SendMessage(EmailTemplate.ReservaTransporteInternoOp, replacements, pedido.Usuarios.Email, "TRASLADOS RED: Reserva Confirmada #" + idPedido);
                    //emailOperador2 = EmailHelper.SendMessage(EmailTemplate.ReservaTransporteInternoOp, replacements, "traslados@rturistica.com", "TRASLADOS RED: Reserva Confirmada #" + idPedido);
                    if (!emailOperador)
                        throw new Exception("Ha ocurrido un error al enviar email de confirmación, por favor contáctese con la empresa.<br /><br />");
                    else
                        lblError.Visible = false;
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