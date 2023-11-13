<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="proveedores.aspx.cs" Inherits="admin_modulos_custom_proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Admin :: Proovedores
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/views/custom/proveedores.js?v=3.6") %>"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container_12">
		<h4>Administración de Proveedores</h4>
		<div class="form-grid">
			<br />
			<table cellpadding="3" cellspacing="2">
				<tr>
                    <td>
						<label class="fldTitle">Nombre</label>
						<div class="fieldwrap">
							<asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" CssClass="form-control" MaxLength="50" />
						</div>		
					</td>
                    <td style="width:20px">&nbsp;</td>
                    <td>
						<label class="fldTitle">Email</label>
						<div class="fieldwrap">
							<asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static" CssClass="form-control" MaxLength="128" />
						</div>		
					</td>
                    <td style="width:20px">&nbsp;</td>
					<td class="buttons bottom-round noboder">
						<label class="fldTitle">&nbsp;</label>
						<div class="fieldwrap">
                        	<input type="button" id="btnBuscar" value="Buscar" class="submit-button-light" onclick="filter();" />
                            <input type="button" id="btnVerTodos" value="Ver todos" class="submit-button-light" onclick="verTodos();" />
                            <input type="button" id="btnNuevo" value="Nuevo" class="submit-button" onclick="Nuevo();" />
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
