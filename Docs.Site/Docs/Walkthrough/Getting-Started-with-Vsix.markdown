# Walkthrough - Getting Started Creating Your Own Vsix feed

Setting up your own Vsix feed has never been easier. MyGet allows you to create your own public or private Visual Studio extension feeds in just a few clicks. You can work with Visual Studio Extensions, Roslyn analyzers and refactorings and more. This section will guide you through it.

## Creating a new feed

1. **Browse to [MyGet.org][1] and log in** using your preferred identity provider.

	![Use an existing identity or create a MyGet account from scratch.](/docs/walkthrough/Images/authenticate.png)

2. **Complete your new MyGet profile** by providing a username and password. These are your *MyGet credentials*, which you'll need to authenticate against private feeds on MyGet.org. From now on, you can also use these to log in on the MyGet.org web site.

3. **Create a new feed** select the desired security template: *public, private or community*

	* public: everyone has read access, only feed owners/managers can write
	* private: only users with explicitly granted permissions can read or write (depending on permissions)
	* community: everyone can read all extensions + anyone can manage the extensions they pushed to the feed

4. (optional) **Invite collaborators** through the *[feed security][2]* settings.

## Working with your Vsix feed

1. **Register the feed** with Visual Studio. The full feed URL can be found on the *feed details* page.

	![VSIX feed URL on MyGet](/docs/walkthrough/Images/vsix-feed-details.png)

	This feed URL can be registered in Visual Studio by opening the _Tools | Options..._ menu. The _Environment | Extensions and Updates_ pane lets us add the feed from MyGet.

	![VSIX feed in Visual Studio](/docs/walkthrough/Images/vsix-vs-options.png)

	Note that to work with a _private_ Vsix feed, you'll have to use the pre-authenticated feed URL.

2. **Add extensions** to the feed by uploading them through the web site.

	On each feed, extensions can be added through the web UI.

	![Add VSIX to feed](/docs/walkthrough/Images/add-vsix.png)

	Additionally, you can do a HTTP POST from your build server to the feed to add a Visual Studio extension from your continuous integration process. The POST URL is in the form of `/F/<feedname>/vsix/upload`. Note you will have to add the `X-NuGet-ApiKey` header with a valid API key as well. For example using cUrl, try:
	`curl -X POST --verbose --data-binary @"path-to-vsix-file.vsix" -H "X-NuGet-ApiKey: api-key-here" https://www.myget.org/F/<feedname>/vsix/upload`

3. **Consume extensions** in Visual Studio.

	The _Tools | Extensions and Updates_ menu now holds your feed's extensions. They can be installed, upgraded and removed from your Visual Studio installation.

	![Consume VSIX or Roslyn from MyGet](/docs/walkthrough/Images/vsix-consume.png)

4. (optional) **Check the licenses of the extensions on your feed** using the *[licenses][3]* tab. This will display a report of the licenses used by the extensions on your feed.

[1]: https://www.myget.org
[2]: https://docs.myget.org/docs/reference/feed-security
[3]: https://docs.myget.org/docs/reference/license-analysis
