<%@ Page Title="Agregar Producto" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarProducto.aspx.cs" Inherits="TPC_Equipo20B.AgregarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<style>
        .validator {
            color: red;
            font-size: 12px;
        }
    </style>--%>

    <div class="mb-4">
        <h2 id="lblTitulo" runat="server" class="fw-bold">Agregar/Editar Producto</h2>
        <p class="text-muted">Complete los campos para registrar o editar un producto</p>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <div class="row">
                <div class="col-md-8">

                    <!-- Descripción -->
                    <div class="mb-3">
                        <label for="txtDescripcion" class="form-label">Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Ej: Producto 1L/1KG" />
                        <asp:RequiredFieldValidator ErrorMessage="Agregar descripcion" CssClass="text-danger small" ControlToValidate="txtDescripcion" runat="server" />
                    </div>

                    <!-- CÓDIGO SKU -->
                    <div class="mb-3">
                        <label for="txtSKU" class="form-label">Código SKU</label>
                        <asp:TextBox ID="txtSKU" runat="server" CssClass="form-control" placeholder="Ej: SKU00187" />
                        <asp:RequiredFieldValidator
                            ID="rfvSKU"
                            runat="server"
                            ControlToValidate="txtSKU"
                            ErrorMessage="El código SKU es obligatorio."
                            CssClass="text-danger small"
                            Display="Dynamic" />
                    </div>

                    <div class="row g-3 mb-3">
                        <div class="col-md-6">
                            <label for="ddlMarca" class="form-label">Marca</label>
                            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select"></asp:DropDownList>
                            <asp:RequiredFieldValidator ErrorMessage="Agregar Marca" CssClass="text-danger small" ControlToValidate="ddlMarca" runat="server" InitialValue="0" Display="Dynamic" />
                        </div>

                        <div class="col-md-6">
                            <label for="ddlCategoria" class="form-label">Categoría</label>
                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
                            <asp:RequiredFieldValidator
                                ErrorMessage="Debe seleccionar una categoría"
                                CssClass="text-danger small"
                                ControlToValidate="ddlCategoria"
                                InitialValue="0"
                                runat="server"
                                Display="Dynamic" />
                        </div>
                    </div>

                    <div class="row g-3 mb-3">
                        <div class="col-md-4">
                            <label for="txtStockMinimo" class="form-label">Stock Mínimo</label>
                            <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="Cargar el Stock Minimo" ControlToValidate="txtStockMinimo" runat="server" CssClass="text-danger small" Display="Dynamic" />
                            <asp:RangeValidator
                                ID="rngStockMin"
                                runat="server"
                                ControlToValidate="txtStockMinimo"
                                MinimumValue="1"
                                MaximumValue="999999"
                                Type="Integer"
                                ErrorMessage="* Debe ser mayor a 0"
                                CssClass="text-danger small"
                                Display="Dynamic" />
                        </div>

                        <div class="col-md-4">
                            <label for="txtStockActual" class="form-label">Stock Actual</label>
                            <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="Cargar el Stock Actual" ControlToValidate="txtStockActual" runat="server" CssClass="text-danger small" Display="Dynamic" />
                            <asp:RangeValidator
                                ID="rngStockActual"
                                runat="server"
                                ControlToValidate="txtStockActual"
                                MinimumValue="0"
                                MaximumValue="999999"
                                Type="Integer"
                                ErrorMessage="* Debe ser 0 o mayor"
                                CssClass="text-danger small"
                                Display="Dynamic" />
                        </div>

                        <div class="col-md-4">
                            <label for="txtGanancia" class="form-label">% Ganancia</label>
                            <asp:TextBox ID="txtGanancia" runat="server" CssClass="form-control" />

                            <asp:RegularExpressionValidator
                                ID="revGanancia"
                                runat="server"
                                ControlToValidate="txtGanancia"
                                ErrorMessage="El formato debe ser numérico con coma decimal (ej: 150,25)."
                                CssClass="text-danger small"
                                Display="Dynamic"
                                ValidationExpression="^\d+(,\d{1,2})?$" />

                            <asp:RequiredFieldValidator
                                ID="rfvGanancia"
                                runat="server"
                                ControlToValidate="txtGanancia"
                                ErrorMessage="El % de ganancia es requerido."
                                CssClass="text-danger small"
                                Display="Dynamic" />
                        </div>
                    </div>

                    <!-- Switch de habilitado usando input HTML para que Bootstrap lo estilice bien -->
                    <div class="form-check form-switch mb-3">
                        <input id="chkHabilitado" runat="server" type="checkbox" class="form-check-input" checked="checked" />
                        <label class="form-check-label" for="chkHabilitado">
                            Producto habilitado
                        </label>
                    </div>

                </div>

                <div class="col-md-4">
                    <label class="form-label fw-bold">Proveedores</label>

                    <div class="input-group mb-2">
                        <asp:TextBox ID="txtBuscarProveedor" runat="server" CssClass="form-control" placeholder="Buscar proveedor..." />
                        <asp:Button ID="btnBuscarProveedor" runat="server" Text="🔍" CssClass="btn btn-outline-secondary" OnClick="btnBuscarProveedor_Click" />
                    </div>

                    <div style="max-height: 500px; overflow-y: auto; border: 1px solid #ddd; border-radius: 6px; padding: 6px;">
                        <asp:GridView ID="gvProveedores" runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-sm table-hover"
                            DataKeyNames="Id">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="35px" CssClass="text-center" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="card-footer d-flex justify-content-end gap-2">
            <asp:Button ID="btnCancelar" runat="server"
                Text="Cancelar"
                CssClass="btn btn-outline-secondary"
                OnClick="btnCancelar_Click" />

            <asp:Button ID="btnGuardar" runat="server"
                Text="Guardar Producto"
                CssClass="btn btn-success"
                OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>
