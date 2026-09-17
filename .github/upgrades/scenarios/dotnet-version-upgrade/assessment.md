# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v8.0.

Detailed findings live alongside this file in `assessment/`. This page is the index: read it first, then open only the documents you need.

## Table of Contents

- [Executive Summary](#executive-summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
  - [Binding Redirect Configuration](#binding-redirect-configuration)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Detailed Reports](#detailed-reports)
  - [Projects Relationship Graph](assessment/project-graph.md)
  - [Aggregate NuGet packages details](assessment/nuget/aggregate-packages.md)
  - [Most Frequent API Issues (complete list)](assessment/api-issues/most-frequent-api-issues.md)
  - [Project Details](#project-details)

## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 9 | All require upgrade |
| Total NuGet Packages | 1 | All packages need upgrade |
| Total Code Files | 131 |  |
| Total Code Files with Incidents | 101 |  |
| Total Lines of Code | 14957 |  |
| Total Number of Issues | 11589 |  |
| Proposed Target Framework | net8.0, net8.0-windows |  |
| Estimated LOC to modify | 11557+ | at least 77,3% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Test Coverage | Package Issues | API Issues | Binding Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| [Simulator\Commons\Commons.csproj](assessment/projects/Commons.md) | net7.0 | 🟢 Low | — | 1 | 0 | 1 |  | ClassicClassLibrary, Sdk Style = False |
| [Simulator\DNP3\DefaultOutstationPlugin\DefaultOutstationPlugin.csproj](assessment/projects/DefaultOutstationPlugin.md) | net7.0 | 🟡 Medium | 🧪 Recommended | 1 | 2041 | 1 | 2041+ | ClassicWinForms, Sdk Style = False |
| [simulator\DNP3\DNP3Commons\DNP3Commons.csproj](assessment/projects/DNP3Commons.md) | net608 | 🟡 Medium | 🧪 Recommended | 1 | 192 | 0 | 192+ | ClassicWinForms, Sdk Style = False |
| [simulator\DNP3\DNP3PluginAPI\DNP3PluginAPI.csproj](assessment/projects/DNP3PluginAPI.md) | net7.0 | 🟢 Low | — | 1 | 0 | 1 |  | ClassicClassLibrary, Sdk Style = False |
| [Simulator\DNP3\DNP3Simulator\DNP3Module.csproj](assessment/projects/DNP3Module.md) | net7.0 | 🟡 Medium | 🧪 Recommended | 1 | 7194 | 1 | 7194+ | ClassicWinForms, Sdk Style = False |
| [simulator\DNP3\RelayOutstationPlugin\RelayOutstationPlugin.csproj](assessment/projects/RelayOutstationPlugin.md) | net7.0 | 🟡 Medium | 🧪 Recommended | 1 | 279 | 1 | 279+ | ClassicWinForms, Sdk Style = False |
| [Simulator\Simulator\Simulator.csproj](assessment/projects/Simulator.md) | net7.0 | 🟡 Medium | 🧪 Recommended | 0 | 1088 | 1 | 1088+ | ClassicWinForms, Sdk Style = False |
| [simulator\SimulatorAPI\ModuleAPI.csproj](assessment/projects/ModuleAPI.md) | net7.0 | 🟢 Low | 🧪 Recommended | 0 | 4 | 1 | 4+ | ClassicWinForms, Sdk Style = False |
| [simulator\SimulatorUI\SimulatorUI.csproj](assessment/projects/SimulatorUI.md) | net7.0 | 🟡 Medium | 🧪 Recommended | 0 | 759 | 1 | 759+ | ClassicWinForms, Sdk Style = False |

🧪 **Test Coverage** — projects risky enough to add behavior-locking tests before upgrading, to catch regressions the upgrade may introduce. Requires the **dotnet-test** plugin.

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 0 | 0,0% |
| ⚠️ Incompatible | 1 | 100,0% |
| 🔄 Upgrade Recommended | 0 | 0,0% |
| ***Total NuGet Packages*** | ***1*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 11214 | High - Require code changes |
| 🟡 Source Incompatible | 343 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 13991 |  |
| ***Total APIs Analyzed*** | ***25548*** |  |

### Binding Redirect Configuration

| Severity | Count | Description |
| :--- | :---: | :--- |
| 🟡Potential | 8 | May cause issues in certain scenarios |
| ***Total Binding Issues*** | ***8*** | ***Across 8 project(s)*** |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| Windows Forms | 11214 | 97,0% | Windows Forms APIs for building Windows desktop applications with traditional Forms-based UI that are available in .NET on Windows. Enable Windows Desktop support: Option 1 (Recommended): Target net9.0-windows; Option 2: Add <UseWindowsDesktop>true</UseWindowsDesktop>; Option 3 (Legacy): Use Microsoft.NET.Sdk.WindowsDesktop SDK. |
| GDI+ / System.Drawing | 335 | 2,9% | System.Drawing APIs for 2D graphics, imaging, and printing that are available via NuGet package System.Drawing.Common. Note: Not recommended for server scenarios due to Windows dependencies; consider cross-platform alternatives like SkiaSharp or ImageSharp for new code. |
| Windows Forms Legacy Controls | 23 | 0,2% | Legacy Windows Forms controls that have been removed from .NET Core/5+ including StatusBar, DataGrid, ContextMenu, MainMenu, MenuItem, and ToolBar. These controls were replaced by more modern alternatives. Use ToolStrip, MenuStrip, ContextMenuStrip, and DataGridView instead. |
| Legacy Configuration System | 4 | 0,0% | Legacy XML-based configuration system (app.config/web.config) that has been replaced by a more flexible configuration model in .NET Core. The old system was rigid and XML-based. Migrate to Microsoft.Extensions.Configuration with JSON/environment variables; use System.Configuration.ConfigurationManager NuGet package as interim bridge if needed. |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |
| T:System.Windows.Forms.Label | 711 | 6,2% | Binary Incompatible |
| T:System.Windows.Forms.Button | 520 | 4,5% | Binary Incompatible |
| T:System.Windows.Forms.NumericUpDown | 483 | 4,2% | Binary Incompatible |
| T:System.Windows.Forms.CheckBox | 474 | 4,1% | Binary Incompatible |
| T:System.Windows.Forms.ComboBox | 369 | 3,2% | Binary Incompatible |
| P:System.Windows.Forms.Control.Name | 364 | 3,1% | Binary Incompatible |
| T:System.Windows.Forms.GroupBox | 349 | 3,0% | Binary Incompatible |
| P:System.Windows.Forms.Control.Size | 347 | 3,0% | Binary Incompatible |
| T:System.Windows.Forms.TabPage | 345 | 3,0% | Binary Incompatible |
| M:System.Windows.Forms.Control.ControlCollection.Add(System.Windows.Forms.Control) | 325 | 2,8% | Binary Incompatible |

The table above is the top 10. See [the complete list](assessment/api-issues/most-frequent-api-issues.md) for every affected API.

## Detailed Reports

- [Projects Relationship Graph](assessment/project-graph.md)
- [Aggregate NuGet packages details](assessment/nuget/aggregate-packages.md)
- [Most Frequent API Issues (complete list)](assessment/api-issues/most-frequent-api-issues.md)

### Project Details

- [Simulator\Commons\Commons.csproj](assessment/projects/Commons.md)
- [Simulator\DNP3\DefaultOutstationPlugin\DefaultOutstationPlugin.csproj](assessment/projects/DefaultOutstationPlugin.md)
- [simulator\DNP3\DNP3Commons\DNP3Commons.csproj](assessment/projects/DNP3Commons.md)
- [simulator\DNP3\DNP3PluginAPI\DNP3PluginAPI.csproj](assessment/projects/DNP3PluginAPI.md)
- [Simulator\DNP3\DNP3Simulator\DNP3Module.csproj](assessment/projects/DNP3Module.md)
- [simulator\DNP3\RelayOutstationPlugin\RelayOutstationPlugin.csproj](assessment/projects/RelayOutstationPlugin.md)
- [Simulator\Simulator\Simulator.csproj](assessment/projects/Simulator.md)
- [simulator\SimulatorAPI\ModuleAPI.csproj](assessment/projects/ModuleAPI.md)
- [simulator\SimulatorUI\SimulatorUI.csproj](assessment/projects/SimulatorUI.md)


