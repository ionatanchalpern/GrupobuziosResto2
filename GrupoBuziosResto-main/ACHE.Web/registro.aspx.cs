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

public partial class registro : WebBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void registrarUsuario(string empresa, string contacto, string direccion, string telefono, string email, string pwd)
    {
        try
        {
            using (var dbContext = new ACHEEntities())
            {
                lblError.Visible = false;

                var entity = new Usuarios();
                entity.TipoUsuario = "O";
                entity.Email = email;
                entity.Pwd = pwd;
                entity.Empresa = empresa;
                entity.Contacto = contacto;
                entity.Direccion = direccion;
                entity.Telefono = telefono;
                entity.Activo = false;
                entity.FechaAlta = DateTime.Now;
                entity.Estado = "P";
                entity.Observaciones = string.Empty;
                entity.PagoPorHabilitado = false;

                ListDictionary usuario = new ListDictionary();

                usuario.Add("<EMAIL>", email);
                usuario.Add("<EMPRESA>", empresa);
                usuario.Add("<CONTACTO>", contacto);
                usuario.Add("<DIRECCION>", direccion);
                usuario.Add("<TELEFONO>", telefono);

                bool mailUsuario = EmailHelper.SendMessage(EmailTemplate.NuevoUsuarioOperador, usuario, email, "Grupo Buzios Restó: Solicitud de registro");
                bool send = EmailHelper.SendMessage(EmailTemplate.NuevoUsuarioAdmin, usuario, ConfigurationManager.AppSettings["Email.To"], "Grupo Buzios Restó: Solicitud de registro");
                if (!send)
                {
                    throw new Exception("No se ha podido enviar sus datos al administrador, por favor intente nuevamente.<br /><br />");
                }
                else
                {
                    dbContext.Usuarios.Add(entity);
                    dbContext.SaveChanges();
                    Response.Redirect("graciasRegistro.aspx");
                }

            }
        }
        catch (Exception e)
        {
            var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
            BasicLog.AppendToFile(Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
            ShowError(e.Message);
        }
    }

    private void ShowError(string msg)
    {
        lblError.Visible = true;
        lblError.Text = msg;
    }
    protected void btnRegistrar_Click1(object sender, EventArgs e)
    {
        string empresa = txtEmpresa.Text;
        string contacto = txtContacto.Text;
        string direccion = txtDireccion.Text;
        string telefono = txtTelefono.Text;
        string email = txtEmail.Text;
        string pwd = txtPwd.Text;
        registrarUsuario(empresa, contacto, direccion, telefono, email, pwd);
    }
}
