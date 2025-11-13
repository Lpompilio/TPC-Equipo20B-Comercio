<%@ Page Title="Gestión de Categorías" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="TPC_Equipo20B.Categorias" %>

<asp:Content ID="HeadContentCategorias" ContentPlaceHolderID="HeadContent" runat="server" />
<asp:Content ID="MainContentCategorias" ContentPlaceHolderID="MainContent" runat="server">

  <div class="d-flex align-items-center justify-content-between mb-3">
    <h2 class="page-title m-0">Gestión de Categorías</h2>
    <asp:HyperLink ID="lnkAgregar" runat="server" NavigateUrl="~/AgregarCategoria.aspx"
      CssClass="btn btn-brand">+ Agregar Categoría</asp:HyperLink>
  </div>

  <div class="toolbar d-flex gap-2 mb-3">
    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar…" />
    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar"
      OnClick="btnBuscar_Click" UseSubmitBehavior="false" />
  </div>

  <div class="grid">
    <asp:GridView ID="gvCategorias" runat="server"
      CssClass="table table-hover align-middle"
      AutoGenerateColumns="False"
      AllowPaging="true" PageSize="12"
      OnPageIndexChanging="gvCategorias_PageIndexChanging">
      <Columns>
        <asp:BoundField DataField="Nombre" HeaderText="Categoría" />
        <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-center">
          <ItemStyle CssClass="action-col text-center" />
          <ItemTemplate>
            <asp:HyperLink runat="server" CssClass="btn btn-primary btn-action-sm me-1"
              NavigateUrl='<%# Eval("Id", "~/AgregarCategoria.aspx?id={0}") %>'>Editar</asp:HyperLink>
            <asp:HyperLink runat="server" CssClass="btn btn-danger btn-action-sm"
              NavigateUrl='<%# Eval("Id", "~/ConfirmarEliminar.aspx?tipo=Categoria&id={0}") %>'>Eliminar</asp:HyperLink>
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>
  </div>

</asp:Content>
