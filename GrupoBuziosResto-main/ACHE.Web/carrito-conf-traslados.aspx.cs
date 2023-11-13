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
using System.Data.Entity.Validation;

public partial class carrito_conf_traslados : WebBasePage
{

    public int IdPedido = 0;
    private string Mode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentUser == null)
                Response.Redirect("/login-traslados.aspx");
            
            if (TrasladosCart.RetrieveTrasladosCart().Total > 0)
                LoadInfo();
            else
                Response.Redirect("/traslados-paso1.aspx");
        }
    }

    private void LoadInfo()
    {
        string origen = string.Empty;
        string destino = string.Empty;
        string NombreServicio = string.Empty;
        bool servicioEspecial = false;
        using (var dbContext = new ACHEEntities())
        {
            if (TrasladosCart.RetrieveTrasladosCart().IDAeropuertoOrigen.HasValue && TrasladosCart.RetrieveTrasladosCart().IDAeropuertoOrigen > 0)
            {
                int aeropuertoOrigen = TrasladosCart.RetrieveTrasladosCart().IDAeropuertoOrigen ?? 0;
                origen = dbContext.LugaresTraslados.Where(x => x.IDLugarTraslado == aeropuertoOrigen).FirstOrDefault().Nombre;
            }

            if (TrasladosCart.RetrieveTrasladosCart().IDAeropuertoDestino.HasValue && TrasladosCart.RetrieveTrasladosCart().IDAeropuertoDestino > 0)
            {
                int idAeropuertoDestino = TrasladosCart.RetrieveTrasladosCart().IDAeropuertoDestino ?? 0;
                destino = dbContext.LugaresTraslados.Where(x => x.IDLugarTraslado == idAeropuertoDestino).FirstOrDefault().Nombre;
            }
            int idServicio = TrasladosCart.RetrieveTrasladosCart().IDServicio;
            NombreServicio = dbContext.Servicios.Where(x => x.IDServicio == idServicio).FirstOrDefault().Nombre;

           servicioEspecial= dbContext.Usuarios.Where(x => x.IDUsuario == CurrentUser.ID).FirstOrDefault().ServiciosEspeciales;

        }
        #region HTML
        string html = string.Empty;

        lnkVolver.NavigateUrl = "/traslados-paso1.aspx?Id=" + IdPedido;
        litServicio.Text = NombreServicio;

        html += "<h4>Tipo Servicio: " + (TrasladosCart.RetrieveTrasladosCart().TipoServicio == "P" ? "Privado" : "Regular") + "</h4>";
        if (TrasladosCart.RetrieveTrasladosCart().NroFile != "")
            html += "<h4>Nro file: " + TrasladosCart.RetrieveTrasladosCart().NroFile + "</h4>";
        if (servicioEspecial) {
            if (TrasladosCart.RetrieveTrasladosCart().PagoPor != "")
                html += "<h4>Pago por: " + TrasladosCart.RetrieveTrasladosCart().PagoPor + "</h4>";
        }
        html += "<hr class='separator' />";
        if (TrasladosCart.RetrieveTrasladosCart().TipoServicio == "R")
        {
            html += "<h4>Tipo Traslado: Ida y Vuelta </h4>";
        }
        else
        {
            if (TrasladosCart.RetrieveTrasladosCart().FechaIda.HasValue)
                html += "<h4>Tipo Traslado: Sólo ida </h4>";
            else
                html += "<h4>Tipo Traslado: Sólo vuelta </h4>";
        }
        html += "<hr class='separator' />";
        if (TrasladosCart.RetrieveTrasladosCart().FechaIda.HasValue)
        {

            html += "<h4>Fecha Ida: " + TrasladosCart.RetrieveTrasladosCart().FechaIda.Value.ToString("dd/MM/yyyy") + "</h4>";
        }
        if (origen != string.Empty)
            html += "<h4>Aeropuerto ida: " + origen + "</h4>";

        if (TrasladosCart.RetrieveTrasladosCart().AerolineaArribo != null && !string.IsNullOrEmpty(TrasladosCart.RetrieveTrasladosCart().AerolineaArribo))
            html += "<h4>Compañia aérea ida: " + TrasladosCart.RetrieveTrasladosCart().AerolineaArribo + "</h4>";

        if (TrasladosCart.RetrieveTrasladosCart().VueloArribo != null && !string.IsNullOrEmpty(TrasladosCart.RetrieveTrasladosCart().VueloArribo))
            html += "<h4>Nro Vuelo ida: " + TrasladosCart.RetrieveTrasladosCart().VueloArribo + "</h4>";

        bool adicional = false;
        if (TrasladosCart.RetrieveTrasladosCart().HoraArribo != null && TrasladosCart.RetrieveTrasladosCart().HoraArribo != string.Empty && TrasladosCart.RetrieveTrasladosCart().HoraArribo.Trim() != ":")
        {
            html += "<h4>Hora de llegada: " + TrasladosCart.RetrieveTrasladosCart().HoraArribo + "</h4>";
            adicional = (int.Parse(TrasladosCart.RetrieveTrasladosCart().HoraArribo.Split(":")[0]) >= 1 || int.Parse(TrasladosCart.RetrieveTrasladosCart().HoraArribo.Split(":")[0]) <= 4);
        }


        if (TrasladosCart.RetrieveTrasladosCart().Hotel2 != null)
        {
            html += "<h4>Desde Hotel : " + TrasladosCart.RetrieveTrasladosCart().Hotel1 + "</h4>";
            html += "<h4>Al hotel" + TrasladosCart.RetrieveTrasladosCart().Hotel2 + "</h4>";
        }
        else if (TrasladosCart.RetrieveTrasladosCart().Hotel1 != null)
            html += "<h4>Hotel ida: " + TrasladosCart.RetrieveTrasladosCart().Hotel1 + "</h4>";

        html += "<div class='clearfix'></div>";

        if (TrasladosCart.RetrieveTrasladosCart().FechaVuelta.HasValue)
        {
            if (TrasladosCart.RetrieveTrasladosCart().FechaIda.HasValue)
                html += "<hr class='separator' />";
            html += "<h4>Fecha Vuelta: " + TrasladosCart.RetrieveTrasladosCart().FechaVuelta.Value.ToString("dd/MM/yyyy") + "</h4>";
        }

        if (destino != string.Empty)
            html += "<h4>Aeropuerto vuelta: " + destino + "</h4>";

        if (TrasladosCart.RetrieveTrasladosCart().AerolineaPartida != null && (TrasladosCart.RetrieveTrasladosCart().AerolineaPartida != ""))
            html += "<h4>Compañia aérea vuelta: " + TrasladosCart.RetrieveTrasladosCart().AerolineaPartida + "</h4>";

        if (TrasladosCart.RetrieveTrasladosCart().VueloPartida != null && !string.IsNullOrEmpty(TrasladosCart.RetrieveTrasladosCart().VueloPartida))
            html += "<h4>Nro Vuelo vuelta: " + TrasladosCart.RetrieveTrasladosCart().VueloPartida + "</h4>";

        if (TrasladosCart.RetrieveTrasladosCart().HoraPartida != null && TrasladosCart.RetrieveTrasladosCart().HoraPartida != string.Empty && TrasladosCart.RetrieveTrasladosCart().HoraPartida.Trim() != ":")
            html += "<h4>Hora de salida: " + TrasladosCart.RetrieveTrasladosCart().HoraPartida + "</h4>";



        if (TrasladosCart.RetrieveTrasladosCart().Hotel4 != null)
        {
            if (TrasladosCart.RetrieveTrasladosCart().Hotel3 != null)
                html += "<h4>Desde Hotel : " + TrasladosCart.RetrieveTrasladosCart().Hotel3 + "</h4>";
            html += "<h4>Al Hotel : " + TrasladosCart.RetrieveTrasladosCart().Hotel4 + "</h4>";
        }
        else if (TrasladosCart.RetrieveTrasladosCart().Hotel3 != null)
            html += "<h4>Desde Hotel : " + TrasladosCart.RetrieveTrasladosCart().Hotel3 + "</h4>";

        html += "<div class='clearfix'></div>";
        html += "<h4>Cant. Personas: " + (TrasladosCart.RetrieveTrasladosCart().CantAdultos + TrasladosCart.RetrieveTrasladosCart().Cantmenores + TrasladosCart.RetrieveTrasladosCart().Cantmenores2) + "</h4>";
        html += "<div class='clearfix'></div>";



        html += "<div class='clearfix'></div>";

        html += "<div class='clearfix'></div>";

        html += "<h4>Total: <b>$" + TrasladosCart.RetrieveTrasladosCart().Total.ToString("N2") + "</b></h4>";
    /*    if (adicional)
            html += "<p style='color:#4b1534'>Incluye adicional nocturno (USD " + this.Session["PrecioNocturno"] + ".- por pax)</p>";*/

        if (!string.IsNullOrEmpty(TrasladosCart.RetrieveTrasladosCart().Observaciones))
            html += "<div class='clearfix'></div><h4>Observaciones: <br /><br />" + TrasladosCart.RetrieveTrasladosCart().Observaciones + "</h4>";




        litDatos.Text = html;
        #endregion




        /*
        using (var dbContext = new ACHEEntities())
        {
            var pedido = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == IdPedido).FirstOrDefault();
            if (pedido != null)
            {
                if(pedido.IDLugarOrigen.HasValue && pedido.IDLugarOrigen>0)
                    origen = dbContext.LugaresTraslados.Where(x => x.IDLugarTraslado == pedido.IDLugarOrigen).FirstOrDefault().Nombre;
                if(pedido.IDLugarDestino.HasValue && pedido.IDLugarDestino>0)
                    destino = dbContext.LugaresTraslados.Where(x => x.IDLugarTraslado == pedido.IDLugarDestino).FirstOrDefault().Nombre;

                #region HTML
                string html = string.Empty;

                lnkVolver.NavigateUrl = "/traslados-paso1.aspx?Id=" + IdPedido;
                litServicio.Text = pedido.Servicios.Nombre;

                html += "<h4>Tipo Servicio: " + (pedido.TipoServicio == "P" ? "Privado" : "Regular") + "</h4>";
                html += "<hr class='separator' />";
                if (pedido.Servicios.SubTipos.Tipo == "R")
                {
                    html += "<h4>Tipo Traslado: Ida y Vuelta </h4>";
                }
                else
                {
                    if (pedido.FechaIda.HasValue)
                        html += "<h4>Tipo Traslado: Sólo ida </h4>";
                    else
                        html += "<h4>Tipo Traslado: Sólo vuelta </h4>";
                }
                if (pedido.FechaIda.HasValue)
                    html += "<h4>Fecha Ida: " + pedido.FechaIda.Value.ToString("dd/MM/yyyy") + "</h4>";
                if (pedido.FechaVuelta.HasValue)
                    html += "<h4>Fecha Vuelta: " + pedido.FechaVuelta.Value.ToString("dd/MM/yyyy") + "</h4>";
                if (origen!=string.Empty)
                    html += "<h4>Origen: " + origen + "</h4>";
                if (destino!=string.Empty)
                    html += "<h4>Destino: " + destino + "</h4>";
                if(pedido.Hotel1!=null)
                    html += "<h4>Hotel Origen: " + pedido.Hotel1 + "</h4>";
                if (pedido.Hotel3 != null)
                    html += "<h4>Hotel Destino: " + pedido.Hotel3 + "</h4>";
                html += "<h4>Cant. Personas: " + (pedido.CantAdultos + pedido.CantMenoresAsiento + pedido.CantMenoresGratis) + "</h4>";
                html += "<div class='clearfix'></div>";

                if (pedido.AerolineaArribo != null &&  !string.IsNullOrEmpty(pedido.AerolineaArribo))
                    html += "<h4>Aerolinea Arribo: " + pedido.AerolineaArribo + "</h4>";

                if (pedido.VueloArribo!=null && !string.IsNullOrEmpty(pedido.VueloArribo))
                    html += "<h4>Vuelo Arribo: " + pedido.VueloArribo + "</h4>";

                bool adicional = false;
                if (pedido.HoraArribo != null && pedido.HoraArribo != string.Empty && pedido.HoraArribo.Trim() != ":")
                {
                    html += "<h4>Hora Arribo: " + pedido.HoraArribo + "</h4><div class='clearfix'></div>";
                    adicional = (int.Parse(pedido.HoraArribo.Split(":")[0]) >= 21 || int.Parse(pedido.HoraArribo.Split(":")[0]) <= 4);
                }

                if (pedido.AerolineaPartida !=null &&!string.IsNullOrEmpty(pedido.AerolineaPartida))
                    html += "<h4>Aerolinea Partida: " + pedido.AerolineaPartida + "</h4>";

                if (pedido.VueloPartida!=null && !string.IsNullOrEmpty(pedido.VueloPartida))
                    html += "<h4>Vuelo Partida: " + pedido.VueloPartida + "</h4>";

                if (pedido.HoraPartida!=null && pedido.HoraPartida != string.Empty && pedido.HoraPartida.Trim() != ":")
                    html += "<h4>Hora Partida: " + pedido.HoraPartida + "</h4><div class='clearfix'></div>";
                html += "<div class='clearfix'></div>";

                html += "<div class='clearfix'></div>";

                html += "<h4>Total: <b>$" + pedido.Total.ToString("N2") + "</b></h4>";
                if (adicional)
                    html += "<p style='color:#4b1534'>Incluye adicional nocturno (USD 37.- por pax)</p>";

                if (!string.IsNullOrEmpty(pedido.Observaciones))
                    html += "<div class='clearfix'></div><h4>Observaciones: <br /><br />" + pedido.Observaciones+ "</h4>";
                litDatos.Text = html;
                #endregion
            }
        }
         * */
    }

    private void showError(string exception)
    {
        lblError.Visible = true;
        lblError.Text = exception;
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        try
        {
            using (var dbContext = new ACHEEntities())
            {
                PedidosTraslado pedido;
                if (TrasladosCart.RetrieveTrasladosCart().IDPedido.HasValue && TrasladosCart.RetrieveTrasladosCart().IDPedido > 0)
                {
                    IdPedido = TrasladosCart.RetrieveTrasladosCart().IDPedido ?? 0;
                    pedido = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == IdPedido).FirstOrDefault();
                }
                else
                {
                    pedido = new PedidosTraslado();
                    pedido.FechaAlta = DateTime.Now;
                }
                //Paso 1
                pedido.IDUsuario = TrasladosCart.RetrieveTrasladosCart().IDUsuario;
                pedido.IDServicio = TrasladosCart.RetrieveTrasladosCart().IDServicio;
                int idserv = TrasladosCart.RetrieveTrasladosCart().IDServicio;
                var serv = dbContext.Servicios.Where(x => x.IDServicio == idserv).FirstOrDefault();
                pedido.Servicios = serv;
                pedido.CantAdultos = TrasladosCart.RetrieveTrasladosCart().CantAdultos;
                pedido.CantMenoresAsiento = TrasladosCart.RetrieveTrasladosCart().Cantmenores;
                pedido.CantMenoresGratis = TrasladosCart.RetrieveTrasladosCart().Cantmenores2;
                int idusu = TrasladosCart.RetrieveTrasladosCart().IDUsuario;

                var usu = dbContext.Usuarios.Where(x => x.IDUsuario == idusu).FirstOrDefault();
                pedido.Usuarios = usu;
                //Paso 2
                pedido.NroFile = TrasladosCart.RetrieveTrasladosCart().NroFile;
                if (TrasladosCart.RetrieveTrasladosCart().Items.Count() > 0)
                {
                    if (IdPedido > 0)
                    {
                        var pasajeros = dbContext.PasajerosPorPedidoTraslado.Where(x => x.IDPedidoTraslado == IdPedido);
                        foreach (var pasajero in pasajeros)
                        {
                            dbContext.PasajerosPorPedidoTraslado.Remove(pasajero);
                        }
                    }
                    foreach (var item in TrasladosCart.RetrieveTrasladosCart().Items)
                    {
                        PasajerosPorPedidoTraslado pasajero = new PasajerosPorPedidoTraslado();
                        pasajero.Nombre = item.Nombre;
                        pasajero.DNI = item.DNI;
                        pasajero.IDPedidoTraslado = pedido.IDPedidoTraslado;
                        dbContext.PasajerosPorPedidoTraslado.Add(pasajero);
                    }
                }
                //Paso 3
                pedido.FechaIda = TrasladosCart.RetrieveTrasladosCart().FechaIda;
                pedido.FechaVuelta = TrasladosCart.RetrieveTrasladosCart().FechaVuelta;
                pedido.TipoServicio = TrasladosCart.RetrieveTrasladosCart().TipoServicio;
                pedido.Total = TrasladosCart.RetrieveTrasladosCart().Total;
                pedido.AerolineaArribo = TrasladosCart.RetrieveTrasladosCart().AerolineaArribo;
                pedido.VueloArribo = TrasladosCart.RetrieveTrasladosCart().VueloArribo;
                pedido.HoraArribo = TrasladosCart.RetrieveTrasladosCart().HoraArribo;
                pedido.AerolineaPartida = TrasladosCart.RetrieveTrasladosCart().AerolineaPartida;
                pedido.IDLugarOrigen = TrasladosCart.RetrieveTrasladosCart().IDAeropuertoOrigen;
                int idLugarOrigen = TrasladosCart.RetrieveTrasladosCart().IDAeropuertoOrigen ?? 0;
                var lugar = dbContext.LugaresTraslados.Where(x => x.IDLugarTraslado == idLugarOrigen).FirstOrDefault();
                if (lugar != null)
                    pedido.LugaresTrasladosOrigen = lugar;

                pedido.IDLugarDestino = TrasladosCart.RetrieveTrasladosCart().IDAeropuertoDestino;
                int idAeropuertodest = TrasladosCart.RetrieveTrasladosCart().IDAeropuertoDestino ?? 0;
                var lugardest = dbContext.LugaresTraslados.Where(x => x.IDLugarTraslado == idAeropuertodest).FirstOrDefault();
                if (lugardest != null)
                    pedido.LugaresTrasladosDestino = lugardest;

                pedido.VueloPartida = TrasladosCart.RetrieveTrasladosCart().VueloPartida;
                pedido.HoraPartida = TrasladosCart.RetrieveTrasladosCart().HoraPartida;
                pedido.Observaciones = TrasladosCart.RetrieveTrasladosCart().Observaciones;
                pedido.Precio = TrasladosCart.RetrieveTrasladosCart().Precio;
                pedido.DireccionHotel1 = TrasladosCart.RetrieveTrasladosCart().DireccionHotel1;
                pedido.DireccionHotel2 = TrasladosCart.RetrieveTrasladosCart().DireccionHotel2;
                pedido.DireccionHotel3 = TrasladosCart.RetrieveTrasladosCart().DireccionHotel3;
                pedido.DireccionHotel4 = TrasladosCart.RetrieveTrasladosCart().DireccionHotel4;
                pedido.Hotel1 = TrasladosCart.RetrieveTrasladosCart().Hotel1;
                pedido.Hotel2 = TrasladosCart.RetrieveTrasladosCart().Hotel2;
                pedido.Hotel3 = TrasladosCart.RetrieveTrasladosCart().Hotel3;
                pedido.Hotel4 = TrasladosCart.RetrieveTrasladosCart().Hotel4;
                pedido.Estado = "Pendiente";
                pedido.PagoPor = TrasladosCart.RetrieveTrasladosCart().PagoPor;
                bool send = false;
                if (IdPedido > 0)
                {
                    dbContext.SaveChanges();
                    //     HttpContext.Current.Session["ASPNETTraslados"] = null;
                    send = EnviarMailPedidoTraslado(pedido, CurrentUser.Nombre, CurrentUser.Email, false);
                }
                else
                {
                    dbContext.PedidosTraslado.Add(pedido);
                    dbContext.SaveChanges();
                    send = EnviarMailPedidoTraslado(pedido, CurrentUser.Nombre, CurrentUser.Email, true);

                }

                if (send)
                {
                    //pedido.Estado = "Pendiente";
                    dbContext.SaveChanges();
                    HttpContext.Current.Session["ASPNETTraslados"] = null;
                    Response.Redirect("carrito-fin-traslados.aspx?Id=" + pedido.IDPedidoTraslado);
                }

                //  string url = "carrito-fin-traslados.aspx?IdPedido=" + pedido.IDPedidoTraslado;
                // Response.Redirect(url);
            }
        }
        catch (DbEntityValidationException ex)
        {
            foreach (var eve in ex.EntityValidationErrors)
            {
                foreach (var ve in eve.ValidationErrors)
                {
                    Response.Write(string.Format("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                        ve.PropertyName,
                        eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                        ve.ErrorMessage));
                }
            }

        }
    }

    private bool EnviarMailPedidoTraslado(PedidosTraslado pedido, string nombreUsuario, string emailUsuario, bool Nueva)
    {
        bool send = false;
        try
        {

            ListDictionary replacements = new ListDictionary();
            string idPedidoEncriptado = Cryptography.Encrypt(pedido.IDPedidoTraslado);

            string idProveedorEncriptado = Cryptography.Encrypt(pedido.Servicios.IDProveedor.ToString());

            replacements.Add("<FECHAGENERACION>", DateTime.Now.ToString("dd/MM/yyyy"));

            replacements.Add("<ID>", pedido.IDPedidoTraslado);
            replacements.Add("<IDPEDIDO>", idPedidoEncriptado);
            replacements.Add("<IDPROVEEDOR>", idProveedorEncriptado);
            replacements.Add("<EMPRESA>", pedido.Usuarios.Empresa);
            replacements.Add("<USUARIOOPERADOR>", nombreUsuario);
            replacements.Add("<EMAILOPERADOR>", pedido.Usuarios.Email);
            replacements.Add("<FECHARESERVA>", pedido.FechaAlta.ToString("dd/MM/yyyy"));

            replacements.Add("<PROVEEDOR>", pedido.Servicios.Proveedores.Nombre);
            replacements.Add("<LOGO>", pedido.Servicios.Proveedores.Logo);
            replacements.Add("<SERVICIO>", pedido.Servicios.Nombre);

            replacements.Add("<ESTADO>", "Pendiente");

            replacements.Add("<NROFILE>", pedido.NroFile);
            replacements.Add("<CANTADULTOS>", pedido.CantAdultos);
            replacements.Add("<CANTMENORES1>", pedido.CantMenoresAsiento);
            replacements.Add("<CANTMENORES2>", pedido.CantMenoresGratis);

            var htmlinfoPasajeros = "<table> <tr><th>Nombre</th><th>DNI</th></tr>";
            foreach (var pasajero in pedido.PasajerosPorPedidoTraslado)
            {
                htmlinfoPasajeros += "<tr>";
                htmlinfoPasajeros += "<td>" + pasajero.Nombre + "</td>";
                htmlinfoPasajeros += "<td>" + pasajero.DNI + "</td>";
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
                replacements.Add("<TITULOIDA>", "IDA");

                replacements.Add("<FECHAIDA>", "Fecha ida: " + pedido.FechaIda.Value.ToString("dd/MM/yyyy"));
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



            //bool send = false;
            if (Nueva)
            {
                send = EmailHelper.SendMessage(EmailTemplate.ReservaTransporteInternoProv, replacements, pedido.Servicios.Proveedores.Email, "TRASLADOS RED: Nueva Reserva #" + pedido.IDPedidoTraslado);
            }
            else
                send = EmailHelper.SendMessage(EmailTemplate.ReservaTransporteInternoProv, replacements, pedido.Servicios.Proveedores.Email, "TRASLADOS RED: Modificación Reserva #" + pedido.IDPedidoTraslado);
            if (!send)
            {
                // throw new Exception("Ha ocurrido un error al enviar el email, por favor intente nuevamente.<br /><br />");
                lblError.Text = "Ha ocurrido un error al enviar el email, por favor intente nuevamente";
                lblError.Visible = true;
            }
            else
            {
                //  HttpContext.Current.Session["ASPNETTraslados"] = null;
                lblError.Visible = false;
                //   Response.Redirect("carrito-fin-traslados.aspx?Id="+pedido.IDPedidoTraslado);
            }

        }
        catch (Exception e)
        {
            lblError.Text = e.Message;
            lblError.Visible = true;
            // showError(e.Message);
        }
        return send;
    }

}