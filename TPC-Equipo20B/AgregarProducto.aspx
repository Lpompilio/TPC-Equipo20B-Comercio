
<%@ Page Title="Agregar Producto" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarProducto.aspx.cs" Inherits="TPC_Equipo20B.AgregarProducto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="max-w-4xl mx-auto bg-white dark:bg-background-dark rounded-xl p-8 shadow border border-primary/20 dark:border-primary/30">
        <h2 class="text-3xl font-bold mb-6 text-slate-900 dark:text-white">Agregar Nuevo Producto</h2>

        <asp:ValidationSummary runat="server" CssClass="text-red-500 mb-4" />

        <!-- 🧾 Información básica -->
        <div class="grid grid-cols-1 md:grid-cols-2 gap-6 mb-6">
            <div>
                <label for="txtDescripcion" class="block font-semibold mb-1">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-primary/50" placeholder="Ej: Agua Purificada 1L"></asp:TextBox>
            </div>

            <div>
                <label for="txtCodigo" class="block font-semibold mb-1">Código SKU</label>
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-primary/50" placeholder="Ej: AGP-001"></asp:TextBox>
            </div>

            <div>
                <label for="ddlMarca" class="block font-semibold mb-1">Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-primary/50">
                </asp:DropDownList>
            </div>

            <div>
                <label for="ddlCategoria" class="block font-semibold mb-1">Categoría</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-primary/50">
                </asp:DropDownList>
            </div>

            <div>
                <label for="ddlProveedor" class="block font-semibold mb-1">Proveedor</label>
                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-primary/50">
                </asp:DropDownList>
            </div>
        </div>

        <!-- 📦 Detalles comerciales -->
        <div class="grid grid-cols-1 md:grid-cols-3 gap-6 mb-6">
            <div>
                <label for="txtStockMinimo" class="block font-semibold mb-1">Stock Mínimo</label>
                <asp:TextBox ID="txtStockMinimo" runat="server" TextMode="Number" CssClass="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-primary/50" />
            </div>

            <div>
                <label for="txtStockActual" class="block font-semibold mb-1">Stock Actual</label>
                <asp:TextBox ID="txtStockActual" runat="server" TextMode="Number" CssClass="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-primary/50" />
            </div>

            <div>
                <label for="txtGanancia" class="block font-semibold mb-1">Porcentaje de Ganancia</label>
                <asp:TextBox ID="txtGanancia" runat="server" TextMode="Number" CssClass="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-primary/50" />
            </div>
        </div>

        <!-- 🖼️ Imagen -->
        <div class="mb-6">
            <label for="txtUrlImagen" class="block font-semibold mb-1">URL de Imagen (opcional)</label>
            <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="w-full border rounded-lg px-3 py-2 focus:ring-2 focus:ring-primary/50" placeholder="https://..."></asp:TextBox>
        </div>

        <!-- ⚙️ Estado -->
        <div class="flex items-center mb-6">
            <asp:CheckBox ID="chkActivo" runat="server" Checked="true" CssClass="mr-2" />
            <label for="chkActivo" class="font-semibold">Producto activo</label>
        </div>

        <!-- 🔘 Botones -->
        <div class="flex justify-end gap-4">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                CssClass="px-6 py-2 bg-gray-200 dark:bg-gray-700 text-slate-900 dark:text-white rounded-lg font-semibold hover:bg-gray-300 dark:hover:bg-gray-600 transition" />

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Producto"
                CssClass="px-6 py-2 bg-primary text-white font-semibold rounded-lg hover:bg-primary/90 transition" OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>
