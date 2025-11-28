<%@ Page Title="Agregar Compra" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarCompra.aspx.cs" Inherits="TPC_Equipo20B.AgregarCompra" %>

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
        <h2 class="fw-bold">Registrar Compra</h2>
        <p class="text-muted">Complete los campos para registrar una nueva compra con sus productos</p>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">

            <div class="row g-3 mb-3">
                <div class="col-md-6">
                    <label for="ddlProveedor" class="form-label">Proveedor</label>
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold" Visible="false"></asp:Label>
                    <asp:DropDownList
                        ID="ddlProveedor"
                        runat="server"
                        CssClass="form-select"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Seleccione un proveedor"
                        ControlToValidate="ddlProveedor"
                        InitialValue="0"
                        runat="server"
                        CssClass="validator error-flotante"
                        ValidationGroup="AgregarLinea" />
                </div>

                <div class="col-md-3">
                    <label for="txtFecha" class="form-label">Fecha</label>
                    <asp:TextBox ID="txtFecha" runat="server"
                        CssClass="form-control"
                        TextMode="Date" />
                </div>
            </div>

            <!-- Observaciones -->
            <div class="mb-3">
                <label for="txtObservaciones" class="form-label">Observaciones (opcional)</label>
                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
            </div>

            <hr />

            <!-- Agregar líneas de productos -->
            <h5 class="fw-bold mb-3">Detalle de la compra</h5>

            <div class="row g-3 align-items-end mb-3">
                <div class="col-md-5">
                    <label for="ddlProducto" class="form-label">Producto</label>
                    <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="rfvProducto" runat="server"
                        ControlToValidate="ddlProducto"
                        ErrorMessage="Seleccionar producto"
                        InitialValue="0"
                        ValidationGroup="AgregarLinea"
                        CssClass="validator error-flotante"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-md-2 position-relative">
                    <label for="txtCantidad" class="form-label">Cantidad</label>

                    <asp:TextBox ID="txtCantidad" runat="server"
                        CssClass="form-control sin-flechas"
                        TextMode="Number"
                        min="0" step="0.01" />

                    <asp:RequiredFieldValidator ID="rfvCantidad" runat="server"
                        ControlToValidate="txtCantidad"
                        ErrorMessage="Ingresar cantidad"
                        ValidationGroup="AgregarLinea"
                        CssClass="validator error-flotante"
                        Display="Dynamic">
                     </asp:RequiredFieldValidator>

                    <asp:CompareValidator ID="cvCantidad" runat="server"
                        ControlToValidate="txtCantidad"
                        ErrorMessage="No se pueden cargar valores negativos"
                        ValueToCompare="0"
                        Operator="GreaterThan"
                        Type="Currency"
                        ValidationGroup="AgregarLinea"
                        CssClass="validator error-flotante"
                        Display="Dynamic">
                    </asp:CompareValidator>
                </div>

                <div class="col-md-3 position-relative">
                    <label for="txtPrecio" class="form-label">Precio Unitario</label>

                    <asp:TextBox ID="txtPrecio" runat="server"
                        CssClass="form-control sin-flechas"
                        TextMode="Number"
                        min="0" step="0.01" />

                    <asp:RequiredFieldValidator ID="rfvPrecio" runat="server"
                        ControlToValidate="txtPrecio"
                        ErrorMessage="Ingresar precio"
                        ValidationGroup="AgregarLinea"
                        CssClass="validator error-flotante"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>

                    <asp:CompareValidator ID="cvPrecio" runat="server"
                        ControlToValidate="txtPrecio"
                        ErrorMessage="No se pueden cargar valores negativos"
                        ValueToCompare="0"
                        Operator="GreaterThan"
                        Type="Currency"
                        ValidationGroup="AgregarLinea"
                        CssClass="validator error-flotante"
                        Display="Dynamic">
                     </asp:CompareValidator>
                </div>

                <div class="col-md-2 d-grid">
                    <asp:Button ID="btnAgregarLinea" runat="server"
                        Text="Agregar"
                        CssClass="btn btn-success"
                        ValidationGroup="AgregarLinea"
                        OnClick="btnAgregarLinea_Click" />
                </div>
            </div>

            <asp:GridView ID="gvLineas" runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-sm table-hover"
                OnRowCommand="gvLineas_RowCommand">

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


            <!-- Total -->
            <div class="text-end mt-3">
                <h5>Total:
                    <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold text-success" Text="$0.00"></asp:Label></h5>
            </div>

        </div>

        <div class="card-footer pb-3">

            <div class="d-flex justify-content-end gap-2 align-items-center">
                <asp:Button ID="btnCancelar" runat="server"
                    Text="Cancelar"
                    CssClass="btn btn-outline-secondary"
                    OnClick="btnCancelar_Click" />

                <asp:Button ID="btnGuardar" runat="server"
                    Text="Guardar Compra"
                    CssClass="btn btn-success"
                    OnClick="btnGuardar_Click"
                    CausesValidation="false" />
            </div>

            <div class="mt-2 text-end">
                <asp:Label ID="lblMensajeFooter" runat="server"
                    CssClass="text-danger fw-bold small"
                    Text=""
                    Visible="false">
                </asp:Label>
            </div>

        </div>
    </div>
</asp:Content>

