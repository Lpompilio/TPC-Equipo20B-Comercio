<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="TPC_Equipo20B.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
    .fila-deshabilitada td {
        background-color: #f0f0f0 !important;
        color: #6c757d !important;
    }

    .precio-venta {
        font-weight: bold;
    }
</style>

<div class="d-flex align-items-center justify-content-between mb-3">
    <h2 class="page-title m-0">Gestión de Productos</h2>

    <% if ((bool)(Session["EsAdmin"] ?? false)) { %>
        <asp:Button ID="btnAgregarProducto" runat="server"
            Text="➕ Agregar Producto"
            CssClass="btn btn-brand"
            OnClick="btnAgregarProducto_Click" />
    <% } %>
</div>

<div class="form-check mb-3">
    <asp:CheckBox ID="chkSoloHabilitados" runat="server"
        CssClass="form-check-input"
        AutoPostBack="true"
        OnCheckedChanged="chkSoloHabilitados_CheckedChanged" />
    <label class="form-check-label" for="chkSoloHabilitados">
        Mostrar solo habilitados
    </label>
</div>

<!-- Panel para que ENTER dispare la búsqueda -->
<asp:Panel ID="pnlBusquedaProductos" runat="server" DefaultButton="btnBuscarProducto">

    <div class="toolbar d-flex gap-2 mb-3">
        <asp:TextBox ID="txtBuscarProducto" runat="server"
            CssClass="form-control"
            placeholder="Buscar…" />

        <asp:DropDownList ID="ddlProveedor" runat="server"
            CssClass="form-select"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged">
        </asp:DropDownList>

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
            OnRowCommand="gvProductos_RowCommand"
            OnRowDataBound="gvProductos_RowDataBound">

            <Columns>


                <asp:BoundField DataField="Descripcion" HeaderText="Producto" />


                <asp:BoundField DataField="CodigoSKU" HeaderText="Código SKU" />


                <asp:TemplateField HeaderText="Marca">
                    <ItemTemplate>
                        <%# Eval("Marca.Nombre") ?? "-" %>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Categoría">
                    <ItemTemplate>
                        <%# Eval("Categoria.Nombre") ?? "-" %>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />

                <asp:TemplateField HeaderText="Precio Venta">
                    <ItemTemplate>
                        <span class="precio-venta">
                            <%# String.Format("$ {0:N2}", Eval("PrecioVenta")) %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Habilitado">
                    <ItemTemplate>
                        <%# (bool)Eval("Habilitado")
                            ? "<span class='badge bg-success'>Sí</span>"
                            : "<span class='badge bg-danger'>No</span>" %>
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
