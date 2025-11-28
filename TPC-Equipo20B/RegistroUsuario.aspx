<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="TPC_Equipo20B.RegistroUsuario" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Registro - AGIAPURR distribuidora</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" crossorigin="anonymous" />
    <link href="https://fonts.googleapis.com" rel="preconnect" />
    <link crossorigin="" href="https://fonts.gstatic.com" rel="preconnect" />
    <link href="https://fonts.googleapis.com/css2?family=Manrope:wght@400;500;700;800&amp;display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined" rel="stylesheet" />

    <style>
        body {
            font-family: 'Manrope', sans-serif;
            background-color: #f6f8f6;
            color: #111813;
        }

        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
            user-select: none;
        }

        .validator {
            color: #dc3545;
            font-size: 0.875em;
            display: block;
            margin-top: 0.25rem;
        }

        .validation-container {
            min-height: 1rem;
        }

        .btn-primary {
            background-color: #11d452;
            border-color: #11d452;
            color: #102216;
            font-weight: 700;
        }

            .btn-primary:hover {
                background-color: #0fbc48;
                border-color: #0fbc48;
                color: #102216;
            }

            .form-control:focus, .btn-primary:focus {
                border-color: #11d452;
                box-shadow: 0 0 0 0.25rem rgba(17, 212, 82, 0.25);
            }

        .link-primary {
            color: #11d452 !important;
            text-decoration: none;
        }

            .link-primary:hover {
                text-decoration: underline !important;
            }

        .card-custom {
            background-color: #ffffff;
            border-color: #dbe6df;
        }

        .header-custom {
            background-color: #ffffff;
            border-bottom: 1px solid #dbe6df;
        }

        .form-control::placeholder {
            color: #61896f;
            opacity: 1;
        }

        /* Contenedor más ancho y responsivo */
        .form-container-wide {
            max-width: 1400px;
            width: 100%;
            margin: 0 auto;
        }

        @media (max-width: 767.98px) {
            .form-container-wide {
                max-width: 100%;
                padding: 0 0.5rem;
            }
        }

        @media (min-width: 768px) and (max-width: 991.98px) {
            .form-container-wide {
                max-width: 90%;
            }
        }

        @media (min-width: 992px) and (max-width: 1399.98px) {
            .form-container-wide {
                max-width: 85%;
            }
        }

        @media (min-width: 1400px) {
            .form-container-wide {
                max-width: 1400px;
            }
        }
    </style>
</head>
<body class="d-flex flex-column min-vh-100">

    <form id="form1" runat="server" class="d-flex flex-column flex-grow-1">

        <header class="w-100 header-custom shadow-sm">
            <div class="container-fluid py-3 px-4">
                <div class="d-flex align-items-center gap-3">
                    <div class="text-primary" style="width: 24px; height: 24px;">
                        <svg fill="none" viewBox="0 0 48 48" xmlns="http://www.w3.org/2000/svg">
                            <path clip-rule="evenodd" d="M24 4H42V17.3333V30.6667H24V44H6V30.6667V17.3333H24V4Z" fill="currentColor" fill-rule="evenodd"></path>
                        </svg>
                    </div>
                    <h2 class="h5 mb-0 fw-bold">AGIAPURR distribuidora</h2>
                </div>
            </div>
        </header>

        <main class="d-flex flex-grow-1 align-items-center justify-content-center p-4">

            <div class="form-container-wide">
                <div class="card card-custom p-4 p-sm-5 rounded-4 shadow-sm">
                    <div class="card-body">

                        <h1 class="text-center h3 fw-bold">Crear Cuenta</h1>
                        <p class="mt-2 text-center text-muted">
                            Complete el formulario para registrarse en el sistema.
                        </p>

                        <div class="mt-4">
                            <div class="row g-3 g-lg-4">
                                <!-- Columna izquierda -->
                                <div class="col-md-6">
                                    <!-- Nombre -->
                                    <div class="mb-3">
                                        <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre y Apellido" CssClass="form-label fw-medium" />
                                        <div class="input-group input-group-lg">
                                            <span class="input-group-text bg-light" style="border-right: 0;">
                                                <span class="material-symbols-outlined text-muted">badge</span>
                                            </span>
                                            <asp:TextBox ID="txtNombre" runat="server" TabIndex="1"
                                                placeholder="Ingrese su nombre"
                                                CssClass="form-control" Style="border-left: 0;" />
                                        </div>
                                        <div class="validation-container">
                                            <asp:RequiredFieldValidator ID="valNombre" runat="server"
                                                ControlToValidate="txtNombre"
                                                ErrorMessage="El nombre es requerido"
                                                Display="Static"
                                                CssClass="validator"
                                                ValidationGroup="Registro" />
                                        </div>
                                    </div>

                                    <!-- Email -->
                                    <div class="mb-3">
                                        <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" Text="Correo Electrónico" CssClass="form-label fw-medium" />
                                        <div class="input-group input-group-lg">
                                            <span class="input-group-text bg-light" style="border-right: 0;">
                                                <span class="material-symbols-outlined text-muted">mail</span>
                                            </span>
                                            <asp:TextBox ID="txtEmail" runat="server" TabIndex="3"
                                                TextMode="Email"
                                                placeholder="correo@ejemplo.com"
                                                CssClass="form-control" Style="border-left: 0;" />
                                        </div>
                                        <div class="validation-container">
                                            <asp:RequiredFieldValidator ID="valEmailRequerido" runat="server"
                                                ControlToValidate="txtEmail"
                                                ErrorMessage="El correo electrónico es requerido"
                                                Display="Dynamic"
                                                CssClass="validator"
                                                ValidationGroup="Registro" />
                                            <asp:RegularExpressionValidator ID="valEmailFormato" runat="server"
                                                ControlToValidate="txtEmail"
                                                ErrorMessage="Ingrese un correo electrónico válido"
                                                ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                                Display="Dynamic"
                                                CssClass="validator"
                                                ValidationGroup="Registro" />
                                        </div>
                                    </div>

                                    <!-- Dirección -->
                                    <div class="mb-3">
                                        <asp:Label ID="lblDireccion" runat="server" AssociatedControlID="txtDireccion" Text="Dirección" CssClass="form-label fw-medium" />
                                        <div class="input-group input-group-lg">
                                            <span class="input-group-text bg-light" style="border-right: 0;">
                                                <span class="material-symbols-outlined text-muted">home</span>
                                            </span>
                                            <asp:TextBox ID="txtDireccion" runat="server" TabIndex="5"
                                                placeholder="Ingrese su dirección"
                                                CssClass="form-control" Style="border-left: 0;" />
                                        </div>
                                        <div class="validation-container"></div>
                                    </div>

                                    <!-- Nombre de usuario -->
                                    <div class="mb-3">
                                        <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUsername" Text="Nombre de Usuario" CssClass="form-label fw-medium" />
                                        <div class="input-group input-group-lg">
                                            <span class="input-group-text bg-light" style="border-right: 0;">
                                                <span class="material-symbols-outlined text-muted">person</span>
                                            </span>
                                            <asp:TextBox ID="txtUsername" runat="server" TabIndex="7"
                                                placeholder="Elija un nombre de usuario"
                                                CssClass="form-control" Style="border-left: 0;" />
                                        </div>
                                        <div class="validation-container">
                                            <asp:RequiredFieldValidator ID="valUsername" runat="server"
                                                ControlToValidate="txtUsername"
                                                ErrorMessage="El nombre de usuario es requerido"
                                                Display="Dynamic"
                                                CssClass="validator"
                                                ValidationGroup="Registro" />
                                        </div>
                                    </div>

                                    <!-- Confirmar contraseña -->
                                    <div class="mb-3">
                                        <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="txtConfirmPassword" Text="Confirmar Contraseña" CssClass="form-label fw-medium" />
                                        <div class="input-group input-group-lg">
                                            <span class="input-group-text bg-light" style="border-right: 0;">
                                                <span class="material-symbols-outlined text-muted">lock</span>
                                            </span>
                                            <asp:TextBox ID="txtConfirmPassword" runat="server" TabIndex="9"
                                                TextMode="Password"
                                                placeholder="Repita su contraseña"
                                                CssClass="form-control" Style="border-left: 0; border-right: 0;" />
                                            <button type="button" id="toggleConfirmPassword" class="input-group-text bg-light" style="border-left: 0; cursor: pointer;">
                                                <span class="material-symbols-outlined text-muted">visibility</span>
                                            </button>
                                        </div>
                                        <div class="validation-container">
                                            <asp:RequiredFieldValidator ID="valConfirmPasswordRequerido" runat="server"
                                                ControlToValidate="txtConfirmPassword"
                                                ErrorMessage="Debe confirmar su contraseña"
                                                Display="Dynamic"
                                                CssClass="validator"
                                                ValidationGroup="Registro" />
                                            <asp:CompareValidator ID="valPasswordMatch" runat="server"
                                                ControlToValidate="txtConfirmPassword"
                                                ControlToCompare="txtPassword"
                                                ErrorMessage="Las contraseñas no coinciden"
                                                Display="Dynamic"
                                                CssClass="validator"
                                                ValidationGroup="Registro" />
                                        </div>
                                    </div>
                                </div>

                                <!-- Columna derecha -->
                                <div class="col-md-6">
                                    <!-- Documento -->
                                    <div class="mb-3">
                                        <asp:Label ID="lblDocumento" runat="server" AssociatedControlID="txtDocumento" Text="Documento" CssClass="form-label fw-medium" />
                                        <div class="input-group input-group-lg">
                                            <span class="input-group-text bg-light" style="border-right: 0;">
                                                <span class="material-symbols-outlined text-muted">credit_card</span>
                                            </span>
                                            <asp:TextBox ID="txtDocumento" runat="server" TabIndex="2"
                                                placeholder="Ingrese su documento"
                                                CssClass="form-control" Style="border-left: 0;" />
                                        </div>
                                        <asp:RequiredFieldValidator
                                            ID="rfvDocumento"
                                            runat="server"
                                            ControlToValidate="txtDocumento"
                                            CssClass="text-danger"
                                            ErrorMessage="El documento es obligatorio."
                                            Display="Dynamic" 
                                            ValidationGroup="Registro"/>

                                        <asp:RegularExpressionValidator
                                            ID="revDocumento"
                                            runat="server"
                                            ControlToValidate="txtDocumento"
                                            ValidationExpression="^\d+$"
                                            ErrorMessage="Ingrese solo números."
                                            CssClass="text-danger"
                                            Display="Dynamic" 
                                            ValidationGroup="Registro"/>

                                        <div class="validation-container"></div>
                                    </div>

                                    <!-- Teléfono -->
                                    <div class="mb-3">
                                        <asp:Label ID="lblTelefono" runat="server" AssociatedControlID="txtTelefono" Text="Teléfono" CssClass="form-label fw-medium" />
                                        <div class="input-group input-group-lg">
                                            <span class="input-group-text bg-light" style="border-right: 0;">
                                                <span class="material-symbols-outlined text-muted">phone</span>
                                            </span>
                                            <asp:TextBox ID="txtTelefono" runat="server" TabIndex="4"
                                                placeholder="Ingrese su teléfono"
                                                CssClass="form-control" Style="border-left: 0;" />
                                        </div>

                                        <asp:RequiredFieldValidator
                                            ID="rfvTelefono"
                                            runat="server"
                                            ControlToValidate="txtTelefono"
                                            CssClass="text-danger"
                                            ErrorMessage="El teléfono es obligatorio."
                                            Display="Dynamic"
                                            ValidationGroup="Registro"/>
                                        <asp:RegularExpressionValidator
                                            ID="revTelefono"
                                            runat="server"
                                            ControlToValidate="txtTelefono"
                                            ValidationExpression="^\d+$"
                                            ErrorMessage="Ingrese solo números."
                                            CssClass="text-danger"
                                            Display="Dynamic" 
                                            ValidationGroup="Registro"/>

                                        <div class="validation-container"></div>
                                    </div>

                                    <!-- Localidad -->
                                    <div class="mb-3">
                                        <asp:Label ID="lblLocalidad" runat="server" AssociatedControlID="txtLocalidad" Text="Localidad" CssClass="form-label fw-medium" />
                                        <div class="input-group input-group-lg">
                                            <span class="input-group-text bg-light" style="border-right: 0;">
                                                <span class="material-symbols-outlined text-muted">location_city</span>
                                            </span>
                                            <asp:TextBox ID="txtLocalidad" runat="server" TabIndex="6"
                                                placeholder="Ingrese su localidad"
                                                CssClass="form-control" Style="border-left: 0;" />
                                        </div>
                                        <div class="validation-container"></div>
                                    </div>

                                    <!-- Contraseña -->
                                    <div class="mb-3">
                                        <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" Text="Contraseña" CssClass="form-label fw-medium" />
                                        <div class="input-group input-group-lg">
                                            <span class="input-group-text bg-light" style="border-right: 0;">
                                                <span class="material-symbols-outlined text-muted">lock</span>
                                            </span>
                                            <asp:TextBox ID="txtPassword" runat="server" TabIndex="8"
                                                TextMode="Password"
                                                placeholder="Mínimo 6 caracteres"
                                                CssClass="form-control" Style="border-left: 0; border-right: 0;" />
                                            <button type="button" id="togglePassword" class="input-group-text bg-light" style="border-left: 0; cursor: pointer;">
                                                <span class="material-symbols-outlined text-muted">visibility</span>
                                            </button>
                                        </div>
                                        <div class="validation-container">
                                            <asp:RequiredFieldValidator ID="valPasswordRequerido" runat="server"
                                                ControlToValidate="txtPassword"
                                                ErrorMessage="La contraseña es requerida"
                                                Display="Dynamic"
                                                CssClass="validator"
                                                ValidationGroup="Registro" />
                                            <asp:RegularExpressionValidator ID="valPasswordLongitud" runat="server"
                                                ControlToValidate="txtPassword"
                                                ErrorMessage="La contraseña debe tener al menos 6 caracteres"
                                                ValidationExpression=".{6,}"
                                                Display="Dynamic"
                                                CssClass="validator"
                                                ValidationGroup="Registro" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Label ID="lblError" runat="server" CssClass="validator text-center mb-3" EnableViewState="false" />
                            <asp:Label ID="lblSuccess" runat="server" CssClass="text-success text-center mb-3 d-block fw-medium" EnableViewState="false" />

                            <asp:Button ID="btnRegistrar" runat="server" TabIndex="10"
                                Text="Crear Cuenta"
                                CssClass="btn btn-primary w-100 btn-lg"
                                OnClick="btnRegistrar_Click"
                                ValidationGroup="Registro" />

                            <div class="mt-3 text-center">
                                <span class="text-muted small">¿Ya tienes una cuenta?</span>
                                <a class="link-primary small ms-1" href="Default.aspx">Iniciar Sesión</a>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </main>

        <footer class="w-100 py-4 px-4">
            <div class="text-center text-muted small">
                <p class="mb-0">© 2025 AGIAPURR distribuidora. Todos los derechos reservados.</p>
            </div>
        </footer>

        <script>
            // Toggle para mostrar/ocultar contraseña
            document.getElementById('togglePassword').addEventListener('click', function () {
                const passwordField = document.getElementById('<%= txtPassword.ClientID %>');
                const icon = this.querySelector('.material-symbols-outlined');

                if (passwordField.type === 'password') {
                    passwordField.type = 'text';
                    icon.textContent = 'visibility_off';
                } else {
                    passwordField.type = 'password';
                    icon.textContent = 'visibility';
                }
            });

            // Toggle para confirmar contraseña
            document.getElementById('toggleConfirmPassword').addEventListener('click', function () {
                const confirmPasswordField = document.getElementById('<%= txtConfirmPassword.ClientID %>');
                const icon = this.querySelector('.material-symbols-outlined');

                if (confirmPasswordField.type === 'password') {
                    confirmPasswordField.type = 'text';
                    icon.textContent = 'visibility_off';
                } else {
                    confirmPasswordField.type = 'password';
                    icon.textContent = 'visibility';
                }
            });
        </script>

    </form>
</body>
</html>
