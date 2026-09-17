# simulator\SimulatorAPI\ModuleAPI.csproj

[← Back to the assessment index](../../assessment.md)

## Project Info

- **Current Target Framework:** net7.0
- **Proposed Target Framework:** net8.0-windows
- **SDK-style**: False
- **Project Kind:** ClassicWinForms
- **Dependencies**: 1
- **Dependants**: 5
- **Number of Files**: 5
- **Number of Files with Incidents**: 2
- **Lines of Code**: 200
- **Estimated LOC to modify**: 4+ (at least 2,0% of the project)

## Related Projects

**Depends on (1)** — projects this one references:

- [D:\DNP3\dnp3-simulator\Simulator\Commons\Commons.csproj](../projects/Commons.md)

**Depended on by (5)** — projects that reference this one:

- [D:\DNP3\dnp3-simulator\Simulator\DNP3\DefaultOutstationPlugin\DefaultOutstationPlugin.csproj](../projects/DefaultOutstationPlugin.md)
- [D:\DNP3\dnp3-simulator\Simulator\DNP3\DNP3PluginAPI\DNP3PluginAPI.csproj](../projects/DNP3PluginAPI.md)
- [D:\DNP3\dnp3-simulator\Simulator\DNP3\DNP3Simulator\DNP3Module.csproj](../projects/DNP3Module.md)
- [D:\DNP3\dnp3-simulator\Simulator\DNP3\RelayOutstationPlugin\RelayOutstationPlugin.csproj](../projects/RelayOutstationPlugin.md)
- [D:\DNP3\dnp3-simulator\Simulator\Simulator\Simulator.csproj](../projects/Simulator.md)

## Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (5)"]
        P4["<b>⚙️&nbsp;DNP3PluginAPI.csproj</b><br/><small>net7.0</small>"]
        P5["<b>⚙️&nbsp;DefaultOutstationPlugin.csproj</b><br/><small>net7.0</small>"]
        P6["<b>⚙️&nbsp;RelayOutstationPlugin.csproj</b><br/><small>net7.0</small>"]
        P8["<b>⚙️&nbsp;DNP3Module.csproj</b><br/><small>net7.0</small>"]
        P9["<b>⚙️&nbsp;Simulator.csproj</b><br/><small>net7.0</small>"]
        click P4 "../projects/DNP3PluginAPI.md"
        click P5 "../projects/DefaultOutstationPlugin.md"
        click P6 "../projects/RelayOutstationPlugin.md"
        click P8 "../projects/DNP3Module.md"
        click P9 "../projects/Simulator.md"
    end
    subgraph current["ModuleAPI.csproj"]
        MAIN["<b>⚙️&nbsp;ModuleAPI.csproj</b><br/><small>net7.0</small>"]
        click MAIN "../projects/ModuleAPI.md"
    end
    subgraph downstream["Dependencies (1)"]
        P1["<b>⚙️&nbsp;Commons.csproj</b><br/><small>net7.0</small>"]
        click P1 "../projects/Commons.md"
    end
    P4 --> MAIN
    P5 --> MAIN
    P6 --> MAIN
    P8 --> MAIN
    P9 --> MAIN
    MAIN --> P1

```

## API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 2 | High - Require code changes |
| 🟡 Source Incompatible | 2 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 98 |  |
| ***Total APIs Analyzed*** | ***102*** |  |

## Binding Redirect Configuration

| Rule | Severity | Details | Recommendation |
| :--- | :---: | :--- | :--- |
| AutoGenerateBindingRedirects not set and no manual redirects | 🟡 Potential | AutoGenerateBindingRedirects is not set in ModuleAPI.csproj, no manual redirects found | Explicitly enable <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects> or add manual binding redirects. |

## Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| Windows Forms | 2 | 50,0% | Windows Forms APIs for building Windows desktop applications with traditional Forms-based UI that are available in .NET on Windows. Enable Windows Desktop support: Option 1 (Recommended): Target net9.0-windows; Option 2: Add <UseWindowsDesktop>true</UseWindowsDesktop>; Option 3 (Legacy): Use Microsoft.NET.Sdk.WindowsDesktop SDK. |
| GDI+ / System.Drawing | 2 | 50,0% | System.Drawing APIs for 2D graphics, imaging, and printing that are available via NuGet package System.Drawing.Common. Note: Not recommended for server scenarios due to Windows dependencies; consider cross-platform alternatives like SkiaSharp or ImageSharp for new code. |

