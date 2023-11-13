<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="carrito-conf-traslados.aspx.cs" Inherits="carrito_conf_traslados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        input[type=submit] {
            background: #560D32;
            font-size: 26px;
            font-family: 'Cuprum', sans-serif;
            font-style: normal;
            font-weight: normal;
            text-transform: uppercase;
            color: white;
            border: 0;
            margin: 15px 0 0 0;
            padding: 5px 5px 5px 5px;
            position: relative;
        }
    </style>
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
            <div class="col-xs-12 col-sm-10 col-md-10 col-lg-10 ">
                <asp:Label runat="server" ID="lblError" CssClass="errorRequired"></asp:Label>
                <h3>Confirmación del traslado</h3>
                <hr class="separator" />

                <h2 style="background: #d0c8b5;color: #4b1534;font-weight: bold;font-size: 22px;font-family: 'Cuprum', sans-serif;    padding-top: 5px;padding-bottom: 5px;padding-left: 10px;height: 35px;">
                    Servicio:
                    &nbsp; <asp:Literal runat="server" ID="litServicio"></asp:Literal>
                </h2>

                <asp:Literal runat="server" ID="litDatos" />
                <div class='clearfix'></div>
                <asp:HyperLink runat="server" ID="lnkVolver">modificar</asp:HyperLink>
                <br />
                <asp:Button runat="server" ID="btnConfirmar" OnClick="btnConfirmar_Click" Text="Confirmar Traslado" />
                <asp:HiddenField runat="server" ID="hdnID" ClientIDMode="Static" Value="0" />
                <asp:HiddenField runat="server" ID="hdnMode" ClientIDMode="Static" Value="" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        function volver() {
            window.location.href = "traslados-paso1.aspx";
        }
    </script>
</asp:Content>

