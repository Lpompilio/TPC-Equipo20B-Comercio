<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TPC_Equipo20B.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- 🔹 Título principal -->
    <div class="mb-8">
        <h2 class="text-3xl font-bold text-slate-900 dark:text-white">Panel de Control</h2>
        <p class="text-sm text-primary mt-1">Resumen general del negocio</p>
    </div>

    <!-- 🔸 Sección 1: Resumen general -->
    <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 mb-10">
        <div class="rounded-xl p-6 border border-primary/20 dark:border-primary/30 bg-white dark:bg-background-dark hover:shadow-lg transition">
            <p class="text-base text-slate-700 dark:text-slate-300">Ventas del Mes</p>
            <asp:Label ID="lblVentasMes" runat="server" CssClass="text-3xl font-bold text-slate-900 dark:text-white"></asp:Label>
            <p class="text-sm text-green-600 dark:text-green-400">+12.5% respecto al mes anterior</p>
        </div>

        <div class="rounded-xl p-6 border border-primary/20 dark:border-primary/30 bg-white dark:bg-background-dark hover:shadow-lg transition">
            <p class="text-base text-slate-700 dark:text-slate-300">Pedidos Completados</p>
            <asp:Label ID="lblPedidosCompletados" runat="server" CssClass="text-3xl font-bold text-slate-900 dark:text-white"></asp:Label>
            <p class="text-sm text-green-600 dark:text-green-400">+6 esta semana</p>
        </div>

        <div class="rounded-xl p-6 border border-primary/20 dark:border-primary/30 bg-white dark:bg-background-dark hover:shadow-lg transition">
            <p class="text-base text-slate-700 dark:text-slate-300">Clientes Nuevos</p>
            <asp:Label ID="lblClientesNuevos" runat="server" CssClass="text-3xl font-bold text-slate-900 dark:text-white"></asp:Label>
            <p class="text-sm text-green-600 dark:text-green-400">+3 desde ayer</p>
        </div>
    </div>

    <!-- 🔸 Sección 2: Productos con stock bajo -->
    <h3 class="text-xl font-bold mb-4 text-slate-900 dark:text-white">Productos con Stock Bajo</h3>
    <div class="overflow-hidden rounded-lg border border-primary/20 dark:border-primary/30 bg-white dark:bg-background-dark mb-10">
        <asp:GridView ID="gvStockBajo" runat="server" AutoGenerateColumns="False"
            CssClass="min-w-full text-left border-collapse"
            GridLines="None">
            <Columns>
                <asp:BoundField DataField="Codigo" HeaderText="Código" />
                <asp:BoundField DataField="Producto" HeaderText="Producto" />
                <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
            </Columns>
        </asp:GridView>
    </div>

    <!-- 🔸 Sección 3: Últimas ventas -->
    <h3 class="text-xl font-bold mb-4 text-slate-900 dark:text-white">Últimas Ventas</h3>
    <div class="overflow-hidden rounded-lg border border-primary/20 dark:border-primary/30 bg-white dark:bg-background-dark">
        <asp:GridView ID="gvUltimasVentas" runat="server" AutoGenerateColumns="False"
            CssClass="min-w-full text-left border-collapse"
            GridLines="None">
            <Columns>
                <asp:BoundField DataField="IdVenta" HeaderText="ID Venta" />
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="Total" HeaderText="Total ($)" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>






