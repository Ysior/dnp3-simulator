# 04-library-consolidation: Consolidate shared libraries on .NET 8

After the upgraded `Simulator` path is green, remove temporary old-target entries from shared libraries and clean up only conditional compilation that exists solely for target-framework differences. Preserve any conditional symbols that represent application behavior, compatibility controls, or other non-TFM concerns, and report unclear symbols for execution review.

Apply the confirmed per-project package approach throughout this task. Record central package management as a post-migration recommendation only after all projects are SDK-style and on a single TFM, when it can be introduced without `VersionOverride` friction.

**Done when**: No shared library retains an unnecessary old target, TFM-only conditional code is removed, non-TFM behavior guards remain intact, and all project references resolve against the consolidated .NET 8 targets.

## Research Findings

- The solution contains nine project entries. The shared libraries in scope are `simulator/Commons/Commons.csproj`, `simulator/SimulatorAPI/ModuleAPI.csproj`, `simulator/DNP3/DNP3PluginAPI/DNP3PluginAPI.csproj`, and `simulator/DNP3/DNP3Commons/DNP3Commons.csproj`.
- Verified current solution TFMs: `Commons` is `net8.0`; `ModuleAPI`, `DNP3PluginAPI`, and `DNP3Commons` are `net8.0-windows`. Their dependent application/plugin projects are also already on `net8.0-windows`.
- The only old-target entry found is `TargetFrameworkVersion` `v6.0.8` in `simulator/DNP3/DNP3Commons/DNP3Commons — kopia.csproj`. This duplicate is not referenced by `simulator.sln` or any `ProjectReference`, so it is excluded from consolidation.
- All in-scope project files use a singular `TargetFramework`; no unnecessary `TargetFrameworks` multi-targeting remains. No TFM-specific `#if`, `#elif`, or `#define` guards were found under `simulator`. Existing configuration/platform `Condition` groups in project files are non-TFM behavior/build configuration and must remain.
- Package management is per-project: `opendnp3` `3.1.2` is declared directly in the affected project files (with `System.IO.Ports` and `System.Resources.Extensions` where applicable). No `Directory.Packages.props` or central package management change is warranted for this task.
- No `// STUB:` markers were found in the affected source trees. Assessment reports show the shared-library package action already resolved to `opendnp3` `3.1.2`; `DNP3Commons`'s remaining assessment findings are Windows Forms API migration findings, not TFM consolidation work.

## Decomposition Verdict

The execution guidance and `breakdown-hints/common.md` plus `breakdown-hints/framework-migration.md` were evaluated. The dependency-ordering hint applies in principle, but this task's source objective is already satisfied across the solution and there are no stubs, package replacements, or TFM-specific code paths to split. The task is therefore treated as atomic audit/validation work. `TaskBreaker` was unavailable in the current tool set.
