<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="login-traslados.aspx.cs" Inherits="login_traslados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #lista li {
            display: inline;
            list-style-type: none;
            padding-right: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="/">Home</a></li>
                    <li class="active">Acceso a Traslados</li>
                </ol>
            </div>
        </div>
    </div>
    <div class="container content-padding">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-10 col-lg-10 col-md-offset-1 col-lg-offset-1 login-register-blocks">
                <h1 style="margin-left:25%">Iniciá Sesión en Traslados Red</h1>
                <div class="row blocks" align="center" style="background-image: none;text-align: left;">
                    <div class="col-xs-3"></div>
                    <div class="col-xs-6 block">
                        <h2 style="overflow:inherit">Operadores Mayoristas</h2>
                        <asp:Panel ID="pnlLogin" CssClass="form" runat="server" DefaultButton="btnIngresar">
                            <asp:Label runat="server" ID="lblError" ClientIDMode="Static" CssClass="errorRequired" />
                            <label>Email</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmail" />
                            <label>Contraseña</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPwd" TextMode="Password" />
                            <a class="forgot" href="recuperar.aspx?Mode=O">¿Olvidó su contraseña?</a>
                            <hr />
                            <div class="submit">
                                <asp:Button runat="server" Text="Ingresar" ID="btnIngresar" OnClientClick="return validar();" OnClick="btnIngresar_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-xs-3"></div>
                    <%--<div class="clearfix"></div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript">
        function validar() {
            $('#lblError').hide();
            var isValid = true;
            var msg = "";

            if ($('#txtEmail').val() == "") {
                isValid = false;
                msg = "Debe ingresar su email";
            }
            else if (!IsValidEmail($('#txtEmail').val())) {
                isValid = false;
                msg = "Debe ingresar un email válido";
            }
            else if ($('#txtPwd').val() == "") {
                isValid = false;
                msg = "Debe ingresar su contraseña";
            }

            if (isValid) {
                $('#lblError').html("");
                return true;
            }
            else {
                $('#lblError').show();
                $('#lblError').html(msg + "<br /><br />");
                return false;
            }
        }

        $(document).ready(function () {
            $("#txtEmail, #txtPwd").keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    return validar();
                }
            });
        });
    </script>
</asp:Content>

