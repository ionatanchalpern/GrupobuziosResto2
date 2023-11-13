using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class login_traslados : WebBasePage {

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (CurrentUser != null)
                Response.Redirect("traslados-paso1.aspx");

            
        }
    }

    protected void btnIngresar_Click(object sender, EventArgs e) {
        var email = txtEmail.Text;
        var password = txtPwd.Text;
        login(email, password);
    }

    private void login(string email, string password) {
        using (var dbContext = new ACHEEntities()) {
            var usuario = dbContext.Usuarios.Where(x => x.Activo && x.Email == email && x.Pwd == password && x.Estado.ToUpper() == "A").FirstOrDefault();
            if (usuario != null) {
                var token = Guid.NewGuid().ToString();
                var id = txtEmail.Text;
                var pwd = txtPwd.Text;
                var expira = DateTime.Now.AddHours(6);

                //almacenarLogin(id, pwd, token, expira);

                CurrentUser = new WebUser(usuario.Empresa, usuario.Email, usuario.Contacto, "", usuario.IDUsuario, false, "O", usuario.Telefono, "", usuario.PagoPorHabilitado, "traslados");
                WebBasePage.UsuarioCerro = false;

                HttpCookie myCookie = new HttpCookie("UserTokenFront");
                myCookie.Value = token;
                myCookie.Expires = DateTime.Now.AddHours(6);
                Response.Cookies.Add(myCookie);

                Response.Redirect("traslados-paso1.aspx");
            }
            else {
                lblError.Visible = true;
                lblError.Text = "Email y/o contraseña incorrecto<br /><br />";
            }
        }
    }

    //private void almacenarLogin(string idUsuario, string pwd, string token, DateTime expira) {
    //    //using (var dbContext = new ACHEEntities())
    //    //{
    //    //    var datosLogin = new AuthenticationToken();
    //    //    datosLogin.IDUsuario = idUsuario;
    //    //    datosLogin.Token = token;
    //    //    datosLogin.Pwd = pwd;
    //    //    datosLogin.FechaExpiracion = expira;
    //    //    datosLogin.FechaUltLogin = DateTime.Now;

    //    //    dbContext.AuthenticationToken.Add(datosLogin);
    //    //    dbContext.SaveChanges();
    //    //}
    //}
}