<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="contacto.aspx.cs" Inherits="contacto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validarFormulario() {
            var isValid = true;
            var msg = "";

            if ($('#txtEmpresa').val() == "") {
                isValid = false;
                msg = "Debe ingresar el nombre"
            }
            else if ($('#txtContacto').val() == "") {
                isValid = false;
                msg = "Debe ingresar la empresa"
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
                    <li class="active">Contacto</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="container content-padding">
        <div class="row" id="divSuperior">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 register-form">
                <h3>Completa tus datos y a la brevedad te responderemos</h3>
                <div class="clearfix"></div>
                <asp:Label runat="server" ID="lblError" ClientIDMode="Static" CssClass="errorRequired" style="margin-left:25%"></asp:Label>
                <asp:Label runat="server" ID="lblOk" ClientIDMode="Static" CssClass="ok" style="margin-left:25%"></asp:Label>
                
                <div class="clearfix"></div>
                <label>* Nombre</label>
                <asp:TextBox runat="server" ID="txtEmpresa" ClientIDMode="Static"></asp:TextBox>
                <div class="clearfix"></div>
                <label class="double-line">* Empresa</label>
                <asp:TextBox runat="server" ID="txtContacto" ClientIDMode="Static"></asp:TextBox>
                <div class="clearfix"></div>
                <label>* Teléfono</label>
                <asp:TextBox runat="server" ID="txtTelefono" ClientIDMode="Static"></asp:TextBox>
                <div class="clearfix"></div>
                <label>* Email</label>
                <asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static"></asp:TextBox>
                <div class="clearfix"></div>
                <label>Asunto</label>
                <asp:TextBox runat="server" ID="txtAsunto" TextMode="MultiLine" Rows="10" ClientIDMode="Static" Height="200px"></asp:TextBox>
            </div>

            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5 register-banner">
                <img src="/imgs/banner-contacto.png" />
            </div>

            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 submit-registration">
                <asp:Button CssClass="registro" Text="Enviar" runat="server" ID="btnEnviar" ClientIDMode="Static" OnClientClick="return validarFormulario();" OnClick="btnEnviar_Click" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

