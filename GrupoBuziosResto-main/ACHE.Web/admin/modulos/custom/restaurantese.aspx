<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="restaurantese.aspx.cs" Inherits="admin_modulos_custom_restaurantese" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: <asp:Literal runat="server" ID="litModo" /> de Restaurante
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container_12" style="margin-left: 20px; width: 750px">
        <br />
        <h4><asp:Literal runat="server" ID="litModo2" /> de Restaurante</h4>
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
                            <abbr class="require">*</abbr>Nombre</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" MaxLength="50" ID="txtNombre"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvCodigo" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Email</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="medium" ClientIDMode="Static" runat="server" MaxLength="50" ID="txtEmail"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvEmail" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="rqvEmailValido" runat="server" ErrorMessage="Debe ingresar una direccion de email valida."
                                Display="Dynamic" CssClass="errorRequired" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Contraseña</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" MaxLength="50" ID="txtPwd"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvPwd" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtPwd"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <%--<li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Confirmar contraseña</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" CssClass="small" TextMode="Password" ClientIDMode="Static" ID="txtPwd2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvPwd2" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtPwd2"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator runat="server" ID="cpvPwd" ClientIDMode="Static" ErrorMessage="Las contraseñas deben coincidir" CssClass="errorRequired" 
                                ControlToValidate="txtPwd" ControlToCompare="txtPwd2"></asp:CompareValidator>
                        </div>
                    </li>--%>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Direccion</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="medium" ClientIDMode="Static" runat="server" MaxLength="50" ID="txtDireccion"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvDireccion" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtDireccion"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Ciudad</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="medium" ClientIDMode="Static" runat="server" MaxLength="50" ID="txtCiudad"></asp:TextBox>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Horario Atención</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="medium" ClientIDMode="Static" runat="server" MaxLength="50" ID="txtAtencion"></asp:TextBox>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Telefono</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" MaxLength="50" ID="txtTelefono"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvTelefono" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtTelefono"></asp:RequiredFieldValidator>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Activo</label>
                        <div class="fieldwrap">
                            <asp:CheckBox ClientIDMode="Static" runat="server" ID="chkActivo" Checked="true" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Observaciones</label>
                        <div class="fieldwrap">
                            <asp:TextBox CssClass="small" ClientIDMode="Static" runat="server" MaxLength="50" ID="txtObservaciones" TextMode="MultiLine" Rows="5" Width="500px" ></asp:TextBox>
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Logo<br /></label>
                        <div class="fieldwrap">
                            <asp:FileUpload ClientIDMode="Static" runat="server" ID="flpLogo" />
                            <br />
                            <br />
                            Logo actual:
                            <asp:Image ClientIDMode="Static" runat="server" ID="imgLogo" Width="120px" />
                            <br />
                            <br />
                            <asp:LinkButton runat="server" Visible="false" ID="lnkEliminarLogo" Text="Eliminar" OnClick="btnEliminarLogo_OnClick" CausesValidation="false"></asp:LinkButton>
                            <br />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Imagenes<br />(Tamaño recomendado: 500 × 500)</label>
                        <div class="fieldwrap">
                            Imagen 1:
                            <asp:FileUpload ClientIDMode="Static" runat="server" ID="flpImagen1" />
                            <br />
                            <br />
                            Imagen actual:
                            <asp:Image ClientIDMode="Static" runat="server" ID="img1" Width="120px" />
                            <br />
                            <br />
                            <asp:LinkButton runat="server" Visible="false" ID="lnkEliminar1" Text="Eliminar" OnClick="btnEliminar1_OnClick" CausesValidation="false"></asp:LinkButton>
                            <br />
                            Imagen 2:
                            <asp:FileUpload ClientIDMode="Static" runat="server" ID="flpImagen2" />
                            <br />
                            <br />
                            Imagen actual:
                            <asp:Image ClientIDMode="Static" runat="server" ID="img2" Width="120px" />
                            <br />
                            <br />
                            <asp:LinkButton runat="server" Visible="false" ID="lnkEliminar2" Text="Eliminar" OnClick="btnEliminar1_OnClick" CausesValidation="false"></asp:LinkButton>
                            <br />
                            Imagen 3:
                            <asp:FileUpload ClientIDMode="Static" runat="server" ID="flpImagen3" />
                            <br />
                            <br />
                            Imagen actual:
                            <asp:Image ClientIDMode="Static" runat="server" ID="img3" Width="120px" />
                            <br />
                            <br />
                            <asp:LinkButton runat="server" Visible="false" ID="lnkEliminar3" Text="Eliminar" OnClick="btnEliminar2_OnClick" CausesValidation="false"></asp:LinkButton>
                            <br />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">Imagen mapa<br /></label>
                        <div class="fieldwrap">
                            <asp:FileUpload ClientIDMode="Static" runat="server" ID="flpMapa" />
                            <br />
                            <br />
                            Mapa actual:
                            <asp:Image ClientIDMode="Static" runat="server" ID="imgMapa" Width="120px" />
                            <br />
                            <br />
                            <asp:LinkButton runat="server" Visible="false" ID="lnkEliminarMapa" Text="Eliminar" OnClick="btnEliminarMapa_OnClick" CausesValidation="false"></asp:LinkButton>
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
