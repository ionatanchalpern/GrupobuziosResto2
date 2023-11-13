<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="recuperar.aspx.cs" Inherits="recuperar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="default.aspx">Home</a></li>
                    <li class="active">Recuperar contraseña</li>
                </ol>
            </div>
        </div>
    </div>
    <div class="container content-padding">
        <div class="row" id="divSuperior">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 register-form" style="border:0">
                
                <h4 style="margin-left: 25px">Ingrese el email con el que se registró para poder recuperar su contraseña</h4>

                <asp:Label runat="server" ID="lblError" CssClass="errorRequired2" ClientIDMode="Static"></asp:Label>
                <asp:Label runat="server" ID="lblOk" CssClass="ok2" ClientIDMode="Static" Visible="false"></asp:Label>
                
                <div class="clearfix"></div>
                <label>Email</label>
                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                
                <br />
                <asp:RequiredFieldValidator runat="server" ID="rqvEmail" ControlToValidate="txtEmail"
                    CssClass="errorRequired2" ErrorMessage="Debe completar este campo"></asp:RequiredFieldValidator>
                <br />
                <asp:RegularExpressionValidator ID="rqvEmailValido" runat="server" ErrorMessage="Debe ingresar una direccion de email valida."
                                        Display="Dynamic" CssClass="errorRequired2" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                <br />
            </div>
            
            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5 register-banner">
                
            </div>

            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 submit-registration">
                <asp:Button runat="server" CssClass="registro" Text="Recuperar" ID="btnEnviar" OnClick="btnEnviar_Click" />
            </div>
        </div>
    </div>    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>