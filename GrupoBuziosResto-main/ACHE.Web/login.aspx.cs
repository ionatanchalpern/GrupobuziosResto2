using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class login : WebBasePage {

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (CurrentUser != null)
                Response.Redirect("default.aspx");
        }
    }

    protected void btnIngresarOp_Click(object sender, EventArgs e) {
        var email = txtEmailOp.Text;
        var password = txtPwdOp.Text;
        loginOperador(email, password);
    }

    protected void btnIngresarRes_Click(object sender, EventArgs e) {
        var email = txtEmailRes.Text;
        var password = txtPwdRes.Text;
        loginRestaurant(email, password);
    }

    private void loginOperador(string email, string password) {
        using (var dbContext = new ACHEEntities()) {
            txtEmailRes.Text = string.Empty;
            txtPwdRes.Text = string.Empty;
            var usuario = dbContext.Usuarios.Where(x => x.Activo && x.Email == email && x.Pwd == password && x.Estado.ToUpper() == "A").FirstOrDefault();
            if (usuario != null) {
                var token = Guid.NewGuid().ToString();
                var id = txtEmailOp.Text;
                var pwd = txtPwdOp.Text;
                var expira = DateTime.Now.AddHours(6);

                almacenarLogin(id, pwd, token, expira);

                CurrentUser = new WebUser(usuario.Empresa, usuario.Email, usuario.Contacto, "", usuario.IDUsuario, false, "O", usuario.Telefono, "", usuario.PagoPorHabilitado, "resto");

                WebBasePage.UsuarioCerro = false;

                HttpCookie myCookie = new HttpCookie("UserTokenFront");
                myCookie.Value = token;
                myCookie.Expires = DateTime.Now.AddHours(6);
                Response.Cookies.Add(myCookie);

                Response.Redirect("default.aspx");
            }
            else {
                lblErrorOperador.Visible = true;
                lblErrorOperador.Text = "Email y/o contraseña incorrecto<br /><br />";
            }
        }
    }

    private void loginRestaurant(string email, string password) {
        using (var dbContext = new ACHEEntities()) {
            txtEmailOp.Text = string.Empty;
            txtPwdOp.Text = string.Empty;
            var restaurantUsuario = dbContext.Restaurantes.Where(x => x.Activo && x.Email == email && x.Pwd == password).FirstOrDefault();
            if (restaurantUsuario != null) {
                var token = Guid.NewGuid().ToString();
                var id = txtEmailRes.Text;
                var pwd = txtPwdRes.Text;
                var expira = DateTime.Now.AddHours(6);

                almacenarLogin(id, pwd, token, expira);

                CurrentUser = new WebUser(restaurantUsuario.Nombre, restaurantUsuario.Email, restaurantUsuario.Nombre, "", restaurantUsuario.IDRestaurant, false, "R", restaurantUsuario.Telefono, "", false, "resto");

                WebBasePage.UsuarioCerro = false;

                HttpCookie myCookie = new HttpCookie("UserTokenFront");
                myCookie.Value = token;
                myCookie.Expires = DateTime.Now.AddHours(6);
                Response.Cookies.Add(myCookie);

                Response.Redirect("default.aspx");
            }
            else {
                lblErrorRestaurant.Visible = true;
                lblErrorRestaurant.Text = "E-mail e / ou senha está incorreta<br /><br />";
            }
        }
    }

    private void almacenarLogin(string idUsuario, string pwd, string token, DateTime expira) {
        //using (var dbContext = new ACHEEntities())
        //{
        //    var datosLogin = new AuthenticationToken();
        //    datosLogin.IDUsuario = idUsuario;
        //    datosLogin.Token = token;
        //    datosLogin.Pwd = pwd;
        //    datosLogin.FechaExpiracion = expira;
        //    datosLogin.FechaUltLogin = DateTime.Now;

        //    dbContext.AuthenticationToken.Add(datosLogin);
        //    dbContext.SaveChanges();
        //}
    }
}