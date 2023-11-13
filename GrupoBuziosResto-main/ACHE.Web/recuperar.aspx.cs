using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Collections.Specialized;

public partial class recuperar : WebBasePage
{
    #region Properties

    private string Mode = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Mode = Request.QueryString["Mode"];
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.ToLower();
        recuperarPwd(email, this.Mode);
    }

    private void recuperarPwd(string email, string modo)
    {
        using (var dbContext = new ACHEEntities())
        {
            string newPwd = string.Empty;
            newPwd = newPwd.GenerateRandom(6);
            ListDictionary datos = new ListDictionary();

            if (modo == "O")
            {
                var usuario = dbContext.Usuarios.Where(x => x.Email == email && x.Activo).FirstOrDefault();
                if (usuario != null)
                {
                    datos.Add("<NOMBRE>", (usuario.Contacto));
                    datos.Add("<PASSWORD>", newPwd);

                    bool send = EmailHelper.SendMessage(EmailTemplate.RecuperoPwd, datos, usuario.Email, "Grupo Buzios Resto: Recuperación de contraseña");
                    if (!send)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Ha ocurrido un error al enviar el email, por favor intente nuevamente.<br /><br />";
                    }
                    else
                    {
                        usuario.Pwd = newPwd;
                        dbContext.SaveChanges();
                        lblError.Visible = false;
                        lblOk.Visible = true;
                        lblOk.Text = "Se le ha enviado su nueva contraseña. <br /><br />";
                        txtEmail.Text = string.Empty;
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "No existe ningun usuario registrado con ese email.<br /><br />";
                }
            }
            else if (modo == "R")
            {
                var resto = dbContext.Restaurantes.Where(x => x.Email == email && x.Activo).FirstOrDefault();
                if (resto != null)
                {
                    datos.Add("<NOMBRE>", (resto.Nombre));
                    datos.Add("<PASSWORD>", newPwd);

                    bool send = EmailHelper.SendMessage(EmailTemplate.RecuperoPwd, datos, resto.Email, "Grupo Buzios Resto: Recuperación de contraseña");
                    if (!send)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Ha ocurrido un error al enviar el email, por favor intente nuevamente.<br /><br />";
                    }
                    else
                    {
                        resto.Pwd = newPwd;
                        dbContext.SaveChanges();
                        lblError.Visible = false;
                        lblOk.Visible = true;
                        lblOk.Text = "Se le ha enviado su nueva contraseña<br /><br />";
                        txtEmail.Text = string.Empty;
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "No existe ningun restaurant registrado con ese email.<br /><br />";
                }
            }
        }
    }
}