<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="TPC_Equipo20B.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="fw-bold">Gestión de Productos</h2>
        <asp:Button ID="btnAgregarProducto" runat="server" Text="➕ Agregar Producto"
            CssClass="btn btn-success" OnClick="btnAgregarProducto_Click" />
    </div>

    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False"
        CssClass="table table-hover align-middle" DataKeyNames="Id" OnRowCommand="gvProductos_RowCommand">
        <Columns>
            <asp:BoundField DataField="Descripcion" HeaderText="Producto" />
            <asp:BoundField DataField="CodigoSKU" HeaderText="Código SKU" />
            <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
            <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
            <asp:BoundField DataField="PorcentajeGanancia" HeaderText="Ganancia (%)" />
            <asp:TemplateField HeaderText="Activo">
                <ItemTemplate>
                    <%# (bool)Eval("Activo") ? "<span class='badge bg-success'>Sí</span>" : "<span class='badge bg-secondary'>No</span>" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton ID="cmdEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-primary me-2">Editar</asp:LinkButton>
                    <asp:LinkButton ID="cmdEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Eliminar producto?');">Eliminar</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
