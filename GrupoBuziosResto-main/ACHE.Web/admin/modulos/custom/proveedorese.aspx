<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="proveedorese.aspx.cs" Inherits="admin_modulos_custom_proveedorese" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: <asp:Literal runat="server" ID="litModo" /> de Proveedor
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container_12" style="margin-left: 20px; width: 750px">
        <br />
        <h4>
            <asp:Literal runat="server" ID="litModo2" /> de Proveedor
        </h4>
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
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Nombre</label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" MaxLength="50" />
                           <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvNombre" ControlToValidate="txtNombre"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired" Display="Dynamic" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Email</label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" MaxLength="128" />
                           <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvEmail" ControlToValidate="txtEmail"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="rqvEmailValido" runat="server" ErrorMessage="Debe ingresar una dirección de email válida."
                            Display="Dynamic" CssClass="errorRequired" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Teléfono</label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" MaxLength="50" />
                           <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvTelefono" ControlToValidate="txtTelefono"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired" Display="Dynamic" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            Logo<br />
                            (Tamaño recomendado: 50 × 50)</label>
                        <div class="fieldwrap">
                            <asp:FileUpload ClientIDMode="Static" runat="server" ID="flpLogo" />
                            <br />
                            <br />
                            Logo actual:
                            <asp:Image ClientIDMode="Static" runat="server" ID="imgLogo" Width="120px" ImageUrl="/files/logos/no-photo.jpg" />
                            <br />
                            <br />
                            <asp:LinkButton runat="server" Visible="false" ID="lnkEliminarLogo" Text="Eliminar" OnClick="lnkEliminarLogo_Click" CausesValidation="false" />
                            <br />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Observaciones</label>
                        <div class="fieldwrap">
                           <asp:TextBox runat="server" ID="txtObservaciones" Rows="7" Columns="45" CssClass="form-control" TextMode="MultiLine" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Activo</label>
                        <div class="fieldwrap">
                            <asp:CheckBox ClientIDMode="Static" runat="server" ID="chkActivo" Checked="true" />
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
