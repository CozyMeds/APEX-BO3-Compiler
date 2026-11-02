# APEX PS4 BO3 compiler tools (development workspace)

One Visual Studio solution, **two independent Windows applications**:

- **APEX.Compiler**: a command-line `.gsc` → `.gscc` compiler for PS4 BO3 1.33.
- **APEX.Compiler.GUI**: a dark-themed WinForms front end that calls `APEX.Compiler.exe` when you click Compile.

The projects are not merged into one executable. You can edit, build, and version them together while shipping **separate GitHub release downloads**.

## Build

1. Open `APEX.DevTools.sln` in Visual Studio Community on Windows.
2. Install the .NET Framework **4.8 developer targeting pack** if prompted; enable NuGet package restore (compiler uses Irony 1.1.0).
3. Select **Release | Any CPU** and **Build > Build Solution**.
4. Check these outputs (do not launch executables until you have inspected and scanned them):

   - `src/APEX.Compiler/bin/Release/net48/APEX.Compiler.exe`
   - `src/APEX.Compiler.GUI/bin/Release/APEX.Compiler.GUI.exe`

The compiler requires `Irony.dll` and `apex_ps4_ops.bin` next to its EXE. .NET Framework 4.8 runtime is required on user machines.

## Run GUI / compiler separately

- **CLI-only:** extract the `APEX-Compiler-CLI-vVERSION.zip` download and run `APEX.Compiler.exe "source-folder" "output.gscc"` in PowerShell.
- **GUI-only:** extract `APEX-Compiler-GUI-vVERSION.zip` and run `APEX.Compiler.GUI.exe`. The GUI **does not include the compiler**. Select the separately installed `APEX.Compiler.exe` with the **SELECT COMPILER** button.
- **Combined optional bundle:** extract `APEX-Compiler-Bundle-vVERSION.zip`, containing GUI and CLI together. The GUI automatically finds `APEX.Compiler.exe` if it is beside the GUI EXE and no valid custom path is saved.

If users extract the CLI files into the same directory as the GUI EXE, the GUI will also detect the compiler automatically (unless a valid custom path is saved). No injector or PS4 payloads are included.

## Make release ZIPs (after verifying distribution rights)

Once **both Release builds** succeed, open PowerShell at the repository root:

```powershell
.\scripts\package.ps1 -Version '1.0.0'
```

The script generates **three archives** in `artifacts/`: CLI-only, GUI-only, and an optional combined bundle. The build script does **not** download dependencies, execute either compiled app, or perform any PS4 injection. It packages only explicitly listed runtime files.

Go to your GitHub repository → **Releases → Draft a new release**, set tag `v1.0.0`, and upload the separate ZIP assets. Do not upload `bin/`, `obj/`, `.vs/` or `.git/` directories; `.gitignore` is included.

## Repository structure

```text
APEX.DevTools.sln
src/
  APEX.Compiler/       # CLI project, C# compiler and opcode data
  APEX.Compiler.GUI/   # WinForms GUI
samples/
  Sample-APEX/         # tiny test GSC
scripts/
  package.ps1          # separate release packaging
```

## Testing and limitations

This repository assembly has been statically verified on Linux, but **not built or executed here on Windows**. The original two apps were reported working by their developer on Windows; the combined solution and GUI's new sibling-EXE fallback need an actual Windows build/test. Compiling a GSCC file does not establish that it will work with every PS4 game version or injector.

## Credits and redistribution permissions

See [CREDITS_AND_RIGHTS.md](CREDITS_AND_RIGHTS.md) **before publishing the compiler-derived source or binaries**. There is no license grant in this development archive.
