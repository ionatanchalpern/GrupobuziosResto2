<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="carrito.aspx.cs" Inherits="carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Bootstrap Date/Time picker -->
    <script type="text/javascript" src="/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/carrito.js?v=2") %>"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="/">Home</a></li>
                    <li class="active">Mi compra</li>
                </ol>
            </div>
        </div>
    </div>

    <asp:Panel runat="server" ID="pnlLogin" Visible="false" ClientIDMode="Static">
        <br />
        <h4 style="margin-left: 130px;">Para realizar un pedido, debes ser un operador registrado. Si aún no lo sos, te puedes registrar <a href="registro.aspx">aquí</a></h4>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </asp:Panel>
    <asp:HiddenField runat="server" ID="hdnIDCurrentUser" ClientIDMode="Static" />
    <asp:Panel runat="server" ID="pnlCarrito">
        <div class="container content-padding">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-10 col-lg-10 order-table"><%--col-md-offset-1 col-lg-offset-1 --%>
                    <table>
                        <tr>
                            <th>Mi Compra</th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                        <tr class="subheader">
                            <td style="min-width: 260px">Cantidad</td>
                            <td>Menú</td>
                            <td>Precio Neto Unitario</td>
                            <td>Subtotal</td>
                            <td></td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptProductos">
                            <ItemTemplate>
                                <tr class="w-border" colspan="5" id="fila_<%# Eval("ID") %>">
                                    <td>
                                        <span style="font-size: 12px">Comidas:&nbsp;</span><input size="1" class="numeric" value="<%# Eval("CantCenas")%>" id='txtCantCenas_<%# Eval("ID")%>' />
                                        &nbsp;&nbsp;<span style="font-size: 12px">Pax:&nbsp;</span><input size="1" class="numeric" value="<%# Eval("CantPax")%>" id='txtCantPax_<%# Eval("ID")%>' style="width: 50px;" />
                                        <a class="refresh" style="cursor: pointer" onclick="ActualizarItemCarrito(<%# Eval("ID")%>, <%# Eval("CantCenas")%>, <%# Eval("CantPax")%>);return false;">
                                            <span class="glyphicon glyphicon-refresh"></span>
                                        </a>
                                    </td>
                                    <td><%# Eval("Tipo") %></td>
                                    <td id="tdPrecioUnitario_<%# Eval("ID") %>">USD <%# Eval("PrecioOperador") %></td>
                                    <td id="tdSubtotal_<%# Eval("ID") %>">USD <%# Eval("Subtotal") %>
                                    </td>
                                    <td>
                                        <a class="delete" href="javascript:RemoveItem(<%# Eval("ID")%>);">
                                            <img src="/imgs/trash-icon.png" />
                                        </a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8 col-sm-offset-3 col-md-offset-2 col-lg-offset-2 order-table-result">
                    <%--<a class="refresh-result" href="#"><span class="glyphicon glyphicon-refresh"></span></a>--%>
                    <span class="result" style="margin-right: 10px;">
                        <span>Total neto</span>
                        USD <span id="lblTotal"></span>
                    </span>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlDatos">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-10 col-lg-10 col-md-offset-1 col-lg-offset-1 date">
                        <hr class="separator" />
                        <label id="lblError" class="errorRequired"></label>
                        <table>
                            <tr>
                                <th>Fecha de estadía</th>
                                <th id="datetimepicker1">Desde: &nbsp;
                                    <b>
                                        <span runat="server" id="spnDesde" clientidmode="static"></span>
                                    </b>
                                    <%--<asp:TextBox runat="server" ID="txtEstadiaDesde" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                <a class="calendar" href="javascript:$('#txtEstadiaDesde').focus();"><img src="/imgs/calendar-icon.png" /></a>--%>
                                </th>
                                <th id="datetimepicker2">Hasta: &nbsp;
                                    <b>
                                        <span runat="server" id="spnHasta" clientidmode="static"></span>
                                    </b>
                                    <%--<asp:TextBox runat="server" ID="txtEstadiaHasta" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                <a class="calendar" href="javascript:$('#txtEstadiaHasta').focus();"><img src="/imgs/calendar-icon.png" /></a>--%>
                                </th>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">
                                    <span id="errorFechas" style="color: red; border: 0; padding-top: 0; display: none; font-weight: normal;">Por favor, ingrese la fecha de estadía<br />
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-10 col-lg-10 col-md-offset-1 col-lg-offset-1 passenger">
                        <hr class="separator" />
                        <table>
                            <tr>
                                <th>Pasajero</th>
                                <th>
                                    <span class="mandatory">*</span> Nombre del pasajero 
                                <asp:TextBox runat="server" ID="txtPasajero" ClientIDMode="Static" MaxLength="100"></asp:TextBox>
                                    <span id="errorPasajero" style="color: red; display: none; font-family: 'Cuprum', sans-serif; font-size: 18px;">
                                        <br />
                                        Por favor, ingrese el nombre del pasajero<br />
                                    </span>
                                </th>
                            </tr>
                            <tr>
                                <th class="placeholder"></th>
                                <th>
                                    <span class="mandatory">*</span> DNI del pasajero
                                <asp:TextBox runat="server" ID="txtNroDocumento" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                                    <span id="errorDni" style="color: red; display: none; font-family: 'Cuprum', sans-serif; font-size: 18px;">
                                        <br />
                                        Por favor, ingrese el DNI del pasajero<br />
                                    </span>
                                </th>
                            </tr>
                            <tr id="trPagoPor" runat="server">
                                <th class="placeholder"></th>
                                <th>Pago por
                                <asp:TextBox runat="server" ID="txtPagoPor" ClientIDMode="Static" MaxLength="100"></asp:TextBox>
                                </th>
                            </tr>
                        </table>

                        <hr class="separator" />
                        <asp:Panel runat="server" ID="pnlFinalizar">
                            <span id="errorPedido" style="color: red; float: right; border: 0; padding-top: 0; display: none; font-weight: normal; font-family: Cuprum, sans-serif; font-size: 18px;"></span>
                            <br />
                            <br />
                            <a style="cursor: pointer" onclick="validarPedido();" class="button">
                                <img src="/imgs/buy-white-icon.png" />
                                Finalizar</a>
                        </asp:Panel>

                        <div class="clearfix"></div>
                    </div>
                </div>

            </asp:Panel>
        </div>
    </asp:Panel>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript">
        $(function () {
            $("#txtNroDocumento").numeric();

            $('#txtEstadiaDesde, #txtEstadiaHasta').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                language: "es",

                todayHighlight: true
            });
        });

        function validarPedido() {
            var isValid = true;
            var msg = "";
            //if ($("#txtEstadiaDesde").val() > $("#txtEstadiaHasta").val()) {
            //    isValid = false;
            //    msg = "La fecha de estadia desde debe ser menor a fecha de estadia hasta."
            //}

            $("#errorPasajero,#errorDni, #errorFechas").hide();

            if ($("#txtEstadiaDesde").val() == "" || $("#txtEstadiaHasta").val() == "") {
                isValid = false;
                $("#errorFechas").show();
            }

            if ($("#txtPasajero").val() == "") {
                isValid = false;
                msg = "Por favor, ingrese el nombre del pasaje."
                $("#errorPasajero").show();
            }

            if ($("#txtNroDocumento").val() == "") {
                isValid = false;
                msg = "Por favor, ingrese el DNI."
                $("#errorDni").show();
            }

            if (isValid) {
                CrearPedido();
                return false;
            }
            else {
                return false;
            }

        }
    </script>

</asp:Content>

