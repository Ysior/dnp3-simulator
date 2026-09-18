# 03.04-entrypoint-validation: Upgrade Simulator entry point and validate application path

## Objective
Complete the Simulator entry-point conversion and validate the full upgraded dependency path.

## Scope
Simulator.csproj and any final integration fixes required by the already-upgraded dependency tiers.

## Research Findings

- The actual entry-point project is `simulator/Simulator/Simulator.csproj` and the solution path is `simulator.sln`.
- `Simulator.csproj` already targets `net8.0-windows`, uses `WinExe`, references `opendnp3` `3.1.2`, and points to the upgraded dependency path: `Commons`, `DNP3Module`, `ModuleAPI`, and `SimulatorUI`.
- The entry-point project still uses the legacy `Microsoft.NET.Sdk.WindowsDesktop` SDK and declares `UseWindowsForms` twice. The modern SDK with one `UseWindowsForms` declaration is the required final shape.
- `simulator/Simulator/Program.cs` has the real WinForms entry point (`[STAThread]`, visual styles, plugin factory creation, and `Application.Run`); no stub marker was found in this project.
- `simulator/Simulator/App.config` contains only the .NET Framework 4.5 `supportedRuntime` declaration. It is legacy configuration and must be removed; no binding redirect should be introduced.
- Required entry-point resources are present, including `automatak.ico` and the WinForms `.resx` files. No native DNP3 output was present in the currently inspected `DNP3Module` Debug output, so the build output must be checked for the package runtime assets after restore/build.
- The full solution contains nine projects with the application dependency chain documented in the assessment. This task is atomic because the source change is confined to the entry-point project/configuration; dependency projects are validation inputs already upgraded by preceding tasks.

## Decomposition Assessment

- Evaluated `execution.md` and `breakdown-hints/common.md`.
- The multi-project dependency-ordering hint was considered but does not trigger decomposition here: this is final entry-point alignment and validation, not independent TFM/package migration across the dependency tiers.
- No `// STUB:` markers were found under `simulator/Simulator`; stub-resolution decomposition is not applicable.

## Current Research Verification

- Focused build command: `dotnet build simulator/DNP3/DNP3Simulator/DNP3Module.csproj --configuration Debug --no-incremental`.
- The dependency projects restore and compile, while `DNP3Module` currently reports 42 errors against opendnp3 3.1.2.
- Affected source areas are `ChannelNode.cs`, `Components/ChannelDialog.cs`, `Components/CROBControl.cs`, `Components/EventBufferConfigControl.cs`, `Components/LinkConfigControl.cs`, `Components/OutstationDialog.cs`, `Components/ScanDialog.cs`, `Components/TLSOptionsControl.cs`, `Components/TemplateDialog.cs`, and `BundledPlugins/MasterForm.cs` (missing entry point).
- API changes to preserve behavior include listener registration via current channel creation APIs, `ChannelRetry`'s three durations, current serial/TCP/TLS/UDP overloads, `ControlRelayOutputBlock(OperationType, TripCloseCode, bool, byte, uint, uint)`, `LinkConfig` current properties, keyed `DatabaseTemplate` configuration, `IMasterOperations.Scan`/`AddScan` task configuration, and current outstation unsolicited-response properties.
- Existing warnings are nullable/obsolete diagnostics in the migrated module; no warning suppression or stub implementation is permitted. The first discriminating check after edits is the focused `DNP3Module` build, followed by `Simulator` and `simulator.sln` builds.

## Steps
1. Align the entry-point project with net8.0-windows and WinForms settings.
2. Remove legacy binding redirect configuration and resolve remaining compile issues.
3. Build the application path and full solution as final validation.
4. Record native/resource validation limitations if runtime launch is unavailable.

## Done when
The entry point and dependency path build successfully with opendnp3 3.1.2, preserved resources/native assets, and no stubs.
