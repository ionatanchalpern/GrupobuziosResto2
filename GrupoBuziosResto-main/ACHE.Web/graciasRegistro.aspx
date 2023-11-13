<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="graciasRegistro.aspx.cs" Inherits="graciasRegistro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="/">Home</a></li>
                    <li class="active">Fin de registro</li>
                </ol>
            </div>
        </div>
    </div>
    
    <div class="container content-padding">
        <div class="row">
            <div class="col-xs-12 col-sm-10 col-md-10 col-lg-10 col-sm-offset-1 col-md-offset-1 col-lg-offset-1 success-buy">
                
                <h3>Muchas gracias por registrarte!</h3>
                <p>Su solicitud de registro ha sido recibida, la misma se encuentra pendiente de aprobación.</p>
                <p>Una vez aprobada, te avisaremos por mail.</p>
                <div class="clearfix"></div>
                <img class="logo-fade" src="imgs/logo-fade.png" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

