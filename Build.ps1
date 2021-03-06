function EnsurePsbuildInstalled {
    [cmdletbinding()]
    param(
        [string]$psbuildInstallUri = 'https://raw.githubusercontent.com/ligershark/psbuild/master/src/GetPSBuild.ps1'
    )
    process {
        if(-not (Get-Command "Invoke-MsBuild" -errorAction SilentlyContinue)){
            'Installing psbuild from [{0}]' -f $psbuildInstallUri | Write-Verbose
            (new-object Net.WebClient).DownloadString($psbuildInstallUri) | iex
        }
        else{
            'psbuild already loaded, skipping download' | Write-Verbose
        }

        if(-not (Get-Command "Invoke-MsBuild" -errorAction SilentlyContinue)){
            throw ('Unable to install/load psbuild from [{0}]' -f $psbuildInstallUri)
        }
    }
}

function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

if(Test-Path .\artifacts) { Remove-Item .\artifacts -Force -Recurse }

EnsurePsbuildInstalled

exec { & dotnet restore }

exec { & dotnet pack .\src\XCommon\XCommon.csproj -c Release -o ..\..\artifacts }
exec { & dotnet pack .\src\XCommon.CloudServices\XCommon.CloudServices.csproj -c Release -o ..\..\artifacts }
exec { & dotnet pack .\src\XCommon.CodeGenerator\XCommon.CodeGenerator.csproj -c Release -o ..\..\artifacts }
exec { & dotnet pack .\src\XCommon.EF\XCommon.EF.csproj -c Release -o ..\..\artifacts }
exec { & dotnet pack .\src\XCommon.EF.Application\XCommon.EF.Application.csproj -c Release -o ..\..\artifacts }
exec { & dotnet pack .\src\XCommon.Web\XCommon.Web.csproj -c Release -o ..\..\artifacts }