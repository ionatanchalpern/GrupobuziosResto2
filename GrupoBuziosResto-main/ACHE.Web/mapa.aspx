<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="mapa.aspx.cs" Inherits="mapa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="default.aspx">Home</a></li>
                    <li class="active">Mapa</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="container content-padding">
        <h3><a href="imgs/mapa-big.jpg" target="_blank" style="font-family: 'Cuprum', sans-serif;font-weight: bold;color: #4b1534;font-size: 26px;position: relative;">Descargar</a></h3>
        <br />
        <img src="imgs/mapa.jpg" />
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>

