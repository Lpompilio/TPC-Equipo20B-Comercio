<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="TPC_Equipo20B.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="fw-bold">Gestión de Marcas</h2>
        <asp:Button ID="btnAgregarMarca" runat="server"
            Text="➕ Agregar Marca"
            CssClass="btn btn-success"
            OnClick="btnAgregarMarca_Click" />
    </div>

    <!-- Buscador -->
    <div class="mb-3">
        <div class="input-group">
            <span class="input-group-text">
                <i class="bi bi-search"></i>
            </span>
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar marca..." />
        </div>
    </div>

    <!-- Grilla -->
    <asp:GridView ID="gvMarcas" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-hover align-middle"
        DataKeyNames="Id"
        OnRowCommand="gvMarcas_RowCommand">

        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Marca" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton ID="cmdEditar" runat="server"
                        CommandName="Editar"
                        CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-sm btn-primary me-2">
                        <i class="bi bi-pencil"></i> Editar
                    </asp:LinkButton>

                    <asp:LinkButton ID="cmdEliminar" runat="server"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-sm btn-danger">
                        <i class="bi bi-trash"></i> Eliminar
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

