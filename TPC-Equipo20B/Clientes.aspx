<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="TPC_Equipo20B.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- 🟢 Encabezado -->
    <div class="flex justify-between items-center mb-6">
        <h2 class="text-3xl font-bold text-slate-900 dark:text-white">Gestión de Clientes</h2>

        <asp:Button ID="btnAgregarCliente" runat="server"
            Text="Agregar Cliente"
            CssClass="flex items-center gap-2 px-4 py-2 rounded-lg bg-primary text-background-dark font-semibold hover:bg-primary/90 transition"
            OnClick="btnAgregarCliente_Click" />
    </div>

    <!-- 🔍 Barra de búsqueda -->
    <div class="flex flex-col md:flex-row gap-4 mb-6">
        <div class="flex-grow">
            <div class="relative">
                <span class="material-symbols-outlined absolute left-3 top-2.5 text-slate-500 dark:text-slate-400">search</span>
                <asp:TextBox ID="txtBuscarCliente" runat="server"
                    CssClass="w-full pl-10 pr-4 py-2 border border-border-light dark:border-border-dark rounded-lg bg-white dark:bg-background-dark text-slate-700 dark:text-slate-200 focus:outline-none focus:ring-2 focus:ring-primary/50"
                    placeholder="Buscar cliente por nombre o documento..." />
            </div>
        </div>
        <asp:Button ID="btnBuscarCliente" runat="server"
            Text="Buscar"
            CssClass="px-6 py-2 bg-primary text-background-dark font-semibold rounded-lg hover:bg-primary/90 transition"
            OnClick="btnBuscarCliente_Click" />
    </div>

    <!-- 📋 Tabla de clientes -->
    <div class="overflow-hidden rounded-lg border border-border-light dark:border-border-dark bg-white dark:bg-background-dark">
        <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False"
            CssClass="min-w-full text-left text-sm text-slate-700 dark:text-slate-200 border-collapse w-full"
            GridLines="None">
            <HeaderStyle CssClass="bg-background-light dark:bg-black/20 text-slate-600 dark:text-slate-300 font-semibold" />
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

</asp:Content>

