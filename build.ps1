# Stop on error
$ErrorActionPreference = "Stop"

Write-Host "Building React App..."

Set-Location -Path "front\react-app"
Write-Host "Running npm install..."
npm install

Write-Host "Running npm run build..."
npm run build
Set-Location -Path "..\"

Write-Host "Building Electron installer..."
npm run dist

Write-Host "Build complete!"
