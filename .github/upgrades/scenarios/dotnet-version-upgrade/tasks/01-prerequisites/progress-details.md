# Progress Details

- Task: `01-prerequisites`
- Scope: toolchain and current solution build baseline only; no test-baseline work was performed.
- SDK check: .NET SDK `8.0.425` is installed. The active SDK is `10.0.401`; no `global.json` or `Directory.Build.*` file selects another SDK.
- Windows desktop check: `Microsoft.WindowsDesktop.App` `8.0.11` and `8.0.31` are installed. Visual Studio Build Tools 2026 `18.9.2` is installed and complete with MSBuild.
- TFM resolution check: `Commons` evaluates as `net8.0` with `TargetFrameworkIdentifier=.NETCoreApp` under SDK `10.0.401`; `DefaultOutstationPlugin` evaluates as `net8.0-windows` with `UseWindowsForms=true` under the same SDK. This confirms repository SDK selection is compatible with both requested target shapes.
- Full baseline: `MSBuild.exe simulator.sln /restore /t:Build /p:Configuration=Debug /p:Platform=Any CPU` failed before project compilation because the Visual Studio Build Tools MSBuild resolver could not locate `Microsoft.NET.Sdk` and `Microsoft.NET.Sdk.WindowsDesktop` in its own `MSBuild\\Sdks` directory. Result: existing infrastructure blocker.
- Full `dotnet build simulator.sln --configuration Debug --no-restore`: failed with `126` errors and `10` warnings. Existing blockers include `RelayOutstationPlugin` still targeting .NET Framework 4.5 while referencing net8 projects, missing/legacy API and WinForms references in DNP3 projects, duplicate generated assembly attributes, and `opendnp3` API mismatches.
- Focused checks: `Commons` and `DefaultOutstationPlugin` resolved their .NET 8 TFMs and entered compilation, then failed on the existing `opendnp3` build target because no `PlatformTarget` was supplied for the dependency graph. `DefaultOutstationPlugin` itself declares `PlatformTarget=x64`, but `Commons` does not.
- Warnings noted: WinForms projects using `Microsoft.NET.Sdk.WindowsDesktop` emit `NETSDK1137`; `.resx` files emit BinaryFormatter deprecation warnings; nullable warning exists in DNP3Commons. No warning suppression or source/project fix was made in this prerequisite task.
- Decomposition verdict: atomic. `execution.md`, `breakdown-hints/common.md`, `framework-migration.md`, `framework-web-migration.md`, and `test.md` were evaluated; no applicable decomposition trigger was found.
- Files changed by this task: `task.md` and this progress file. Existing unrelated worktree changes were preserved.
