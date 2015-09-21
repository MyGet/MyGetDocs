# NuGet Configuration Inheritance

By default, all NuGet clients (the command-line tool, the Visual Studio extension and the Package Manager Console) all make use of the default NuGet configuration file which lives under *%AppData%\NuGet\NuGet.config*. NuGet can make use of other configuration files as well! In fact, NuGet can walk an entire tree of configuration files and fetch settings from those. 

## Which configuration file will be used?

While the answer to this question depends on the client being used (WebMatrix does it slightly different, for example), here’s a generalized version of the three that is walked for building the configuration the client will work with.

* The current directory and all its parents
* The user-specific config file located under %AppData%\NuGet\NuGet.config 
* IDE-specific configuration files, for example:
	* *%ProgramData%\NuGet\Config\{IDE}\{Version}\{SKU}\*.config* (e.g. *%ProgramData%\NuGet\Config\VisualStudio\12.0\Pro\NuGet.config*)
	* *%ProgramData%\NuGet\Config\{IDE}\{Version}\*.config *
	* *%ProgramData%\NuGet\Config\{IDE}\*.config *
	* *%ProgramData%\NuGet\Config\*.config*
* The machine-wide config file located under *%ProgramData%\NuGet\NuGetDefaults.config* (this is a good one to put default configuration options in by using an Active Directory Group Policy)

Full details can be found in the [NuGet docs](http://docs.nuget.org/docs/reference/nuget-config-file).

The interesting observation here is that all clients start with a *NuGet.config* in the current directory and then walk up to the drive root, and only then are the standard files checked.This means every parent folder of a project or solution can contain additional configuration details that will be applied (note: items the file that is checked first wins).

Here's another example: if you have a solution file C:\Projects\CustomerA\AwesomeSolution\AwesomeSolution.sln, all NuGet clients will load configuration values from: 

* C:\Projects\CustomerA\AwesomeSolution\NuGet.config
* C:\Projects\CustomerA\NuGet.config
* C:\Projects\NuGet.config
* C:\NuGet.config
* All the other locations mentioned above

This behaviour allows some interesting setups! Let's cover a few.

## Example 1: a project-specific configuration

Why would you let all your developers add the private feed used in your team feed to their NuGet configuration if a *NuGet.config* can be shipped in source control? Putting the following file right next to your .sln file will add the feed to every developer's configuration (for that solution):

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <packageSources>
	    <add key="Chuck Norris Feed" value="https://www.myget.org/F/chucknorris" />
	  </packageSources>
	</configuration>

Want to block access to NuGet.org and only use the private feed? Here is how:

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <packageSources>
	    <add key="Chuck Norris Feed" value="https://www.myget.org/F/chucknorris" />
	  </packageSources>
	  <disabledPackageSources>
	    <add key="nuget.org" value="true" />
	  </disabledPackageSources>
	  <activePackageSource>
	    <add key="Chuck Norris Feed" value="https://www.myget.org/F/chucknorris" />
	  </activePackageSource>
	</configuration>

## Example 2: help, developers are pushing our internal framework to NuGet.org!

When pushing packages to a private feed, it may happen that a developer forgets to use the *-Source* parameter to *NuGet.exe*, causing an accidental push to the default package source which is [NuGet.org](http://www.nuget.org). Place the following *NuGet.config* next to the .sln file and you should be good:

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <config>
	    <add key="DefaultPushSource" value="https://www.myget.org/F/chucknorris/api/v2/package" />
	  </config>
	</configuration>

Note this can be combined with the first example as well.

## Example 3: NuGet.exe keeps asking for proxy credentials

NuGet allows configuring the default proxy credentials that should be used. While it is possible to put this one in a project, it's probably better to do this in the default *%AppData%\NuGet\NuGet.config*:

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <config>
	    <add key="http_proxy" value="host" />
	    <add key="http_proxy.user" value="username" />
	    <add key="http_proxy.password" value="encrypted_password" />
	  </config>
	</configuration>

## Example 4: feed inheritance and package restore

Imagine the situation where your team has a specific feed they can use for every customer project. Every customer project can contain the following *NuGet.config*:

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <packageSources>
	    <add key="Customer X" value="https://www.myget.org/F/customerx" />
	  </packageSources>
	</configuration>

In the *C:\Projects* folder, we can add another configuration file which adds in another feed for every project located under *C:\Projects*. All customer projects typically use both of these feeds. Customer specific components are used, as well as that framework built in-house, each on their own feed. But all of a sudden, package restore starts complaining no package named X can be found!

The reason for that is probably the active package source is set to one specific feed and not the "aggregate" of all configured feeds. Here’s a solution to that which can go in *C:\Projects\NuGet.config*:

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <packageSources>
	    <add key="Our Cool Framework" value="https://www.myget.org/F/ourcoolframework" />
	  </packageSources>
	  <activePackageSource>
	    <add key="All" value="(Aggregate source)" />
	  </activePackageSource>
	</configuration>

<p class="alert alert-info">
    <strong>Note:</strong> Also see <a href="/docs/how-to/package-not-found-during-package-restore">Package not found during package restore</a>
</p>
