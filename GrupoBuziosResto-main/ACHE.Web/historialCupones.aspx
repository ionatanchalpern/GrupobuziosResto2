<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="historialCupones.aspx.cs" Inherits="historialCupones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="/">Home</a></li>
                    <li class="active">História validado cupons</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="container content-padding">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 order-table">
                <table>
                   
                    <tr class="subheader">
                        <td>Código</td>
                        <td>Data Validation</td>
                        <td>Menú</td>
                        <%--<td>Operador</td>--%>
                        <td>Passageiro</td>
                        <td>Neto Restó</td>
                        <td></td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptProductos">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Codigo") %></td>
                                <td><%# Eval("FechaValidacion") %></td>
                                <td><%# Eval("Tipo") %></td>
                                <%--<td><%# Eval("Operador") %></td>--%>
                                <td><%# Eval("Pasajero") %></td>
                                <td><%# Eval("Total") %></td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>

