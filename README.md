# APEX DevTools

**A PlayStation 4 Black Ops III GSC compilation toolkit featuring a standalone command-line compiler and a custom Windows GUI.**

APEX DevTools provides a streamlined way to compile `.gsc` source files into `.gscc` files for Call of Duty: Black Ops III on PlayStation 4.

Designed for **PS4 BO3 version 1.33**, built using **C# and .NET Framework 4.8**.

![APEX Compiler GUI](assets/apex-compiler-gui.png?v=2)

## Download

**[Download the latest release](https://github.com/CozyMeds/APEX-BO3-Compiler/releases/latest)**

| Package | Description |
|---|---|
| **APEX Compiler Bundle** | GUI and compiler together — recommended |
| APEX Compiler GUI | Standalone graphical interface; requires the CLI separately |
| APEX Compiler CLI | Command-line compiler for advanced users |

No Visual Studio installation is required to use the precompiled releases.

### System requirements

- Windows
- .NET Framework 4.8 runtime
- Compatible PS4 BO3 1.33 environment for testing compiled scripts

---

## Features

### APEX Compiler — CLI

- GSC to GSCC compilation
- PS4 BO3 1.33 opcode support
- Standalone command-line operation
- Suitable for scripting and automated build workflows

### APEX Compiler — GUI

- Redesigned dark-themed Windows Forms interface
- Clean, resizable layout with organised build configuration
- Automatic compiler detection when bundled
- Configurable compiler executable location
- Source directory and output file selection
- One-click GSCC compilation
- Coloured, automatically scrolling build console
- Real-time compilation status and compiler detection indicators
- Compilation duration tracking
- Final output path and file size reporting
- Output verification before replacing existing GSCC files
- Full-path tooltips for file and directory selections
- Persistent compiler, source and output paths
- Open compiled output directly in File Explorer

Both applications are maintained in one Visual Studio solution but can be built and used independently.

---

## What's New in v1.1.0

This release introduces a redesigned graphical interface and improved compilation feedback.

**Interface improvements**
- Reorganised compiler, source and output configuration
- Improved window resizing and minimum dimensions
- Updated action buttons and build console layout
- Simplified status footer
- Added full-path tooltips

**Compilation improvements**
- Total build duration reporting
- Final output filename and size displayed in the build log
- Improved success and error feedback
- Existing GSCC output preserved when compilation fails

---

## Installation

### Recommended — APEX Compiler Bundle

1. Open the [latest GitHub release](https://github.com/CozyMeds/APEX-BO3-Compiler/releases/latest).
2. Download the `APEX-Compiler-Bundle` ZIP.
3. Extract it to a folder of your choice.
4. Launch `APEX.Compiler.GUI.exe`.
5. Select your GSC source directory.
6. Choose your GSCC output destination.
7. Click **COMPILE GSCC**.

The GUI automatically detects `APEX.Compiler.exe` when both executables are in the same directory, unless a valid custom compiler path is already configured.

### GUI Only

Download the `APEX-Compiler-GUI` ZIP from the latest release.

Extract and launch `APEX.Compiler.GUI.exe`.

Use the **Browse** button beside **Compiler Executable** to select your existing compatible `APEX.Compiler.exe`.

### Command-Line Usage

Download the `APEX-Compiler-CLI` ZIP.

Open PowerShell in the compiler directory and execute:

```powershell
.\APEX.Compiler.exe "C:\Path\To\GSC-Source" "C:\Path\To\APEX.gscc"
```

The compiler requires `Irony.dll` and `apex_ps4_ops.bin` in the same directory as its executable.

---

## Building from Source

1. Clone or download this repository.
2. Open `APEX.DevTools.sln` in Visual Studio Community.
3. Install the .NET Framework 4.8 developer targeting pack if required.
4. Restore NuGet dependencies.
5. Select **Release | Any CPU**.
6. Select **Build → Build Solution**.

### Repository Structure

```text
APEX-BO3-Compiler/
├── APEX.DevTools.sln
├── src/
│   ├── APEX.Compiler/
│   └── APEX.Compiler.GUI/
├── samples/
├── scripts/
│   └── package.ps1
├── assets/
│   └── apex-compiler-gui.png
├── README.md
└── CREDITS_AND_RIGHTS.md
```

## Packaging Releases

After building both projects in Release mode, open PowerShell in the repository root.

For example, to package v1.1.0:

```powershell
.\scripts\package.ps1 -Version "1.1.0"
```

The script creates three downloadable archives in `artifacts/`:

```text
APEX-Compiler-CLI-v1.1.0.zip
APEX-Compiler-GUI-v1.1.0.zip
APEX-Compiler-Bundle-v1.1.0.zip
```

Each archive can be distributed independently.

---

## Compatibility and Limitations

- Designed for PS4 Black Ops III version 1.33.
- Other game versions have not been validated.
- Compilation does not guarantee successful execution in-game.
- No PS4 injector or console payloads are included.
- The application requires Windows and .NET Framework 4.8.
- Release packages should be tested after each update.

## Credits

APEX DevTools builds upon earlier community work, including:

- [shiversoftdev/t7-compiler](https://github.com/shiversoftdev/t7-compiler)
- [someguythatmods/ps4-bo3-gsc-injector](https://github.com/someguythatmods/ps4-bo3-gsc-injector)

See [CREDITS_AND_RIGHTS.md](CREDITS_AND_RIGHTS.md) for attribution and redistribution information.

## Disclaimer

APEX DevTools is an independent community project and is not affiliated with, endorsed by, or sponsored by Activision, Treyarch, or Sony Interactive Entertainment.

Call of Duty and Black Ops are trademarks of their respective owners.
