<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="traslados-paso2.aspx.cs" Inherits="traslados_paso2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/traslados-pasos.css?v=1" rel="stylesheet" />
    <%--<script type="text/javascript" src="js/lightbox.min.js"></script>--%>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/traslados-paso2.js?v=3.6") %>"></script>
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
        <input type="hidden" name="ctl00$MainContent$hdnIDUsuario" id="hdnIDUsuario" value="192" />
          <asp:HiddenField runat="server" ID="hdnIDPedido" ClientIDMode="Static" Value="0" />
                  <asp:HiddenField runat="server" ID="hdnTipoServicio" ClientIDMode="Static" value="" />

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
		 	<div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step">Paso 1</div>
			<div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step active">Paso 2</div>
			<div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step">Paso 3</div>
			<div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step">Fin</div>
		  </div>
            <br />
            <div id="divError" class="errorRequired" visible="false" style="width:740px; margin-left:0px"></div>
               <div class="row">
                    <div class="col-sm-3 col-md-3" >
                           <h3>Nro de file: </h3>
                         <asp:TextBox runat="server" id="txtNroFile" ClientIDMode="static"></asp:TextBox>
                    </div>
                    <div id="divPagoPor" runat="server"  class="col-sm-3 col-md-3" >
                           <h3>Pago por: </h3>
                         <asp:TextBox runat="server" id="txtPagoPor" ClientIDMode="static"></asp:TextBox>
                    </div>
               </div>
            <br />
             <div class="row">
                    <div class="col-sm-12 col-md-12" id="tblPasajeros" >
                           <table class="table table-striped table-bordered mediaTable" >
				            <thead>
					            <tr>
						            <th>Nombre del pasajero</th>
						            <th>Documento de identidad(DNI)</th>
					            </tr>
				            </thead>
				            <tbody id="bodyPasajeros">
                                <tr><td colspan="4">Calculando...</td></tr>
				            </tbody>
			            </table>
                    </div>
         <div class="clearfix"></div>
		</div>
        
            <div class="col-xs-12 padding-left-30 padding-right-30 text-center">
  	          <button onclick="return irPaso1();" class="reserve pull-left before">
  	            <span>▸</span>
                Anterior
  	          </button>

  	          <button onclick="return continuar();" class="reserve after">
  	            Siguiente
  	            <span>▸</span>
  	          </button>

                </div>

             </div>
            	
         
      </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">


</asp:Content>

