# Package not found during package restore

When working with your own feed, whether private or public, chances are you want to consume more than just that feed. When using your MyGet feed and the NuGet.org feed simultaneously, an interesting error mayoccur  during package restore.

	Unable to find version xxxx of package yyyy

The reason this happens is because the NuGet command line, the NuGet Visual Studio Extension and the NuGet PowerShell Console all have a configuration option specifying which package source to install from. When this setting is changed to one specific feed, other feeds will be ignored and the error above will be shown during package restore.

The solution is very simple: you can set the active package source to aggregate in Visual Studio, or simply configure NuGet to always use the aggregate package source for the current project. NuGet has an inheritance system for NuGet.config files, where the NuGet.config file closest to the solution file gets the last say. If you add the following NuGet.config file next to the solution file for your project, you should be fine:

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <activePackageSource>
	    <add key="All" value="(Aggregate source)" />
	  </activePackageSource>
	</configuration>

We can take this one step further and instead of configuring your MyGet feed globally for your system (and requiring other devs on your team to do the same), why not distribute a NuGet.config along with the sources?

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <packageRestore>
	    <add key="enabled" value="True" />
	    <add key="automatic" value="True" />
	  </packageRestore>
	  <packageSources>
	    <add key="nuget.org" value="https://www.nuget.org/api/v2/" />
	    <add key="MyGet" value="https://www.myget.org/F/chcuknorris/" />
	  </packageSources>
	  <disabledPackageSources />
	  <activePackageSource>
	    <add key="All" value="(Aggregate source)" />
	  </activePackageSource>
	</configuration>

This really makes working with multiple feeds a breeze. But we can go even further and use only MyGet, proxying packages from NuGet.org along the way. For more info on how that works, check the [documentation on upstream package sources](/docs/reference/package-sources#Scenario_-_Proxying_upstream_feeds_and_packages).