<%@ Page Title="Agregar Cliente" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarCliente.aspx.cs" Inherits="TPC_Equipo20B.AgregarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-4">
        <h2 id="lblTitulo" runat="server" class="fw-bold">Agregar Nuevo Cliente</h2>
        <p class="text-muted">Complete los datos del cliente</p>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <div class="row g-3 mb-3">
                <div class="col-md-6">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre del cliente" />
                </div>

                <div class="col-md-6">
                    <label for="txtDocumento" class="form-label">Documento / CUIT</label>
                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Ej: 30-12345678-9" />
                </div>

                <div class="col-md-6">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="correo@cliente.com" />
                </div>

                <div class="col-md-6">
                    <label for="txtTelefono" class="form-label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ej: 011-4321-5678" />
                </div>

                <div class="col-md-6">
                    <label for="txtDireccion" class="form-label">Dirección</label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ej: Av. Belgrano 1234" />
                </div>

                <div class="col-md-6">
                    <label for="txtLocalidad" class="form-label">Localidad</label>
                    <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Ej: CABA" />
                </div>

                <div class="col-md-6">
                    <label for="ddlCondicionIVA" class="form-label">Condición IVA</label>
                    <asp:DropDownList ID="ddlCondicionIVA" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Responsable Inscripto" />
                        <asp:ListItem Text="Monotributista" />
                        <asp:ListItem Text="Consumidor Final" />
                        <asp:ListItem Text="Exento" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-6 d-flex align-items-center mt-2">
                    <asp:CheckBox ID="chkHabilitado" runat="server" Checked="true" CssClass="form-check-input me-2" />
                    <label for="chkHabilitado" class="form-check-label">Cliente habilitado</label>
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


