using ACHE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class admin_modulos_custom_menuese : AdminBasePage
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
            cargarRestaurantes();
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

    private void cargarRestaurantes() 
    {
        using (var dbContext = new ACHEEntities())
        {
            var restaurantes = dbContext.Restaurantes.Where(x => x.Activo).ToList();
            if (restaurantes != null)
            {
                cmbRestaurant.DataSource = restaurantes;
                cmbRestaurant.DataValueField = "IDRestaurant";
                cmbRestaurant.DataTextField = "Nombre";
                cmbRestaurant.DataBind();

                cmbRestaurant.Items.Insert(0, new ListItem("", ""));
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
            var entity = dbContext.Menues.Where(x => x.IDMenu == this.idEntidad).FirstOrDefault();
            if (entity != null)
            {
                cmbRestaurant.SelectedValue = entity.IDRestaurant.ToString();
                cmbTipos.SelectedValue = entity.TipoMenu;
                txtEntrada.Text = entity.Entrada;
                txtPlatoPrincipal.Text = entity.PlatoPrincipal;
                txtPostre.Text = entity.Postre;
                chkBebida.Checked = entity.IncluyeBebida;

                #region imagen
                if (!string.IsNullOrEmpty(entity.Imagen))
                {
                    img1.ImageUrl = "~/files/menues/" + entity.Imagen;
                    lnkEliminarImagen.Visible = true;
                }
                else
                {
                    img1.ImageUrl = "~/files/menues/no-photo.jpg";
                    lnkEliminarImagen.Visible = false;
                }
                #endregion

                #region menu
                if (!string.IsNullOrEmpty(entity.ImagenMenu))
                {
                    imgMenu.ImageUrl = "~/files/menues/" + entity.ImagenMenu;
                    lnkEliminarMenu.Visible = true;
                }
                else
                {
                    imgMenu.ImageUrl = "~/files/menues/no-photo.jpg";
                    lnkEliminarMenu.Visible = false;
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
            Menues entity = null;
            if (this.idEntidad > 0)
                entity = dbContext.Menues.Where(x => x.IDMenu == this.idEntidad).FirstOrDefault();
            else
                entity = new Menues();

            entity.IDRestaurant = int.Parse(cmbRestaurant.SelectedValue);
            entity.TipoMenu = cmbTipos.SelectedValue;
            entity.Entrada = txtEntrada.Text;
            entity.PlatoPrincipal = txtPlatoPrincipal.Text;
            entity.Postre = txtPostre.Text;
            entity.IncluyeBebida = chkBebida.Checked;

            #region imagen
            if (flpImagen.HasFile)
            {
                string ext = System.IO.Path.GetExtension(flpImagen.FileName);
                string uniqueName = "img_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ext;
                string path = System.IO.Path.Combine(Server.MapPath("~/files/menues/"), uniqueName);
                //save the file to our local path
                flpImagen.SaveAs(path);
                entity.Imagen = uniqueName;
            }
            #endregion

            #region menu
            if (flpMenu.HasFile)
            {
                string ext = System.IO.Path.GetExtension(flpMenu.FileName);
                string uniqueName = "menu_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ext;
                string path = System.IO.Path.Combine(Server.MapPath("~/files/menues/"), uniqueName);
                //save the file to our local path
                flpMenu.SaveAs(path);
                entity.ImagenMenu = uniqueName;
            }
            #endregion

            if (this.idEntidad > 0)
                dbContext.SaveChanges();
            else
            {
                dbContext.Menues.Add(entity);
                dbContext.SaveChanges();
            }
        }
    }

    protected void btnAceptar_OnClick(object sender, EventArgs e)
    {
        if (IsValid == true)
        {
            Response.Redirect("menues.aspx");
        }
    }

    protected void btnCancelar_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("menues.aspx");
    }

    private void ShowError(string msg)
    {
        litError.Text = msg;
        pnlError.Visible = true;
    }

    protected void btnEliminarImagen_OnClick(object sender, EventArgs e)
    {
        using (var dbContext = new ACHEEntities())
        {
            Menues entity = dbContext.Menues.Where(x => x.IDMenu == this.idEntidad).FirstOrDefault();
            //Eliminar foto fisica
            if (entity.Imagen != null)
            {
                string path = Server.MapPath("~/files/menues/") + entity.Imagen;
                FileInfo myfileinf = new FileInfo(path);
                myfileinf.Delete();
            }

            entity.Imagen = null;
            img1.ImageUrl = "~/files/menues/no-photo.jpg";

            dbContext.SaveChanges();

            lnkEliminarImagen.Visible = false;
        }
    }

    protected void btnEliminarMenu_OnClick(object sender, EventArgs e)
    {
        using (var dbContext = new ACHEEntities())
        {
            Menues entity = dbContext.Menues.Where(x => x.IDMenu == this.idEntidad).FirstOrDefault();

            //Eliminar foto fisica
            if (entity.ImagenMenu != null)
            {
                string path = Server.MapPath("~/files/menues/") + entity.ImagenMenu;
                FileInfo myfileinf = new FileInfo(path);
                myfileinf.Delete();
            }

            entity.ImagenMenu = null;
            imgMenu.ImageUrl = "~/files/menues/no-photo.jpg";

            dbContext.SaveChanges();

            lnkEliminarMenu.Visible = false;
        }
    }
}

