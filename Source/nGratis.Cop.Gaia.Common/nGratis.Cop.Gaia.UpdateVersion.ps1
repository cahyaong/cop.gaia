$Host.UI.RawUI.BackgroundColor = "Black"
$Host.UI.RawUI.ForegroundColor = "White"

Clear-Host
Write-Host "0> ----- Updating global assembly information started -----" -ForegroundColor Yellow

$MajorVersion = 0
$MinorVersion = 1

$RootFolderPath = "$([System.IO.Path]::GetDirectoryName($MyInvocation.MyCommand.Path))\"
$T4ExePath = [System.Environment]::ExpandEnvironmentVariables("%CommonProgramFiles(x86)%\Microsoft Shared\TextTemplating\12.0\TextTransform.exe")

if (-not (Test-Path -Path $T4ExePath))
{
	Write-Error "0> T4 executable file '$T4ExePath' is not found"
	throw "Failed to update global assembly information!"
}

$BuildTimestamp = [System.DateTimeOffset]::UtcNow;
$EpochTimestamp = [System.DateTimeOffset]::Parse(
	"01/01/2015 00:00:00AM +00:00", 
	[System.Globalization.CultureInfo]::InvariantCulture, 
	[System.Globalization.DateTimeStyles]::AdjustToUniversal)	

$BuildVersion = (($BuildTimestamp - $EpochTimestamp).TotalDays -as [int]).ToString([System.Globalization.CultureInfo]::InvariantCulture)
$RevisionVersion = $BuildTimestamp.ToString("HHmm", [System.Globalization.CultureInfo]::InvariantCulture)

$Version = "$MajorVersion.$MinorVersion.$BuildVersion.$RevisionVersion"
$BuildTimestamp = $BuildTimestamp.ToString("O", [System.Globalization.CultureInfo]::InvariantCulture)

Write-Host "0> Version: '$Version'"
Write-Host "0> Build Timestamp: '$BuildTimestamp'"
Write-Host "0> Root Folder: '$RootFolderPath'"
Write-Host "0> T4 Execuatable: '$T4ExePath'"

& $T4ExePath "$($RootFolderPath)GlobalAssemblyInfo.tt" -a !!Version!"$Version" -a !!BuildTimestamp!"$BuildTimestamp"

Write-Host "0> ----- Updating global assembly information ended -----" -ForegroundColor Green