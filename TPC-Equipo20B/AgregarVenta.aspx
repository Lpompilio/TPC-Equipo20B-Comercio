<%@ Page Title="Agregar Venta" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarVenta.aspx.cs" Inherits="TPC_Equipo20B.AgregarVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .validator {
            color: red;
            font-size: 12px;
        }
    </style>

    <div class="mb-4">
        <h2 class="fw-bold">Registrar Venta</h2>
        <p class="text-muted">Complete los datos para registrar una nueva venta</p>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">

            <!-- Datos principales -->
            <div class="row g-3 mb-3">
                <div class="col-md-6">
                    <label for="ddlCliente" class="form-label">Cliente</label>
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select"></asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Seleccione un cliente"
                        ControlToValidate="ddlCliente" InitialValue="0"
                        CssClass="validator" runat="server" />
                </div>

                <div class="col-md-3">
                    <label for="ddlMetodoPago" class="form-label">Método de Pago</label>
                    <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select">
                        <asp:ListItem Text="-- Seleccione --" Value="0" />
                        <asp:ListItem Text="Efectivo" Value="Efectivo" />
                        <asp:ListItem Text="Tarjeta" Value="Tarjeta" />
                        <asp:ListItem Text="Transferencia" Value="Transferencia" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label for="txtFecha" class="form-label">Fecha</label>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>

            <!-- Detalle de la venta -->
            <hr />
            <h5 class="fw-bold mb-3">Detalle de la Venta</h5>

            <div class="row g-3 align-items-end mb-3">
                <div class="col-md-5">
                    <label for="ddlProducto" class="form-label">Producto</label>
                    <asp:DropDownList ID="ddlProducto" runat="server"
                        CssClass="form-select"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label for="txtCantidad" class="form-label">Cantidad</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number" />
                </div>

                <div class="col-md-3">
                    <label for="txtPrecio" class="form-label">Precio Unitario</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>

                <div class="col-md-2 d-grid">
                    <asp:Button ID="btnAgregarLinea" runat="server"
                        Text="Agregar"
                        CssClass="btn btn-success"
                        OnClick="btnAgregarLinea_Click" />
                </div>
            </div>

            <!-- Tabla detalle -->
            <asp:GridView ID="gvLineas" runat="server" AutoGenerateColumns="False"
                CssClass="table table-sm table-hover" OnRowCommand="gvLineas_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Producto.Descripcion" HeaderText="Producto" />
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="cmdEliminar" runat="server"
                                CommandName="Eliminar"
                                CommandArgument='<%# Container.DataItemIndex %>'
                                CssClass="btn btn-sm btn-danger">
                                <i class="bi bi-trash"></i> Quitar
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="text-end mt-3">
                <h5>Total: <asp:Label ID="lblTotal" runat="server"
                    CssClass="fw-bold text-success" Text="$0.00"></asp:Label></h5>
            </div>
        </div>

        <!-- Footer botones -->
        <div class="card-footer d-flex justify-content-end gap-2">
            <asp:Button ID="btnCancelar" runat="server"
                Text="Cancelar"
                CssClass="btn btn-outline-secondary"
                OnClick="btnCancelar_Click" />

            <asp:Button ID="btnGuardar" runat="server"
                Text="Guardar Venta"
                CssClass="btn btn-success"
                OnClick="btnGuardar_Click" />
        </div>
    </div>

</asp:Content>

