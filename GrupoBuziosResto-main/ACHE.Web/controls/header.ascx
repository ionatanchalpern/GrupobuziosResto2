<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="controls_header" %>


<div class="container-fluid top-bar">
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Panel runat="server" ID="pnlNoLogin" Visible="false">
                    <a class="register" href="registro.aspx">Registrarse</a>
                    <a class="access" href="login.aspx">Acceso a grupo buzios resto</a>
                    <a class="traslados" href="login-traslados.aspx">Acceso a traslados</a>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlLogin" Visible="false">
                    <span class="loginname">Bienvenido: <asp:Literal runat="server" ID="litUsuario" /></span>
                    <a id="dropdownMenu1" href="#" data-toggle="dropdown" aria-expanded="true">Mi Cuenta</a>

                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                        <li runat="server" id="liMisdatos" role="presentation"><a role="menuitem" tabindex="-1" href="misDatos.aspx">Mis Datos</a></li>
                        <li runat="server" id="liValidar" role="presentation"><a role="menuitem" tabindex="-1" href="validar.aspx">Validar</a></li>
                        <li role="presentation"><a role="menuitem" tabindex="-1" href="cambiarPwd.aspx"><%= (( ACHE.Model.WebUser)Session["CurrentUser"]).TipoUsuario=="O" ? "Modificar Contraseña": "Alterar a senha" %></a></li>
                        <li runat="server" id="liHistorial" role="presentation"><a role="menuitem" tabindex="-1" href="historial.aspx">Historial de Compras</a></li>
                        <li runat="server" id="liHistorialCupones" role="presentation"><a role="menuitem" tabindex="-1" href="historialCupones.aspx">Cupons de história</a></li>
                        <li runat="server" id="liHistorialTraslados" role="presentation"><a role="menuitem" tabindex="-1" href="historial-traslados.aspx">Historial de traslados</a></li>
                        <li role="presentation"><a role="menuitem" tabindex="-1" href="logout.aspx"><%= (( ACHE.Model.WebUser)Session["CurrentUser"]).TipoUsuario=="O" ? "Cerrar sesión": "Cancelar assinar" %></a></li>
                    </ul>
                    <%--</asp:Panel>
                    <asp:Panel runat="server" ID="pnlOperador" Visible="false">
                         <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="misDatos.aspx">Mis Datos</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="cambiarPwd.aspx">Modificar Contraseña</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="historial.aspx">Historial de Compras</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="logout.aspx">Cerrar sesión</a></li>
                        </ul>
                    </asp:Panel>--%>
                </asp:Panel>

                <div class="dropdown responsive-menu hide">
                    <button class="btn btn-default dropdown-toggle" type="button" id="Button1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                        Menu <i class="fa fa-bars"></i>
                    </button>
                    <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">
                        <li><a href="registro.aspx">Registrarse</a></li>
                        <li><a href="login.aspx">Acceso a grupo buzios resto</a></li>
                        <li><a href="login-traslados.aspx">Acceso a traslados</a></li>
                        <%--<li><a href="#">Historial de Compras</a></li>
                        <li><a href="#">Acceso a Traslados</a></li>--%>
                    </ul>
                </div>

            </div>
        </div>
    </div>
</div>

<div class="container-fluid head">
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 logo">
                <asp:HyperLink runat="server" ID="lnkLogo" NavigateUrl="~/default.aspx">
                    <asp:Image runat="server" ID="imgLogo" ImageUrl="~/imgs/logo.png" />
                </asp:HyperLink>
            </div>

            <div class="col-xs-12 col-sm-8 col-mg-8 col-lg-8 logo-side-menu w-buy">
                <ul>
                    <li>
                        <asp:HyperLink runat="server" ID="lnkHome" NavigateUrl="~/default.aspx">Home</asp:HyperLink>
                        <%--<a href="default.aspx">Home</a>--%>
                    </li>

                    <li>
                        <a href="quienes-somos.aspx">Quiénes Somos</a>
                    </li>

                    <li>
                        <a href="contacto.aspx">Contacto</a>
                    </li>
                    <li runat="server" id="liComprar">
                        <a class="buy" href="productos.aspx">Comprar <img src="imgs/buy-index.png" /></a>
                    </li>
                    <li runat="server" id="liValidar2" style="top: -2px; position: relative;">
                        <a href="validar.aspx"><img src="imgs/boton_validar.png" style="margin-left: -10px;" /></a>
                    </li>
                    <li runat="server" id="liMapa">
                        <a href="mapa.aspx"><img src="/imgs/icono-MAPA.png" style="padding-bottom: 10px; margin-left: -30px;" /></a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</div>
