# MyGet npm support

After [signing up for a MyGet account](http://www.myget.org/Account/Login) and creating a feed that serves as a Bower registry, you can work with Bower packages using the Bower command line and *bower.json*.

## Your Bower registry URL

The full URL to your Bower feed on MyGet can be found on the *feed details* page.

![Bower feed URL on MyGet](Images/bower-feed-details.png)

This URL can be used with any Bower-compatible client. Note that a [pre-authenticated URL](/docs/reference/feed-endpoints) is also available for private Bower feeds.

The easiest way of registering the feed is running the following command:

	echo {"registry": "https://www.myget.org/F/your-feed-name/bower/"} > .bowerrc

For private feeds, make sure to either use a [pre-authenticated URL](/docs/reference/feed-endpoints) or include the username and password in the registry URL:

	echo {"registry": "https://username:password@www.myget.org/F/your-feed-name/bower/"} > .bowerrc

<p class="alert alert-info">
	<strong>Note:</strong> If you have any special characters in your username or password, such as an @ or a space, make sure to use the URL encoded value (e.g. `%40` for @, %23 for #, %2F for / and so on).
</p>

## Using multiple Bower registries

By default, your MyGet Bower feed will only contain packages you have explicitly added, either using the web UI or the Bower client. To have the public Bower registry blended into your own, go to the *Package Sources* tab, edit the *Bower.io* package source and enable the *Make all upstream packages available in clients* option and the the *Automatically add downloaded upstream packages to the current feed (mirror)* option.

Note that using these settings it's also possible to blend more than one Bower registry into one. 

![Mix your Bower registry with the public Bower registry](Images/proxy-bower-registry.png)

Additionally, your [`.bowerrc`](http://bower.io/docs/config/) file can be configured to search multiple feeds without requiring the feed to blend the feeds. Here's an example `.bowerrc` file that uses the public Bower registry and a MyGet feed to search for packages, and only supports registering packages with the MyGet feed:

	{
	  "registry": {
	    "search": [
	      "https://www.myget.org/F/your-feed-name/bower/",
	      "https://bower.herokuapp.com"
	    ],
		"register": "https://username:password@www.myget.org/F/your-feed-name/bower/"
	  }
	}

## Registering Bower packages
 
If you want to register a Bower package with a registry, you usually run the `bower register` command. This is not different with MyGet: `bower register` will register your package with a MyGet feed.

<p class="alert alert-info">
    <strong>Note:</strong> Make sure to use a <a href="/docs/reference/feed-endpoints">pre-authenticated URL</a> or include credentials in the registry URL if you want to register a package.
</p>

When this is done, any package can be registered on the MyGet Bower feed using the `register` command:

	bower register moment https://github.com/moment/moment.git

## Working with private Bower registries

If a MyGet Bower feed is marked as *private*, it will always require authentication. To setup authentication, make sure to either use a [pre-authenticated URL](/docs/reference/feed-endpoints) or include the username and password in the registry URL:

	echo {"registry": "https://username:password@www.myget.org/F/your-feed-name/bower/"} > .bowerrc

<p class="alert alert-info">
	<strong>Note:</strong> If you have any special characters in your username or password, such as an @ or a space, make sure to use the URL encoded value (e.g. `%40` for @, %23 for #, %2F for / and so on).
</p>

## Referencing Bower packages in bower.json

If you would like to reference packages, you can do so by using the package name and version in your `bower.json` file, or by using the URL to a specific version branch. For example:

	{
	  "name": "awesomeapplication",
	  "description": "An awesome application",
	  "version": "1.0.0",
	  "repository": {
	    "type": "git",
	    "url": "git://github.com/foo/bar.git"
	  },
	  "dependencies": {
	    "awesomepackage1": ">= 1.0.0",
	    "awesomepackage2": "https://github.com/owner/package.git#branch"
	  }
	}

Running `bower install` will make sure any dependency is downloaded and installed.

## Company proxy server

When using a company proxy server, make sure to configure it correctly. Justin James has a great guide to [npm, bower, git and bash proxy configurations](http://digitaldrummerj.me/proxy-configurations/) available.