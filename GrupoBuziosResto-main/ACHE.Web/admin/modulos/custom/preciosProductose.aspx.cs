using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_modulos_custom_preciosProductose : AdminBasePage
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
            Response.Redirect("preciosProductos.aspx");
        }
    }

    protected void btnCancelar_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("preciosProductos.aspx");
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
            var entity = dbContext.PreciosProductos.Where(x => x.IDPrecioProducto == this.idEntidad).FirstOrDefault();
            if (entity != null)
            {
                cmbTipos.SelectedValue = entity.Productos.Tipo;

                txtFechaDesde.Text = entity.FechaDesde.ToString("dd/MM/yyyy");
                txtPrecioOp.Text = entity.PrecioOperador.ToString();
                txtPrecioResto.Text = entity.PrecioRestaurante.ToString();
                txtFechaHasta.Text = entity.FechaHasta.ToString("dd/MM/yyyy");
            }
            else
                throw new Exception("Entidad inexistente");
        }
    }

    private void CreateEntity()
    {
        using (var dbContext = new ACHEEntities())
        {
            PreciosProductos entity = null;
            if (this.idEntidad > 0)
            {
                entity = dbContext.PreciosProductos.Where(x => x.IDPrecioProducto == this.idEntidad).FirstOrDefault();
            }
            else
            {
                entity = new PreciosProductos();
            }

            var tipo = cmbTipos.SelectedValue;
            var Producto = dbContext.Productos.Where(x => x.Tipo == tipo).FirstOrDefault();

            entity.IDProducto = Producto.IDProducto;
            entity.PrecioRestaurante = decimal.Parse(txtPrecioResto.Text);

            entity.PrecioOperador = decimal.Parse(txtPrecioOp.Text);

            string[] auxFecha = txtFechaDesde.Text.Split('/');
            string[] auxFecha2 = txtFechaHasta.Text.Split('/');
            entity.FechaDesde = DateTime.Parse(auxFecha[2] + "-" + auxFecha[1] + "-" + auxFecha[0]);
            entity.FechaHasta = DateTime.Parse(auxFecha2[2] + "-" + auxFecha2[1] + "-" + auxFecha2[0]);
            var isValid = true;
            if (this.idEntidad > 0)
            {
                var precioAnt = dbContext.PreciosProductos.Where(x => x.IDPrecioProducto != this.idEntidad && x.IDProducto == entity.IDProducto && (x.FechaHasta >= entity.FechaDesde && x.FechaDesde <= entity.FechaHasta)).FirstOrDefault();
                if (precioAnt != null)
                    isValid = false;
            }
            else
            {
                var precioAnt = dbContext.PreciosProductos.Where(x => x.IDProducto == entity.IDProducto && (x.FechaHasta >= entity.FechaDesde && x.FechaDesde <= entity.FechaHasta)).FirstOrDefault();
                if (precioAnt != null)
                    isValid = false;
            }
            if (isValid)
            {
                if (this.idEntidad > 0)
                    dbContext.SaveChanges();
                else
                {
                    dbContext.PreciosProductos.Add(entity);
                    dbContext.SaveChanges();
                }
            }
            else
                throw new Exception("Ya existe un precio creado para esa fecha");
        }
    }








}