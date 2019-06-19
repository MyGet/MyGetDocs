# Walkthrough - Getting Started Creating Your Own npm registry

Setting up your own npm registry has never been easier. MyGet allows you to create your own public or private npm registries in just a few clicks and work with your own node modules (packages). This section will guide you through it.

## Creating a new feed

1. **Browse to [MyGet.org][1] and log in** using your preferred identity provider.

	![Use an existing identity or create a MyGet account from scratch.](/docs/walkthrough/Images/authenticate.png)

2. **Complete your new MyGet profile** by providing a username and password. These are your *MyGet credentials*, which you'll need to authenticate against private feeds on MyGet.org. From now on, you can also use these to log in on the MyGet.org web site.

3. **Create a new feed** that will serve as a registry and select the desired security template: *public, private or community*

	* public: everyone has read access, only feed owners/managers can write
	* private: only users with explicitly granted permissions can read or write (depending on permissions)
	* community: everyone can read all packages + anyone can manage the packages they pushed to the feed

4. (optional) **Invite collaborators** through the *[feed security][2]* settings.

## Working with your NPM registry

1. **Register the feed** with the npm command line. This can be done by using the `--registry` switch on every npm command, or by editing the `.npmrc`. The full feed URL can be found on the *feed details* page.

	![NPM feed URL on MyGet](/docs/walkthrough/Images/npm-feed-details.png)

	If you plan on using your MyGet npm feed all the time, we recommend running the following command:

		npm config set registry https://www.myget.org/F/your-feed-name/npm/
	
	If a MyGet npm feed is marked as *private*, it will always require authentication. To setup authentication, run the following commands:
		
		npm login --registry=https://www.myget.org/F/your-feed-name/npm/
		npm config set always-auth true 

2. **Add packages** to the feed by either uploading them through the web site, referencing/mirroring them from the npm registry, or pushing them using the npm client.

	On each feed, packages can be added through the web UI.

	![Add package from NPM registry](/docs/walkthrough/Images/add-npm-fromfeed.png)

3. (optional) **Enable upstream source proxy** to seamlessly blend your MyGet feed with the public npm registry.

	From the *Upstream Sources* tab, edit the *Npmjs.org* upstream source and enable the *Make all upstream packages available in clients* option. If you prefer to have the package binaries downloaded to your feed for subsequent requests, also enable the *Automatically add downloaded upstream packages to the current feed (mirror)* option.

	![Mix your npm registry with the public npm registry](/docs/walkthrough/Images/proxy-npm-registry.png)

	Note that using these settings it's also possible to blend more than one npm registry into one. You can also [push npm packages to other npm registries using MyGet](/docs/reference/package-sources#Scenario_-_Pushing_a_package_upstream)

4. (optional) **Check the licenses of the packages on your feed** using the *[licenses][3]* tab. This will display a report of the licenses used by the packages on your npm feed.

	![Inspect package licenses](/docs/walkthrough/Images/npm-licenses.png)

[1]: https://www.myget.org
[2]: https://docs.myget.org/docs/reference/security#Inviting_other_users_to_your_feed
[3]: https://docs.myget.org/docs/reference/license-analysis