<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

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
                    <li class="active">Acceso</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="container content-padding">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-10 col-lg-10 col-md-offset-1 col-lg-offset-1 login-register-blocks">
                <h1>Inicia Sesión en Grupo Búzios Restó</h1>
                <div class="row blocks">
                    <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5 block">
                        <h2>Operadores Mayoristas</h2>
                        <asp:Panel ID="Panel2" CssClass="form" runat="server" DefaultButton="btnIngresarOp">
                            <asp:Label runat="server" ID="lblErrorOperador" ClientIDMode="Static" CssClass="errorRequired"></asp:Label>
                            <label>Email</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmailOp"></asp:TextBox>
                            <label>Contraseña</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPwdOp" TextMode="Password"></asp:TextBox>
                            <a class="forgot" href="recuperar.aspx?Mode=O">¿Olvido su contraseña?</a>
                            <hr />
                            <div class="submit">
                                <%--<span class="remember">
                                    <asp:CheckBox runat="server" ID="chkRecordarOp" />
                                    Recordarme
                                </span>--%>
                                <asp:Button runat="server" Text="Ingresar" ID="btnIngresarOp" OnClientClick="return validarOperador();" OnClick="btnIngresarOp_Click"></asp:Button>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5 block pull-right">
                        <h2>Restaurantes</h2>

                        
                        <asp:Panel ID="Panel1" CssClass="form" runat="server" DefaultButton="btnIngresarRes">
                            <asp:Label runat="server" ID="lblErrorRestaurant" ClientIDMode="Static" CssClass="errorRequired"></asp:Label>
                            <label>Email</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmailRes"></asp:TextBox>
                            <label>Senha</label>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPwdRes" TextMode="Password"></asp:TextBox>
                            <a class="forgot" href="recuperar.aspx?Mode=R">Esqueceu sua senha?</a>
                            <hr />
                            <div class="submit">
                                <%--<span class="remember">
                                    <asp:CheckBox runat="server" ID="chkRecordarRes" />
                                    Recordarme
                                </span>---%>
                                <asp:Button runat="server" Text="Inicie Sessão" ID="btnIngresarRes" OnClientClick="return validarRestaurant();" OnClick="btnIngresarRes_Click"></asp:Button>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">

    <script type="text/javascript">
        function validarOperador() {
            $('#lblErrorRestaurant').hide();
            var isValid = true;
            var msg = "";

            if ($('#txtEmailOp').val() == "") {
                isValid = false;
                msg = "Debe ingresar su email";
            }
            else if (!IsValidEmail($('#txtEmailOp').val())) {
                isValid = false;
                msg = "Debe ingresar un email válido";
            }
            else if ($('#txtPwdOp').val() == "") {
                isValid = false;
                msg = "Debe ingresar su contraseña";
            }

            if (isValid) {
                $('#lblErrorOperador').html("");
                return true;
            }
            else {
                $('#lblErrorOperador').show();
                $('#lblErrorOperador').html(msg + "<br /><br />");
                return false;
            }
        }

        function validarRestaurant() {
            $('#lblErrorOperador').hide();
            var isValid = true;
            var msg = "";

            if ($('#txtEmailRes').val() == "") {
                isValid = false;
                msg = "Você deve digitar um endereço de email";
            }
            else if (!IsValidEmail($('#txtEmailRes').val())) {
                isValid = false;
                msg = "Você deve digitar um endereço de email válido";
            }
            else if ($('#txtPwdRes').val() == "") {
                isValid = false;
                msg = "Você deve digitar sua senha";
            }

            if (isValid) {
                $('#lblErrorRestaurant').html("");
                return true;
            }
            else {
                $('#lblErrorRestaurant').show();
                $('#lblErrorRestaurant').html(msg + "<br /><br />");
                return false;
            }
        }

        $(document).ready(function () {
            $("#txtEmailRes, #txtPwdRes").keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    return validarRestaurant();
                }
            });


            $("#txtEmailOp, #txtPwdOp").keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    return validarOperador();
                }
            });

            
        });

    </script>

</asp:Content>

