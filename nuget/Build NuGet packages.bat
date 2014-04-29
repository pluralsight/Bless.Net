"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe" ..\Bless.Net.sln /t:Build /p:configuration=Release

del *.nupkg

NuGet.exe Pack Bless.Core.nuspec
NuGet.exe Pack Bless.nuspec