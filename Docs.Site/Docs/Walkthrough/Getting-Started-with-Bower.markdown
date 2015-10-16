# Walkthrough - Getting Started Creating Your Own Bower registry

Setting up your own Bower registry has never been easier. MyGet allows you to create your own public or private Bower registries in just a few clicks and work with your own Bower packages. This section will guide you through it.

## Creating a new feed

1. **Browse to [MyGet.org][1] and log in** using your preferred identity provider.

	![Use an existing identity or create a MyGet account from scratch.](Images/authenticate.png)

2. **Complete your new MyGet profile** by providing a username and password. These are your *MyGet credentials*, which you'll need to authenticate against private feeds on MyGet.org. From now on, you can also use these to log in on the MyGet.org web site.

3. **Create a new feed** that will serve as a registry and select the desired security template: *public, private or community*

	* public: everyone has read access, only feed owners/managers can write
	* private: only users with explicitly granted permissions can read or write (depending on permissions)
	* community: everyone can read all packages + anyone can manage the packages they pushed to the feed

4. (optional) **Invite collaborators** through the *[feed security][2]* settings.

## Working with your Bower registry

1. **Register the feed** with Bower by adding a registry entry to your [`.bowerrc`](http://bower.io/docs/config/) file. The full feed URL can be found on the *feed details* page.

	![Bower feed URL on MyGet](Images/bower-feed-details.png)

	The easiest way of registering the feed is running the following command:

		echo {"registry": "https://www.myget.org/F/your-feed-name/bower/"} > .bowerrc

	For private feeds, make sure to either use a [pre-authenticated URL](/docs/reference/feed-endpoints) or include the username and password in the registry URL:

		echo {"registry": "https://username:password@www.myget.org/F/your-feed-name/bower/"} > .bowerrc

	<p class="alert alert-info">
	    <strong>Note:</strong> If you have any special characters in your username or password, such as an @ or a space, make sure to use the URL encoded value (e.g. `%40` for @, %23 for #, %2F for / and so on).
	</p>

2. **Add packages** to the feed by through the web site, mirroring them from the Bower registry, or pushing them using the Bower client.

	On each feed, packages can be added through the web UI.

	![Add package from Bower registry](Images/add-bower-fromfeed.png)

3. (optional) **Enable package source proxy** to seamlessly blend your MyGet feed with the public Bower registry.

	From the *Package Sources* tab, edit the *Bower.io* package source and enable the *Make all upstream packages available in clients* option. If you prefer to have the package binaries downloaded to your feed for subsequent requests, also enable the *Automatically add downloaded upstream packages to the current feed (mirror)* option.

	![Mix your Bower registry with the public Bower registry](Images/proxy-bower-registry.png)

	Note that using these settings it's also possible to blend more than one Bower registry into one.

[1]: http://www.myget.org
[2]: http://docs.myget.org/docs/reference/feed-security
