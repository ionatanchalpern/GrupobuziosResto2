using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACHE.Model;
using ACHE.Extensions;

public partial class restaurant : WebBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
        {

            var id = int.Parse(Request.QueryString["Id"]);
            if (!IsPostBack)
            {
                if (id != 0)
                    cargarDatos(id);
                //     else
                // Response.Redirect("default.aspx");
            }
      }
      else
          Response.Redirect("default.aspx");
    
    }

    private void cargarDatos(int? idRestaurant)
    {
        using (var dbContext = new ACHEEntities())
        {
            var info = dbContext.Restaurantes.Include("Menues").Where(x => x.IDRestaurant == idRestaurant && x.Activo).FirstOrDefault();
            if (info != null)
            {
                litNombreBc.Text = info.Nombre;
                litObservacionesBc.Text = info.Observaciones;
                litNombreBc2.Text = info.Nombre;
                imgLogo.ImageUrl = "/files/logos/" + info.Logo;
                litNombre.Text = info.Nombre;
                //litCiudadTitulo.Text = info.Ciudad;
                litObservaciones.Text = info.Observaciones == string.Empty ? "&nbsp;" : info.Observaciones;
                litDireccion.Text = info.Direccion == string.Empty ? "&nbsp;" : info.Direccion;
                litCiudad.Text = info.Ciudad == string.Empty ? "&nbsp;" : info.Ciudad;
                litAtencion.Text = info.HorarioAtencion == string.Empty ? "&nbsp;" : info.HorarioAtencion;
                litTelefono.Text = info.Telefono;

                if (string.IsNullOrEmpty(info.Imagen1) && string.IsNullOrEmpty(info.Imagen2) && string.IsNullOrEmpty(info.Imagen3))
                    pnlImagenes.Visible = false;

                litImagen1.Text = string.IsNullOrEmpty(info.Imagen1) ? "" : "<a class='thumb' href='files/restaurantes/" + info.Imagen1 + "' data-lightbox='pictures'><span class='image' style='background-image: url(files/restaurantes/" + info.Imagen1 + ");'></span><span class='glyphicon glyphicon-zoom-in'></span></a>";
                litImagen2.Text = string.IsNullOrEmpty(info.Imagen2) ? "" : "<a class='thumb' href='files/restaurantes/" + info.Imagen2 + "' data-lightbox='pictures'><span class='image' style='background-image: url(files/restaurantes/" + info.Imagen2 + ");'></span><span class='glyphicon glyphicon-zoom-in'></span></a>";
                litImagen3.Text = string.IsNullOrEmpty(info.Imagen3) ? "" : "<a class='thumb' href='files/restaurantes/" + info.Imagen3 + "' data-lightbox='pictures'><span class='image' style='background-image: url(files/restaurantes/" + info.Imagen3 + ");'></span><span class='glyphicon glyphicon-zoom-in'></span></a>";

                if (!string.IsNullOrEmpty(info.ImagenMapa))
                    litImageMapa.Text = "<a href='files/restaurantes/" + info.ImagenMapa + "' data-lightbox='imageMapa'><img src='/files/restaurantes/" + info.ImagenMapa + "' class='mapa-logo' /></a>";
                

                #region menues

                var menues = info.Menues.OrderBy(x => x.IDMenu).ToList();
                if (menues.Count != 0)
                {
                    if (!(menues.Any(x => x.TipoMenu == "M")))
                    {
                        divMenuKids.Visible = false;
                    }
                    else
                    {
                       var kids= menues.Where(x => x.TipoMenu == "M").FirstOrDefault();

                       litPrincipalKids.Text = kids.PlatoPrincipal;
                       litPostreKids.Text = kids.Postre;
                       if (kids.IncluyeBebida)
                        {
                            pnlSiBebidaKids.Visible = true;
                            litBebidaKids.Text = "Incluye 1 (una) bebida a elección.";
                        }
                        else
                            pnlNoBebidaKids.Visible = true;
                    }

                    /*
                    if (!(menues.Any(x => x.TipoMenu == "P")))
                    {
                        divMenuPremium.Visible = false;
                    }
                    else
                    {
                        var premium = menues.Where(x => x.TipoMenu == "P").FirstOrDefault();
                        
                        litEntradaPremium.Text = premium.Entrada;
                        litPrincipalPremium.Text = premium.PlatoPrincipal;
                        litPostrePremium.Text = premium.Postre;
                        if (premium.IncluyeBebida)
                        {
                            pnlSiBebidaPremium.Visible = true;
                            litBebidaPremium.Text = "Incluye 1 (una) bebida a elección.";
                        }
                        else
                            pnlNoBebidaPremium.Visible = true;

                        if (!string.IsNullOrEmpty(premium.ImagenMenu))
                        {
                            pnlPremiumDet.Visible = true;
                            lnkMenuPremium.NavigateUrl = "/files/menues/" + premium.ImagenMenu;
                        }
                        else
                            pnlPremiumDet.Visible = false;
                    }
                    */

                    if (!(menues.Any(x => x.TipoMenu == "B")))
                    {
                        divMenuPlaya.Visible = false;
                    }
                    else
                    {
                        var playa = menues.Where(x => x.TipoMenu == "B").FirstOrDefault();

                        litEntradaPlaya.Text = playa.Entrada;
                        litPrincipalPlaya.Text = playa.PlatoPrincipal;
                        litPostrePlaya.Text = playa.Postre;
                        if (playa.IncluyeBebida)
                        {
                            pnlSiBebidaPlaya.Visible = true;
                            litBebidaPlaya.Text = "Incluye 1 (una) bebida a elección.";
                        }
                        else
                            pnlNoBebidaPlaya.Visible = true;

                        if (!string.IsNullOrEmpty(playa.ImagenMenu))
                        {
                            pnlPlayaDet.Visible = true;
                            lnkMenuPlaya.NavigateUrl = "/files/menues/" + playa.ImagenMenu;
                        }
                        else
                            pnlPlayaDet.Visible = false;
                    }
                    if (!(menues.Any(x => x.TipoMenu == "T")))
                    {
                        divMenuTurista.Visible = false;
                    }
                    else
                    {

                        var turista = menues.Where(x => x.TipoMenu == "T").FirstOrDefault();

                        litEntradaTurista.Text = turista.Entrada;
                        litPrincipalTurista.Text = turista.PlatoPrincipal;
                        litPostreTurista.Text = turista.Postre;

                        if (turista.IncluyeBebida)
                        {
                            pnlSiBebidaTurista.Visible = true;
                            litBebidaTurista.Text = "Incluye 1 (una) bebida a elección.";
                        }
                        else
                            pnlNoBebidaTurista.Visible = true;

                        if (!string.IsNullOrEmpty(turista.ImagenMenu))
                        {
                            pnlTuristaDet.Visible = true;
                            lnkMenuTurista.NavigateUrl = "/files/menues/" + turista.ImagenMenu;
                        }
                        else
                            pnlTuristaDet.Visible = false;
                    }

                    if (!(menues.Any(x => x.TipoMenu == "C")))
                    {
                        divMenuClasico.Visible = false;
                    }
                    else
                    {
                        var clasico = menues.Where(x => x.TipoMenu == "C").FirstOrDefault();

                        litEntradaClasico.Text = clasico.Entrada;
                        litPlatoPrincipalClasico.Text = clasico.PlatoPrincipal;
                        litPostreClasico.Text = clasico.Postre;
                        if (clasico.IncluyeBebida)
                        {
                            pnlSiBebidaClasico.Visible = true;
                            litBebidaClasico.Text = "Incluye 1 (una) bebida a elección.";
                        }
                        else
                            pnlNoBebidaClasico.Visible = true;

                        if (!string.IsNullOrEmpty(clasico.ImagenMenu))
                        {
                            pnlClasicoDet.Visible = true;
                            lnkMenuClasico.NavigateUrl = "/files/menues/" + clasico.ImagenMenu;
                        }
                        else
                            pnlClasicoDet.Visible = false;
                    }
                    if (!(menues.Any(x => x.TipoMenu == "L")))
                    {
                        divMenuBuffetLibre.Visible = false;
                    }
                    else
                    {
                        var buffetLibre = menues.Where(x => x.TipoMenu == "L").FirstOrDefault();

                        litEntradaBuffetLibre.Text = buffetLibre.Entrada;
                        litPrincipalBuffetLibre.Text = buffetLibre.PlatoPrincipal;
                        litPostreBuffetLibre.Text = buffetLibre.Postre;
                        if (buffetLibre.IncluyeBebida)
                        {
                            pnlSiBebidaBuffetLibre.Visible = true;
                            litBebidaBuffetLibre.Text = "Incluye 1 (una) bebida a elección.";
                        }
                        else
                            pnlNoBebidaBuffetLibre.Visible = true;

                        if (!string.IsNullOrEmpty(buffetLibre.ImagenMenu))
                        {
                            pnlBuffetLibreDet.Visible = true;
                            lnkMenuBuffetLibre.NavigateUrl = "/files/menues/" + buffetLibre.ImagenMenu;
                        }
                        else
                            pnlBuffetLibreDet.Visible = false;
                    }
                }

                #endregion
            }
            else
            {
              //  Response.Redirect("default.aspx");
            }
        }
    }
}