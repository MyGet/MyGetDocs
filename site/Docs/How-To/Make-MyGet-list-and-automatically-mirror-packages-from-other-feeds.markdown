# Make MyGet list and automatically mirror packages from other feeds
Many teams like to make MyGet the only feed that they use. Developers can upload their own packages to that hosted feed and consume them from Visual Studio or a build server. MyGet can also proxy search results and packages from an upstream package source, making management of feeds in a team much easier.

![Proxy upstream feed](Images/proxy-schema.png)

Package sources play a key role in this scenario. By default, the NuGet Gallery is configured as an upstream package source from which packages can be added explicitly. However using the package proxy feature, search results and packages from upstream sources can be automatically mirrored to your feed.

As you can imagine, using this feature yields some benefits:

* Normally, upstream packages that are deleted are no longer available on your MyGet feed unless explicitly mirrored. Using this feature will automatically add upstream packages you use to your feed.
* When an outage occurs on the upstream package source, the proxy is of no use because it can’t connect to the upstream package source. If the package is automatically mirrored, you can keep working regardless of upstream package source availability.

## Adding a package source
First of all, we'll have to configure a package source for our MyGet feed. Navigate to the feed settings and browse the *Package Sources* tab. Then click *Add Package Source*.

A dialog will prompt us for package source information and will also expose a few common presets we can take advantage of.

![Add Package Source Dialog](Images/add_package_source.png)

We can use one of the presets that are available, as well as enter information for the feed we want to proxy, perhaps a TeamCity feed or similar. We can also specify an optional filter (to only include packages matching given criteria) and authentication information in case we're proxying a private feed.

## Displaying search results from NuGet.org or other package sources on your feed
At the bottom of the _Add Package Source_ dialog, we can specify two additional options:

![Proxy settings](Images/proxy-settings.png)

These options will do the following:

* **Include packages from the package source in search results**: the upstream package source will be searched and packages that match the search will be displayed when consuming the feed from Visual Studio. Packages from the upstream source can be installed into a project but will be downloaded from the upstream package source.

* **Automatically add downloaded upstream packages to the current feed**: when the above option is enabled, packages that are downloaded will automatically be mirrored on your feed, ensuring the package is available all the time (even when the upstream package source experiences an outage).

Enabling both options will ensure that the MyGet feed is always up-to-date with the packages you are consuming in your organization and that upstream packages are displayed and mirrored on your feed.

## Putting it to the test: consuming a package from Visual Studio
Once both aforementioned options are enabled, we can consume our own feed from Visual Studio which will also list upstream packages. For example, the _acmecompany_ feed I've created only lists one package:

![One package on our feed](Images/acmefeed-one-package.png)

When searching in Visual Studio, we do see packages that originate from upstream package sources:

![Visual Studio showing upstream packages](Images/acmefeed-upstream-search.png)

After installing this package, our feed now automatically contains a copy of the jQuery package:

![Mirror upstream pckages](Images/acmefeed-two-packages.png)

From now on, the package is available from our MyGet feed directly, without having to explicitly add it manually from the upstream package source.