<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="pedidosTraslado.aspx.cs" Inherits="admin_modulos_custom_pedidos_traslado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: Pedidos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/views/custom/pedidosTraslado.js?v=1") %>"></script>
    <link rel="stylesheet" href="<%= ResolveClientUrl("~/admin/css/bootstrap.css") %>" type="text/css" media="screen" />
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/bootstrap-transition.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/bootstrap-modal.js") %>"></script>
    <link href="<%= ResolveClientUrl("~/admin/css/kendo.common.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveClientUrl("~/admin/css/kendo.silver.min.css") %>" rel="stylesheet" />
    <script type="text/javascript" src="<%= ResolveClientUrl("~/admin/js/kendo.all.min.js") %>"></script>
    <script  type="text/javascript"  src="<%= ResolveUrl("~/admin/js/jquery.dataTables.min.js") %>"></script>
    <script  type="text/javascript"  src="<%= ResolveUrl("~/admin/js/jquery.dataTables.bootstrap.min.js") %>"></script>
	
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container_12">
        <h4>Administración de Pedidos de Traslado</h4>
        <div class="form-grid">
            <br />
            <table cellpadding="3" cellspacing="2">
                <tr>
                    <td>
                        <label class="fldTitle">Operador</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtOperador" CssClass="full" MaxLength="128" ClientIDMode="Static" />
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldtitle">Contacto</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtContacto" CssClass="full" MaxLength="128" ClientIDMode="Static" />
                        </div>
                    </td>
                   <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Pasajero</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtPasajero" CssClass="full" MaxLength="128" ClientIDMode="Static" />
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Fecha Reserva desde</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtAltaDesde" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Fecha Reserva hasta</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtAltaHasta" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="fldTitle">Fecha Ida desde</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtIdaDesde" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Fecha Ida hasta</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtIdaHasta" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Fecha Vuelta desde</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtVueltaDesde" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Fecha Vuelta hasta</label>
                        <div class="fieldwrap" style="margin-left: 5px; margin-bottom: 15px;">
                            <asp:TextBox runat="server" ID="txtVueltaHasta" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0; margin-left: -5px; height: 15px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td class="buttons bottom-round noboder">
                        <label class="fldTitle">&nbsp;</label>
                        <div class="fieldwrap">
                            <input type="button" id="btnBuscar" value="Buscar" class="submit-button-light" onclick="filter();" />
                            <input type="button" id="btnVerTodos" value="Ver todos" class="submit-button-light" onclick="verTodos();" />
                            <button class="submit-button-blue" type="button" id="btnExportar" onclick="exportar();">Exportar</button>
                            <img src="/admin/images/ajax-loader/ajax-loader-bar-1.gif" id="imgLoading" style="display: none" />
                            <a href="" id="lnkDownload" download="Pedidos" style="display: none">Descargar</a>
                        </div>
                    </td>
                </tr>
            </table>
            <div runat="server" id="divErrores" class="notification-wrap failure" visible="false" style="width: 740px; margin-left: 0px">
                <span class="icon-failure">ERROR</span>
                <p id="msgError">
                    <asp:Literal runat="server" ID="litError" />
                </p>
            </div>
        </div>
        <br />
        <div id="grid"></div>
    </div>
     <div class="modal fade" id="modalPasajeros">
		<div class="modal-dialog">
			<div class="modal-content" >
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h3 class="modal-title" id="titPasajeros"></h3>
				</div>
				<div class="modal-body">
					<!--div class="alert alert-info">In this table jquery plugin turns a table row into a clickable link.</!--div-->
					<table class="table table-condensed table-striped" id="tablePasajeros">
						<thead id="headPasajeros">
							<tr>
                                <th>Nombre</th> 
                                <th>DNI</th> 
                            </tr>
						</thead>
						<tbody id="bodyPasajeros">
							
						</tbody>
					</table>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-default" onclick="$('#modalPasajeros').modal('hide');">Cerrar</button>
				</div>
			</div>
		</div>
	</div>
</asp:Content>



