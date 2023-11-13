<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="menuese.aspx.cs" Inherits="admin_modulos_custom_menuese" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: <asp:Literal runat="server" ID="litModo" /> de Menú
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container_12" style="margin-left: 20px; width: 750px">
        <br />
        <h4><asp:Literal runat="server" ID="litModo2" /> de Menú</h4>
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
                            <abbr class="require">*</abbr>Restaurant</label>
                        <div class="fieldwrap">
                            <asp:DropDownList ClientIDMode="Static" runat="server" ID="cmbRestaurant"></asp:DropDownList>
                           <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvRestaurant" ControlToValidate="cmbRestaurant"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Tipo Menu</label>
                        <div class="fieldwrap">
                            <asp:DropDownList ClientIDMode="Static" runat="server" ID="cmbTipos">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Turista" Value="T"></asp:ListItem>
                                <asp:ListItem Text="Premium" Value="P"></asp:ListItem>
                                <asp:ListItem Text="Menores" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Playa" Value="B"></asp:ListItem>
                                <asp:ListItem Text="Clasico" Value="C"></asp:ListItem>
                                <asp:ListItem Text="Buffet Libre" Value="L"></asp:ListItem>

                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ClientIDMode="Static" runat="server" ID="rqvTipo" ControlToValidate="cmbTipos"
                                ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Entrada</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" ID="txtEntrada" TextMode="MultiLine" Rows="5" Width="500px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvCodigo" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtEntrada"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Plato Principal</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" ID="txtPlatoPrincipal" TextMode="MultiLine" Rows="5" Width="500px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtPlatoPrincipal"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Postre</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" ID="txtPostre" TextMode="MultiLine" Rows="5" Width="500px"></asp:TextBox>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>¿Incluye Bebida?</label>
                        <div class="fieldwrap">
                            <asp:CheckBox ClientIDMode="Static" runat="server" ID="chkBebida" Checked="true" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            Imagen<br />
                            (Tamaño recomendado: 50 × 50)</label>
                        <div class="fieldwrap">
                            <asp:FileUpload ClientIDMode="Static" runat="server" ID="flpImagen" />
                            <br />
                            <br />
                            Imagen actual:
                            <asp:Image ClientIDMode="Static" runat="server" ID="img1" Width="120px" />
                            <br />
                            <br />
                            <asp:LinkButton runat="server" Visible="false" ID="lnkEliminarImagen" Text="Eliminar" OnClick="btnEliminarImagen_OnClick" CausesValidation="false"></asp:LinkButton>
                            <br />
                        </div>
                    </li>
                     <li>
                        <label class="fldTitle">Imagen menu<br /></label>
                        <div class="fieldwrap">
                            <asp:FileUpload ClientIDMode="Static" runat="server" ID="flpMenu" />
                            <br />
                            <br />
                            Logo actual:
                            <asp:Image ClientIDMode="Static" runat="server" ID="imgMenu" Width="120px" />
                            <br />
                            <br />
                            <asp:LinkButton runat="server" Visible="false" ID="lnkEliminarMenu" Text="Eliminar" OnClick="btnEliminarMenu_OnClick" CausesValidation="false"></asp:LinkButton>
                            <br />
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
