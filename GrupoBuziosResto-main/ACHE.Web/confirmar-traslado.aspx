<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="confirmar-traslado.aspx.cs" Inherits="confirmar_traslado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="/">Home</a></li>
                    <li class="active">Pedido traslado finalizado</li>
                </ol>
            </div>
        </div>
    </div>
    <div class="container content-padding">
        <div class="row">
            <div class="col-xs-12 col-sm-10 col-md-10 col-lg-10 col-sm-offset-1 col-md-offset-1 col-lg-offset-1 success-buy">
                <asp:Label runat="server" ID="lblError" CssClass="errorRequired"></asp:Label>
                <h3 runat="server" id="hConfirmada">La reserva #<asp:Literal runat="server" ID="litId" /> ha sido <span class="salmon">CONFIRMADA</span> con éxito!</h3>
                <div class="clearfix"></div>
                <img class="logo-fade" src="/imgs/logo-traslados-fade.png" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

