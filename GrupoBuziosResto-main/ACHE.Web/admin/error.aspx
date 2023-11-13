<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Error.master" AutoEventWireup="true" Inherits="admin_error" CodeFile="error.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="error-container">
		<div class="leftSide">
			<div class="errorCode">
				 error
			</div>
			<div class="bubble">
				<h3><span>Oops!</span>Ha ocurrido un error...</h3>
				<h4>Lo siento</h4>
				<p>
					 La accion que intentas realizar ha causado un error inesperado. :(
				</p>
			</div>
		</div>
		<div class="rightSide">
			<h3><span>Y ahora?</span> Podes acceder a ...</h3>
			<ol>
                <li>Administrar tus <a href="modulos/pedidos/categorias.aspx">categorias</a>.</li>
				<li>Administrar tus <a href="modulos/pedidos/productos.aspx">productos</a>.</li>
				<li>Administrar tus <a href="modulos/pedidos/pedidos.aspx">pedidos</a>.</li>
				<li>Administrar tus <a href="modulos/pedidos/clientes.aspx">clientes</a>.</li>
			</ol>
			<p class="error-report">
				 Si crees que esto no deberia haber sucedido puedes comunicarte con el administrador.
			</p>
		</div>
	</div>
</asp:Content>
