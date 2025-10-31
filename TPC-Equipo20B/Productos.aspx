
<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="TPC_Equipo20B.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- 🟢 Encabezado -->
    <div class="flex justify-between items-center mb-6">
        <h2 class="text-3xl font-bold text-slate-900 dark:text-white">Gestión de Productos</h2>

        <asp:Button ID="btnAgregarProducto" runat="server" 
            CssClass="flex items-center gap-2 px-4 py-2 rounded-lg bg-primary text-background-dark font-semibold hover:bg-primary/90 transition"
            Text="Agregar Producto"
            OnClick="btnAgregarProducto_Click" />
    </div>

    <!-- 🔍 Barra de búsqueda -->
    <div class="flex flex-col md:flex-row gap-4 mb-6">
        <div class="flex-grow">
            <div class="relative">
                <span class="material-symbols-outlined absolute left-3 top-2.5 text-slate-500 dark:text-slate-400">search</span>
                <asp:TextBox ID="txtBuscar" runat="server"
                    placeholder="Buscar por nombre o código..."
                    CssClass="w-full pl-10 pr-4 py-2 border border-border-light dark:border-border-dark rounded-lg bg-white dark:bg-background-dark text-slate-700 dark:text-slate-200 focus:outline-none focus:ring-2 focus:ring-primary/50" />
            </div>
        </div>

        <div class="flex gap-3">
            <asp:DropDownList ID="ddlCategoria" runat="server"
                CssClass="px-4 py-2 rounded-lg bg-white dark:bg-background-dark border border-border-light dark:border-border-dark hover:bg-border-light dark:hover:bg-border-dark transition">
                <asp:ListItem Text="Todas las categorías" Value="0" />
                <asp:ListItem Text="Aguas" Value="1" />
                <asp:ListItem Text="Bebidas" Value="2" />
                <asp:ListItem Text="Snacks" Value="3" />
            </asp:DropDownList>

            <asp:DropDownList ID="ddlEstado" runat="server"
                CssClass="px-4 py-2 rounded-lg bg-white dark:bg-background-dark border border-border-light dark:border-border-dark hover:bg-border-light dark:hover:bg-border-dark transition">
                <asp:ListItem Text="Todos" Value="0" />
                <asp:ListItem Text="Activos" Value="1" />
                <asp:ListItem Text="Inactivos" Value="2" />
            </asp:DropDownList>

            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar"
                CssClass="flex items-center gap-1 px-4 py-2 rounded-lg bg-primary text-background-dark font-semibold hover:bg-primary/90 transition"
                OnClick="btnFiltrar_Click" />
        </div>
    </div>

    <!-- 📋 Tabla de productos -->
    <div class="overflow-hidden rounded-lg border border-border-light dark:border-border-dark bg-white dark:bg-background-dark">
        <div class="overflow-x-auto">
            <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False"
                CssClass="min-w-full text-left border-collapse"
                GridLines="None">
                <HeaderStyle CssClass="bg-background-light dark:bg-black/20" />
                <RowStyle CssClass="border-t border-border-light dark:border-border-dark hover:bg-background-light/50 dark:hover:bg-background-dark/60 transition" />

                <Columns>
                    <asp:BoundField DataField="Descripcion" HeaderText="Producto" />
                    <asp:BoundField DataField="CodigoSKU" HeaderText="Código" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="StockActual" HeaderText="Stock" ItemStyle-HorizontalAlign="Right" />
                    <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# (bool)Eval("Activo")
                                ? "<span class='inline-flex items-center rounded-full bg-green-100 dark:bg-green-900/40 px-3 py-1 text-xs font-semibold text-green-800 dark:text-green-300'>En Stock</span>"
                                : "<span class='inline-flex items-center rounded-full bg-red-100 dark:bg-red-900/40 px-3 py-1 text-xs font-semibold text-red-800 dark:text-red-300'>Inactivo</span>" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <div class="flex justify-center gap-2">
                                <asp:Button ID="btnEditar" runat="server" Text="✏️"
                                    CssClass="p-2 rounded-full hover:bg-border-light dark:hover:bg-border-dark transition"
                                    CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                <asp:Button ID="btnEliminar" runat="server" Text="🗑"
                                    CssClass="p-2 rounded-full hover:bg-border-light dark:hover:bg-border-dark transition"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- 📄 Paginación (simple) -->
    <div class="flex flex-col sm:flex-row justify-between items-center gap-4 mt-6">
        <p class="text-sm text-slate-600 dark:text-slate-400">
            Mostrando <span class="font-semibold">1</span>-<span class="font-semibold">5</span> de
            <span class="font-semibold">50</span> productos
        </p>
        <div class="flex items-center gap-1">
            <button class="h-8 w-8 flex items-center justify-center rounded-lg border border-border-light dark:border-border-dark hover:bg-border-light dark:hover:bg-border-dark transition" disabled>
                <span class="material-symbols-outlined text-sm">chevron_left</span>
            </button>
            <button class="h-8 w-8 flex items-center justify-center rounded-lg bg-primary text-black text-sm font-bold">1</button>
            <button class="h-8 w-8 flex items-center justify-center rounded-lg hover:bg-border-light dark:hover:bg-border-dark text-sm transition">2</button>
            <button class="h-8 w-8 flex items-center justify-center rounded-lg hover:bg-border-light dark:hover:bg-border-dark text-sm transition">3</button>
            <button class="h-8 w-8 flex items-center justify-center rounded-lg border border-border-light dark:border-border-dark hover:bg-border-light dark:hover:bg-border-dark transition">
                <span class="material-symbols-outlined text-sm">chevron_right</span>
            </button>
        </div>
    </div>
</asp:Content>

