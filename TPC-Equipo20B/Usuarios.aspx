<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="TPC_Equipo20B.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .fila-admin td {
            background-color: #e7f1ff !important; /* celestito leve */
        }

        .sort-icon {
            font-size: 0.8rem;
            opacity: 0.6;
            margin-left: 4px;
        }

        .sort-link {
            text-decoration: none;
            color: inherit;
            cursor: pointer;
        }

        .sort-link:hover {
            opacity: 0.7;
        }

        /* ----- Botones de acciones de usuario ----- */
       .action-col {
    text-align: right !important;
    vertical-align: middle !important;
    width: 260px; /* ajustá este valor si hace falta */
}

       .btn-group-actions {
    display: flex;
    align-items: center;
    justify-content: flex-end;
    gap: 0.5rem;
    width: 100%;
}

        .action-col .btn-user-action {
    font-size: 0.8rem;
    padding: 0.25rem 0.7rem;
    border-radius: 999px;
    line-height: 1.2;
    white-space: nowrap;
    min-width: 110px;  /* 👈 clave: mismo ancho para todos */
    text-align: center;
}

        .action-col .btn-role-admin {
            background-color: #ffc107;
            border-color: #ffc107;
            color: #4b3b00;
        }

        .action-col .btn-role-vendedor {
            background-color: #e2e3e5;
            border-color: #e2e3e5;
            color: #343a40;
        }

        .action-col .btn-edit {
            background-color: #0d6efd;
            border-color: #0d6efd;
            color: #fff;
        }

        .action-col .btn-disable {
            background-color: #dc3545;
            border-color: #dc3545;
            color: #fff;
        }

        .action-col .btn-enable {
            background-color: #198754;
            border-color: #198754;
            color: #fff;
        }

        .action-col .btn-user-action:hover {
            filter: brightness(0.95);
        }

        /* Aseguramos alineación vertical en toda la tabla */
        .table td, .table th {
            vertical-align: middle;
        }
    </style>

    <div class="d-flex align-items-center justify-content-between mb-3">
        <h2 class="page-title m-0">Gestión de Usuarios</h2>
    </div>

    <asp:Panel ID="pnlBusquedaUsuarios" runat="server" DefaultButton="btnBuscarUsuario">

        <div class="toolbar d-flex gap-2 mb-3">
            <asp:TextBox ID="txtBuscarUsuario" runat="server"
                CssClass="form-control"
                placeholder="Buscar usuario…" />

            <asp:Button ID="btnBuscarUsuario" runat="server"
                Text="Buscar"
                CssClass="btn btn-primary"
                OnClick="btnBuscarUsuario_Click" />
        </div>

        <div class="grid">
            <asp:GridView ID="gvUsuarios" runat="server"
                CssClass="table table-hover align-middle"
                AutoGenerateColumns="False"
                DataKeyNames="Id"
                OnRowDataBound="gvUsuarios_RowDataBound"
                OnRowCommand="gvUsuarios_RowCommand">

                <Columns>
                    <asp:BoundField DataField="Username" HeaderText="Usuario" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="RolDescripcion" HeaderText="Rol" />

                    <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-end">
                        <ItemStyle CssClass="action-col text-end" />
                        <ItemTemplate>
                            <div class="btn-group-actions">
                                <!-- Cambiar rol -->
                                <asp:LinkButton ID="btnCambiarRol" runat="server"
                                    CssClass="btn btn-sm btn-user-action"
                                    CommandName="CambiarRol"
                                    CommandArgument='<%# Eval("Id") %>'>
                                </asp:LinkButton>

                                <!-- Editar usuario -->
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    Text="Editar"
                                    CssClass="btn btn-sm btn-user-action btn-edit"
                                    CommandName="EditarUsuario"
                                    CommandArgument='<%# Eval("Id") %>'>
                                </asp:LinkButton>

                                <!-- Activar / Desactivar -->
                                <asp:LinkButton ID="btnToggleActivo" runat="server"
                                    CssClass="btn btn-sm btn-user-action"
                                    CommandName="ToggleActivo"
                                    CommandArgument='<%# Eval("Id") %>'>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>

    </asp:Panel>

</asp:Content>
