# 02-sdk-style-conversion: Convert legacy project files

Convert all 9 non-SDK-style project files to SDK-style while preserving project references, WinForms resource and designer behavior, native/runtime assets, and the existing per-project package management choice. Keep this separate from the target-framework upgrade so conversion issues can be isolated before framework and API changes are applied.

**Done when**: Every project in the solution is SDK-style, all intended source/resource/project-reference inputs remain included, and the solution still restores and builds against its current targets.

## Research Findings

The solution `simulator.sln` contains exactly 9 projects. The assessment's original inventory reports all 9 as non-SDK-style and shows this dependency chain: Commons, ModuleAPI, and SimulatorUI are leaves; DNP3PluginAPI depends on ModuleAPI; DNP3Commons is a leaf; DefaultOutstationPlugin and RelayOutstationPlugin depend on shared APIs; DNP3Module depends on the shared libraries and plugins; Simulator is the application root.

The working tree is already dirty from earlier scenario work. A current inspection (before this task's source edits) found:

| Solution project | Current file state | Current TFM | Package management | Project references observed |
|---|---|---|---|---|
| `simulator/Commons/Commons.csproj` | SDK-style | `net8.0` | `PackageReference` (`opendnp3` 3.1.2) | none |
| `simulator/SimulatorUI/SimulatorUI.csproj` | SDK-style | `net8.0` | `PackageReference` (`opendnp3` 3.1.2) | none in current file; verify against prior references |
| `simulator/SimulatorAPI/ModuleAPI.csproj` | SDK-style WindowsDesktop | `net8.0-windows` | `PackageReference` (`opendnp3` 3.1.2) | none |
| `simulator/DNP3/DNP3PluginAPI/DNP3PluginAPI.csproj` | SDK-style | `net8.0` | `PackageReference` (`opendnp3` 3.1.2) | none in current file; assessment says ModuleAPI |
| `simulator/DNP3/DNP3Commons/DNP3Commons.csproj` | SDK-style | `net8.0` | `packages.config` still exists and `PackageReference` is present | none |
| `simulator/DNP3/DefaultOutstationPlugin/DefaultOutstationPlugin.csproj` | SDK-style | `net8.0-windows` | `PackageReference` (`opendnp3` 3.1.2) | ModuleAPI, Commons |
| `simulator/DNP3/RelayOutstationPlugin/RelayOutstationPlugin.csproj` | legacy | legacy `net7.0` entry plus `TargetFrameworkVersion` `v4.5` | `packages.config` | ModuleAPI, DNP3Commons, DNP3PluginAPI |
| `simulator/DNP3/DNP3Simulator/DNP3Module.csproj` | SDK-style | `net8.0-windows` | `packages.config` still exists and `PackageReference` is present | none in current file; assessment says 6 project references |
| `simulator/Simulator/Simulator.csproj` | SDK-style WindowsDesktop | `net8.0-windows` | `PackageReference` (`opendnp3` 3.1.2) | none in current file; assessment says DNP3Module, Commons, ModuleAPI, SimulatorUI |

Four `packages.config` files remain beside DNP3Commons, DNP3PluginAPI, DNP3Module, and RelayOutstationPlugin. The repository also contains similarly named `— kopia.csproj` files that are not solution members and must not be converted as part of this task. No `// STUB:` markers were found under `simulator`.

Relevant assessment details: DNP3Commons has 192 binary API findings and RelayOutstationPlugin has 268 binary plus 11 source findings; both use WinForms/GDI+ and the incompatible `opendnp3` package. DNP3Module has 7,054 binary and 140 source findings and is WinForms; DNP3PluginAPI has no API findings but uses `opendnp3`. These API/package upgrades are out of scope here. Conversion must preserve WinForms designer `.cs` and `.resx` inputs, native/runtime references/assets, project-reference edges, and existing package versions/management semantics. Current target frameworks must not be changed by this task; any already-present TFM/package changes are pre-existing and must be reported, not expanded.

## Conversion and Validation Approach

Use the SDK conversion tool sequentially in tool-provided bottom-up dependency order, building each converted project directly with restore after conversion. Because the projects contain WinForms resources and .NET Framework-era references, use full Visual Studio `msbuild.exe` when the SDK build cannot process resources or classic TFMs. Verify after each conversion that project references and resource/designer inputs remain, and that no converted project's `packages.config` remains. Finish with solution restore/build against the current checked-in targets.

## Decomposition Assessment

The execution guidance and `breakdown-hints/common.md` were evaluated. This task affects 9 projects with a dependency chain, so it matches the mandatory 4+ project SDK-conversion rule and the multi-project dependency-ordering hint. It is not one coherent unit: split by dependency tier with validation between tiers. No stub-resolution or package-replacement decomposition applies.
