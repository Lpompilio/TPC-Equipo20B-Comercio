<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs"
    Inherits="TPC_Equipo20B.Ventas" MasterPageFile="~/Site.Master" %>

<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Título -->
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="fw-bold mb-0">Informes de Ventas</h2>
        <asp:Button ID="btnNuevaVenta" runat="server" Text="➕ Nueva Venta"
            CssClass="btn btn-success" OnClick="btnNuevaVenta_Click" />
    </div>

    <!-- Filtros -->
    <div class="card shadow-sm mb-4">
        <div class="card-header bg-white">
            <h5 class="mb-0">Filtros</h5>
        </div>
        <div class="card-body">
            <div class="row g-3">
                <div class="col-12 col-md-6">
                    <label class="form-label">Rango de Fechas</label>
                    <div class="row g-2">
                        <div class="col">
                            <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date"
                                CssClass="form-control" placeholder="Desde" />
                        </div>
                        <div class="col">
                            <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date"
                                CssClass="form-control" placeholder="Hasta" />
                        </div>
                    </div>
                </div>

                <div class="col-12 col-md-4">
                    <label class="form-label">Producto o Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Todas las categorías" Value="0" />
                        <asp:ListItem Text="Aguas" Value="1" />
                        <asp:ListItem Text="Bebidas" Value="2" />
                        <asp:ListItem Text="Snacks" Value="3" />
                    </asp:DropDownList>
                </div>

                <div class="col-12 col-md-2 d-grid d-md-flex align-items-end">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Aplicar Filtros"
                        CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Resultados -->
    <div class="card shadow-sm">
        <div class="card-header bg-white d-flex justify-content-between align-items-center">
            <h5 class="mb-0">Detalle de Ventas</h5>
            <asp:Button ID="btnExportar" runat="server" Text="⬇️ Exportar CSV"
                CssClass="btn btn-outline-secondary" OnClick="btnExportar_Click" />
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gvVentas" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-hover mb-0" GridLines="None">
                <HeaderStyle CssClass="table-light" />
                <Columns>
                    <asp:BoundField DataField="Producto" HeaderText="Producto" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                    <asp:BoundField DataField="UnidadesVendidas" HeaderText="Unidades Vendidas"
                        DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario"
                        DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Total" HeaderText="Ingresos Totales"
                        DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="card-footer text-muted small">
            Mostrando 1-4 de 25 resultados
        </div>
    </div>

</asp:Content>
