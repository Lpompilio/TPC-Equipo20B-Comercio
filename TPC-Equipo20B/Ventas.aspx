<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="TPC_Equipo20B.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="fw-bold">Gestión de Ventas</h2>
        <asp:Button ID="btnNuevaVenta" runat="server"
            Text="➕ Nueva Venta"
            CssClass="btn btn-success"
            OnClick="btnNuevaVenta_Click" />
    </div>

    <asp:GridView ID="gvVentas" runat="server" AutoGenerateColumns="False"
        CssClass="table table-hover align-middle" DataKeyNames="Id"
        OnRowCommand="gvVentas_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="N° Venta" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="Cliente.Nombre" HeaderText="Cliente" />
            <asp:BoundField DataField="MetodoPago" HeaderText="Método de Pago" />
            <asp:BoundField DataField="TotalBD" HeaderText="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton ID="cmdDetalle" runat="server"
                        CommandName="Detalle"
                        CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-sm btn-info me-2">
                        <i class="bi bi-eye"></i> Ver Detalle
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
