$root = $PSScriptRoot;
. $root\..\Shared.ps1

$clientUrl = "https://localhost:44339";

# Authorization Server
Push-Location "$root/Verkuyl.Server"
dotnet restore
dotnet build --no-incremental #rebuild
Start-Process dotnet -ArgumentList "watch run urls=https://localhost:44314" -PassThru 
Pop-Location

# Client Application
Push-Location "$root/Verkuyl.Client"
dotnet restore
dotnet build --no-incremental
Start-Process dotnet -ArgumentList "watch run urls=$clientUrl" -PassThru
Pop-Location

Start-Browser $clientUrl;