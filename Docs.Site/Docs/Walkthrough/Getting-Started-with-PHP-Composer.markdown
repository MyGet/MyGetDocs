# Walkthrough - Getting Started Creating Your Own PHP Composer Repository

Setting up your own [PHP Composer](https://getcomposer.org) repository has never been easier. MyGet allows you to create your own public or private PHP Composer repositories in just a few clicks. This section will guide you through it.

## Creating a new MyGet feed

1. **Browse to [MyGet.org][1] and log in** using your preferred identity provider. We currently support Microsoft Account, Google, GitHub, Facebook, StackExchange and OpenID.

	![Use an existing identity or create a MyGet account from scratch.](Images/authenticate.png)

2. **Complete your new MyGet profile** by providing a username and password. These are your *MyGet credentials*, which you'll need to authenticate against private feeds on MyGet.org. From now on, you can also use these to log in on the MyGet.org web site.

3. **Create a new feed** (our name for a repository) and select the desired security template: *public, private or community*

	* public: everyone has read access, only feed owners/managers can write
	* private: only users with explicitly granted permissions can read or write (depending on permissions)
	* community: everyone can read all packages + anyone can manage the packages they pushed to the feed

4. (optional) **Invite collaborators** through the *[feed security][2]* settings.

## Working with your PHP Composer repository

### Register the repository in composer.json

Register the repository URL in the project's `composer.json` file by adding (or extending) the `repositories` property:

    "repositories": [
        {
          "type":"composer",
          "url":"https://www.myget.org/F/your-feed-name/composer"
        }
    ]

For public and community repositories, there's no need to add authentication details. For private repositories, you can use the _pre-authenticated feed URL_ listed on your MyGet feed's _Details_ tab.

<p class="alert alert-info">
    <strong>Note:</strong> Basic authentication is available using our <a href="/downloads/composer.phar"><code>composer.phar</code> build</a>. This build includes <a href="https://github.com/composer/composer/pull/6717">a fix for basic authentication with redirects in PHP Composer</a> - please upvote this pull request!
</p>

We can add our MyGet credentials to `auth.json`, either right next to `composer.json` or on `COMPOSER_HOME`:

    {
        "http-basic": {
            "www.myget.org": {
                "username": "my-username1",
                "password": "my-secret-password"
            }
        }
    }

More info on authentication is available from the [Composer docs](https://getcomposer.org/doc/articles/http-basic-authentication.md).

<p class="alert alert-warning">
    <strong>Important!</strong> Make sure to add <code>auth.json</code> to your <code>.gitignore/.hgignore/...</code> file to prevent accidental leaking of credentials via source control.
</p>

## Next steps

1. **Add packages** to the feed by either uploading them through the web site, referencing/mirroring them from [Packagist.org](https://www.packagist.org), or publishing them using `curl`.

	On each feed, packages can be added through the web UI.

	![Upload package or add package from Packagist.org](Images/add-phpcomposer-fromupload.png)
	
	Uploading packages via cURL is supported as well, here's a sample command that can be used:
	
	`curl -k -X POST https://www.myget.org/F/your-feed-name/composer/dist/<packageid>/<packageversion>.zip -H "Authorization: Bearer <your access token>" -F "data=@Package.zip"`

2. (optional) **Enable upstream source proxy** to seamlessly blend your MyGet feed with the [Packagist.org](https://www.packagist.org) repository.

	From the *Upstream Sources* tab, edit the *Packagist* upstream source and enable the *Make all upstream packages available in clients* option. If you prefer to have the package binaries downloaded to your feed for subsequent requests, also enable the *Automatically add downloaded upstream packages to the current feed (mirror)* option.

	![Mix your MyGet PHP Composer repository with the public Packagist.org repository](Images/proxy-npm-registry.png)

	Note that using these settings it's also possible to blend more than one PHP Composer repository into one.

3. (optional) **Check the licenses of the packages on your feed** using the *[licenses][3]* tab. This will display a report of the licenses used by the packages on your PHP Composer feed.

	![Inspect package licenses](Images/maven-licenses.png)

[1]: https://www.myget.org
[2]: https://docs.myget.org/docs/reference/feed-security
[3]: https://docs.myget.org/docs/reference/license-analysis
