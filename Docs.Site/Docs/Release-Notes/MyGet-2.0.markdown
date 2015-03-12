# MyGet 2.0 Release Notes

MyGet 2.0 was released on March 12, 2015.

## Highlights

This 2.0 release of MyGet adds brand new functionality to the service. With this release, we bring all the functionality we already had for NuGet also to Bower and NPM!

This means, from now on, you can use MyGet to host and build your own NuGet, NPM or Bower feeds, whether public or secured.

<a href="https://www.myget.org">
	<img src="Images\MyGet-2.0-highlights2.png" alt="MyGet 2.0 Highlights" />
</a>

## Features

### MyGet (all plans)

The following applies to **all** MyGet plans:

* **[NPM support](../reference/myget-npm-support)!**
* **[Bower support](../reference/myget-bower-support)!**
* Integrated FastSpring as payment provider with support for subscription auto-renewal
* NuGet: (IE-only) new link on package details page to open NuGet packages directly in NuGet Package Explorer
* NuGet: support for v2 Target Framework filtering
* NuGet: [modify NuGet package description and release notes before pushing upstream](http://blog.myget.org/post/2015/01/13/modify-nuget-package-description-and-release-notes-before-pushing-upstream.aspx)

## MyGet (paid plans)

Obviously all paid plans also get the enhancements made available on the free plan.
The following applies to the MyGet Starter and Professional plans:

* NuGet: [New MyGet Feed Sync](../reference/feed-sync) service

### MyGet Enterprise

The enterprise plan has all functionality from the paid subscription plans, and more!
The following applies only to the MyGet Enterprise plan:

* Allow [customization of the message displayed on the login page](../reference/myget-enterprise#Login_page)
* Support [domain whitelisting/blacklisting using wildcards for registration email address](../reference/myget-enterprise#Registration)
* Support [IP address range whitelisting/blacklisting](../reference/myget-enterprise#IP_security)

### MyGet Build Services
* Support for [pre- and post-build step scripts](../reference/build-services#Pre-_and_post-build_steps)
* Support for [service messages](../reference/build-services#Service_Messages)
* Support for [user-defined environment variables](../reference/build-services#User-defined_environment_variables)
* Support for [user-defined environment variable expansions](../reference/build-services#User-defined_environment_variable_expansions)

## Bug Fixes
* Improved detection of hanging builds
* Build now properly fails when compilation fails and no packages have been produced or pushed to your feed
* Upstream package source proxying enhancements
* No longer prompt the user that a package update is available upstream if the package source is set to auto-update

Keep that [feedback](http://myget.uservoice.com/) coming! MyGet is built for you!

_Happy packaging!_

<a href="https://www.myget.org">
	<img src="Images\MyGet-2.0-highlights.png" alt="MyGet 2.0 Highlights" />
</a>