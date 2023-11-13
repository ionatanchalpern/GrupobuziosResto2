<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="cambiarPwd.aspx.cs" Inherits="cambiarPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validarPwd() {
            var isValid = true;
            var msg = "";
            var msg1 = "<%= CurrentUser.TipoUsuario =="O" ? "Debe ingresar la contraseña actual": "Você deve digitar a senha atual" %>";
            var msg2 = "<%= CurrentUser.TipoUsuario =="O" ? "Debe ingresar la nueva contraseña": "Você deve digitar a nova senha" %>";
            var msg3 = "<%= CurrentUser.TipoUsuario =="O" ? "Debe confirmar la nueva contraseña": "Você deve confirmar a nova senha" %>";
            var msg4 = "<%= CurrentUser.TipoUsuario =="O" ? "La nueva contraseña debe coincidir": "A nova senha deve corresponder" %>";
            var msg5 = "<%= CurrentUser.TipoUsuario =="O" ? "La nueva contraseña no puede ser igual a la actual": "A nova senha não pode ser igual à corrente" %>";
            
            if ($('#txtPwdOld').val() == "") {
                isValid = false;
                msg = msg1;
            }
            else if ($('#txtPwdNew').val() == "") {
                isValid = false;
                msg = msg2;
            }
            else if ($('#txtPwdNew2').val() == "") {
                isValid = false;
                msg = msg3;
            }
            else if ($('#txtPwdNew').val() != $('#txtPwdNew2').val())
            {
                isValid = false;
                msg = msg4;
            }
            else if ($('#txtPwdOld').val() == $('#txtPwdNew').val()) {
                isValid = false;
                msg = msg5;
            }

            if (isValid) {
                $('#lblError').hide();
                return true;
            }
            else {
                $('#lblError').show();
                $('#lblError').html(msg + "<br /><br />");
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
                    <li><a href="misDatos.aspx"><%= CurrentUser.TipoUsuario =="O" ? "Mis Datos": "Meus dados" %></a></li>
                    <li class="active"><%= CurrentUser.TipoUsuario =="O" ? "Modificar Contraseña": "Alterar a senha" %></li>
                </ol>
            </div>
        </div>
    </div>
    <div class="container content-padding">
        <div class="row" id="divSuperior">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 register-form" style="border:0">
                
                <asp:Label runat="server" ID="lblError" CssClass="errorRequired2" ClientIDMode="Static"></asp:Label>
                <asp:Label runat="server" ID="lblOk" CssClass="ok2" ClientIDMode="Static" Visible="false"></asp:Label>

                <div class="clearfix"></div>
                <label class="double-line"><%= CurrentUser.TipoUsuario =="O" ? "Contraseña actual": "Senha atual" %></label>
                <asp:TextBox runat="server" ID="txtPwdOld" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
                
                <label class="double-line"><%= CurrentUser.TipoUsuario =="O" ? "Contraseña nueva": "Nova senha" %></label>
                <asp:TextBox runat="server" ID="txtPwdNew" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
                
                <label class="double-line"><%= CurrentUser.TipoUsuario =="O" ? "Confirmar contraseña": "Confirme sua senha" %></label>
                <asp:TextBox runat="server" ID="txtPwdNew2" ClientIDMode="Static" TextMode="Password"></asp:TextBox>
            </div>
            
            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5 register-banner">
                
            </div>

            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 submit-registration">
                <asp:Button runat="server" CssClass="registro" Text="" ID="btnCambiarPwd" OnClientClick="return validarPwd();" OnClick="btnCambiarPwd_Click" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

