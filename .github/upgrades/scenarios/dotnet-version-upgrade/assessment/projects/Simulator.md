# Simulator\Simulator\Simulator.csproj

[← Back to the assessment index](../../assessment.md)

## Project Info

- **Current Target Framework:** net7.0
- **Proposed Target Framework:** net8.0-windows
- **SDK-style**: False
- **Project Kind:** ClassicWinForms
- **Dependencies**: 4
- **Dependants**: 0
- **Number of Files**: 19
- **Number of Files with Incidents**: 13
- **Lines of Code**: 1373
- **Estimated LOC to modify**: 1088+ (at least 79,2% of the project)

## Related Projects

**Depends on (4)** — projects this one references:

- [D:\DNP3\dnp3-simulator\Simulator\Commons\Commons.csproj](../projects/Commons.md)
- [D:\DNP3\dnp3-simulator\Simulator\DNP3\DNP3Simulator\DNP3Module.csproj](../projects/DNP3Module.md)
- [D:\DNP3\dnp3-simulator\Simulator\SimulatorAPI\ModuleAPI.csproj](../projects/ModuleAPI.md)
- [D:\DNP3\dnp3-simulator\Simulator\SimulatorUI\SimulatorUI.csproj](../projects/SimulatorUI.md)

## Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["Simulator.csproj"]
        MAIN["<b>⚙️&nbsp;Simulator.csproj</b><br/><small>net7.0</small>"]
        click MAIN "../projects/Simulator.md"
    end
    subgraph downstream["Dependencies (4)"]
        P1["<b>⚙️&nbsp;Commons.csproj</b><br/><small>net7.0</small>"]
        P8["<b>⚙️&nbsp;DNP3Module.csproj</b><br/><small>net7.0</small>"]
        P3["<b>⚙️&nbsp;ModuleAPI.csproj</b><br/><small>net7.0</small>"]
        P2["<b>⚙️&nbsp;SimulatorUI.csproj</b><br/><small>net7.0</small>"]
        click P1 "../projects/Commons.md"
        click P8 "../projects/DNP3Module.md"
        click P3 "../projects/ModuleAPI.md"
        click P2 "../projects/SimulatorUI.md"
    end
    MAIN --> P1
    MAIN --> P8
    MAIN --> P3
    MAIN --> P2

```

## API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 1050 | High - Require code changes |
| 🟡 Source Incompatible | 38 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 1203 |  |
| ***Total APIs Analyzed*** | ***2291*** |  |

## Binding Redirect Configuration

| Rule | Severity | Details | Recommendation |
| :--- | :---: | :--- | :--- |
| AutoGenerateBindingRedirects not set and no manual redirects | 🟡 Potential | AutoGenerateBindingRedirects is not set in Simulator.csproj, no manual redirects found | Explicitly enable <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects> or add manual binding redirects. |

## Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| Legacy Configuration System | 2 | 0,2% | Legacy XML-based configuration system (app.config/web.config) that has been replaced by a more flexible configuration model in .NET Core. The old system was rigid and XML-based. Migrate to Microsoft.Extensions.Configuration with JSON/environment variables; use System.Configuration.ConfigurationManager NuGet package as interim bridge if needed. |
| Windows Forms Legacy Controls | 20 | 1,8% | Legacy Windows Forms controls that have been removed from .NET Core/5+ including StatusBar, DataGrid, ContextMenu, MainMenu, MenuItem, and ToolBar. These controls were replaced by more modern alternatives. Use ToolStrip, MenuStrip, ContextMenuStrip, and DataGridView instead. |
| GDI+ / System.Drawing | 36 | 3,3% | System.Drawing APIs for 2D graphics, imaging, and printing that are available via NuGet package System.Drawing.Common. Note: Not recommended for server scenarios due to Windows dependencies; consider cross-platform alternatives like SkiaSharp or ImageSharp for new code. |
| Windows Forms | 1050 | 96,5% | Windows Forms APIs for building Windows desktop applications with traditional Forms-based UI that are available in .NET on Windows. Enable Windows Desktop support: Option 1 (Recommended): Target net9.0-windows; Option 2: Add <UseWindowsDesktop>true</UseWindowsDesktop>; Option 3 (Legacy): Use Microsoft.NET.Sdk.WindowsDesktop SDK. |

