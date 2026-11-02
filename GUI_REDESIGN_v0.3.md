# APEX Compiler GUI v0.3 — layout redesign

This update changes **only the Windows Forms GUI**. The CLI compiler, opcode data,
project settings, packaging script, and compile logic are otherwise untouched.

## Layout changes

- Resizable 920×720 window (minimum 800×660), keeping the navy/cyan theme.
- Dedicated **Build Configuration** area with matching compiler, source and output rows.
- New read-only `txtCompiler` field that displays the saved or automatically detected
  sibling `APEX.Compiler.exe` path. A small badge reports whether the compiler exists.
- Compile and Open Output Folder actions placed together in a responsive two-column row.
- Larger, padded build console that expands with the window.
- Simplified footer with just the status indicator, status text and GUI version.
- Segoe UI labels/buttons and Consolas console font, with improved spacing and hover colours.
- Existing button event handlers and saved settings preserved. Source/output paths now
  use browse-only read-only textboxes, as specified for this redesign.

## Local build and test

1. Back up any local GUI edits before replacing files.
2. Open `APEX.DevTools.sln` using Visual Studio on **Windows**.
3. Select **Release | Any CPU**, then **Build > Build Solution**.
4. Run `APEX.Compiler.GUI` and test:
   - Compiler detection with a CLI next to the GUI and selection via Browse.
   - Source and output browsing and persistence across application restarts.
   - Successful and unsuccessful GSC compilation and coloured console output.
   - Open Output Folder and the compile button's busy/ready states.
   - Window resizing down to the minimum and back up.
5. After confirming the layout works, run `./scripts/package.ps1 -Version '1.1.0'`
   from PowerShell in the repository root to make release ZIPs.

No built executables are included in this source archive. The C# files have been
statically checked for control names and event-handler wiring, but **a Windows build
and visual QA are still required** before publishing a new release.

## v0.3.1 polish update (November 2026)

- Increased minimum WinForms outer window size to **800 x 760** so the build console does not collapse when minimized by resizing.
- Added tooltips that display the full compiler, source and output paths when hovering over the respective read-only text fields. The tooltip stays current when paths change.
- Added a total build stopwatch; success, failed and error status messages include elapsed seconds.
- After verified compilation, the build console reports the **final destination** (rather than only the temporary output path), output size and total duration.
- Preserved console autoscroll, async output collection, UI theme, existing event handlers and verified-temp-file promotion.

No compiled Windows binaries are bundled in this source ZIP. Open `APEX.DevTools.sln` in Visual Studio, choose Release | Any CPU, then Build Solution.
