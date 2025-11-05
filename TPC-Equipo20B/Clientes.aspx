<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="TPC_Equipo20B.Clientes" %>

<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Encabezado -->
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="fw-bold mb-0">Gestión de Clientes</h2>
        <asp:Button ID="btnAgregarCliente" runat="server" Text="Agregar Cliente"
            CssClass="btn btn-success" OnClick="btnAgregarCliente_Click" />
    </div>

    <!-- Buscador -->
    <div class="card mb-4 shadow-sm">
        <div class="card-body">
            <div class="row g-2">
                <div class="col-12 col-md-10">
                    <div class="input-group">
                        <span class="input-group-text"><i class="bi bi-search"></i></span>
                        <asp:TextBox ID="txtBuscarCliente" runat="server"
                            CssClass="form-control" placeholder="Buscar cliente por nombre o documento..." />
                    </div>
                </div>
                <div class="col-12 col-md-2 d-grid">
                    <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar"
                        CssClass="btn btn-primary" OnClick="btnBuscarCliente_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Tabla -->
    <div class="card shadow-sm">
        <div class="table-responsive">
            <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False"
                CssClass="table table-hover align-middle mb-0" GridLines="None">
                <HeaderStyle CssClass="table-light" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Documento" HeaderText="DNI / CUIT" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
