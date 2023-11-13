<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="carrito-fin.aspx.cs" Inherits="carrito_fin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="/">Home</a></li>
                    <li class="active">Compra finalizada</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="container content-padding">
        <div class="row">
            <div class="col-xs-12 col-sm-10 col-md-10 col-lg-10 col-sm-offset-1 col-md-offset-1 col-lg-offset-1 success-buy">
                <asp:Label runat="server" ID="lblError" CssClass="errorRequired" Visible="false"></asp:Label>
                <asp:Label runat="server" ID="lblOk" CssClass="ok" Visible="false"></asp:Label>

                <h3>Su proceso de compra ha <span class="salmon">FINALIZADO</span> con éxito!</h3>
                <p>Haga click aquí en el botón de IMPRIMIR para imprimir sus cupones.</p>
                <asp:LinkButton runat="server" ID="btnImprimirCupones" OnClick="btnImprimirCupones_Click" target="_blank">
                    <span class="link-label">Imprimir</span> <img src="/imgs/print-icon.png" />
                </asp:LinkButton>
                <hr class="separator" />
                <p>Haga click en el botón de ENVIAR para recibir sus cupones en su mail.</p>
                <asp:LinkButton runat="server" ID="btnEnviarMail" OnClick="btnEnviarMail_Click">
                    <span class="link-label">Enviar</span> <img src="/imgs/mail-icon.png" />
                </asp:LinkButton>
                <div class="clearfix"></div>
                <img class="logo-fade" src="imgs/logo-fade.png" />
                <hr class="separator" />
                <a href="/changesession.aspx?Tipo=traslados">
                    <img src="imgs/banner-traslados.png" style="max-width:100%" />
                </a>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

