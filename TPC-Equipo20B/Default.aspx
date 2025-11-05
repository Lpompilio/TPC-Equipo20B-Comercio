<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Equipo20B.Default" %>

<!DOCTYPE html>
<html class="light" lang="es"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Inicio de Sesión - AGIAPURR distribuidora</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com" rel="preconnect"/>
<link crossorigin="" href="https://fonts.gstatic.com" rel="preconnect"/>
<link href="https://fonts.googleapis.com/css2?family=Manrope:wght@400;500;700;800&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined" rel="stylesheet"/>
<script>
    tailwind.config = {
      darkMode: "class",
      theme: {
        extend: {
          colors: {
            "primary": "#11d452",
            "background-light": "#f6f8f6",
            "background-dark": "#102216",
            "text-light": "#111813",
            "text-dark": "#f0f4f2",
            "border-light": "#dbe6df",
            "border-dark": "#344a3b",
            "card-light": "#ffffff",
            "card-dark": "#182c1e",
            "placeholder-light": "#61896f",
            "placeholder-dark": "#8a9e91"
          },
          fontFamily: {
            "display": ["Manrope", "sans-serif"]
          },
          borderRadius: {
            "DEFAULT": "0.25rem",
            "lg": "0.5rem",
            "xl": "0.75rem",
            "full": "9999px"
          },
        },
      },
    }
  </script>
<style>
    .material-symbols-outlined {
      font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
    }
  </style>
</head>
<body class="bg-background-light dark:bg-background-dark font-display text-text-light dark:text-text-dark">
    <div class="relative flex min-h-screen w-full flex-col items-center justify-between">

        <header class="w-full border-b border-solid border-border-light dark:border-border-dark bg-card-light dark:bg-card-dark/50">
            <div class="mx-auto flex max-w-7xl items-center justify-between whitespace-nowrap px-6 py-4">
                <div class="flex items-center gap-4 text-text-light dark:text-text-dark">
                    <div class="size-6 text-primary">
                        <svg fill="none" viewBox="0 0 48 48" xmlns="http://www.w3.org/2000/svg">
                            <path clip-rule="evenodd" d="M24 4H42V17.3333V30.6667H24V44H6V30.6667V17.3333H24V4Z" fill="currentColor" fill-rule="evenodd"></path>
                        </svg>
                    </div>
                    <h2 class="text-lg font-bold leading-tight tracking-[-0.015em]">AGIAPURR distribuidora</h2>
                </div>
            </div>
        </header>

        <main class="flex w-full flex-1 items-center justify-center p-4">
            <div class="flex w-full max-w-md flex-col rounded-xl border border-border-light dark:border-border-dark bg-card-light dark:bg-card-dark p-8 shadow-sm">

                <h1 class="text-center text-3xl font-bold tracking-tight text-text-light dark:text-text-dark">Inicio de Sesión</h1>
                <p class="mt-2 text-center text-sm text-placeholder-light dark:text-placeholder-dark">
                    Bienvenido de nuevo. Por favor, ingrese sus credenciales.
                </p>

                
                <form id="form1" runat="server" class="mt-8 flex flex-col gap-6">

                    <div class="flex flex-col gap-2">
                        <label class="text-sm font-medium" for="username">Nombre de Usuario</label>
                        <div class="relative flex w-full items-center">
                            <span class="material-symbols-outlined absolute left-4 text-xl text-placeholder-light dark:text-placeholder-dark">person</span>
                            <input class="h-14 w-full rounded-lg border border-border-light bg-background-light p-4 pl-12 text-base text-text-light placeholder:text-placeholder-light focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20 dark:border-border-dark dark:bg-background-dark dark:text-text-dark dark:placeholder:text-placeholder-dark dark:focus:border-primary"
                                id="username" placeholder="Ingrese su nombre de usuario" type="text" />
                        </div>
                    </div>

                    <div class="flex flex-col gap-2">
                        <label class="text-sm font-medium" for="password">Contraseña</label>
                        <div class="relative flex w-full items-center">
                            <span class="material-symbols-outlined absolute left-4 text-xl text-placeholder-light dark:text-placeholder-dark">lock</span>
                            <input class="h-14 w-full rounded-lg border border-border-light bg-background-light p-4 px-12 text-base text-text-light placeholder:text-placeholder-light focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20 dark:border-border-dark dark:bg-background-dark dark:text-text-dark dark:placeholder:text-placeholder-dark dark:focus:border-primary"
                                id="password" placeholder="Ingrese su contraseña" type="password" />
                            <button class="absolute right-4 text-placeholder-light dark:text-placeholder-dark" type="button">
                                <span class="material-symbols-outlined">visibility</span>
                            </button>
                        </div>
                    </div>

                    <div class="flex items-center justify-end">
                        <a class="text-sm font-medium text-primary hover:underline" href="#">Olvidé mi contraseña</a>
                    </div>

                  
                    <asp:Button ID="btnLogin" runat="server"
                        Text="Iniciar Sesión"
                        CssClass="flex h-12 w-full cursor-pointer items-center justify-center overflow-hidden rounded-lg bg-primary text-base font-bold text-background-dark transition-colors hover:bg-primary/90"
                        OnClick="btnLogin_Click" />

                </form>
            </div>
        </main>

        <footer class="w-full py-6 px-4">
            <div class="text-center text-sm text-placeholder-light dark:text-placeholder-dark">
                <p>© 2024 AGIAPURR distribuidora. Todos los derechos reservados.</p>
            </div>
        </footer>
    </div>
</body>
</html>
