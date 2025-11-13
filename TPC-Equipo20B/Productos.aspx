<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="TPC_Equipo20B.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  <div class="d-flex align-items-center justify-content-between mb-3">
    <h2 class="page-title m-0">Gestión de Productos</h2>
    <asp:Button ID="btnAgregarProducto" runat="server"
        Text="➕ Agregar Producto"
        CssClass="btn btn-brand"
        OnClick="btnAgregarProducto_Click" />
  </div>

  <!-- Panel para que ENTER dispare la búsqueda y no el botón Agregar -->
  <asp:Panel ID="pnlBusquedaProductos" runat="server" DefaultButton="btnBuscarProducto">

    <div class="toolbar d-flex gap-2 mb-3">
      <asp:TextBox ID="txtBuscarProducto" runat="server"
        CssClass="form-control"
        placeholder="Buscar…" />
      <asp:Button ID="btnBuscarProducto" runat="server"
        CssClass="btn btn-primary"
        Text="Buscar"
        OnClick="btnBuscarProducto_Click"
        UseSubmitBehavior="false" />
    </div>

    <div class="grid">
      <asp:GridView ID="gvProductos" runat="server"
        CssClass="table table-hover align-middle"
        AutoGenerateColumns="False"
        DataKeyNames="Id"
        OnRowCommand="gvProductos_RowCommand">

        <Columns>
          <asp:BoundField DataField="Descripcion" HeaderText="Producto" />
          <asp:BoundField DataField="CodigoSKU" HeaderText="Código SKU" />
          <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
          <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
          <asp:BoundField DataField="PorcentajeGanancia" HeaderText="Ganancia (%)" />

          <asp:TemplateField HeaderText="Activo">
            <ItemTemplate>
              <%# (bool)Eval("Activo")
                ? "<span class='badge bg-success'>Sí</span>"
                : "<span class='badge bg-secondary'>No</span>" %>
            </ItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-center">
            <ItemStyle CssClass="action-col text-center" />
            <ItemTemplate>
              <asp:LinkButton ID="cmdEditar" runat="server"
                CssClass="btn btn-primary btn-action-sm me-1"
                CommandName="Editar"
                CommandArgument='<%# Eval("Id") %>'>Editar</asp:LinkButton>

              <asp:LinkButton ID="cmdEliminar" runat="server"
                CssClass="btn btn-danger btn-action-sm"
                CommandName="Eliminar"
                CommandArgument='<%# Eval("Id") %>'>Eliminar</asp:LinkButton>
            </ItemTemplate>
          </asp:TemplateField>

        </Columns>
      </asp:GridView>
    </div>

  </asp:Panel>

</asp:Content>
