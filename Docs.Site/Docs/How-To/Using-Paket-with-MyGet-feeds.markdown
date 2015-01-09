# Using Paket with MyGet feeds

<img src="Images/paket.png" align="right"/> Just like NuGet, Paket is a dependency manager for .NET and Mono projects. It is designed to work with NuGet packages but also enables referencing files directly from GitHub repositories and GitHub Gists.

Let's see how we can use Paket with a MyGet feed.

## Downloading Paket

We will need Paket on our system, either instaled globally or on a per-project basis.

To install Paket globally, [download Paket from GitHub](https://github.com/fsprojects/Paket/releases/latest), copy it to disk and make sure it is available from the system `PATH`.

To install Paket per project, use NuGet. For example:

	Install-Package Paket

This will download the Paket executable to the packages folder of our project.

## Converting from NuGet to Paket

Since Paket uses its own mechanism for specifying dependencies, we may want to convert existing projects to make use of Paket. We can [convert from NuGet to Paket manually or automated](http://fsprojects.github.io/Paket/paket-convert-from-nuget.html), using the approach outlined [in the Paket documentation](http://fsprojects.github.io/Paket/paket-convert-from-nuget.html).

As an example, let's automatically convert from using NuGet to using Paket by running `paket convert-from-nuget`:

	paket convert-from-nuget --force --creds-migration selective

Paket will ask if we want to migrate feed credentials and if they have to be stored encrypted or plain-text. Once finished, our `packages.config` file will be coverted to a `paket.dependencies` file which may look like this:

	source https://www.myget.org/F/paket-demo/api/v2
	
	nuget Antlr 3.4.1.9004
	nuget bootstrap 3.0.0
	nuget jQuery 1.10.2
	nuget jQuery.Validation 1.11.1
	nuget Microsoft.AspNet.Mvc 5.2.2
	nuget Microsoft.AspNet.Razor 3.2.2
	nuget Microsoft.AspNet.Web.Optimization 1.1.3
	nuget Microsoft.AspNet.WebPages 3.2.2
	nuget Microsoft.jQuery.Unobtrusive.Validation 3.2.2
	nuget Microsoft.Web.Infrastructure 1.0.0.0
	nuget Modernizr 2.6.2
	nuget Newtonsoft.Json 6.0.4
	nuget Respond 1.2.0
	nuget WebGrease 1.5.2

We can now use Paket to restore an update NuGet packages in our project. Running `paket install` will perform a package restore. By running `paket init-auto-restore` this also injected into our Visual Studio project so we don't have to resort to the command-line when building our project.


## Using a private MyGet feed

To work with private feeds, we will have to provide Paket with credentials of some sort. In `paket.dependencies`, we can specify the credentials to be used for a feed.

Plain-text password:

	source https://www.myget.org/F/paket-demo/api/v2 username: "username_here" password: "password_here"

This does, however, require checking in credentials into source control. Alternatively, we can make use of environment variables:

	source https://www.myget.org/F/paket-demo/api/v2 username: "%MY_USERNAME%" password: "%MY_PASSWORD%"

Using this technique is interesting as it makes it possible to securely provide credentials in an environment variable on build servers like [TeamCity](http://www.jetbrains.com) or our own [build services](/docs/reference/build-services#User-defined_environment_variables).

<p class="alert alert-info">
    <strong>Note:</strong> It is also possible to make use of a <a href="/docs/reference/feed-endpoints#Private_feed_endpoints_and_authentication">pre-authenticated feed URL</a>. Do keep in mind that such URLs contain a MyGet API key and should be treated as confidential.<br/<br/>
	The Paket source will look like the following in such case:<br/>
	<pre class="prettyprint"><code>source https://www.myget.org/F/paket-demo/auth/147fae61-95fb-4747-9e54-09debb256c99/</code></pre>
</p>

## Adding a package from MyGet

To add a package from MyGet, all we need is to add a `source` to the `paket.dependencies` file and specify the dependencies. Let's add `https://www.myget.org/F/paket-demo/api/v2` as the feed and add JSON.NET as a dependency:

	source https://www.myget.org/F/paket-demo/api/v2
	
	nuget Newtonsoft.Json 6.0.4

Running `paket install` will install this package into our project.

## Restoring packages with Paket

Paket comes with two diferent files: `paket.dependencies`, which specifies the dependencies for our project, and `paket.lock` which maintains the state of the packages that are actually installed.

To restore packages, we can either:

* Build the project in Visual Studio. Note that beforehand we should have run `paket init-auto-restore` once to make sure Paket is hooked into the build process.
* Run `paket install`.

Both will make sure Paket downloads the missing packages from `paket.lock` and brings our project to its desired state.

## Updating packages from MyGet using Paket

When using Paket, it's really easy to determine if the packages that are installed are outdated. All we need to do is make sure the `paket.dependencies` file contains [the version constraints](https://fsprojects.github.io/Paket/nuget-dependencies.html) that tell Paket which versions are okay to update to.

We can run `paket outdated` to see which updates are available:

	Paket version 0.22.6.0
	found: paket.dependencies
	Resolving packages:
	    - exploring Microsoft.AspNet.Mvc 5.2.2
	    - exploring Antlr 3.4.1.9004
	    - exploring Microsoft.AspNet.Razor 3.2.2
	    - exploring Microsoft.AspNet.WebPages 3.2.2
	    - exploring Microsoft.jQuery.Unobtrusive.Validation 3.2.2
	    - exploring bootstrap 3.0.0
	    - exploring Modernizr 2.6.2
	    - exploring jQuery.Validation 1.11.1
	    - exploring jQuery 1.10.2
	    - exploring WebGrease 1.5.2
	    - exploring Respond 1.2.0
	    - exploring Microsoft.AspNet.Web.Optimization 1.1.3
	    - exploring Microsoft.Web.Infrastructure 1.0.0.0
	  - fetching versions for Newtonsoft.Json
	    - exploring Newtonsoft.Json 6.0.7
	Outdated packages found:
	  * Newtonsoft.Json 6.0.4 -> 6.0.7
	3 seconds - ready.

To download and install updates, we can run `paket update`. This will update `paket.lock` with the latest packages installed.
