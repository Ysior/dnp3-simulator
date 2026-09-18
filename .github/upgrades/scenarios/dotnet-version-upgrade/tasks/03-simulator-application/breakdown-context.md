# Breakdown Context

- Task: `03-simulator-application`
- Scope: 9-project Simulator entry-point dependency path, including WinForms resources and native opendnp3 assets.
- Evidence: `Commons` -> `ModuleAPI`/`DNP3Commons` -> plugin/API tier -> `DNP3Module` -> `Simulator`; `SimulatorUI` joins the application path. Current files are mixed after earlier work: most projects are SDK-style/net8, while `RelayOutstationPlugin` still has legacy TFM/package declarations and `SimulatorUI` is `net8.0` despite WinForms resources.
- Assessment: 11,589 estimated affected lines; opendnp3 2.2.0-M1 incompatible, target 3.1.2; WinForms/System.Drawing/configuration issues across the path; no source stubs found.
- Guidance evaluated: scenario `execution.md` Sections 0, 1, 3, 4, and 5; `breakdown-hints/common.md` hint `multi-project-dependency-ordering`.
- Verdict: escalate to TaskBreaker. The MUST dependency-ordering condition applies because the task spans 3+ projects in a dependency chain and requires validation between tiers.