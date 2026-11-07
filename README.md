# Slot Machine Ultra — C# Console Learning Project

**Historical programming exercise**, illustrating random game-grid generation, selected betting lines, and payout logic with **simulated values only**. No real-money transactions, accounts or gambling payments are implemented.

## Source layout
- `SlotMachineUltra/Program.cs` — application entry point.
- `SlotMachineUltra/SlotMachineGame.cs` — board generation and win calculations.
- `SlotMachineUltra/SlotMachineUI.cs` — console prompts and display.
- `SlotMachineUltra/BetChoice.cs`, `Constants.cs`, `Configurations.cs`, `Messages.cs` — supporting types.

## Setup
The project targets `net7.0`, which is no longer supported. Use an appropriate SDK/runtime for historical exploration, or modernize the project before publishing a supported application.

```bash
dotnet restore SlotMachineUltra.sln
dotnet build SlotMachineUltra.sln
dotnet run --project SlotMachineUltra/SlotMachineUltra.csproj
```

**Verification status:** No successful build, runtime test, or automated unit tests were independently verified during this GitHub audit. The original README contained an unclosed code block; this document corrects the installation instructions without changing source code.

## Version history
[SlotMachineimproved.1](https://github.com/Esoham/SlotMachineimproved.1) is an earlier exercise. Retain it for learning history if useful, but consider this repository the primary version for that exercise.

## QA learning opportunities
Unit-test payout rules, invalid wager values, edge-case game-over logic and repeated-play control flow. The current repo has no dedicated automated test suite, so these are suggested improvements rather than existing coverage.

For a professional-quality, CI-verified software test framework, see [CommerceQA / FarmFresh](https://github.com/Esoham/QA-Automation-Project).
