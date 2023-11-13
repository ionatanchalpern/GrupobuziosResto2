using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Collections.Specialized;

public partial class cambiarPwd : WebBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentUser == null)
                Response.Redirect("login.aspx");
            else {
                btnCambiarPwd.Text = CurrentUser.TipoUsuario == "O" ? "Aceptar" : "Aceitar";
            }
        }
    }

    protected void btnCambiarPwd_Click(object sender, EventArgs e)
    {
        string tipoUsuario = CurrentUser.TipoUsuario.ToUpper();
        cambiar(CurrentUser.ID, tipoUsuario);
    }

    private void cambiar(int id, string tipo)
    {
        var pwdOld = txtPwdOld.Text;
        using (var dbContext = new ACHEEntities())
        {
            if (tipo == "R")
            {
                var resto = dbContext.Restaurantes.Where(x => x.IDRestaurant == id && x.Pwd == pwdOld).FirstOrDefault();
                if (resto != null)
                {
                    ListDictionary datosResto = new ListDictionary();
                    datosResto.Add("<NOMBRE>", resto.Nombre);
                    datosResto.Add("<PASSWORD>", txtPwdNew.Text);

                    bool send = EmailHelper.SendMessage(EmailTemplate.RecuperoPwdResto, datosResto, resto.Email, "Grupo Buzios Restó: Nova Senha");
                    if (send)
                    {
                        resto.Pwd = txtPwdNew.Text;
                        dbContext.SaveChanges();
                        lblError.Visible = false;
                        lblOk.Visible = true;
                        lblOk.Text = "A senha foi alterada com sucesso<br />";
                    }
                    else
                    {
                        lblOk.Visible = false;
                        lblError.Visible = true;
                        lblError.Text = "Não foi possível modificar, tente novamente<br />";
                    }
                }
                else
                {
                    lblOk.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "Senha atual incorreta";
                }
            }
            else if (tipo == "O")
            {
                var usuario = dbContext.Usuarios.Where(x => x.IDUsuario == id && x.Pwd == pwdOld).FirstOrDefault();
                if (usuario != null)
                {
                    ListDictionary datosUsuario = new ListDictionary();
                    datosUsuario.Add("<NOMBRE>", usuario.Contacto);
                    datosUsuario.Add("<PASSWORD>", txtPwdNew.Text);

                    bool send = EmailHelper.SendMessage(EmailTemplate.RecuperoPwd, datosUsuario, usuario.Email, "Grupo Buzios Restó: Nueva contraseña");
                    if (send)
                    {
                        usuario.Pwd = txtPwdNew.Text;
                        dbContext.SaveChanges();
                        lblError.Visible = false;
                        lblOk.Visible = true;
                        lblOk.Text = "La contraseña se ha modificado correctamente<br />";
                    }
                    else
                    {
                        lblOk.Visible = false;
                        lblError.Visible = true;
                        lblError.Text = "No se ha podido modificar, intente nuevamente<br />";
                    }
                }
                else
                {
                    lblOk.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "Contraseña actual incorrecta";
                }
            }
        }
    }
}