using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class admin_modulos_custom_lugaresTrasladose : AdminBasePage {

    #region Properties

    private string Mode = string.Empty;

    private int idEntidad {
        get {
            if (ViewState["idEntidad"] != null)
                return (int)ViewState["idEntidad"];
            else
                return 0;
        }
        set { ViewState["idEntidad"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e) {
        this.Mode = Request.QueryString["Mode"];
        if (this.Mode != "A")
            this.idEntidad = int.Parse(Request.QueryString["Id"]);

        if (!IsPostBack) {
            LoadInfo();
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

    private void LoadInfo() {
        using (var dbContext = new ACHEEntities()) {
            var proveedores = dbContext.Proveedores.ToList();
            if (proveedores != null && proveedores.Count() > 0) {
                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DataValueField = "IDProveedor";
                cmbProveedor.DataTextField = "Nombre";
                cmbProveedor.DataBind();

                cmbProveedor.Items.Insert(0, new ListItem("", ""));
            }
        }
    }

    protected void ServerValidate(object sender, ServerValidateEventArgs args) {
        args.IsValid = Process();
    }

    private bool Process() {
        bool value = false;

        if (IsValid) {
            try {
                CreateEntity();
                value = true;
            }
            catch (Exception e) {
                value = false;
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                BasicLog.AppendToFile(Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
                ShowError(e.Message);
            }
        }
        return value;
    }

    private void LoadEntity() {
        using (var dbContext = new ACHEEntities()) {
            var entity = dbContext.LugaresTraslados.Where(x => x.IDLugarTraslado == this.idEntidad).FirstOrDefault();
            if (entity != null) {
                cmbProveedor.SelectedValue = entity.IDProveedor.ToString();
                txtNombre.Text = entity.Nombre;
                cmbTipo.SelectedValue = entity.Tipo;
                chkActivo.Checked = entity.Activo;
            }
            else
                throw new Exception("Entidad inexistente");
        }
    }

    private void CreateEntity() {
        using (var dbContext = new ACHEEntities()) {
            LugaresTraslados entity = null;
            if (this.idEntidad > 0)
                entity = dbContext.LugaresTraslados.Where(x => x.IDLugarTraslado == this.idEntidad).FirstOrDefault();
            else
                entity = new LugaresTraslados();

            entity.IDProveedor = int.Parse(cmbProveedor.SelectedValue);
            entity.Nombre = txtNombre.Text.Trim();
            entity.Tipo = cmbTipo.SelectedValue;
            entity.Activo = chkActivo.Checked;

            if (this.idEntidad > 0)
                dbContext.SaveChanges();
            else {
                dbContext.LugaresTraslados.Add(entity);
                dbContext.SaveChanges();
            }
        }
    }

    protected void btnAceptar_OnClick(object sender, EventArgs e) {
        if (IsValid == true)
            Response.Redirect("lugaresTraslados.aspx");
    }

    protected void btnCancelar_OnClick(object sender, EventArgs e) {
        Response.Redirect("lugaresTraslados.aspx");
    }

    private void ShowError(string msg) {
        litError.Text = msg;
        pnlError.Visible = true;
    }
}

