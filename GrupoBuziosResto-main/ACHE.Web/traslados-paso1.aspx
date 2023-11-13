<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="traslados-paso1.aspx.cs" Inherits="traslados_paso1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/traslados-pasos.css?v=1" rel="stylesheet" />
    <%--<script type="text/javascript" src="js/lightbox.min.js"></script>--%>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/traslados-paso1.js?v=3.6") %>"></script>
    <%--<link href="css/lightbox.css" rel="stylesheet" />--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
      <div class="container-fluid location-breadcrumbs">
        <div class="container">
          <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <ol class="breadcrumb">
              <li>
                <a href="#">Home</a>
              </li>
              <li>
                <a href="/login-traslados.aspx">Acceso</a>
              </li>
              <li class="active">TrasladosRed</li>
            </ol>
          </div>
        </div>
      </div>
      <div id="MainContent_pnlCarrito">
             <asp:HiddenField runat="server"  ID="hdnIDUsuario" ClientIDMode="Static" Value="0" />
          <asp:HiddenField runat="server" ID="hdnIDPedido" ClientIDMode="Static" Value="0" />
        <div class="container content-padding traslados">
          <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 title">
              <h1>Traslados Red</h1>
              <h2>
                <b>Traslados Red</b> le ofrece la posibilidad de reservar online los transfers necesarios para sumar a la Reserva de su Agencia/Pasajero.
                <br />
                Les brindamos opciones en <b>traslados Regulares y Privados</b> desde distintos Aeropuertos de Río de Janeiro a Búzios, Arraial Do Cabo, Cabo Frío y viceversa.
                <br />
                <b>Una opción segura y cómoda con la mejor tarifa del Mercado.</b>
              </h2>
            </div>
          </div>

		  <div class="formularios-steps text-center clearfix">
		 	<div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step active">Paso 1</div>
			<div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step">Paso 2</div>
			<div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step">Paso 3</div>
			<div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step">Fin</div>
		  </div>

          <div class="row formularios mods">
		        <div class="col-xs-12 col-sm-12 co-md-4 col-lg-4 transport mod">
		          <h3>Seleccione el transportista</h3>
		          <span class="select-default">
		             <asp:DropDownList runat="server" ID="cmbProveedor" ClientIDMode="Static">
		                 <asp:ListItem Value="" Text="Seleccione un transportista"/>
		             </asp:DropDownList>
		          </span>
                       <br />
                        <span id="spnProveedor" class="errorRequired"></span>
                       <br />   <br />  
                 <img src="imgs/icono_horario.png" />   <a href="javascript:showModal();">  <span class="leyenda">Ver horarios de Servicios de Traslados</span></a>

		        </div>
		        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4 services">
		          <h3>Seleccione el servicio de traslado</h3>
		          <span class="select-default">
		            <asp:DropDownList runat="server" ID="cmbServicio" ClientIDMode="Static">
                            <asp:ListItem Value="" Text="Seleccione un servicio" />
                        </asp:DropDownList>
		          </span>
                    <br />
                    <div id="divServiciosEspeciales" runat="server"> 
		              <h3>Servicio Especial</h3>
		              <span class="select-default">
		                <asp:DropDownList runat="server" ID="cmbServicioEspecial" ClientIDMode="Static">
                                <asp:ListItem Value="" Text="Seleccione un servicio" />
                            </asp:DropDownList>
		              </span>
                        <br />              
                    </div>
		          <div class="clearfix"></div>
		        </div>
				<div class="col-xs-12 col-sm-12 col-md-4 col-lg-4 services last">
                         <h3>Cantidad de pasajeros</h3><br />
				    <span class="select-default small">
		              <asp:DropDownList runat="server" ID="cmbCantidadAdultos" ClientIDMode="static">
                                <asp:ListItem Value="" />
                                <asp:ListItem Value="0" Text="0" />
                                <asp:ListItem Value="1" Text="1" />
                                <asp:ListItem Value="2" Text="2" />
                                <asp:ListItem Value="3" Text="3" />
                                <asp:ListItem Value="4" Text="4" />
                                <asp:ListItem Value="5" Text="5" />
                                <asp:ListItem Value="6" Text="6" />
                                <asp:ListItem Value="7" Text="7" />
                                <asp:ListItem Value="8" Text="8" />
                                <asp:ListItem Value="9" Text="9" />
                                <asp:ListItem Value="10" Text="10" />
                            </asp:DropDownList>
		            </span>
                   
				  <h3 class="inline no-bg">Cantidad de adultos</h3>
                 <br />    <span id="spnCantidadAdultos" class="errorRequired"></span>

		          <div class="spacer"></div>
                    <span class="select-default small">
		               <asp:DropDownList runat="server" ID="cmbCantidadMenores" ClientIDMode="static">
                                <asp:ListItem Value="0" Text="0" />
                                <asp:ListItem Value="1" Text="1" />
                                <asp:ListItem Value="2" Text="2" />
                                <asp:ListItem Value="3" Text="3" />
                                <asp:ListItem Value="4" Text="4" />
                                <asp:ListItem Value="5" Text="5" />
                                <asp:ListItem Value="6" Text="6" />
                                <asp:ListItem Value="7" Text="7" />
                                <asp:ListItem Value="8" Text="8" />
                                <asp:ListItem Value="9" Text="9" />
                                <asp:ListItem Value="10" Text="10" />
                            </asp:DropDownList>
		          </span>
                   <h3 class="inline no-bg">Cantidad de menores que ocupan asiento</h3>
                   <span id="spnCantidadMenores" class="errorRequired"></span>
		           <div class="spacer"></div>
				<span class="select-default small">
		            <asp:DropDownList runat="server" ID="cmbCantidadMenores2" ClientIDMode="static">
                                <asp:ListItem Value="0" Text="0" />
                                <asp:ListItem Value="1" Text="1" />
                                <asp:ListItem Value="2" Text="2" />
                                <asp:ListItem Value="3" Text="3" />
                                <asp:ListItem Value="4" Text="4" />
                                <asp:ListItem Value="5" Text="5" />
                                <asp:ListItem Value="6" Text="6" />
                                <asp:ListItem Value="7" Text="7" />
                                <asp:ListItem Value="8" Text="8" />
                                <asp:ListItem Value="9" Text="9" />
                                <asp:ListItem Value="10" Text="10" />
                            </asp:DropDownList>
		          </span>
				 <h3 class="inline no-bg">Cantidad de menores gratis hasta 3 años</h3>
                    <span id="spnCantidadMenores2" class="errorRequired"></span>
                     <button class="reserve" onclick="return continuar();" style="margin-top:38px">
		            Continuar
		            <span>▸</span>
		          </button>
		          <div class="clearfix"></div>
		      </div>
		 </div>
	   </div>
      </div>
  
     <div class="modal fade" id="modalHorarios">
            <div class="modal-dialog">
                <div class="modal-content" style="width: 800px;">
                    <div class="modal-header">
						Servicios de Traslados
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                       
                    </div>
                    <div class="modal-body">
                            <div class="container">
                                  <img src="imgs/grillahorarios.png" />
                            </div>
                    </div>
                   
                </div>
            </div>
    </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">
      <script type="text/javascript">
          $(document).ready(function () {
              $(".numeric").numeric();
          });
          
          function showModal() {
              $("#modalHorarios").modal("show");
          }
    </script>
</asp:Content>

