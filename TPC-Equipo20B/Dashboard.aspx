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
                CssClass="table table-striped table-hover mb-0 align-middle" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="Código">
                        <HeaderStyle CssClass="text-center" />
                        <ItemStyle CssClass="text-center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Producto" HeaderText="Producto">
                        <HeaderStyle CssClass="text-center" />
                        <ItemStyle CssClass="text-center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="StockActual" HeaderText="Stock Actual">
                        <HeaderStyle CssClass="text-center" />
                        <ItemStyle CssClass="text-center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo">
                        <HeaderStyle CssClass="text-center" />
                        <ItemStyle CssClass="text-center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <h4 class="fw-bold text-dark mb-3">Últimas Ventas</h4>
    <div class="card shadow-sm">
        <div class="card-body p-0">
            <asp:GridView ID="gvUltimasVentas" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-hover mb-0 align-middle"
                GridLines="None">
                <Columns>

                    <asp:BoundField DataField="IdVenta" HeaderText="N° Venta">
                        <HeaderStyle CssClass="text-center" />
                        <ItemStyle CssClass="text-center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Cliente" HeaderText="Cliente">
                        <HeaderStyle CssClass="text-center" />
                        <ItemStyle CssClass="text-center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Fecha" HeaderText="Fecha">
                        <HeaderStyle CssClass="text-center" />
                        <ItemStyle CssClass="text-center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Total" HeaderText="Total ($)" DataFormatString="{0:C}">
                        <HeaderStyle CssClass="text-center" />
                        <ItemStyle CssClass="text-center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Vendedor" HeaderText="Vendedor">
                        <HeaderStyle CssClass="text-center" />
                        <ItemStyle CssClass="text-center" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Estado">
                        <HeaderStyle CssClass="text-center" />
                        <ItemStyle CssClass="text-center" />
                        <ItemTemplate>
                            <%# (bool)Eval("Cancelada")
                                ? "<span class='badge bg-danger'>Cancelada</span>"
                                : "<span class='badge bg-success'>Activa</span>" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
