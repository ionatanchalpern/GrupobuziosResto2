<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="validar.aspx.cs" Inherits="validar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="default.aspx">Home</a></li>
                    <li class="active">Cupons de validação</li>
                </ol>
            </div>
        </div>
    </div>
    <div class="container content-padding">
        <div class="row" id="divSuperior">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 register-form" style="border:0">

                <h4 style="margin-left: 25px">Digite o código do cupom para validar</h4>

                <asp:Label runat="server" ID="lblError" CssClass="errorRequired2" Visible="false"></asp:Label>
                <asp:Label runat="server" ID="lblOk" CssClass="ok2" Visible="false"></asp:Label>
            
                 <div class="clearfix"></div>
                <label>Código</label>
                <asp:TextBox runat="server" ID="txtCodigo" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rqvCodigo" ControlToValidate="txtCodigo" ErrorMessage="<br />Debe completar este campo" CssClass="errorRequired2" Display="Dynamic"></asp:RequiredFieldValidator>


            </div>
            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-5 register-banner">
                
            </div>

            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-7 submit-registration">
                <asp:Button runat="server" CssClass="registro" Text="Validar" ID="btnValidar" OnClick="btnValidar_Click" />
            </div>
        </div>
     </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>

