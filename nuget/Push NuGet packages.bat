@echo off
call "Build NuGet packages.bat"

for %%f in (*.nupkg) do (
	NuGet.exe Push %%~nf.nupkg
)
