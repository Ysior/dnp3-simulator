# Progress: 02.05-simulator-root

## Changes
- Verified `simulator/Simulator/Simulator.csproj` was already SDK-style; no conversion tool call or TFM change was needed.
- Restored the original application metadata: `RootNamespace`, `AssemblyName`, `ApplicationIcon`, `WinExe`, `net8.0-windows`, WinForms, and x64 settings remain in effect.
- Restored four project references: `Commons`, `DNP3Module`, `ModuleAPI`, and `SimulatorUI`.
- Preserved the per-project `opendnp3` `3.1.2` `PackageReference`; no package or API upgrade was performed.
- Confirmed `Program.cs`, `App.config`, `automatak.ico`, and all five listed PNG assets exist. SDK default item globs preserve the source/resource inputs.

## Validation
- Focused `dotnet build simulator/Simulator/Simulator.csproj --configuration Debug`: blocked during restore by 4 existing `NU1201` incompatibilities in the mixed-TFM dependency graph. `ModuleAPI` is `net8.0-windows7.0` while consumers include `net8.0`/`net7.0`; `DNP3Commons` and `DNP3PluginAPI` are `net8.0` while `RelayOutstationPlugin` remains `net7.0`. One existing `NU1701` warning reports `opendnp3 2.2.0-M1` restored using .NET Framework assets in `RelayOutstationPlugin`.
- Full `dotnet build simulator.sln --configuration Debug`: same 4 restore errors and 1 warning; no compilation was reached.
- Full Visual Studio MSBuild fallback (`MSBuild.exe simulator.sln /restore /t:Build /p:Configuration=Debug`): blocked by `MSB4236`/SDK resolution because VS BuildTools 18.9.1 cannot locate `Microsoft.NET.Sdk` or `Microsoft.NET.Sdk.WindowsDesktop`; restore also emitted `NU1503` for all 9 projects.
- Structural audit: 9 solution projects are SDK-style and the solution contains one `Simulator\\Simulator.csproj` entry. `Simulator` has no local `packages.config`.

## Remaining Blockers / Scope Notes
- `simulator/DNP3/DNP3Commons/packages.config` still exists. It is outside this task's allowed scope and prevents claiming that the full solution has no leftover `packages.config` files; it must be handled by the owning conversion task.
- Full solution restore/build cannot pass until the remaining project TFMs are aligned and the Visual Studio MSBuild SDK installation/resolution is repaired. These are pre-existing cross-project/toolchain blockers, not changes to the Simulator project.
- Assessment inventory still labels this project `net7.0` and classic; the checked-in project is already `net8.0-windows` SDK-style. Framework/API remediation remains deferred to later tasks.

## Decomposition
- Atomic task. Evaluated `execution.md`, `breakdown-hints/common.md`, and `breakdown-hints/framework-migration.md`; no stub, multi-project edit, or separate Windows API isolation task was triggered for this scoped project.
