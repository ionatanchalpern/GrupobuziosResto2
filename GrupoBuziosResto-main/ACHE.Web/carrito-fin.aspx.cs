using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Collections.Specialized;
using System.Configuration;

public partial class carrito_fin : WebBasePage {

    #region Properties

    private int ultimoPedidoId;

    #endregion

    protected void Page_Load(object sender, EventArgs e) {
        ultimoPedidoId = int.Parse(Request.QueryString["IdPedido"]);
        if (!IsPostBack) {
            if (CurrentUser == null || CurrentUser.TipoUsuario != "O")
                Response.Redirect("login.aspx");
            if (ultimoPedidoId == 0)
                Response.Redirect("default.aspx");
        }
    }

    protected void btnEnviarMail_Click(object sender, EventArgs e) {
        EnviarMailPedido(ultimoPedidoId, CurrentUser.Nombre, CurrentUser.Email);
    }

    protected void btnImprimirCupones_Click(object sender, EventArgs e) {
        imprimirCupones();
    }

    private void imprimirCupones() {
        Response.Redirect("pedidoPrint.aspx?Id=" + ultimoPedidoId);
    }

    private void EnviarMailPedido(int idPedido, string nombreUsuario, string emailUsuario) {
        try {
            ListDictionary replacements = new ListDictionary();

            replacements.Add("<IDPEDIDO>", idPedido);
            replacements.Add("<NOMBRE>", nombreUsuario);

            string detalle = string.Empty;
            using (var dbContext = new ACHEEntities()) {
                var detalles = dbContext.PedidosDetalle.Where(x => x.IDPedido == idPedido).ToList();
                if (detalles != null) {
                    foreach (var item in detalles) {
                        detalle += "<tr>";

                        if (item.Productos.Tipo == "T")
                            detalle += "<td style='text-align:left'>Turista</td>";
                        else if (item.Productos.Tipo == "P")
                            detalle += "<td style='text-align:left'>Premium</td>";
                        else if (item.Productos.Tipo == "C")
                            detalle += "<td style='text-align:left'>Clasico</td>";
                        else if (item.Productos.Tipo == "M")
                            detalle += "<td style='text-align:left'>Menores</td>";
                      
                        detalle += "<td style='text-align:right'>" + (item.PrecioOperador).ToString("N2") + "</td>";

                        detalle += "</tr>";
                    }
                    replacements.Add("<DETALLE>", detalle);

                    bool send = EmailHelper.SendMessage(EmailTemplate.Pedido, replacements, emailUsuario, ConfigurationManager.AppSettings["Email.Pedidos"], "GrupoBuziosResto: Pedido realizado");
                    if (!send) {
                        throw new Exception("Ha ocurrido un error al enviar el email, por favor intente nuevamente.<br /><br />");
                    }
                    else {
                        lblError.Visible = false;
                        lblOk.Visible = true;
                        lblOk.Text = "Se le ha enviado un mail con los datos del pedido.<br /><br />";
                    }
                }
            }
        }
        catch (Exception e) {
            showError(e.Message);
        }
    }

    private void showError(string exception) {
        lblError.Visible = true;
        lblError.Text = exception;
    }
}