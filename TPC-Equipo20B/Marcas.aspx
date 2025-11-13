<%@ Page Title="Gestión de Marcas" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="TPC_Equipo20B.Marcas" %>

<asp:Content ID="HeadContentMarcas" ContentPlaceHolderID="HeadContent" runat="server" />
<asp:Content ID="MainContentMarcas" ContentPlaceHolderID="MainContent" runat="server">

  <div class="d-flex align-items-center justify-content-between mb-3">
    <h2 class="page-title m-0">Gestión de Marcas</h2>
    <asp:HyperLink ID="lnkAgregar" runat="server" NavigateUrl="~/AgregarMarca.aspx"
      CssClass="btn btn-brand">+ Agregar Marca</asp:HyperLink>
  </div>

  <div class="toolbar d-flex gap-2 mb-3">
    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar…" />
    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar"
      OnClick="btnBuscar_Click" UseSubmitBehavior="false" />
  </div>

  <div class="grid">
    <asp:GridView ID="gvMarcas" runat="server"
      CssClass="table table-hover align-middle"
      AutoGenerateColumns="False"
      AllowPaging="true" PageSize="12"
      DataKeyNames="Id"
      OnPageIndexChanging="gvMarcas_PageIndexChanging">
      <Columns>
        <asp:BoundField DataField="Nombre" HeaderText="Marca" />
        <asp:TemplateField HeaderText="Acciones">
          <ItemStyle CssClass="action-col text-center" />
          <ItemTemplate>
            <asp:HyperLink runat="server" CssClass="btn btn-primary btn-action-sm me-1"
              NavigateUrl='<%# Eval("Id", "~/AgregarMarca.aspx?id={0}") %>'>Editar</asp:HyperLink>
            <asp:HyperLink runat="server" CssClass="btn btn-danger btn-action-sm"
              NavigateUrl='<%# Eval("Id", "~/ConfirmarEliminar.aspx?tipo=Marca&id={0}") %>'>Eliminar</asp:HyperLink>
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>
  </div>

</asp:Content>
