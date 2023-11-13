<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="serviciose.aspx.cs" Inherits="admin_modulos_custom_serviciose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: <asp:Literal runat="server" ID="litModo" /> de Servicio
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">

   <script type="text/javascript">
       $(document).ready(function () {
           $('.numeric').numeric();
       });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container_12" style="margin-left: 20px; width: 750px">
        <br />
        <h4>
            <asp:Literal runat="server" ID="litModo2" /> de Servicio
        </h4>
        <asp:HiddenField runat="server" ID="hdnIDServicio" ClientIDMode="Static" value="0"/>

        <br />
        <div id="divEdicion" class="form-grid">
            <asp:Panel runat="server" ID="pnlError" Visible="false" class="notification-wrap failure" Style="margin-left: -5px">
                <span class="icon-failure">ERROR</span>
                <asp:Literal runat="server" ID="litError" />
            </asp:Panel>
            <asp:CustomValidator ID="valCustom" runat="server" Display="None" OnServerValidate="ServerValidate" />
            <div class="form leftLabel">
                <ul style="list-style-type: none">
                    <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>Proveedor</label>
                        <div class="fieldwrap">
                           <asp:DropDownList runat="server" ID="cmbProveedor" CssClass="medium" />
                           <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvProveedor" ControlToValidate="cmbProveedor"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired" Display="Dynamic" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>Tipo</label>
                        <div class="fieldwrap">
                            <asp:DropDownList runat="server"  AutoPostBack="true" OnSelectedIndexChanged="CargarSubTipos" ClientIDMode="Static" ID="ddlTipo" CssClass="medium">
                                <asp:ListItem Text="Round Trip" Value="R"></asp:ListItem>
                                <asp:ListItem Text="One Way" Value="O"></asp:ListItem>
                            </asp:DropDownList>
                           
                        </div>
                    </li>
                         <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>SubTipo</label>
                        <div class="fieldwrap">
                            <asp:DropDownList runat="server"  ID="ddlSubTipos"  ClientIDMode="Static" CssClass="medium"/>
                            <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvSubtipos" ControlToValidate="ddlSubTipos"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired" Display="Dynamic" />
                           
                        </div>

                    </li>
                    <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>Nombre</label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control full" MaxLength="200" />
                           <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvNombre" ControlToValidate="txtNombre"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired" Display="Dynamic" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Activo</label>
                        <div class="fieldwrap">
                            <asp:CheckBox ClientIDMode="Static" runat="server" ID="chkActivo" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Servicio Especial</label>
                        <div class="fieldwrap">
                            <asp:CheckBox ClientIDMode="Static" runat="server" ID="chkServicioEspecial" />
                        </div>
                    </li>
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
