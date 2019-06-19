## Creating a new feed

1. **Browse to [MyGet.org][1] and log in** using your preferred identity provider.

	![Use an existing identity or create a MyGet account from scratch.](/docs/walkthrough/Images/authenticate.png)

2. **Complete your new MyGet profile** by providing a username and password. These are your *MyGet credentials*, which you'll need to authenticate against private feeds on MyGet.org. From now on, you can also use these to log in on the MyGet.org web site.

3. **Create a new feed** that will serve as a registry and select the desired security template: *public, private or community*

	* public: everyone has read access, only feed owners/managers can write
	* private: only users with explicitly granted permissions can read or write (depending on permissions)
	* community: everyone can read all packages + anyone can manage the packages they pushed to the feed

4. (optional) **Invite collaborators** through the *[feed security][2]* settings.

## Working with your Python registry

1. **Add packages** to the feed by either uploading them through the web site, referencing/mirroring them from *<a href="https://pypi.org" target="_blank" rel="noopener">pypi.org</a>*

	On each feed, packages can be added through the web UI.

	Keep in mind that currently MyGet does not support hosting tar.gz packages. Hovewer package which as a dependency has some other package which is available only in tar.gz can still be installed with „pip” tool. 
	If you type something in search field, the search is performed on pypi.org and returns all packages which names matches your term. Click on specific name and all .whl packages will be presented to you if they are available.

2. (optional) **Enable upstream source proxy** to seamlessly blend your MyGet feed with the *<a href="https://pypi.org" target="_blank" rel="noopener">pypi.org</a>*  repository.

	From the *Upstream Sources* tab, edit the *Python upstream source* and enable the *Make all upstream packages available in clients* option. If you prefer to have the package binaries downloaded to your feed for subsequent requests, also enable the *Automatically add downloaded upstream packages to the current feed (mirror)* option.

	In order to use new Python Wheel packages in an existing feed, you need to manually add an upstream source for this feed, using a preset - PyPI. We are working on a database migration to make it all automatic, but before this is completed that steps need to be taken.


[1]: https://www.myget.org
[2]: https://docs.myget.org/docs/reference/security#Inviting_other_users_to_your_feed
[3]: https://pypi.org
