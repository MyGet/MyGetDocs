# Build Services

MyGet Build Services allows you to connect to three separate Source Control Repositories:

* [GitHub](https://github.com/ "GitHub")
* [BitBucket](https://bitbucket.org "BitBucket")
* [CodePlex](http://www.codeplex.com/ "CodePlex")

And once downloaded, the source code can then be built using a number of different methodologies.

<p class="info">
    <strong>NOTE:</strong> Although fully operational, MyGet Build Services is currently still in the Beta Stage.  While in Beta, you can't trigger a Build, manually or otherwise, faster then one every 5 minutes
</p>


## Tapping into the Build Process

Using MyGet Build Services, you have the opportunity to control exactly how your project gets built.  MyGet Build Services will scan the contents of your Source Control Repository looking for a file which it can work with.  In order of precedence, the following files are searched for:

* build.bat
* MyGet.sln
* Any other *.sln file
* *.csproj (and *.vbproj, etc)
* *.nuspec (yep, we support packaging simple [convention-based NuGet directories](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package#From_a_convention_based_working_directory "Convention Based Nuget Directories") as well)

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
	</tbody>
</table>