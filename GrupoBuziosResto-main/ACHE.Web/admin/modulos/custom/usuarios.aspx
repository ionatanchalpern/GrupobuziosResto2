<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" Inherits="admin_modulos_custom_usuarios" CodeFile="usuarios.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Admin :: Operadores
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/views/custom/usuarios.js") %>"></script>
    <link rel="stylesheet" href="<%= ResolveClientUrl("~/admin/css/bootstrap.css") %>" type="text/css" media="screen" />
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/bootstrap-transition.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/bootstrap-modal.js") %>"></script>
        <script type="text/javascript" src="<%= ResolveUrl("~/admin/js/views/custom/usuarios.js") %>"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField runat="server" ID="hdnIDUsuario" ClientIDMode="Static" value="0"/>
    <div class="container_12">

        <h4>Administración de operadores</h4>

        <div class="form-grid">
            <br />
            <table cellpadding="3" cellspacing="2">
                <tr>
                    <td>
                        <label class="fldTitle">Empresa</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtEmpresa" CssClass="full" MaxLength="128" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Nombre contacto</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtContacto" CssClass="full" MaxLength="128" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td>
                        <label class="fldTitle">Email</label>
                        <div class="fieldwrap">
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="full" MaxLength="128" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20px">&nbsp;</td>
                    <td class="buttons bottom-round noboder">
                        <label class="fldTitle">&nbsp;</label>
                        <div class="fieldwrap">
                            <input type="button" id="btnBuscar" value="Buscar" class="submit-button-light" onclick="filter();" />
                            <input type="button" id="btnVerTodos" value="Ver todos" class="submit-button-light" onclick="verTodos();" />
                            <button class="submit-button-blue" type="button" id="btnExportar" onclick="exportar();">Exportar a Excel</button>
                            <img src="/admin/images/ajax-loader/ajax-loader-bar-1.gif" id="imgLoading" style="display: none" />
                            <a href="" id="lnkDownload" download="Usuarios" style="display: none">Descargar</a>
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

    <div id="modalUsuario" class="modal hide fade" tabindex="-1" role="dialog">
        <div class="modal-header" style="background-color: #e8e8e8">
            <button type="button" class="close" data-dismiss="modal" id="btnCloseModal">×</button>
            <h3>Perfil del usuario</h3>
        </div>
        <div class="modal-body">
            <div id="wrapperDialog">

                <div class="container_12">
                    <div id="divEdicionModal" class="form-grid">
                        <div class="alert alert-error" id="divErrorModal" style="display: none"></div>

                        <div class="form leftLabel">
                            <ul style="list-style-type: none" id="ulModal">
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Empresa</label>
                                    <div class="fieldwrap">
                                        <label id="lblEmpresa"></label>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Nombre contacto</label>
                                    <div class="fieldwrap">
                                        <label id="lblContacto"></label>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Fecha de alta</label>
                                    <div class="fieldwrap">
                                        <label id="lblFechaAlta"></label>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Email</label>
                                    <div class="fieldwrap">
                                        <label id="lblEmail"></label>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Pwd</label>
                                    <div class="fieldwrap">
                                        <label id="lblPwd"></label>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Teléfono</label>
                                    <div class="fieldwrap">
                                        <label id="lblTel"></label>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Dirección</label>
                                    <div class="fieldwrap">
                                        <label id="lblDir"></label>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Activo</label>
                                    <div class="fieldwrap">
                                        <label id="lblActivo"></label>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Servicio Especial</label>
                                    <div class="fieldwrap">
                                          <input type="checkbox" id="chkServiciosEsp"><br>
                                    </div>
                                </li>
                                <li>
                                    <label class="fldTitle" style="font-weight: bold">Observaciones</label>
                                    <div class="fieldwrap">
                                        <label id="lblObservaciones"></label>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button class="btn" id="btnAceptarModal" onclick="guardarUsuario();return false;">Aceptar</button>
        </div>
    </div>
</asp:Content>

