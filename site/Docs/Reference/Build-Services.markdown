# Build Services

MyGet Build Services allows you to connect to different types of source control systems:

* Git
* Mercurial (Hg)
* Subversion

Next to that, integration with several Source Control Repositories is available as well:

* [GitHub](https://github.com/ "GitHub")
* [BitBucket](https://bitbucket.org "BitBucket")
* [CodePlex](http://www.codeplex.com/ "CodePlex")

Once downloaded, source code can then be built using a number of different methodologies.

<p class="info">
    <strong>Note:</strong> Although fully operational, MyGet Build Services is currently still in the Beta Stage.  While in Beta, you can't trigger a Build, manually or otherwise, faster then one every 5 minutes
</p>

## The Build Process

Using MyGet Build Services, you have the opportunity to control exactly how your project gets built. MyGet Build Services works based on several conventions to run builds. It will scan the contents of your Source Control Repository looking for a file which it can work with.  In order of precedence, the following files are searched for:

* MyGet.bat, MyGet.cmd or MyGet.ps1
* build.bat, build.cmd or build.ps1
* MyGet.sln
* Any other *.sln file
* *.csproj (and *.vbproj, etc)
* *.nuspec (yep, we support packaging simple [convention-based NuGet directories](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package#From_a_convention_based_working_directory "Convention Based Nuget Directories") as well)

Based on the files found, the build process will be slightly different.

### Build process for solution files and project files

The following build steps will be run when building from solution or project files:

* Fetch source code
* Run package restore
* Patch AssemblyInfo (if enabled)
* Build solution or project file(s)
* Run unit tests
* Create NuGet packages
* Push packages to your MyGet feed
* Label sources (if enabled)

### Build process for batch / PowerShell based builds

The following build steps will be run when building from batch or PowerShell scripts:

* Fetch source code
* Patch AssemblyInfo (if enabled)
* Run batch or PowerShell script
* Push packages to your MyGet feed
* Label sources (if enabled)

## AssemblyVersion patching

When enabled for the build, MyGet Build Services will patch AssemblyVersion attributes in C# and VB.NET code. We are using Roslyn as the engine for parsing and updating attribute values. This approach is much more reliable than the regular expression based approaches most build systems use.

Two attributes will be patched: AssemblyVersion and AssemblyInformationalVersion.

* The patched AssemblyVersion version is always in the form major.minor.patch. A package version 1.0.0 as well as 1.0.0-pre will yield an AssemblyVersion of 1.0.0.
* The patched AssemblyInformationalVersion version supports semantic versioning and can be in the form major.minor.patch as well as major.minor.patch-prerelease.

Patching of these attributes will occur whenever the feature is enabled, no matter which build process is used (solution, project or build.bat).

## Pushing symbols

By default, when symbols packages (*.sympols.nupkg) are created, MyGet Build Services will push symbols packages to [SymbolSource](http://www.symbolsource.org). When disabled for the build, no symbols packages will be pushed.

## Source labeling (tagging)

When enabled in the build source configuration on MyGet, source code can be labeled with the build number. This can be done for successful builds only (recommended) as well as for failed builds.

The label originating from MyGet will always be named in the form ```vX.Y.Z```, ```vX.Y.Z.P``` or ```v.X.Y.Z-pre```. The description for the label will always be the label name (the version number), followed by "- MyGet Build Services".

Note that for labeling sources, you must provide credentials that can commit to the originating source repository. If omitted, labeling will fail.

The labeling scheme is compatible with [GitHub releases](https://help.github.com/articles/about-releases) and can link a given NuGet package version number to a GitHub release.

<p class="info">
    <strong>Note:</strong> If you enable build labeling on build configurations that have been created before 2013-09-11, <strong>you will have to</strong> add the build configuration again or specify credentials to connect to the remote repository. Builds with labeling enabled will fail if this is neglected.
</p>

## Package Restore

Since NuGet 2.7 was released, MyGet Build Services runs NuGet Package Restore as part of every build of solution or project files even if it's not enabled for the solution. Note that package restore is _not_ run for builds making use of batch or PowerShell scripts. In those cases, you are the responsible for running package restore.

In order of precedence, the following package restore commands are run. When one succeeds, package other commands will be skipped.

* ```nuget restore MyGet.sln -NoCache -NonInteractive -ConfigFile MyGet.NuGet.config```
* ```nuget restore MyGet.sln -NoCache -NonInteractive -ConfigFile NuGet.config```
* ```nuget restore <your solution file> -NoCache -NonInteractive -ConfigFile MyGet.NuGet.config```
* ```nuget restore <your solution file> -NoCache -NonInteractive -ConfigFile NuGet.config```
* ```nuget restore packages.config -NoCache -NonInteractive -ConfigFile MyGet.NuGet.config```
* ```nuget restore packages.config -NoCache -NonInteractive -ConfigFile NuGet.config```
* ```nuget restore MyGet.sln -NoCache -NonInteractive```
* ```nuget restore <your solution file> -NoCache -NonInteractive```
* ```nuget restore packages.config -NoCache -NonInteractive```

If you want MyGet Build Services to restore packages from a specific feed, there are two available options. One is to add package sources to your feed through the MyGet UI, the other is adding a ```MyGet.NuGet.config``` file to your repository is the key to success. See the [NuGet docs](http://docs.nuget.org/docs/reference/nuget-config-file) for more information on how such file can be created. The following is a sample registering a custom NuGet feed for package restore.

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <packageSources>
	    <add key="Chuck Norris feed" value="https://www.myget.org/F/chucknorris/api/v2/" />
	  </packageSources>
	  <activePackageSource>
	    <add key="All" value="(Aggregate source)" />
	  </activePackageSource>
	</configuration>

## Package Sources

MyGet gives you the option to specify one or more package sources for a feed. Package sources for a feed are also available during build. This can be useful in the following scenarios:

* An additional package source is needed during build. MyGet will make the package source available during build if it has been added to the feed's package sources.
* If you have an authenticated feed but do not wish to add credentials to source control, credentials can be added to the feed's package source. These credentials will be available during build and allow you to consume a protected feed with ease.

## Supported project types and SDK

Build services supports the following frameworks and SDK's:

* .NET 2.0, .NET 3.0, .NET 3.5, .NET 4.0, .NET 4.5 and .NET 4.5.1
* WinRT Class Libraries (Windows 8.0 and 8.1)
* PCL support (2012)
* Windows Azure SDK 1.7, 1.8, 2.0, 2.1, 2.2
* Windows Identity Foundation SDK
* Silverlight 4, Silverlight 5
* Windows Phone 7.1 SDK, Windows Phone 8 SDK
* TypeScript SDK
* psake
* ripple

The following test runners are supported:

* MsTest
* MbUnit 2, MbUnit 3
* NBehave
* NUnit (up to 2.6.2)
* xUnit.net
* csUnit
* RSpec

We believe adding frameworks and SDK's out-of-the-box provides a lot of value to our users and we want to continue investing in expanding the number of supported SDK's.

## Available Environment Variables

If you provide your own build.bat script or MyGet.sln, you can specifically instruct MyGet Build Services on how to act on your sources. This also means you'll need to take care of applying a version number to your build. That's why we provide you with the following set of parameters so you can benefit from using the version scheme you have already defined within the MyGet User Interface, as well as the build-counter attached to your build source. 

<p class="info">
    <strong>Note:</strong> that these environment variables are read-only and are reset to the initial values at the start of the build process.
</p>

<table class="reference">
	<tbody>
    	<tr>
    	    <th>Environment Variable Name</th> 
			<th>Description</th>
    	</tr>
    	<tr>
    	    <td><strong>%BuildRunner%</strong></td>
    	    <td>
    	        Always <code>MyGet</code> can be used to determine if running on MyGet Build Services
	        </td>
    	</tr>
    	<tr>
    	    <td><strong>%NuGet%</strong></td>
    	    <td>Path to a maintained-by-MyGet NuGet.exe</td>
    	</tr>
    	<tr>
    	    <td><strong>%SourcesPath%<strong></td>
    	    <td>
    	        Path to source code being built        
    	    </td>
    	</tr>
    	<tr>
    	    <td><strong>%Configuration%</strong></td>
    	    <td>Build configuration (defaults to Debug)</td>
    	</tr>
    	<tr>
    	    <td><strong>%Platform%</strong></td>
    	    <td>
    	        Platform to build (defaults to blank)
    	    </td>
    	</tr>
    	<tr>
    	    <td><strong>%VersionFormat%</strong></td>
    	    <td>
    	        Version format specified in build configuration
    	    </td>
    	</tr>
    	<tr>
    	    <td><strong>%BuildCounter%</strong></td>
    	    <td>
    	        Build counter value
    	    </td>
    	</tr>
    	<tr>
    	    <td><strong>%PackageVersion%</strong></td>
    	    <td>
    	        %VersionFormat% with %BuildCounter% filled in, used as the auto-generated package version number
	        </td>
    	</tr>
    	<tr>
    	    <td><strong>%EnableNuGetPackageRestore%</strong></td>
    	    <td>NuGet package restore enabled? Always true.</td>
    	</tr>
    	<tr>
    	    <td><strong>%GallioEcho%</strong></td>
    	    <td>Path to the Gallio.Echo.exe test runner</td>
    	</tr>
	</tbody>
</table>

## GitHub Status API

When a build source is linked to a GitHub repository and has credentials specified, MyGet Build Services will make use of the [GitHub Commit Status API](https://github.com/blog/1227-commit-status-api) to report status messages about a build back to GitHub and makign them visible with commits and pull requests on GitHub. The status message posted to GitHub reflects the build status and links to the build log on MyGet.

![GitHub Commit Status](Images/github_commit_status.png)

To enable GitHub Commit Status messages on your builds, make sure the build configuration has credentials specified. Specifying credentials can be done by removing and adding the build configuration again, a method which doesn't require you to enter your password. You can also specify credentials manually by editing the build source.
