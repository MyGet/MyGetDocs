# Frequently Asked Questions

## What is required to be able to use MyGet?

MyGet is fully compatible with all NuGet clients. This means that we support all NuGet Visual Studio extensions as well as the NuGet commandline or NuGet Package Explorer.
That also means we are compatible with Chocolatey, Orchard, and OctopusDeploy!

## What version of NuGet is required to use MyGet?

MyGet really is a NuGet-as-a-Service platform, so we'll make sure our feeds will be upgraded to the latest NuGet format.
This effectively means you can safely upgrade your NuGet client to the latest version and benefit from new features whenever they're added into the NuGet.Server or NuGet.Core packages.

For backwards compatibility with older NuGet clients and Orchard, we also still expose the NuGet v1 API. Check your feed settings to see the available endpoints for your feed.

## How do I get my package in the feed?

See the [Creating and publishing a package](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package) page on the NuGet Documentation for details on how to create NuGet packages.

In order to publish them onto your MyGet feeds, you'll need to create a MyGet account first.

## When do packages appear on the feed after pushing them successfully?

We often get this question: 
"I pushed a package successfully but it is not yet available (even though activity log shows it). Why?"

Usually, your packages will appear on the feed within a minute. Depending on load and other factors, this could take up to 5 minutes. Very sporadically even longer (this can happen for instance when we are scaling out during burst load scenarios).

Note that MyGet.org is an eventually consistent system (and so is NuGet.org).

If you really need your package to be instantly available, it is very likely you should question whether the consuming project really needs a package dependency, versus a project reference. We found in practice that this was often the case.

## I get a 402 Payment Required when working with my private feed

The *402 Payment Required* status code means that the private feed is locked because the feed owner's subscription has expired.

If the owner of a private feed downgrades a paid subscription to a free one or if the subscription expires, any private feeds on the account will become read-only for a period of 7 days. After this period, the feed will become locked, meaning private feeds cannot be accessed until the subscription is renewed or the feed is made public.

<div class="alert alert-info">
    <strong>Note:</strong> Locked feeds will not be made public, nor will they be automatically deleted.
</div>

[Upgrading to a paid subscription](http://www.myget.org/plans) will automatically unlock the private feed. Another option is to make the feed public and restore access. Note that making the feed public will make it accessible to everyone.

## I get a 409 Conflict when pushing packages to my MyGet feed

The *409 Conflict* status code can be returned because of several reasons:

* The package size is too large for the current [subscription](http://www.myget.org/plans). E.g. the Free plan only supports packages <= 100 MB. Check the package size and your subscription plan quota. When the feed owner created a private feed and you are on a plan that has lesser quota, MyGet will apply your subscription quota. For example, when you are on the Free plan and are pushing packages to a private feed created by someone on a Starter plan, packages can still only be <= 100 MB.
* The feed is over quota for the current [subscription](http://www.myget.org/plans). Check the feed quota and your subscription plan quota.
* You enabled any of the following package settings for your feed. Verify the package settings for the feed you are pushing to.
  * **Forbid overwriting of existing packages?** - this will forbid overwriting packages that already exist on your feed (same package id and version)
  * **Forbid packages which are non-compliant with Semantic Version?** - this will forbid uploading packages that are not compliant with [Semantic Versioning](http://www.semver.org). E.g. a package version like 2.0.234.255 will not be supported.

MyGet will return a detailed error when pushing packages with a full description of the issue. If your NuGet client is not showing this error, use the *-verbosity Detailed* switch.

## I get a 413 RequestEntityTooLarge when pushing packages to my MyGet feed

First of all, when pushing to a v2 NuGet feed, ensure you are targeting the `api/v2/package` endpoint.

There are various reasons that can result in this exception being thrown. Usually, the response is enriched with more details about the cause of the exception.

Here's an overview:

* The package is too large according to your subscription quota. Please verify before asking support as you can often simply resolve it by upgrading your subscription plan or adjusting retention policies on your feed to free up some space.
* The total size of the packages that are pending upload (through the web UI) is too large.
* The package is simply too large (as in Gb's of size) and our servers reject it.

## Does MyGet support Mono?

As long as NuGet.Core and the NuGet CLI support Mono, we'll do the same.

The command-line application (*nuget.exe*) builds and runs under Mono and allows you to create packages in Mono.
This is especially true for Mono on Windows, but there are some known issues for Mono on 

## Where is my data stored?

MyGet runs its operations from the [Microsoft Azure](http://www.azure.com) in the West Europe region, near Amsterdam, the Netherlands. All free and paid plans are hosted in this location, with backups in the North Europe region, near Dublin, Ireland.

Enterprise plan customers have a choice in where data is stored between the US East region, or our primary West Europe region.

Check our [terms and conditions](https://www.myget.org/policies/terms) to learn more about Security, privacy, and Customer Data.
