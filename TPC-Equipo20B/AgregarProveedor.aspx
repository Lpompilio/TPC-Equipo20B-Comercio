<%@ Page Title="Agregar Proveedor" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AgregarProveedor.aspx.cs" Inherits="TPC_Equipo20B.AgregarProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- 🟢 Encabezado -->
    <div class="mb-8">
        <h2 class="text-3xl font-bold text-slate-900 dark:text-white">Agregar Nuevo Proveedor</h2>
        <p class="text-sm text-primary mt-1">Complete los datos del proveedor</p>
    </div>

    <!-- 📋 Formulario de alta -->
    <div class="bg-white dark:bg-background-dark rounded-xl p-8 border border-border-light dark:border-border-dark shadow-sm">
        <div class="grid grid-cols-1 md:grid-cols-2 gap-x-8 gap-y-6">

            <!-- Nombre -->
            <div>
                <label class="block text-sm font-medium mb-2" for="txtNombre">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server"
                    CssClass="w-full rounded-lg border border-border-light dark:border-border-dark p-3 bg-white dark:bg-background-dark text-slate-800 dark:text-slate-200 focus:ring-2 focus:ring-primary/40" />
            </div>

            <!-- Razón Social -->
            <div>
                <label class="block text-sm font-medium mb-2" for="txtRazonSocial">Razón Social</label>
                <asp:TextBox ID="txtRazonSocial" runat="server"
                    CssClass="w-full rounded-lg border border-border-light dark:border-border-dark p-3 bg-white dark:bg-background-dark text-slate-800 dark:text-slate-200 focus:ring-2 focus:ring-primary/40" />
            </div>

            <!-- Documento / CUIT -->
            <div>
                <label class="block text-sm font-medium mb-2" for="txtDocumento">Documento / CUIT</label>
                <asp:TextBox ID="txtDocumento" runat="server"
                    CssClass="w-full rounded-lg border border-border-light dark:border-border-dark p-3 bg-white dark:bg-background-dark text-slate-800 dark:text-slate-200 focus:ring-2 focus:ring-primary/40" />
            </div>

            <!-- Email -->
            <div>
                <label class="block text-sm font-medium mb-2" for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"
                    CssClass="w-full rounded-lg border border-border-light dark:border-border-dark p-3 bg-white dark:bg-background-dark text-slate-800 dark:text-slate-200 focus:ring-2 focus:ring-primary/40" />
            </div>

            <!-- Teléfono -->
            <div>
                <label class="block text-sm font-medium mb-2" for="txtTelefono">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server"
                    CssClass="w-full rounded-lg border border-border-light dark:border-border-dark p-3 bg-white dark:bg-background-dark text-slate-800 dark:text-slate-200 focus:ring-2 focus:ring-primary/40" />
            </div>

            <!-- Dirección -->
            <div>
                <label class="block text-sm font-medium mb-2" for="txtDireccion">Dirección</label>
                <asp:TextBox ID="txtDireccion" runat="server"
                    CssClass="w-full rounded-lg border border-border-light dark:border-border-dark p-3 bg-white dark:bg-background-dark text-slate-800 dark:text-slate-200 focus:ring-2 focus:ring-primary/40" />
            </div>

            <!-- Localidad -->
            <div>
                <label class="block text-sm font-medium mb-2" for="txtLocalidad">Localidad</label>
                <asp:TextBox ID="txtLocalidad" runat="server"
                    CssClass="w-full rounded-lg border border-border-light dark:border-border-dark p-3 bg-white dark:bg-background-dark text-slate-800 dark:text-slate-200 focus:ring-2 focus:ring-primary/40" />
            </div>

            <!-- Condición IVA -->
            <div>
                <label class="block text-sm font-medium mb-2" for="ddlCondicionIVA">Condición IVA</label>
                <asp:DropDownList ID="ddlCondicionIVA" runat="server"
                    CssClass="w-full rounded-lg border border-border-light dark:border-border-dark p-3 bg-white dark:bg-background-dark text-slate-800 dark:text-slate-200 focus:ring-2 focus:ring-primary/40">
                    <asp:ListItem Text="Responsable Inscripto" />
                    <asp:ListItem Text="Monotributista" />
                    <asp:ListItem Text="Consumidor Final" />
                    <asp:ListItem Text="Exento" />
                </asp:DropDownList>
            </div>
        </div>

        <!-- 🔘 Botones -->
        <div class="flex justify-end gap-4 mt-8 border-t border-border-light dark:border-border-dark pt-6">
            <asp:Button ID="btnCancelar" runat="server"
                Text="Cancelar"
                CssClass="px-6 py-2 bg-gray-200 dark:bg-gray-700 text-slate-900 dark:text-white rounded-lg font-semibold hover:bg-gray-300 dark:hover:bg-gray-600 transition" />

            <asp:Button ID="btnGuardar" runat="server"
                Text="Guardar Proveedor"
                CssClass="px-6 py-2 bg-primary text-white rounded-lg font-semibold hover:bg-primary/90 transition"
                OnClick="btnGuardar_Click" />
        </div>
    </div>

</asp:Content>

