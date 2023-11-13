<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="pedidos.aspx.cs" Inherits="admin_modulos_custom_pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: Pedidos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/views/custom/pedidos.js?v=1") %>"></script>
    <link rel="stylesheet" href="<%= ResolveClientUrl("~/admin/css/bootstrap.css") %>" type="text/css" media="screen" />
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/bootstrap-transition.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/bootstrap-modal.js") %>"></script>
    <link href="<%= ResolveClientUrl("~/admin/css/kendo.common.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveClientUrl("~/admin/css/kendo.silver.min.css") %>" rel="stylesheet" />
    <script type="text/javascript" src="<%= ResolveClientUrl("~/admin/js/kendo.all.min.js") %>"></script>

    <script type="text/javascript">
        function desvalidar(id) {
            if (confirm("¿Esta seguro que desea desvalidar el item seleccionado?")) {
                $.ajax({
                    type: "POST",
                    url: "pedidos.aspx/Desvalidar",
                    data: "{ id: " + id + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //data: "id=" + dataItem.IDTutorial,
                    success: function (data, text) {
                        $("#tdValidado_" + id).html("No");
                        $("#tdDesvalidar_" + id).html("");
                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);
                        alert(r.Message);
                    }
                });
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container_12">

        <h4>Administración de Pedidos</h4>

        <div class="form-grid">
            <br />
            <table cellpadding="3" cellspacing="2">
                <tr>
                    <td>
                        <label class="fldTitle">Operador</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtOperador" CssClass="full" MaxLength="128" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Contacto</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtContacto" CssClass="full" MaxLength="128" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Pasajero</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtPasajero" CssClass="full" MaxLength="128" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Fecha Compra desde</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtAltaDesde" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Fecha Compra hasta</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtAltaHasta" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="fldTitle">Estadia IN desde</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtEstadiaInDesde" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Estadia IN hasta</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtEstadiaInHasta" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Estadia OUT desde</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtEstadiaOutDesde" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Estadia OUT hasta</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtEstadiaOutHasta" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td class="buttons bottom-round noboder">
                        <label class="fldTitle">&nbsp;</label>
                        <div class="fieldwrap">
                            <input type="button" id="btnBuscar" value="Buscar" class="submit-button-light" onclick="filter();" />
                            <input type="button" id="btnVerTodos" value="Ver todos" class="submit-button-light" onclick="verTodos();" />
                            <button class="submit-button-blue" type="button" id="btnExportar" onclick="exportar();">Exportar a Excel</button>
                            <img src="/admin/images/ajax-loader/ajax-loader-bar-1.gif" id="imgLoading" style="display: none" />
                            <a href="" id="lnkDownload" download="Pedidos" style="display: none">Descargar</a>
                        </div>
                    </td>
                </tr>
            </table>
            <div runat="server" id="divErrores" class="notification-wrap failure" visible="false" style="width: 740px; margin-left: 0px">
                <span class="icon-failure">ERROR</span>
                <p id="msgError">
                    <asp:Literal runat="server" ID="litError"></asp:Literal>
                </p>
            </div>
        </div>

        <br />
        <div id="grid"></div>
    </div>

    <div id="modalPedido" class="modal hide fade" tabindex="-1" role="dialog">
        <div class="modal-header" style="background-color: #e8e8e8">
            <button type="button" class="close" data-dismiss="modal" id="btnCloseModal">×</button>
            <h3>Detalles de pedido</h3>
        </div>
        <div class="modal-body">
            <div id="wrapperDialog">

                <div class="container_12">
                    <div id="divEdicionModal" class="form-grid">
                        <div class="alert alert-error" id="divErrorModal" style="display: none"></div>

                        <div id="divTabla"></div>

                        <%--<div class="form leftLabel">
                            <ul style="list-style-type: none" id="ulModal">
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Codigo</label>
                                    <div class="fieldwrap">
                                        <label id="lblCodigo"></label>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Tipo</label>
                                    <div class="fieldwrap">
                                        <label id="lblTipo"></label>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Precio</label>
                                    <div class="fieldwrap">
                                        <label id="lblPrecio"></label>
                                    </div>
                                </li>
                            </ul>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button class="btn" id="btnAceptar" onclick="cerrarModal();">Aceptar</button>
            <%--<button class="btn" id="btnAceptarModal" onclick="contestarSolicitud('A');return false;">Aceptar</button>--%>
            <%--<button class="btn" id="btnRechazarModal" onclick="contestarSolicitud('R');return false;">Rechazar</button>--%>
        </div>
    </div>
</asp:Content>



