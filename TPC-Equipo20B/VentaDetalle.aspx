<%@ Page Title="Detalle de Venta" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="VentaDetalle.aspx.cs" Inherits="TPC_Equipo20B.VentaDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function imprimirComprobante(nombreArchivo) {
            if (nombreArchivo && nombreArchivo.trim() !== "") {
                document.title = nombreArchivo;
            }
            window.print();
            return false;
        }
    </script>

    <style type="text/css">
        @media print {
            .navbar,
            .btn-cerrar,
            .btn-imprimir,
            .btn-enviar-mail,
            .no-print {
                display: none !important;
            }

            body {
                background: #ffffff !important;
            }

            .card {
                box-shadow: none !important;
                border: 1px solid #000 !important;
                width: 100% !important; /* ocupa todo el ancho al imprimir */
            }

            .card-header {
                background-color: #ffffff !important;
                color: #000 !important;
                border-bottom: 1px solid #000 !important;
            }

            .alert {
                border: 1px solid #000 !important;
            }

            /* Centrado para impresión + tamaño de fuente un poco mayor */
            #MainContent_gvLineas {
                width: 100% !important;
            }

            #MainContent_gvLineas th,
            #MainContent_gvLineas td {
                text-align: center !important;
                font-size: 1.05rem !important;
                padding: 8px 12px !important;
            }

            /* Primera columna (Producto) más ancha y a la izquierda */
            #MainContent_gvLineas th:first-child,
            #MainContent_gvLineas td:first-child {
                text-align: left !important;
                width: 60% !important;
            }
        }

        /* 📦 Remito más grande en pantalla */
        .card-remito {
            width: 900px !important;     /* antes 700px */
            max-width: 98% !important;
        }

        /* La tabla usa todo el ancho disponible */
        #MainContent_gvLineas {
            width: 100% !important;
        }

        /* ⭐ Centrar todas las columnas del Grid + mejorar legibilidad */
        #MainContent_gvLineas th,
        #MainContent_gvLineas td {
            text-align: center !important;
            font-size: 1rem !important;
            padding: 8px 12px !important;
        }

        /* 📌 Dejar la primera columna (Producto) más ancha y alineada a la izquierda */
        #MainContent_gvLineas th:first-child,
        #MainContent_gvLineas td:first-child {
            text-align: left !important;
            width: 55% !important;
        }
    </style>

    <div class="d-flex justify-content-center align-items-center" style="min-height: 80vh;">
        <div class="card shadow-lg border-0 card-remito">

            <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                <h4 class="mb-0">
                    <i class="bi bi-receipt me-2"></i>Detalle de Venta
                </h4>

                <div class="d-flex gap-2">
                    <asp:Button ID="btnEnviarMail" runat="server"
                        CssClass="btn btn-warning btn-sm fw-bold btn-enviar-mail"
                        Text="Reenviar mail"
                        OnClick="btnEnviarMail_Click" />

                    <asp:Button ID="btnImprimir" runat="server"
                        CssClass="btn btn-light btn-sm fw-bold btn-imprimir"
                        Text="Imprimir / PDF"
                        UseSubmitBehavior="false"
                        OnClientClick="return imprimirComprobante('<%= NombreComprobante %>');" />

                    <asp:Button ID="btnCerrar" runat="server" Text="✖"
                        CssClass="btn btn-light btn-sm fw-bold btn-cerrar"
                        OnClick="btnCerrar_Click" />
                </div>
            </div>

            <div runat="server" id="panelCancelada" visible="false"
                class="alert alert-danger fw-bold mb-0">
                <i class="bi bi-x-circle"></i>Esta venta está cancelada.<br />
                Motivo:
                <asp:Label ID="lblMotivo" runat="server" /><br />
                N° Nota de Crédito:
                <asp:Label ID="lblNC" runat="server" /><br />
                Fecha:
                <asp:Label ID="lblFechaCanc" runat="server" /><br />
                Usuario:
                <asp:Label ID="lblUsuarioCanc" runat="server" />
            </div>

            <div class="card-body">
                <div class="mb-3">
                    <strong>Vendedor:</strong>
                    <asp:Label ID="lblVendedor" runat="server" CssClass="ms-1" />
                </div>

                <div class="mb-3">
                    <strong>Cliente:</strong>
                    <asp:Label ID="lblCliente" runat="server" CssClass="ms-1" />
                </div>

                <div class="mb-3">
                    <strong>Fecha:</strong>
                    <asp:Label ID="lblFecha" runat="server" CssClass="ms-1" />
                </div>

                <div class="mb-3">
                    <strong>Método de Pago:</strong>
                    <asp:Label ID="lblMetodoPago" runat="server" CssClass="ms-1" />
                </div>

                <div class="mb-3">
                    <strong>N° Remito:</strong>
                    <asp:Label ID="lblFactura" runat="server" CssClass="ms-1" />
                </div>
            </div>

            <!-- 🟦 TABLA DE PRODUCTOS -->
            <div class="table-responsive px-3">
                <asp:GridView ID="gvLineas" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-sm table-striped align-middle mb-0">
                    <HeaderStyle CssClass="table-primary" />
                    <Columns>

                        <asp:TemplateField HeaderText="Producto">
                            <ItemTemplate>
                                <%# Eval("Producto.Descripcion") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad"
                            DataFormatString="{0:N2}" />

                        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario"
                            DataFormatString="{0:C}" />

                        <asp:BoundField DataField="Subtotal" HeaderText="Subtotal"
                            DataFormatString="{0:C}" />

                    </Columns>
                </asp:GridView>
            </div>

            <div class="card-footer d-flex justify-content-end bg-light">
                <h5 class="fw-bold text-end mb-0">Total:
                    <asp:Label ID="lblTotal" runat="server"
                        CssClass="text-success fw-bold" />
                </h5>
            </div>

        </div>
    </div>

</asp:Content>
