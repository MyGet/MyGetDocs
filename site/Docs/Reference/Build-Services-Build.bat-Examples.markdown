# Build Services - Build.bat examples

As described in the [Build Services Reference](/docs/reference/build-services), MyGet Build Services supports scripting your own build using a batch file named build.bat, build.cmd or using a PowerShell script. This page lists several example build.bat files which can be used in other projects as well.

<p class="info">
    <strong>NOTE:</strong> All environment variables described in the <a href="/docs/reference/build-services">Build Services Reference</a> can be used in build.bat files.
</p>

## Simple build.bat

The following build.bat executes several steps:

* Compile the solution using msbuild
* Create NuGet packages for specific projects in the solution, also creating symbols packages. Package version will come from %PackageVersion% environment variable, if set.

build.bat:

    @echo Off
    set config=%1
    if "%config%" == "" (
       set config=Release
    )
    
    set version=
    if not "%PackageVersion%" == "" (
       set version=-Version %PackageVersion%
    )
    
    REM Build
    %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild YourSolution.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

    REM Package
    mkdir Build
	cmd /c %nuget% pack "YourSolution\ProjectA.csproj" -symbols -o Build -p Configuration=%config% %version%
	cmd /c %nuget% pack "YourSolution\ProjectB.csproj" -symbols -o Build -p Configuration=%config% %version%
	
## build.bat creating a single NuGet package containing all projects

The following build.bat executes several steps:

* Compile the solution using msbuild
* Create a single NuGet package containing all referenced projects. Package version will come from %PackageVersion% environment variable, if set.

build.bat:

    @echo Off
    set config=%1
    if "%config%" == "" (
       set config=Release
    )
    
    set version=
    if not "%PackageVersion%" == "" (
       set version=-Version %PackageVersion%
    )
    
    REM Build
    %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild YourSolution.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

    REM Package
    mkdir Build
	cmd /c %nuget% pack "YourSolution\ProjectA.csproj" -IncludeReferencedProjects -o Build -p Configuration=%config% %version%
	
## build.bat manually running unit tests using default test runner

The following build.bat executes several steps:

* Compile the solution using msbuild
* Run unit tests using default MyGet test runner ([Gallio Echo](http://www.gallio.org))
* Create a single NuGet package containing all referenced projects, also creating symbols packages. Package version will come from %PackageVersion% environment variable, if set.

This build.bat also verifies error level after every step and reports build success/failure back to MyGet.

build.bat:

    @echo Off
    set config=%1
    if "%config%" == "" (
       set config=Release
    )
    
    set version=
    if not "%PackageVersion%" == "" (
       set version=-Version %PackageVersion%
    )
    
    REM Build
    %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild YourSolution.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
	if not "%errorlevel%=="0" goto failure

	REM Unit tests
	%GallioEcho% YourSolution\ProjectA.Tests\bin\%config%\ProjectA.Tests.dll
	if not "%errorlevel%=="0" goto failure
	
    REM Package
    mkdir Build
	cmd /c %nuget% pack "YourSolution\YourSolution.ProjectA.csproj" -symbols -o Build -p Configuration=%config% %version%
	if not "%errorlevel%=="0" goto failure
	
	:success
	exit 0
	
	:failure
	exit -1
	
## build.bat manually running unit tests using custom test runner

The following build.bat executes several steps:

* Compile the solution using msbuild
* Run unit tests using a custom test runner provided through the project's source control repository
* Create a single NuGet package containing all referenced projects, also creating symbols packages. Package version will come from %PackageVersion% environment variable, if set.

This build.bat also verifies error level after every step and reports build success/failure back to MyGet.

build.bat:

    @echo Off
    set config=%1
    if "%config%" == "" (
       set config=Release
    )
    
    set version=
    if not "%PackageVersion%" == "" (
       set version=-Version %PackageVersion%
    )
    
	set nunit="tools\nunit\nunit-console.exe"
	
    REM Build
    %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild YourSolution.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
	if not "%errorlevel%=="0" goto failure

	REM Unit tests
	%nunit% YourSolution\ProjectA.Tests\bin\%config%\ProjectA.Tests.dll
	if not "%errorlevel%=="0" goto failure
	
    REM Package
    mkdir Build
	cmd /c %nuget% pack "YourSolution\YourSolution.ProjectA.csproj" -symbols -o Build -p Configuration=%config% %version%
	if not "%errorlevel%=="0" goto failure
	
	:success
	exit 0
	
	:failure
	exit -1

## build.bat doing manual package restore before build

The following build.bat executes several steps:

* Perform manual package restore for several projects' packages.config.
* Compile the solution using msbuild
* Create NuGet packages for specific projects in the solution, also creating symbols packages. Package version will come from %PackageVersion% environment variable, if set.

Note that the nuget.exe used is provided by the project itself through its source control, hence the calls to .nuget\nuget.exe.

build.bat:

    @echo Off
    set config=%1
    if "%config%" == "" (
       set config=Release
    )
    
    set version=
    if not "%PackageVersion%" == "" (
       set version=-Version %PackageVersion%
    )
    
    REM Package restore
    .nuget\nuget.exe install GoogleAnalyticsTracker\packages.config -OutputDirectory %cd%\packages -NonInteractive
    .nuget\nuget.exe install GoogleAnalyticsTracker.WP7\packages.config -OutputDirectory %cd%\packages -NonInteractive

    REM Build
    %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild GoogleAnalyticsTracker.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

    REM Package
    mkdir Build
    mkdir Build\net40
    .nuget\nuget.exe pack "GoogleAnalyticsTracker\GoogleAnalyticsTracker.csproj" -symbols -o Build\net40 -p Configuration=%config% %version%
    copy GoogleAnalyticsTracker\bin\%config%\*.dll Build\net40
    copy GoogleAnalyticsTracker\bin\%config%\*.pdb Build\net40
    
    mkdir Build\sl4-wp71
    .nuget\nuget.exe pack "GoogleAnalyticsTracker.WP7\GoogleAnalyticsTracker.WP7.csproj" -symbols -o Build\sl4-wp71 -p Configuration=%config% %version%
    copy GoogleAnalyticsTracker.WP7\bin\%config%\*.dll Build\sl4-wp71
    copy GoogleAnalyticsTracker.WP7\bin\%config%\*.pdb Build\sl4-wp71

    mkdir Build\wp8
    .nuget\nuget.exe pack "GoogleAnalyticsTracker.WP8\GoogleAnalyticsTracker.WP8.csproj" -symbols -o Build\wp8 -p Configuration=%config% %version%
    copy GoogleAnalyticsTracker.WP8\bin\%config%\*.dll Build\wp8
    copy GoogleAnalyticsTracker.WP8\bin\%config%\*.pdb Build\wp8

From the [GoogleAnalyticsTracker](https://github.com/maartenba/GoogleAnalyticsTracker) project on GitHub.
