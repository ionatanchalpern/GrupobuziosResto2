using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ACHE.Model {
    public class WebBasePage : System.Web.UI.Page {

        public string TipoPagina = "front";
        public static bool UsuarioCerro = false;

        public WebUser CurrentUser {
            get { return (Session["CurrentUser"] != null) ? (WebUser)Session["CurrentUser"] : null; }
            set { Session["CurrentUser"] = value; }
        }

        public string TipoSitio {
            get { return (Session["TipoSitio"] != null) ? (string)Session["TipoSitio"] : "resto"; }
            set { Session["TipoSitio"] = value; }
        }

        //public WebUser CurrentVendedor {
        //    get { return (Session["CurrentVendedor"] != null) ? (WebUser)Session["CurrentVendedor"] : null; }
        //    set { Session["CurrentVendedor"] = value; }
        //}

        protected override void OnPreInit(EventArgs e) {
            ValidateUser();
            SetConfigurations();
        }

        protected virtual void SetConfigurations() {
            //MaintainScrollPositionOnPostBack = true;
        }

        public string GetUserIP() {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipList)) {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }

        private void ValidateUser() {
            string pageName = Request.FilePath.Substring(Request.FilePath.LastIndexOf(@"/") + 1).ToLower();
            if (pageName != "default.aspx" && pageName != "recuperar.aspx" && pageName != "quienes-somos.aspx" && pageName != "contacto.aspx" && pageName != "registro.aspx" && pageName != "login.aspx" && pageName != "login-traslados.aspx" && pageName != "traslados-paso1.aspx" && pageName != "restaurant.aspx")
            {
                if (CurrentUser == null) {
                    if (UsuarioCerro) {
                        Response.Redirect("default.aspx");
                    }
                    else {
                        using (var dbContext = new ACHEEntities()) {
                            //if (Context.Request.Cookies["UserTokenFront"] != null)
                            //{
                            //    var token = Context.Request.Cookies["UserTokenFront"].Value;
                            //    var usuarioLogin = dbContext.AuthenticationToken.Where(x => x.Token == token).FirstOrDefault();
                            //    var datosUsuario = dbContext.Usuarios.Where(x => x.Email == usuarioLogin.IDUsuario).FirstOrDefault();
                            //    if (datosUsuario == null)
                            //    {
                            //        var datosResto = dbContext.Restaurantes.Where(x => x.Email == usuarioLogin.IDUsuario).FirstOrDefault();
                            //        if (datosResto != null)
                            //        {
                            //            CurrentUser = new WebUser(datosResto.Nombre, datosResto.Email, datosResto.Nombre, "", datosResto.IDRestaurant, false, "R", datosResto.Telefono, "");
                            //        }
                            //    }
                            //    else
                            //        CurrentUser = new WebUser(datosUsuario.Empresa, datosUsuario.Email, datosUsuario.Contacto, "", datosUsuario.IDUsuario, false, "O", datosUsuario.Telefono, "");
                            //}
                        }
                    }
                }
            }
        }
    }
}
