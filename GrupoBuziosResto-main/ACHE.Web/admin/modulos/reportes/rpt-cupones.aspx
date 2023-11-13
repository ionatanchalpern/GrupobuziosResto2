<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="rpt-cupones.aspx.cs" Inherits="admin_modulos_reportes_rpt_cupones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: Reporte de Cupones
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/views/reportes/rpt-cupones.js") %>"></script>
    <link href="<%= ResolveClientUrl("~/admin/css/kendo.common.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveClientUrl("~/admin/css/kendo.silver.min.css") %>" rel="stylesheet" />
    <script type="text/javascript" src="<%= ResolveClientUrl("~/admin/js/kendo.all.min.js") %>"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container_12">

        <h4>Reporte de Cupones Validados</h4>

        <div class="form-grid">
            <br />
            <table cellpadding="3" cellspacing="2">
                 <tr>
                    <td>
                        <label class="fldTitle">Operador</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtOperador" CssClass="full" MaxLength="128" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Código</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtCodigo" CssClass="full" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Restaurant</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtRestaurant" CssClass="full" MaxLength="128" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </td>
                    
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Fecha Val. desde</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtEstadiaDesde" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0;margin-left: -5px;"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Fecha Val. hasta</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtEstadiaHasta" MaxLength="128" ClientIDMode="Static" Style="padding-left: 0;margin-left: -5px;"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="buttons bottom-round noboder" colspan="7">
                        <label class="fldTitle">&nbsp;</label>
                        <div class="fieldwrap">
                            <input type="button" id="btnBuscar" value="Buscar" class="submit-button-light" onclick="filter();" />
                            <input type="button" id="btnVerTodos" value="Ver todos" class="submit-button-light" onclick="verTodos();" />
                            <button class="submit-button-blue" type="button" id="btnExportar" onclick="exportar();">Exportar a Excel</button>
                            <img src="/admin/images/ajax-loader/ajax-loader-bar-1.gif" id="imgLoading" style="display: none" />
                            <a href="" id="lnkDownload" download="ReporteCupones" style="display: none">Descargar</a>
                        </div>
                    </td>
                </tr>
            </table>
            <div runat="server" id="divErrores" class="notification-wrap failure" visible="false" style="width: 740px; margin-left: 0px">
                <span class="icon-failure">ERROR</span>
                <p id="msgError">
                    <asp:Literal runat="server" ID="litError"></asp:Literal>
                </p>
            </div>
        </div>

        <br />
        <div id="grid"></div>
    </div>
</asp:Content>



