# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade the 9-project `simulator.sln` solution from `net7.0`/`net608` to .NET 8, using `net8.0` for libraries and `net8.0-windows` for WinForms projects.
**Scope**: Large legacy WinForms solution with 131 code files, approximately 14,957 lines, 1 incompatible NuGet package, and 11,589 reported issues, including 11,214 binary-incompatible APIs.

### Selected Strategy
**Top-Down (Application-First)** — Applications upgraded first, libraries multi-targeted temporarily.
**Rationale**: The solution has a deep dependency graph around the `Simulator` entry point and substantial WinForms migration work; upgrading the application path first keeps the work incrementally buildable while shared libraries are prepared.

**Applications, in priority order**: `Simulator/Simulator/Simulator.csproj` is the entry-point WinForms application. Its dependency path includes `SimulatorUI`, `DNP3Module`, `DefaultOutstationPlugin`, `RelayOutstationPlugin`, `DNP3Commons`, `DNP3PluginAPI`, `SimulatorAPI/ModuleAPI`, and `Commons`.

**Libraries requiring temporary multi-targeting**: Shared projects in the application dependency path, especially `Commons`, `SimulatorUI`, `ModuleAPI`, `DNP3PluginAPI`, `DNP3Commons`, `DefaultOutstationPlugin`, `RelayOutstationPlugin`, and `DNP3Module`, until the application no longer consumes the old target.

**Phase 2 trigger**: Begin library consolidation only after `Simulator` and every required dependency target .NET 8 and the upgraded application path builds successfully.

## Upgrade Options

| Option | Selected | Why |
|--------|----------|-----|
| Upgrade Strategy | Top-Down | The 9-project solution has a deep application dependency path and 11,589 reported issues, so incremental application-first migration is appropriate. |
| Package Management | Per-Project | The projects are old-style and package versions must remain independently manageable during conversion and migration. |
| Unsupported Packages | Resolve Inline | The assessment identifies only one incompatible package, `opendnp3`, making inline replacement or adaptation tractable. |
| Unsupported API Handling | Fix Inline | The assessment reports 11,557+ estimated lines affected, dominated by known WinForms and related API changes that must be resolved in place. |
| Windows Native APIs | Windows Compatibility Pack | The solution is a Windows Forms application and uses Windows-specific APIs including System.Drawing and legacy desktop controls. |
| Assembly Binding Redirects | Remove | The upgraded .NET applications should use modern dependency resolution rather than legacy binding redirect configuration. |
| Test Coverage | Skip | Test baseline generation was explicitly disabled for this upgrade. |

## Tasks

### 01-prerequisites: Verify the .NET 8 toolchain

Confirm that the .NET 8 SDK and required Windows desktop tooling are available, and verify that repository SDK selection is compatible with the `net8.0` and `net8.0-windows` targets. Establish the current solution build as the execution baseline without introducing test-baseline work.

**Done when**: The required .NET 8 SDK is available, SDK selection is compatible, and the solution's current build state and any existing blockers are recorded for comparison.

---

### 02-sdk-style-conversion: Convert legacy project files

Convert all 9 non-SDK-style project files to SDK-style while preserving project references, WinForms resource and designer behavior, native/runtime assets, and the existing per-project package management choice. Keep this separate from the target-framework upgrade so conversion issues can be isolated before framework and API changes are applied.

**Done when**: Every project in the solution is SDK-style, all intended source/resource/project-reference inputs remain included, and the solution still restores and builds against its current targets.

---

### 03-simulator-application: Upgrade the Simulator application path

Upgrade `Simulator/Simulator/Simulator.csproj` to `net8.0-windows` together with `SimulatorUI`, `DNP3Module`, `DefaultOutstationPlugin`, `RelayOutstationPlugin`, `DNP3Commons`, `DNP3PluginAPI`, `SimulatorAPI/ModuleAPI`, and `Commons` as needed by the entry point. Temporarily multi-target shared libraries when required to keep old consumers buildable, and update project settings for Windows Forms, System.Drawing, Windows compatibility support, configuration, resources, and native/runtime assets.

Resolve the incompatible `opendnp3` 2.2.0-M1 dependency inline across the six affected projects, using version 3.1.2 or a documented compatible adaptation. Fix the reported WinForms, legacy-control, System.Drawing, configuration, and other source/binary API issues inline, including the highest-risk areas in `DNP3Module` (7,194 estimated affected lines), `DefaultOutstationPlugin` (2,041+), and `Simulator` (1,088+). Remove legacy binding redirect configuration as part of the application conversion.

**Done when**: The entry-point application and its dependency path target .NET 8, the incompatible package is resolved in every affected project, all required API changes compile without deferred stubs, Windows Forms resources and native behavior are preserved, and the upgraded application path builds successfully.

---

### 04-library-consolidation: Consolidate shared libraries on .NET 8

After the upgraded `Simulator` path is green, remove temporary old-target entries from shared libraries and clean up only conditional compilation that exists solely for target-framework differences. Preserve any conditional symbols that represent application behavior, compatibility controls, or other non-TFM concerns, and report unclear symbols for execution review.

Apply the confirmed per-project package approach throughout this task. Record central package management as a post-migration recommendation only after all projects are SDK-style and on a single TFM, when it can be introduced without `VersionOverride` friction.

**Done when**: No shared library retains an unnecessary old target, TFM-only conditional code is removed, non-TFM behavior guards remain intact, and all project references resolve against the consolidated .NET 8 targets.

---

### 05-final-validation: Validate the upgraded solution

Run restore and a full solution build for `simulator.sln`, then inspect the resulting project graph and runtime outputs for missing references, resource regressions, native asset problems, package conflicts, and residual binding redirect configuration. Because test coverage was skipped, document that no generated behavioral baseline was run and capture any remaining manual verification recommendations.

**Done when**: The complete solution restores and builds without errors, all 9 projects target the intended .NET 8 framework variant, no incompatible package or legacy binding redirect remains, and residual risks or deferred recommendations are documented.
