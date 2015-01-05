# Walkthrough - Getting Started

Setting up your own NuGet repository has never been easier. MyGet allows you to create your own public or private NuGet feeds in just a few clicks. This section will guide you through it.

## Creating a new MyGet feed

1. **Browse to [MyGet.org][1] and log in** using your preferred identity provider. We currently support Microsoft Account, Google, GitHub, Facebook, StackExchange and OpenID.

2. **Complete your new MyGet profile** by providing a username and password. These are your *MyGet credentials*, which you'll need to authenticate against private feeds on MyGet.org. From now on, you can also use these to log in on the MyGet.org web site.

3. **Create a new feed** and select the feed type: *public, private or community*

	* public: everyone has read access, only feed owners/managers can write
	* private: only users with explicitly granted permissions can read or write (depending on permissions)
	* community: everyone can read all packages + anyone can manage the packages they pushed to the feed

## Setting up collaboration

1. **Invite collaborators** through the *[feed security][2]* settings.

2. **Register the feed** in your NuGet client: add the package source in Visual Studio, or store it in your NuGet.config for instance.

	You can register a MyGet feed the same way you register any NuGet package source by using the _Package Manager Settings_ dialog.
	You can find it under _Tools > Library Package Manager > Package Manager Settings_ in the Visual Studio menu.

	![Register MyGet Feed](Images/faq_register_myget_feed.png)

	To store your MyGet feed credentials in your account's roaming profile, you can use the following Gist in combination with the NuGet Commandline tool (<a href="https://nuget.org/nuget.exe" title="Click here to download the NuGet commandline tool">NuGet.exe</a>):

	<script src="https://gist.github.com/xavierdecoster/3205826.js"></script>

	Also check out our blog post dedicated to this topic: <a href="http://blog.myget.org/post/2012/12/12/NuGet-package-restore-from-a-secured-feed.aspx" target="_blank">NuGet package restore from a secured feed</a>.

3. **Add packages** to the feed by either uploading them through the web site, referencing/mirroring them from nuget.org, or pushing them using your preferred NuGet client tools.

[1]: http://www.myget.org
[2]: http://docs.myget.org/docs/reference/feed-security
