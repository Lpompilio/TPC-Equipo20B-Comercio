<%@ Page Title="Agregar Marca" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarMarca.aspx.cs" Inherits="TPC_Equipo20B.AgregarMarca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-4">
        <h2 id="lblTitulo" runat="server" class="fw-bold">Agregar Nueva Marca</h2>
        <p class="text-muted">Ingrese el nombre de la marca</p>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>
        </div>

        <div class="card-footer d-flex justify-content-end gap-2">
            <asp:Button ID="btnCancelar" runat="server"
                Text="Cancelar"
                CssClass="btn btn-outline-secondary"
                OnClick="btnCancelar_Click" />

            <asp:Button ID="btnGuardar" runat="server"
                Text="Guardar Marca"
                CssClass="btn btn-success"
                OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>

