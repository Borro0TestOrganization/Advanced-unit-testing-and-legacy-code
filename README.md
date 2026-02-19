# Advanced-unit-testing-and-legacy-code

## Prerequisites

Make sure you have .NET10-SDK installed: <https://dotnet.microsoft.com/en-us/download>

## How to build

Run `dotnet build` in the root of the repository.

## How to run tests

Run `dotnet test` from the project folder you want to test.
Otherwise you can use your IDE to run a specific test.

### VSCode

- Make sure you have C# Dev Kit extension installed.
- Open the desired solution (ctrl + shift + P -> .NET Open Solution).
- Open test explorer menu on the left menu bar.
- Discover tests and select a test to run.

## Git on windows bug

You might need to configure your git on windows with this (run with admin powershell):
git config --system core.longpaths true
