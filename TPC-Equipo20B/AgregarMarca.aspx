<%@ Page Title="Agregar Marca" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarMarca.aspx.cs" Inherits="TPC_Equipo20B.AgregarMarca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-8">
        <h2 class="text-3xl font-bold text-slate-900 dark:text-white">Agregar Nueva Marca</h2>
        <p class="text-sm text-primary mt-1">Ingrese el nombre de la marca</p>
    </div>

    <div class="bg-white dark:bg-background-dark rounded-xl p-8 border border-border-light dark:border-border-dark shadow-sm">
        <div class="mb-6">
            <label class="block text-sm font-medium mb-2" for="txtNombre">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server"
                CssClass="w-full rounded-lg border border-border-light dark:border-border-dark p-3 bg-white dark:bg-background-dark text-slate-800 dark:text-slate-200 focus:ring-2 focus:ring-primary/40" />
        </div>

        <div class="flex justify-end gap-4 border-t border-border-light dark:border-border-dark pt-6">
            <asp:Button ID="btnCancelar" runat="server"
                Text="Cancelar"
                CssClass="px-6 py-2 bg-gray-200 dark:bg-gray-700 text-slate-900 dark:text-white rounded-lg font-semibold hover:bg-gray-300 dark:hover:bg-gray-600 transition"
                OnClick="btnCancelar_Click" />

            <asp:Button ID="btnGuardar" runat="server"
                Text="Guardar Marca"
                CssClass="px-6 py-2 bg-primary text-white rounded-lg font-semibold hover:bg-primary/90 transition"
                OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>

