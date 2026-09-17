# simulator\DNP3\DNP3PluginAPI\DNP3PluginAPI.csproj

[← Back to the assessment index](../../assessment.md)

## Project Info

- **Current Target Framework:** net7.0
- **Proposed Target Framework:** net8.0
- **SDK-style**: False
- **Project Kind:** ClassicClassLibrary
- **Dependencies**: 1
- **Dependants**: 3
- **Number of Files**: 2
- **Number of Files with Incidents**: 1
- **Lines of Code**: 117
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

## Related Projects

**Depends on (1)** — projects this one references:

- [D:\DNP3\dnp3-simulator\Simulator\SimulatorAPI\ModuleAPI.csproj](../projects/ModuleAPI.md)

**Depended on by (3)** — projects that reference this one:

- [D:\DNP3\dnp3-simulator\Simulator\DNP3\DefaultOutstationPlugin\DefaultOutstationPlugin.csproj](../projects/DefaultOutstationPlugin.md)
- [D:\DNP3\dnp3-simulator\Simulator\DNP3\DNP3Simulator\DNP3Module.csproj](../projects/DNP3Module.md)
- [D:\DNP3\dnp3-simulator\Simulator\DNP3\RelayOutstationPlugin\RelayOutstationPlugin.csproj](../projects/RelayOutstationPlugin.md)

## Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (3)"]
        P5["<b>⚙️&nbsp;DefaultOutstationPlugin.csproj</b><br/><small>net7.0</small>"]
        P6["<b>⚙️&nbsp;RelayOutstationPlugin.csproj</b><br/><small>net7.0</small>"]
        P8["<b>⚙️&nbsp;DNP3Module.csproj</b><br/><small>net7.0</small>"]
        click P5 "../projects/DefaultOutstationPlugin.md"
        click P6 "../projects/RelayOutstationPlugin.md"
        click P8 "../projects/DNP3Module.md"
    end
    subgraph current["DNP3PluginAPI.csproj"]
        MAIN["<b>⚙️&nbsp;DNP3PluginAPI.csproj</b><br/><small>net7.0</small>"]
        click MAIN "../projects/DNP3PluginAPI.md"
    end
    subgraph downstream["Dependencies (1)"]
        P3["<b>⚙️&nbsp;ModuleAPI.csproj</b><br/><small>net7.0</small>"]
        click P3 "../projects/ModuleAPI.md"
    end
    P5 --> MAIN
    P6 --> MAIN
    P8 --> MAIN
    MAIN --> P3

```

## API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 17 |  |
| ***Total APIs Analyzed*** | ***17*** |  |

## NuGet Package Issues

| Package | Current Version | Suggested Version | Severity | Issue |
| :--- | :---: | :---: | :---: | :--- |
| opendnp3 | 2.2.0-M1 | 3.1.2 | 🔴 Mandatory | Pakiet NuGet jest niezgodny |

Every project affected by these packages, and the versions the repository settles on: [aggregate NuGet packages](../nuget/aggregate-packages.md).

## Binding Redirect Configuration

| Rule | Severity | Details | Recommendation |
| :--- | :---: | :--- | :--- |
| AutoGenerateBindingRedirects not set and no manual redirects | 🟡 Potential | AutoGenerateBindingRedirects is not set in DNP3PluginAPI.csproj, no manual redirects found | Explicitly enable <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects> or add manual binding redirects. |

