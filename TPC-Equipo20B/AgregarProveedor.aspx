<%@ Page Title="Agregar Proveedor" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarProveedor.aspx.cs" Inherits="TPC_Equipo20B.AgregarProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .validator {
            color: red;
            font-size: 12px;
        }
    </style>
    <div class="mb-4">
        <h2 id="lblTitulo" runat="server" class="fw-bold">Agregar Nuevo Proveedor</h2>
        <p class="text-muted">Complete los datos del proveedor</p>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <div class="row g-3 mb-3">
                <div class="col-md-6">
                    <label class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre comercial" />
                    <asp:RequiredFieldValidator ErrorMessage="Ingresar Nombre del proveedor" ControlToValidate="txtNombre" runat="server" CssClass="validator" Display="Dynamic" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Razón Social</label>
                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" placeholder="Ej: Distribuidora Norte S.A." />
                    <asp:RequiredFieldValidator ErrorMessage="Ingresar Razón Social" ControlToValidate="txtRazonSocial" runat="server" CssClass="validator" Display="Dynamic" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Documento / CUIT</label>
                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Ej: 30-12345678-9" />
                    <asp:RequiredFieldValidator
                        ID="valReqDocumento"
                        runat="server"
                        ControlToValidate="txtDocumento"
                        ErrorMessage="El DNI / Cuit es requerido"
                        Display="Dynamic"
                        CssClass="validator" />

                    <asp:RegularExpressionValidator
                        ID="valRegexDocumento"
                        runat="server"
                        ControlToValidate="txtDocumento"
                        ErrorMessage="Solo se permiten números y guiones"
                        Display="Dynamic"
                        CssClass="validator"
                        ValidationExpression="^[0-9-]+$" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Condición IVA</label>
                    <asp:TextBox ID="txtIVA" runat="server" CssClass="form-control" placeholder="Ej: Responsable Inscripto" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="correo@proveedor.com" />
                    <asp:RequiredFieldValidator ID="vflEmail" ErrorMessage="Ingresar un Mail" ControlToValidate="txtEmail" runat="server" CssClass="validator" Display="Dynamic" />
                    <asp:RegularExpressionValidator
                        ID="revEmail"
                        runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Formato de email no válido."
                        CssClass="validator"
                        Display="Dynamic"
                        SetFocusOnError="true"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej: 011-4321-5678" />
                    <asp:RequiredFieldValidator ID="vflTelefono" ErrorMessage="Ingresa Teléfono del proveedor" ControlToValidate="txtTelefono" runat="server" CssClass="validator" Display="Dynamic" />
                    <asp:RegularExpressionValidator
                        ID="revTelefono"
                        runat="server"
                        ControlToValidate="txtTelefono"
                        ErrorMessage="Formato no válido. Solo números y guiones."
                        CssClass="validator"
                        Display="Dynamic"
                        SetFocusOnError="true"
                        ValidationExpression="^[0-9-]+$" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Dirección</label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ej: Av. Belgrano 3456" />
                    <asp:RequiredFieldValidator ErrorMessage="Ingresar dirección del proveedor" ControlToValidate="txtDireccion" runat="server" CssClass="validator" Display="Dynamic" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Localidad</label>
                    <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Ej: CABA" />
                    <asp:RequiredFieldValidator ErrorMessage="Ingresar Localidad del proveedor" ControlToValidate="txtLocalidad" runat="server" CssClass="validator" Display="Dynamic"/>
                </div>
            </div>
        </div>

        <div class="card-footer d-flex justify-content-end gap-2">
            <asp:Button ID="btnCancelar" runat="server"
                Text="Cancelar"
                CssClass="btn btn-outline-secondary"
                OnClick="btnCancelar_Click" />

            <asp:Button ID="btnGuardar" runat="server"
                Text="Guardar Proveedor"
                CssClass="btn btn-success"
                OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>

