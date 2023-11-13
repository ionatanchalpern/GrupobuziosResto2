<%@ Page Language="C#" AutoEventWireup="true" Inherits="admin_default" CodeFile="default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin :: Login</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link rel="shortcut icon" href="<%= ResolveUrl("~/imgs/favicon.ico") %>" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/admin/css/styles.css") %>" type="text/css" media="screen"/>
	<link rel="stylesheet" href="<%= ResolveUrl("~/admin/css/ui/jquery-ui-1.8.13.custom.css") %>" type="text/css" media="screen"/>
	<link rel="stylesheet" href="<%= ResolveUrl("~/admin/css/grid.css") %>" type="text/css" media="screen"/>
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/jquery-1.8.2.min.js") %>"></script>
	<script type="text/javascript" src="<%= ResolveUrl("~/admin/js/jquery-ui-1.8.16.custom.js") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="login-wrapper">
	        <div id="login-header" class="top-round">
		        <div class="login-left">
			        <span class="icon-wrap-lb-less"><span class="icon-block-black key-tw-b"></span>Acceso al sistema</span>
		        </div>
		        <div class="login-right">
			        <a href="#" title="Admin Panel"><img src="<%= ResolveUrl("~/admin/images/logo.png") %>" alt="Admin Panel" /></a>
		        </div>
	        </div>
            <div class="login-box bottom-round">
                <asp:Panel runat="server" ID="pnlLoginError" Visible="false" class="notification-wrap failure">
			        <span class="icon-failure">ERROR</span>
			        Email y/o contraseña incorrecta
		        </asp:Panel>
		        <ul>
			        <li>
                        <label>Email</label>
                        <asp:TextBox runat="server"  ID="txtUsuario" CssClass="login-text-box"></asp:TextBox>
                    </li>
			        <li>
                        <label>Contrase&ntilde;a</label>
                        <asp:TextBox runat="server"  ID="txtPwd" TextMode="Password" CssClass="login-text-box usr"></asp:TextBox>
                    </li>
			        <li>
                        <label>&nbsp;</label>
                        <asp:Button runat="server" ID="btnAceptar" CssClass="submit-button-login" OnClick="Login" Text="Aceptar" />
                    </li>
			        <%--<li style="margin-left:120px"><a href="recuperar-pass.aspx">¿Olvidaste tu contrase&ntilde;a?</a></li>--%>
		        </ul>
	        </div>
        </div>
        <div id="footer-wrap">
	        <div id="footer">
		        <div class="login-footer-container">
			         2014 - GrupoBuziosResto
		        </div>
	        </div>
        </div>

    </form>
</body>
</html>

