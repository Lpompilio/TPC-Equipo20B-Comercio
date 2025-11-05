<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="TPC_Equipo20B.Reportes" %>

<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="fw-bold mb-3">Reportes</h2>

    <div class="row g-3 mb-4">
        <div class="col-md-3">
            <div class="card border-success shadow-sm">
                <div class="card-body">
                    <p class="text-muted mb-1">Ventas del Mes</p>
                    <asp:Label ID="lblRptVentasMes" runat="server" CssClass="h4 fw-bold d-block"></asp:Label>
                    <small class="text-success">+12.5%</small>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card border-success shadow-sm">
                <div class="card-body">
                    <p class="text-muted mb-1">Pedidos Completados</p>
                    <asp:Label ID="lblRptPedidos" runat="server" CssClass="h4 fw-bold d-block"></asp:Label>
                    <small class="text-success">+6</small>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card border-success shadow-sm">
                <div class="card-body">
                    <p class="text-muted mb-1">Clientes Nuevos</p>
                    <asp:Label ID="lblRptClientes" runat="server" CssClass="h4 fw-bold d-block"></asp:Label>
                    <small class="text-success">+3</small>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card border-success shadow-sm">
                <div class="card-body">
                    <p class="text-muted mb-1">Ticket Promedio</p>
                    <asp:Label ID="lblRptTicket" runat="server" CssClass="h4 fw-bold d-block"></asp:Label>
                    <small class="text-success">+2.1%</small>
                </div>
            </div>
        </div>
    </div>

    <div class="card shadow-sm">
        <div class="card-header bg-white">
            <h5 class="mb-0">Top Productos Vendidos</h5>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvTopProductos" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-hover mb-0" GridLines="None">
                <HeaderStyle CssClass="table-light" />
                <Columns>
                    <asp:BoundField DataField="Producto" HeaderText="Producto" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                    <asp:BoundField DataField="Unidades" HeaderText="Unidades" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Ingresos" HeaderText="Ingresos" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
