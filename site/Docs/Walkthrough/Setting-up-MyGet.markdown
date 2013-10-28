# Walktrough #1 - Setting up MyGet

Setting up your own NuGet repository has never been easier. MyGet allows you to create your own public or private NuGet feeds in just a few clicks. This section will guide you through it.

## Creating up a new MyGet feed

1. **Browse to [MyGet.org][1] and log in** using your preferred identity provider. We currently support Google, Live ID, Yahoo!, Facebook, StackOverflow, GitHub and OpenID.

2. **Complete your new MyGet profile** by providing a username and password. These are your *MyGet credentials*, which you'll need to authenticate against private feeds on MyGet.org. From now on, you can also use these to log in on the MyGet.org web site.

3. **Create a new feed** and select the feed type: *public, private or community*

	* public: everyone has read access, only feed owners/managers can write
	* private: only users with explicitly granted permissions can read or write (depending on permissions)
	* community: everyone can read all packages + anyone can manage the packages he pushed to the feed

## Setting up collaboration

1. **Invite collaborators** through the *[feed security][2]* settings.

2. **Register the feed** in your NuGet client: add the package source in Visual Studio, or store it in your NuGet.config for instance. ([how to?][3])

3. **Add packages** to the feed by either uploading them through the web site, referencing/mirroring them from nuget.org, or pushing them using your preferred NuGet client tools.

[1]: http://www.myget.org
[2]: http://docs.myget.org/docs/reference/feed-security
[3]: http://docs.myget.org/docs/how-to/register-myget-feeds-in-visual-studio