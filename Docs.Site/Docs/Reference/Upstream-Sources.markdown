# Working with upstream sources

Upstream sources play a key role in a professional approach towards Package Management. MyGet gives you the option to specify one or more upstream sources for a feed.

**Q: Why use upstream sources?**

* Upstream sources make it easy to pull in packages from other package sources onto your downstream MyGet feeds.
* You can also target these upstream sources to push packages upstream from your MyGet feeds.
* Any configured upstream source on a MyGet feed will be made available to you in MyGet Build Services without having to commit any credentials or secrets in your source repository.

Before diving into some practical scenarios, let's make sure we are clear in terms of terminology. The following Q&A should help you with that in a rather poetic way.

**Q: I'm confused about what "upstream" means in the context of package sources. What is upstream?**

Poetic answer:

> _Consider the direction in which packages are flowing from a given package source to an ocean of consumers._
>
> _Your package can have dependencies "upstream", to packages on another feed._
> _From the point of view of those dependencies, the depending package is located "downstream"._
> _When a user consumes the downstream package, it will also fetch the upstream dependencies._
>
> _The consumer, however, is only allowed to fetch or query those upstream packages if the feed he's talking to (downstream) is also configured to proxy the upstream package source._

## Adding an upstream source to your MyGet feed

By default, MyGet feeds have the public, central repositories configured for each package type. This includes:

* NuGet: https://www.nuget.org/api/v2
* Bower: https://bower.herokuapp.com
* npm: http://registry.npmjs.org
* Maven: https://repo1.maven.org/maven2

To configure an additional upstream source for your MyGet feed, navigate to _Feed Settings > Upstream Sources_. Then click _Add Upstream Source_ and select the package source type you want to add.

![](Images/add-package-source-button.png)

A dialog will prompt your for upstream source information and will also expose a few common presets for you to take advantage of.

### Upstream Source Credentials

If you have any access privileges to other MyGet feeds, you will see those in the MyGet Feeds presets, so you can easily build a chain of upstream sources to facilitate a *package promotion flow*.

![](Images/add-package-source-MyGet-preset.png)

If you select a private MyGet feed you have access to as an upstream source, there's no need to provide credentials to be able to restore packages from it on MyGet Build Services. MyGet will impersonate your user account when authenticating against that upstream feed.

For any non-MyGet upstream source that requires authentication to pull packages, you'll have to provide username and password to be used during Basic Auth.

<p class="alert alert-warning">
<strong>Warning!</strong> <span style="text-decoration:underline">Be very careful with password managers and browser add-ons providing auto-completion of credentials!</span><br/>
We recommend disabling these credential managers on the MyGet web site to avoid issues when editing upstream sources. Oftentimes, these tools auto-complete the credentials fields with out-dated credentials (even when editing different settings in the dialog). <br/>
When running into package restore failures on MyGet Build Services, or when noticing that upstream packages are no longer available downstream, this is the most common source of the issue.
</p>

In the opposite direction, in order to push packages from your downstream MyGet feed to the upstream source, you may need to configure a (scoped) API key or access token.

### Upstream Source Filtering

<p class="alert alert-info">
Applies to: <strong>NuGet (v2 only!)</strong>
</p>

When the upstream source is a v2 NuGet feed, you may configure additional OData filtering.
Filtering is based on the [OData v3 Filtering System](http://www.odata.org/documentation/odata-version-3-0/odata-version-3-0-core-protocol#thefiltersystemqueryoption). 
Valid filters are similar to `Id eq 'jQuery'` or `IsLatestVersion eq true and Id ne 'Foo'`.

<p class="alert alert-warning">
<strong>Warning!</strong> This capability may go away at some point in favor of newer NuGet v3 APIs.<br/>
We currently still keep the feature around for some scenarios that are not yet fully supported on NuGet v3.
</p>

## Adding a package from an upstream source

You can easily add packages to your MyGet feed originating from an upstream source, such as nuget.org, nmpjs.org, etc. This is using the feed's configured upstream sources under the hood. If you want to add a package from another feed onto your MyGet feed, the other feed needs to be configured as an upstream source to that feed.

Adding a package from an upstream source can happen in three ways: manually, by reference (proxying), or by value (mirroring).

* **Manually**: you can add packages from an upstream source to your feed manually by using the _Add Package_ button you will find under your feed's _Packages_ page.

![](Images/add-package-button.png)

Select _From Feed_ in the dialog that prompts.

![](Images/add-package-from-feed.png)

* **Proxying**: the package metadata is copied to the MyGet feed, the package itself remains hosted on the upstream source. When querying the package, we call the upstream source to fetch the package.
* **Mirroring**: the package metadata and the package itself are copied onto the MyGet feed. When querying the package, we server the package directly and don't use the upstream packages source. Mirroring of a package version happens upon the first request for that given package version.

Configuring upstream sources on your MyGet feed unlocks quite a few integration scenarios and automation opportunities!

![](Images/package-source-compatibility.png)

### Proxy packages from an upstream source

You can configure an upstream source to proxy upstream packages through your MyGet feed to your feed consumers. Proxying makes it easy to have a single MyGet feed aggregate packages from multiple sources. Package consumers need only to configure a single MyGet feed, and all packages available on upstream, proxied package sources will become available to them.

Benefits:

* upstream packages do not count against your MyGet storage quota
* authentication against upstream, private MyGet feeds happens automatically (see [Upstream Source Credentials](/docs/reference/upstream-sources#Upstream_Source_Credentials))

Drawbacks:

* every package request will incur additional latency as opposed to storing the package onto the MyGet feed

<p class="alert alert-warning">
<strong>Warning!</strong> Avoid configuring multiple upstream source proxies on a single feed, or in a chain of feeds, as this will magnify the disadvantages, and result in very slow feed response times.
</p>

The following diagram illustrates the effects of upstream source proxying.

![](Images/package-source-proxying.png)

To enable upstream source proxying, you must tick the checkmark next to _Make all upstream packages available in clients_.

![](Images/setting-package-source-proxying.png)

## Mirror packages from an upstream source

You can configure an upstream source to mirror upstream packages onto your MyGet feed. This configuration is similar to package proxying, but takes it one step further.

Whenever someone requests a particular package from your MyGet feed for the first time, MyGet will query the upstream source and copy the package onto the MyGet feed.

Benefits:

* No additional latency (except for the first hit that triggers the package mirroring)
* Protected against upstream source availability issues
* Protected against upstream package removal
* authentication against upstream, private MyGet feeds happens automatically (see [Upstream Source Credentials](/docs/reference/upstream-sources#Upstream_Source_Credentials))

Drawbacks:

* mirrored packages count towards your MyGet storage quota (you can always upgrade your subscription or <a href="mailto:support@myget.org">request a quote</a>!)

The following diagram illustrates the effects of upstream source mirroring.

![](Images/package-source-mirroring.png)

To enable upstream source mirroring, you must tick the checkmark next to _Automatically add downloaded upstream packages to the current feed (mirror)_.

![](Images/setting-package-source-mirroring.png)

Optionally, you can also tick the third checkmark to indicate that any package found upstream is to be considered a _package dependency_ (and should not be consumed directly). This will hide those packages from search results, whilst still allowing you to restore them.

Once upstream source mirroring is enabled, we can consume our MyGet feed from Visual Studio which will also list upstream packages. For example, the example _acmecompany_ feed only lists one package:

![One package on our feed](Images/acmefeed-one-package.png)

When searching in Visual Studio, we do see packages that originate from upstream sources:

![Visual Studio showing upstream packages](Images/acmefeed-upstream-search.png)

After installing this package, our feed now automatically contains a copy of the jQuery package:

![Mirror upstream pckages](Images/acmefeed-two-packages.png)

From now on, the package is available from our MyGet feed directly, without having to explicitly add it manually from the upstream source.

## Using a MyGet feed as a staging area (before pushing upstream)

Many development teams are using some kind of _package promotion workflow_: pushing a package from one feed to another based on quality gates, target audience, or any other criteria. This is very typical scenario for which upstream sources are essential.

Of course, all of this can happen in an automated fashion using package manager client. However, as promoting a package typically involves some kind of human intervention (e.g. release manager approval), we've also made it a first-class feature in the MyGet web site.

Simply pick the package version you want to promote from the package details page, and hit the _Push_ button to initiate the package promotion flow.

![](Images/push-upstream-button.png)

A dialog will provide you with additional options. MyGet is also smart enough to detect any package dependencies you might want to push along in one go as part of this package promotion flow.

![](Images/push-package-upstream-dialog.png)

At this point, you can still make a few metadata changes before pushing upstream.
This dialog allows you to:

* modify or remove the prerelease label of the upstream package version. This allows you to e.g. drop the prerelease label to _release_ a package without rebuilding/repackaging.
* add release notes to be included in the package metadata. MyGet will even support release notes written in markdown and render them properly on the web site!
* modify or remove the SemVer2 build metadata part of the upstream package version
* exclude any detected dependencies or satellite packages from the push action
* apply source labeling if the package was built using MyGet Build Services. When enabled, MyGet will find the build from which the package originated and will add a label to the source control revision it was built from.

To edit a package's metadata, simply click the _Edit_ button next to it and make the modifications. To apply a given modification to all packages in the dialog, hit the _rain drop_ button next to the editable field.

![](Images/push-upstream-edit-package.png)

## Using upstream sources on MyGet Build Services

<p class="alert alert-info">
Applies to: <strong>NuGet, npm</strong>
</p>

Upstream sources for a feed are also available during build. This can be useful in the following scenarios:

* An additional upstream source is needed during build. MyGet will make the upstream source available during build if it has been added to the feed's upstream sources.
* If you have a private feed requiring authentication but do not wish to add credentials to source control, credentials can be added to the feed's upstream source. These credentials will be available during build and allow you to consume a protected feed with ease.

<p class="alert alert-info">
Applies to: <strong>NuGet</strong>
</p>

* The configured API key for an upstream source is also made available during the build process. This means during a build, you can call into [`nuget.exe push`](https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference#push) and push packages to configured upstream sources.
* If you want to make use of [`nuget.exe push`](https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference#push) in a build script without having to specify the `-Source` parameter. This requires a default upstream source to be defined.

<p class="alert alert-info">
Applies to: <strong>npm</strong><br/>
We strongly suggest to <i>proxy</i> registry.npmjs.org to be able to run `npm install` during build, as npm will default to the MyGet feed as the default registry.
</p>

### Setting default upstream sources to be used on MyGet Build Services

<p class="alert alert-info">
Applies to: <strong>NuGet</strong>
</p>

The `NuGet.config` file on our build agents is configured using NuGet's defaults, enriched with all NuGet upstream sources configured for a feed. Based on these defaults, the following conventions are active:

* The default upstream source is set to `(Aggregate Source)`, meaning all feeds will be queried for packages in the order defined in the feed's upstream sources.
* The default push source (when using `nuget push` without the `-Source` parameter) is NuGet.org.

Both of these conventions can be overridden by editing the build source configuration.

## Auto updating packages

MyGet feeds can automatically fetch package updates made available through the upstream sources.

When adding or editing an upstream source, we can enable this behaviour per upstream source, as well as an interval when MyGet should check for updates.

![Upstream Source Options](Images/add-package-source-options.png)

The following options are available:

* **E-mail me when package updates are available**: Sends an e-mail to the specified recipient(s) when package updates are available on the upstream source.
* **Include prerelease versions**: By default, MyGet will only consider stable packages. When enabled, we will also check prerelease packages from the upstream source.
* **Automatically update packages to their latest versions**: Enables the behaviour of automatically updating packages from the upstream source.
* **Update interval**: Depending on your subscription plan, we can specify how often MyGet should check for updates (up to every 30 minutes on a [Professional subscription](https://www.myget.org/plans))
