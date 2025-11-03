<%@ Page Title="Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductoEditar.aspx.cs" Inherits="TPC_Equipo20B.ProductoEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="fw-bold mb-3"><asp:Literal ID="litTitulo" runat="server" Text="Agregar Nuevo Producto"></asp:Literal></h2>

    <div class="card shadow-sm">
        <div class="card-body">
            <div class="row g-3">
                <div class="col-md-6">
                    <label class="form-label">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6">
                    <label class="form-label">Código SKU</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="form-label">Stock Mínimo</label>
                    <asp:TextBox ID="txtStockMinimo" runat="server" TextMode="Number" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="form-label">Stock Actual</label>
                    <asp:TextBox ID="txtStockActual" runat="server" TextMode="Number" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="form-label">% Ganancia</label>
                    <asp:TextBox ID="txtGanancia" runat="server" TextMode="Number" CssClass="form-control" />
                </div>
                <div class="col-md-12">
                    <label class="form-label">URL Imagen</label>
                    <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-12 form-check mt-2">
                    <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input" Checked="true" />
                    <label class="form-check-label">Producto activo</label>
                </div>
            </div>
        </div>
        <div class="card-footer d-flex justify-content-end gap-2">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" OnClick="btnCancelar_Click" />
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>
