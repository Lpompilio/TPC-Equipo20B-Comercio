<%@ Page Title="Gestión de Clientes" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="TPC_Equipo20B.Clientes" %>

<asp:Content ID="HeadContentCli" ContentPlaceHolderID="HeadContent" runat="server" />
<asp:Content ID="MainContentCli" ContentPlaceHolderID="MainContent" runat="server">

  <div class="d-flex align-items-center justify-content-between mb-3">
    <h2 class="page-title m-0">Gestión de Clientes</h2>
    <asp:HyperLink ID="lnkAgregar" runat="server" NavigateUrl="~/AgregarCliente.aspx"
      CssClass="btn btn-brand">+ Agregar Cliente</asp:HyperLink>
  </div>

  <div class="toolbar d-flex gap-2 mb-3">
    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar cliente por nombre o documento…" />
    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar"
      OnClick="btnBuscar_Click" UseSubmitBehavior="false" />
  </div>

  <div class="grid">
    <asp:GridView ID="gvClientes" runat="server"
      CssClass="table table-hover align-middle"
      AutoGenerateColumns="False"
      AllowPaging="true" PageSize="12"
      OnPageIndexChanging="gvClientes_PageIndexChanging"
        OnRowDataBound="gvClientes_RowDataBound">
      <Columns>
        <asp:BoundField DataField="Nombre" HeaderText="Cliente" />
        <asp:BoundField DataField="Documento" HeaderText="Documento" />
        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
        <asp:BoundField DataField="NombreVendedor" HeaderText="Vendedor" />
<asp:TemplateField HeaderText="Acciones">
  <ItemStyle CssClass="action-col text-center" />
  <ItemTemplate>

    <asp:HyperLink ID="lnkEditar" runat="server"
      CssClass="btn btn-primary btn-action-sm me-1"
      NavigateUrl='<%# Eval("Id", "~/AgregarCliente.aspx?id={0}") %>'>
      Editar
    </asp:HyperLink>

    <asp:HyperLink ID="lnkEliminar" runat="server"
      CssClass="btn btn-danger btn-action-sm"
      NavigateUrl='<%# Eval("Id", "~/ConfirmarEliminar.aspx?tipo=Cliente&id={0}") %>'>
      Eliminar
    </asp:HyperLink>

  </ItemTemplate>
</asp:TemplateField>

      </Columns>
    </asp:GridView>
  </div>

</asp:Content>
