using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using System.Configuration;

public partial class admin_default : AdminBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUser = null;
    }

    protected void Login(object sender, EventArgs e)
    {

        //if (txtUsuario.Text == ConfigurationManager.AppSettings["Admin.User"] && txtPwd.Text == ConfigurationManager.AppSettings["Admin.Pwd"])
        //{
        //    CurrentUser = new WebUser("admin", "admin", "", "", 1, true, "A", "", "");

        //    var returnUrl = Request.QueryString["returnUrl"];
        //    if (string.IsNullOrEmpty(returnUrl))
        //    {
        //        Response.Redirect("home.aspx");
        //    }
        //    else
        //    {
        //        Response.Redirect(returnUrl);
        //    }
        //}
        //else
        //    pnlLoginError.Visible = true;

        using (var dbContext = new ACHEEntities())
        {
            var usuario = dbContext.UsuariosAdmin.Where(u => u.Email == txtUsuario.Text && u.Pwd == txtPwd.Text && u.Activo).FirstOrDefault();
            if (usuario != null)
            {
                //bool guardarUsuario = chkRecordarme.Checked;
                //FormsAuthentication.SetAuthCookie(usuario.Email, false);

                var token = Guid.NewGuid().ToString();
                var id = txtUsuario.Text;
                var pwd = txtPwd.Text;
                var expira = DateTime.Now.AddHours(6);

                //almacenarLogin(id, pwd, token, expira);

                CurrentUser = new WebUser(usuario.Nombre, usuario.Email, "", "", usuario.IDUsuario, true, usuario.Tipo, "", "", false, "resto");

                /*AdminBasePage.UsuarioCerro = false;

                HttpCookie myCookie = new HttpCookie("UserToken");
                myCookie.Value = token;
                myCookie.Expires = DateTime.Now.AddHours(6);
                Response.Cookies.Add(myCookie);*/

                var returnUrl = Request.QueryString["returnUrl"];
                if (string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect("home.aspx");
                }
                else
                {
                    Response.Redirect(returnUrl);
                }
            }
            else
                pnlLoginError.Visible = true;
        }
    }

    /*private void almacenarLogin(string idUsuario, string pwd, string token, DateTime expira)
    {
        using (var dbContext = new ACHEEntities())
        {
            var datosLogin = new AuthenticationToken();
            datosLogin.IDUsuario = idUsuario;
            datosLogin.Token = token;
            datosLogin.Pwd = pwd;
            datosLogin.FechaExpiracion = expira;
            datosLogin.FechaUltLogin = DateTime.Now;

            dbContext.AuthenticationToken.Add(datosLogin);
            dbContext.SaveChanges();
        }
    }*/

}
