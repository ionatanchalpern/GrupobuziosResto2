<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="registro.aspx.cs" Inherits="registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validarFormulario() {
            var isValid = true;
            var msg = "";

            if ($('#txtEmpresa').val() == "") {
                isValid = false;
                msg = "Debe ingresar la empresa"
            }
            else if ($('#txtContacto').val() == "") {
                isValid = false;
                msg = "Debe ingresar el contacto de la empresa"
            }
            else if ($('#txtDireccion').val() == "") {
                isValid = false;
                msg = "Debe ingresar la dirección"
            }
            else if ($('#txtTelefono').val() == "") {
                isValid = false;
                msg = "Debe ingresar un teléfono"
            }
            else if ($('#txtEmail').val() == "") {
                isValid = false;
                msg = "Debe ingresar un email"
            }
            else if (!IsValidEmail($('#txtEmail').val())) {
                isValid = false;
                msg = "Debe ingresar un email válido"
            }
            else if ($('#txtPwd').val() == "") {
                isValid = false;
                msg = "Debe ingresar una contraseña"
            }
            else if ($('#txtPwd2').val() == "") {
                isValid = false;
                msg = "Debe confirmar la contraseña"
            }
            else if (!(($('#txtPwd').val()) == ($('#txtPwd2').val()))) {
                isValid = false;
                msg = "Las contraseñas deben coincidir"
            }
            else if (!($("#chkTerminos").is(':checked'))) {
                isValid = false;
                msg = "Debe aceptar todos los términos y condiciones del servicio y politica de privacidad."
            }

            if (isValid) {
                return true;
            }
            else {
                $('#lblError').html(msg + "<br /><br />");
                $('html, body').animate({
                    scrollTop: $("#divSuperior").offset().top
                }, 1000);
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="default.aspx">Home</a></li>
                    <li class="active">Acceso</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="container content-padding">
        <div class="row" id="divSuperior">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 register-form">
                <h3>Registrate para obtener una cuenta</h3>
                <span class="form-title">Operador Mayorista</span>
                <div class="clearfix"></div>
                <asp:Label runat="server" ID="lblError" ClientIDMode="Static" CssClass="errorRequired" style="margin-left:25%"></asp:Label>
                
                <div class="clearfix"></div>
                <label>Empresa</label>
                <asp:TextBox runat="server" ID="txtEmpresa" ClientIDMode="Static"></asp:TextBox>
                <div class="clearfix"></div>
                <label class="double-line">Contacto Empresa</label>
                <asp:TextBox runat="server" ID="txtContacto" ClientIDMode="Static"></asp:TextBox>
                <div class="clearfix"></div>
                <label>Dirección</label>
                <asp:TextBox runat="server" ID="txtDireccion" ClientIDMode="Static"></asp:TextBox>
                <div class="clearfix"></div>
                <label>Teléfono</label>
                <asp:TextBox runat="server" ID="txtTelefono" ClientIDMode="Static"></asp:TextBox>
                <div class="clearfix"></div>
                <label>Email</label>
                <asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static"></asp:TextBox>
                
                <div class="clearfix"></div>
                <label>Contraseña</label>
                <asp:TextBox runat="server" ID="txtPwd" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
                <div class="clearfix"></div>
                <label class="double-line">Confirmar Contraseña</label>
                <asp:TextBox runat="server" ID="txtPwd2" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
                <div class="clearfix"></div>
                <span class="tos">
                    <asp:CheckBox runat="server" ID="chkTerminos" ClientIDMode="Static" Checked="false" />
                    He leído y aceptado todos los <a href="terminos.aspx">términos y condiciones del servicio</a> y <a href="politica.aspx">política de privacidad</a>.</span>
            </div>

            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5 register-banner">
                <img src="/imgs/banner-registro.png" />
            </div>

            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 submit-registration">
                <a href="login.aspx">¿Está registrado?</a>
                <asp:Button CssClass="registro" Text="Registrarse" runat="server" ID="btnRegistrar" ClientIDMode="Static" OnClientClick="return validarFormulario();" OnClick="btnRegistrar_Click1" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

