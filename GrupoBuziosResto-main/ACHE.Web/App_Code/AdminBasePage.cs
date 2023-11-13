using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ACHE.Model
{
    public class AdminBasePage : System.Web.UI.Page
    {

        public static class RoleNames
        {
            public static readonly string Admin = "Admin";
            public static readonly string Usuario = "Usuario";
        }

        public string TipoPagina = "admin";
        public static bool UsuarioCerro = false;

        public bool KeepScrollPosition
        {
            get
            {
                return (ViewState["KeepScrollPosition"] != null) ? (bool)ViewState["KeepScrollPosition"] : true;
            }

            set
            {
                ViewState["KeepScrollPosition"] = value;
            }
        }

        public WebUser CurrentUser
        {
            get { return (Session["AdminUser"] != null) ? (WebUser)Session["AdminUser"] : null; }
            set { Session["AdminUser"] = value; }
        }

        protected virtual void SetConfigurations()
        {
            MaintainScrollPositionOnPostBack = KeepScrollPosition;
        }

        //private bool UserHasAcces()
        //{
        //    if (CurrentUser.Tipo =="U")
        //        return true;
        //    else
        //        return false;
        //}

        public static void ValidateUser()
        {
            string pageName = HttpContext.Current.Request.FilePath.Substring(HttpContext.Current.Request.FilePath.LastIndexOf(@"/") + 1).ToLower();
            if (pageName != "default.aspx" && pageName != "recuperar-pass.aspx")
            {
                if (HttpContext.Current.Session["AdminUser"] == null)
                {
                    if (UsuarioCerro)
                    {
                        HttpContext.Current.Response.Redirect("~/admin/default.aspx");
                    }
                    else
                    {
                        using (var dbContext = new ACHEEntities())
                        {
                            //if (HttpContext.Current.Request.Cookies["UserToken"] != null)
                            //{
                            //    var token = HttpContext.Current.Request.Cookies["UserToken"].Value;
                            //    var usuarioLogin = dbContext.AuthenticationToken.Where(x => x.Token == token).FirstOrDefault();
                            //    var datosUsuario = dbContext.UsuariosAdmin.Where(x => x.Email == usuarioLogin.IDUsuario).FirstOrDefault();
                            //    if (usuarioLogin != null)
                            //    {
                            //        HttpContext.Current.Session["AdminUser"] = new WebUser(datosUsuario.Email, datosUsuario.Email, datosUsuario.Nombre, "", datosUsuario.IDUsuario, true, datosUsuario.Tipo, "", "");
                            //    }
                            //}
                        }
                    }
                }
                /*else
                {
                    using (var dbContext = new ACHEEntities()) 
                    {
                        if (Context.Request.Cookies["UserToken"] != null)
                        {
                            var token = Context.Request.Cookies["UserToken"].Value;
                            var usuarioLogin = dbContext.AuthenticationToken.Where(x => x.Token == token).FirstOrDefault();
                            var datosUsuario =  dbContext.UsuariosAdmin.Where(x => x.Email == usuarioLogin.IDUsuario).FirstOrDefault();
                            if (usuarioLogin != null) 
                            {
                                Context.Session["AdminUser"] = new WebUser(datosUsuario.Email, datosUsuario.Email, datosUsuario.Nombre, "", datosUsuario.IDUsuario, true, datosUsuario.Tipo, "", "");
                            }
                        }
                    }
                }*/
                //else if (!UserHasAcces())
                //    Response.Redirect("~/login-usuarios.aspx");
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            ValidateUser();
            SetConfigurations();
        }

        public string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
