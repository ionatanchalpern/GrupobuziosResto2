<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="productos.aspx.cs" Inherits="productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="<%= ResolveUrl("~/css/lightbox.css") %>" rel="stylesheet" />
    <script type="text/javascript" src="<%= ResolveUrl("~/js/lightbox.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/carrito.js?v=25") %>"></script>
    <script type="text/javascript" src="/js/bootstrap-datetimepicker.min.js"></script>

    <style type="text/css">
        .lb-closeContainer {
            display: none;
        }

        .lightbox
        {
            display: none;
        }

        .titulo {
            float: left;
            margin-right: 25px;
            background-color: #D0C8B5;
            max-width: 210px;
            text-align: center;
            padding: 10px 15px 10px 15px;
            color: #570D32;
            font-family: 'Cuprum', sans-serif;
            font-size: 25px;
            font-weight: bold;
            height: 52px;
        }

        .fechas {
            background-color: #570D32;
        }

        .fechas tr td {
                padding: 5px 10px 5px 10px;
            }
        
        .fechas tr td.first {
                    padding-left: 100px;
                }
        
        .fechas tr td.last {
                    padding-right: 100px;
                }
        
        .fechas tr td label {
                    color: #ffffff;
                    font-family: 'Cuprum', sans-serif;
                    font-size: 26px;
                    font-weight: bold;
                }
        
        .fechas tr td .button {
                    background: #ca4d4a;
                    border: 0;
                    font-family: 'Cuprum', sans-serif;
                    text-transform: uppercase;
                    color: white;
                    font-size: 22px;
                    padding: 5px 11px;
                    line-height: 22px;
                    vertical-align: middle;
                    cursor: pointer;
                }
     
        .fechas tr td .button:hover {
                        text-decoration: none;
                    }
     
        hr.separator {
            border: 0;
            border-top: 1px solid #ab9b84;
            margin-top: 10px;
            margin-bottom: 10px;
        }
    </style>
     <script type="text/javascript">
         $(function () {
             $('.fecha').datepicker({
                 format: 'dd/mm/yyyy',
                 autoclose: true,
                 language: "es",
                 todayHighlight: true
             });
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    
   
    <div class="container content-padding" style="padding-top: 0px; padding-bottom: 0px">
        <div class="row">
            <asp:HiddenField   runat="server" ID="hdnValidarFecha" ClientIDMode="Static" />
            <div class="col-lg-12" style="padding-bottom: 20px;">
                <label id="lblOk" class="ok" ></label>
                <label id="lblError" class="errorRequired" ></label>
                <br />
                <div class="row">
                    <div class="col-xs-12 col-sm-3 colFE">
                      <div class="tituloFecha">Fecha de estad&iacute;a</div>
                    </div>
                    <div class="col-xs-12 col-sm-9 colDH">
                          <div class="row">
                              <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-xs-4 col-md-3 tituloFecha2"><label><span style="color:#ca4d4a">*&nbsp;</span>Desde:&nbsp;</label> </div>
                                    <div class="col-xs-5 col-md-7">
                                        <input name="txtEstadiaDesde" type="text" maxlength="10" id="txtEstadiaDesde" class="fecha" style="width: 100%" />
                                    </div>
                                    <div class="col-xs-3 col-md-2">
                                      <a class="calendar" onclick="$('#txtEstadiaDesde').datepicker('show');" style="cursor:pointer">
                                          <img src="./imgs/calendar-icon.png" />
                                      </a>
                                    </div>
                                </div>
                              </div>
                              <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-xs-4 col-md-3 tituloFecha2"><label><span style="color:#ca4d4a">*&nbsp;</span>Hasta:&nbsp;</label></div>
                                    <div class="col-xs-5 col-md-7">
                                        <input name="txtEstadiaHasta" type="text" maxlength="10" id="txtEstadiaHasta" class="fecha" style="width: 100%"  />
                                    </div>
                                    <div class="col-xs-3 col-md-2">
                                      <a class="calendar" onclick="$('#txtEstadiaHasta').datepicker('show');" style="cursor:pointer">
                                          <img src="./imgs/calendar-icon.png" />
                                      </a>
                                    </div>
                                </div>
                              </div>
                          </div>
                    </div>
                </div>

                <br />
                <hr class="separator" />
              
            </div>
        </div>

         <asp:Panel runat="server" id="pnlClasico" Visible="false" style="border: 1px solid #d8d2c5; border-bottom: 11px solid #d8d2c5; margin-bottom: 20px ">
            <div class="row"  style="margin: 0;">
                <div class="col-xs-12 col-sm-12 col-md-4 menuImgC">
                    <img src="./imgs/menus/clasico2.png" class="menuPerfil" alt="" />
                </div>
                <div runat="server" id="divinfoClasico" class="col-xs-12 col-sm-6 col-md-4 infoMenu">
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 infoMenu ">
                        <div class="dividerCants"></div>
                        <div class="lastCol">
                           
                            <span class="amount">
                                <label>
                                    Cantidad<br class="hidden-xs" />
                                    Comidas
                                </label>
                                <span class="select-amount">
                                                         <asp:DropDownList runat="server" ID="ddlCantCenasClasico" ClientIDMode="Static"></asp:DropDownList>

                                </span>
                            </span>
                            <div class="cant-pax-dinner">
                                <span class="amount">
                                    <label>Cantidad<br class="hidden-xs"  />pax</label>
                                    <span class="select-amount">                             
                                               <asp:DropDownList runat="server" ID="ddlCantPaxClasico" ClientIDMode="Static"></asp:DropDownList>
   </span>
                                </span>
                                <asp:Literal runat="server" ID="litAgregarClasico"></asp:Literal>
                            </div>
                        </div>
                </div>
            </div>
        </asp:Panel>

         <asp:Panel runat="server" id="pnlTurista" Visible="false" style="border: 1px solid #b4a78a; border-bottom: 11px solid #b4a78a; margin-bottom: 20px ">
            <div class="row"  style="margin: 0;">
                <div class="col-xs-12 col-sm-12 col-md-4 menuImgC">
                    <img src="./imgs/menus/turista2.png" class="menuPerfil" alt="" />
                </div>
                <div runat="server" id="divInfoTurista" class="col-xs-12 col-sm-6 col-md-4 infoMenu">
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 infoMenu ">
                        <div class="dividerCants"></div>
                        <div class="lastCol">
                           
                            <span class="amount">
                                <label>
                                    Cantidad<br class="hidden-xs" />
                                    Comidas
                                </label>
                                <span class="select-amount">
                                
                                                                        <asp:DropDownList runat="server" ID="ddlCantCenasTurista" ClientIDMode="Static"></asp:DropDownList>

                                

                                </span>
                            </span>
                            <div class="cant-pax-dinner">
                                <span class="amount">
                                    <label>Cantidad<br class="hidden-xs"  />pax</label>
                                    <span class="select-amount">
                                                                    <asp:DropDownList runat="server" ID="ddlCantPaxTurista" ClientIDMode="Static"></asp:DropDownList>

                                    </span>
                                </span>
                                <asp:Literal runat="server" ID="litAgregarTurista"></asp:Literal>
                             <!---   <a class='button' onclick="AddItem(4, $('#ddlCantCenasClasico').val(), $('#ddlCantPaxClasico').val())" style='cursor:pointer'>Agregar <img src='./imgs/buy-proc-compra.png' /></a>-->
                            </div>
                        </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" id="pnlBuffetLibre" Visible="false" style="border: 1px solid #b4a78a; border-bottom: 11px solid #b4a78a; margin-bottom: 20px ">
            <div class="row"  style="margin: 0;">
                <div class="col-xs-12 col-sm-12 col-md-4 menuImgC">
                    <img src="./imgs/menus/buffetLibre.png" class="menuPerfil" alt="" />
                </div>
                <div runat="server" id="divInfoBuffetLibre" class="col-xs-12 col-sm-6 col-md-4 infoMenu">
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 infoMenu ">
                        <div class="dividerCants"></div>
                        <div class="lastCol">
                           
                            <span class="amount">
                                <label>
                                    Cantidad<br class="hidden-xs" />
                                    Comidas
                                </label>
                                <span class="select-amount">
                                
                                                                        <asp:DropDownList runat="server" ID="ddlCantCenasBuffetLibre" ClientIDMode="Static"></asp:DropDownList>

                                

                                </span>
                            </span>
                            <div class="cant-pax-dinner">
                                <span class="amount">
                                    <label>Cantidad<br class="hidden-xs"  />pax</label>
                                    <span class="select-amount">
                                                                    <asp:DropDownList runat="server" ID="ddlCantPaxBuffetLibre" ClientIDMode="Static"></asp:DropDownList>

                                    </span>
                                </span>
                                <asp:Literal runat="server" ID="litAgregarBuffetLibre"></asp:Literal>
                             <!---   <a class='button' onclick="AddItem(4, $('#ddlCantCenasClasico').val(), $('#ddlCantPaxClasico').val())" style='cursor:pointer'>Agregar <img src='./imgs/buy-proc-compra.png' /></a>-->
                            </div>
                        </div>
                </div>
            </div>
        </asp:Panel>


       <!--

         <asp:Panel runat="server" id="pnlPremium" Visible="false" style="border: 1px solid #4b1534; border-bottom: 11px solid #4b1534; margin-bottom: 20px ">
            <div class="row"  style="margin: 0;">
                <div class="col-xs-12 col-sm-12 col-md-4 menuImgC">
                    <img src="./imgs/menus/premium2.png" class="menuPerfil" alt="" />
                </div>
                <div runat="server" id="divInfoPremium" class="col-xs-12 col-sm-6 col-md-4 infoMenu">

                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 infoMenu ">
                        <div class="dividerCants"></div>
                        <div class="lastCol">
                           
                            <span class="amount">
                                <label>
                                    Cantidad<br class="hidden-xs" />
                                    Comidas
                                </label>
                                <span class="select-amount">
                                                                  <asp:DropDownList runat="server" ID="ddlCantCenasPremium" ClientIDMode="Static"></asp:DropDownList>

                                </span>
                            </span>
                            <div class="cant-pax-dinner">
                                <span class="amount">
                                    <label>Cantidad<br class="hidden-xs"  />pax</label>
                                    <span class="select-amount">
                                                                        <asp:DropDownList runat="server" ID="ddlCantPaxPremium" ClientIDMode="Static"></asp:DropDownList>

                                    </span>
                                </span>
                                <asp:Literal runat="server" ID="litAgregarPremium"></asp:Literal>
                            </div>
                        </div>
                </div>
            </div>
        </asp:Panel>
     
        
         <asp:Panel runat="server" id="pnlPlaya" Visible="false" style="border: 1px solid #4b1534; border-bottom: 11px solid #4b1534; margin-bottom: 20px ">
            <div class="row"  style="margin: 0;">
                <div class="col-xs-12 col-sm-12 col-md-4 menuImgC">
                    <img src="./imgs/menus/playa.png" class="menuPerfil" alt="" />
                </div>
                <div runat="server" id="divInfoPlaya" class="col-xs-12 col-sm-6 col-md-4 infoMenu">

                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 infoMenu ">
                        <div class="dividerCants"></div>
                        <div class="lastCol">
                           
                            <span class="amount">
                                <label>
                                    Cantidad<br class="hidden-xs" />
                                    Comidas
                                </label>
                                <span class="select-amount">
                                                                  <asp:DropDownList runat="server" ID="ddlCantCenasPlaya" ClientIDMode="Static"></asp:DropDownList>

                                </span>
                            </span>
                            <div class="cant-pax-dinner">
                                <span class="amount">
                                    <label>Cantidad<br class="hidden-xs"  />pax</label>
                                    <span class="select-amount">
                                                                        <asp:DropDownList runat="server" ID="ddlCantPaxPlaya" ClientIDMode="Static"></asp:DropDownList>

                                    </span>
                                </span>
                                <asp:Literal runat="server" ID="litAgregarPlaya"></asp:Literal>
                            </div>
                        </div>
                </div>
            </div>
        </asp:Panel>
     -->

         <asp:Panel runat="server" id="pnlMenores" Visible="false" style="border: 1px solid #6c5c5d; border-bottom: 11px solid #6c5c5d; margin-bottom: 20px ">
            <div class="row"  style="margin: 0;">
                <div class="col-xs-12 col-sm-12 col-md-4 menuImgC">
                    <img src="./imgs/menus/kids2.png" class="menuPerfil" alt="" />
                </div>
                <div runat="server" id="divInfoMenores" class="col-xs-12 col-sm-6 col-md-4 infoMenu">
                       

                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 infoMenu ">
                        <div class="dividerCants"></div>
                        <div class="lastCol">
                            
                            <span class="amount">
                                <label>
                                    Cantidad<br class="hidden-xs" />
                                    Comidas
                                </label>
                                <span class="select-amount">
                                    <asp:DropDownList runat="server" ID="ddlCantCenasKids" ClientIDMode="Static"></asp:DropDownList>

                                </span>
                            </span>
                            <div class="cant-pax-dinner">
                                <span class="amount">
                                    <label>Cantidad<br class="hidden-xs"  />pax</label>
                                    <span class="select-amount">
                                                                           <asp:DropDownList runat="server" ID="ddlCantPaxKids" ClientIDMode="Static"></asp:DropDownList>

                                    </span>
                                </span>
                                <asp:Literal runat="server" ID="litAgregarMenores"></asp:Literal>
                             <!---   <a class='button' onclick="AddItem(4, $('#ddlCantCenasClasico').val(), $('#ddlCantPaxClasico').val())" style='cursor:pointer'>Agregar <img src='./imgs/buy-proc-compra.png' /></a>-->
                            </div>
                        </div>
                </div>
            </div>
        </asp:Panel>
                 
                
  </div>
  <div class="clearfix"></div>
  <br />
 
      <a href="/imgs/confirm.png" id="lnkConfirm" data-lightbox="imageConfirm"></a>
      <div class=" container">
      <div class="row">
          <div class="col-md-4"></div>
          <div class="col-md-5">

          </div>
          <div class="col-md-3 text-right continue-button">

              <a style="cursor: pointer" onclick="validarFechas();">
                  <img src="./imgs/boton_continuar.png" style="margin-right: 22px;" />
              </a>
          </div>
      </div>
  </div>
<br/><br/>


   
   
    </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

