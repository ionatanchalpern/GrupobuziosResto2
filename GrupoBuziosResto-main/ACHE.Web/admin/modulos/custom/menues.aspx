<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="menues.aspx.cs" Inherits="admin_modulos_custom_menues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Admin :: Menúes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/views/custom/menues.js") %>"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container_12">
		
		<h4>Administración de Menúes</h4>
		
		<div class="form-grid">
			<br />
			<table cellpadding="3" cellspacing="2">
				<tr>
                    <td>
						<label class="fldTitle">Restaurant</label>
						<div class="fieldwrap">
							<asp:DropDownList ClientIDMode="Static" runat="server" ID="cmbRestos"></asp:DropDownList>
						</div>		
					</td>
                    <td style="width:20px">&nbsp;</td>
                    <td>
						<label class="fldTitle">Tipo Menú</label>
						<div class="fieldwrap">
							<asp:DropDownList ClientIDMode="Static" runat="server" ID="cmbTipos">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Turista" Value="Turista"></asp:ListItem>
                                <asp:ListItem Text="Premium" Value="Premium"></asp:ListItem>
                                <asp:ListItem Text="Menores" Value="Menores"></asp:ListItem>
                                <asp:ListItem Text="Clasico" Value="Clasico"></asp:ListItem>
                                <asp:ListItem Text="Playa" Value="Playa"></asp:ListItem>
                                <asp:ListItem Text="Buffet Libre" Value="Libre"></asp:ListItem>

							</asp:DropDownList>
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
					<asp:Literal runat="server" id="litError"></asp:Literal>
				</p>
			</div>
		</div>

		<br />
        <div id="grid"></div>
    </div>
</asp:Content>
