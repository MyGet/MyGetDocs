# MyGet Package Sources Explained

Package sources play a key role in the MyGet Build Services workflow. By default, the NuGet Gallery is configured as an upstream package source. You could obviously also configure Chocolatey, an Orchard feed, or your own MyGet feed, or all of them! 

These upstream package sources allow you to reference or mirror packages onto your own feed. It also allows you to easily push packages from your feed to the the upstream package source, given you configured your API key in the package source configuration.

Another scenario for package sources is making MyGet your only NuGet feed. By forwarding package searches and proxying upstream packages, your MyGet feed is a one-stop feed for all packages you are using in your projects.

## Adding a package source
To configure a package source for your MyGet feed, navigate to the feed settings and browse the *Package Sources* tab. Then click *Add Package Source*.

A dialog will prompt your for package source information and will also expose a few common presets for you to take advantage of.

![Add Package Source Dialog](Images/add_package_source.png)

## Scenario - Add package from feed
This is the first scenario that makes use of upstream package sources. If you want to add a package from another feed onto your MyGet feed, the other feed needs to be configured as a package source to that feed.

Adding a package can happen in two ways: referencing or mirroring.

* **Referencing**: the package metadata is copied to the MyGet feed, the package itself remains hosted on the package source. When querying the package, we call the upstream package source to fetch the package.

* **Mirroring**: the package metadata and the package itself are copied onto the MyGet feed. When querying the package, we serve the package directly and don't use the upstream package source.

![Package Source Aggregation](Images/Aggregate_Package_Sources.png)

## Scenario - Pushing a package upstream
Another major scenario made possible by using package sources is the *package promotion* workflow: pushing a package from one feed to another.

Choose the package you want to promote and with a click of a button you can push it upstream. A dialog will provide you with additional options, e.g. configure the package version to be used upstream.

![Push Package Upstream](Images/push_package_upstream.png)

It is also possible to label sources when pushing a package upstream. When enabled, MyGet will find the build from which the package originated and will add a label to the source control revision it was built from.

## Scenario - Proxying upstream feeds and packages
One use case for MyGet is to create your own NuGet feed on MyGet and upload your own packages to that hosted feed. MyGet can also be used as the central NuGet feed in your development team, including your own packages and proxying search results and packages from an upstream package source.

![Proxy upstream feed](Images/proxy-schema.png)

When creating a package source, these options are available:

![Proxy settings](Images/proxy-settings.png)

These options will do the following:

* **Make all upstream packages available in NuGet clients**: the upstream package source will be searched and packages that match the search will be displayed when consuming the feed from Visual Studio. Packages from the upstream source can be installed into a project but will be downloaded from the upstream package source.
* **Automatically add downloaded upstream packages to the current feed (mirror)**: when the above option is enabled, packages that are downloaded will automatically be mirrored on your feed, ensuring the package is available all the time (even when the upstream package source experiences an outage).
* **Upstream packages are dependencies and should be unlisted**: upstream packages will be unlisted (not searchable but available when using package restore)
 
Enabling both options will ensure that the MyGet feed is always up-to-date with the packages you are consuming in your organization.

### Feed proxying and authentication

When the feed that is being proxied is a MyGet feed, we will impersonate the user against the upstream feed. When other credentials are required or the package source is not hosted on MyGet, credentials should be entered when defining the package source.

## Scenario - Automatically updating packages from upstream
Sometimes it would be nice if we could automatically update packages on our feed that originate from upstream package sources. When adding or editing a package source, we can enable this behaviour per package source, as well as an interval when MyGet should check for updates.

* **E-mail me when package updates are available**: Sends an e-mail to the specified recipient(s) when package updates are available on the upstream package source.
* **Include prerelease versions**: By default, MyGet will only consider stable packages. When enabled, we will also check prerelease packages from the upstream package source.
* **Automatically update packages to their latest versions**: Enables the behaviour of automatically updating packages from the package source.
* **Update interval**: Depending on your subscription plan, we can specify how often MyGet should check for updates (up to every 5 minutes on a [Professional subscription](http://www.myget.org/plans))

![Package auto update](Images/auto-update.png)