Setting up your own Python registry has never been easier. MyGet allows you to create your own public or private Python registries in just a few clicks and work with your own Python packages. This section will guide you through it.

## Creating a new feed

1. **Browse to [MyGet.org][1] and log in** using your preferred identity provider.

	![Use an existing identity or create a MyGet account from scratch.](/docs/walkthrough/Images/authenticate.png)

2. **Complete your new MyGet profile** by providing a username and password. These are your *MyGet credentials*, which you'll need to authenticate against private feeds on MyGet.org. From now on, you can also use these to log in on the MyGet.org web site.

3. **Create a new feed** that will serve as a registry and select the desired security template: *public, private or community*

	* public: everyone has read access, only feed owners/managers can write
	* private: only users with explicitly granted permissions can read or write (depending on permissions)
	* community: everyone can read all packages + anyone can manage the packages they pushed to the feed

4. (optional) **Invite collaborators** through the *[feed security][2]* settings.

## Next steps

1. **Add packages** to the feed by either uploading them through the web site, referencing/mirroring them from *<a href="https://pypi.org" target="_blank" rel="noopener">pypi.org</a>*

	On each feed, packages can be added through the web UI.

	![On each feed, packages can be added through the web UI.](/docs/walkthrough/Images/python-package-web-ui.png)

	Keep in mind that currently MyGet does not support hosting tar.gz packages. Hovewer package which as a dependency has some other package which is available only in tar.gz can still be installed with „pip” tool. 

	If you type something in search field, the search is performed on pypi.org and returns all packages which names matches your term. Click on specific name and all .whl packages will be presented to you if they are available.

2. (optional) **Enable upstream source proxy** to seamlessly blend your MyGet feed with the *<a href="https://pypi.org" target="_blank" rel="noopener">pypi.org</a>* repository.

	From the *Upstream Sources*  tab, edit the *Python upstream source* and enable the *Make all upstream packages available in clients* option. If you prefer to have the package binaries downloaded to your feed for subsequent requests, also enable the *Automatically add downloaded upstream packages to the current feed (mirror)* option.

	![Enable upstream source proxy.](/docs/walkthrough/Images/enable-upstream-source-proxy.png)


## Working with your Python repository

In order to be able to fetch and install packages with pip tool you need to specify url under which your repository (feed) can be found.

a) for non private feed

		pip install --index-url https://<your_myget_domain>/F/<your-feed-name>/python <package_name>

b) for private feed

		pip install --index-url https://<username>:<password>@<your_myget_domain>/F/<your-feed-name>/python   <package_name>

For your convenience, we recommend you to add these parameters to your pip.conf file in order to avoid unnecessary typing. Then your command for package installation will look like this:

		pip install <package_name> 

Please take a note, that if you try to install package that is not present on your MyGet feed, tool will fetch that package from main repository ( *<a href="https://pypi.org/simple" target="_blank" rel="noopener">https://pypi.org/simple</a>*/ ). 

If you have configured your pip tool and added some package to your feed, you are now able to install it.

There is often a situation  when your package added to feed has some other dependencies. In that case if your package and its dependent packages are available on feed, pip will fetch and install these from that feed.

On the other hand, if dependencies are not available on your feed, pip will look for it on its main repository and install it along with your package which is placed on feed.

Keep in mind that if you want pip to install your package from  feed, it must be mirrored on that feed

<<<<<<< HEAD
##Installing specific version

For installing specific version you can use the same commands which pip normally exposes. For example in order to install version 0.0.4 of package A:

               pip install --index-url https://<your_myget_domain>/F/<your-feed-name>/python A==0.0.4

If you don’t specify package version, the newest version of that package which is placed and mirrored on that feed will be installed.

=======
>>>>>>> 5c29b3f0dc4ff5adda38caedbb3fd7e770ac3ef2
[1]: https://www.myget.org
[2]: https://docs.myget.org/docs/reference/security#Inviting_other_users_to_your_feed
[3]: https://pypi.org
