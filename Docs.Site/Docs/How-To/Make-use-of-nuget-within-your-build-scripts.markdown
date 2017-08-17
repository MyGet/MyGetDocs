# Make use of nuget within your build scripts

Depending on what exactly you are building, you may require the use of the nuget package manager to create a nuget package which wraps up all the assemblies and source files that make up the package.  MyGet Build Services makes calling the nuget executable very simple!

## What is nuget?

>NuGet is the package manager for the Microsoft development platform including .NET. The NuGet client tools provide the ability to produce and consume packages. The NuGet Gallery (nuget.org) is the central package repository used by all package authors and consumers.

For more information be sure to check out the [nuget home page](https://nuget.org/ "nuget home page").

## Use local nuget executable

This is really simple!

The MyGet Build Server already has a maintained version of the nuget executable ready to be called.  As a result, you can directly call nuget in your build scripts.  This is exposed in the form of an environment variable that you can first check to ensure is set before using.  As a result, all you need to do is the following::

    set nuget=
    if "%nuget%" == "" (
	    set nuget=nuget
    )

    %nuget% pack "projectA.nuspec" -NoPackageAnalysis -OutputDirectory $buildArtifactsDirectory

MyGet Build Services actually provides a number of other environment variables that can be consumed within your build scripts.  A complete list of these environment variables can be found on the [Build Services Reference page](../reference/build-services "Build Services Reference Page").

For a complete example of how this can be used within a build script, check out the [build.bat file for the ReSharper.RazorExtensions nuget package](https://github.com/xavierdecoster/ReSharper.RazorExtensions/blob/master/build.bat "Example build.bat file using the nuget environment variable").

## Use nuget from within your source code repository

Although you _can_ make use of the local nuget executable that is available on the MyGet Build Server, this may not work for you.  For instance, you may need to target a specific version of nuget, which the MyGet Build Servers may not have installed.  In this case, one option that you would have would be to include the necessary files within your Source Control Repository, and call the nuget executable directly from there.  This is possible due to the fact that the MyGet Build Server downloads all the source code from your repository before executing the build.

This is typically done by creating a __lib__ or a __sharedbinaries__ folder in your Source Control Repository, and include the nuget executable file there, for example:

![Include psake files within Source Control](Images/nuget_include_in_source_control.png)


Once in place, you can then call the nuget executable directly by first locating it in your source tree, and then running it, for example (using a PowerShell script):

    $nugetExe = "./../lib/NuGet.exe";

    exec { 
	    .$nugetExe pack "projectA.nuspec" -NoPackageAnalysis -OutputDirectory $buildArtifactsDirectory 
	}

Alternative ways to include a specific version of nuget.exe into your source code repository are:

* use the .nuget\nuget.exe version you use for package restore
* install a specific version of the [NuGet.CommandLine package](https://nuget.org/packages/NuGet.CommandLine/) on the solution level and use the nuget.exe from the Packages\NuGet.CommandLine.{version} folder