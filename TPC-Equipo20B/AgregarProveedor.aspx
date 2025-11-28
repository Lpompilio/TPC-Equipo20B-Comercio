<%@ Page Title="Agregar Proveedor" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarProveedor.aspx.cs" Inherits="TPC_Equipo20B.AgregarProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function validarNombreORazon(source, args) {

            var nombre = document.getElementById('<%= txtNombre.ClientID %>').value;
            var razon = document.getElementById('<%= txtRazonSocial.ClientID %>').value;


            if (nombre.trim() !== "" || razon.trim() !== "") {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script>

    <style>
        .error-flotante {
            position: absolute;
            font-size: 0.8rem;
            margin-top: 2px;
            color: #dc3545;
        }

        .col-md-6 {
            position: relative;
        }
    </style>
    <div class="mb-4">
        <h2 id="lblTitulo" runat="server" class="fw-bold">Agregar Nuevo Proveedor</h2>
        <p class="text-muted">Complete los datos del proveedor</p>
    </div>

    <asp:Panel ID="panelError" runat="server" Visible="false">
        <div class="alert alert-danger d-flex align-items-center" role="alert">
            <i class="bi bi-exclamation-triangle-fill me-2"></i>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
    </asp:Panel>


    <div class="card shadow-sm">
        <div class="card-body">
            <div class="row g-3 mb-3">

                <div class="col-md-6">
                    <label class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre comercial" />

                    <asp:CustomValidator ID="cvNombreRazon" runat="server"
                        ErrorMessage="Complete Nombre o Razón Social"
                        ClientValidationFunction="validarNombreORazon"
                        OnServerValidate="cvNombreRazon_ServerValidate"
                        ValidationGroup="GuardarProveedor"
                        CssClass="error-flotante"
                        Display="Dynamic">
                    </asp:CustomValidator>
                </div>

                <div class="col-md-6">
                    <label class="form-label">Razón Social</label>
                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" placeholder="Ej: Distribuidora Norte S.A." />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Documento / CUIT</label>
                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Ej: 30-12345678-9" />

                    <asp:RegularExpressionValidator ID="revDocumento" runat="server"
                        ControlToValidate="txtDocumento"
                        ErrorMessage="Solo números y guiones"
                        ValidationExpression="^[0-9-]+$"
                        ValidationGroup="GuardarProveedor"
                        CssClass="error-flotante"
                        Display="Dynamic" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Condición IVA</label>
                    <asp:DropDownList ID="ddlIVA" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                        <asp:ListItem Text="Responsable Inscripto" Value="Responsable Inscripto"></asp:ListItem>
                        <asp:ListItem Text="Monotributista" Value="Monotributista"></asp:ListItem>
                        <asp:ListItem Text="Consumidor Final" Value="Consumidor Final"></asp:ListItem>
                        <asp:ListItem Text="Exento" Value="Exento"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="rfvIVA" runat="server"
                        ControlToValidate="ddlIVA"
                        InitialValue=""
                        ErrorMessage="Seleccione condición fiscal"
                        ValidationGroup="GuardarProveedor"
                        CssClass="error-flotante"
                        Display="Dynamic" />
                </div>


                <div class="col-md-6">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="correo@proveedor.com" />

                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Ingresar mail"
                        ValidationGroup="GuardarProveedor"
                        CssClass="error-flotante"
                        Display="Dynamic" />

                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Formato inválido"
                        ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                        ValidationGroup="GuardarProveedor"
                        CssClass="error-flotante"
                        Display="Dynamic" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej: 011-4321-5678" />

                    <asp:RequiredFieldValidator ID="rfvTelefono" runat="server"
                        ControlToValidate="txtTelefono"
                        ErrorMessage="Ingresa un numero de telefono"
                        ValidationGroup="GuardarProveedor"
                        CssClass="error-flotante"
                        Display="Dynamic" />

                    <asp:RegularExpressionValidator ID="revTelefono" runat="server"
                        ControlToValidate="txtTelefono"
                        ErrorMessage="Solo números y guiones"
                        ValidationExpression="^[0-9-]+$"
                        ValidationGroup="GuardarProveedor"
                        CssClass="error-flotante"
                        Display="Dynamic" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Dirección</label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ej: Av. Belgrano 3456" />

                    <asp:RequiredFieldValidator ID="rfvDireccion" runat="server"
                        ControlToValidate="txtDireccion"
                        ErrorMessage="Ingresar Direccion"
                        ValidationGroup="GuardarProveedor"
                        CssClass="error-flotante"
                        Display="Dynamic" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Localidad</label>
                    <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Ej: CABA" />

                    <asp:RequiredFieldValidator ID="rfvLocalidad" runat="server"
                        ControlToValidate="txtLocalidad"
                        ErrorMessage="Ingresar Localidad"
                        ValidationGroup="GuardarProveedor"
                        CssClass="error-flotante"
                        Display="Dynamic" />
                </div>
            </div>
        </div>

        <div class="card-footer d-flex justify-content-end gap-2">
            <asp:Button ID="btnCancelar" runat="server"
                Text="Cancelar"
                CssClass="btn btn-outline-secondary"
                OnClick="btnCancelar_Click"
                CausesValidation="false" />

            <asp:Button ID="btnGuardar" runat="server"
                Text="Guardar Proveedor"
                CssClass="btn btn-success"
                OnClick="btnGuardar_Click"
                ValidationGroup="GuardarProveedor" />
        </div>
    </div>
</asp:Content>

