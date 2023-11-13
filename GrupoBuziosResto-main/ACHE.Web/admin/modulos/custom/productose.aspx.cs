using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class admin_modulos_custom_productose : AdminBasePage
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
            this.idEntidad = int.Parse(Request.QueryString["Id"]);

        if (!IsPostBack)
        {
            switch (this.Mode.ToUpper()) {
                case "A":
                    litModo.Text = litModo2.Text = "Creación";
                    break;
                case "E":
                    litModo.Text = litModo2.Text = "Edición";
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

    private void LoadEntity()
    {
        using (var dbContext = new ACHEEntities())
        {
            var entity = dbContext.Productos.Where(x => x.IDProducto == this.idEntidad).FirstOrDefault();
            if (entity != null)
            {
                cmbTipos.SelectedValue = entity.Tipo;
                txtEntrada.Text = entity.Entrada;
                txtPlatoPrincipal.Text = entity.PlatoPrincipal;
                txtPostre.Text = entity.Postre;
                chkBebida.Checked = entity.IncluyeBebida;
             txtPrecio.Text = entity.Precio.ToString();
                txtPrecioOperador.Text = entity.PrecioOperador.ToString();

                txtPrecioProxTemp.Text = entity.PrecioProxTemp.ToString();
                txtPrecioOperadorProxTemp.Text = entity.PrecioOperadorProxTemp.ToString();
                
                chkActivo.Checked = entity.Activo;
            }
            else
                throw new Exception("Entidad inexistente");
        }
    }

    private void CreateEntity()
    {
        using (var dbContext = new ACHEEntities())
        {
            Productos entity = null;
            if (this.idEntidad > 0)
                entity = dbContext.Productos.Where(x => x.IDProducto == this.idEntidad).FirstOrDefault();
            else
                entity = new Productos();

            entity.Tipo = cmbTipos.SelectedValue;
            entity.Entrada = txtEntrada.Text;
            entity.PlatoPrincipal = txtPlatoPrincipal.Text;
            entity.Postre = txtPostre.Text;
            entity.IncluyeBebida = chkBebida.Checked;
         /*   entity.Precio = int.Parse(txtPrecio.Text);
            entity.PrecioOperador = int.Parse(txtPrecioOperador.Text);

            entity.PrecioProxTemp = int.Parse(txtPrecioProxTemp.Text);
            entity.PrecioOperadorProxTemp = int.Parse(txtPrecioOperadorProxTemp.Text);
            */
            if(this.idEntidad>0){
              
            }else{
                entity.Precio = 0;
                entity.PrecioOperador =0;

                entity.PrecioProxTemp = 0;
                entity.PrecioOperadorProxTemp = 0;
            }
            entity.FechaAlta = DateTime.Now;
            entity.Activo = chkActivo.Checked;

            if (this.idEntidad > 0)
                dbContext.SaveChanges();
            else
            {
                dbContext.Productos.Add(entity);
                dbContext.SaveChanges();
            }
        }
    }

    protected void btnAceptar_OnClick(object sender, EventArgs e)
    {
        if (IsValid == true)
        {
            Response.Redirect("productos.aspx");
        }
    }

    protected void btnCancelar_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("productos.aspx");
    }

    private void ShowError(string msg)
    {
        litError.Text = msg;
        pnlError.Visible = true;
    }
}

