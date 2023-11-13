using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Configuration;
using ACHE.Model;
using ACHE.Extensions;
using System.Web.Services;

public partial class traslados_paso2 : WebBasePage
{
    private int idPedido;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentUser == null)
                Response.Redirect("/login-traslados.aspx");
            else
                divPagoPor.Visible = CurrentUser.PagoPorHabilitado;

            int idServicio = TrasladosCart.RetrieveTrasladosCart().IDServicio;
            if(TrasladosCart.RetrieveTrasladosCart().NroFile!=string.Empty){
                txtNroFile.Text = TrasladosCart.RetrieveTrasladosCart().NroFile;
                txtPagoPor.Text = TrasladosCart.RetrieveTrasladosCart().PagoPor;
            }
            this.idPedido = Request.QueryString["Id"] != null ? int.Parse(Request.QueryString["Id"]) : 0;
            if (this.idPedido > 0)
                hdnIDPedido.Value = idPedido.ToString();

            using (var dbContext = new ACHEEntities())
            {
                var servicio = dbContext.Servicios.Where(x => x.IDServicio == idServicio).FirstOrDefault();
                hdnTipoServicio.Value = servicio.SubTipos.Tipo;
                if (this.idPedido > 0)
                {
                    var pedido = dbContext.PedidosTraslado.Where(x=>x.IDPedidoTraslado==idPedido).FirstOrDefault();
                    this.txtNroFile.Text = pedido.NroFile;
                    this.txtPagoPor.Text = pedido.PagoPor != null ? pedido.PagoPor : "";
                }
            }
          
        }
    }
      [WebMethod(true)]
    public static void guardar(string info,string nroFile,string pagoPor)
    {
        TrasladosCart.RetrieveTrasladosCart().Items.Clear();

        string Nombre = "";
        string DNI = "";
        var filas = info.Split("%").ToList();
        for (int i = 1; i < filas.Count(); i++)
        {
           
            var fila = filas[i].Split("-").ToList();
            for (int j = 0; j < fila.Count(); j++)
            {
                var valor = fila[j].Split("#").ToList();
                switch (valor[0])
                {
                    case "txtNombre":
                        Nombre = valor[1];
                        break;
                    case "txtDni":
                        DNI = valor[1];
                        break;
                   
                }
            }

            TrasladosCart.RetrieveTrasladosCart().add(Nombre, DNI);
            TrasladosCart.RetrieveTrasladosCart().NroFile = nroFile;

            if(!string.IsNullOrEmpty(pagoPor))
                TrasladosCart.RetrieveTrasladosCart().PagoPor = pagoPor;
        }
    }


    [System.Web.Services.WebMethod(true)]
    public static string generarTabla(int idPedido)
    {
        var html = string.Empty;
        using (var dbContext = new ACHEEntities())
        {

            int CantPersonas=0;
            int CantPersonas2=0;
            if (idPedido > 0 && TrasladosCart.RetrieveTrasladosCart().Items.Count() == 0)
            {
                var pedido = dbContext.PedidosTraslado.Where(x => x.IDPedidoTraslado == idPedido).FirstOrDefault();
                if (pedido != null )
                {

                    CantPersonas2 = pedido.CantAdultos + pedido.CantMenoresAsiento + pedido.CantMenoresGratis;
                    if (TrasladosCart.RetrieveTrasladosCart()!=null)
                    {
                        CantPersonas = TrasladosCart.RetrieveTrasladosCart().CantAdultos + TrasladosCart.RetrieveTrasladosCart().Cantmenores + TrasladosCart.RetrieveTrasladosCart().Cantmenores2;
                    }
                    if (CantPersonas == CantPersonas2)
                    {
                        foreach(var item in pedido.PasajerosPorPedidoTraslado)
                        {
                            html += "<tr class=\"selectPasajeros\" id=\"%\">";
                            html += "<td> <input id=\"txtNombre\" class=\"selectPasajeros\"  type=\"textbox\" value=\"" + item.Nombre + "\" /></td>";
                            html += "<td> <input id=\"txtDni\" class=\"selectPasajeros\" maxlength='50'  type=\"textbox\" value=\"" + item.DNI + "\" /></td>";
                            html += "</tr>";
                        }
                    }else if (CantPersonas > CantPersonas2)  {
                        foreach (var item in pedido.PasajerosPorPedidoTraslado)
                        {
                            html += "<tr class=\"selectPasajeros\" id=\"%\">";
                            html += "<td> <input id=\"txtNombre\" class=\"selectPasajeros\" type=\"textbox\" value=\"" + item.Nombre + "\" /></td>";
                            html += "<td> <input id=\"txtDni\" class=\"selectPasajeros\" maxlength='50'  type=\"textbox\" value=\"" + item.DNI + "\" /></td>";
                            html += "</tr>";
                        }
                        for (int i = 0; i < (CantPersonas - CantPersonas2); i++)
                        {
                            html += "<tr class=\"selectPasajeros\" id=\"%\">";
                            html += "<td> <input id=\"txtNombre\" class=\"selectPasajeros\"  type=\"textbox\" /></td>";
                            html += "<td> <input id=\"txtDni\" class=\"selectPasajeros\"  maxlength='50' type=\"textbox\" /></td>";
                            html += "</tr>";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < CantPersonas; i++)
                        {
                            html += "<tr class=\"selectPasajeros\" id=\"%\">";
                            html += "<td> <input id=\"txtNombre\" class=\"selectPasajeros\" type=\"textbox\" /></td>";
                            html += "<td> <input id=\"txtDni\" class=\"selectPasajeros\" type=\"textbox\" maxlength='50' /></td>";
                            html += "</tr>";
                        }
                    }
                }
            }
            else
            {   
                CantPersonas = TrasladosCart.RetrieveTrasladosCart().CantAdultos + TrasladosCart.RetrieveTrasladosCart().Cantmenores + TrasladosCart.RetrieveTrasladosCart().Cantmenores2;
                if( TrasladosCart.RetrieveTrasladosCart().Items.Count()>0){
                    if (TrasladosCart.RetrieveTrasladosCart().Items.Count() == CantPersonas)
                    {
                        foreach (var item in TrasladosCart.RetrieveTrasladosCart().Items)
                        {
                            html += "<tr class=\"selectPasajeros\" id=\"%\">";
                            html += "<td> <input id=\"txtNombre\" class=\"selectPasajeros\" type=\"textbox\" value=\"" + item.Nombre + "\" /></td>";
                            html += "<td> <input id=\"txtDni\" class=\"selectPasajeros\" type=\"textbox\"  value=\"" + item.DNI + "\" maxlength='50' /></td>";
                            html += "</tr>";
                        }
                    }
                    else if (CantPersonas > TrasladosCart.RetrieveTrasladosCart().Items.Count())
                    {
                        foreach (var item in TrasladosCart.RetrieveTrasladosCart().Items)
                        {
                            html += "<tr class=\"selectPasajeros\" id=\"%\">";
                            html += "<td> <input id=\"txtNombre\" class=\"selectPasajeros\" type=\"textbox\" value=\"" + item.Nombre + "\" /></td>";
                            html += "<td> <input id=\"txtDni\" class=\"selectPasajeros\" maxlength='50' type=\"textbox\"  value=\"" + item.DNI + "\"  /></td>";
                            html += "</tr>";
                        }
                        for (int i = 0; i < (CantPersonas - TrasladosCart.RetrieveTrasladosCart().Items.Count()); i++)
                        {
                            html += "<tr class=\"selectPasajeros\" id=\"%\">";
                            html += "<td> <input id=\"txtNombre\" class=\"selectPasajeros\"  type=\"textbox\" /></td>";
                            html += "<td> <input id=\"txtDni\" class=\"selectPasajeros\"  maxlength='50' type=\"textbox\" /></td>";
                            html += "</tr>";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < CantPersonas; i++)
                        {
                            html += "<tr class=\"selectPasajeros\" id=\"%\">";
                            html += "<td> <input id=\"txtNombre\" class=\"selectPasajeros\" type=\"textbox\" /></td>";
                            html += "<td> <input id=\"txtDni\" class=\"selectPasajeros\" maxlength='50'  type=\"textbox\" /></td>";
                            html += "</tr>";
                        }
                    }
                }else{
                    for (int i = 0; i < CantPersonas; i++)
                    {
                        html += "<tr class=\"selectPasajeros\" id=\"%\">";
                        html += "<td> <input id=\"txtNombre\" class=\"selectPasajeros\" type=\"textbox\" /></td>";
                        html += "<td> <input id=\"txtDni\" class=\"selectPasajeros\" maxlength='50'  type=\"textbox\" /></td>";
                        html += "</tr>";
                    }
                }
            }

        }
        return html;
    }
}