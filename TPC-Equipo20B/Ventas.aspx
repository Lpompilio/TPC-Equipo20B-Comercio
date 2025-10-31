<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="TPC_Equipo20B.Ventas" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="px-4 sm:px-8 md:px-16 lg:px-24 xl:px-40 py-5">
    <div class="max-w-[1200px] mx-auto">

        <!-- 🔹 Título principal -->
        <div class="flex flex-wrap justify-between items-center gap-4 mb-8">
            <h1 class="text-gray-900 dark:text-white text-4xl font-black leading-tight tracking-[-0.033em]">
                Informes de Ventas
            </h1>
        </div>

        <!-- 🔹 Filtros -->
        <div class="bg-white dark:bg-background-dark/50 rounded-lg shadow-sm mb-8">
            <h3 class="text-gray-900 dark:text-white text-lg font-bold px-6 pt-6">Filtros</h3>

            <div class="p-6 flex flex-col gap-6">

                <!-- 🗓 Rango de Fechas -->
                <div class="flex flex-col gap-2">
                    <label class="text-gray-800 dark:text-gray-200 text-sm font-medium">Rango de Fechas</label>
                    <div class="flex flex-col sm:flex-row gap-3">
                        <asp:TextBox ID="txtFechaDesde" runat="server"
                            CssClass="form-input rounded-lg border border-gray-300 dark:border-gray-700 bg-background-light dark:bg-background-dark/80 h-12 px-4 text-base focus:border-primary focus:ring-primary/30 flex-1"
                            TextMode="Date" placeholder="Desde" />
                        <asp:TextBox ID="txtFechaHasta" runat="server"
                            CssClass="form-input rounded-lg border border-gray-300 dark:border-gray-700 bg-background-light dark:bg-background-dark/80 h-12 px-4 text-base focus:border-primary focus:ring-primary/30 flex-1"
                            TextMode="Date" placeholder="Hasta" />
                    </div>
                </div>

                <!--  Producto o Categoría -->
                <div class="flex flex-col gap-2">
                    <label class="text-gray-800 dark:text-gray-200 text-sm font-medium">Producto o Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server"
                        CssClass="form-input rounded-lg border border-gray-300 dark:border-gray-700 bg-background-light dark:bg-background-dark/80 h-12 px-4 text-base">
                        <asp:ListItem Text="Todas las categorías" Value="0" />
                        <asp:ListItem Text="Aguas" Value="1" />
                        <asp:ListItem Text="Bebidas" Value="2" />
                        <asp:ListItem Text="Snacks" Value="3" />
                    </asp:DropDownList>
                </div>

                <!--  Botones -->
                <div class="flex flex-col sm:flex-row justify-end gap-3 pt-4 border-t border-gray-200 dark:border-gray-700">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Aplicar Filtros"
                        CssClass="w-full sm:w-auto flex items-center justify-center rounded-lg h-12 px-6 bg-primary text-white text-sm font-bold hover:bg-primary/90 transition"
                        OnClick="btnFiltrar_Click" />

                    <asp:Button ID="btnNuevaVenta" runat="server" Text="➕ Nueva Venta"
                        CssClass="w-full sm:w-auto flex items-center justify-center rounded-lg h-12 px-6 bg-green-500 text-white text-sm font-bold hover:bg-green-600 transition"
                        OnClick="btnNuevaVenta_Click" />
                </div>
            </div>
        </div>

        <!--  Tabla de resultados -->
        <div class="bg-white dark:bg-background-dark/50 rounded-lg shadow-sm overflow-hidden">
            <div class="p-6 flex justify-between items-center flex-wrap gap-4">
                <h4 class="text-lg font-bold text-gray-900 dark:text-white">Detalle de Ventas</h4>
                <asp:Button ID="btnExportar" runat="server" Text="⬇️ Exportar CSV"
                    CssClass="flex items-center justify-center rounded-lg h-10 px-4 bg-background-light dark:bg-background-dark/80 text-gray-700 dark:text-gray-300 border border-gray-200 dark:border-gray-700 text-sm font-bold hover:bg-gray-200 dark:hover:bg-gray-700 transition"
                    OnClick="btnExportar_Click" />
            </div>

            <!-- 🔸 GridView con datos -->
            <asp:GridView ID="gvVentas" runat="server" AutoGenerateColumns="False"
                CssClass="min-w-full text-left border-t border-gray-200 dark:border-gray-700">
                <HeaderStyle CssClass="bg-background-light dark:bg-background-dark/70 text-gray-700 dark:text-gray-300 text-sm font-semibold" />
                <RowStyle CssClass="border-b border-gray-200 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-800 transition" />

                <Columns>
                    <asp:BoundField DataField="Producto" HeaderText="Producto" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                    <asp:BoundField DataField="UnidadesVendidas" HeaderText="Unidades Vendidas" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Total" HeaderText="Ingresos Totales" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>

            <div class="p-4 flex justify-between items-center border-t border-gray-200 dark:border-gray-700">
                <p class="text-sm text-gray-500 dark:text-gray-400">Mostrando 1-4 de 25 resultados</p>
            </div>
        </div>
    </div>
</div>

</asp:Content>

