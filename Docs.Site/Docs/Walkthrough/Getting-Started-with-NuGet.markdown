# Walkthrough - Getting Started Creating Your Own NuGet Feed

Setting up your own NuGet repository has never been easier. MyGet allows you to create your own public or private NuGet feeds in just a few clicks. This section will guide you through it.

## Creating a new MyGet feed

1. **Browse to [MyGet.org][1] and log in** using your preferred identity provider.

	![Use an existing identity or create a MyGet account from scratch.](/docs/walkthrough/Images/authenticate.png)

2. **Complete your new MyGet profile** by providing a username and password. These are your *MyGet credentials*, which you'll need to authenticate against private feeds on MyGet.org. From now on, you can also use these to log in on the MyGet.org web site.

3. **Create a new feed** and select the desired security template: *public, private or community*

	* public: everyone has read access, only feed owners/managers can write
	* private: only users with explicitly granted permissions can read or write (depending on permissions)
	* community: everyone can read all packages + anyone can manage the packages they pushed to the feed

## Setting up collaboration

1. **Invite collaborators** through the *[feed security][2]* settings.

2. **Register the feed** in your NuGet client: add the package source in Visual Studio, or store it in your NuGet.config for instance.

	You can register a MyGet feed the same way you register any NuGet package source by using the _Package Manager Settings_ dialog.
	You can find it under _Tools > Library Package Manager > Package Manager Settings_ in the Visual Studio menu.

	![Register MyGet Feed](/docs/walkthrough/Images/faq_register_myget_feed.png)

	To store your MyGet feed credentials in your account's roaming profile, you can use the latest NuGet Commandline tool (<a href="https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" title="Click here to download the latest NuGet commandline tool">NuGet.exe</a>).

	Also check out our blog post dedicated to this topic: <a href="https://blog.myget.org/post/2012/12/12/NuGet-package-restore-from-a-secured-feed.aspx" target="_blank">NuGet package restore from a secured feed</a>.

	Execute the following script using your MyGet `feedUrl` and MyGet `username`, `password` and `apikey`.
	Run this from a command line where you have access to `nuget.exe` (or set the path to your `nuget.exe` in a system environment variable).

	a. **Option A: Store credentials in machine-level `nuget.config` (non-transferable)**

	Register credentials for new package source:

		nuget setapikey [apikey] -source [url]
		nuget sources add -name [name] -source [url] -user [username] -pass [pwd]

	or update an already registered package source:

		nuget setapikey [apikey] -source [url]
		nuget sources update -name [name] -source [url] -user [username] -pass [pwd]

	b. **Option B: Store credentials in specific `nuget.config` (non-transferable)**

	Register credentials for new package source:

		nuget setapikey [apikey] -source [url] -configFile [configFilePath]
		nuget sources add -name [name] -source [url] -user [username] -pass [pwd] -configFile [path]

	or update an already registered package source:
	
		nuget setapikey [apikey] -source [url] -configFile [configFilePath]
		nuget sources update -name [name] -source [url] -user [username] -pass [pwd] -configFile [path]

	c. **Option C: Store credentials in specific `nuget.config` (transferable)**

	The above `nuget.config` files will only work on the machine and the account that created the configs, as its contents are encrypted with a machine-specific and user-specific key.
	
	If you want to share the `nuget.config` file in source control and be able to share credentials, use the `-StorePasswordInClearText` option.
	
	Register credentials for new package source:

		nuget setapikey [apikey] -source [url] -configFile [configFilePath]
		nuget sources add -name [name] -source [url] -user [username] -pass [pwd] -configFile [path] -StorePasswordInClearText

	or update an already registered package source:
	
		nuget setapikey [apikey] -source [url] -configFile [configFilePath]
		nuget sources update -name [name] -source [url] -user [username] -pass [pwd] -configFile [path] -StorePasswordInClearText

	d. **Option D: Manually add credentials to `nuget.config` (transferable)**
	
	The approaches mentioned earlier are using `nuget.exe` to update `nuget.config`. You can also register the feed manually [as described in the NuGet documentation](https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file#package-source-sections).
	
	Here's a sample `nuget.config` that can consume a private MyGet feed, by adding package source credentials:
	
		<?xml version="1.0" encoding="utf-8"?>
		<configuration>
		  <packageSources>
		    <add key="MyGet" value="https://www.myget.org/F/feedname-here/api/v3/index.json" />
		  </packageSources>
		  <packageSourceCredentials>
		    <MyGet>
		      <add key="Username" value="username-here" />
		      <add key="ClearTextPassword" value="password-here" />
		    </MyGet>
		  </packageSourceCredentials>
		  <activePackageSource>
		    <add key="All" value="(Aggregate source)" />
		  </activePackageSource>
		</configuration>

	To also push to this feed, add an `apiKeys` element to this file:
	
		<apikeys>
		  <add key="https://www.myget.org/F/feedname-here/api/v3/index.json" value="api-key-here" />
		</apikeys>

	Note that this config file will also work with `dotnet restore` on Windows, Mac OS X and Linux.
	
	e. **Option E: Register a pre-authenticated feed URL in `nuget.config` (transferable)**
	
	Your MyGet feed's **Feed Details** tab will provide you with a pre-authenticated feed URL. This URL contains an access token in the URL and will not require setting additional credentials in your `nuget.config` file. Note that the pre-authenticated feed URL contains a credential that will be displayed in build logs etc., so use this final option with caution!
	
	[Read more about pre-authenticated feed URLs](/docs/reference/feed-endpoints#Private_feed_endpoints_and_authentication).

3. **Add packages** to the feed by either uploading them through the web site, referencing/mirroring them from nuget.org, or pushing them using your preferred NuGet client tools.

## Configure a build source

MyGet supports many ways of integrating with hosted VCS or CI infrastructure, including built-in support for Visual Studio Team Services, GitHub and BitBucket, as well as any other hosted Git repository. For a full reference of MyGet Build Services, please take a look [here][3].

For the purpose of demonstration, this walkthrough will guide you through setting up a GitHub build source and link it to your MyGet feed. We have a sample GitHub repository that shows you how to make use of MyGet's smart conventions specifically designed to easily produce NuGet packages and serve them to your package consumers.
You'll notice we don't like you to be bound to a specific, single CI tool, so we explicitly try to stay clear of any MyGet-specific conventions or infrastructure.

The **sample repository** can be found on [GitHub][4].
If you don't want to hook up MyGet Build Services with your version control system, you can easily skip this section and continue reading the next one.

The easiest way to add our samples repository to your feed as a build source is by [forking the GitHub repository][5], and simply select **Add Build Source > from GitHub**.

![Adding a MyGet build source](/docs/walkthrough/Images/build-svc-add.png)

![Add your GitHub fork as a MyGet build source](/docs/walkthrough/Images/build-svc-addFromGitHub.png)

Alternatively, you can manually link the samples repository to your feed by copying the Git endpoint URL in the dialog presented by MyGet when you select **Add Build Source > Manually add build source...**

![Manually register a Git repository as a MyGet build source](/docs/walkthrough/Images/build-svc-addManually.png)

## Building sources and producing NuGet packages

Once the build source is configured, you can manually hit the **Build** button for this build source. The build agent will follow [these conventions][6] and automatically detect anything that should be packaged and push them to your feed. We also allow you to override some conventions, or even [completely take over the build process and customize it][7] to your needs.
Our sample repository only makes use of the tokenized NuGet manifest files that accompany the project files, which Just Works&trade;.

Now, try pushing some changes to your fork and watch what happens with your build source in just an instance. 

![A build got queued](/docs/walkthrough/Images/build-queued.png)

![Building...](/docs/walkthrough/Images/build-building.png)

![Successful!](/docs/walkthrough/Images/build-success.png)

When creating the build source, we **automatically configured a service hook on GitHub** to trigger a build on your MyGet build source.

![GitHub service hook to trigger a MyGet build source](/docs/walkthrough/Images/mygetdocs-github-deployhook.png)

You also want that shiny build status badge in your readme file? Or anywhere else? Simply copy-paste the HTML or Markdown by clicking the links in your build source details.

![A readme file containing the MyGet build status badge](/docs/walkthrough/Images/build-badge.png)

[1]: https://www.myget.org
[2]: https://docs.myget.org/docs/reference/security#Inviting_other_users_to_your_feed
[3]: https://docs.myget.org/docs/reference/build-services
[4]: https://github.com/myget/MyGetDocs-Samples
[5]: https://github.com/myget/MyGetDocs-Samples/fork
[6]: https://docs.myget.org/docs/reference/build-services#The_Build_Process
[7]: https://docs.myget.org/docs/reference/custom-build-scripts
