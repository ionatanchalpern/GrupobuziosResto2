<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="misDatos.aspx.cs" Inherits="misDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="default.aspx">Home</a></li>
                    <li class="active">Mis datos</li>
                </ol>
            </div>
        </div>
    </div>
    <div class="container content-padding">
        <div class="row" id="divSuperior">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 register-form" style="border:0">
                <label>Email</label>
                <asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                <br />
                <label>* Empresa</label>
                <asp:TextBox runat="server" ID="txtEmpresa" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rqvEmpresa" ControlToValidate="txtEmpresa" CssClass="errorRequired2" 
                    ErrorMessage="Debe completar este campo" Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <label class="double-line">* Nombre contacto</label>
                <asp:TextBox runat="server" ID="txtContacto" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rqvContacto" ControlToValidate="txtContacto" CssClass="errorRequired2" 
                    ErrorMessage="Debe completar este campo" Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <label>Dirección</label>
                <asp:TextBox runat="server" ID="txtDireccion" ClientIDMode="Static"></asp:TextBox>
                <br />
                <label>Telefono</label>
                <asp:TextBox runat="server" ID="txtTelefono" ClientIDMode="Static"></asp:TextBox>
                <div class="clearfix"></div>
                <span class="tos">
                    <a href="cambiarPwd.aspx">Modificar contraseña</a>
                </span>
            </div>
            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5 register-banner">
                
            </div>

            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 submit-registration">
                <asp:Button runat="server" CssClass="registro" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click"/>
            </div>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

