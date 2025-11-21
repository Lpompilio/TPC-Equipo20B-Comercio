<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="TPC_Equipo20B.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="fw-bold">Gestión de Compras</h2>
        <asp:Button ID="btnNuevaCompra" runat="server"
            Text="➕ Nueva Compra"
            CssClass="btn btn-success"
            OnClick="btnNuevaCompra_Click" />
    </div>

    <!-- 🔎 Toolbar de búsqueda -->
    <div class="toolbar d-flex gap-2 mb-3">
        <asp:TextBox ID="txtBuscarCompra" runat="server"
            CssClass="form-control"
            placeholder="Buscar por proveedor, usuario u observaciones…" />
        <asp:Button ID="btnBuscarCompra" runat="server"
            CssClass="btn btn-primary"
            Text="Buscar"
            OnClick="btnBuscarCompra_Click"
            UseSubmitBehavior="false" />
    </div>

<asp:GridView ID="gvCompras" runat="server" AutoGenerateColumns="False"
    CssClass="table table-hover align-middle" DataKeyNames="Id"
    OnRowCommand="gvCompras_RowCommand"
    OnRowDataBound="gvCompras_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="N° Compra" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />

            <asp:TemplateField HeaderText="Proveedor">
                <ItemTemplate><%# Eval("Proveedor.Nombre") %></ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <%# (bool)Eval("Cancelada") 
                     ? "<span class='badge bg-danger'>Cancelada</span>" 
                        : "<span class='badge bg-success'>Activa</span>" %>
                 </ItemTemplate>
                </asp:TemplateField>

            <asp:TemplateField HeaderText="Usuario">
                <ItemTemplate><%# Eval("Usuario.Nombre") %></ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton ID="cmdDetalle" runat="server"
                        CommandName="Detalle"
                        CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-sm btn-info me-2">
                        <i class="bi bi-eye"></i> Ver Detalle
                    </asp:LinkButton>
                    <asp:LinkButton ID="cmdEliminar" runat="server"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-sm btn-danger">
                        <i class="bi bi-x-circle"></i> Cancelar
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
