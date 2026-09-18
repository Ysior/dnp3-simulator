# 02.05-simulator-root: Convert and validate Simulator application

## Objective
Process the solution root application last and complete SDK-style conversion validation for the full project graph.

## Scope
- `simulator/Simulator/Simulator.csproj`

## Preconditions
Complete `02.04-dnp3-module`. The application currently has pre-existing SDK-style changes and must be verified without reverting them.

## Research Findings
- The scoped project is already SDK-style (`Microsoft.NET.Sdk.WindowsDesktop`), so no conversion tool call is required and the current TFM must remain `net8.0-windows`.
- `Simulator.oldcsproj` shows four required project references that are absent from the current SDK file: `Commons`, `DNP3Module`, `ModuleAPI`, and `SimulatorUI`. `Program.cs` consumes `ModuleAPI` and `DNP3Module`; the other references are part of the original application graph and must remain intact.
- The old project defines `WinExe`, `PlatformTarget=x64`, `ApplicationIcon=automatak.ico`, and `AssemblyName=SimulatorApplication`; these are application metadata to preserve. SDK default globs already cover the `.cs`, `.resx`, `Properties` files, and `App.config`, while the `resources` files and icon need explicit preservation checks.
- Package intent is the existing per-project `opendnp3` `3.1.2` `PackageReference`; no package version or TFM upgrade is in scope here. The legacy `App.config` contains a .NET Framework startup declaration and must be retained as configuration input, with compatibility assessed by the build rather than rewritten in this structural task.
- Assessment reports this project as classic WinForms, current `net7.0` in the stale inventory, four project dependencies, and a binding-redirect warning. The checked-in SDK project already uses `net8.0-windows`; this task preserves that current value and defers API/framework remediation to later tasks.
- No `// STUB:` markers were found under `simulator/Simulator`.

## Decomposition Verdict
Atomic: one SDK-style WinForms entry-point project with one coherent metadata/reference repair and validation gate. Evaluated `execution.md`, `breakdown-hints/common.md`, and `breakdown-hints/framework-migration.md`; no multi-project or stub-resolution trigger applies to this scoped edit.

## Steps
1. Confirm Simulator remains the solution entry-point and retains its references to DNP3Module, Commons, ModuleAPI, and SimulatorUI.
2. If any legacy conversion work remains, invoke it sequentially; otherwise validate the existing SDK-style project.
3. Preserve current TFM, WinForms designer/resource behavior, application configuration, native/runtime assets, and package version/management choice. Do not broaden into framework/API/package upgrades.
4. Restore and build the complete solution against current targets, using full Visual Studio MSBuild if SDK build tooling cannot process the legacy Windows/WinForms resources.
5. Verify all nine solution projects are SDK-style, solution references are intact, intended source/resource/project-reference inputs remain included, and no converted project retains an unintended `packages.config`.

## Done Condition
The complete solution is validated as SDK-style and restores/builds against current targets, with any environmental or pre-existing compatibility blocker clearly recorded for the subsequent framework-upgrade tasks.
