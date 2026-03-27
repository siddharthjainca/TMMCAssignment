@echo off
setlocal EnableExtensions
title TMMCAssignment
cd /d "%~dp0"

set "EXE=%~dp0TMMCAssignment\bin\Release\net9.0-windows\TMMCAssignment.exe"
if not exist "%EXE%" set "EXE=%~dp0TMMCAssignment\bin\Debug\net9.0-windows\TMMCAssignment.exe"

if not exist "%EXE%" (
    echo.
    echo Could not find TMMCAssignment.exe.
    echo Build the project first from this folder:
    echo   dotnet build TMMCAssignment\TMMCAssignment.csproj -c Release
    echo.
    pause
    exit /b 1
)

"%EXE%" %*
echo.
pause
