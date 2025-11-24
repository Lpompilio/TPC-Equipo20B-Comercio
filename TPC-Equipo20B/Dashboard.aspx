<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TPC_Equipo20B.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="mb-4">
        <h2 class="fw-bold text-dark">Panel de Control</h2>
        <p class="text-success small mb-0">Resumen general del negocio</p>
    </div>

    <div class="row mb-4">
        <div class="col-md-4 mb-3">
            <div class="card shadow-sm border-success">
                <div class="card-body">
                    <p class="mb-1 text-muted">Ventas del Mes</p>
                    <asp:Label ID="lblVentasMes" runat="server" CssClass="h3 fw-bold d-block text-dark"></asp:Label>
                    <p class="small text-success mb-0">Ventas confirmadas del mes actual</p>
                </div>
            </div>
        </div>

        <div class="col-md-4 mb-3">
            <div class="card shadow-sm border-success">
                <div class="card-body">
                    <p class="mb-1 text-muted">Pedidos Completados</p>
                    <asp:Label ID="lblPedidosCompletados" runat="server" CssClass="h3 fw-bold d-block text-dark"></asp:Label>
                    <p class="small text-success mb-0">Ventas no canceladas en el mes</p>
                </div>
            </div>
        </div>

        <div class="col-md-4 mb-3">
            <div class="card shadow-sm border-success">
                <div class="card-body">
                    <p class="mb-1 text-muted">Clientes Nuevos</p>
                    <asp:Label ID="lblClientesNuevos" runat="server" CssClass="h3 fw-bold d-block text-dark"></asp:Label>
                    <p class="small text-success mb-0">Clientes con compras en el mes</p>
                </div>
            </div>
        </div>
    </div>

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

    <h4 class="fw-bold text-dark mb-3">Últimas Ventas</h4>
    <div class="card shadow-sm">
        <div class="card-body p-0">
            <asp:GridView ID="gvUltimasVentas" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-hover mb-0" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="IdVenta" HeaderText="ID Venta" />
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="Total" HeaderText="Total ($)" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Vendedor" HeaderText="Vendedor" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
