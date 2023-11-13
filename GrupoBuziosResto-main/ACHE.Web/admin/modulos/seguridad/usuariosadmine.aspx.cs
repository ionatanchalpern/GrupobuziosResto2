using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Configuration;

public partial class admin_modulos_seguridad_usuariosadmine : AdminBasePage
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
            if (CurrentUser.TipoUsuario != "A")
                Response.Redirect("~/admin/home.aspx");

            switch (this.Mode.ToUpper()) {
                case "A":
                    litModo.Text = litModo2.Text = "Creación";
                    break;
                case "E":
                    rqvPwd.Enabled = false;
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
            var entity = dbContext.UsuariosAdmin.Where(x => x.IDUsuario == this.idEntidad).FirstOrDefault();
            if (entity != null)
            {
                txtNombre.Text = entity.Nombre;
                txtEmail.Text = entity.Email;
                chkActivo.Checked = entity.Activo;
                ddlTipo.SelectedValue = entity.Tipo;
            }
            else
                throw new Exception("Entidad inexistente");
        }
    }

    private void CreateEntity()
    {
        using (var dbContext = new ACHEEntities())
        {
            UsuariosAdmin entity = null;
            if (this.idEntidad > 0)
                entity = dbContext.UsuariosAdmin.Where(x => x.IDUsuario == this.idEntidad).FirstOrDefault();
            else
            {
                if(dbContext.UsuariosAdmin.Any(x =>x.Nombre.ToLower() == txtNombre.Text.ToLower()))
                    throw new Exception("El nombre de usuario ya se encuentra registrado");
                if (dbContext.UsuariosAdmin.Any(x => x.Email.ToLower() == txtEmail.Text.ToLower()))
                    throw new Exception("El email ya se encuentra registrado");
                
                entity = new UsuariosAdmin();
            }

            entity.Nombre = txtNombre.Text;
            entity.Email = txtEmail.Text;
            entity.Activo = chkActivo.Checked;
            entity.Tipo = ddlTipo.SelectedValue;
            if (txtPwd.Text != string.Empty)
                entity.Pwd = txtPwd.Text;

            if (this.idEntidad > 0)
                dbContext.SaveChanges();
            else
            {
                dbContext.UsuariosAdmin.Add(entity);
                dbContext.SaveChanges();
            }
        }
    }

    protected void btnAceptar_OnClick(object sender, EventArgs e)
    {
        if (IsValid == true)
        {
            Response.Redirect("usuariosadmin.aspx");
        }
    }

    protected void btnCancelar_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("usuariosadmin.aspx");
    }

    private void ShowError(string msg)
    {
        litError.Text = msg;
        pnlError.Visible = true;
    }
}