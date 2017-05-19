# MyGet 2016.2 Release Notes

MyGet 2016.2 was released on August 19, 2016.

## Highlights

This 2016.2 release of MyGet again adds some new functionality to the service. 
Major highlights of this release are: 

* our **[improved build log viewer](http://blog.myget.org/post/2016/08/04/Improved-build-log-viewer-with-error-navigation.aspx)** with warning and error navigation, log level coloring and deep linking support,
* a new profile section providing access to your invoices and billing settings,
* opt-in **[support for expiring access tokens](http://blog.myget.org/post/2016/06/14/Setting-an-expiration-time-for-your-MyGet-access-tokens.aspx)** (API keys).

<a href="https://www.myget.org">
	<img src="Images/host your packages on myget.gif" alt="MyGet 2016.2 Highlights" />
</a>

## Features

### MyGet (all plans)

The following applies to **all** MyGet plans:

* Security: **opt-in** [support for expiring access tokens](http://blog.myget.org/post/2016/06/14/Setting-an-expiration-time-for-your-MyGet-access-tokens.aspx) (API keys)
* Package retention: [added support for new constraint on package downloads; ability to never delete packages that have at least 1 download](http://blog.myget.org/post/2016/08/22/Keeping-feeds-clean-with-retention-rules.aspx)
* Symbols: added a new, symbols-specific push endpoint to your MyGet repositories
* Symbols: added support for **native .dll** symbols and .NET's new symbols package format
* Bower: now also supporting .tar.gz packages on Bower endpoints
* VSIX: added support for VSTS Marketplace VSIX packages
* Gallery: packages listed on gallery feed pages now have links to a package details page with installation instructions
* Support markdown in package description for NuGet and Chocolatey packages

## MyGet (paid plans)

Obviously all paid plans also get the enhancements made available on the free plan.
The following applies to the MyGet Starter and Professional plans:

* Billing: new profile section providing access to your invoices
* Billing: ability to configure a different email address for billing

### MyGet Enterprise

The enterprise plan has all functionality from the paid subscription plans, and more!
The following applies only to the MyGet Enterprise plan:

* User management: we added support to block user registration so that an invite-only environment can be created
* User management: we introduced a new **Feed Creator** role, allowing MyGet Enterprise administrators to delegate feed creation permissions to a non-administrator account

### MyGet Build Services
* **[Improved build log viewer](http://blog.myget.org/post/2016/08/04/Improved-build-log-viewer-with-error-navigation.aspx)** with warning and error navigation, log level coloring and deep linking support
* The build log now also recognizes Kiln source control: commit SHA now also has a hyperlink to Kiln source repository
* Made performance optimizations to the Build Sources page

<a href="http://blog.myget.org/post/2016/08/04/Improved-build-log-viewer-with-error-navigation.aspx">
	<img src="Images/MyGet-2.2-logviewer.png" alt="MyGet 2016.2 Highlights" />
</a>


## Bug Fixes & Other Improvements
* NuGet: Preserve Chocolatey-specific additions to the NuGet package manifest (.nuspec) when pushing packages upstream
* NuGet: Fixed an issue with NuGet packages that caused *Summary* metadata not to be populated properly when uploaded through the web site
* NuGet: Fixed an issue causing failures when proxying Sonatype Nexus feeds on calls to GetUpdates() and Search()
* Bower: now also detecting bower.json in subdirectories of uploaded bower packages
* NPM: fixed an issue causing 404 errors when proxying upstream *scoped* packages
* Usability: improved ordering of SemVer and non-SemVer package versions in the same feed
* Usability: no longer allow 0 as value for package retention rules
* Usability: when filtering package views, the *Delete All* button should not be visible to reduce potential confusion and avoid accidental deletes
* Fixed an issue causing pushing to upstream package source to fail downloading the package from source feed on MyGet Enterprise with custom domain
* Support Github-style code blocks in markdown

We love hearing from you, so keep that [feedback](http://myget.uservoice.com/) coming! MyGet is built for you!

_Happy packaging!_