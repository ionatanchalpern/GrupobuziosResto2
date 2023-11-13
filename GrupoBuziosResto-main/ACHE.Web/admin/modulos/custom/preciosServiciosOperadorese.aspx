<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="preciosServiciosOperadorese.aspx.cs" Inherits="admin_modulos_custom_preciosServiciosOperadorese" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
         Admin ::
    <asp:Literal runat="server" ID="litTitulo" />
    de Precios por servicios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
           <link href="<%= ResolveClientUrl("~/admin/css/kendo.common.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveClientUrl("~/admin/css/kendo.silver.min.css") %>" rel="stylesheet" />
    <script type="text/javascript" src="<%= ResolveClientUrl("~/admin/js/kendo.all.min.js") %>"></script>

     <script type="text/javascript">

         $(document).ready(function () {
             $("#txtFechaDesde").kendoDatePicker({ format: 'dd/MM/yyyy' });
             $("#txtFechaHasta").kendoDatePicker({ format: 'dd/MM/yyyy' });
             $('.numeric').numeric();
         });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="container_12" style="margin-left: 20px; width: 750px">
        <br />
        <h4>
            <asp:Literal runat="server" ID="litTitulo2" />
            de precios por servicio por Operador
        </h4>
        <br />
        <div id="divEdicion" class="form-grid">
            <asp:Panel runat="server" ID="pnlError" Visible="false" class="notification-wrap failure" Style="margin-left: -5px">
                <span class="icon-failure">ERROR</span>
                <asp:Literal runat="server" ID="litError"></asp:Literal>
            </asp:Panel>
            <asp:CustomValidator ID="valCustom" runat="server" Display="None" OnServerValidate="ServerValidate"></asp:CustomValidator>
            <div class="form leftLabel">
                <ul style="list-style-type: none">

                   <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Operador</label>
                        <div class="fieldwrap">
                            <asp:DropDownList ClientIDMode="Static" runat="server" ID="cmbOperador"  >
                              
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="RequiredFieldValidator5" ControlToValidate="cmbOperador" 
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                   <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Proveedor</label>
                        <div class="fieldwrap">
                            <asp:DropDownList ClientIDMode="Static" runat="server" ID="cmbProveedor"  AutoPostBack="true" OnSelectedIndexChanged="cargarServicios">
                              
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="RequiredFieldValidator2" ControlToValidate="cmbProveedor" 
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Servicio</label>
                        <div class="fieldwrap">
                            <asp:DropDownList ClientIDMode="Static" runat="server" ID="ddlServicios">
                              
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlServicios"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                
                     <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>Fecha desde</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small numeric" ClientIDMode="Static" runat="server" MaxLength="4" ID="txtFechaDesde"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtFechaDesde"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                       <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>Fecha hasta</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small numeric" ClientIDMode="Static" runat="server" MaxLength="4" ID="txtFechaHasta"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtFechaHasta"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Precio Regular <br /><small>Ingrese 0 para ocultarlo</small></label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtPrecioRegular" CssClass="form-control numeric" />
                           <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvPrecioRegular" ControlToValidate="txtPrecioRegular"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired" Display="Dynamic" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Observaciones Regular</label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtObservacionesRegular" Rows="7" Width="300px" CssClass="form-control" TextMode="MultiLine" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>Precio Privado <br /><small>Ingrese 0 para ocultarlo</small></label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtPrecioPrivado" CssClass="form-control numeric" />
                           <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvPrecioPrivado" ControlToValidate="txtPrecioPrivado"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired" Display="Dynamic" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Observaciones Privado</label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtObservacionesPrivado" Rows="7" Width="300px" CssClass="form-control" TextMode="MultiLine" />
                        </div>
                    </li>

<%--                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Importe Neto<br /><small>Ingrese 0 para ocultarlo</small></label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtPrecioNeto" CssClass="form-control numeric" />
                           <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvPrecioRegular" ControlToValidate="txtPrecioNeto"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired" Display="Dynamic" />
                        </div>
                    </li>--%>
<%--                    <li>
                        <label class="fldTitle">Observaciones Neto</label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtObservacionesNeto" Rows="7" Width="300px" CssClass="form-control" TextMode="MultiLine" />
                        </div>
                    </li>--%>
                    
                    <li class="buttons bottom-round noboder">
                        <div class="fieldwrap">
                            <br />
                            <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" CssClass="submit-button" OnClick="btnAceptar_OnClick" />
                            <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="submit-button" OnClick="btnCancelar_OnClick" CausesValidation="false" />
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>

