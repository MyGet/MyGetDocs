# Build Services

MyGet Build Services allows you to connect to different types of source control systems:

* Git
* Mercurial (Hg)
* Subversion

Next to that, integration with several Source Control Repositories is available as well:

* [GitHub](https://github.com/ "GitHub")
* [BitBucket](https://bitbucket.org "BitBucket")
* [Visual Studio Online](http://www.visualstudio.com)
* [CodePlex](http://www.codeplex.com/ "CodePlex")

Once downloaded, source code can then be built using a number of different methodologies.

<p class="alert alert-info">
    <strong>Note:</strong> MyGet Build Services has a 5 minute cooldown period between builds during which you can't trigger a build, manually or otherwise. Please contact MyGet support for more information about our dedicated Build Services offering to avoid this cooldown period.
</p>

## The Build Process

Using MyGet Build Services, you have the opportunity to control exactly how your project gets built. MyGet Build Services works based on several conventions to run builds. It will scan the contents of your Source Control Repository looking for a file which it can work with.  In order of precedence, the following files are searched for:

* Project files (*.csproj, *.vbproj, *.fsproj, ...) that are explicitly [specified in the build source configuration](#Configuring_Projects_To_Build).
* MyGet.bat, MyGet.cmd or MyGet.ps1
* build.bat, build.cmd or build.ps1
* MyGet.sln
* Any other *.sln file
* *.csproj (and *.vbproj, *.fsproj, ...)
* *.nuspec (yep, we support packaging simple [convention-based NuGet directories](http://docs.nuget.org/create/creating-and-publishing-a-package#package-conventions "Convention Based Nuget Directories") as well)

Based on the files found, the build process will be slightly different.

### Build process for solution files and project files

The following build steps will be run when building from solution or project files:

* Fetch source code
* Run package restore
* Patch AssemblyInfo (if enabled)
* Build solution or project file(s)
* Run unit tests (see [Which tests are run?](#Which_tests_are_run))
* Create NuGet packages (see [Which packages are created?](#Which_packages_are_created))
* Push packages to your MyGet feed
* Label sources (if enabled)

### Build process for batch / PowerShell based builds

The following build steps will be run when building from batch or PowerShell scripts:

* Fetch source code
* Patch AssemblyInfo (if enabled)
* Run batch or PowerShell script
* Push packages to your MyGet feed
* Label sources (if enabled)

### Configuring Projects to Build

When needed, the project files or scripts to build can be specified in the build source configuration. If this setting is omitted, the [default build process conventions](#The_Build_Process) will be used.

This setting can contain projects, solutions and scripts (like .bat, .cmd and .ps1 files) but not mixed together (e.g. .csproj and .bat will result in only the .csproj to be built). Note that when configured, pre- and post-build scripts will be ignored unless manually added to this list.

![Configure Projects to Build](Images/configure-projects-to-build.png)

### Pre- and post-build steps

When using [batch / PowerShell based builds](#Build_process_for_batch__PowerShell_based_builds), MyGet Build Services will scan for batch files to be executed. In addition to the MyGet.bat (or .cmd or .ps1) and build.bat (or .cmd or .ps1), we search for pre- and post-build steps as well. These can be batch scripts or PowerShell scripts that are run before and after the actual build file.

The following files are detected as being pre-build steps:

* pre-MyGet.bat, pre-MyGet.cmd or pre-MyGet.ps1
* pre-build.bat, pre-build.cmd or pre-build.ps1

The following files are detected as being post-build steps:

* post-MyGet.bat, post-MyGet.cmd or post-MyGet.ps1
* post-build.bat, post-build.cmd or post-build.ps1

## AssemblyVersion patching

When enabled for the build, MyGet Build Services will patch AssemblyVersion attributes in C# and VB.NET code. We are using Roslyn as the engine for parsing and updating attribute values. This approach is much more reliable than the regular expression based approaches most build systems use.

Two attributes will be patched: AssemblyVersion and AssemblyInformationalVersion.

* The patched AssemblyVersion version is always in the form major.minor.patch. A package version 1.0.0 as well as 1.0.0-pre will yield an AssemblyVersion of 1.0.0.
* The patched AssemblyInformationalVersion version supports semantic versioning and can be in the form major.minor.patch as well as major.minor.patch-prerelease.

Patching of these attributes will occur whenever the feature is enabled, no matter which build process is used (solution, project or build.bat).

<p class="alert alert-info">
    <strong>Note:</strong> For DNX-based builds, we will patch the <code>project.json</code> file when assembly version patching is enabled for the build source.
</p>

## Which tests are run?

When running a convention-based build (so without build scripts), MyGet Build Services by default will scan your built projects for any assemblies that contain the word **Test** in the filename, and run them.

You can however instruct MyGet Build Services not to run certain test projects by simply not building them. This can be achieved by [explicitly defining a list of projects to build](#Configuring_Projects_to_Build) in the Build Source settings.

When running a scripted build, you can of course modify your build script accordingly. We have a few [sample build scripts that demonstrate this](custom-build-scripts).

## Which packages are created?

When you use MyGet's default build conventions and simply let us handle the build of your solution, then we will perform a few steps to determine which packages should be created.

Here's the workflow:

* Verify if packages were created during compilation (for those using MSBuild for creating packages).

  If any `.nupkg` file is found in any directory (that was not present in source control or has not been downloaded during package restore), we don't bother creating any additional packages.

* If no `.nupkg` files were found, we scan for *packageable* files.

  A packageable file is any `.csproj`, `.vbproj`, `.fsproj` or `.nuspec` file. We ignore any file with the word `test` in its path.

  Since only a few of you are building test frameworks, we feel we shouldn't annoy the majority of people by creating and publishing test assemblies by default. If you are creating a test framework, you can always customize the build process using a build script.

* We call the following command on all found *packageable* files:

  ```
  nuget.exe pack "{0}" -OutputDirectory "{1}"
                       -Prop Configuration={2}
                       -NoPackageAnalysis
                       -Symbols
                       {3}
  ```

  where

  {0} = path to packageable file,

  {1} = `\bin` subdirectory of packageable path,

  {2} = build configuration (as specified in build source or `'Release'` by default),

  {3} = optionally appended `"-Version {4}"` if version patching is enabled

* Note that we by default always create symbols packages

The above workflow also clearly prefers targeting project files if present. This means that you can benefit from this by having a companion `projectname.nuspec` file next to your `projectname.csproj` (within the same directory!). [Check this StackOverflow thread](http://stackoverflow.com/questions/14797525/differences-between-nuget-packing-a-csproj-vs-nuspec/14808085#14808085) if you want to learn about the differences between targeting a project file and targeting a nuspec file directly.

## Which packages are pushed to my feed?

By default, MyGet will push all NuGet packages generated during build to your feed, except for packages that were present in source control or have been downloaded during package restore. The reason for this is that the *packages* folder is reserved by NuGet itself and may contain packages that were used during the build process and are not necessarily to be added to your feed. Note that when creating a batch-based build, a good practice is to generate packages in a folder that is not named *packages*. A good example would be *output*.

To override this default behaviour, a series of filters can be specified in the build configuration. When omitted, all packages generated during build will be pushed to your feed. When specified, only packages matching any of the specified filters or wildcards will be pushed to your feed.

![Configure Packages to Push](Images/configure-packages-to-push.png)

Filters can be of different types:

* Plain text, e.g. `MyPackage` to match a specific package id.
* Containing a wildcard, e.g. `MyPackage.*` to match all package id's that start with `MyPackage.`.
* Starting with a negation, e.g. `!MyPackage.*` to explicitly exclude all package id's starting with `Mypackage.*`.

Filters are executed in order of precedence. If a negation comes first, packages matching the negation will be excluded, even if the next rule defines to include the package.

<p class="alert alert-info">
    <strong>Note:</strong> By default, MyGet generates prerelease packages. Configure the client correctly or edit the build source to change the version number template and generate release packages instead.
</p>

## Pushing symbols

By default, MyGet pushes symbols packages (*.sympols.nupkg) to the current feed. If you prefer pushing to [SymbolSource](http://www.symbolsource.org) instead, edit the build source and select the target for pushing symbols packages.

![Change target for symbols](Images/symbols-options-build.png)

Pushing symbols can also be disabled for the build, in which case no symbols packages will be pushed.

<div class="alert alert-block">
  <strong>Warning!</strong><br/>
  SymbolSource support is being deprecated and support for it <strong>will end on November 1, 2016</strong>. We recommend using the <a href="/docs/reference/symbols">MyGet symbol server</a> instead.
</div>

## Source labeling (tagging)

When enabled in the build source configuration on MyGet, source code can be labeled with the build number. This can be done for successful builds only (recommended) as well as for failed builds.

The label originating from MyGet will always be named in the form ```vX.Y.Z```, ```vX.Y.Z.P``` or ```v.X.Y.Z-pre```. The description for the label will always be the label name (the version number), followed by "- MyGet Build Services".

Note that for labeling sources, you must provide credentials that can commit to the originating source repository. If omitted, labeling will fail.

The labeling scheme is compatible with [GitHub releases](https://help.github.com/articles/about-releases) and can link a given NuGet package version number to a GitHub release.

<p class="alert alert-info">
    <strong>Note:</strong> If you enable build labeling on build configurations that have been created before 2013-09-11, <strong>you will have to</strong> add the build configuration again or specify credentials to connect to the remote repository. Builds with labeling enabled will fail if this is neglected.
</p>

## Package Restore

Since NuGet 2.7 was released, MyGet Build Services runs NuGet Package Restore as part of every build of solution or project files even if it's not enabled for the solution. Note that package restore is _not_ run for builds making use of batch or PowerShell scripts. In those cases, you are the responsible for running package restore.

In order of precedence, the following package restore commands are run. When one succeeds, other commands will be skipped.

* ```nuget restore MyGet.sln -NoCache -NonInteractive -ConfigFile MyGet.NuGet.config```
* ```nuget restore MyGet.sln -NoCache -NonInteractive -ConfigFile NuGet.config```
* ```nuget restore <your solution file> -NoCache -NonInteractive -ConfigFile MyGet.NuGet.config```
* ```nuget restore <your solution file> -NoCache -NonInteractive -ConfigFile NuGet.config```
* ```nuget restore packages.config -NoCache -NonInteractive -ConfigFile MyGet.NuGet.config```
* ```nuget restore packages.config -NoCache -NonInteractive -ConfigFile NuGet.config```
* ```nuget restore packages.<project>.config -NoCache -NonInteractive -ConfigFile MyGet.NuGet.config```
* ```nuget restore packages.<project>..config -NoCache -NonInteractive -ConfigFile NuGet.config```
* ```nuget restore MyGet.sln -NoCache -NonInteractive```
* ```nuget restore <your solution file> -NoCache -NonInteractive```
* ```nuget restore packages.config -NoCache -NonInteractive```
* ```nuget restore packages.<project>.config -NoCache -NonInteractive```

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
* The API key for a package source is also transferred to the build server. This means during a build, you can call into ```nuget.exe push``` and push packages to configured package sources.
* You want to make use of ```nuget.exe push``` in a build script without having to specify the ```-Source``` parameter.

### Setting default package sources during build

The ```NuGet.config``` on our build machines is configured using NuGet's defaults, enriched with all package sources configured for a feed. Based on these defaults, the following conventions are active:

* The default package source is set to ```(Aggregate Source)```, meaning all feeds will be queried for packages in the order defined in the feed's package sources.
* The default push source (when using ```nuget push``` without the ```-Source``` parameter) is NuGet.org.

Both of these conventions can be overridden by editing the build source configuration.

![Setting default package sources during build](Images/package-source-defaults.png)

## Supported project types and SDK

Build services supports the following frameworks and SDK's:

* .NET 2.0, .NET 3.0, .NET 3.5, .NET 4.0, .NET 4.5, .NET 4.5.1, .NET 4.5.2, .NET 4.6, .NET 4.6.1
* .NET Core
* Mono 2, Mono 3
* Visual Studio 2013 Update 4
* Visual Studio 2015 Update 3
* Visual Studio 2013 SDK
* Visual Studio 2015 SDK
* WinRT Class Libraries (Windows 8.0 and 8.1)
* Universal Apps support
* Xamarin support
* PCL support
* Azure SDK 2.0, 2.1, 2.4, 2.5, 2.6, 2.7, 2.8, 2.8.1, 2.8.2, 2.9)
* Azure Service Fabric SDK
* Windows Identity Foundation SDK
* Silverlight 4, Silverlight 5
* Windows Phone 7.1 SDK, Windows Phone 8 SDK
* Lightswitch
* Office Developer Tools (2013)
* TypeScript SDK
* FSharp Tools 3.1.1
* MSBuild Community Tasks
* NAnt
* Ruby 1.8.7, 1.9.2, 1.9.3
* Python (2 and 3, [see documentation](https://docs.python.org/3/using/windows.html))
* JDK 1.5, 1.6, 1.7, 1.8
* Go
* ScriptCS
* psake
* ripple
* Git

* node.js, npm
* Grunt
* Gulp
* Bower
* Go programming language

The following test runners are supported:

* MsTest
* MbUnit 2, MbUnit 3
* NBehave
* NUnit (up to 2.6.2)
* xUnit 1.0
* xUnit 1.9.2
* xUnit 2.0
* csUnit
* RSpec
* Pester (v3.3.6)

We believe adding frameworks and SDK's out-of-the-box provides a lot of value to our users and we want to continue investing in expanding the number of supported SDK's.

## Available Environment Variables

If you provide your own build.bat script or MyGet.sln, you can specifically instruct MyGet Build Services on how to act on your sources. This also means you'll need to take care of applying a version number to your build. That's why we provide you with the following set of parameters so you can benefit from using the version scheme you have already defined within the MyGet User Interface, as well as the build-counter attached to your build source.

<p class="alert alert-info">
    <strong>Note:</strong> These environment variables are read-only and are reset to the initial values at the start of the build process.
</p>

<table class="table table-condensed">
	<thead>
    	<tr>
    	    <th>Environment Variable Name</th>
    	    <th>Description</th>
    	</tr>
	</thead>
	<tbody>
    	<tr>
    	    <td><strong>%BuildRunner%</strong></td>
    	    <td>
    	        Always <code>MyGet</code>, can be used to determine if running on MyGet Build Services
    	    </td>
    	</tr>
    	<tr>
    	    <td><strong>%NuGet%</strong></td>
    	    <td>Path to a maintained-by-MyGet NuGet.exe</td>
    	</tr>
    	<tr>
    	    <td><strong>%MsBuildExe%</strong></td>
    	    <td>Path to msbuild.exe</td>
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
    	    <td><strong>%Targets%</strong></td>
    	    <td>Build targets (defaults to Rebuild)</td>
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
    	    <td><strong>%PrereleaseTag%</strong></td>
    	    <td>
    	        The prerelease tag generated based on the VersionFormat and BuildCounter values
    	    </td>
    	</tr>
    	<tr>
    	    <td><strong>%PackageVersion%</strong></td>
    	    <td>
    	        <code>%VersionFormat%</code> with <code>%BuildCounter%</code> filled in, used as the auto-generated package version number
	    </td>
    	</tr>
    	<tr>
    	    <td><strong>%EnableNuGetPackageRestore%</strong></td>
    	    <td>NuGet package restore enabled? Always true.</td>
    	</tr>
    	<tr>
    	    <td><strong>%GallioEcho%</strong></td>
    	    <td>Path to the <code>Gallio.Echo.exe</code> test runner executable.</td>
    	</tr>
    	<tr>
    	    <td><strong>%XUnit192Path%</strong></td>
    	    <td>Path to XUnit 1.9.2. <code>xunit.console.clr4.exe</code>, <code>xunit.console.clr4.x86.exe</code>, <code>xunit.console.exe</code> and <code>xunit.console.x86.exe</code> are located under this path.</td>
    	</tr>
    	<tr>
    	    <td><strong>%XUnit20Path%</strong></td>
    	    <td>Path to XUnit 2.0. <code>xunit.console.exe</code> and <code>xunit.console.x86.exe</code> are located under this path.</td>
    	</tr>
    	<tr>
    	    <td><strong>%VsTestConsole%</strong></td>
    	    <td>Path to <code>VsTest.Console.exe</code></td>
    	</tr>
	</tbody>
</table>

### Git-based builds

For Git-based builds, the following environment variables are added:

<table class="table table-condensed">
	<thead>
    	<tr>
    	    <th>Environment Variable Name</th>
    	    <th>Description</th>
    	</tr>
	</thead>
	<tbody>
    	<tr>
    	    <td><strong>%GitPath%</strong></td>
    	    <td>
    	        Path to <code>git</code> executable
    	    </td>
    	</tr>
    	<tr>
    	    <td><strong>%GitVersion%</strong></td>
    	    <td>
    	        Path to <code>gitversion</code> executable (see <a href="https://github.com/Particular/GitVersion">GitVersion documentation</a>)
    	    </td>
    	</tr>
	</tbody>
</table>

## GitVersion and Semantic Versioning

When a Git-based build is executed, it is possible to run [GitVersion](https://github.com/Particular/GitVersion) during your build. This utility uses a convention to derive a SemVer product version from a GitFlow based repository.

Running ```GitVersion /output buildserver``` will cause MyGet Build Services to set the current ```%PackageVersion%``` value to the NuGet-compatible SemVer generated by GitVersion.

We advise to run a tool like GitVersion in a pre-build script so that it can set additional environment variables for the actual build script. GitVersion outputs [service messages](#Service_Messages) to provide these variables. The variables are explained and demonstrated in the [GitVersion documentation](https://github.com/Particular/GitVersion/wiki/Command-Line-Tool#consume-gitversion-from-build-scripts).

	GitVersion.Major=3
	GitVersion.Minor=0
	GitVersion.Patch=23
	GitVersion.PreReleaseTag=
	GitVersion.PreReleaseTagWithDash=
	GitVersion.BuildMetaData=0
	GitVersion.FullBuildMetaData=0.Branch.master.Sha.4898f534dfd906bd3a56818752f5fa4e16c31267
	GitVersion.MajorMinorPatch=3.0.23
	GitVersion.SemVer=3.0.23
	GitVersion.LegacySemVer=3.0.23
	GitVersion.LegacySemVerPadded=3.0.23
	GitVersion.AssemblySemVer=3.0.23.0
	GitVersion.FullSemVer=3.0.23+0
	GitVersion.InformationalVersion=3.0.23+0.Branch.master.Sha.4898f534dfd906bd3a56818752f5fa4e16c31267
	GitVersion.ClassicVersion=3.0.23.0
	GitVersion.ClassicVersionWithTag=3.0.23.0
	GitVersion.BranchName=master
	GitVersion.Sha=4898f534dfd906bd3a56818752f5fa4e16c31267
	GitVersion.AssemblyVersion=3.0.23.0
	GitVersion.AssemblyFileVersion=3.0.23.0
	GitVersion.OriginalRelease=4898f534dfd906bd3a56818752f5fa4e16c31267.2014-08-26 13:32:36Z
	GitVersion.NuGetVersionV2=3.0.23
	GitVersion.NuGetVersion=3.0.23

<p class="alert alert-info">
    <strong>Note:</strong> If you require the <code>AssemblyInfo.cs</code> files in your project to be patched with the information from GitVersion, you will have to run it manually, for example using the command <code>call %GitVersion% /updateassemblyinfo true</code>.
</p>

## User-defined environment variables

Sometimes you may want to pass in a value to the build scripts without hard-coding it into the build script. MyGet supports setting additional environment variables that can be used in custom build scripts as well as MSBuild. From the Build Source configuration, you can add user-defined environment variables that will be made available during build.

![Environment variables](Images/environment-variable.png)

Environment variables can be used in your builds:

* MSBuild: use the ```$(VARIABLE_KEY)``` syntax
* Batch files: use the ```%VARIABLE_KEY%``` syntax
* PowerShell: use the ```$env:VARIABLE_KEY``` syntax

Environment variables will be shown in the build log. If needed, environment variable values can be hidden in the build log using the open/closed eye button.

![Environment variables in log](Images/environment-variable-log.png)

## User-defined environment variable expansions

Sometimes you may want to define an environment variable and pass in a value that is based on another environment variable value.
You can easily reference existing environment variables by using the %-annotation, as shown in the examples below:

	DNX_BUILD_VERSION = %PrereleaseTag%
	OutputPath = bin\%Configuration%\%PackageVersion%

## Service Messages

It is possible for a build to interact with MyGet Build Services using Service Messages. For example writing a message to the build log, setting the package version or triggering a build failure.

Service messages are strings that have a specific format that MyGet recognizes and can parse. Being strings, they can be written using an `echo` or the MSBuild `Message` task. The format of a service message can be:

* `##myget[messageName 'value']`, passing a single value to the build
* `##myget[messageName key='value' key='value']`, passing multiple values to the build

Special characters (like `\r`, `\n`, `'`, `[` and `]`) should be prefixed with a `|`.

<p class="alert alert-info">
    <strong>Note:</strong> MyGet Build Services also parses <a href="https://confluence.jetbrains.com/display/TCD8/Build+Script+Interaction+with+TeamCity">TeamCity</a> service messages. This means all tools that use TeamCity-specific service messages for communication with the build process will also work with MyGet Build Services.
</p>

### Overriding the Package Version

The `%PackageVersion%` [environment variable](#Available_Environment_Variables) can be changed by using the `buildNumber` service message, for example:

	##myget[buildNumber '1.0.0-beta1']

Note that this should be done in a [pre-build step](#Pre-_and_post-build_steps), as the environment variable is not updated in the running script (this could be done by just setting the environment variable manually).

### Explicitly publishing a package

Packages can be published explicitly to a MyGet feed by using the `publishPackage` service message, for example:

	##myget[publishPackage path='<relative-path-no-wildcards>' type='nuget|symbols|npm|bower|vsix']

This service message requires the relative path to the package (from the root of the source control repository). Wildcards are not supported: each artifact that needs to be published has to be enumerated.

Note that MyGet Build Services also applies [package filters](#Which_packages_are_pushed_to_my_feed) when the `publishPackage` service message is used. If a filter excludes a given package, the package will not be published to the MyGet feed.

### Writing messages to the build log

Messages can be written to the build log using the `message` service message, for example:

	##myget[message text='Text to write' errorDetails='stack trace' status='ERROR']

The `errorDetails` are not required. The `status` defaults to `NORMAL` and can be set to `NORMAL`, `WARNING`, `FAILURE` or `ERROR`.

### Setting environment variables for a future process

The `setParameter` service message allows setting an environment variable that can be used by a future process. For example, in a [pre-build step](#Pre-_and_post-build_steps) an environment variable can be set for the actual build script that runs later. The `setParameter` service message has the following format:

	##myget[setParameter name='Name' value='Value']

Note that this should be done in a [pre-build step](#Pre-_and_post-build_steps), as the environment variable is not updated in the running script (this could be done by just setting the environment variable manually).

### Reporting build failure

Using the `buildProblem` service message, a build failure can be triggered. This will stop the build at the exact position the message was written to output. For example:

	##myget[buildProblem description='Build failed because of this error']

## Build Status Badges

You can embed a status image for a build into any web page out there, including your project’s README file or documentation. Your users will be immediately updated about the status of the last build performed. Here’s an example badge for a successful build:

![Successful Build](Images/successful.png)

Badges will be shown for pending builds (queued or building) as well as successful and failed builds. The URL for a build badge can be obtained through the Build Services configuration:

![Where to get the URL](Images/build-badge.png)

It can then be used in HTML, for example with a hyperlink to your feed on the [MyGet Gallery](http://www.myget.org/gallery):

<pre><code>&lt;a href="https://www.myget.org/gallery/googleanalyticstracker"&gt;&lt;img alt="GoogleAnalyticsTracker Nightly Build Status" src="https://www.myget.org/BuildSource/Badge/googleanalyticstracker?identifier=479ff619-28f2-47c0-9574-2774ed0cd855" /&gt;&lt;/a&gt;</code></pre>

You can do the same in Markdown:

<pre><code>[![GoogleAnalyticsTracker Nightly Build Status](https://www.myget.org/BuildSource/Badge/googleanalyticstracker?identifier=479ff619-28f2-47c0-9574-2774ed0cd855)](https://www.myget.org/gallery/googleanalyticstracker)</code></pre>

Of course, you can also use it in any other markup language that supports embedding images.

## GitHub Status API

When a build source is linked to a GitHub repository and has credentials specified, MyGet Build Services will make use of the [GitHub Commit Status API](https://github.com/blog/1227-commit-status-api) to report status messages about a build back to GitHub and making them visible with commits and pull requests on GitHub. The status message posted to GitHub reflects the build status and links to the build log on MyGet.

![GitHub Commit Status](Images/github_commit_status.png)

To enable GitHub Commit Status messages on your builds, make sure the build configuration has credentials specified. Specifying credentials can be done by removing and adding the build configuration again, a method which doesn't require you to enter your password. You can also specify credentials manually by editing the build source.
