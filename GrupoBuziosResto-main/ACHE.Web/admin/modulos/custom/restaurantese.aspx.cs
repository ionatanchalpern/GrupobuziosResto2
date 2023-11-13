using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class admin_modulos_custom_restaurantese : AdminBasePage
{
    #region Properties

    private string Mode = string.Empty;

    private int idEntidad
    {
        get
        {
            if (ViewState["idEntidad"] != null)
                return (int)ViewState["idEntidad"];
            else
                return 0;
        }
        set { ViewState["idEntidad"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Mode = Request.QueryString["Mode"];
        if (this.Mode != "A")
            this.idEntidad = int.Parse(Request.QueryString["Id"]);

        if (!IsPostBack)
        {
            switch (this.Mode.ToUpper()) {
                case "A":
                    litModo.Text = litModo2.Text = "Creación";
                    break;
                case "E":
                    litModo.Text = litModo2.Text = "Edición";
                    LoadEntity();
                    break;
                default:
                    throw new Exception("Parametros incorrectos");
            }
        }
    }

    protected void ServerValidate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = Process();
    }

    private bool Process()
    {
        bool value = false;

        if (IsValid)
        {
            try
            {
                CreateEntity();
                value = true;
            }
            catch (Exception e)
            {
                value = false;
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                BasicLog.AppendToFile(Server.MapPath(ConfigurationManager.AppSettings["BasicLogError"]), msg, e.ToString());
                ShowError(e.Message);
            }
        }
        return value;
    }

    private void LoadEntity()
    {
        using (var dbContext = new ACHEEntities())
        {
            var entity = dbContext.Restaurantes.Where(x => x.IDRestaurant == this.idEntidad).FirstOrDefault();
            if (entity != null)
            {
                txtNombre.Text = entity.Nombre;
                txtEmail.Text = entity.Email;
                txtPwd.Text = entity.Pwd;
                txtDireccion.Text = entity.Direccion;
                txtCiudad.Text = entity.Ciudad;
                txtAtencion.Text = entity.HorarioAtencion;
                txtTelefono.Text = entity.Telefono;
                chkActivo.Checked = entity.Activo;
                txtObservaciones.Text = entity.Observaciones;

                #region logo
                if (!string.IsNullOrEmpty(entity.Logo))
                {
                    imgLogo.ImageUrl = "~/files/logos/" + entity.Logo;
                    lnkEliminarLogo.Visible = true;
                }
                else
                {
                    img1.ImageUrl = "~/files/logos/no-photo.jpg";
                    lnkEliminarLogo.Visible = false;
                }
                #endregion

                #region fotos
                if (!string.IsNullOrEmpty(entity.Imagen1))
                {
                    img1.ImageUrl = "~/files/restaurantes/" + entity.Imagen1;
                    lnkEliminar1.Visible = true;
                }
                else
                {
                    img1.ImageUrl = "~/files/restaurantes/no-photo.jpg";
                    lnkEliminar1.Visible = false;
                }
                if (!string.IsNullOrEmpty(entity.Imagen2))
                {
                    img2.ImageUrl = "~/files/restaurantes/" + entity.Imagen2;
                    lnkEliminar2.Visible = true;

                }
                else
                {
                    img2.ImageUrl = "~/files/restaurantes/no-photo.jpg";
                    lnkEliminar2.Visible = false;
                }
                if (!string.IsNullOrEmpty(entity.Imagen3))
                {
                    img3.ImageUrl = "~/files/restaurantes/" + entity.Imagen3;
                    lnkEliminar3.Visible = true;
                }
                else
                {
                    img3.ImageUrl = "~/files/restaurantes/no-photo.jpg";
                    lnkEliminar3.Visible = false;
                }
                #endregion

                #region mapa
                if (!string.IsNullOrEmpty(entity.ImagenMapa))
                {
                    imgMapa.ImageUrl = "~/files/restaurantes/" + entity.ImagenMapa;
                    lnkEliminarMapa.Visible = true;
                }
                else
                {
                    imgMapa.ImageUrl = "~/files/restaurantes/no-photo.jpg";
                    lnkEliminarMapa.Visible = false;
                }
                #endregion


            }
            else
                throw new Exception("Entidad inexistente");
        }
    }

    private void CreateEntity()
    {
        using (var dbContext = new ACHEEntities())
        {
            Restaurantes entity = null;
            if (this.idEntidad > 0)
                entity = dbContext.Restaurantes.Where(x => x.IDRestaurant == this.idEntidad).FirstOrDefault();
            else
                entity = new Restaurantes();

            entity.Nombre = txtNombre.Text;
            entity.Email = txtEmail.Text;
            entity.Pwd = txtPwd.Text;
            entity.Direccion = txtDireccion.Text;
            entity.Ciudad = txtCiudad.Text;
            entity.HorarioAtencion = txtAtencion.Text;
            entity.Telefono = txtTelefono.Text;
            entity.FechaAlta = DateTime.Now;
            entity.Activo = chkActivo.Checked;
            entity.Observaciones = txtObservaciones.Text;

            #region logo
            if (flpLogo.HasFile)
            {
                string ext = System.IO.Path.GetExtension(flpLogo.FileName);
                string uniqueName = "logo_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ext;
                string path = System.IO.Path.Combine(Server.MapPath("~/files/logos/"), uniqueName);
                //save the file to our local path
                flpLogo.SaveAs(path);
                entity.Logo = uniqueName;
            }
            #endregion

            #region fotos
            if (flpImagen1.HasFile)
            {
                string ext = System.IO.Path.GetExtension(flpImagen1.FileName);
                string uniqueName = "1_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ext;
                string path = System.IO.Path.Combine(Server.MapPath("~/files/restaurantes/"), uniqueName);
                //save the file to our local path
                flpImagen1.SaveAs(path);
                entity.Imagen1 = uniqueName;
            }

            if (flpImagen2.HasFile)
            {
                string ext = System.IO.Path.GetExtension(flpImagen2.FileName);
                string uniqueName = "2_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ext;
                string path = System.IO.Path.Combine(Server.MapPath("~/files/restaurantes/"), uniqueName);
                //save the file to our local path
                flpImagen2.SaveAs(path);
                entity.Imagen2 = uniqueName;
            }

            if (flpImagen3.HasFile)
            {
                string ext = System.IO.Path.GetExtension(flpImagen3.FileName);
                string uniqueName = "3_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ext;
                string path = System.IO.Path.Combine(Server.MapPath("~/files/restaurantes/"), uniqueName);
                //save the file to our local path
                flpImagen3.SaveAs(path);
                entity.Imagen3 = uniqueName;
            }
            #endregion

            #region mapa
            if (flpMapa.HasFile)
            {
                string ext = System.IO.Path.GetExtension(flpMapa.FileName);
                string uniqueName = "mapa_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ext;
                string path = System.IO.Path.Combine(Server.MapPath("~/files/restaurantes/"), uniqueName);
                //save the file to our local path
                flpMapa.SaveAs(path);
                entity.ImagenMapa = uniqueName;
            }
            #endregion

            if (this.idEntidad > 0)
                dbContext.SaveChanges();
            else
            {
                dbContext.Restaurantes.Add(entity);
                dbContext.SaveChanges();
            }
        }
    }

    protected void btnAceptar_OnClick(object sender, EventArgs e)
    {
        if (IsValid == true)
        {
            Response.Redirect("restaurantes.aspx");
        }
    }

    protected void btnCancelar_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("restaurantes.aspx");
    }

    private void ShowError(string msg)
    {
        litError.Text = msg;
        pnlError.Visible = true;
    }

    protected void btnEliminarLogo_OnClick(object sender, EventArgs e)
    {
        using (var dbContext = new ACHEEntities())
        {
            Restaurantes entity = dbContext.Restaurantes.Where(x => x.IDRestaurant == this.idEntidad).FirstOrDefault();

            //Eliminar foto fisica
            if (entity.Logo != null)
            {
                string path = Server.MapPath("~/files/logos/") + entity.Logo;
                FileInfo myfileinf = new FileInfo(path);
                myfileinf.Delete();
            }

            entity.Logo = null;
            imgLogo.ImageUrl = "~/files/logos/no-photo.jpg";

            dbContext.SaveChanges();

            lnkEliminarLogo.Visible = false;
        }
    }

    protected void btnEliminar1_OnClick(object sender, EventArgs e)
    {
        using (var dbContext = new ACHEEntities())
        {
            Restaurantes entity = dbContext.Restaurantes.Where(x => x.IDRestaurant == this.idEntidad).FirstOrDefault();
            //Eliminar foto fisica
            if (entity.Imagen1 != null)
            {
                string path = Server.MapPath("~/files/restaurantes/") + entity.Imagen1;
                FileInfo myfileinf = new FileInfo(path);
                myfileinf.Delete();
            }

            entity.Imagen1 = null;
            img1.ImageUrl = "~/files/restaurantes/no-photo.jpg";

            dbContext.SaveChanges();

            lnkEliminar1.Visible = false;
        }
    }

    protected void btnEliminar2_OnClick(object sender, EventArgs e)
    {
        using (var dbContext = new ACHEEntities())
        {
            Restaurantes entity = dbContext.Restaurantes.Where(x => x.IDRestaurant == this.idEntidad).FirstOrDefault();
            //Eliminar foto fisica
            if (entity.Imagen2 != null)
            {
                string path = Server.MapPath("~/files/restaurantes/") + entity.Imagen2;
                FileInfo myfileinf = new FileInfo(path);
                myfileinf.Delete();
            }

            entity.Imagen2 = null;
            img2.ImageUrl = "~/files/restaurantes/no-photo.jpg";

            dbContext.SaveChanges();

            lnkEliminar2.Visible = false;
        }
    }

    protected void btnEliminar3_OnClick(object sender, EventArgs e)
    {
        using (var dbContext = new ACHEEntities())
        {
            Restaurantes entity = dbContext.Restaurantes.Where(x => x.IDRestaurant == this.idEntidad).FirstOrDefault();
            //Eliminar foto fisica
            if (entity.Imagen3 != null)
            {
                string path = Server.MapPath("~/files/restaurantes/") + entity.Imagen3;
                FileInfo myfileinf = new FileInfo(path);
                myfileinf.Delete();
            }

            entity.Imagen3 = null;
            img3.ImageUrl = "~/files/restaurantes/no-photo.jpg";

            dbContext.SaveChanges();

            lnkEliminar3.Visible = false;
        }
    }

    protected void btnEliminarMapa_OnClick(object sender, EventArgs e)
    {
        using (var dbContext = new ACHEEntities())
        {
            Restaurantes entity = dbContext.Restaurantes.Where(x => x.IDRestaurant == this.idEntidad).FirstOrDefault();

            //Eliminar foto fisica
            if (entity.ImagenMapa != null)
            {
                string path = Server.MapPath("~/files/restaurantes/") + entity.ImagenMapa;
                FileInfo myfileinf = new FileInfo(path);
                myfileinf.Delete();
            }

            entity.ImagenMapa = null;
            imgMapa.ImageUrl = "~/files/restaurantes/no-photo.jpg";

            dbContext.SaveChanges();

            lnkEliminarMapa.Visible = false;
        }
    }


}

