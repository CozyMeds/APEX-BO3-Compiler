param(
    [Parameter(Mandatory = $true)]
    [ValidatePattern('^[0-9A-Za-z._-]+$')]
    [string]$Version
)
$ErrorActionPreference = 'Stop'
$repo = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$compilerDir = Join-Path $repo 'src\APEX.Compiler\bin\Release\net48'
$guiDir = Join-Path $repo 'src\APEX.Compiler.GUI\bin\Release'
$releaseDir = Join-Path $repo 'artifacts'
$stageDir = Join-Path $releaseDir '_staging'
if (Test-Path $stageDir) { Remove-Item $stageDir -Recurse -Force }
New-Item -ItemType Directory -Force -Path $stageDir, $releaseDir | Out-Null
try {
    $cliFiles = @('APEX.Compiler.exe', 'Irony.dll', 'apex_ps4_ops.bin')
    $guiFiles = @('APEX.Compiler.GUI.exe', 'APEX.Compiler.GUI.exe.config')
    foreach ($file in $cliFiles) {
        if (!(Test-Path (Join-Path $compilerDir $file))) {
            throw "Missing compiler runtime file: $file. Build APEX.Compiler in Release mode first."
        }
    }
    foreach ($file in $guiFiles) {
        if (!(Test-Path (Join-Path $guiDir $file))) {
            throw "Missing GUI runtime file: $file. Build APEX.Compiler.GUI in Release mode first."
        }
    }
    $assets = @(
        @{ Name = "APEX-Compiler-CLI-v$Version"; IncludeCLI = $true; IncludeGUI = $false },
        @{ Name = "APEX-Compiler-GUI-v$Version"; IncludeCLI = $false; IncludeGUI = $true },
        @{ Name = "APEX-Compiler-Bundle-v$Version"; IncludeCLI = $true; IncludeGUI = $true }
    )
    foreach ($asset in $assets) {
        $folder = Join-Path $stageDir $asset.Name
        New-Item -ItemType Directory -Force -Path $folder | Out-Null
        if ($asset.IncludeCLI) {
            foreach ($file in $cliFiles) { Copy-Item (Join-Path $compilerDir $file) -Destination $folder }
            $optionalCliConfig = Join-Path $compilerDir 'APEX.Compiler.exe.config'
            if (Test-Path $optionalCliConfig) { Copy-Item $optionalCliConfig -Destination $folder }
        }
        if ($asset.IncludeGUI) {
            foreach ($file in $guiFiles) { Copy-Item (Join-Path $guiDir $file) -Destination $folder }
        }
        Copy-Item (Join-Path $repo 'CREDITS_AND_RIGHTS.md') -Destination $folder
        $zip = Join-Path $releaseDir ($asset.Name + '.zip')
        if (Test-Path $zip) { Remove-Item $zip -Force }
        Compress-Archive -Path (Join-Path $folder '*') -DestinationPath $zip -CompressionLevel Optimal
        Write-Host "Created $zip"
    }
} finally {
    if (Test-Path $stageDir) { Remove-Item $stageDir -Recurse -Force }
}
