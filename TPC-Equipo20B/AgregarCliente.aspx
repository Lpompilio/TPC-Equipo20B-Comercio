<%@ Page Title="Agregar Cliente" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarCliente.aspx.cs" Inherits="TPC_Equipo20B.AgregarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .validator {
            color: red;
            font-size: 12px;
        }
    </style>
    <div class="mb-4">
        <h2 id="lblTitulo" runat="server" class="fw-bold">Agregar Nuevo Cliente</h2>
        <p class="text-muted">Complete los datos del cliente</p>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <div class="row g-3 mb-3">
                <div class="col-md-6">
                    <label for="txtNombre" class="form-label">Nombre y Apellido</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre y Apellido del cliente" />
                    <asp:RequiredFieldValidator ErrorMessage="Ingrese nombre del cliente" ControlToValidate="txtNombre" runat="server" CssClass="validator" />
                    <asp:RegularExpressionValidator
                        ID="revNombre"
                        runat="server"
                        ControlToValidate="txtNombre"
                        ErrorMessage="El nombre solo debe contener letras y espacios"
                        CssClass="validator"
                        Display="Dynamic"
                        SetFocusOnError="true"
                        ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$" />
                </div>

<div class="col-md-6">
    <label for="txtDocumento" class="form-label">Documento / CUIT</label>
    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Ej: 30123456789" />

    <asp:RequiredFieldValidator ErrorMessage="Ingrese documento del cliente" ControlToValidate="txtDocumento" runat="server" CssClass="validator" />

    <asp:RegularExpressionValidator
        ID="revSoloNumeros"
        runat="server"
        ControlToValidate="txtDocumento"
        ErrorMessage="Este campo solo acepta números."
        CssClass="validator"
        Display="Dynamic"
        ValidationExpression="^\d+$" />

    <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold"></asp:Label>
</div>
                    
                <div class="col-md-6">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="correo@cliente.com" />
                    <asp:RequiredFieldValidator ID="rfvEmail" ErrorMessage="Ingresar un Mail" ControlToValidate="txtEmail" runat="server" CssClass="validator" Display="Dynamic" />
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
                    <label for="txtTelefono" class="form-label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej: 011-4321-5678" />
                    <asp:RequiredFieldValidator ID="rfvTelefono" ErrorMessage="Ingresa Telefono del cliente" ControlToValidate="txtTelefono" runat="server" CssClass="validator" Display="Dynamic" />
                    <asp:RegularExpressionValidator
                        ID="revTelefono"
                        runat="server"
                        ControlToValidate="txtTelefono"
                        ErrorMessage="Formato no válido. Solo números y guiones."
                        CssClass="validator"
                        Display="Dynamic"
                        SetFocusOnError="true"
                        ValidationExpression="^[\d-]+$" />
                </div>

                <div class="col-md-6">
                    <label for="txtDireccion" class="form-label">Dirección</label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ej: Av. Belgrano 1234" />
                    <asp:RequiredFieldValidator ErrorMessage="Ingresar dirección del cliente" ControlToValidate="txtDireccion" runat="server" CssClass="validator" Display="Dynamic" />
                </div>

                <div class="col-md-6">
                    <label for="txtLocalidad" class="form-label">Localidad</label>
                    <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Ej: CABA" />
                    <asp:RequiredFieldValidator ErrorMessage="Ingresar localidad del cliente" ControlToValidate="txtLocalidad" runat="server" CssClass="validator" Display="Dynamic" />
                </div>

                <div class="col-md-6">
                    <label for="ddlCondicionIVA" class="form-label">Condición IVA</label>
                    <asp:DropDownList ID="ddlCondicionIVA" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Seleccione..." />
                        <asp:ListItem Text="Responsable Inscripto" />
                        <asp:ListItem Text="Monotributista" />
                        <asp:ListItem Text="Consumidor Final" />
                        <asp:ListItem Text="Exento" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Seleccione condición IVA" CssClass="validator" ControlToValidate="ddlCondicionIVA" runat="server" InitialValue="" />
                </div>

                <div class="col-md-6 mt-2">
    <div class="form-check form-switch">
        <!-- Único checkbox real, tipo HtmlInputCheckBox -->
        <input id="chkHabilitado" runat="server"
               type="checkbox"
               class="form-check-input" />

        <!-- Label asociado, usando el ClientID del checkbox -->
        <label class="form-check-label fw-bold"
               for="<%= chkHabilitado.ClientID %>">
            Cliente habilitado
        </label>
    </div>
</div>




            </div>
        </div>

        <div class="card-footer d-flex justify-content-end gap-2">
            <asp:Button ID="btnCancelar" runat="server"
                Text="Cancelar"
                CssClass="btn btn-outline-secondary"
                OnClick="btnCancelar_Click" />

            <asp:Button ID="btnGuardar" runat="server"
                Text="Guardar Cliente"
                CssClass="btn btn-success"
                OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>


