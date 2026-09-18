# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: .NET 8 (`net8.0`)

## Source Control
- **Source Branch**: master
- **Working Branch**: upgrade-dotnet-10
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)

## Upgrade Options

### Strategy
- Upgrade Strategy: Top-Down

### Project Structure
- Package Management: Per-Project

### Compatibility
- Unsupported Packages: Resolve Inline (1 incompatible package)
- Unsupported API Handling: Fix Inline
- Windows Native APIs: Windows Compatibility Pack

### Modernization
- Assembly Binding Redirects: Remove

### Reliability
- Test Coverage: Skip

## Strategy
**Selected**: Top-Down (Application-First)
**Rationale**: The solution has 9 projects, all require upgrade, and 11,589 reported issues, with the largest concentration in the WinForms application path.

### Execution Constraints
- Upgrade the entry-point application first, preparing shared libraries for the new target only when the application requires them.
- Preserve buildability during the application migration through temporary multi-targeting of shared libraries where required.
- Resolve the incompatible `opendnp3` package and breaking APIs inline; do not create deferred stubs.
- Consolidate library target frameworks only after all applications target .NET 8.
- Validate the full solution at the end; do not generate a test baseline because Test Coverage is set to Skip.
