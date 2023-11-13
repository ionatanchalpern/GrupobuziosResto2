using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class admin_modulos_custom_parametros : AdminBasePage {

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack)
            LoadInfo();
    }

    private void LoadInfo() {
        using (var dbContext = new ACHEEntities()) {
            var entity = dbContext.Parametros.FirstOrDefault();
            if (entity != null) {
                txtBeneficios.Text = entity.TextoBeneficios;
                txtTraslados.Text = entity.TextoTraslados;
                txtAdicionalNoche.Text = entity.AdicionalNocturno.ToString().Replace(".00", "").Replace(",00", "");
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

    private void CreateEntity() {
        using (var dbContext = new ACHEEntities()) {
            var entity = dbContext.Parametros.FirstOrDefault();
            if (entity != null) {
                entity.TextoBeneficios = txtBeneficios.Text.Trim();
                entity.TextoTraslados = txtTraslados.Text.Trim();
                entity.AdicionalNocturno = decimal.Parse(txtAdicionalNoche.Text.Trim());
                dbContext.SaveChanges();
            }
            else throw new Exception("Hubo un error, por favor intente nuevamente");
        }
    }

    protected void btnAceptar_OnClick(object sender, EventArgs e) {
        if (IsValid == true) {
            pnlError.Visible = false;
            pnlOk.Visible = true;
        }
    }

    protected void btnCancelar_OnClick(object sender, EventArgs e) {
        Response.Redirect("/admin/home.aspx");
    }

    private void ShowError(string msg) {
        litError.Text = msg;
        pnlError.Visible = true;
    }
}

