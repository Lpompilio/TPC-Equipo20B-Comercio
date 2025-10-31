<%@ Page Title="Categorías" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="TPC_Equipo20B.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- 🟢 Encabezado -->
    <div class="flex justify-between items-center mb-6">
        <h2 class="text-3xl font-bold text-slate-900 dark:text-white">Gestión de Categorías</h2>
        <asp:Button ID="btnAgregarCategoria" runat="server"
            Text="Agregar Categoría"
            CssClass="flex items-center gap-2 px-4 py-2 rounded-lg bg-primary text-background-dark font-semibold hover:bg-primary/90 transition"
            OnClick="btnAgregarCategoria_Click" />
    </div>

    <!-- 🔍 Barra de búsqueda -->
    <div class="flex flex-col md:flex-row gap-4 mb-6">
        <div class="flex-grow">
            <div class="relative">
                <span class="material-symbols-outlined absolute left-3 top-2.5 text-slate-500 dark:text-slate-400">search</span>
                <asp:TextBox ID="txtBuscar" runat="server" placeholder="Buscar categoría..."
                    CssClass="w-full pl-10 pr-4 py-2 border border-border-light dark:border-border-dark rounded-lg bg-white dark:bg-background-dark text-slate-700 dark:text-slate-200 focus:outline-none focus:ring-2 focus:ring-primary/50" />
            </div>
        </div>
    </div>

    <!-- 📋 Tabla de categorías -->
    <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="False"
        CssClass="w-full border border-border-light dark:border-border-dark rounded-lg overflow-hidden bg-white dark:bg-background-dark">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        </Columns>
    </asp:GridView>

</asp:Content>

