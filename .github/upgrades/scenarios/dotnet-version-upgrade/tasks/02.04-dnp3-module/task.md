# 02.04-dnp3-module: Convert and validate DNP3Module

## Objective
Process the DNP3Module project after shared libraries and both outstation plugins are validated.

## Scope
- `simulator/DNP3/DNP3Simulator/DNP3Module.csproj`

## Preconditions
Complete `02.03-relay-plugin`. The assessment reports DNP3Module as the dependent module with six project references, WinForms/resource behavior, and a remaining `packages.config`.

## Research Findings
- The solution contains `simulator/DNP3/DNP3Simulator/DNP3Module.csproj` and references it under the `DNP3Module` solution entry; the similarly named `DNP3Module — kopia.csproj` is not a solution project and is out of scope.
- The assessment project report confirms six dependencies: `Commons`, `ModuleAPI`, `DefaultOutstationPlugin`, `DNP3Commons`, `DNP3PluginAPI`, and `RelayOutstationPlugin`; `Simulator.csproj` is the sole dependant.
- The live project is already SDK-style and currently targets `net8.0-windows`, with `OutputType=WinExe`, `PlatformTarget=x64`, and `opendnp3` `3.1.2`. This differs from the assessment's original legacy inventory (`net7.0`, `opendnp3` `2.2.0-M1`), so no TFM or package upgrade is in scope here.
- The live project currently has no `ProjectReference` or explicit WinForms/resource metadata. The legacy reference copy and project contents identify six project edges, 76 project files, designer-generated WinForms code, embedded `.resx` resources, and `Properties\\Resources.Designer.cs` that must remain included. No `// STUB:` markers were found.
- Package management is per-project; the adjacent `packages.config` still declares `opendnp3` `2.2.0-M1`, while the live project already declares `opendnp3` `3.1.2`. Conversion mechanics may remove `packages.config`, but package/API reconciliation is deferred and must not be broadened here.
- Decomposition verdict: atomic. Evaluated `execution.md`, `breakdown-hints/common.md`, and `breakdown-hints/framework-migration.md`; the task has one project, no stub-resolution trigger, no System.Web dependency, and no multi-project/package-replacement scope.

## Steps
1. Inspect the solution membership and existing project references before conversion; do not use or convert similarly named non-solution projects.
2. Invoke the SDK conversion workflow sequentially for DNP3Module if it is not already SDK-style.
3. Preserve all six assessment-identified project-reference edges, current TFM, WinForms designer/resource inputs, native/runtime assets, and existing package versions/management semantics.
4. Migrate or remove only conversion-owned package configuration as appropriate; leave package/API upgrade findings for later tasks.
5. Restore/build DNP3Module and its dependency closure against the current targets. Verify the solution still references this project and no converted-project package configuration is stale.

## Done Condition
DNP3Module is SDK-style, all intended references and inputs are retained, and focused module/dependency validation succeeds or records a concrete blocker.
