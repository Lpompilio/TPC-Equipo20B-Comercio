<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="TPC_Equipo20B.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flex justify-between items-center mb-6">
        <h2 class="text-3xl font-bold text-slate-900 dark:text-white">Gestión de Marcas</h2>
        <asp:Button ID="btnAgregarMarca" runat="server"
            Text="Agregar Marca"
            CssClass="flex items-center gap-2 px-4 py-2 rounded-lg bg-primary text-background-dark font-semibold hover:bg-primary/90 transition"
            OnClick="btnAgregarMarca_Click" />
    </div>

    <div class="flex flex-col md:flex-row gap-4 mb-6">
        <div class="flex-grow">
            <div class="relative">
                <span class="material-symbols-outlined absolute left-3 top-2.5 text-slate-500 dark:text-slate-400">search</span>
                <asp:TextBox ID="txtBuscar" runat="server" placeholder="Buscar marca..."
                    CssClass="w-full pl-10 pr-4 py-2 border border-border-light dark:border-border-dark rounded-lg bg-white dark:bg-background-dark text-slate-700 dark:text-slate-200 focus:outline-none focus:ring-2 focus:ring-primary/50" />
            </div>
        </div>
    </div>

    <asp:GridView ID="gvMarcas" runat="server" AutoGenerateColumns="False"
        CssClass="w-full border border-border-light dark:border-border-dark rounded-lg overflow-hidden bg-white dark:bg-background-dark">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        </Columns>
    </asp:GridView>
</asp:Content>
