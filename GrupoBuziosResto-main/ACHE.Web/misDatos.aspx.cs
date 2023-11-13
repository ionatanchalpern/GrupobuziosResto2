using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class misDatos : WebBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            if (CurrentUser != null && CurrentUser.TipoUsuario=="O")
            {
                cargarDatos(CurrentUser.ID);
            }
            else
                Response.Redirect("login.aspx");
        }
    }

    private void cargarDatos(int id) 
    {
        using (var dbContext = new ACHEEntities())
        {
            var datosUsuario = dbContext.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault();
            if (datosUsuario != null)
            {
                txtEmail.Text = datosUsuario.Email;
                txtEmpresa.Text = datosUsuario.Empresa;
                txtContacto.Text = datosUsuario.Contacto;
                txtDireccion.Text = datosUsuario.Direccion;
                txtTelefono.Text = datosUsuario.Telefono;
            }
            else 
            {
                Response.Redirect("default.aspx");
            }
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        guardarDatos(CurrentUser.ID);
    }

    private void guardarDatos(int id) 
    {
        using (var dbContext = new ACHEEntities()) 
        {
            var datosUsuario = dbContext.Usuarios.Where(x => x.IDUsuario == id).FirstOrDefault();
            if (datosUsuario != null)
            {
                datosUsuario.Empresa = txtEmpresa.Text;
                datosUsuario.Contacto = txtContacto.Text;
                datosUsuario.Direccion = txtDireccion.Text;
                datosUsuario.Telefono = txtTelefono.Text;

                dbContext.SaveChanges();
            }
        }
    }
}