using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;

public partial class admin_modulos_custom_serviciose : AdminBasePage
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
            LoadInfo();
            switch (this.Mode.ToUpper())
            {
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

    private void LoadInfo()
    {
        using (var dbContext = new ACHEEntities())
        {
            var proveedores = dbContext.Proveedores.ToList();
            if (proveedores != null && proveedores.Count() > 0)
            {
                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DataValueField = "IDProveedor";
                cmbProveedor.DataTextField = "Nombre";
                cmbProveedor.DataBind();

                cmbProveedor.Items.Insert(0, new ListItem("", ""));
            }
            var subTipos = dbContext.SubTipos.Where(x => x.Tipo == "R").Select(x => new { x.IDSubTipo,Nombre= x.Lugar1 + " - " + x.Lugar2 + " - " + x.Lugar3 }).ToList();
            if (subTipos != null && subTipos.Count() > 0)
            {
                ddlSubTipos.DataSource = subTipos;
                ddlSubTipos.DataValueField = "IDSubTipo";
                ddlSubTipos.DataTextField = "Nombre";
                ddlSubTipos.DataBind();
                ddlSubTipos.Items.Insert(0, new ListItem("", ""));
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
    protected void CargarSubTipos(object sender, EventArgs e)
    {
        using (var dbContext = new ACHEEntities())
        {
            var tipo = ddlTipo.SelectedValue;
            var subTipos = dbContext.SubTipos.Where(x => x.Tipo ==tipo).Select(x => new { x.IDSubTipo, Nombre = (tipo == "R") ? x.Lugar1 + " - " + x.Lugar2 + " - " + x.Lugar3 : x.Lugar1 + " - " + x.Lugar2 }).ToList();
            if (subTipos != null && subTipos.Count() > 0)
            {
                ddlSubTipos.DataSource = subTipos;
                ddlSubTipos.DataValueField = "IDSubTipo";
                ddlSubTipos.DataTextField = "Nombre";
                ddlSubTipos.DataBind();
                ddlSubTipos.Items.Insert(0, new ListItem("", ""));
            }
        }
    }

    private void LoadEntity()
    {
        using (var dbContext = new ACHEEntities())
        {
            var entity = dbContext.Servicios.Where(x => x.IDServicio == this.idEntidad).FirstOrDefault();
            if (entity != null)
            {
                hdnIDServicio.Value = entity.IDServicio.ToString();
                cmbProveedor.SelectedValue = entity.IDProveedor.ToString();
                txtNombre.Text = entity.Nombre;
                chkActivo.Checked = entity.Activo;
                chkServicioEspecial.Checked = entity.ServicioEspecial;
                ddlTipo.SelectedValue = entity.SubTipos.Tipo;
                var subTipos = dbContext.SubTipos.Where(x => x.Tipo == entity.SubTipos.Tipo).Select(x => new { x.IDSubTipo, Nombre = (entity.SubTipos.Tipo == "R") ? x.Lugar1 + " - " + x.Lugar2 + " - " + x.Lugar3 : x.Lugar1 + " - " + x.Lugar2 }).ToList();
                if (subTipos != null && subTipos.Count() > 0)
                {
                    ddlSubTipos.DataSource = subTipos;
                    ddlSubTipos.DataValueField = "IDSubTipo";
                    ddlSubTipos.DataTextField = "Nombre";
                    ddlSubTipos.DataBind();
                    ddlSubTipos.Items.Insert(0, new ListItem("", ""));
                }
                ddlSubTipos.SelectedValue = entity.SubTipos.IDSubTipo.ToString();
            }
            else
                throw new Exception("Entidad inexistente");
        }
    }

    private void CreateEntity()
    {
        using (var dbContext = new ACHEEntities())
        {

            Servicios entity = null;
            if (this.idEntidad > 0)
                entity = dbContext.Servicios.Where(x => x.IDServicio == this.idEntidad).FirstOrDefault();
            else
                entity = new Servicios();

            entity.IDProveedor = int.Parse(cmbProveedor.SelectedValue);
            entity.Nombre = txtNombre.Text.Trim();
            entity.PrecioRegular = 0;
            entity.PrecioRegularNoRepresentado = 0;// decimal.Parse(txtPrecioRegularNR.Text.Trim());
            entity.ObservacionesRegular ="";
            entity.PrecioPrivado =0;
            entity.PrecioPrivadoNoRepresentado = 0;// decimal.Parse(txtPrecioPrivadoNR.Text.Trim());
            entity.ObservacionesPrivado = "";
            entity.Activo = chkActivo.Checked;
            entity.ServicioEspecial = chkServicioEspecial.Checked;
            var subTipo = ddlSubTipos.SelectedValue;
            entity.IDSubTipo =int.Parse(subTipo);
           //entity.IDSubTipo = ids;
            if (this.idEntidad > 0)
                dbContext.SaveChanges();
            else
            {
                dbContext.Servicios.Add(entity);
                dbContext.SaveChanges();
            }
        }
    }

    protected void btnAceptar_OnClick(object sender, EventArgs e)
    {
        if (IsValid == true)
            Response.Redirect("servicios.aspx");
    }

    protected void btnCancelar_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("servicios.aspx");
    }

    private void ShowError(string msg)
    {
        litError.Text = msg;
        pnlError.Visible = true;
    }
    [WebMethod(true)]
    public static List<ComboViewModel> GetSubTipos(string tipo)
    {
        List<ComboViewModel> result = new List<ComboViewModel>();

        using (var dbContext = new ACHEEntities())
        {
            var subTipos = dbContext.SubTipos.Where(x => x.Tipo==tipo).ToList().Select(x => new ComboViewModel()
            {
                ID = x.IDSubTipo.ToString(),
                Nombre = tipo == "R" ? x.Lugar1 + " - " + x.Lugar2 + " - " + x.Lugar3 : x.Lugar1 + " - " + x.Lugar2
            });

            if (subTipos != null && subTipos.Count() > 0)
                result.AddRange(subTipos);
        }
        return result;
    }
}

