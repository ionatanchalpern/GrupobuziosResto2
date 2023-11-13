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

public partial class traslados_paso3_oneWay : WebBasePage
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
         //   if (this.idPedido > 0)
           LoadInfoPedido(this.idPedido);
           using (var dbContext = new ACHEEntities())
           {
               int SERVID = TrasladosCart.Instance.IDServicio;

               var servici = dbContext.Servicios.Find(SERVID);
               bool esPrivado = servici.EsPrivado;

               if (servici != null)
               {


                   if (esPrivado)
                   {
                       titPrecio.InnerText = "Costo del Servicio Fijo por Auto";
                   }
                   else
                   {
                       titPrecio.InnerText = "Costo del Servicio Fijo por Persona";
                   }
               }
           }

        }

    }

    
    private void LoadInfoPedido(int id)
    {
        if (TrasladosCart.RetrieveTrasladosCart().FechaIda.HasValue || TrasladosCart.RetrieveTrasladosCart().FechaVuelta.HasValue)
        {
            if (TrasladosCart.RetrieveTrasladosCart().FechaIda.HasValue)
                txtFechaIda.Text = TrasladosCart.RetrieveTrasladosCart().FechaIda.Value.ToString("dd/MM/yyyy");
            if (TrasladosCart.RetrieveTrasladosCart().FechaVuelta.HasValue)
            {
                txtFechaVuelta.Text = TrasladosCart.RetrieveTrasladosCart().FechaVuelta.Value.ToString("dd/MM/yyyy");
                if (TrasladosCart.RetrieveTrasladosCart().TipoServicio == "O")
                    hdnIda.Value = "false";
            }
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
        }else{
            using (var dbContext = new ACHEEntities())
            {

                var pedido = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == id).FirstOrDefault();
                hdnIDPedido.Value = idPedido.ToString();
                if (pedido != null)
                {

                    if (pedido.FechaIda.HasValue)
                        txtFechaIda.Text = pedido.FechaIda.Value.ToString("dd/MM/yyyy");
                    if (pedido.FechaVuelta.HasValue)
                    {
                        txtFechaVuelta.Text = pedido.FechaVuelta.Value.ToString("dd/MM/yyyy");
                        if (pedido.Servicios.SubTipos.Tipo == "O")
                            hdnIda.Value = "false";
                    }
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
             if(TrasladosCart.RetrieveTrasladosCart()!=null && TrasladosCart.RetrieveTrasladosCart().IDServicio>0)
                   idServicio = TrasladosCart.RetrieveTrasladosCart().IDServicio;
             else
                  idServicio = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == id).Select(x => x.IDServicio).FirstOrDefault();
            var servicio = dbContext.Servicios.Where(x => x.IDServicio == idServicio).FirstOrDefault();
            
            var subtipo = servicio.SubTipos;
            if (subtipo != null)
            {
                spnSubTipo.InnerText = servicio.Nombre;
                if (subtipo.Tipo == "O")
                {
                    hdnLugar2.Value = subtipo.Lugar2;
                    hdnLugar1.Value = subtipo.Lugar1;
                    cargarCombos();
                }
            }
        }
    }
    
    private void cargarCombos()
    {
        using (var dbContext = new ACHEEntities())
        {
            var aeropuertos = dbContext.LugaresTraslados.Where(x => x.Activo).ToList();
            if (aeropuertos != null && aeropuertos.Count() > 0)
            {
               
                    aeropuertos = aeropuertos.Where(x => x.Tipo == "O").ToList();
                    cmbAeropuertoIda.DataSource = aeropuertos;
                    cmbAeropuertoIda.DataValueField = "IDLugarTraslado";
                    cmbAeropuertoIda.DataTextField = "Nombre";
                    cmbAeropuertoIda.DataBind();

                    cmbAeropuertoIda.Items.Insert(0, new ListItem("Seleccione una opción", ""));
               /*
                    aeropuertos = aeropuertos.Where(x => x.Tipo == "D").ToList();
                    cmbAeropuertoVuelta.DataSource = aeropuertos;
                    cmbAeropuertoVuelta.DataValueField = "IDLugarTraslado";
                    cmbAeropuertoVuelta.DataTextField = "Nombre";
                    cmbAeropuertoVuelta.DataBind();

                    cmbAeropuertoVuelta.Items.Insert(0, new ListItem("Seleccione una opción", ""));
                */
            }
        }
    }
    
    [WebMethod(true)]
    public static List<ComboViewModel>  GetAeropuertosVuelta(int idPedido)
    {
        List<ComboViewModel> result = new List<ComboViewModel>();

        using (var dbContext = new ACHEEntities())
        {
            int idLugarDestino=0;
            ComboViewModel aux = new ComboViewModel();
            if (idPedido > 0)
            {
                var pedido = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == idPedido).FirstOrDefault();
                if (pedido.IDLugarDestino != null)
                {
                    idLugarDestino = pedido.LugaresTrasladosDestino.IDLugarTraslado;
                    aux.ID = pedido.LugaresTrasladosDestino.IDLugarTraslado.ToString();
                    aux.Nombre = pedido.LugaresTrasladosDestino.Nombre;
                }
                else
                {
                    aux.ID = "";
                    aux.Nombre = "seleccione un Aeropuerto";
                }
               
            }
            else
            {
                aux.ID = "";
                aux.Nombre = "seleccione un Aeropuerto";
            }
            result.Add(aux);
            var aeropuertos= dbContext.LugaresTraslados.Where(x => x.Activo && x.Tipo == "D"&& x.IDLugarTraslado!=idLugarDestino).ToList().Select(x => new ComboViewModel()
                {
                    ID = x.IDLugarTraslado.ToString(),
                    Nombre = x.Nombre
                });
            
             if (aeropuertos != null && aeropuertos.Count() > 0)
                 result.AddRange(aeropuertos);
        }
        return result;
    }
    
}