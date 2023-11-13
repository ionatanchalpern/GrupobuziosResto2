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

public partial class traslados_paso1 : WebBasePage
{

    private int idPedido;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentUser == null)
                Response.Redirect("/login-traslados.aspx");
            
            cargarDatos();
            this.idPedido = Request.QueryString["Id"] != null ? int.Parse(Request.QueryString["Id"]) : 0;
            if (this.idPedido > 0)
                LoadInfoPedido(this.idPedido);            
        }
    }
    
    private void LoadInfoPedido(int id)
    {
        using (var dbContext = new ACHEEntities())
        {
            hdnIDPedido.Value = idPedido.ToString();
            var pedido = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == id ).FirstOrDefault();
            if (pedido != null && !(TrasladosCart.RetrieveTrasladosCart().IDUsuario >0))
            {
               
                cmbProveedor.SelectedValue = pedido.Servicios.IDProveedor.ToString();
                var servicios = GetServicios(pedido.Servicios.IDProveedor);
                var serviciosEspeciales = GetServiciosEspeciales(pedido.Servicios.IDProveedor);
                if (servicios != null && servicios.Count() > 0)
                {
                    cmbServicio.DataSource = servicios;
                    cmbServicio.DataValueField = "ID";
                    cmbServicio.DataTextField = "Nombre";
                    cmbServicio.DataBind();

                    cmbServicio.SelectedValue = pedido.IDServicio.ToString();


                    cmbServicioEspecial.DataSource = serviciosEspeciales;
                    cmbServicioEspecial.DataValueField = "ID";
                    cmbServicioEspecial.DataTextField = "Nombre";
                    cmbServicioEspecial.DataBind();

                    cmbServicioEspecial.SelectedValue = pedido.IDServicio.ToString();
                }

                cmbCantidadAdultos.SelectedValue = pedido.CantAdultos.ToString();
                cmbCantidadMenores.SelectedValue = pedido.CantMenoresAsiento.ToString();
                cmbCantidadMenores2.SelectedValue = pedido.CantMenoresGratis.ToString();
            }
        }
    }
    
    private void cargarDatos()
    {
        using (var dbContext = new ACHEEntities())
        {
            if (CurrentUser != null) {
                hdnIDUsuario.Value = CurrentUser.ID.ToString();
                divServiciosEspeciales.Visible = dbContext.Usuarios.Where(x => x.IDUsuario == CurrentUser.ID).FirstOrDefault().ServiciosEspeciales;                
            }
        
            var proveedores = dbContext.Proveedores.Where(x => x.Activo).ToList();
            if (proveedores != null && proveedores.Count() > 0)
            {
                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DataValueField = "IDProveedor";
                cmbProveedor.DataTextField = "Nombre";
                cmbProveedor.DataBind();

                cmbProveedor.Items.Insert(0, new ListItem("Seleccione un transportista", ""));
            }
            if (TrasladosCart.RetrieveTrasladosCart().IDUsuario > 0)
            {
                int idServ=TrasladosCart.RetrieveTrasladosCart().IDServicio;
                var serv = dbContext.Servicios.Where(x => x.IDServicio == idServ).FirstOrDefault();
                cmbProveedor.SelectedValue = serv.IDProveedor.ToString();

                var servicios = dbContext.Servicios.Where(x => x.Activo && x.IDProveedor==serv.IDProveedor && !x.ServicioEspecial).ToList();
                if (servicios != null && servicios.Count() > 0)
                {
                    cmbServicio.DataSource = servicios;
                    cmbServicio.DataValueField = "IDServicio";
                    cmbServicio.DataTextField = "Nombre";
                    cmbServicio.DataBind();
                    cmbServicio.Items.Insert(0, new ListItem("Seleccione un servicio", ""));
                }
                cmbServicio.SelectedValue = idServ.ToString();

                var serviciosEspeciales = dbContext.Servicios.Where(x => x.Activo && x.IDProveedor == serv.IDProveedor && x.ServicioEspecial).ToList();
                if (serviciosEspeciales != null && serviciosEspeciales.Count() > 0) {
                    cmbServicioEspecial.DataSource = serviciosEspeciales;
                    cmbServicioEspecial.DataValueField = "IDServicio";
                    cmbServicioEspecial.DataTextField = "Nombre";
                    cmbServicioEspecial.DataBind();
                    cmbServicioEspecial.Items.Insert(0, new ListItem("Seleccione un servicio", ""));
                }
                cmbServicioEspecial.SelectedValue = idServ.ToString();

                cmbCantidadAdultos.SelectedValue = TrasladosCart.RetrieveTrasladosCart().CantAdultos.ToString();
                cmbCantidadMenores.SelectedValue = TrasladosCart.RetrieveTrasladosCart().Cantmenores.ToString();
                cmbCantidadMenores2.SelectedValue = TrasladosCart.RetrieveTrasladosCart().Cantmenores2.ToString();

            }
        //    this.Session["PrecioNocturno"] = litPrecioNocturno1.Text = litPrecioNocturno2.Text = litPrecioNocturno3.Text = dbContext.Parametros.First().AdicionalNocturno.ToString();
            this.Session["PrecioNocturno"] = dbContext.Parametros.First().AdicionalNocturno.ToString();

        }
    }
    
    [WebMethod(true)]
    public static List<ComboViewModel> GetServicios(int idProveedor)
    {
        List<ComboViewModel> result = new List<ComboViewModel>();
        using (var dbContext = new ACHEEntities())
        {
            var servicios = dbContext.Servicios
                .Where(x => x.Activo && x.IDProveedor == idProveedor && !x.ServicioEspecial)
                .OrderBy(x => x.Nombre)
                .ToList()
                .Select(x => new ComboViewModel()
                {
                    ID = x.IDServicio.ToString(),
                    Nombre = x.Nombre
                });
            if (servicios != null && servicios.Count() > 0)
                result.AddRange(servicios);
        }
        return result;
    }

    [WebMethod(true)]
    public static List<ComboViewModel> GetServiciosEspeciales(int idProveedor) {
        List<ComboViewModel> result = new List<ComboViewModel>();
        using (var dbContext = new ACHEEntities()) {
            var servicios = dbContext.Servicios
                .Where(x => x.Activo && x.IDProveedor == idProveedor && x.ServicioEspecial)
                .OrderBy(x => x.Nombre)
                .ToList()
                .Select(x => new ComboViewModel() {
                    ID = x.IDServicio.ToString(),
                    Nombre = x.Nombre
                });
            if (servicios != null && servicios.Count() > 0)
                result.AddRange(servicios);
        }
        return result;
    }

    [WebMethod(true)]
    public static List<string> GetPrecios(int idServicio)
    {
        List<string> result = new List<string>();
        using (var dbContext = new ACHEEntities())
        {
            var servicio = dbContext.Servicios.Where(x => x.Activo && x.IDServicio == idServicio).FirstOrDefault();
            if (servicio != null)
            {
                result.Insert(0, servicio.PrecioRegular.ToString("N2").Replace(".00", "").Replace(",00", ""));
                result.Insert(1, servicio.PrecioPrivado.ToString("N2").Replace(".00", "").Replace(",00", ""));
                result.Insert(2, servicio.PrecioRegularNoRepresentado.ToString("N2").Replace(".00", "").Replace(",00", ""));
                result.Insert(3, servicio.PrecioPrivadoNoRepresentado.ToString("N2").Replace(".00", "").Replace(",00", ""));
                result.Insert(4, servicio.ObservacionesRegular);
                result.Insert(5, servicio.ObservacionesPrivado);
           //     result.Insert(6, servicio.Tipo);
            }
        }
        return result;
    }

    [WebMethod(true)]
    public static void AddTraslado(int idUsuario, int IDServicio, int cantAdultos, int cantMenores, int cantMenores2)
    {
        TrasladosCart.RetrieveTrasladosCart().IDUsuario = idUsuario;
        TrasladosCart.RetrieveTrasladosCart().IDServicio = IDServicio;
        TrasladosCart.RetrieveTrasladosCart().CantAdultos = cantAdultos;
        TrasladosCart.RetrieveTrasladosCart().Cantmenores = cantMenores;
        TrasladosCart.RetrieveTrasladosCart().Cantmenores2 = cantMenores2;

      //  TrasladosCart.RetrieveTrasladosCart().Items.Clear();

    }

}