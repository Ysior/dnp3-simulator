# 02.03-relay-plugin: Convert and validate RelayOutstationPlugin

## Objective
Convert the remaining legacy RelayOutstationPlugin project after all of its direct dependencies are validated.

## Scope
- `simulator/DNP3/RelayOutstationPlugin/RelayOutstationPlugin.csproj`

## Preconditions
Complete `02.02-shared-api-projects`. The current file is known to remain legacy and contains a legacy `net7.0` entry plus `TargetFrameworkVersion` `v4.5`; do not silently choose a new TFM in this task.

## Steps
1. Inspect the dirty project and preserve unrelated existing changes. Resolve only the SDK-style conversion mechanics.
2. Invoke the SDK conversion workflow for RelayOutstationPlugin sequentially.
3. Preserve its references to ModuleAPI, DNP3Commons, and DNP3PluginAPI, along with resources, designer files, native/runtime assets, and package version/management intent.
4. Handle its adjacent `packages.config` as part of conversion only; do not replace the incompatible `opendnp3` package or fix its binary/source API findings.
5. Restore/build RelayOutstationPlugin and its dependency closure against the current target configuration, and verify the solution project reference remains intact.

## Done Condition
RelayOutstationPlugin is SDK-style with its dependency edges and inputs preserved; conversion validation is recorded, including any blocker caused by its pre-existing target-framework/package state.

## Research Findings
- Assessment: `net7.0` current target, proposed `net8.0-windows`; project is classic WinForms, SDK-style `False`, 7 files, 3 project dependencies, 1 dependent project.
- Assessment issues: 268 binary-incompatible and 11 source-incompatible API findings, primarily Windows Forms; these are later TFM/API work and are out of scope for this format-only conversion.
- Current project inputs: `OutstationForm.cs`, its designer and `.resx`, `OutstationInstance.cs`, generated resources, three `ProjectReference` entries to `ModuleAPI`, `DNP3Commons`, and `DNP3PluginAPI`, plus `System.Windows.Forms`/`System.Drawing` references.
- Package state: adjacent `packages.config` contains only `opendnp3` `2.2.0-M1` for `net45`; the project directly references the matching DLLs under `packages/opendnp3.2.2.0-M1/lib/net45`. Preserve this version and package intent; do not upgrade or replace it.
- Target state: the legacy file contains both `TargetFrameworkVersion` `v4.5` and `TargetFramework` `net7.0`. This task must not silently resolve that conflict; both values remain represented in the converted project until a later target-framework task decides the intended target.
- Decomposition verdict: `atomic`. Only one project is in scope; no stubs, package replacement, System.Web dependency, test project, or independent concern was found. Evaluated `execution.md`, `breakdown-hints/common.md`, and `breakdown-hints/framework-migration.md`.
- Validation approach: restore/build the converted project with Visual Studio MSBuild because it is WinForms and retains a .NET Framework target intent; record any failures caused by the existing target/package state.
