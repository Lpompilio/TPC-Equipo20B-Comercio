<%@ Page Title="Agregar Venta" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarVenta.aspx.cs" Inherits="TPC_Equipo20B.AgregarVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .validator {
            color: red;
            font-size: 12px;
        }

        .error-flotante {
            position: absolute;
            font-size: 0.8rem;
            margin-top: 2px;
        }

        .sin-flechas::-webkit-outer-spin-button,
        .sin-flechas::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
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

                <div class="col-md-6 position-relative">
                    <label for="ddlCliente" class="form-label">Cliente</label>
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="rfvCliente" runat="server"
                        ControlToValidate="ddlCliente"
                        ErrorMessage="Seleccione un cliente"
                        InitialValue="0"
                        ValidationGroup="AgregarLinea"
                        CssClass="text-danger error-flotante"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-md-3 position-relative">
                    <label for="ddlMetodoPago" class="form-label">Método de Pago</label>
                    <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select">
                        <asp:ListItem Text="-- Seleccione --" Value="0" />
                        <asp:ListItem Text="Efectivo" Value="Efectivo" />
                        <asp:ListItem Text="Tarjeta" Value="Tarjeta" />
                        <asp:ListItem Text="Transferencia" Value="Transferencia" />
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="rfvMetodoPago" runat="server"
                        ControlToValidate="ddlMetodoPago"
                        ErrorMessage="Seleccione método"
                        InitialValue="0"
                        ValidationGroup="AgregarLinea"
                        CssClass="text-danger error-flotante"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-md-3">
                    <label for="txtFecha" class="form-label">Fecha</label>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>

            <!-- Detalle de la venta -->
            <h5 class="fw-bold mb-3">Detalle de la Venta</h5>

            <div class="row g-3 align-items-end mb-3">

                <div class="col-md-5 position-relative">
                    <label for="ddlProducto" class="form-label">Producto</label>
                    <asp:DropDownList ID="ddlProducto" runat="server"
                        CssClass="form-select"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="rfvProducto" runat="server"
                        ControlToValidate="ddlProducto"
                        ErrorMessage="Seleccione un producto"
                        InitialValue="0"
                        ValidationGroup="AgregarLinea"
                        CssClass="text-danger error-flotante"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-md-2 position-relative">
                    <label for="txtCantidad" class="form-label">Cantidad</label>

                    <asp:TextBox ID="txtCantidad" runat="server"
                        CssClass="form-control sin-flechas"
                        TextMode="Number"
                        min="1" />

                    <asp:RequiredFieldValidator ID="rfvCantidad" runat="server"
                        ControlToValidate="txtCantidad"
                        ErrorMessage="Ingresar cantidad"
                        ValidationGroup="AgregarLinea"
                        CssClass="text-danger error-flotante"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>

                    <asp:CompareValidator ID="cvCantidad" runat="server"
                        ControlToValidate="txtCantidad"
                        ErrorMessage="Debe ser mayor a 0"
                        ValueToCompare="0"
                        Operator="GreaterThan"
                        Type="Currency"
                        ValidationGroup="AgregarLinea"
                        CssClass="text-danger error-flotante"
                        Display="Dynamic">
                    </asp:CompareValidator>

                    <asp:CustomValidator ID="CustomValidatorCantidad" runat="server"
                        ControlToValidate="txtCantidad"
                        ErrorMessage="Stock insuficiente"
                        CssClass="text-danger error-flotante"
                        Display="Dynamic"
                        OnServerValidate="ValidarCantidad_ServerValidate"
                        ValidationGroup="AgregarLinea"
                        EnableClientScript="false">
                    </asp:CustomValidator>
                </div>

                <div class="col-md-3">
                    <label for="txtPrecio" class="form-label">Precio Unitario</label>
                    <asp:TextBox ID="txtPrecio" runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />
                </div>

                <div class="col-md-2">
                    <asp:Label ID="lblErrorStock" runat="server" CssClass="text-danger fw-bold small d-block mb-1" />
                    <asp:Button ID="btnAgregarLinea" runat="server"
                        Text="Agregar"
                        CssClass="btn btn-success w-100"
                        ValidationGroup="AgregarLinea"
                        OnClick="btnAgregarLinea_Click"
                        CausesValidation="true" />
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
                                CssClass="btn btn-sm btn-danger"
                                CausesValidation="false">
                                <i class="bi bi-trash"></i> Quitar
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="text-end mt-3">
                <h5>Total:
                    <asp:Label ID="lblTotal" runat="server"
                        CssClass="fw-bold text-success" Text="$0.00"></asp:Label></h5>
            </div>
        </div>

        <!-- Footer botones -->
        <div class="card-footer pb-3">
            <div class="d-flex justify-content-end gap-2">
                <asp:Button ID="btnCancelar" runat="server"
                    Text="Cancelar"
                    CssClass="btn btn-outline-secondary"
                    OnClick="btnCancelar_Click"
                    CausesValidation="false"
                    UseSubmitBehavior="false" />

                <asp:Button ID="btnGuardar" runat="server"
                    Text="Guardar Venta"
                    CssClass="btn btn-success"
                    OnClick="btnGuardar_Click"
                    CausesValidation="false" />
            </div>

            <div class="mt-2 text-end">
                <asp:Label ID="lblMensajeFooter" runat="server"
                    CssClass="text-danger fw-bold small"
                    Visible="false">
        </asp:Label>
            </div>
        </div>
</asp:Content>
