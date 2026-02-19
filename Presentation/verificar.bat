@echo off
echo ========================================
echo VERIFICACION DE CONFIGURACION
echo ========================================
echo.

echo [1/4] Verificando archivos del proyecto Presentation...
if exist "Usuario.aspx" (
    echo   [OK] Usuario.aspx existe
) else (
    echo   [ERROR] Usuario.aspx NO existe
)

if exist "UsuarioConsulta.aspx" (
    echo   [OK] UsuarioConsulta.aspx existe
) else (
    echo   [ERROR] UsuarioConsulta.aspx NO existe
)

if exist "Site.Master" (
    echo   [OK] Site.Master existe
) else (
    echo   [ERROR] Site.Master NO existe
)

echo.
echo [2/4] Verificando Web.config...
if exist "Web.config" (
    echo   [OK] Web.config existe
    findstr /C:"UserServiceReference" Web.config >nul
    if errorlevel 1 (
        echo   [ADVERTENCIA] No se encontro configuracion de WCF en Web.config
        echo   Esto es normal si aun no has agregado la Service Reference
    ) else (
        echo   [OK] Configuracion de WCF encontrada
    )
) else (
    echo   [ERROR] Web.config NO existe
)

echo.
echo [3/4] Verificando Service Reference...
if exist "Service References" (
    if exist "Service References\UserServiceReference" (
        echo   [OK] Service Reference agregada correctamente
    ) else (
        echo   [PENDIENTE] Service Reference NO agregada aun
        echo   DEBES agregar la Service Reference siguiendo PASOS_SIGUIENTES.md
    )
) else (
    echo   [PENDIENTE] Carpeta Service References no existe
    echo   DEBES agregar la Service Reference siguiendo PASOS_SIGUIENTES.md
)

echo.
echo [4/4] Verificando archivos code-behind...
if exist "Usuario.aspx.cs" (
    echo   [OK] Usuario.aspx.cs existe
) else (
    echo   [ERROR] Usuario.aspx.cs NO existe
)

if exist "UsuarioConsulta.aspx.cs" (
    echo   [OK] UsuarioConsulta.aspx.cs existe
) else (
    echo   [ERROR] UsuarioConsulta.aspx.cs NO existe
)

echo.
echo ========================================
echo RESUMEN
echo ========================================
echo.
echo Proximos pasos:
echo 1. Lee el archivo PASOS_SIGUIENTES.md
echo 2. Ejecuta el proyecto Business (F5)
echo 3. Agrega la Service Reference en Presentation
echo 4. Descomenta el codigo en los archivos .cs
echo 5. Ejecuta ambos proyectos
echo.
echo ========================================
pause
