# Walkthrough - Getting Started Creating Your Own Private or Public Ruby Gems Repository

Setting up your own Ruby gem repository has never been easier. MyGet allows you to host public or private package feeds to publish your own Ruby gems and mirror open-source gems from upstream sources like RubyGems.org. 

Once uploaded, you can use industry-standard tools like RubyGems to install gems from MyGet in your local environment, as well as install/manage dependencies, scan your hosted gems for license compliance and vulnerabilities, and more.

This article will guide you through the process of creating your Ruby gem repository in a MyGet feed and uploading your first gem.
<br/>

## Creating a new feed for your Ruby Gem

In MyGet, Ruby gems live in repositories we call “feeds.” Feeds allow you to curate a custom assortment of packages in one container, including NuGet, npm, Maven, Bower, PyPI, PHP Composer, and of course, Ruby gems.

1. **Browse to [MyGet.org][1]** and sign up for a new MyGet account, or log in using your username/password.

![Sign into MyGet to get started uploading your own private gem files.](/docs/walkthrough/Images/gem_docs_login.jpg)

2. **If you are signing up for MyGet for the first time,** complete your new MyGet profile by providing a username and password. These are your MyGet credentials, which you will need to authenticate against private MyGet feeds from your local dev environment. From now on, you can also use these to log in on the MyGet.org website.

3. **Create a new feed** that will serve as a repository for your gems and select the desired security template: public, private or community:

![Create a MyGet feed to serve as a repository for your gems.](/docs/walkthrough/Images/gem_docs_feed_details.png)

* **public:** everyone has read access, but only feed owners/managers can update or publish new gems
* **private:** only users with explicitly granted permissions can read or write (depending on permissions)
* **community:** everyone can read all packages in your feed, publish new packages to the feed, and manage the new packages they have added (similar to RubyGems.org)

4. (optional) **Invite collaborators** through the feed security settings. If you have created a private feed, you will need to explicitly grant users access to contribute to (write) or consume (read) the gems in your feed.

<br/>

## Add a gem to your MyGet feed

1. **Add gems to your feed** by (a) referencing/mirroring them from RubyGems.org, or (b) uploading them directly from your local machine.

a) **From Feed (upstream source).** To add a gem from RubyGems.org to your MyGet feed, click “Add package” and select the “From Feed” tab. From here, you can search any gems published to RubyGems.org and add them as a reference to the gem hosted by RubyGems.org or import the actual gem file to your MyGet-hosted feed.

![Add packages from upstream sources.](/docs/walkthrough/Images/gem_docs_add_package.jpg)

* **Search options.** Type a package name or a related term in the search field to search gems published on RubyGems.org and return a list of all of the gems matching your term. You can specify whether you would like to return results for all published versions of the package or just the most recent published version, as well as versions marked as “prerelease.”
* **Dependencies.** When you upload packages from an upstream source, you can also include dependencies (runtime). If the option is checked, all dependencies will be installed with the primary gem package. MyGet will try to fetch and add highest possible version of each dependency to satisfy the dependency requirements of the primary package you are uploading. If you choose to add an older version of a gem, MyGet will attempt to add the versions of the dependencies specified in the .gemspec file. You can also add package dependencies to your feed manually by independently adding them from RubyGems.org or your local machine just like you uploaded your primary package.
* **Mirroring.** When you add a package to MyGet from an upstream source, you can select whether MyGet should mirror that package or not. If you select the mirror option, MyGet will import the .gem file and host it on your MyGet account. Otherwise, MyGet will display the gem on your feed, referencing the .gem file hosted on RubyGems.org without importing the actual .gem file to MyGet itself.


b) **Upload from your local machine.** If you already have the gem file you would like to add to your MyGet on your local machine, you can upload it directly to MyGet from the web UI. You can upload one package at a time, or upload multiple packages simultaneously.

![Add packages from your local machine.](/docs/walkthrough/Images/gem_docs_add_package_upload.jpg)

* **Naming structure.** If you want to add a gem from your machine, keep in mind that the package name has to follow the naming structure below:

                <gem_name>-<gem_version>.gem
				
* **Package metadata.** When you upload a package, MyGet will import most of its metadata from the .gemspec file. If your package does not include a .gemspec file, you will see much less information about that package displayed in your MyGet feed.
* **Include dependencies.** You can also include dependencies when you upload packages from your local machine. If you choose to “Include dependencies,” MyGet will parse the .gemspec file in your gem to gather a list of all needed dependencies and try to fetch them from RubyGems.org. If MyGet cannot find the specified dependencies in the upstream source, only your primary gem will be added. You can always manually add dependencies from RubyGems.org or upload from your local machine later.
* **Mirror dependencies.** If you choose to mirror dependencies, MyGet will attempt to import the gem dependencies from RubyGems.org and host them for you on your MyGet feed. Otherwise, MyGet will add references to the dependencies on RubyGems.org so that you can view them in your MyGet feed without counting against your MyGet storage allotment.


2. (optional) **Enable upstream source proxying for RubyGems.org** to seamlessly blend your MyGet feed with the RubyGems.org repository.

Upstream package sources should be considered as *upstream repositories* for your MyGet feed. You can *aggregate* them in a single MyGet feed, filter each of them individually, and even *proxy* upstream package sources to include them in your feed queries.

This way, you can make packages from multiple community package hosting sites (like RubyGems.org, NuGet.org, npmjs.org, etc.) directly available via your MyGet feed, whether or not you have previously added them. 

(For more information about using Upstream Sources, check our <a href="https://docs.myget.org/docs/reference/upstream-sources" target="_blank" rel="noopener">help docs</a>.)

From your new Feed page, click the *Upstream Sources* tab in the menu to the left. Click the Edit for the RubyGems *upstream source* displayed in the preconfigured upstream sources list, and enable the*Make all upstream packages available in clients* option. If you prefer to have the package binaries downloaded to your feed when making a request for an upstream package not currently added to your MyGet feed, also enable the *Automatically add downloaded upstream packages to the current feed (mirror) option.* 

![Proxy upstream sources into MyGet.](/docs/walkthrough/Images/gem_docs_edit_upstream.jpg)

<br/>

## Pushing a gem package to your feed from the command line

You can easily upload a gem package through the MyGet.org website, but you can also push a gem from your command line if you would like using the cUrl utility and API key from your MyGet account. 

    $ curl -k -X POST https://<your_myget_domain>/F/<your_feed_name>/gem/upload -H “Authorization: Bearer <your_api_key>” -F “data=@<gem_name>-<gem_version>.gem”
	
(You can find your API key under the “Feed details” tab from your feed homepage. This key is automatically generated for you while you are adding a new feed.)

<br/>
## Working with your gems on MyGet
After adding gems to your MyGet feed repository, you can download the gems directly from MyGet.org, install them using the RubyGems installer, or publish new gems to MyGet from your local machine with Bundler. 

To fetch and install packages with gem tool you need to specify URL under which your repository (feed) can be found.

a) If your MyGet feed is set to **public** or **community**, you need only to specify the URL associated with your feed as in the example below:

    $ gem install <gem_name> --source https://<your_myget_domain>/F/<your_feed_name>/geminstall/
    
IMPORTANT: for feeds hosted on MyGet.org, your MyGet domain is simply `myget.org`. However, if your MyGet feeds are hosted on a MyGet Enterprise instance, you will need to specify your MyGet Enterprise subdomain as well (i.e. `mycompany.myget.org`).

b) If your MyGet Feed is **private**, you will need to include your MyGet username and password in URL specified during installation:

    $ gem install <gem_name> --source https://<username>:<password>@<your_myget_domain>/F/<your-feed-name>/geminstall/

or you can use your **api key** of your feed like this:

    $ gem install <gem_name> --source https://<your_myget_domain>/F/<your_feed_name>/auth/<your_api_key>/geminstall/

For your convenience, we recommend configuring MyGet as a `source` in your **rubygems** tool. You can set rubygems to authenticate with MyGet using a username/password or API key.

**Configuring rubygems to install from MyGet with a Username/Password:**

    $ gem sources -a https://<username>:<password>@<your_myget_domain>/F/<your-feed-name>/geminstall/

**Configuring rubygems to install gems from MyGet with an API key**:

    $ gem sources -a https://<your_myget_domain>/F/<your_feed_name>/auth/<your_api_key>/geminstall/   

After that you can install gems from your MyGet feed by simply invoking command:
   
    $ gem install <gem_name> 

MyGet doesn’t technically have gem server repository under the hood, but it emulates it. When you invoke the `gem install` command, MyGet will attempt to return the specified package as well as any specified dependencies in the gem’s .gemspec file. This means that your gem and all its dependencies should be uploaded or mirrored to your MyGet feed before installation so that the gem tool can correctly install them. 

If you haven’t installed your gem’s dependencies on MyGet but already have them installed in your local environment, installation will also be successful. 

However, if you don’t have needed dependencies locally or in your MyGet feed, gem install will fail on request for specs files (so-called ‘gem index’).

<br/>

## Next steps with MyGet Ruby gems
In addition to hosting your own public or private Ruby gem repository feeds, once you have uploaded gems to your MyGet account you can take advantage of MyGet’s other package management features, such as:

* <a href="https://docs.myget.org/docs/reference/license-analysis" target="_blank" rel="noopener">License Analysis.</a> View a report of the licenses used in the open source gems uploaded to your MyGet feed, so they can be inspected and managed
* <a href="https://docs.myget.org/docs/reference/vulnerability-report">Vulnerability Report.</a> Check if any of the gems you’ve uploaded to your MyGet feed have any known vulnerabilities published in the <a href="https://ossindex.net/" target="_blank" rel="noopener">OSSIndex</a> and <a href="https://www.vorsecurity.com/" target="_blank" rel="noopener">Vor Security databases</a>.
* API Feed Endpoints. Automatically send Ruby gems to MyGet from your CI/CD system using MyGet’s Feed Endpoints API. Check out the <a href="https://docs.myget.org/docs/reference/feed-endpoints" target="_blank" rel="noopener">Feed Endpoints documentation</a> to learn more. 

[1]: https://www.myget.org
