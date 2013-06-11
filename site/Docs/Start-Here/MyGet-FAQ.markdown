# MyGet Frequently Asked Questions

## What is required to be able to use MyGet?

MyGet is fully compatible with all NuGet clients. This means that we support all NuGet Visual Studio extensions as well as the NuGet commandline or NuGet Package Explorer.
That also means we are compatible with Chocolatey, Orchard, SymbolSource and OctopusDeploy!

## What version of NuGet is required to use MyGet?

MyGet really is a NuGet-as-a-Service platform, so we'll make sure our feeds will be upgraded to the latest NuGet format.
This effectively means you can safely upgrade your NuGet client to the latest version and benefit from new features whenever they're added into the NuGet.Server or NuGet.Core packages.

For backwards compatibility with older NuGet clients and Orchard, we also still expose the NuGet v1 API. Check your feed settings to see the available endpoints for your feed.

## How do I get my package in the feed?

See the [Creating and publishing a package](http://docs.nuget.org/creating-packages/creating-and-publishing-a-package) page on the NuGet Documentation for details on how to create NuGet packages.

In order to publish them onto your MyGet feeds, you'll need to create a MyGet account first.
//todo

## Does MyGet support Mono?

As long as NuGet.Core and the NuGet CLI support Mono, we'll do the same.

The command-line application (*nuget.exe*) builds and runs under Mono and allows you to create packages in Mono.
This is especially true for Mono on Windows, but there are some known issues for Mono on Linux and OS X.  To review
the known issues, [search for Mono in the NuGet issue list](http://nuget.codeplex.com/workitem/list/basic?field=Votes&direction=Descending&issuesToDisplay=Open&keywords=mono&emailSubscribedItemsOnly=false).

## Is there a command-line tool for MyGet?

**Yes there is!** It's called nuget.exe :)

Simply use the official NuGet CLI to target MyGet from the command-line.
You can read more on [the NuGet Command-Line Reference](http://docs.nuget.org/docs/reference/command-line-reference).

## Can I use MyGet outside of Visual Studio?

**You sure can!** 

There are multiple NuGet clients that work completely outside of Visual Studio:

* [SharpDevelop Alpha](http://community.sharpdevelop.net/blogs/mattward/archive/2011/01/23/NuGetSupportInSharpDevelop.aspx). (See a demo of this in [Phil Haack's MvcConf talk](http://bit.ly/fzrJDa).) 
* ASP.NET Web Pages in WebMatrix. (See a demo of this in [Phil Haack's MvcConf talk](http://bit.ly/fzrJDa).) 
* [NuGet.exe](http://blog.davidebbo.com/2011/01/installing-nuget-packages-directly-from.html) 

## How does MyGet secure my private NuGet feeds?

MyGet offers you private NuGet feeds. In order for them to be private, they obviously need to be secured somehow. Security is a large word with many nuances and many possible scenarios, many which are supported by MyGet.
For example, some users can manage users and packages, while others can only manage packages and others can only consume packages.

We dedicated an entire page on this topic to guide you through MyGet’s security model and to show you how to set up security and permissions in MyGet.
Read more on [MyGet's Security Model Explained](MyGet-Security-FAQ)</a>.

## What happens to my private feeds when my subscription expires?
In short, the following will happen:

* Your public feeds will continue working
* Your community feeds will continue working
* Your private feeds will continue working in a read-only mode: you can consume packages but not add or push packages to this feed

## I'm trying to get my build server to push packages to my private feed but having to enter a username/password every time I push is a no go. Can I set that somewhere in the sources for nuget?

Since NuGet v1.7 there is support for storing username and password credentials in your NuGet configuration. 

Run the following command to do so:

    nuget.exe source Update -Name <name> -UserName <username> -Password <password>

The credentials are encrypted (using DPAPI the same as your APIKey) and stored in the nuget.config file in `%LocalAppData%\NuGet`.

`<Name>` is the name of the package source in the nuget.config file that corresponds with the MyGet feed for which you want to register these credentials.

## If I put a package on MyGet with a dependency that's on NuGet, do I need to copy it over?

**No. You don't have to, but you can.** NuGet performs cross-feed checking for package dependencies.

For the package being installed, NuGet looks in the current package source (if specified, otherwise all). Regardless of wether a package source is specified or not, NuGet will look for the package's dependencies in all configured package sources.

## Can I push packages without listing them?

**Yes, you can.** In earlier NuGet clients, it was possible to push a package to a NuGet server without publishing it. Later NuGet clients no longer support this scenario using one command. However by using both the `push` command followed by `delete`, this behaviour can be mimicked with later clients.

    nuget push MyPackage-1.0.nupkg -Source <feed> <apikey>
	nuget delete MyPackage 1.0 -Source <feed> <apikey>
	
This workflow will first push and publish the package and immediately request to unlist the package from the feed.

## What is the SLA for MyGet?

We provide our service best-effort and [have achieved 99.9% availability or more during for months in row](http://status.myget.org/519401/history).
Being hosted on Windows Azure, our level of availability is also subject to the levels [Microsoft provides us](http://www.windowsazure.com/en-us/support/legal/).

## Where is my data?

MyGet makes use of Microsoft's Windows Azure platform in the West Europe datacenter (primary) and North Europe datacenter (secondary).
Withregards of data and privacy compliance, we are subject to the terms listed in the [Windows Azure Trust Center](http://www.windowsazure.com/en-us/support/trust-center/). We make use of Cloud Services, Storage and the Access Control Service.
Windows Azure follows the [EU Data Protection Directive](http://www.windowsazure.com/en-us/support/trust-center/privacy/).

## Is my data backed up?

Yes! We run backups several times a week and retain weekly backups for 3 months. Data is backed up to our North Europe location.

## Are there any articles about MyGet out there?

Sure! We have a lot of content available [on our blog](http://blog.myget.org).

You can also check these:

* [Get your local NuGet repository online in a private MyGet feed](http://www.xavierdecoster.com/post/2011/06/08/Get-your-local-NuGet-repository-online-in-a-private-MyGet-feed.aspx)
* [Adding NuGet packages from the official feed to your MyGet feed: some improvements](http://www.xavierdecoster.com/post/2011/06/14/Adding-NuGet-packages-from-the-official-feed-to-your-MyGet-feed-some-improvements.aspx)
* [MyGet now supports pushing from the command line](http://blog.maartenballiauw.be/post/2011/06/01/MyGet-now-supports-pushing-from-the-command-line.aspx)
* [Delegate feed privileges to other users on MyGet](http://blog.maartenballiauw.be/post/2011/06/29/Delegate-feed-privileges-to-other-users-on-MyGet.aspx)
* [3 simple steps to publish a nupkg to MyGet using NuGet Package Explorer 1.6](http://www.xavierdecoster.com/post/2011/07/11/3-simple-steps-to-publish-a-nupkg-to-MyGet-using-NuGet-Package-Explorer-16.aspx)
* [How to help yourself when NuGet goes down?](http://www.xavierdecoster.com/post/2012/03/09/How-to-help-yourself-when-NuGet-goes-down.aspx)
* [MyGet tops vanilla NuGet feeds with a Chocolatey flavor](http://www.xavierdecoster.com/post/2012/03/01/MyGet-tops-Vanilla-NuGet-feeds-with-a-Chocolatey-flavor.aspx)
