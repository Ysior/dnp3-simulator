# 02.02-shared-api-projects: Validate or convert shared API projects

## Objective
Process the second dependency tier after the leaf projects. Ensure the shared API projects are SDK-style and validate their dependency edges before downstream plugin conversion.

## Scope
- `simulator/DNP3/DNP3PluginAPI/DNP3PluginAPI.csproj` (depends on `simulator/SimulatorAPI/ModuleAPI.csproj`)
- `simulator/DNP3/DefaultOutstationPlugin/DefaultOutstationPlugin.csproj` (depends on ModuleAPI and Commons)

## Preconditions
Complete `02.01-leaf-projects` and use its validated project state. Existing SDK-style changes are pre-existing work and must be preserved.

## Research Findings (2026-09-18)
- `DNP3PluginAPI.csproj` is already SDK-style in the working tree, but its current contents are a minimal executable project targeting `net8.0`, with `opendnp3` `3.1.2` and no `ModuleAPI` reference. This differs from the assessed/required dependency edge and from `HEAD`'s legacy project, so it must be repaired without broadening scope.
- `DefaultOutstationPlugin.csproj` is already SDK-style and targets `net8.0-windows` with WinForms enabled, `PlatformTarget=x64`, and `opendnp3` `3.1.2`. It retains `ModuleAPI` and `Commons` references in the working tree; its removed legacy `DNP3.API` reference is not part of this task's required graph.
- Assessment reports: `DNP3PluginAPI` current TFM `net7.0`, SDK-style `false`, no API issues, one incompatible `opendnp3` package issue; `DefaultOutstationPlugin` current TFM `net7.0`, SDK-style `false`, 1,928 binary and 113 source API issues, and the same package issue. This task addresses project shape and dependency preservation only; no API or package upgrade is applied.
- Existing leaf-project work converted `ModuleAPI` and `Commons` to SDK-style. `DNP3Commons` remains outside this task and is not a required direct reference of `DefaultOutstationPlugin` in the current repository layout.
- No `// STUB:` markers were found in either project scope. No `packages.config` exists beside either target project; the repository-level `packages` assets and native `opendnp3` runtime layout must remain untouched.
- Dependency order is `ModuleAPI`/`Commons` -> `DNP3PluginAPI` -> `DefaultOutstationPlugin`; focused restore/build will validate both target projects and these processed dependencies.
- Assessment detail query was unavailable in this worker tool surface; per-project narrative documents above are the available assessment evidence.

## Decomposition Assessment
- Loaded execution guidance and `breakdown-hints/common.md` plus `breakdown-hints/framework-migration.md`.
- Common multi-project ordering hint applies in principle, but the two projects form one coherent SDK-style/dependency-edge validation tier and no 3+ project chain or package replacement is in scope.
- Windows API isolation is present in `DefaultOutstationPlugin`, but API migration is explicitly deferred; no separate implementation concern is introduced.
- Verdict: `atomic`; no TaskBreaker escalation.

## Steps
1. Inspect each project and convert only a remaining legacy project, sequentially, using the SDK conversion workflow.
2. Confirm `DNP3PluginAPI` retains its ModuleAPI project reference and `DefaultOutstationPlugin` retains ModuleAPI and Commons references.
3. Preserve current TFMs, package versions/management choice, native/runtime assets, and any WinForms/resource/designer inputs. Do not apply API or package upgrades.
4. Verify any package-config migration required by the conversion and ensure no stale converted-project package configuration remains.
5. Restore/build both projects and their already-processed dependencies against current targets.

## Done Condition
Both projects are SDK-style, dependency edges and project inputs are intact, and focused tier restore/build validation succeeds or records a concrete blocker.
