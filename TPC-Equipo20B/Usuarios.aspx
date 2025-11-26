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

        <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-center">
            <ItemStyle CssClass="action-col text-center" />

            <ItemTemplate>
                <asp:LinkButton ID="btnHacerAdmin" runat="server"
                    Text="Hacer Admin"
                    CssClass="btn btn-warning btn-action-sm"
                    CommandName="HacerAdmin"
                    CommandArgument='<%# Eval("Id") %>'>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>

</asp:GridView>



    </div>

</asp:Panel>

</asp:Content>

