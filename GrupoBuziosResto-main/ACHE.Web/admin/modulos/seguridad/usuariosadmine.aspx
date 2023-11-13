<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.master" AutoEventWireup="true" CodeFile="usuariosadmine.aspx.cs" Inherits="admin_modulos_seguridad_usuariosadmine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Admin :: <asp:Literal runat="server" ID="litModo" /> de Usuario
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="container_12" style="margin-left:20px; width:750px">
		<br />
		<h4><asp:Literal runat="server" ID="litModo2" /> de Usuario</h4>
		<br />
        <div id="divEdicion" class="form-grid">
			<asp:Panel runat="server" ID="pnlError" Visible="false" class="notification-wrap failure" style="margin-left:-5px">
				<span class="icon-failure">ERROR</span>
				<asp:Literal runat="server" ID="litError"></asp:Literal>
			</asp:Panel>
			<asp:CustomValidator ID="valCustom" runat="server" Display="None" OnServerValidate="ServerValidate"></asp:CustomValidator>
            
            <div class="form leftLabel">
				<ul style="list-style-type: none">
					<li>
						<label class="fldTitle">Tipo</label>
						<div class="fieldwrap">
							<asp:DropDownList runat="server" ID="ddlTipo" TabIndex="1">
                                <asp:ListItem Text="Admin" Value="A"></asp:ListItem>
                                <asp:ListItem Text="Usuario" Value="U"></asp:ListItem>
							</asp:DropDownList>
                            <br />
                            <span><i>Un usuario administrador será el único que podrá crear otros usuarios.</i></span>
						</div>
					</li>
                    <li>
						<label class="fldTitle"><abbr title="Required Field" class="require">*</abbr> Nombre</label>
						<div class="fieldwrap">
							<asp:TextBox runat="server" ID="txtNombre" TabIndex="2" MaxLength="128" CssClass="medium"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator runat="server" ID="rqvNomre" ControlToValidate="txtNombre"
							    ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"></asp:RequiredFieldValidator>
						</div>
					</li>
                    <li>
						<label class="fldTitle"><abbr title="Required Field" class="require">*</abbr> Email</label>
						<div class="fieldwrap">
							<asp:TextBox runat="server" ID="txtEmail" TabIndex="3" MaxLength="128" CssClass="medium"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmail"
							    ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"></asp:RequiredFieldValidator>
						</div>
					</li>
                    <li>
						<label class="fldTitle">Contraseña</label>
						<div class="fieldwrap">
							<asp:TextBox runat="server" ID="txtPwd" TabIndex="4" MaxLength="10" TextMode="Password" CssClass="medium"></asp:TextBox>

                            <asp:RequiredFieldValidator runat="server" ID="rqvPwd" ControlToValidate="txtPwd"
							    ErrorMessage="Este campo es obligatorio." CssClass="errorRequired"></asp:RequiredFieldValidator>
						</div>
					</li>
                    <li>
						<label class="fldTitle">Confirmar Contraseña</label>
						<div class="fieldwrap">
							<asp:TextBox runat="server" ID="txtPwd2" TabIndex="4" MaxLength="10" TextMode="Password" CssClass="medium"></asp:TextBox>

                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToValidate="txtPwd2"
                                CssClass="errorRequired"
                                ControlToCompare="txtPwd"
                                ErrorMessage="Las contraseñas no coinciden" />
						</div>
					</li>

                    <li>
						<label class="fldTitle">Activo</label>
						<div class="fieldwrap">
							<asp:CheckBox runat="server" ID="chkActivo" Checked="true" TabIndex="5" />
						</div>
					</li>
                   
                    <li class="buttons bottom-round noboder">
						<div class="fieldwrap">
							<br />
                            <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" CssClass="submit-button" OnClientClick="btnAceptarClick(this);" OnClick="btnAceptar_OnClick" TabIndex="20" />
							<asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="submit-button" OnClick="btnCancelar_OnClick" CausesValidation="false" TabIndex="21" />
						</div>
					</li>
				</ul>
			</div>		
            
        </div>
	</div>
</asp:Content>


