<%@ Page Title="Editar Usuario" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="EditarUsuario.aspx.cs" Inherits="TPC_Equipo20B.EditarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex align-items-center justify-content-between mb-3">
        <h2 class="page-title m-0">Editar Usuario</h2>
        <asp:HyperLink ID="lnkVolver" runat="server"
            NavigateUrl="Usuarios.aspx"
            CssClass="btn btn-outline-secondary btn-sm">
            Volver a la lista
        </asp:HyperLink>
    </div>

    <asp:Panel ID="pnlEditarUsuario" runat="server" CssClass="card shadow-sm">
        <div class="card-body">
            <div class="row g-3">
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre y Apellido</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                            ControlToValidate="txtNombre"
                            ErrorMessage="El nombre es requerido"
                            CssClass="text-danger small"
                            Display="Dynamic" />
                    </div>

                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                            ControlToValidate="txtEmail"
                            ErrorMessage="El email es requerido"
                            CssClass="text-danger small"
                            Display="Dynamic" />
                    </div>

                    <div class="mb-3">
                        <label for="txtDocumento" class="form-label">Documento</label>
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label for="txtTelefono" class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="txtDireccion" class="form-label">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label for="txtLocalidad" class="form-label">Localidad</label>
                        <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label for="txtUsername" class="form-label">Usuario</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" ReadOnly="true" />
                        <small class="text-muted">El nombre de usuario no se puede cambiar.</small>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Estado</label><br />
                        <asp:Label ID="lblEstado" runat="server" CssClass="badge bg-success"></asp:Label>
                    </div>
                </div>
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="mt-2 d-block"></asp:Label>
        </div>

        <div class="card-footer d-flex justify-content-end gap-2">
            <asp:Button ID="btnCancelar" runat="server"
                Text="Cancelar"
                CssClass="btn btn-outline-secondary"
                OnClick="btnCancelar_Click" />

            <asp:Button ID="btnGuardar" runat="server"
                Text="Guardar cambios"
                CssClass="btn btn-success"
                OnClick="btnGuardar_Click" />
        </div>
    </asp:Panel>

</asp:Content>
