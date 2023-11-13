using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Extensions;
using ACHE.Model;
using System.Collections.Specialized;
using System.Configuration;

public partial class contacto : WebBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            lblOk.Text = "";

            ListDictionary usuario = new ListDictionary();

            usuario.Add("<EMAIL>", txtEmail.Text);
            usuario.Add("<EMPRESA>", txtContacto.Text);
            usuario.Add("<NOMBRE>", txtEmpresa.Text);
            usuario.Add("<ASUNTO>", txtAsunto.Text);
            usuario.Add("<TELEFONO>", txtTelefono.Text);

            bool send = EmailHelper.SendMessage(EmailTemplate.NuevoContacto, usuario, ConfigurationManager.AppSettings["Email.To"], "Grupo Buzios Restó: Contacto");
            if (!send)
            {
                throw new Exception("No se ha podido enviar el mensaje, por favor intente nuevamente.<br /><br />");
                lblOk.Visible = false;
            }
            else
            {
                lblOk.Visible = true;
                lblOk.Text = "Su mensaje ha sido enviado correctamente. A la brevedad le responderemos.<br /><br />";
                txtEmail.Text = txtContacto.Text = txtEmpresa.Text = txtAsunto.Text = txtTelefono.Text = "";
            }
        }
        catch (Exception ex)
        {
            var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            BasicLog.AppendToFile(Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
            ShowError(ex.Message);
        }
    }

    private void ShowError(string msg)
    {
        lblError.Visible = true;
        lblError.Text = msg;
        lblOk.Visible = false;
    }
}
