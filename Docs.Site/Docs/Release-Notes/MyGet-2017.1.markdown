# MyGet 2017.1 Release Notes

MyGet 2017.1 was released on June 1, 2017.

## Highlights

This 2017.1 release of MyGet again adds some new functionality to the service. 
Major highlights of this release are: 

We've built a **[MyGet Credential Provider for Visual Studio 2017](/docs/reference/credential-provider-for-visual-studio)**! This extension allows you to authenticate against your MyGet feeds using OAuth. [Install it from the Visual Studio Gallery](https://marketplace.visualstudio.com/vsgallery/79609fc1-58d5-4a31-a171-124b952ca9e0)!
<div style="text-align:center; display:block; margin-bottom:10px;" >
<a href="https://marketplace.visualstudio.com/vsgallery/79609fc1-58d5-4a31-a171-124b952ca9e0">
	<img src="Images/install-VSIX.png" alt="Install the MyGet Credential Provider for Visual Studio 2017!"/>
</a>
</div>

We added **Maven** support, and welcome Java/Android developers to the MyGet family! ([Announcement](https://blog.myget.org/post/2017/02/09/maven-packages-just-arrived-on-myget-early-access-preview.aspx) | [Docs](docs/walkthrough/getting-started-with-maven))

<div style="text-align:center; display:block; margin-bottom:10px;" >
<a href="docs/walkthrough/getting-started-with-maven">
	<img src="Images/myget-maven.png" alt="Getting started with Maven on MyGet!" width="300px"/>
</a>
</div>

We've built a web utility to help you learn and adopt Semantic Versioning: check out our **[MyGet SemVer Explorer](https://semver.myget.org/)**!

<div style="text-align:center; display:block; margin-bottom:10px;" >
<a href="https://semver.myget.org">
	<img src="Images/myget-semver-explorer.png" alt="MyGet SemVer Explorer" width="500px"/>
</a>
</div>

We've partnered with OSSIndex.net to check for potential package vulnerabilities on your MyGet feeds! ([Announcement](https://blog.myget.org/post/2016/10/14/Checking-potential-vulnerabilities-in-project-dependencies.aspx) | [Docs](/docs/reference/vulnerability-report))

<div style="text-align:center; display:block; margin-bottom:10px;" >
<a href="docs/reference/vulnerability-report">
	<img src="Images/ossindex-vulnerabilities.png" alt="Check for potential package vulnerabilities on your MyGet feeds!" width="500px"/>
</a>
</div>

## Features

### MyGet (all plans)

The following applies to **all** MyGet plans:

* NPM: added support for token authentication
* NPM: added support for upstream token authentication, which now also supports Telerik's NPM registry as an upstream package source
* NPM: added support for the *fast search* endpoint
* NPM: added support for package deprecation
* NPM: added support for package tagging
* NPM: added support for dist-tag
* NuGet: added support for NuGet's SemVer2 protocol, and added support for modifying build metadata on push upstream dialog
* Maven: introduced support for Maven artifacts
* Maven: introduced support for Android AAR artifacts
* Symbols: added a toggle to support pushing symbols upstream as well
* Symbols: when the upstream target feed is a MyGet feed, we automatically also push the symbols upstream
* Usability: no longer show symbols packages separately on the Gallery's feed details view
* Usability: minor modifications to the Gallery feed details UI to improve the user experience
* Usability: added a *download all* button to the packages dropdown in build results view
* Usability: hide pre-authenticated feed endpoints from Feed Details view when the feed is not a private feed
* Usability: added a copy-to-clipboard button to the connection details popup on Gallery feeds
* Security: we've built a **[MyGet Credential Provider for Visual Studio 2017](/docs/reference/credential-provider-for-visual-studio)**! This extension allows you to authenticate against your MyGet feeds using OAuth.
* Security: we've consolidated the login page: one page to rule them all!
* Security: we no longer display access tokens (you can still copy them though)
* Security: improved auditing
* Security: added support for feed and privilege scopes to access tokens / api keys (in addition to expiration support which we already had)
* Integrations: SymbolSource.org integration has been retired in favor of MyGet's own Symbols functionality
* Integrations: added OSSIndex.net integration to detect package vulnerabilities and report them on your feed details view

### MyGet Enterprise

The enterprise plan has all functionality from the paid subscription plans, and more!
The following applies only to the MyGet Enterprise plan:

* Usability: the Gallery index is now the default landing page when authenticated on MyGet Enterprise
* Security: added support for marking users as *external to the tenant*. This prevents the external user from accessing *Enterprise* feeds, unless privileges are explicitly assigned at the feed level. (see [Feed Types](https://docs.myget.org/docs/reference/security#Available_Feed_Types))

### MyGet Build Services
* Added support for **Visual Studio 2017**, **.NET Core** and the new **PackageReference** project format ([Announcement](https://blog.myget.org/post/2017/03/15/visual-studio-2017-and-net-core-support-on-myget.aspx))
* AssemblyInfo patching now supports globbing patterns (like `**\**.cs`)

## Bug Fixes & Other Improvements

* MyGet has been upgraded to run on .NET Framework 4.6.2, which seemed to have positive effect on performance
* Overwriting source symbols is now blocked when forbid overwrite is enabled on the feed
* Fixed a bug in semantic version range parsing of npm dependencies (tilde and carret ranges)
* Show quota per feed on user profile page (helps answer the question: 'which of my feeds consumes most?')
* Fixed an issue caused by breaking changes in VSTS API (repository `remoteUrl` returned by VSTS API no longer contained VSTS collection name)
* Fixed an issue in the Gallery index view related to feed icons
* Fixed an issue that caused an HTTP 500 when a nuspec contained some invalid data
* Fixed an issue that caused NPM push upstream to fail when no package description was given
* Fixed an issue with the symbols code browser when a file was not found or could not be displayed
* NuGet: allow packages.config files to be uploaded without version number specified

We love hearing from you, so keep that [feedback](https://myget.uservoice.com/) coming! MyGet is built for you!

_Happy packaging!_

<a href="https://www.myget.org">
	<img src="Images/host your packages on myget.gif" alt="Host your packages on MyGet!" />
</a>