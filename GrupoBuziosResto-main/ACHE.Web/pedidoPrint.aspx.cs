using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;
using System.Text;
using System.Net;
using Pechkin;
using System.IO;

public partial class pedidoPrint : WebBasePage {

    public string texto;

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            var session = string.IsNullOrEmpty(Request.QueryString["session"]) ? true : false;
            if (CurrentUser != null || !session) {
                int idPedido = string.IsNullOrEmpty(Request.QueryString["Id"]) ? 0 : int.Parse(Request.QueryString["Id"]);
                if (idPedido != 0)
                    generarDetallePedido(idPedido, session);
                else {
                    if (Request.UrlReferrer != null)
                        Response.Redirect(Request.UrlReferrer.ToString());
                    else
                        Response.Redirect("default.aspx");
                }
            }
            else
                Response.Redirect("login.aspx");
        }
    }

    private void generarDetallePedido(int idPedido, bool session) {
        if (!session)
            btnDescargar.Visible = false;

        using (var dbContext = new ACHEEntities()) {
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var textoBeneficio = dbContext.Parametros.FirstOrDefault().TextoBeneficios;
            if (!string.IsNullOrEmpty(textoBeneficio)) {
                litBeneficios.Text = textoBeneficio;
            }

            var restoTurismo = "";
         //   var restoPremium = "";
            var restoClasico = "";
            var restoPlaya = "";

            var restoKids = "";
            var restoLibre= "";
			var restoTotal = "";

            var listFinal = new List<VouchersViewModel>();

            foreach (var resto in dbContext.Menues.Include("Restaurantes").Where(x => x.TipoMenu == "T" && x.Restaurantes.Activo).ToList())
                restoTurismo += resto.Restaurantes.Nombre + " - ";
            foreach (var resto in dbContext.Menues.Include("Restaurantes").Where(x => x.TipoMenu == "L" && x.Restaurantes.Activo).ToList())
                restoLibre += resto.Restaurantes.Nombre + " - ";
            foreach (var resto in dbContext.Menues.Include("Restaurantes").Where(x => x.TipoMenu == "B" && x.Restaurantes.Activo).ToList())
                restoPlaya += resto.Restaurantes.Nombre + " - ";
            foreach (var resto in dbContext.Menues.Include("Restaurantes").Where(x => x.TipoMenu == "M" && x.Restaurantes.Activo).ToList())
                restoKids += resto.Restaurantes.Nombre + " - ";
            foreach (var resto in dbContext.Menues.Include("Restaurantes").Where(x => x.TipoMenu == "C" && x.Restaurantes.Activo).ToList())
                restoClasico += resto.Restaurantes.Nombre + " - ";
            foreach (var resto in dbContext.Restaurantes.Where(x => x.Activo && x.Menues.Any()).OrderBy(x=>x.Nombre).ToList())
                restoTotal += resto.Nombre + " - ";

            var detallesTurista = dbContext.PedidosDetalle.Include("Pedidos").Include("Productos")
                .Where(x => x.IDPedido == idPedido && x.Productos.Tipo == "T").ToList()
                .Select(x => new VouchersViewModel() {
                    Codigo = x.IDDetalle,
                    DigitoVerificador = x.DigitoVerficador,
                    Nombre = x.Pedidos.Pasajero,
                    NroDocumento = x.Pedidos.NroDocumento,
                    FechaDesde = x.Pedidos.FechaEstadiaDesde,
                    FechaHasta = x.Pedidos.FechaEstadiaHasta,
                    //Entrada = x.Productos.Entrada.RemoverAcentos(),
                    //Principal = x.Productos.PlatoPrincipal.RemoverAcentos(),
                    //Postre = x.Productos.Postre.RemoverAcentos(),
                    //Bebida = x.Productos.IncluyeBebida ? "Incluye (1) Bebida" : "NO incluye Bebida",
                    Asociados = restoTurismo,
                    Ubicacion = "<strong>CENTRO DE BÚZIOS:</strong>  BUZIN - LA DOLCE VITA - ENU SUSHI - NOI - PARVATI - MINEIRO GRILL -  BASTIDORES. <strong>JOAO FERNANDES: </strong> LA PLAGE ( BEACH CLUB).<strong> ORLA BARDOT:</strong> MATAHARI - RINCON - DO JEITO BUZIANO - LA BARDOT. <strong>FERRADURA:</strong> SAN TELMO (SOBRE AV. PRINCIPAL). <strong>GERIBÁ:</strong> QUIOSQUE DO MINEIRO (SOBRE AV. PRINCIPAL)",
                    TipoMenu = "TURISTA",
                    Texto = "Men&uacute; Turista: " + x.Productos.Entrada.RemoverAcentos() + " + " + x.Productos.PlatoPrincipal.RemoverAcentos() + " + " + x.Productos.Postre.RemoverAcentos() + " " + (x.Productos.IncluyeBebida ? "Incluye (1) Bebida." : "NO incluye Bebida."),
                    Excepto = "",
                    Excepciones = "<strong>Quiosque do Mineiro  de 12 a 16HS. </strong>",
                    HorarioAtencion = "Horario de atenci&oacute;n: 12 a 22:00 hs.",
                    HorarioAtencionDatosUtiles = "<strong>HORARIO DE ATENCI&Oacute;N:</strong> Todos los restaurantes asociados a Grupo B&uacute;zios Rest&oacute; atienden de 12 a 22:00HS."
                }).ToList();

            if (detallesTurista.Any())
                listFinal.AddRange(detallesTurista);

         /*   var detallesPremium = dbContext.PedidosDetalle.Include("Pedidos").Include("Productos")
                .Where(x => x.IDPedido == idPedido && x.Productos.Tipo == "P").ToList()
                .Select(x => new VouchersViewModel() {
                    Codigo = x.IDDetalle,
                    DigitoVerificador = x.DigitoVerficador,
                    Nombre = x.Pedidos.Pasajero,
                    NroDocumento = x.Pedidos.NroDocumento,
                    FechaDesde = x.Pedidos.FechaEstadiaDesde,
                    FechaHasta = x.Pedidos.FechaEstadiaHasta,
                    //Entrada = x.Productos.Entrada.RemoverAcentos(),
                    //Principal = x.Productos.PlatoPrincipal.RemoverAcentos(),
                    //Postre = x.Productos.Postre.RemoverAcentos(),
                    //Bebida = x.Productos.IncluyeBebida ? "Incluye (1) Bebida" : "NO incluye Bebida",
                    Asociados = restoPremium,
                    TipoMenu = "PREMIUM",
                    Texto = "Men&uacute; Premium: " + x.Productos.Entrada.RemoverAcentos() + " + " + x.Productos.PlatoPrincipal.RemoverAcentos() + " + " + x.Productos.Postre.RemoverAcentos() + " " + (x.Productos.IncluyeBebida ? "Incluye (1) Bebida." : "NO incluye Bebida."),
                    Excepto = "<strong>Excepto Dona Flor (s&oacute;lo cena) de 18 a 21.30 hs.</strong>",
                    Excepciones =""

                }).ToList();

            if (detallesPremium.Any())
                listFinal.AddRange(detallesPremium);
            */
            var detallesPlaya = dbContext.PedidosDetalle.Include("Pedidos").Include("Productos")
               .Where(x => x.IDPedido == idPedido && x.Productos.Tipo == "B").ToList()
               .Select(x => new VouchersViewModel()
               {
                   Codigo = x.IDDetalle,
                   DigitoVerificador = x.DigitoVerficador,
                   Nombre = x.Pedidos.Pasajero,
                   NroDocumento = x.Pedidos.NroDocumento,
                   FechaDesde = x.Pedidos.FechaEstadiaDesde,
                   FechaHasta = x.Pedidos.FechaEstadiaHasta,
                   //Entrada = x.Productos.Entrada.RemoverAcentos(),
                   //Principal = x.Productos.PlatoPrincipal.RemoverAcentos(),
                   //Postre = x.Productos.Postre.RemoverAcentos(),
                   //Bebida = x.Productos.IncluyeBebida ? "Incluye (1) Bebida" : "NO incluye Bebida",
                   Asociados = restoPlaya,
                   TipoMenu = "PLAYA",
                   Texto = "Men&uacute; Playa: " + x.Productos.Entrada.RemoverAcentos() +((x.Productos.PlatoPrincipal!="-")? " + " + x.Productos.PlatoPrincipal.RemoverAcentos() :"")+((x.Productos.Postre!="-")? " + " + x.Productos.Postre.RemoverAcentos():"") + " " + (x.Productos.IncluyeBebida ? "Incluye (1) Bebida." : "NO incluye Bebida."),
                   Excepto = "",
                   Ubicacion = "<strong>JOAO FERNANDES:</strong> Restaurante La Plage, ubicado en el BEACH CLUB. ",
                   Excepciones = "",
                   HorarioAtencion = "Horario de atenci&oacute;n:12 a 16:00 hs y de 19 a 22 Hs.",
                   HorarioAtencionDatosUtiles = "<strong>HORARIO DE ATENCI&Oacute;N:</strong>Para almuerzos de 12 a 16Hs. Para Cenas: el horario es de 19 a 22 Hs.<u> Sólo se podrá consumir la Cena, adquiriendo también un voucher para Almuerzo.</u>"

               }).ToList();

            if (detallesPlaya.Any())
                listFinal.AddRange(detallesPlaya);
            var detallesClasico = dbContext.PedidosDetalle.Include("Pedidos").Include("Productos")
                .Where(x => x.IDPedido == idPedido && x.Productos.Tipo == "C").ToList()
                .Select(x => new VouchersViewModel()
                {
                    Codigo = x.IDDetalle,
                    DigitoVerificador = x.DigitoVerficador,
                    Nombre = x.Pedidos.Pasajero,
                    NroDocumento = x.Pedidos.NroDocumento,
                    FechaDesde = x.Pedidos.FechaEstadiaDesde,
                    FechaHasta = x.Pedidos.FechaEstadiaHasta,
                    //Entrada = x.Productos.Entrada.RemoverAcentos(),
                    //Principal = x.Productos.PlatoPrincipal.RemoverAcentos(),
                    //Postre = x.Productos.Postre.RemoverAcentos(),
                    //Bebida = x.Productos.IncluyeBebida ? "Incluye (1) Bebida" : "NO incluye Bebida",
                    Asociados = restoClasico,
                    TipoMenu = "CLÁSICO",
                    Texto = "Men&uacute; Clasico: " + x.Productos.Entrada.RemoverAcentos() + " + " + x.Productos.PlatoPrincipal.RemoverAcentos() + " + " + x.Productos.Postre.RemoverAcentos() + " " + (x.Productos.IncluyeBebida ? "Incluye (1) Bebida." : "NO incluye Bebida."),
                    Excepto = "",
                    Excepciones = "",
                    Ubicacion = "<strong>CENTRO DE BÚZIOS:</strong> LA DOLCE VITA - ENU SUSHI - PARVATI - MINEIRO GRILL - BASTIDORES - NOI - BUZIN.  <strong>ORLA BARDOT:  </strong> MATAHARI - LA BARDOT - RINCON - DO JEITO BUZIANO. <strong>FERRADURA:</strong> SAN TELMO (SOBRE AV. PRINCIPAL). <strong>GERIBA:</strong> QUIOSQUE DO MINEIRO (RESTO DE PRAIA).  ",
                    HorarioAtencion = "Horario de atenci&oacute;n: 12 a 22:00 hs.",
                    HorarioAtencionDatosUtiles = "<strong>HORARIO DE ATENCI&Oacute;N:</strong> Todos los restaurantes asociados a Grupo B&uacute;zios Rest&oacute; atienden de 12 a 22:00HS."

                }).ToList();

            if (detallesClasico.Any())
                listFinal.AddRange(detallesClasico);



            var detallesKids = dbContext.PedidosDetalle.Include("Pedidos").Include("Productos")
                .Where(x => x.IDPedido == idPedido && x.Productos.Tipo == "M").ToList()
                .Select(x => new VouchersViewModel() {
                    Codigo = x.IDDetalle,
                    DigitoVerificador = x.DigitoVerficador,
                    Nombre = x.Pedidos.Pasajero,
                    NroDocumento = x.Pedidos.NroDocumento,
                    FechaDesde = x.Pedidos.FechaEstadiaDesde,
                    FechaHasta = x.Pedidos.FechaEstadiaHasta,
                    Entrada = "<span>(3 a 10 a&ntilde;os) Menores de 2 a&ntilde;os no abonan.</span>",
                    //Principal = x.Productos.PlatoPrincipal.RemoverAcentos(),
                    //Postre = x.Productos.Postre.RemoverAcentos(),
                    //Bebida = x.Productos.IncluyeBebida ? "Incluye (1) Bebida" : "NO incluye Bebida",
                    Asociados = restoKids,
                    TipoMenu = "KIDS",
                    Texto = "Men&uacute; Kids: " + x.Productos.Entrada.RemoverAcentos() + " + " + x.Productos.PlatoPrincipal.RemoverAcentos() + " + " + x.Productos.Postre.RemoverAcentos() + " " + (x.Productos.IncluyeBebida ? "Incluye (1) Bebida." : "NO incluye Bebida."),
                    Excepto = "<strong>Excepto Club La Plage (s&oacute;lo almuerzo con bebida + 1 caf&eacute; o caipirinha de cortes&iacute;a) de 12 a 16 hs. Dona Flor (s&oacute;lo cena) de 18 a 21.30 hs.</strong>",
                    Excepciones = "<strong>Quiosque do Mineiro  de 12 a 16HS. </strong>",
                    Ubicacion = " <strong>CENTRO DE BÚZIOS: </strong>  BUZIN - LA DOLCE VITA - ENU SUSHI  - NOI - PARVATI - MINEIRO GRILL -  BASTIDORES.  <strong>JOAO FERNANDES:  </strong>LA PLAGE ( BEACH CLUB).  <strong>ORLA BARDOT:  </strong>MATAHARI - RINCON  - DO JEITO BUZIANO - LA BARDOT. <strong>FERRADURA: </strong> SAN TELMO (SOBRE AV. PRINCIPAL).  <strong>GERIBÁ:  </strong>QUIOSQUE DO MINEIRO (SOBRE AV. PRINCIPAL)",
                    HorarioAtencion = "Horario de atenci&oacute;n: 12 a 22:00 hs.",
                    HorarioAtencionDatosUtiles = "<strong>HORARIO DE ATENCI&Oacute;N:</strong> Todos los restaurantes asociados a Grupo B&uacute;zios Rest&oacute; atienden de 12 a 22:00HS."
                }).ToList();

            if (detallesKids.Any())
                listFinal.AddRange(detallesKids);

            var detallesLibre = dbContext.PedidosDetalle.Include("Pedidos").Include("Productos")
               .Where(x => x.IDPedido == idPedido && x.Productos.Tipo == "L").ToList()
               .Select(x => new VouchersViewModel()
               {
                   Codigo = x.IDDetalle,
                   DigitoVerificador = x.DigitoVerficador,
                   Nombre = x.Pedidos.Pasajero,
                   NroDocumento = x.Pedidos.NroDocumento,
                   FechaDesde = x.Pedidos.FechaEstadiaDesde,
                   FechaHasta = x.Pedidos.FechaEstadiaHasta,
                   Asociados = restoLibre,
                   TipoMenu = "Buffet Libre",
                   Texto = "Men&uacute; Buffet Libre: " + x.Productos.Entrada.RemoverAcentos() + ((x.Productos.PlatoPrincipal != "-") ? " + " + x.Productos.PlatoPrincipal.RemoverAcentos() : "") + ((x.Productos.Postre != "-") ? " + " + x.Productos.Postre.RemoverAcentos() : "") + " " + (x.Productos.IncluyeBebida ? "Incluye (1) Bebida." : "NO incluye Bebida."),
                   Excepto = "",
                   Ubicacion = "<strong>JOAO FERNANDES:</strong> Restaurante La Plage, ubicado en el BEACH CLUB. ",
                   Excepciones = "",
                   HorarioAtencion = "Horario de atenci&oacute;n:12 a 16:00 hs y de 19 a 22 Hs.",
                   HorarioAtencionDatosUtiles = "<strong>HORARIO DE ATENCI&Oacute;N:</strong>Para almuerzos de 12 a 16Hs. Para Cenas: el horario es de 19 a 22 Hs.<u> Sólo se podrá consumir la Cena, adquiriendo también un voucher para Almuerzo.</u>"

               }).ToList();

            if (detallesLibre.Any())
                listFinal.AddRange(detallesLibre);

            if (listFinal.Any()) {
                foreach (var detalle in listFinal) {
                    detalle.CodigoString = Cryptography.Encrypt(detalle.Codigo.ToString());
                    if (detalle.DigitoVerificador.HasValue)
                        detalle.CodigoString += "-" + detalle.DigitoVerificador.Value.ToString();

                    detalle.FechaDesdeString = detalle.FechaDesde.ToString("dd/MM/yyyy");
                    detalle.FechaHastaString = detalle.FechaHasta.ToString("dd/MM/yyyy");
                }
                rptPedidos.DataSource = listFinal;
                rptPedidos.DataBind();
            }
        }
    }

    protected void Descargar(object sender, EventArgs e) {
        string html;

        #region Read the HTML content

        using (var client = new WebClient()) {
            //client.Headers[HttpRequestHeader.Cookie] =System.Web.HttpContext.Current.Request.Headers["Cookie"];
            client.Encoding = Encoding.UTF8;
            html = client.DownloadString(@"http://www.grupobuziosresto.com/pedidoPrint.aspx?session=false&Id=3556" /*+ Request.QueryString["Id"]*/);
            //html = html.ReplaceAll("é", "&eacute;");
        }

        #endregion

        #region Transform the HTML into PDF

        var pechkin = Factory.Create(new GlobalConfig());
        var pdf = pechkin.Convert(new ObjectConfig()
                                .SetLoadImages(true).SetZoomFactor(2.0)
                                .SetPrintBackground(true)
                                .SetScreenMediaType(true)
                                .SetIntelligentShrinking(true)
                                .SetAllowLocalContent(true)
                                .SetCreateForms(false)
                                .SetCreateExternalLinks(true), html);

        #endregion

        #region Return the pdf file

        Response.Clear();

        Response.ClearContent();
        Response.ClearHeaders();

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Voucher-" + Request.QueryString["Id"] + ".pdf; size={0}", pdf.Length));
        Response.BinaryWrite(pdf);

        Response.Flush();
        Response.End();

        #endregion
    }
}

/*string htmlAdjunto = string.Empty;
            string datosPedido = string.Empty;

            #region armo htmlAdjunto
            htmlAdjunto += "<table style='border-collapse: collapse;text-align: center; border: 1px solid #833332; font-family: Arial,Trebuchet MS,Segoe UI,Helvetica,sans-serif; font-size: 14px; background-color: #FFF'>";
            htmlAdjunto += "<tbody id='formulario'>";
            htmlAdjunto += "<tr>";
            htmlAdjunto += "<td style='border: 1px solid #833332; font-weight: bold; width: 100px;' rowspan='1'>Codigo</td>";
            htmlAdjunto += "<td style='border: 1px solid #833332; font-weight: bold; width: 100px;' rowspan='1'>Tipo</td>";
            htmlAdjunto += "<td style='border: 1px solid #833332; font-weight: bold; width: 100px;' rowspan='1'>Precio</td>";
            htmlAdjunto += "<td style='width: 100px; border-left: 1px solid black; border-right: 1px solid black;'><b>Entrada</b></td>";
            htmlAdjunto += "<td style='width: 100px; border-left: 1px solid black; border-right: 1px solid black;'><b>Plato Principal</b></td>";
            htmlAdjunto += "<td style='width: 100px; border-left: 1px solid black; border-right: 1px solid black;'><b>Postre</b></td>";
            htmlAdjunto += "<td style='width: 100px; border-left: 1px solid black; border-right: 1px solid black;'><b>Incluye Bebida</b></td>";
            htmlAdjunto += "</tr>";
            #endregion

            var pedido = dbContext.Pedidos.Where(x => x.IDPedido == idPedido).FirstOrDefault();
            var pedidosDetalle = dbContext.PedidosDetalle.Where(x => x.IDPedido == idPedido).ToList();

            if (pedido != null)
            {
                datosPedido += "<style>body { font-family: calibri;}</style>";
                datosPedido += "Pasajero: " + pedido.Pasajero + "<br/>";
                datosPedido += "Estadia desde: " + pedido.FechaEstadiaDesde.ToShortDateString() + "</br>";
                datosPedido += "Estadia hasta: " + pedido.FechaEstadiaHasta.ToShortDateString() + "</br>";
            }

            if (pedidosDetalle != null)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                string codigo = string.Empty;
                foreach (var detalle in pedidosDetalle
                    .OrderBy(x => x.IDProducto)
                    .Select(x => new
                {
                    IDDetalle = x.IDDetalle,
                    Tipo = x.Productos.Tipo == "T" ? "Turista" : x.Productos.Tipo == "P" ? "Premium" : "Menores",
                    Precio = x.Precio,
                    Entrada = x.Productos.Entrada,
                    PlatoPrincipal = x.Productos.PlatoPrincipal,
                    Postre = x.Productos.Postre,
                    IncluyeBebida = x.Productos.IncluyeBebida ? "Si" : "No",
                }))
                {

                    var result = new string(
                        Enumerable.Repeat(chars, 4)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());

                    codigo = result;

                    htmlAdjunto += "<tr style='border: 1px solid #833332; width: 100px;'>";

                    //if (detalle.IDDetalle.ToString().Length == 1)
                    //    htmlAdjunto += "<td style='border: 1px solid black; width: 100px;'>" + codigo + "-0" + detalle.IDDetalle + "</td>";
                    //if (detalle.IDDetalle.ToString().Length == 2)
                    //    htmlAdjunto += "<td style='border: 1px solid black; width: 100px;'>" + codigo + "-" + detalle.IDDetalle + "</td>";
                    htmlAdjunto += "<td style='border: 1px solid black; width: 100px;'>" + codigo + "-" + detalle.IDDetalle + "</td>";
                    codigo = string.Empty;
                    htmlAdjunto += "<td style='border: 1px solid black; width: 100px;'>" + detalle.Tipo + "</td>";
                    htmlAdjunto += "<td style='border: 1px solid black; width: 100px;'>" + detalle.Precio + "</td>";
                    htmlAdjunto += "<td style='width: 100px; border: 1px solid black;'>" + detalle.Entrada + "</td>";
                    htmlAdjunto += "<td style='width: 100px; border: 1px solid black;'>" + detalle.PlatoPrincipal + "</td>";
                    htmlAdjunto += "<td style='width: 100px; border: 1px solid black;'>" + detalle.Postre + "</td>";
                    htmlAdjunto += "<td style='width: 100px; border: 1px solid black;'>" + detalle.IncluyeBebida + "</td>";

                    htmlAdjunto += "</tr>";
                }
            }

            htmlAdjunto += "</table>";

            divTabla.InnerHtml = datosPedido + "<br /><br/>" + htmlAdjunto;*/