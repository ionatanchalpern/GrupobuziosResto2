using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Configuration;
using ACHE.Model;
using ACHE.Extensions;
using System.Web.Services;

public partial class traslados_paso3 : WebBasePage
{

    private int idPedido;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (CurrentUser == null)
                Response.Redirect("/login-traslados.aspx");

            this.idPedido = Request.QueryString["Id"] != null ? int.Parse(Request.QueryString["Id"]) : 0; 
            LoadInfo(this.idPedido);
           // if (this.idPedido > 0)
            LoadInfoPedido(this.idPedido);
           
        }
    }
    
    private void LoadInfoPedido(int id)
    {
        if (TrasladosCart.RetrieveTrasladosCart().FechaIda.HasValue && TrasladosCart.RetrieveTrasladosCart().FechaVuelta.HasValue)
        {
            hdnIDPedido.Value = idPedido.ToString();
            if (TrasladosCart.RetrieveTrasladosCart().FechaIda.HasValue)
                txtFechaIda.Text = TrasladosCart.RetrieveTrasladosCart().FechaIda.Value.ToString("dd/MM/yyyy");
            if (TrasladosCart.RetrieveTrasladosCart().FechaVuelta.HasValue)
                txtFechaVuelta.Text = TrasladosCart.RetrieveTrasladosCart().FechaVuelta.Value.ToString("dd/MM/yyyy");
            if (TrasladosCart.RetrieveTrasladosCart().IDAeropuertoOrigen != null && TrasladosCart.RetrieveTrasladosCart().IDAeropuertoOrigen > 0)
            {
                cmbAeropuertoIda.SelectedValue = TrasladosCart.RetrieveTrasladosCart().IDAeropuertoOrigen.ToString();
                txtCompaniaAerea.Text = TrasladosCart.RetrieveTrasladosCart().AerolineaArribo;
                txtNroVuelo.Text = TrasladosCart.RetrieveTrasladosCart().VueloArribo;
                string horaA = TrasladosCart.RetrieveTrasladosCart().HoraArribo.Split(":")[0];
                string minA = TrasladosCart.RetrieveTrasladosCart().HoraArribo.Split(":")[1];
                cmbHoraIda.SelectedValue = horaA;
                cmbMinutosIda.SelectedValue = minA;
            }
            if (TrasladosCart.RetrieveTrasladosCart().IDAeropuertoDestino != null && TrasladosCart.RetrieveTrasladosCart().IDAeropuertoDestino > 0)
            {
                cmbAeropuertoVuelta.SelectedValue = TrasladosCart.RetrieveTrasladosCart().IDAeropuertoDestino.ToString();
                txtCompaniaVuelta.Text = TrasladosCart.RetrieveTrasladosCart().AerolineaPartida;
                txtNroVueloVuelta.Text = TrasladosCart.RetrieveTrasladosCart().VueloPartida;
                string horaA = TrasladosCart.RetrieveTrasladosCart().HoraPartida.Split(":")[0];
                string minA = TrasladosCart.RetrieveTrasladosCart().HoraPartida.Split(":")[1];
                cmbHoraVuelta.SelectedValue = horaA;
                cmbMinutosVuelta.SelectedValue = minA;
            }
            if (TrasladosCart.RetrieveTrasladosCart().Hotel1 != null && TrasladosCart.RetrieveTrasladosCart().Hotel1 != "")
            {
                txtNombreHotelIda.Text = TrasladosCart.RetrieveTrasladosCart().Hotel1;
                txtDireccionHotelIda.Text = TrasladosCart.RetrieveTrasladosCart().DireccionHotel1;
            }

            if (TrasladosCart.RetrieveTrasladosCart().Hotel2 != null && TrasladosCart.RetrieveTrasladosCart().Hotel2 != "")
            {
                txtNombreHotelIda2.Text = TrasladosCart.RetrieveTrasladosCart().Hotel2;
                txtDireccionHotelIda2.Text = TrasladosCart.RetrieveTrasladosCart().DireccionHotel2;
            }
            if (TrasladosCart.RetrieveTrasladosCart().Hotel3 != null && TrasladosCart.RetrieveTrasladosCart().Hotel3 != "")
            {
                txtNombreHotelVuelta.Text = TrasladosCart.RetrieveTrasladosCart().Hotel3;
                txtDireccionHotelVuelta.Text = TrasladosCart.RetrieveTrasladosCart().DireccionHotel3;
            }
            if (TrasladosCart.RetrieveTrasladosCart().Hotel4 != null && TrasladosCart.RetrieveTrasladosCart().Hotel4 != "")
            {
                txtNombreHotelVuelta2.Text = TrasladosCart.RetrieveTrasladosCart().Hotel4;
                txtDireccionHotelVuelta2.Text = TrasladosCart.RetrieveTrasladosCart().DireccionHotel4;
            }

            txtObservaciones.Text = TrasladosCart.RetrieveTrasladosCart().Observaciones;

         }else if (id > 0){
                using (var dbContext = new ACHEEntities())
                {
                    var pedido = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == id).FirstOrDefault();
                    if (pedido != null)
                    {
                        hdnIDPedido.Value = idPedido.ToString();
                        if (pedido.FechaIda.HasValue)
                            txtFechaIda.Text = pedido.FechaIda.Value.ToString("dd/MM/yyyy");
                        if (pedido.FechaVuelta.HasValue)
                            txtFechaVuelta.Text = pedido.FechaVuelta.Value.ToString("dd/MM/yyyy");
                        if (pedido.IDLugarOrigen != null && pedido.IDLugarOrigen > 0)
                        {
                            cmbAeropuertoIda.SelectedValue = pedido.IDLugarOrigen.ToString();
                            txtCompaniaAerea.Text = pedido.AerolineaArribo;
                            txtNroVuelo.Text = pedido.VueloArribo;
                            string horaA = pedido.HoraArribo.Split(":")[0];
                            string minA = pedido.HoraArribo.Split(":")[1];
                            cmbHoraIda.SelectedValue = horaA;
                            cmbMinutosIda.SelectedValue = minA;
                        }
                        if (pedido.IDLugarDestino != null && pedido.IDLugarDestino > 0)
                        {
                            cmbAeropuertoVuelta.SelectedValue = pedido.IDLugarDestino.ToString();
                            txtCompaniaVuelta.Text = pedido.AerolineaPartida;
                            txtNroVueloVuelta.Text = pedido.VueloPartida;
                            string horaA = pedido.HoraPartida.Split(":")[0];
                            string minA = pedido.HoraPartida.Split(":")[1];
                            cmbHoraVuelta.SelectedValue = horaA;
                            cmbMinutosVuelta.SelectedValue = minA;
                        }
                        if (pedido.Hotel1 != null && pedido.Hotel1 != "")
                        {
                            txtNombreHotelIda.Text = pedido.Hotel1;
                            txtDireccionHotelIda.Text = pedido.DireccionHotel1;
                        }

                        if (pedido.Hotel2 != null && pedido.Hotel2 != "")
                        {
                            txtNombreHotelIda2.Text = pedido.Hotel2;
                            txtDireccionHotelIda2.Text = pedido.DireccionHotel2;
                        }
                        if (pedido.Hotel3 != null && pedido.Hotel3 != "")
                        {
                            txtNombreHotelVuelta.Text = pedido.Hotel3;
                            txtDireccionHotelVuelta.Text = pedido.DireccionHotel3;
                        }
                        if (pedido.Hotel4 != null && pedido.Hotel4 != "")
                        {
                            txtNombreHotelVuelta2.Text = pedido.Hotel4;
                            txtDireccionHotelVuelta2.Text = pedido.DireccionHotel4;
                        }

                        txtObservaciones.Text = pedido.Observaciones; 
                }
            }
        }
    }
    
    private void LoadInfo(int id)
    {
        int idServicio;
      
        using (var dbContext = new ACHEEntities())
        {

            if (TrasladosCart.RetrieveTrasladosCart() != null && TrasladosCart.RetrieveTrasladosCart().IDServicio > 0)
                idServicio = TrasladosCart.RetrieveTrasladosCart().IDServicio;
            else
                idServicio = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == id).Select(x => x.IDServicio).FirstOrDefault();


            var servicio = dbContext.Servicios.Where(x => x.IDServicio == idServicio).FirstOrDefault();
            if (servicio != null)
            {
                var subtipo = servicio.SubTipos;
                if (subtipo != null)
                {
                    spnSubTipo.InnerText = servicio.Nombre;
                    if (subtipo.Tipo == "R")
                    {
                        spnSubTipo.InnerText = servicio.Nombre;
                        pnlRoundTrip.Visible = true;
                        pnlIda.Visible = true;
                        pnlVuelta.Visible = true;
                        switch (subtipo.Lugar1)
                        {
                            case "Aeropuerto":
                                pnlAeropuertoIda.Visible = true;
                                cargarCombo(true);
                                pnlHotelIda.Visible = true;
                                spnHotelIda.InnerText = "Al Hotel";
                                if (subtipo.Lugar3 == "Aeropuerto" )
                                {
                                    pnlAeropuertoVuelta.Visible = true;
                                    cargarCombo(false);
                                }
                                else
                                {
                                    pnlHotelVuelta.Visible = true;
                                    pnlHotelVuelta2.Visible = true;
                                    spnHotelVuelta.InnerText = "De Hotel 1";
                                    spnHotelVuelta2.InnerText = "Al Hotel 2";
                                }
                                break;
                            case "Hotel":
                                pnlHotelIda.Visible = true;
                                pnlHotelIda2.Visible = true;
                               

                                if (subtipo.Lugar3 == "Hotel")
                                {
                                    if (subtipo.Lugar2 == "Hotel")
                                    {
                                        spnHotelIda.InnerText = "Desde Hotel en Río ";
                                        spnHotelIda2.InnerText = "Al Hotel en Búzios";
                                        spnHotelVuelta.InnerText = "Desde Hotel en Búzios";
                                        spnHotelVuelta2.InnerText = "Al Hotel en Río";
                                    }
                                    else
                                    {
                                        spnHotelVuelta.InnerText = "Desde Hotel 1";
                                        spnHotelVuelta2.InnerText = "Al Hotel 2";
                                    }

                                    pnlHotelVuelta.Visible = true;
                                    pnlHotelVuelta2.Visible = true;
                                }
                                else
                                {
                                    if (subtipo.Lugar2 == "Hotel")
                                    {
                                        spnHotelIda.InnerText = "Desde Hotel";
                                        spnHotelIda2.InnerText = "Al Hotel";
                                    }
                                    else
                                        spnHotelIda.InnerText = "Al Hotel";
                                    pnlAeropuertoVuelta.Visible = true;
                                    cargarCombo(false);
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
   
    private void cargarCombo(bool ida)
    {
        using (var dbContext = new ACHEEntities())
        {
            var aeropuertos = dbContext.LugaresTraslados.Where(x => x.Activo).ToList();
            if (aeropuertos != null && aeropuertos.Count() > 0)
            {
                if (ida)
                {
                    aeropuertos = aeropuertos.Where(x=>x.Tipo=="O").ToList();
                    cmbAeropuertoIda.DataSource = aeropuertos;
                    cmbAeropuertoIda.DataValueField = "IDLugarTraslado";
                    cmbAeropuertoIda.DataTextField = "Nombre";
                    cmbAeropuertoIda.DataBind();

                    cmbAeropuertoIda.Items.Insert(0, new ListItem("Seleccione una opción", ""));
                }
                else
                {
                    aeropuertos = aeropuertos.Where(x => x.Tipo == "D").ToList();
                    cmbAeropuertoVuelta.DataSource = aeropuertos;
                    cmbAeropuertoVuelta.DataValueField = "IDLugarTraslado";
                    cmbAeropuertoVuelta.DataTextField = "Nombre";
                    cmbAeropuertoVuelta.DataBind();

                    cmbAeropuertoVuelta.Items.Insert(0, new ListItem("Seleccione una opción", ""));
                }

            }
        }
    }

    [WebMethod(true)]
    public static void CrearPedido(int idPedido,string fechaIda, int idAeropuertoIda, string companiaIda, string nroVueloIda, string horaArriboIda, string nombreHotel1, string direccionHotel1
                                 , string nombreHotel2, string direccionHotel2, string fechaVuelta, int idAeropuertoVuelta, string companiaVuelta, string nroVueloVuelta
                                 , string horaArriboVuelta, string nombreHotel3, string direccionHotel3, string nombreHotel4, string direccionHotel4, string observaciones)
    {
        if (HttpContext.Current.Session["CurrentUser"] != null)
        {
            using (var dbContext = new ACHEEntities())
            {
                try
                {
                    int idServicio = 0;
                    if(idPedido>0 &&TrasladosCart.RetrieveTrasladosCart().IDServicio==null&&TrasladosCart.RetrieveTrasladosCart().IDServicio==0)
                         idServicio=dbContext.PedidosTraslado.Where(x=>x.IDPedidoTraslado==idPedido).FirstOrDefault().IDServicio;
                    else
                        idServicio = TrasladosCart.RetrieveTrasladosCart().IDServicio;
                    var servicio = dbContext.Servicios.Where(x => x.IDServicio == idServicio).FirstOrDefault();
                    if (servicio != null)
                    {
                        TrasladosCart.RetrieveTrasladosCart().IDPedido = idPedido;
                        string[] aux1 = fechaIda.Split("/");
                        string[] aux2 = fechaVuelta.Split("/");

                        //Esto esta asi proque el servidor esta configurado en USA. No modificar
                        if (!string.IsNullOrEmpty(fechaIda))
                            TrasladosCart.RetrieveTrasladosCart().FechaIda = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);//DateTime.Parse(fechaIda);// DateTime.Parse(fechaIda);
                        else
                            TrasladosCart.RetrieveTrasladosCart().FechaIda = null;
                        if (!string.IsNullOrEmpty(fechaVuelta))
                            TrasladosCart.RetrieveTrasladosCart().FechaVuelta = DateTime.Parse(aux2[2] + "-" + aux2[1] + "-" + aux2[0]);//DateTime.Parse(fechaVuelta);// DateTime.Parse(fechaVuelta);
                        else
                            TrasladosCart.RetrieveTrasladosCart().FechaVuelta = null;
                        decimal precio = 0;
                        decimal PrecioRegular = TrasladosCart.RetrieveTrasladosCart().getPrecioRegular(idServicio, TrasladosCart.RetrieveTrasladosCart().FechaIda, TrasladosCart.RetrieveTrasladosCart().FechaVuelta, dbContext);

                        decimal PrecioPrivado = TrasladosCart.RetrieveTrasladosCart().getPrecioPrivado(idServicio, TrasladosCart.RetrieveTrasladosCart().FechaIda, TrasladosCart.RetrieveTrasladosCart().FechaVuelta, dbContext);
                        if (PrecioRegular!=0)
                            precio = PrecioRegular;
                        else
                            precio = PrecioPrivado;
                        /* CAMBIO PARA QUE SEA POR FECHA 
                        if (servicio.PrecioRegular != 0)
                            precio = servicio.PrecioRegular;
                        else if (servicio.PrecioPrivado != 0)
                            precio = servicio.PrecioPrivado;
                        else if (servicio.PrecioRegularNoRepresentado != 0)
                            precio = servicio.PrecioRegularNoRepresentado;
                        else
                            precio = servicio.PrecioPrivadoNoRepresentado;
                        */

                        if (TrasladosCart.RetrieveTrasladosCart() != null && TrasladosCart.RetrieveTrasladosCart().CantAdultos > 0)
                        {
                            int SERVID = TrasladosCart.Instance.IDServicio;
                            var servici = dbContext.Servicios.Find(SERVID);
                            bool esPrivado = servici.EsPrivado;

                            int cantAdultos = TrasladosCart.RetrieveTrasladosCart().CantAdultos;
                            int cantMenores = TrasladosCart.RetrieveTrasladosCart().Cantmenores;
                            decimal total = 0;

                            if (servici != null && esPrivado)
                            {
                                total = precio;

                            }
                            else
                            {
                                total = precio * (cantAdultos + cantMenores);
                            }

                          /*  #region Adicional Nocturno
                            var parametros = dbContext.Parametros.FirstOrDefault();
                            if (horaArriboIda != string.Empty && horaArriboIda != ":")
                            {
                                int hora = int.Parse(horaArriboIda.Split(":")[0]);
                                int minutos = int.Parse(horaArriboIda.Split(":")[1]);
                                
                                TimeSpan start = new TimeSpan(1, 40, 0); //10 o'clock
                                TimeSpan end = new TimeSpan(3, 40, 0); //12 o'clock
                                TimeSpan now = new TimeSpan(hora, minutos, 0); //10 o'clock

                                if ((now > start) && (now < end))
                                {
                                    //match found
                                    total = total + (parametros.AdicionalNocturno * (cantAdultos + cantMenores));
                                }
                            }
                            #endregion*/
                            TrasladosCart.RetrieveTrasladosCart().Total = total;
                            TrasladosCart.RetrieveTrasladosCart().Precio = precio;
                        }

                    

                       if(servicio!=null)
                           TrasladosCart.RetrieveTrasladosCart().TipoServicio = servicio.SubTipos.Tipo;
                      
                        if (companiaIda != "")
                            TrasladosCart.RetrieveTrasladosCart().AerolineaArribo = companiaIda;
                        else
                            TrasladosCart.RetrieveTrasladosCart().AerolineaArribo = null;

                        if (nroVueloIda != "")
                            TrasladosCart.RetrieveTrasladosCart().VueloArribo = nroVueloIda;
                        else
                            TrasladosCart.RetrieveTrasladosCart().VueloArribo = null;

                        if (horaArriboIda != ":")
                            TrasladosCart.RetrieveTrasladosCart().HoraArribo = horaArriboIda;
                        else
                            TrasladosCart.RetrieveTrasladosCart().HoraArribo = null;

                        if (companiaVuelta != "")
                            TrasladosCart.RetrieveTrasladosCart().AerolineaPartida = companiaVuelta;
                        else
                            TrasladosCart.RetrieveTrasladosCart().AerolineaPartida = null;

                        if (nroVueloVuelta != "")
                            TrasladosCart.RetrieveTrasladosCart().VueloPartida = nroVueloVuelta;
                        else
                            TrasladosCart.RetrieveTrasladosCart().VueloPartida = null;

                        if (horaArriboVuelta != ":")
                            TrasladosCart.RetrieveTrasladosCart().HoraPartida = horaArriboVuelta;
                        else
                            TrasladosCart.RetrieveTrasladosCart().HoraPartida = null;

                        TrasladosCart.RetrieveTrasladosCart().Observaciones = observaciones;


                        if (nombreHotel1 != "")
                            TrasladosCart.RetrieveTrasladosCart().Hotel1 = nombreHotel1;
                        else
                            TrasladosCart.RetrieveTrasladosCart().Hotel1 = null;

                        if (nombreHotel2 != "")
                            TrasladosCart.RetrieveTrasladosCart().Hotel2 = nombreHotel2;
                        else
                            TrasladosCart.RetrieveTrasladosCart().Hotel2 = null;

                        if (nombreHotel3 != "")
                            TrasladosCart.RetrieveTrasladosCart().Hotel3 = nombreHotel3;
                        else
                            TrasladosCart.RetrieveTrasladosCart().Hotel3 = null;

                        if (nombreHotel4 != "")
                            TrasladosCart.RetrieveTrasladosCart().Hotel4 = nombreHotel4;
                        else
                            TrasladosCart.RetrieveTrasladosCart().Hotel4 = null;

                        if (direccionHotel1 != "")
                            TrasladosCart.RetrieveTrasladosCart().DireccionHotel1 = direccionHotel1;
                        else
                            TrasladosCart.RetrieveTrasladosCart().DireccionHotel1 = null;

                        if (direccionHotel2 != "")
                            TrasladosCart.RetrieveTrasladosCart().DireccionHotel2 = direccionHotel2;
                        else
                            TrasladosCart.RetrieveTrasladosCart().DireccionHotel2 = null;

                        if (direccionHotel3 != "")
                            TrasladosCart.RetrieveTrasladosCart().DireccionHotel3 = direccionHotel3;
                        else
                            TrasladosCart.RetrieveTrasladosCart().DireccionHotel3 = null;

                        if (direccionHotel4 != "")
                            TrasladosCart.RetrieveTrasladosCart().DireccionHotel4 = direccionHotel4;
                        else
                            TrasladosCart.RetrieveTrasladosCart().DireccionHotel4 = null;

                     //   if(idServicio>0)
                       //     TrasladosCart.RetrieveTrasladosCart().IDServicio = idServicio;
                       // if (TrasladosCart.RetrieveTrasladosCart() != null && TrasladosCart.RetrieveTrasladosCart().IDUsuario>0)
                         //    TrasladosCart.RetrieveTrasladosCart().IDUsuario = TrasladosCart.RetrieveTrasladosCart().IDUsuario;

                        if (idAeropuertoVuelta > 0)
                            TrasladosCart.RetrieveTrasladosCart().IDAeropuertoDestino = idAeropuertoVuelta;
                        else
                            TrasladosCart.RetrieveTrasladosCart().IDAeropuertoDestino = null;

                        if (idAeropuertoIda > 0)
                            TrasladosCart.RetrieveTrasladosCart().IDAeropuertoOrigen = idAeropuertoIda;
                        else
                            TrasladosCart.RetrieveTrasladosCart().IDAeropuertoOrigen = null;
                        /*
                        if (TrasladosCart.RetrieveTrasladosCart() != null && TrasladosCart.RetrieveTrasladosCart().Items.Count() > 0)
                        {
                            if(idPedido>0){
                                var pasajeros = dbContext.PasajerosPorPedidoTraslado.Where(x => x.IDPedidoTraslado == idPedido);
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
                   
                        if(idPedido==0)
                            dbContext.PedidosTraslado.Add(pedido);

                        dbContext.SaveChanges();
                        HttpContext.Current.Session["ASPNETTraslados"] = null;
                  //      TrasladosCart.RetrieveTrasladosCart().Items.Clear();
                        result = pedido.IDPedidoTraslado;     */
                    }

                }
                catch (Exception ex)
                {
                    var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    BasicLog.AppendToFile(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, ex.ToString());
                    throw new Exception("Hubo un error, por favor intente nuevamente");
                }
            }
        }
    }


    [WebMethod(true)]
    public static List<string> GetPrecios(string fechaIda, string fechaVuelta,int idPedido)
    {
        List<string> result = new List<string>();
        using (var dbContext = new ACHEEntities())
        {
            int idServicio = 0;
            if (idPedido > 0 && TrasladosCart.RetrieveTrasladosCart().IDServicio == null && TrasladosCart.RetrieveTrasladosCart().IDServicio == 0)
                idServicio = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == idPedido).FirstOrDefault().IDServicio;
            else
                idServicio = TrasladosCart.RetrieveTrasladosCart().IDServicio;
            if (idServicio !=0)
            {
                string[] aux1 = fechaIda.Split("/");
                string[] aux2 = fechaVuelta.Split("/");

                DateTime? fIda;
                DateTime? fVuelta;
                if (!string.IsNullOrEmpty(fechaIda))
                    fIda = DateTime.Parse(aux1[2] + "-" + aux1[1] + "-" + aux1[0]);//DateTime.Parse(fechaIda);// DateTime.Parse(fechaIda);
                else
                    fIda = null;

                if (!string.IsNullOrEmpty(fechaVuelta))
                    fVuelta = DateTime.Parse(aux2[2] + "-" + aux2[1] + "-" + aux2[0]);//DateTime.Parse(fechaVuelta);// DateTime.Parse(fechaVuelta);
                else
                    fVuelta = null;


                decimal PrecioRegular = TrasladosCart.RetrieveTrasladosCart().getPrecioRegular(idServicio, fIda, fVuelta, dbContext);

                decimal PrecioPrivado = TrasladosCart.RetrieveTrasladosCart().getPrecioPrivado(idServicio, fIda, fVuelta, dbContext);


                string ObsRegular = TrasladosCart.RetrieveTrasladosCart().getObservacionesRegular(idServicio, fIda, fVuelta, dbContext);

                string ObsPriPrivado = TrasladosCart.RetrieveTrasladosCart().getObservacionesPrivado(idServicio, fIda, fVuelta, dbContext);

                result.Insert(0, PrecioRegular.ToString("N2").Replace(".00", "").Replace(",00", ""));
                result.Insert(1, PrecioPrivado.ToString("N2").Replace(".00", "").Replace(",00", ""));


                result.Insert(2, ObsRegular);
                result.Insert(3, ObsPriPrivado);

                //decimal PrecioNeto = TrasladosCart.RetrieveTrasladosCart().getPrecioNeto(idServicio, fIda, fVuelta, dbContext);
                //string ObsNeto = TrasladosCart.RetrieveTrasladosCart().getObservacionesNeto(idServicio, fIda, fVuelta, dbContext);
                //result.Insert(4, PrecioPrivado.ToString("N2").Replace(".00", "").Replace(",00", ""));
                //result.Insert(5, PrecioPrivado.ToString("N2").Replace(".00", "").Replace(",00", ""));
            }
        }
        return result;
    }
}