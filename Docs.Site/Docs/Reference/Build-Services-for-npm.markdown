# Build Services for Npm

MyGet Build Services allows you to connect to different types of source control systems:

* Git
* Mercurial (Hg)
* Subversion

Next to that, integration with several Source Control Repositories is available as well:

* [GitHub](https://github.com/ "GitHub")
* [BitBucket](https://bitbucket.org "BitBucket")
* [CodePlex](http://www.codeplex.com/ "CodePlex")

Once downloaded, source code can then be built using a number of different methodologies.

<p class="alert alert-info">
    <strong>Note:</strong> Although fully operational, MyGet Build Services is currently still in the Beta Stage.  While in Beta, you can't trigger a Build, manually or otherwise, faster then one every 5 minutes
</p>

## The Build Process

Using MyGet Build Services, you have the opportunity to control exactly how your project gets built. MyGet Build Services works based on several conventions to run builds. It will scan the contents of your Source Control Repository looking for a file which it can work with.  In order of precedence, the following files are searched for:

* Scripts (*.bat, *.cmd, *.ps1) that are explicitly [specified in the build source configuration](#Configuring_Projects_To_Build).
* MyGet.bat, MyGet.cmd or MyGet.ps1
* build.bat, build.cmd or build.ps1
* package.json

Based on the files found, the build process will be slightly different.

### Build process for package.json files

The following build steps will be run when building from package.json files:

* Fetch source code
* Set npm registry to the current feed
* Update package.json and set the version value to [%PackageVersion%](Available Environment Variables)
* Run npm install
* Run npm test
* Run npm pack
* Push packages to your MyGet feed
* Label sources (if enabled)

### Build process for batch / PowerShell based builds

The following build steps will be run when building from batch or PowerShell scripts:

* Fetch source code
* Run batch or PowerShell script
* Push packages to your MyGet feed
* Label sources (if enabled)

### Configuring Projects to Build

When needed, the project files or scripts to build can be specified in the build source configuration. If this setting is omitted, the [default build process conventions](#The_Build_Process) will be used.

This setting can contain scripts (like .bat, .cmd and .ps1 files). Note that when configured, pre- and post-build scripts will be ignored unless manually added to this list.

![Configure Projects to Build](Images/configure-projects-to-build.png)

### Pre- and post-build steps

When using [batch / PowerShell based builds](#Build_process_for_batch__PowerShell_based_builds), MyGet Build Services will scan for batch files to be executed. In addition to the MyGet.bat (or .cmd or .ps1) and build.bat (or .cmd or .ps1), we search for pre- and post-build steps as well. These can be batch scripts or PowerShell scripts that are run before and after the actual build file.

The following files are detected as being pre-build steps:

* pre-MyGet.bat, pre-MyGet.cmd or pre-MyGet.ps1
* pre-build.bat, pre-build.cmd or pre-build.ps1

The following files are detected as being post-build steps:

* post-MyGet.bat, post-MyGet.cmd or post-MyGet.ps1
* post-build.bat, post-build.cmd or post-build.ps1

## Which packages are pushed to my feed?

By default, MyGet will push all npm packages generated during build to your feed, except for packages that are found in `node_modules`. The reason for this is that the `node_modules` folder is reserved by npm itself and may contain packages that were used during the build process and are not necessarily to be added to your feed. Note that when creating a batch-based build, a good practice is to generate packages in a folder that is not named `node_modules`. A good example would be *output*.

To override this default behaviour, a series of filters can be specified in the build configuration. When omitted, all packages generated during build will be pushed to your feed. When specified, only packages matching any of the specified filters or wildcards will be pushed to your feed.

![Configure Packages to Push](Images/configure-packages-to-push.png)

Filters can be of different types:

* Plain text, e.g. `MyPackage` to match a specific package id.
* Containing a wildcard, e.g. `MyPackage.*` to match all package id's that start with `MyPackage.`.
* Starting with a negation, e.g. `!MyPackage.*` to explicitly exclude all package id's starting with `Mypackage.*`.

Filters are executed in order of precedence. If a negation comes first, packages matching the negation will be excluded, even if the next rule defines to include the package.

## Source labeling (tagging)

When enabled in the build source configuration on MyGet, source code can be labeled with the build number. This can be done for successful builds only (recommended) as well as for failed builds.

The label originating from MyGet will always be named in the form ```vX.Y.Z```, ```vX.Y.Z.P``` or ```v.X.Y.Z-pre```. The description for the label will always be the label name (the version number), followed by "- MyGet Build Services".

Note that for labeling sources, you must provide credentials that can commit to the originating source repository. If omitted, labeling will fail.

The labeling scheme is compatible with [GitHub releases](https://help.github.com/articles/about-releases) and can link a given npm package version number to a GitHub release.

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

Running ```GitVersion /output buildserver``` will cause MyGet Build Services to set the current ```%PackageVersion%``` value to the npm-compatible SemVer generated by GitVersion.

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

## User-defined environment variables

Sometimes you may want to pass in a value to the build scripts without hard-coding it into the build script. MyGet supports setting additional environment variables that can be used in custom build scripts as well as MSBuild. From the Build Source configuration, you can add user-defined environment variables that will be made available during build.

![Environment variables](Images/environment-variable.png)

Environment variables can be used in your builds:

* MSBuild: use the ```$(VARIABLE_KEY)``` syntax
* Batch files: use the ```%VARIABLE_KEY%``` syntax
* PowerShell: use the ```$env:VARIABLE_KEY``` syntax

Environment variables will be shown in the build log. If needed, environment variable values can be hidden in the build log using the open/closed eye button.

![Environment variables in log](Images/environment-variable-log.png)

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
