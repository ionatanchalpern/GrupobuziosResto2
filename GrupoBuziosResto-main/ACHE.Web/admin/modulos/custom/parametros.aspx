<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="parametros.aspx.cs" Inherits="admin_modulos_custom_parametros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: Parámetros
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/redactor.min.js") %>"></script>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/admin/css/redactor.css") %>" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtBeneficios, #txtTraslados').redactor({
                buttons: ['bold', 'italic', 'unorderedlist']
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container_12" style="margin-left: 20px; width: 750px">
        <br />
        <h4>Edición de Parámetros</h4>
        <br />
        <div id="divEdicion" class="form-grid">
            <asp:Panel runat="server" ID="pnlError" Visible="false" class="notification-wrap failure" Style="margin-left: -5px">
                <span class="icon-failure">ERROR</span>
                <asp:Literal runat="server" ID="litError" />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlOk" Visible="false" class="notification-wrap success" Style="margin-left: -5px">
                <span class="icon-success">OK</span>
                Los datos han sido actualizados correctamente.
            </asp:Panel>
            <asp:CustomValidator ID="valCustom" runat="server" Display="None" OnServerValidate="ServerValidate" />
            <div class="form leftLabel">
                <ul style="list-style-type: none">
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Texto Beneficios</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" TextMode="MultiLine" Rows="5" Width="300" ID="txtBeneficios" ClientIDMode="Static" />
                            <br />
                            <asp:RequiredFieldValidator ID="rqvBeneficios" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtBeneficios" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Texto Traslados</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" TextMode="MultiLine" Rows="5" Width="300" ID="txtTraslados" ClientIDMode="Static" />
                            <br />
                            <asp:RequiredFieldValidator ID="rqvTraslados" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"
                                runat="server" ControlToValidate="txtTraslados" />
                        </div>
                    </li>
                    <li>
                        <label class="fldTitle">
                            <abbr class="require">*</abbr>Adicional Horario Nocturno</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtAdicionalNoche" ClientIDMode="Static" CssClass="numeric" MaxLength="10" />
                            <br />
                            <asp:RequiredFieldValidator ID="rqvAdicionalNoche" ClientIDMode="Static" ErrorMessage="Este campo es obligatorio."
                                runat="server" ControlToValidate="txtAdicionalNoche" ForeColor="Red" />
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
