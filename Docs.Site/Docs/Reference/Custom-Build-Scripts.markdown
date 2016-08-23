# Custom Build Scripts

As described in the [Build Services Reference](/docs/reference/build-services), MyGet Build Services supports scripting your own build using a batch file named build.bat, build.cmd or using a PowerShell script. This page lists several example build.bat files which can be used in other projects as well.

<p class="alert alert-info">
    <strong>Note:</strong> All environment variables described in the <a href="/docs/reference/build-services">Build Services Reference</a> can be used in build.bat files.
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
    "%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" YourSolution.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

    REM Package
    mkdir Build
	call %nuget% pack "YourSolution\ProjectA.csproj" -symbols -o Build -p Configuration=%config% %version%
	call %nuget% pack "YourSolution\ProjectB.csproj" -symbols -o Build -p Configuration=%config% %version%
	
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
    "%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" YourSolution.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

    REM Package
    mkdir Build
	call %nuget% pack "YourSolution\ProjectA.csproj" -IncludeReferencedProjects -o Build -p Configuration=%config% %version%
	
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
    "%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" YourSolution.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
	if not "%errorlevel%"=="0" goto failure

	REM Unit tests
	%GallioEcho% YourSolution\ProjectA.Tests\bin\%config%\ProjectA.Tests.dll
	if not "%errorlevel%"=="0" goto failure
	
    REM Package
    mkdir Build
	call %nuget% pack "YourSolution\YourSolution.ProjectA.csproj" -symbols -o Build -p Configuration=%config% %version%
	if not "%errorlevel%"=="0" goto failure
	
	:success
	exit 0
	
	:failure
	exit -1
	
## build.bat manually running unit tests using custom test runner

The following build.bat executes several steps:

* Compile the solution using msbuild
* Run unit tests using a custom test runner provided through the project's source control repository
* Create a single NuGet package containing a specific projects, also creating symbols packages. Package version will come from %PackageVersion% environment variable, if set.

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
    "%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" YourSolution.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
	if not "%errorlevel%"=="0" goto failure

	REM Unit tests
	%nunit% YourSolution\ProjectA.Tests\bin\%config%\ProjectA.Tests.dll
	if not "%errorlevel%"=="0" goto failure
	
    REM Package
    mkdir Build
	call %nuget% pack "YourSolution\YourSolution.ProjectA.csproj" -symbols -o Build -p Configuration=%config% %version%
	if not "%errorlevel%"=="0" goto failure
	
	:success
	exit 0
	
	:failure
	exit -1

## build.bat manually running unit tests using custom test runner which is downloaded on the fly

The following build.bat executes several steps:

* Compile the solution using msbuild
* Download NUnit 2.6.4 from NuGet.org
* Run unit tests using the downloaded NUnit 2.6.4 test runner
* Create a single NuGet package containing a specific projects, also creating symbols packages. Package version will come from %PackageVersion% environment variable, if set.

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
    "%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" YourSolution.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
    if not "%errorlevel%"=="0" goto failure

    REM Unit tests
    call %nuget% install NUnit.Runners -Version 2.6.4 -OutputDirectory packages
    packages\NUnit.Runners.2.6.4\tools\nunit-console.exe /config:%config% /framework:net-4.5 YourSolution\ProjectA.Tests\bin\%config%\ProjectA.Tests.dll
    if not "%errorlevel%"=="0" goto failure
	
    REM Package
    mkdir Build
    call %nuget% pack "YourSolution\YourSolution.ProjectA.csproj" -symbols -o Build -p Configuration=%config% %version%
    if not "%errorlevel%"=="0" goto failure
	
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
    call %NuGet% restore GoogleAnalyticsTracker\packages.config -OutputDirectory %cd%\packages -NonInteractive
    call %NuGet% restore GoogleAnalyticsTracker.WP7\packages.config -OutputDirectory %cd%\packages -NonInteractive

    REM Build
    "%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" GoogleAnalyticsTracker.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

    REM Package
    mkdir Build
    mkdir Build\net40
    call %NuGet% pack "GoogleAnalyticsTracker\GoogleAnalyticsTracker.csproj" -symbols -o Build\net40 -p Configuration=%config% %version%
    copy GoogleAnalyticsTracker\bin\%config%\*.dll Build\net40
    copy GoogleAnalyticsTracker\bin\%config%\*.pdb Build\net40
    
    mkdir Build\sl4-wp71
    call %NuGet% pack "GoogleAnalyticsTracker.WP7\GoogleAnalyticsTracker.WP7.csproj" -symbols -o Build\sl4-wp71 -p Configuration=%config% %version%
    copy GoogleAnalyticsTracker.WP7\bin\%config%\*.dll Build\sl4-wp71
    copy GoogleAnalyticsTracker.WP7\bin\%config%\*.pdb Build\sl4-wp71

    mkdir Build\wp8
    call %NuGet% pack "GoogleAnalyticsTracker.WP8\GoogleAnalyticsTracker.WP8.csproj" -symbols -o Build\wp8 -p Configuration=%config% %version%
    copy GoogleAnalyticsTracker.WP8\bin\%config%\*.dll Build\wp8
    copy GoogleAnalyticsTracker.WP8\bin\%config%\*.pdb Build\wp8

From the [GoogleAnalyticsTracker](https://github.com/maartenba/GoogleAnalyticsTracker) project on GitHub.

## PowerShell Build.ps1 example building for different target framework versions

The following is an advanced PowerShell build script which was contributed by Peter Sunde. It allows building multiple NuGet packages for different target frameworks and CPU versions.

NuSpec file:

	<?xml version="1.0"?>
	<package >
	  <metadata>
		<id>AsposeCloud.SDK</id>
		<version>$version$</version>
		<title>AsposeCloud.SDK for .NET</title>
		<authors>Aspose</authors>
		<owners>Aspose</owners>
	  <licenseUrl>http://www.aspose.com/corporate/purchase/end-user-license-agreement.aspx</licenseUrl>
		<projectUrl>http://www.aspose.com/cloud/total-api.aspx</projectUrl>
		<requireLicenseAcceptance>false</requireLicenseAcceptance>
		<description>Aspose for Cloud is a cloud-based document generation, conversion and automation platform for developers. 
		Aspose for Cloud’s REST APIs gives developers on all platforms total control over documents and file formats. 
		It interoperates seamlessly with other cloud services.</description>
		<copyright>Copyright 2013 Aspose</copyright>
		<tags>Aspose.Cloud SDK PDF XFA XPS TIFF PCL SVG HTML XML XSL-FO FDF XFDF PDF/A form Portfolio EPUB to PDF DOC DOCX</tags>
	  </metadata>
	  <files
		 <!-- $bin => bin\$version$\$platform$\$configugration$ -->
		 <file src="$bin$\v4.0\AsposeCloud.SDK.dll" target="lib\net40" />
		 <file src="$bin$\v4.5\AsposeCloud.SDK.dll" target="lib\net45" />
		 <file src="$bin$\v4.5.1\AsposeCloud.SDK.dll" target="lib\net451" />
	  </files>
	</package>

build.ps1:

	# original: https://gist.github.com/peters/6812859/raw/a965bdb5ae39dbff351420da64df4e9925e07e36/Build.ps1

	param(
		[string]$packageVersion = $null,
		[string]$config = "Release",
		[string[]]$targetFrameworks = @("v4.0", "v4.5", "v4.5.1"),
		[string[]]$platforms = @("AnyCpu"),
		[ValidateSet("rebuild", "build")]
		[string]$target = "rebuild",
		[ValidateSet("quiet", "minimal", "normal", "detailed", "diagnostic")]
		[string]$verbosity = "minimal",
		[bool]$alwaysClean = $true
	)

	# Diagnostic 
	function Write-Diagnostic {
		param([string]$message)

		Write-Host
		Write-Host $message -ForegroundColor Green
		Write-Host
	}

	function Die([string]$message, [object[]]$output) {
		if ($output) {
			Write-Output $output
			$message += ". See output above."
		}
		Write-Error $message
		exit 1
	}

	function Create-Folder-Safe {
		param(
			[string]$folder = $(throw "-folder is required.")
		)

		if(-not (Test-Path $folder)) {
			[System.IO.Directory]::CreateDirectory($folder)
		}

	}

	# Build
	function Build-Clean {
		param(
			[string]$rootFolder = $(throw "-rootFolder is required."),
			[string]$folders = "bin,obj"
		)

		Write-Diagnostic "Build: Clean"

		Get-ChildItem $rootFolder -Include $folders -Recurse | ForEach-Object {
		   Remove-Item $_.fullname -Force -Recurse 
		}
	}

	function Build-Bootstrap {
		param(
			[string]$solutionFile = $(throw "-solutionFile is required."),
			[string]$nugetExe = $(throw "-nugetExe is required.")
		)

		Write-Diagnostic "Build: Bootstrap"

		$solutionFolder = [System.IO.Path]::GetDirectoryName($solutionFile)

		. $nugetExe config -Set Verbosity=quiet
		. $nugetExe restore $solutionFile -NonInteractive

		Get-ChildItem $solutionFolder -filter packages.config -recurse | 
			Where-Object { -not ($_.PSIsContainer) } | 
			ForEach-Object {

			. $nugetExe restore $_.FullName -NonInteractive -SolutionDirectory $solutionFolder

		}
	}

	function Build-Nupkg {
		param(
			[string]$rootFolder = $(throw "-rootFolder is required."),
			[string]$project = $(throw "-project is required."),
			[string]$nugetExe = $(throw "-nugetExe is required."),
			[string]$outputFolder = $(throw "-outputFolder is required."),
			[string]$config = $(throw "-config is required."),
			[string]$version = $(throw "-version is required."),
			[string]$platform = $(throw "-platform is required.")
		)

		$outputFolder = Join-Path $outputFolder "$config"
		$nuspecFilename = [System.IO.Path]::GetFullPath($project) -ireplace ".csproj$", ".nuspec"

		if(-not (Test-Path $nuspecFilename)) {
			Die("Could not find nuspec: $nuspecFilename")
		}

		Write-Diagnostic "Creating nuget package for platform $platform"

		# http://docs.nuget.org/docs/reference/command-line-reference#Pack_Command
		. $nugetExe pack $nuspecFilename -OutputDirectory $outputFolder -Symbols -NonInteractive `
			-Properties "Configuration=$config;Bin=$outputFolder;Platform=$platform" -Version $version

		if($LASTEXITCODE -ne 0) {
			Die("Build failed: $projectName")
		}

		# Support for multiple build runners
		if(Test-Path env:BuildRunner) {
			$buildRunner = Get-Content env:BuildRunner

			switch -Wildcard ($buildRunner.ToString().ToLower()) {
				"myget" {
					
					$mygetBuildFolder = Join-Path $rootFolder "Build"

					Create-Folder-Safe -folder $mygetBuildFolder

					Get-ChildItem $outputFolder -filter *.nupkg | 
					Where-Object { -not ($_.PSIsContainer) } | 
					ForEach-Object {
						$fullpath = $_.FullName
						$filename = $_.Name

						cp $fullpath $mygetBuildFolder\$filename
					}

				}
			}
		}
	}

	function Build-Project {
		param(
			[string]$project = $(throw "-project is required."),
			[string]$outputFolder = $(throw "-outputFolder is required."),
			[string]$nugetExe = $(throw "-nugetExe is required."),
			[string]$config = $(throw "-config is required."),
			[string]$target = $(throw "-target is required."),
			[string[]]$targetFrameworks = $(throw "-targetFrameworks is required."),
			[string[]]$platform = $(throw "-platform is required.")
		)

		$projectPath = [System.IO.Path]::GetFullPath($project)
		$projectName = [System.IO.Path]::GetFileName($projectPath) -ireplace ".csproj$", ""

		Create-Folder-Safe -folder $outputFolder

		if(-not (Test-Path $projectPath)) {
			Die("Could not find csproj: $projectPath")
		}

		$targetFrameworks | foreach-object {
			$targetFramework = $_
			$platformOutputFolder = Join-Path $outputFolder "$config\$targetFramework"
			
			Create-Folder-Safe -folder $platformOutputFolder

			Write-Diagnostic "Build: $projectName ($platform / $config - $targetFramework)"

			& "$(Get-Content env:windir)\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" `
				$projectPath `
				/t:$target `
				/p:Configuration=$config `
				/p:OutputPath=$platformOutputFolder `
				/p:TargetFrameworkVersion=$targetFramework `
				/p:Platform=$platform `
				/m `
				/v:M `
				/fl `
				/flp:LogFile=$platformOutputFolder\msbuild.log `
				/nr:false

			if($LASTEXITCODE -ne 0) {
				Die("Build failed: $projectName ($Config - $targetFramework)")
			}
		}
	}

	function Build-Solution {
		param(
			[string]$rootFolder = $(throw "-rootFolder is required."),
			[string]$solutionFile = $(throw "-solutionFile is required."),
			[string]$outputFolder = $(throw "-outputFolder is required."),
			[string]$version = $(throw "-version is required"),
			[string]$config = $(throw "-config is required."),
			[string]$target = $(throw "-target is required."),
			[bool]$alwaysClean = $(throw "-alwaysclean is required"),
			[string[]]$targetFrameworks = $(throw "-targetFrameworks is required."),
			[string[]]$projects = $(throw "-projects is required."),
			[string[]]$platforms = $(throw "-platforms is required.")
		)

		if(-not (Test-Path $solutionFile)) {
			Die("Could not find solution: $solutionFile")
		}

		$solutionFolder = [System.IO.Path]::GetDirectoryName($solutionFile)
		$nugetExe = if(Test-Path Env:NuGet) { Get-Content env:NuGet } else { Join-Path $solutionFolder ".nuget\nuget.exe" }

		if($alwaysClean) {
			Build-Clean -root $solutionFolder
		}

		Build-Bootstrap -solutionFile $solutionFile -nugetExe $nugetExe

		$projects | ForEach-Object {

			$project = $_

			$platforms | ForEach-Object {
				$platform = $_

				$buildOutputFolder = Join-Path $outputFolder "$version\$platform"

				Build-Project -rootFolder $solutionFolder -project $project -outputFolder $buildOutputFolder `
					-nugetExe $nugetExe -target $target -config $config `
					-targetFrameworks $targetFrameworks -version $version -platform $platform

				Build-Nupkg -rootFolder $rootFolder -project $project -nugetExe $nugetExe -outputFolder $buildOutputFolder `
					-config $config -version $version -platform $platform
			}
		}
	}

	function TestRunner-Nunit {
		param(
			[string]$outputFolder = $(throw "-outputFolder is required."),
			[string]$config = $(throw "-config is required."),
			[string]$target = $(throw "-target is required."),
			[string[]]$projects = $(throw "-projects is required."),
			[string[]]$platforms = $(throw "-platforms is required.")
		)

		Die("TODO")
	}

	# Bootstrap
	$rootFolder = Split-Path -parent $script:MyInvocation.MyCommand.Definition
	$outputFolder = Join-Path $rootFolder "bin"
	$testsFolder = Join-Path $outputFolder "tests"

	$config = $config.substring(0, 1).toupper() + $config.substring(1)
	$version = $config.trim()

	# Myget
	$currentVersion = if(Test-Path env:PackageVersion) { Get-Content env:PackageVersion } else { $packageVersion }

	if($currentVersion -eq "") {
		Die("Package version cannot be empty")
	}

	# Build AsposeCloud
	Build-Solution -solutionFile $rootFolder\AsposeCloud.SDK-for-.NET-master\AsposeCloud.SDK.sln `
		-projects @( `
			"$rootFolder\AsposeCloud.SDK-for-.NET-master\AsposeCloud.SDK\AsposeCloud.csproj"
		) `
		-rootFolder $rootFolder `
		-outputFolder $outputFolder `
		-platforms $platforms `
		-version $currentVersion `
		-config $config `
		-target $target `
		-targetFrameworks $targetFrameworks `
		-alwaysClean $alwaysClean

## PowerShell Build.ps1 example creating Chocolatey packages

The following is a Powershell script that, instead of creating NuGet packages, creates [Chocolatey](http://www.chocolatey.org) packages.

build.ps1:

	Write-Host "Building Chocolatey packages..."
	
	$nuspecs = Get-ChildItem -Path $PSScriptRoot -Filter *.nuspec -Recurse
	
	foreach ($nuspec in $nuspecs) {
	    choco pack $nuspec.FullName
	}
	
	$artifactsFolder = "./artifacts"
	
	Remove-Item -Path $artifactsFolder -Force -Recurse -ErrorAction SilentlyContinue
	New-Item $artifactsFolder -Force -Type Directory | Out-Null
	Move-Item *.nupkg $artifactsFolder

	Write-Host "Finished building Chocolatey packages."

## .NET Core running xunit tests

MyGet can build .NET Core projects out of the box. However, creating a `build.bat` often makes a lot of sense if you want to add a custom test runner or have more control over the build process. The following files make up a build for a .NET Core library that:

* Restores packages using `dotnet restore`
* Builds packages using `dotnet build`
* Runs tests using `dotnet test`, making use of the XUnit test framework
* Packages the library using `dotnet pack`

Before we dive into the build itself, there are some prerequisites. First of all, the solution folder should have a `NuGet.config` that adds the XUnit nightlies feed so that we can use the XUnit test runner:

NuGet.config:

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <activePackageSource>
	    <add key="All" value="(Aggregate source)" />
	  </activePackageSource>
	  <packageSources>
		<clear />
	    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
	    <add key="xunit" value="https://www.myget.org/F/xunit/api/v3/index.json" />
	  </packageSources>
	</configuration>

Make sure that the test project is configured correctly. XUnit has a tutorial "[Getting started with xUnit.net (.NET Core / ASP.NET Core)](https://xunit.github.io/docs/getting-started-dotnet-core.html)" which describes the steps involved.

Finally, make sure the library project itself has a `project.json` that holds at least the `name` and `description` to be used in the generated NuGet package:

project.json:

	{
	  "version": "1.0.0-*",
	  
	  "name": "MyLibrary",
	  "description": "My cool library",
	
	  "dependencies": {
	    "NETStandard.Library": "1.6.0"
	  },
	
	  "frameworks": {
	    "netstandard1.6": {
	      "imports": "dnxcore50"
	    }
	  }
	}

The following build uses the `dotnet` command to do all the work, except compilation. We'll be using `msbuild` for that as it automatically resolves project dependencies and builds projects in the right order.

We're also using MyGet's `%BuildCounter%` environment variable to generate a version number suffix (which will replace the `*` in `1.0.0-*`). This is optional and can be removed if you prefer using the version numbers .NET Core generates itself.

build.bat:

	@echo Off
	set config=%1
	if "%config%" == "" (
	   set config=Release
	)
	
	set version=
	if not "%BuildCounter%" == "" (
	   set version=--version-suffix ci-%BuildCounter%
	)
	
	REM (optional) build.bat is in the root of our repo, cd to the correct folder where sources/projects are
	cd MyLibrary
	
	REM Restore
	call dotnet restore
	if not "%errorlevel%"=="0" goto failure
	
	REM Build
	REM - Option 1: Run dotnet build for every source folder in the project
	REM   e.g. call dotnet build <path> --configuration %config%
	REM - Option 2: Let msbuild handle things and build the solution
	"%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" MyLibrary.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
	REM call dotnet build --configuration %config%
	if not "%errorlevel%"=="0" goto failure
	
	REM Unit tests
	call dotnet test tests\MyLibrary.Tests --configuration %config%
	if not "%errorlevel%"=="0" goto failure
	
	REM Package
	mkdir %cd%\Artifacts
	call dotnet pack src\MyLibrary --configuration %config% %version% --output Artifacts
	if not "%errorlevel%"=="0" goto failure
	
	:success
	exit 0
	
	:failure
	exit -1
