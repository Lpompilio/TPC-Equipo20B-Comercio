<%@ Page Title="Agregar Producto" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarProducto.aspx.cs" Inherits="TPC_Equipo20B.AgregarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .validator {
            color: red;
            font-size: 12px;
        }
    </style>
    <div class="mb-4">
        <h2 id="lblTitulo" runat="server" class="fw-bold">Agregar/Editar Producto</h2>
        <p class="text-muted">Complete los campos para registrar o editar un producto</p>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">

            <!-- Información básica -->
            <div class="row g-3 mb-3">
                <div class="col-md-6">
                    <label for="txtDescripcion" class="form-label">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Ej: Agua Purificada 1L" />
                    <asp:RequiredFieldValidator ErrorMessage="Agregar descripcion" CssClass="validator" ControlToValidate="txtDescripcion" runat="server" />
                </div>

                <div class="col-md-6">
                    <label for="txtCodigo" class="form-label">Código SKU</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" placeholder="Ej: AGP-001" />
                </div>
            </div>

            <div class="row g-3 mb-3">
                <div class="col-md-4">
                    <label for="ddlMarca" class="form-label">Marca</label>
                    <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select"></asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Agregar Marca" CssClass="validator" ControlToValidate="ddlMarca" runat="server" InitialValue="0" />
                </div>

                <div class="col-md-4">
                    <label for="ddlCategoria" class="form-label">Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
                    <asp:RequiredFieldValidator
                        ErrorMessage="Debe seleccionar una categoría"
                        CssClass="validator"
                        ControlToValidate="ddlCategoria"
                        InitialValue="0"
                        runat="server" />
                </div>

                <div class="col-md-4">
                    <label for="ddlProveedor" class="form-label">Proveedor</label>
                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
            </div>

            <!-- Detalles de stock -->
            <div class="row g-3 mb-3">
                <div class="col-md-4">
                    <label for="txtStockMinimo" class="form-label">Stock Mínimo</label>
                    <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ErrorMessage="Cargar el Stock Minimo" ControlToValidate="txtStockMinimo" runat="server" CssClass="validator" />
                </div>

                <div class="col-md-4">
                    <label for="txtStockActual" class="form-label">Stock Actual</label>
                    <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ErrorMessage="Cargar el Stock Actual" ControlToValidate="txtStockActual" runat="server" CssClass="validator" />
                </div>

                <div class="col-md-4">
                    <label for="txtGanancia" class="form-label">% Ganancia</label>
                    <asp:TextBox ID="txtGanancia" runat="server" CssClass="form-control" />

                    <asp:RegularExpressionValidator
                        ID="revGanancia"
                        runat="server"
                        ControlToValidate="txtGanancia"
                        ErrorMessage="El formato debe ser numérico con coma decimal (ej: 150,25)."
                        CssClass="validator"
                        Display="Dynamic"
                        SetFocusOnError="true"
                        ValidationExpression="^\d+(,\d{1,2})?$" />
                    <asp:RequiredFieldValidator
                        ID="rfvGanancia"
                        runat="server"
                        ControlToValidate="txtGanancia"
                        ErrorMessage="El % de ganancia es requerido."
                        CssClass="validator"
                        Display="Dynamic" />
                </div>

                <!-- Imagen -->
                <div class="mb-3">
                    <label for="txtUrlImagen" class="form-label">URL de Imagen (opcional)</label>
                    <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" placeholder="https://..." />
                </div>

                <!-- Estado -->
                <div class="form-check form-switch mb-3">
                    <asp:CheckBox ID="chkActivo" runat="server" Checked="true" CssClass="form-check-input" />
                    <label class="form-check-label" for="chkActivo">Producto activo</label>
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
