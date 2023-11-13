<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="productose.aspx.cs" Inherits="admin_modulos_custom_productose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: <asp:Literal runat="server" ID="litModo" /> de Producto
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtPrecio, #txtPrecioProxTemp, #txtPrecioOperador, #txtPrecioOperadorProxTemp').numeric();
            $('.precios').hide();
        });

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container_12" style="margin-left: 20px; width: 750px">
        <br />
        <h4><asp:Literal runat="server" ID="litModo2" /> de Producto</h4>
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
                        <label class="fldTitle"><abbr class="require">*</abbr>Tipo</label>
                        <div class="fieldwrap">
                            <asp:DropDownList ClientIDMode="Static" runat="server" ID="cmbTipos" Enabled="false">
                                <asp:ListItem Text="Turista" Value="T"></asp:ListItem>
                                <asp:ListItem Text="Premium" Value="P"></asp:ListItem>
                                <asp:ListItem Text="Menores" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Playa" Value="B"></asp:ListItem>
                                <asp:ListItem Text="Clasico" Value="C"></asp:ListItem>
                                <asp:ListItem Text="Buffet Libre" Value="L"></asp:ListItem>

							</asp:DropDownList>
                            <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="RequiredFieldValidator4" ControlToValidate="cmbTipos"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>Entrada</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" ID="txtEntrada" TextMode="MultiLine" Rows="5" Width="500px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvCodigo" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtEntrada"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>Plato Principal</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" ID="txtPlatoPrincipal" TextMode="MultiLine" Rows="5" Width="500px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtPlatoPrincipal"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>Postre</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" ID="txtPostre" TextMode="MultiLine" Rows="5" Width="500px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtPostre"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle"><abbr class="require">*</abbr>¿Incluye Bebida?</label>
                        <div class="fieldwrap">
                            <asp:CheckBox ClientIDMode="Static" runat="server" ID="chkBebida" Checked="true" />
                        </div>
                    </li>
                  
                    <li class="precios">
                        <label class="fldTitle"><abbr class="require">*</abbr>Neto Restó Temp Actual</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" MaxLength="4" ID="txtPrecio"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtPrecio"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li class="precios">
                        <label class="fldTitle"><abbr class="require">*</abbr>Neto Restó Prox Temp</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" MaxLength="4" ID="txtPrecioProxTemp"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtPrecioProxTemp"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li class="precios">
                        <label class="fldTitle"><abbr class="require">*</abbr>Neto Operador Temp Actual</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" MaxLength="4" ID="txtPrecioOperador"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtPrecioOperador"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li class="precios">
                        <label class="fldTitle"><abbr class="require">*</abbr>Neto Operador Prox Temp</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" MaxLength="4" ID="txtPrecioOperadorProxTemp"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtPrecioOperadorProxTemp"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li style="display:none">
                        <label class="fldTitle"><abbr class="require">*</abbr>Activo</label>
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
