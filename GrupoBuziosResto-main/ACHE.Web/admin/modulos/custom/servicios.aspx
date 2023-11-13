<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="servicios.aspx.cs" Inherits="admin_modulos_custom_servicios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Admin :: Servicios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/views/custom/servicios.js?v=1") %>"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container_12">
		<h4>Administración de Servicios</h4>
		<div class="form-grid">
			<br />
			<table cellpadding="3" cellspacing="2">
				<tr>
                    <td>
						<label class="fldTitle">Proveedor</label>
						<div class="fieldwrap">
							<asp:DropDownList runat="server" ID="cmbProveedor" ClientIDMode="Static" CssClass="form-control" />
						</div>		
					</td>
                    <td style="width:20px">&nbsp;</td>
                    <td>
						<label class="fldTitle">Nombre</label>
						<div class="fieldwrap">
							<asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" CssClass="form-control" MaxLength="50" />
						</div>		
					</td>
                    <td style="width:20px">&nbsp;</td>
					<td class="buttons bottom-round noboder">
						<label class="fldTitle">&nbsp;</label>
						<div class="fieldwrap">
                        	<input type="button" id="btnBuscar" value="Buscar" class="submit-button-light" onclick="filter();" />
                            <input type="button" id="btnVerTodos" value="Ver todos" class="submit-button-light" onclick="verTodos();" />
                            <input type="button" id="btnNuevo" value="Nuevo" class="submit-button" onclick="nuevo();" />
						</div>
					</td>
				</tr>
			</table>
			<div runat="server" id="divErrores" class="notification-wrap failure" visible="false" style="width:740px; margin-left:0px">
				<span class="icon-failure">ERROR</span>
				<p id="msgError">
					<asp:Literal runat="server" id="litError" />
				</p>
			</div>
		</div>
		<br />
        <div id="grid"></div>
    </div>
</asp:Content>
