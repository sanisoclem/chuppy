@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild .\chuppy.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

mkdir build

%nuget% pack ".\nuget\chuppy.nuspec" -NoPackageAnalysis -verbosity detailed -o build -p Configuration="%config%"