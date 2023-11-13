using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;


public partial class validar : WebBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentUser == null || CurrentUser.TipoUsuario == "O")
                Response.Redirect("login.aspx");
        }
    }

    protected void btnValidar_Click(object sender, EventArgs e)
    {
        var codigo = txtCodigo.Text.ToUpper();

        if (codigo.Length > 4 && codigo.StartsWith("GBR-"))
        {
            //    var a = codigoReal[1].ToString();
            //    int leer = 0;

            //    if (a.Count() == 1)
            //        leer = 1;
            //    else if (a.Count() == 2)
            //        leer = 2;
            //    else if (a.Count() == 3)
            //        leer = 3;
            //    else if (a.Count() == 4)
            //        leer = 4;

            validarDetalle(codigo);
        }
        else
            showError("O código digitado não existe<br /><br />");
    }

    private void validarDetalle(string codigo)
    {
        try
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                int codigoReal = 0;
                int? digitoVerificador = null;
                //codigoReal = int.Parse(codigo.Substring(codigo.Length - leer));
                if (codigo.Split("-").Length == 3)
                {
                    codigoReal = int.Parse(Cryptography.Decrypt(codigo.Split("-")[1]));
                    digitoVerificador = int.Parse(codigo.Split("-")[2]);
                }
                else
                {
                    codigoReal = int.Parse(Cryptography.Decrypt(codigo));
                }

                using (var dbContext = new ACHEEntities())
                {
                    var detalle = dbContext.PedidosDetalle.Include("Pedidos").Where(x => x.IDDetalle == codigoReal).FirstOrDefault();
                    if (detalle != null)
                    {
                        #region validacion
                        var dtHasta = detalle.Pedidos.FechaEstadiaHasta.AddDays(2);

                        if (detalle.Validado == true)
                            throw new Exception("Este código já foi validado<br /><br />");
                        if (!(DateTime.Now >= detalle.Pedidos.FechaEstadiaDesde && DateTime.Now <= dtHasta))
                            throw new Exception("O código não corresponde a data da estadia<br /><br />");
                        if (digitoVerificador.HasValue && detalle.DigitoVerficador != digitoVerificador.Value)
                            throw new Exception("Código inválido<br /><br />");

                        #endregion

                        detalle.IDRestauranteValidacion = CurrentUser.ID;
                        detalle.FechaValidacion = DateTime.Now;
                        detalle.Validado = true;
                        dbContext.SaveChanges();
                        lblError.Visible = false;
                        lblOk.Visible = true;
                        lblOk.Text = "Foi validado código<br /><br />";

                        txtCodigo.Text = "";
                    }
                    else
                        throw new Exception("O código digitado não existe<br /><br />");
                }
            }
            else
                throw new Exception("Código inválido<br /><br />");
        }
        catch (Exception e)
        {
            showError(e.Message);
        }
    }

    private void showError(string error)
    {
        lblError.Visible = true;
        lblError.Text = error;
    }
}