# Publish to MyGet from a VSTS build

Visual Studio Team Services or VSTS in short allows us to create rich build pipelines. As part of a build, you may want to publish NuGet packages, often including [debugger symbols](/docs/reference/symbols), to your MyGet feed. Let's see how this can be done.

<p class="alert alert-info">
    <strong>Note:</strong> This tutorial describes manually pushing NuGet and/or symbols packages from a VSTS build. <b>The easiest way to publish NuGet package artifacts from VSTS to MyGet</b> is to <a href="http://blog.myget.org/post/2014/05/12/Announcing-Visual-Studio-Online-integration.aspx#scenario1">enable VSTS integration and configure Visual Studio Team Services as an upstream package source</a>.
</p>

## Prerequisites

In this tutorial, we assume NuGet packages are already being created, for example using the *NuGet Packager* build task. We'll explain the next steps in publishing these packages to your MyGet feed.
 
## Adding the NuGet Publisher build step

Edit your build definition and add a new build step. Under the **Package** node, find the **NuGet Publisher** task and add it to your build definition. The order in which this step runs is not very important, except that it has to happen after the NuGet packages already have been created. Ideally, this step only runs when build and running tests is successful.

![Adding the NuGet Publisher build task to publish from VSTS to MyGet](Images/vsts-add-nuget-publisher.png)

## Configure the NuGet Publisher build step

In the NuGet Publisher build task, make sure the generated packages can be found. The path/pattern will have to be configured correctly (the default usually works fine). The feed type is an *External NuGet feed*.

Before being able to select the **NuGet Server Endpoint**, click the **Manage** button on the right.  You will first need to add a new endpoint to the VSTS configuration. 

In the toolbar, hit **New Service Endpoint | Generic** and configure your MyGet feed:

* **Connection Name**: a description of the feed you will be publishing to.
* **Server URL**: the package publish URL which can be found in your MyGet feed's "Feed Details" tab. Note that the URL has to end in `/api/v2/package` for publishing to work succesfully.
* **Password/Token Key**: the API key to your MyGet feed. This can be found in your MyGet feed's "Feed Details" tab.

![Add generic endpoint](Images/vsts-add-generic-endpoint.png)

Once configured, we can select the **NuGet Server Endpoint** we just created and save the build step.

![Configure MyGet as the package push endpoint](Images/vsts-configure-endpoint.png)

## Run the build

As part of the build, VSTS will now publish NuGet and/or symbols packages to MyGet.

<p class="alert alert-info">
    <strong>Note:</strong> The blog of Ricci Gian Maria has also a great example of <a href="http://www.codewrecks.com/blog/index.php/2015/09/26/publishing-a-nuget-package-to-nugetmyget-with-vso-build-vnext/">Publishing a Nuget package to Nuget/Myget with VSTS Build vNext></a>.
</p>
