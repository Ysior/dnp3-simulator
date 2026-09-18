# 01-prerequisites: Verify the .NET 8 toolchain

Confirm that the .NET 8 SDK and required Windows desktop tooling are available, and verify that repository SDK selection is compatible with the `net8.0` and `net8.0-windows` targets. Establish the current solution build as the execution baseline without introducing test-baseline work.

**Done when**: The required .NET 8 SDK is available, SDK selection is compatible, and the solution's current build state and any existing blockers are recorded for comparison.

## Research Findings

- Repository: `d:\DNP3\dnp3-simulator`; solution: `simulator.sln`; scope is the full 9-project solution.
- SDK selection: no `global.json` and no `Directory.Build.props`/`Directory.Build.targets` were found, so the active SDK follows installed SDK resolution. Installed SDKs are 6.0.400, 8.0.425, and 10.0.401; the active SDK is 10.0.401.
- Windows desktop tooling: `Microsoft.WindowsDesktop.App` runtimes 8.0.11 and 8.0.31 are installed. Visual Studio Build Tools 2026 (18.9.2) with MSBuild is installed and complete. No .NET workloads are installed; none is required by the current WinForms projects.
- Project target inventory: `Commons`=`net8.0`; `DefaultOutstationPlugin`=`net8.0-windows` with `UseWindowsForms=true`; `DNP3Commons`=`net8.0`; `DNP3PluginAPI`=`net8.0`; `DNP3Module`=`net8.0-windows`; `Simulator`=`net8.0-windows`; `ModuleAPI`=`net8.0-windows`; `SimulatorUI`=`net8.0`; `RelayOutstationPlugin`=`net7.0` and remains legacy/non-SDK-style.
- Build-tool pattern: SDK-style projects can use `dotnet build`, but the solution includes WinForms, legacy project format, designer resources, and `net7.0`; the full solution baseline should therefore use Visual Studio `MSBuild /restore` when available, with `dotnet build` as a diagnostic comparison if needed.
- Dependency/configuration observations: four `packages.config` files remain under `DNP3Commons`, `DNP3PluginAPI`, `DNP3Simulator`, and `RelayOutstationPlugin`; no `// STUB:` markers were found. These are recorded as existing migration blockers, not changed by this prerequisite task.

## Decomposition Assessment

Loaded `execution.md` and breakdown hints `common.md`, `framework-migration.md`, `framework-web-migration.md`, and `test.md`. The task is atomic: it performs environment/configuration inspection and one solution baseline, does not modify multiple project TFMs or packages, and does not include test-baseline generation. No hint condition requiring TaskBreaker was met.
