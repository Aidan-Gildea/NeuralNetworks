# .NET CLI Cheatsheet

## Diagnostics

- `dotnet --info` — Full SDK, runtime, and RID info. Best first check when something's misconfigured.
- `dotnet --version` — Just the active SDK version.
- `dotnet --list-sdks` — Every installed SDK and its location.
- `dotnet --list-runtimes` — Every installed runtime.

## Creating projects

- `dotnet new list` — List all available project templates.
- `dotnet new console -o MyApp` — Console app in `./MyApp`.
- `dotnet new console --use-program-main -o MyApp` — Console app with an explicit `Program` class and `Main` method instead of top-level statements.
- `dotnet new classlib -o MyLib` — Class library.
- `dotnet new webapi -o MyApi` — ASP.NET Core Web API.
- `dotnet new xunit -o MyApp.Tests` — xUnit test project.
- `dotnet new gitignore` — Drop a .NET-aware `.gitignore` in the current folder.
- `dotnet new install Avalonia.Templates` — Install a third-party template pack.

## Solutions

- `dotnet new sln -n MySolution` — Create an empty solution file.
- `dotnet sln add MyApp/MyApp.csproj` — Add a project to the solution.
- `dotnet sln list` — List projects in the solution.

## Building and running

- `dotnet restore` — Download NuGet dependencies. Usually implicit.
- `dotnet build` — Compile the project.
- `dotnet run` — Build and run the current project.
- `dotnet run --project MyApp` — Build and run a specific project.
- `dotnet watch run` — Run and auto-restart on every file save.
- `dotnet test` — Run all tests.
- `dotnet clean` — Delete build outputs (`bin/`, `obj/`).
- `dotnet publish -c Release` — Produce an optimized build for distribution.

## Dependencies

- `dotnet add package Newtonsoft.Json` — Add a NuGet package.
- `dotnet add reference ../MyLib/MyLib.csproj` — Reference another project.
- `dotnet list package` — Show installed packages.
- `dotnet remove package Newtonsoft.Json` — Remove a package.

## VS Code (C# Dev Kit)

- `F5` — Build and run with the debugger attached.
- `Ctrl+F5` — Run without debugging.
- `Cmd+Shift+B` — Build.
- `Cmd+Shift+P` → `.NET: New Project…` — Visual Studio-style template picker.

## Notes

- **WinForms and WPF don't run on macOS.** The `winforms` and `wpf` templates appear in `dotnet new list`, but they target `net10.0-windows` and need `Microsoft.WindowsDesktop.App`, which has no macOS build. `-p:EnableWindowsTargeting=true` lets them compile but not run. Use Avalonia UI or .NET MAUI for cross-platform desktop GUIs.
- **`--use-program-main` is per-project**; there's no global default. Use a shell alias if you always want it.
