# MyGet 2016.1 Release Notes

MyGet 2016.1 was released on February 12, 2016.

## Highlights

This 2016.1 release of MyGet again adds some new functionality to the service. 
Major highlights of this release are: 

* our new **[MyGet Symbols Services](../reference/symbols)** offering,
* our new support for **[building and hosting Visual Studio Extensions](../walkthrough/getting-started-with-vsix)**,
* and the implementation of NuGet v3 support.

This means, from now on, you can now also use MyGet to host your symbols, publicly or secured.

<a href="https://www.myget.org">
	<img src="Images/host your packages on myget.gif" alt="MyGet 2016.1 Highlights" />
</a>

## Features

### MyGet (all plans)

The following applies to **all** MyGet plans:

* **[MyGet Symbols Services](../reference/symbols)!**
* **[Support for building and hosting VisualStudio Extensions (VSIX Support)](../walkthrough/getting-started-with-vsix)**
* NuGet: support for v3 feeds
* NuGet: [modify NuGet package description and release notes before pushing upstream](http://blog.myget.org/post/2015/01/13/modify-nuget-package-description-and-release-notes-before-pushing-upstream.aspx)
* NuGet: [improved support for localized satellite packages](http://blog.myget.org/post/2016/01/14/improved-support-for-localized-satellite-nuget-packages.aspx)
* Bower: support for Bower binary packages
* npm: support for NPM scopes
* MyGet Gallery: we now show the total download count for each feed in the gallery (as well as on your MyGet feed details page)
* Package Sources: **[support for Dropbox](http://blog.myget.org/post/2016/02/11/Dropbox-as-a-package-source-for-NuGet-npm-Bower-and-VSIX-packages.aspx)**!
* Package Sources: [quick-access to your other MyGet feeds using feed presets](http://blog.myget.org/post/2015/12/12/adding-another-myget-feed-as-a-package-source-feed-presets.aspx)
* Package Sources: support for BitBucket API v2.0 and switch to OAuth 2.0
* Support multiple simultaneous package uploads through the web site
* Feed details page now also [displays the readme from its GitHub repository](http://blog.myget.org/post/2015/10/21/Package-details-showing-GitHub-project-README.aspx) (provided the repository is public)
* We will now notify feed owners when they are about to reach their subscription quota, so you can anticipate and act before hitting your limits

## MyGet (paid plans)

Obviously all paid plans also get the enhancements made available on the free plan.
The following applies to the MyGet Starter and Professional plans:

* Security: Feed privilege claim tokens now expire after 15 days when unused
* Billing: we now allow for yearly billing to reduce the administrative overhead of billing cycles

### MyGet Enterprise

The enterprise plan has all functionality from the paid subscription plans, and more!
The following applies only to the MyGet Enterprise plan:

* Support for restricting feed creation to administrators only

### MyGet Build Services
* Support for [explicitly publishing artifacts using service messages](../reference/build-services#Explicitly_publishing_a_package)
* Support for BitBucket API v2.0 and switch to OAuth 2.0
* Support for xUnit v2.0
* Support GitLab POST hooks
* Performance improvements in build hook payload parsers
* Web Hooks: new Build Queued web hook
* New option: toggle access to build log for feed consumers

## Bug Fixes
* Various performance improvements in the web site
* Bower: license analysis now also checks Bower package licenses
* Usernames on feed security page now link to the user profile page
* npm: proxy dist-tags from upstream when available
* Fixed a bug where in some cases uploading a ZIP file Bower package threw an exception
* Fixed a bug where in some cases pushing an npm package upstream to npmjs.com failed
* Fixed a bug where editing a package source was not possible when the package source is unavailable

We love hearing from you, so keep that [feedback](http://myget.uservoice.com/) coming! MyGet is built for you!

_Happy packaging!_