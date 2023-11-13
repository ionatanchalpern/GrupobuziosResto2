<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFront.master" AutoEventWireup="true" CodeFile="historial-traslados.aspx.cs" Inherits="historial_traslados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/js/carrito-traslados.js") %>"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="container-fluid location-breadcrumbs">
        <div class="container">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ol class="breadcrumb">
                    <li><a href="/traslados-paso1.aspx">Home</a></li>
                    <li class="active">Historial de reservas</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="container content-padding">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 order-table">
                <table>
                    <tr>
                        <th colspan="6">Reservas realizadas</th>
                        <th></th>
                    </tr>
                    <tr class="subheader">
                        <td>Fecha Alta</td>
                        <td>Estadia</td>
                        <td>File</td>
                        <td>Pasajero</td>
                        <td>Total</td>
                        <td style="min-width: 180px">Opciones</td>
                        <td></td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptProductos">
                        <ItemTemplate>
                            <tr id="fila_<%# Eval("IDPedidoTraslado") %>">
                                <td><%# Eval("FechaAlta") %></td>
                                <td><%# Eval("Estadia") %></td>
                                <td><%# Eval("File") %></td>
                                <td><%# Eval("Pasajero") %></td>
                                <td><%# Eval("Total") %></td>
                                <td>
                                    <a href="/pedidoPrintTraslado.aspx?Id=<%# Eval("IDPedidoTraslado") %>" target="_blank">Imprimir</a>&nbsp;
                                    <%# Eval("Links") %>
                                </td>
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

