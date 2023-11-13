using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_modulos_custom_preciosServiciose : AdminBasePage
{
    #region Properties

    private string Mode = string.Empty;

    private int idEntidad
    {
        get
        {
            if (ViewState["idEntidad"] != null)
                return (int)ViewState["idEntidad"];
            else
                return 0;
        }
        set { ViewState["idEntidad"] = value; }
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Mode = Request.QueryString["Mode"];
        if (this.Mode != "A")
        {
            this.idEntidad = int.Parse(Request.QueryString["Id"]);
            litTitulo.Text = litTitulo2.Text = "Edición";
        }
        else
            litTitulo.Text = litTitulo2.Text = "Creación";

        if (!IsPostBack)
        {
            cargarCombos();
            switch (this.Mode.ToUpper())
            {
                case "A":
                    break;
                case "E":
                    LoadEntity();
                    break;
                default:
                    throw new Exception("Parametros incorrectos");
            }
        }
    }
    private void cargarCombos()
    {
        using (var dbContext = new ACHEEntities())
        {
            var proveedores = dbContext.Proveedores.OrderBy(x => x.Nombre).ToList();
            if (proveedores != null && proveedores.Count() > 0)
            {
                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DataValueField = "IDProveedor";
                cmbProveedor.DataTextField = "Nombre";
                cmbProveedor.DataBind();

                cmbProveedor.Items.Insert(0, new ListItem("", "0"));
            }
            /*
            var servicios = dbContext.Servicios.Where(x => x.Activo).ToList();
            if (servicios != null)
            {
                ddlServicios.DataSource = servicios;
                ddlServicios.DataValueField = "IDServicio";
                ddlServicios.DataTextField = "Nombre";
                ddlServicios.DataBind();
                ddlServicios.Items.Insert(0, new ListItem("", ""));
            }
             */ 
        }
    }
    protected void cargarServicios(object sender, System.EventArgs e)
    {
        using (var dbContext = new ACHEEntities())
        {
            int idProveedor = int.Parse(cmbProveedor.SelectedValue);
            var servicios = dbContext.Servicios.Where(x => x.Activo && x.IDProveedor==idProveedor).ToList();
            if (servicios != null)
            {
                ddlServicios.DataSource = servicios;
                ddlServicios.DataValueField = "IDServicio";
                ddlServicios.DataTextField = "Nombre";
                ddlServicios.DataBind();
                ddlServicios.Items.Insert(0, new ListItem("", ""));
            }
        }
    }


    private void cargarServicioPorProveedor (int idProveedor, ACHEEntities dbContext){
        var servicios = dbContext.Servicios.Where(x => x.Activo && x.IDProveedor == idProveedor).ToList();
        if (servicios != null)
        {
            ddlServicios.DataSource = servicios;
            ddlServicios.DataValueField = "IDServicio";
            ddlServicios.DataTextField = "Nombre";
            ddlServicios.DataBind();
            ddlServicios.Items.Insert(0, new ListItem("", ""));
        }
}
    protected void ServerValidate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = Process();
    }

    private bool Process()
    {
        bool value = false;

        if (IsValid)
        {
            try
            {
                CreateEntity();
                value = true;
            }
            catch (Exception e)
            {
                value = false;
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                BasicLog.AppendToFile(Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
                ShowError(e.Message);
            }
        }
        return value;
    }


    protected void btnAceptar_OnClick(object sender, EventArgs e)
    {
        if (IsValid == true)
        {
            Response.Redirect("preciosServicios.aspx");
        }
    }

    protected void btnCancelar_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("preciosServicios.aspx");
    }

    private void ShowError(string msg)
    {
        litError.Text = msg;
        pnlError.Visible = true;
    }











    private void LoadEntity()
    {
        using (var dbContext = new ACHEEntities())
        {
            var entity = dbContext.PreciosServicios.Where(x => x.IDPrecioServicio == this.idEntidad).FirstOrDefault();
            if (entity != null)
            {
                cmbProveedor.SelectedValue = entity.Servicios.IDProveedor.ToString();
                cargarServicioPorProveedor(entity.Servicios.IDProveedor,dbContext);

                ddlServicios.SelectedValue = entity.Servicios.IDServicio.ToString();

                txtFechaDesde.Text = entity.FechaDesde.ToString("dd/MM/yyyy");
                txtObservacionesPrivado.Text = entity.ObservacionesPrivado;
                txtObservacionesRegular.Text = entity.ObservacionesRegular;
                txtFechaHasta.Text = entity.FechaHasta.ToString("dd/MM/yyyy");
                txtPrecioPrivado.Text = entity.PrecioPrivado.ToString();
                txtPrecioRegular.Text = entity.PrecioRegular.ToString();
            }
            else
                throw new Exception("Entidad inexistente");
        }
    }

    private void CreateEntity()
    {
        using (var dbContext = new ACHEEntities())
        {
            PreciosServicios entity = null;
            if (this.idEntidad > 0)
            {
                entity = dbContext.PreciosServicios.Where(x => x.IDPrecioServicio == this.idEntidad).FirstOrDefault();
            }
            else
            {
                entity = new PreciosServicios();
            }
      

            entity.IDServicio =int.Parse( ddlServicios.SelectedValue);
            entity.PrecioPrivado = decimal.Parse(txtPrecioPrivado.Text);
            entity.PrecioRegular = decimal.Parse(txtPrecioRegular.Text);
            entity.ObservacionesRegular = txtObservacionesRegular.Text;
            entity.ObservacionesPrivado = txtObservacionesPrivado.Text;
            string[] auxFecha = txtFechaDesde.Text.Split('/');
            string[] auxFecha2 = txtFechaHasta.Text.Split('/');
            entity.FechaDesde = DateTime.Parse(auxFecha[2] + "-" + auxFecha[1] + "-" + auxFecha[0]);
            entity.FechaHasta = DateTime.Parse(auxFecha2[2] + "-" + auxFecha2[1] + "-" + auxFecha2[0]);
            var isValid = true;
            if (this.idEntidad > 0)
            {
                var precioAnt = dbContext.PreciosServicios.Where(x => x.IDPrecioServicio != this.idEntidad && x.IDServicio == entity.IDServicio && x.IDUsuario == null && (x.FechaHasta >= entity.FechaDesde && x.FechaDesde <= entity.FechaHasta)).FirstOrDefault();
                if (precioAnt != null)
                    isValid = false;
            }
            else
            {
                var precioAnt = dbContext.PreciosServicios.Where(x => x.IDServicio == entity.IDServicio && x.IDUsuario == null && (x.FechaHasta >= entity.FechaDesde && x.FechaDesde <= entity.FechaHasta)).FirstOrDefault();
                if (precioAnt != null)
                    isValid = false;
            }
            if (isValid)
            {
                if (this.idEntidad > 0)
                    dbContext.SaveChanges();
                else
                {
                    dbContext.PreciosServicios.Add(entity);
                    dbContext.SaveChanges();
                }
            }
            else
                throw new Exception("Ya existe un precio creado para esa fecha");
        }
    }



}