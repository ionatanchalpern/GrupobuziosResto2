using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class admin_modulos_custom_proveedorese : AdminBasePage {

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
            var entity = dbContext.Proveedores.Where(x => x.IDProveedor == this.idEntidad).FirstOrDefault();
            if (entity != null) {
                txtNombre.Text = entity.Nombre;
                txtEmail.Text = entity.Email;
                txtTelefono.Text = entity.Telefono;
                txtObservaciones.Text = entity.Observaciones;
                chkActivo.Checked = entity.Activo;
                #region Logo
                if (!string.IsNullOrEmpty(entity.Logo)) {
                    imgLogo.ImageUrl = "~/files/logos/" + entity.Logo;
                    lnkEliminarLogo.Visible = true;
                }
                else {
                    imgLogo.ImageUrl = "~/files/logos/no-photo.jpg";
                    lnkEliminarLogo.Visible = false;
                }
                #endregion
            }
            else
                throw new Exception("Entidad inexistente");
        }
    }

    private void CreateEntity() {
        using (var dbContext = new ACHEEntities()) {
            Proveedores entity = null;
            if (this.idEntidad > 0)
                entity = dbContext.Proveedores.Where(x => x.IDProveedor == this.idEntidad).FirstOrDefault();
            else {
                entity = new Proveedores();
                if (dbContext.Proveedores.Any(x => x.Email.ToLower() == txtEmail.Text.Trim().ToLower()))
                    throw new Exception("Ya hay un proveedor registrado con el email: " + txtEmail.Text.Trim() + ", por favor ingrese otro.");
            }
            
            entity.Nombre = txtNombre.Text.Trim();
            entity.Email = txtEmail.Text.Trim();
            entity.Telefono = txtTelefono.Text.Trim();
            entity.Observaciones = txtObservaciones.Text.Trim();
            entity.Activo = chkActivo.Checked;

            #region Logo
            if (flpLogo.HasFile) {
                string ext = System.IO.Path.GetExtension(flpLogo.FileName);
                string uniqueName = "img_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ext;
                string path = System.IO.Path.Combine(Server.MapPath("~/files/logos/"), uniqueName);
                //save the file to our local path
                flpLogo.SaveAs(path);
                entity.Logo = uniqueName;
            }
            #endregion

            if (this.idEntidad > 0)
                dbContext.SaveChanges();
            else {
                dbContext.Proveedores.Add(entity);
                dbContext.SaveChanges();
            }
        }
    }

    protected void btnAceptar_OnClick(object sender, EventArgs e) {
        if (IsValid == true)
            Response.Redirect("proveedores.aspx");
    }

    protected void btnCancelar_OnClick(object sender, EventArgs e) {
        Response.Redirect("proveedores.aspx");
    }

    private void ShowError(string msg) {
        litError.Text = msg;
        pnlError.Visible = true;
    }

    protected void lnkEliminarLogo_Click(object sender, EventArgs e) {
        using (var dbContext = new ACHEEntities()) {
            Proveedores entity = dbContext.Proveedores.Where(x => x.IDProveedor == this.idEntidad).FirstOrDefault();
            if (entity.Logo != null) {
                string path = Server.MapPath("~/files/logos/") + entity.Logo;
                FileInfo myfileinf = new FileInfo(path);
                myfileinf.Delete();
            }

            entity.Logo = null;
            imgLogo.ImageUrl = "~/files/logos/no-photo.jpg";

            dbContext.SaveChanges();

            lnkEliminarLogo.Visible = false;
        }
    }
}

