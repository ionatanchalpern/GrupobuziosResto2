<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="traslados-paso3-oneWay.aspx.cs" Inherits="traslados_paso3_oneWay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/traslados-pasos.css?v=1" rel="stylesheet" />

    <%--<script type="text/javascript" src="js/lightbox.min.js"></script>--%>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/traslados-paso3-oneWay.js?v=3.6") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/bootstrap-datetimepicker.min.js") %>"></script>
    <%--<link href="css/lightbox.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    

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
            <asp:HiddenField runat="server" ID="hdnIda" ClientIDMode="Static" Value="true" />
            <asp:HiddenField runat="server" ID="hdnLugar1" ClientIDMode="Static" Value="" />
            <asp:HiddenField runat="server" ID="hdnLugar2" ClientIDMode="Static" Value="" />
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
                    <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step ">Paso 2</div>
                    <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step active">Paso 3</div>
                    <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 step">Fin</div>
                </div>
                <div class="clearfix"></div>
                <br />
                <div class="row formularios">
                    <div class="col-xs-12">
                        <div class="col-xs-12 traslados-mods-title">
                            <h3><span id="spnSubTipo" runat="server"></span></h3>

                            <h4>Seleccione el tramo:
               
                                <input value="ida" name="idayvuelta" type="radio" id="soloida" group="idaovuelta" />
                                <label for="soloida">Sólo Ida</label>

                                <input value="vuelta" name="idayvuelta" type="radio" id="solovuelta" group="idaovuelta" />
                                <label for="solovuelta">Sólo Vuelta</label>
                            </h4>

                        </div>
                        <div id="divIda" class="col-xs-12 col-sm-12 col-md-6 col-lg-6 data traslados-data ida" style="display: none">
                            <div class="col-xs-12 name date no-border no-bottom-padding">
                                <h1 class="super">Ida</h1>
                                <h1 class="sub">Fecha Ida (In) <!--span style="color:#e74c48">Atención: Fecha de LLEGADA del aéreo</span--></h1>
                                <asp:TextBox runat="server" ID="txtFechaIda" class="datepicker-input" placeholder="DD/MM/AA" ClientIDMode="static"></asp:TextBox>
                                <span id="spnFechaIda" class="errorRequired"></span>
                                <br />
                            </div>
                            <div id="divAeropuertoIda" style="display: none">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 origin">
                                    <h1 class="neutral">AEROPUERTO</h1>
                                    <span class="select-default">
                                        <asp:DropDownList runat="server" ID="cmbAeropuertoIda" ClientIDMode="Static">
                                            <asp:ListItem Value="" Text="Aeropuerto EZE (Buenos Aires, Argentina)" />
                                        </asp:DropDownList>
                                    </span>
                                    <span id="spnAeropuertoIda" class="errorRequired"></span>
                                    <br />
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4 arrival">
                                    <h2 class="no-margin">&nbsp;</h2>
                                    <h3>Compañía aérea</h3>
                                    <asp:TextBox runat="server" ID="txtCompaniaAerea" ClientIDMode="static"></asp:TextBox>
                                    <span id="spnCompaniaAerea" class="errorRequired"></span>
                                    <br />
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-3 arrival">
                                    <h2 class="no-margin">&nbsp;</h2>
                                    <h3>Nº de vuelo</h3>
                                    <asp:TextBox runat="server" ID="txtNroVuelo" ClientIDMode="static"></asp:TextBox>
                                    <span id="spnNroVuelo" class="errorRequired"></span>
                                    <br />
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5 arrival">
                                    <h2 class="no-margin">&nbsp;</h2>
                                    <h3 class="small">Hora de LLEGADA del Aéreo</h3>
                                    <span class="select-small">
                                        <asp:DropDownList runat="server" ClientIDMode="static" ID="cmbHoraIda" class="time">
                                            <asp:ListItem Value="00" Text="00" />
                                            <asp:ListItem Value="01" Text="01" />
                                            <asp:ListItem Value="02" Text="02" />
                                            <asp:ListItem Value="03" Text="03" />
                                            <asp:ListItem Value="04" Text="04" />
                                            <asp:ListItem Value="05" Text="05" />
                                            <asp:ListItem Value="06" Text="06" />
                                            <asp:ListItem Value="07" Text="07" />
                                            <asp:ListItem Value="08" Text="08" />
                                            <asp:ListItem Value="09" Text="09" />
                                            <asp:ListItem Value="10" Text="10" />
                                            <asp:ListItem Value="11" Text="11" />
                                            <asp:ListItem Value="12" Text="12" />
                                            <asp:ListItem Value="13" Text="13" />
                                            <asp:ListItem Value="14" Text="14" />
                                            <asp:ListItem Value="15" Text="15" />
                                            <asp:ListItem Value="16" Text="16" />
                                            <asp:ListItem Value="17" Text="17" />
                                            <asp:ListItem Value="18" Text="18" />
                                            <asp:ListItem Value="19" Text="19" />
                                            <asp:ListItem Value="20" Text="20" />
                                            <asp:ListItem Value="21" Text="21" />
                                            <asp:ListItem Value="22" Text="22" />
                                            <asp:ListItem Value="23" Text="23" />
                                            <asp:ListItem Value="24" Text="24" />
                                        </asp:DropDownList>

                                    </span>
                                    <span class="dots">:</span>
                                    <span class="select-small">
                                        <asp:DropDownList runat="server" ID="cmbMinutosIda" ClientIDMode="static" CssClass="time">
                                            <asp:ListItem Value="00" Text="00" />
                                            <asp:ListItem Value="01" Text="01" />
                                            <asp:ListItem Value="02" Text="02" />
                                            <asp:ListItem Value="03" Text="03" />
                                            <asp:ListItem Value="04" Text="04" />
                                            <asp:ListItem Value="05" Text="05" />
                                            <asp:ListItem Value="06" Text="06" />
                                            <asp:ListItem Value="07" Text="07" />
                                            <asp:ListItem Value="08" Text="08" />
                                            <asp:ListItem Value="09" Text="09" />
                                            <asp:ListItem Value="10" Text="10" />
                                            <asp:ListItem Value="11" Text="11" />
                                            <asp:ListItem Value="12" Text="12" />
                                            <asp:ListItem Value="13" Text="13" />
                                            <asp:ListItem Value="14" Text="14" />
                                            <asp:ListItem Value="15" Text="15" />
                                            <asp:ListItem Value="16" Text="16" />
                                            <asp:ListItem Value="17" Text="17" />
                                            <asp:ListItem Value="18" Text="18" />
                                            <asp:ListItem Value="19" Text="19" />
                                            <asp:ListItem Value="20" Text="20" />
                                            <asp:ListItem Value="21" Text="21" />
                                            <asp:ListItem Value="22" Text="22" />
                                            <asp:ListItem Value="23" Text="23" />
                                            <asp:ListItem Value="24" Text="24" />
                                            <asp:ListItem Value="25" Text="25" />
                                            <asp:ListItem Value="26" Text="26" />
                                            <asp:ListItem Value="27" Text="27" />
                                            <asp:ListItem Value="28" Text="28" />
                                            <asp:ListItem Value="29" Text="29" />
                                            <asp:ListItem Value="30" Text="30" />
                                            <asp:ListItem Value="31" Text="31" />
                                            <asp:ListItem Value="32" Text="32" />
                                            <asp:ListItem Value="33" Text="33" />
                                            <asp:ListItem Value="34" Text="34" />
                                            <asp:ListItem Value="35" Text="35" />
                                            <asp:ListItem Value="36" Text="36" />
                                            <asp:ListItem Value="37" Text="37" />
                                            <asp:ListItem Value="38" Text="38" />
                                            <asp:ListItem Value="39" Text="39" />
                                            <asp:ListItem Value="40" Text="40" />
                                            <asp:ListItem Value="41" Text="41" />
                                            <asp:ListItem Value="42" Text="42" />
                                            <asp:ListItem Value="43" Text="43" />
                                            <asp:ListItem Value="44" Text="44" />
                                            <asp:ListItem Value="45" Text="45" />
                                            <asp:ListItem Value="46" Text="46" />
                                            <asp:ListItem Value="47" Text="47" />
                                            <asp:ListItem Value="48" Text="48" />
                                            <asp:ListItem Value="49" Text="49" />
                                            <asp:ListItem Value="50" Text="50" />
                                            <asp:ListItem Value="51" Text="51" />
                                            <asp:ListItem Value="52" Text="52" />
                                            <asp:ListItem Value="53" Text="53" />
                                            <asp:ListItem Value="54" Text="54" />
                                            <asp:ListItem Value="55" Text="55" />
                                            <asp:ListItem Value="56" Text="56" />
                                            <asp:ListItem Value="57" Text="57" />
                                            <asp:ListItem Value="58" Text="58" />
                                            <asp:ListItem Value="59" Text="59" />
                                        </asp:DropDownList>

                                    </span>
                                    <span class="hours">hs.</span>
                                     <h4 style="display:none;"> <span id="spnPrecioAdicional" style="color:#560D32; display:none; font-size: 15px;">Se cobrará un <u>adicional para vuelo nocturno</u></span></h4>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div id="divHotelIda" style="display: none">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 destination">
                                    <h2><span id="spnHotelIda" /></h2>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5 destination">
                                    <asp:TextBox runat="server" ID="txtNombreHotelIda" placeholder="Nombre del Hotel" ClientIDMode="static"></asp:TextBox>
                                    <span id="spnNombreHotelIda" class="errorRequired"></span>
                                    <br />
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-7 destination">
                                    <asp:TextBox runat="server" ID="txtDireccionHotelIda" placeholder="Dirección del Hotel" ClientIDMode="static"></asp:TextBox>
                                 
                                    <br />
                                </div>
                            </div>
                            <br />
                            <div id="divHotelIda2" style="display: none">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 destination">
                                    <br />
                                     <h2><span id="spnHotelIda2" /></h2>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5 destination">
                                    <asp:TextBox runat="server" ID="txtNombreHotelIda2" placeholder="Nombre del Hotel" ClientIDMode="static"></asp:TextBox>
                                    <span id="spnNombreHotelIda2" class="errorRequired"></span>
                                    <br />
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-7 destination">
                                    <asp:TextBox runat="server" ID="txtDireccionHotelIda2" placeholder="Dirección del Hotel" ClientIDMode="static"></asp:TextBox>
                                 
                                    <br />
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div id="divVuelta" class="col-xs-12 col-sm-12 col-md-6 col-lg-6 data traslados-data last vuelta" style="display: none">
                            <div class="col-xs-12 name date no-border no-bottom-padding">
                                <h1 class="super">Vuelta</h1>
                                <h1 class="sub">Fecha Vuelta (Out) <!--span style="color:#e74c48">Atención: Fecha de SALIDA del aéreo</span--></h1>
                                <asp:TextBox runat="server" ID="txtFechaVuelta" class="datepicker-input" placeholder="DD/MM/AA" ClientIDMode="static"></asp:TextBox>
                                <span id="spnFechaVuelta" class="errorRequired"></span>
                                <br />
                            </div>
                            <br />
                            <div id="divHotelVuelta" style="display: none">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 destination">
                                    <br />
                                    <h2><span id="spnHotelVuelta" /></h2>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5 destination">
                                    <asp:TextBox runat="server" ID="txtNombreHotelVuelta" placeholder="Nombre del Hotel" ClientIDMode="static"></asp:TextBox>
                                    <span id="spnNombreHotelVuelta" class="errorRequired"></span>
                                    <br />
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-7 destination">
                                    <asp:TextBox runat="server" ID="txtDireccionHotelVuelta" placeholder="Dirección del Hotel" ClientIDMode="static"></asp:TextBox>
                                   
                                    <br />
                                </div>
                            </div>
                            <div id="divAeropuertoVuelta" style="display: none">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 origin">
                                    <h1 class="neutral">Hasta AEROPUERTO</h1>
                                    <span class="select-default">
                                        <asp:DropDownList runat="server" ID="cmbAeropuertoVuelta" ClientIDMode="Static">
                                            <asp:ListItem Value="" Text="Aeropuerto EZE (Buenos Aires, Argentina)" />
                                        </asp:DropDownList>

                                    </span>
                                    <span id="spnAeropuertoVuelta" class="errorRequired"></span>
                                    <br />
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4 arrival">
                                    <h2 class="no-margin">&nbsp;</h2>
                                    <h3>Compañía aérea</h3>
                                    <asp:TextBox runat="server" ID="txtCompaniaVuelta" ClientIDMode="static"></asp:TextBox>
                                    <span id="spnCompaniaVuelta" class="errorRequired"></span>
                                    <br />
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-3 arrival">
                                    <h2 class="no-margin">&nbsp;</h2>
                                    <h3>Nº de vuelo</h3>
                                    <asp:TextBox runat="server" ID="txtNroVueloVuelta" ClientIDMode="static"></asp:TextBox>
                                    <span id="spnNroVueloVuelta" class="errorRequired"></span>
                                    <br />
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5 arrival">
                                    <h2 class="no-margin">&nbsp;</h2>
                                    <h3 class="small">Hora de SALIDA del Aéreo</h3>
                                    <span class="select-small">
                                        <asp:DropDownList runat="server" ClientIDMode="static" ID="cmbHoraVuelta" class="time">
                                            <asp:ListItem Value="00" Text="00" />
                                            <asp:ListItem Value="01" Text="01" />
                                            <asp:ListItem Value="02" Text="02" />
                                            <asp:ListItem Value="03" Text="03" />
                                            <asp:ListItem Value="04" Text="04" />
                                            <asp:ListItem Value="05" Text="05" />
                                            <asp:ListItem Value="06" Text="06" />
                                            <asp:ListItem Value="07" Text="07" />
                                            <asp:ListItem Value="08" Text="08" />
                                            <asp:ListItem Value="09" Text="09" />
                                            <asp:ListItem Value="10" Text="10" />
                                            <asp:ListItem Value="11" Text="11" />
                                            <asp:ListItem Value="12" Text="12" />
                                            <asp:ListItem Value="13" Text="13" />
                                            <asp:ListItem Value="14" Text="14" />
                                            <asp:ListItem Value="15" Text="15" />
                                            <asp:ListItem Value="16" Text="16" />
                                            <asp:ListItem Value="17" Text="17" />
                                            <asp:ListItem Value="18" Text="18" />
                                            <asp:ListItem Value="19" Text="19" />
                                            <asp:ListItem Value="20" Text="20" />
                                            <asp:ListItem Value="21" Text="21" />
                                            <asp:ListItem Value="22" Text="22" />
                                            <asp:ListItem Value="23" Text="23" />
                                            <asp:ListItem Value="24" Text="24" />
                                        </asp:DropDownList>
                                    </span>
                                    <span class="dots">:</span>
                                    <span class="select-small">
                                        <asp:DropDownList runat="server" ID="cmbMinutosVuelta" ClientIDMode="static" CssClass="time">
                                            <asp:ListItem Value="00" Text="00" />
                                            <asp:ListItem Value="01" Text="01" />
                                            <asp:ListItem Value="02" Text="02" />
                                            <asp:ListItem Value="03" Text="03" />
                                            <asp:ListItem Value="04" Text="04" />
                                            <asp:ListItem Value="05" Text="05" />
                                            <asp:ListItem Value="06" Text="06" />
                                            <asp:ListItem Value="07" Text="07" />
                                            <asp:ListItem Value="08" Text="08" />
                                            <asp:ListItem Value="09" Text="09" />
                                            <asp:ListItem Value="10" Text="10" />
                                            <asp:ListItem Value="11" Text="11" />
                                            <asp:ListItem Value="12" Text="12" />
                                            <asp:ListItem Value="13" Text="13" />
                                            <asp:ListItem Value="14" Text="14" />
                                            <asp:ListItem Value="15" Text="15" />
                                            <asp:ListItem Value="16" Text="16" />
                                            <asp:ListItem Value="17" Text="17" />
                                            <asp:ListItem Value="18" Text="18" />
                                            <asp:ListItem Value="19" Text="19" />
                                            <asp:ListItem Value="20" Text="20" />
                                            <asp:ListItem Value="21" Text="21" />
                                            <asp:ListItem Value="22" Text="22" />
                                            <asp:ListItem Value="23" Text="23" />
                                            <asp:ListItem Value="24" Text="24" />
                                            <asp:ListItem Value="25" Text="25" />
                                            <asp:ListItem Value="26" Text="26" />
                                            <asp:ListItem Value="27" Text="27" />
                                            <asp:ListItem Value="28" Text="28" />
                                            <asp:ListItem Value="29" Text="29" />
                                            <asp:ListItem Value="30" Text="30" />
                                            <asp:ListItem Value="31" Text="31" />
                                            <asp:ListItem Value="32" Text="32" />
                                            <asp:ListItem Value="33" Text="33" />
                                            <asp:ListItem Value="34" Text="34" />
                                            <asp:ListItem Value="35" Text="35" />
                                            <asp:ListItem Value="36" Text="36" />
                                            <asp:ListItem Value="37" Text="37" />
                                            <asp:ListItem Value="38" Text="38" />
                                            <asp:ListItem Value="39" Text="39" />
                                            <asp:ListItem Value="40" Text="40" />
                                            <asp:ListItem Value="41" Text="41" />
                                            <asp:ListItem Value="42" Text="42" />
                                            <asp:ListItem Value="43" Text="43" />
                                            <asp:ListItem Value="44" Text="44" />
                                            <asp:ListItem Value="45" Text="45" />
                                            <asp:ListItem Value="46" Text="46" />
                                            <asp:ListItem Value="47" Text="47" />
                                            <asp:ListItem Value="48" Text="48" />
                                            <asp:ListItem Value="49" Text="49" />
                                            <asp:ListItem Value="50" Text="50" />
                                            <asp:ListItem Value="51" Text="51" />
                                            <asp:ListItem Value="52" Text="52" />
                                            <asp:ListItem Value="53" Text="53" />
                                            <asp:ListItem Value="54" Text="54" />
                                            <asp:ListItem Value="55" Text="55" />
                                            <asp:ListItem Value="56" Text="56" />
                                            <asp:ListItem Value="57" Text="57" />
                                            <asp:ListItem Value="58" Text="58" />
                                            <asp:ListItem Value="59" Text="59" />
                                        </asp:DropDownList>
                                    </span>
                                    <span class="hours">hs.</span>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div id="divHotelVuelta2" style="display: none">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 destination">
                                    <br />
                                      <h2><span id="spnHotelVuelta2" /></h2>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-5 destination">
                                    <asp:TextBox runat="server" ID="txtNombreHotelVuelta2" placeholder="Nombre del Hotel" ClientIDMode="static"></asp:TextBox>
                                    <span id="spnNombreHotelVuelta2" class="errorRequired"></span>
                                    <br />
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-7 destination">
                                    <asp:TextBox runat="server" ID="txtDireccionHotelVuelta2" placeholder="Dirección del Hotel" ClientIDMode="static"></asp:TextBox>
                                   
                                    <br />
                                </div>
                            </div>
                            <div class="clearfix"></div>

                        </div>
                    </div>


                    <div class="col-xs-12 border-top-purple">
                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 destination last">
                            <h2>Observaciones</h2>
                            <h3>(Si hubiese cambio de Hotel en Destino)</h3>
                            <asp:TextBox ClientIDMode="Static" runat="server" TextMode="multiline" Rows="5" ID="txtObservaciones"></asp:TextBox>
                        </div>
                           <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 destination last">
                            
                                   <span id="spnServicio" class="errorRequired"></span><br />
                  <h1 class="sub" id="titPrecio" runat="server"></h1>
                          <div id="divRegular" style="display:none" >
		                  <div class="option">
		                    <input type="radio" name="group" id="rdbRegular"/>
		                    <label for="uno">
		                      Servicio Regular
		                      <span id="spnRegular" class="price"> </span>
		                    </label>
		                  </div><br/><br/><br/>
				          <div class="disclaimer">
				  	        <h4>Observaciones</h4>
				  	        <p class="small"><span id="obsRegular"></span></p>
				          </div>
                            </div>
                                 <div id="divPrivado" style="display:none">
		                          <div class="option">
		                            <input type="radio" name="group" id="rdbPrivado"/>
		                            <label for="uno">
		                              Servicio Privado
		                              <span id="spnPrivado" class="price"> </span>
		                            </label>
		                          </div><br/><br/><br/>
				                  <div class="disclaimer">
				  	                <h4>Observaciones</h4>
				  	                <p class="small"><span id="obsPrivado"></span></p>
				                  </div>
                                     </div>
                        <div class="clearfix"></div>
                    </div>

                    <div class="col-xs-12 padding-left-30 padding-right-30 text-center">
                        <button onclick="return irPaso2();" class="reserve pull-left before">
                            <span>▸</span>
                            Anterior
  	         
                        </button>

                        <button onclick="return grabarDatos();" class="reserve after">
                            Siguiente
  	           
                            <span>▸</span>
                        </button>

                    </div>
                </div>
            </div>
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>


