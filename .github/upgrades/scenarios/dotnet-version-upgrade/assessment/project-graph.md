# Projects Relationship Graph

[← Back to the assessment index](../assessment.md)

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>⚙️&nbsp;Commons.csproj</b><br/><small>net7.0</small>"]
    P2["<b>⚙️&nbsp;SimulatorUI.csproj</b><br/><small>net7.0</small>"]
    P3["<b>⚙️&nbsp;ModuleAPI.csproj</b><br/><small>net7.0</small>"]
    P4["<b>⚙️&nbsp;DNP3PluginAPI.csproj</b><br/><small>net7.0</small>"]
    P5["<b>⚙️&nbsp;DefaultOutstationPlugin.csproj</b><br/><small>net7.0</small>"]
    P6["<b>⚙️&nbsp;RelayOutstationPlugin.csproj</b><br/><small>net7.0</small>"]
    P7["<b>⚙️&nbsp;DNP3Commons.csproj</b><br/><small>net608</small>"]
    P8["<b>⚙️&nbsp;DNP3Module.csproj</b><br/><small>net7.0</small>"]
    P9["<b>⚙️&nbsp;Simulator.csproj</b><br/><small>net7.0</small>"]
    P2 --> P1
    P3 --> P1
    P4 --> P3
    P5 --> P3
    P5 --> P7
    P5 --> P4
    P6 --> P3
    P6 --> P7
    P6 --> P4
    P8 --> P1
    P8 --> P3
    P8 --> P5
    P8 --> P7
    P8 --> P4
    P8 --> P6
    P9 --> P1
    P9 --> P8
    P9 --> P3
    P9 --> P2
    click P1 "projects/Commons.md"
    click P2 "projects/SimulatorUI.md"
    click P3 "projects/ModuleAPI.md"
    click P4 "projects/DNP3PluginAPI.md"
    click P5 "projects/DefaultOutstationPlugin.md"
    click P6 "projects/RelayOutstationPlugin.md"
    click P7 "projects/DNP3Commons.md"
    click P8 "projects/DNP3Module.md"
    click P9 "projects/Simulator.md"

```

