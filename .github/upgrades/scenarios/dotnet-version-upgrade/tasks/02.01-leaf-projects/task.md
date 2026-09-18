# 02.01-leaf-projects: Validate or convert leaf projects

## Objective
Process the bottom dependency tier for SDK-style conversion. Ensure these four solution projects are SDK-style without changing their current target frameworks or expanding package/API modernization scope.

## Scope
- `simulator/Commons/Commons.csproj`
- `simulator/SimulatorUI/SimulatorUI.csproj`
- `simulator/SimulatorAPI/ModuleAPI.csproj`
- `simulator/DNP3/DNP3Commons/DNP3Commons.csproj`

## Steps
1. Inspect the current dirty working tree and preserve existing user/scenario changes; do not revert them.
2. For any project in scope that is still legacy, invoke the SDK conversion workflow sequentially. Projects already SDK-style require verification rather than reconversion.
3. Preserve each project's current TFM, project inputs, WinForms designer/resource behavior, native/runtime assets, project references, and package version/management intent. Do not convert similarly named non-solution files such as `DNP3Commons — kopia.csproj`.
4. For `DNP3Commons`, account for the remaining adjacent `packages.config` and verify whether it is still required after conversion; do not perform unrelated package upgrades.
5. Build/restore the affected tier against its current targets, then verify the solution references and intended source/resource inclusion.

## Done Condition
All four projects are SDK-style, any conversion is behavior-preserving, current targets remain unchanged, and focused restore/build validation for this tier succeeds or records a concrete environment blocker for the next task.

## Constraints
Use the conversion tool one project at a time. API/package compatibility fixes and target-framework upgrades are out of scope.

## Research Findings (2026-09-18)

- The solution topological-order tool reports these projects in the solution dependency order: `DNP3PluginAPI`, `DNP3Commons`, `ModuleAPI`, `Commons`, `Simulator`, `DNP3Module`, `RelayOutstationPlugin`, `DefaultOutstationPlugin`, `SimulatorUI`. The four projects in this task are therefore one dependency tier; `Commons` is referenced by `ModuleAPI` and `SimulatorUI`, while `DNP3Commons` has no project dependency.
- The assessment narrative and query data are stale relative to the repository. They report all four projects as classic and report `opendnp3` `2.2.0-M1`, but the checked-in project files in `HEAD` are already SDK-style and reference `opendnp3` `3.1.2`.
- Current checked-in project metadata is: `Commons` `net8.0`, `SimulatorUI` `net8.0`, `ModuleAPI` `net8.0-windows` with `UseWindowsForms`, and `DNP3Commons` `net8.0`. These values must remain unchanged for this format-only task.
- The four project files contain no explicit project references, compile/resource/content includes, or imports. Their current native/runtime metadata was missing `PlatformTarget`, although history shows all four legacy projects used `x86`; this is restored minimally below so the opendnp3 native assets select the existing x86 runtime. SDK default globs remain the active source/resource inclusion behavior. The solution still contains all four project entries with their original paths.
- Each scoped project retains a hand-authored `Properties/AssemblyInfo.cs`; `GenerateAssemblyInfo=false` is therefore set in each project to preserve the existing assembly metadata and avoid SDK-generated duplicate attributes.
- The first focused build exposed two conversion metadata defects: the native `opendnp3` target requires the historical `x86` platform, and `Commons` failed with CS5001 because the prior SDK file incorrectly declared `OutputType=Exe`. `PlatformTarget=x86`, `GenerateAssemblyInfo=false`, and `OutputType=Library` for `Commons`/`DNP3Commons` preserve the legacy project behavior and are the minimal conversion fixes.
- `Nullable=disable` is retained for all four projects because the legacy sources are not nullable-annotated; this removes conversion-introduced nullable warnings without changing source/API behavior.
- `SimulatorUI` contains binary image streams in `LogFileControl.resx` and `LogWindow.resx`. SDK resource generation therefore required `System.Resources.Extensions` `8.0.0`; this is a conversion support dependency, while the existing `opendnp3` `3.1.2` intent remains unchanged. The remaining MSB3825 notices concern legacy BinaryFormatter serialization in those existing resources and are recorded for later modernization rather than suppressed.
- Legacy comparison also showed `SimulatorUI` and `ModuleAPI` are libraries, not executables, and `SimulatorUI` references `Commons`; these project semantics are restored explicitly. WinForms support is enabled for `SimulatorUI` and `DNP3Commons` while their current TFM text remains unchanged, so any SDK requirement for a `-windows` TFM is an explicit validation blocker rather than an unapproved TFM change.
- Focused validation found `net8.0` is invalid for the WinForms projects when `UseWindowsForms=true`: SDK error NETSDK1136 requires `net8.0-windows`. This cannot be corrected within this task because preserving current TFM text is an explicit constraint; `SimulatorUI` and `DNP3Commons` therefore remain blocked for build until the later TFM-upgrade task authorizes that change.
- `simulator/DNP3/DNP3Commons/packages.config` still exists and declares `opendnp3` `2.2.0-M1` for `net45`; it is adjacent to the already SDK-style `DNP3Commons.csproj`. It is not removed here because conversion is already complete in `HEAD`, and removing it would be a package-management change outside this task.
- No `// STUB:` markers were found in any scoped source tree. Assessment-reported API/package issues are deferred to later modernization work; this task makes no API or package changes.

## Decomposition Assessment

- Execution guidance evaluated: `execution.md`.
- Breakdown hints evaluated: `breakdown-hints/common.md` and `breakdown-hints/framework-migration.md`.
- The common multi-project ordering hint applies, but this task is already the planned leaf/dependency-tier unit. No System.Web or other framework-migration split condition applies; no stub or package-replacement trigger applies. Verdict: `atomic`.

## Already-Done Check

All four scoped project files are SDK-style in `HEAD`, so the SDK conversion tool is not invoked again. The remaining work is focused validation and verification of solution paths, project metadata, and the adjacent `packages.config` state.
