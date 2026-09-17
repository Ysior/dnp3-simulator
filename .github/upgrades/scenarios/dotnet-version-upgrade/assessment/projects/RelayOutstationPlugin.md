# simulator\DNP3\RelayOutstationPlugin\RelayOutstationPlugin.csproj

[← Back to the assessment index](../../assessment.md)

## Project Info

- **Current Target Framework:** net7.0
- **Proposed Target Framework:** net8.0-windows
- **SDK-style**: False
- **Project Kind:** ClassicWinForms
- **Dependencies**: 3
- **Dependants**: 1
- **Number of Files**: 7
- **Number of Files with Incidents**: 4
- **Lines of Code**: 666
- **Estimated LOC to modify**: 279+ (at least 41,9% of the project)

## Related Projects

**Depends on (3)** — projects this one references:

- [D:\DNP3\dnp3-simulator\Simulator\DNP3\DNP3Commons\DNP3Commons.csproj](../projects/DNP3Commons.md)
- [D:\DNP3\dnp3-simulator\Simulator\DNP3\DNP3PluginAPI\DNP3PluginAPI.csproj](../projects/DNP3PluginAPI.md)
- [D:\DNP3\dnp3-simulator\Simulator\SimulatorAPI\ModuleAPI.csproj](../projects/ModuleAPI.md)

**Depended on by (1)** — projects that reference this one:

- [D:\DNP3\dnp3-simulator\Simulator\DNP3\DNP3Simulator\DNP3Module.csproj](../projects/DNP3Module.md)

## Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P8["<b>⚙️&nbsp;DNP3Module.csproj</b><br/><small>net7.0</small>"]
        click P8 "../projects/DNP3Module.md"
    end
    subgraph current["RelayOutstationPlugin.csproj"]
        MAIN["<b>⚙️&nbsp;RelayOutstationPlugin.csproj</b><br/><small>net7.0</small>"]
        click MAIN "../projects/RelayOutstationPlugin.md"
    end
    subgraph downstream["Dependencies (3)"]
        P3["<b>⚙️&nbsp;ModuleAPI.csproj</b><br/><small>net7.0</small>"]
        P7["<b>⚙️&nbsp;DNP3Commons.csproj</b><br/><small>net608</small>"]
        P4["<b>⚙️&nbsp;DNP3PluginAPI.csproj</b><br/><small>net7.0</small>"]
        click P3 "../projects/ModuleAPI.md"
        click P7 "../projects/DNP3Commons.md"
        click P4 "../projects/DNP3PluginAPI.md"
    end
    P8 --> MAIN
    MAIN --> P3
    MAIN --> P7
    MAIN --> P4

```

## API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 268 | High - Require code changes |
| 🟡 Source Incompatible | 11 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 386 |  |
| ***Total APIs Analyzed*** | ***665*** |  |

## NuGet Package Issues

| Package | Current Version | Suggested Version | Severity | Issue |
| :--- | :---: | :---: | :---: | :--- |
| opendnp3 | 2.2.0-M1 | 3.1.2 | 🔴 Mandatory | Pakiet NuGet jest niezgodny |

Every project affected by these packages, and the versions the repository settles on: [aggregate NuGet packages](../nuget/aggregate-packages.md).

## Binding Redirect Configuration

| Rule | Severity | Details | Recommendation |
| :--- | :---: | :--- | :--- |
| AutoGenerateBindingRedirects not set and no manual redirects | 🟡 Potential | AutoGenerateBindingRedirects is not set in RelayOutstationPlugin.csproj, no manual redirects found | Explicitly enable <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects> or add manual binding redirects. |

## Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| GDI+ / System.Drawing | 11 | 3,9% | System.Drawing APIs for 2D graphics, imaging, and printing that are available via NuGet package System.Drawing.Common. Note: Not recommended for server scenarios due to Windows dependencies; consider cross-platform alternatives like SkiaSharp or ImageSharp for new code. |
| Windows Forms | 268 | 96,1% | Windows Forms APIs for building Windows desktop applications with traditional Forms-based UI that are available in .NET on Windows. Enable Windows Desktop support: Option 1 (Recommended): Target net9.0-windows; Option 2: Add <UseWindowsDesktop>true</UseWindowsDesktop>; Option 3 (Legacy): Use Microsoft.NET.Sdk.WindowsDesktop SDK. |

