<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TPC_Equipo20B.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Título principal -->
    <div class="mb-4">
        <h2 class="fw-bold text-dark">Panel de Control</h2>
        <p class="text-success small mb-0">Resumen general del negocio</p>
    </div>

    <!-- Sección 1: Resumen general -->
    <div class="row mb-4">
        <div class="col-md-4 mb-3">
            <div class="card shadow-sm border-success">
                <div class="card-body">
                    <p class="mb-1 text-muted">Ventas del Mes</p>
                    <asp:Label ID="lblVentasMes" runat="server" CssClass="h3 fw-bold d-block text-dark"></asp:Label>
                    <p class="small text-success mb-0">+12.5% respecto al mes anterior</p>
                </div>
            </div>
        </div>

        <div class="col-md-4 mb-3">
            <div class="card shadow-sm border-success">
                <div class="card-body">
                    <p class="mb-1 text-muted">Pedidos Completados</p>
                    <asp:Label ID="lblPedidosCompletados" runat="server" CssClass="h3 fw-bold d-block text-dark"></asp:Label>
                    <p class="small text-success mb-0">+6 esta semana</p>
                </div>
            </div>
        </div>

        <div class="col-md-4 mb-3">
            <div class="card shadow-sm border-success">
                <div class="card-body">
                    <p class="mb-1 text-muted">Clientes Nuevos</p>
                    <asp:Label ID="lblClientesNuevos" runat="server" CssClass="h3 fw-bold d-block text-dark"></asp:Label>
                    <p class="small text-success mb-0">+3 desde ayer</p>
                </div>
            </div>
        </div>
    </div>

    <!-- Sección 2: Productos con stock bajo -->
    <h4 class="fw-bold text-dark mb-3">Productos con Stock Bajo</h4>
    <div class="card shadow-sm mb-5">
        <div class="card-body p-0">
            <asp:GridView ID="gvStockBajo" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-hover mb-0" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                    <asp:BoundField DataField="Producto" HeaderText="Producto" />
                    <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                    <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- Sección 3: Últimas ventas -->
    <h4 class="fw-bold text-dark mb-3">Últimas Ventas</h4>
    <div class="card shadow-sm">
        <div class="card-body p-0">
            <asp:GridView ID="gvUltimasVentas" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-hover mb-0" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="IdVenta" HeaderText="ID Venta" />
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="Total" HeaderText="Total ($)" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>






