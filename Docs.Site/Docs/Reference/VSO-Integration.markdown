# Visual Studio Team Services Integration

MyGet and Visual Studio Team Services provide an integrated user experience using the VSTS Extensions API. This document describes the newly enabled scenarios.

* Fetch NuGet packages from VSTS drop location and serve them from MyGet
* Add a VSTS Git repository as a Build Source for a MyGet feed
* Register a MyGet Service Hook within Visual Studio Team Services (if you have an existing MyGet Build Source)

<hr/>

## Fetch NuGet packages from VSTS drop location and serve them from MyGet

Having a VSTS Build Definition creating NuGet packages but having trouble consuming those? You can now very easily serve those NuGet packages from a real NuGet feed by having MyGet fetching them directly from your Build Definition’s drop location!

MyGet has the concept of *[Package Sources](/docs/reference/package-sources)*. Package sources could be considered as *upstream repositories* for your MyGet feed, which is how you can look at the VSTS Build Definition’s drop location. 

The following simplified diagram provides a schematic overview of this new integration scenario.

![Visual Studio Team Services as a Package Source](Images/vso-package-source-diagram.png)

To make use of this integration, you need to add a new package source to your MyGet feed:

1.	Go to feed settings and select the **Upstream Sources** tab. Select **Visual Studio Team Services build definition** from the **Add package source** button.
	![Step 1](Images/vso-package-source-step1.png)

2.	Provide your Visual Studio Team Services **account name** in the dialog that appears and click **Continue**.
	![Step 2](Images/vso-package-source-step2.png)

3.	Select the **Team Project** and **Build Definition** to add as the package source for your feed. Click the **Add** button to complete the configuration of the new package source.
	![Step 3](Images/vso-package-source-step3.png)

4.	The new VSTS package source has been added to your feed’s package sources, and newly built NuGet packages will be fetched from your Build Definition’s drop location automatically and pushed to the MyGet feed.
	![Step 4](Images/vso-package-source-step4.png)

<hr/>

## Add a VSTS Git repository as a Build Source for a MyGet feed

This is the same scenario we already support today for GitHub, BitBucket and Codeplex: you can now register MyGet Build Services as a service hook for your VSTS Git repositories, listening for the *code is pushed* event. 

MyGet feeds have the concept of *[Build Sources](/docs/reference/build-services)*. Build sources are a way to express *a link between MyGet Build Services and a source repository*, whether on GitHub, BitBucket, Codeplex or now also on Visual Studio Team Services. 

The following simplified diagram provides a schematic overview of this new integration scenario.

![Visual Studio Team Services as a Build Source](Images\vso-build-source-diagram.png)

<p class="alert alert-info">
	<strong>Note:</strong> we currently only support Git repositories (no TFVC support yet).
</p>

Follow these steps to enable this scenario:

1.	Go to your MyGet feed settings and select the **Build Services** tab. Select **from Visual Studio Team Services (git only)** from the **Add build source…** button.
	![Step 1](Images\vso-build-source-step1.png)

2.	Provide your Visual Studio Team Services **account name** in the dialog that appears and click **Continue**.
	![Step 2](Images\vso-build-source-step2.png)

3.	Select the desired build source to **link**. Click the **Add** button to complete the configuration of the new Build Source.
	![Step 3](Images\vso-build-source-step3.png)

4.	The new VSTS build source has been added to your feed’s build sources, and a service hook has been registered within VSTS. When pushing to your VSTS Git repository, a new MyGet build is going to be triggered and will produce your NuGet packages and push them to your feed.
	![Step 4.1](Images\vso-build-source-step4-1.png)

	If you look at your Visual Studio Team Services Administration dashboard and browse your Team Project’s Service Hooks, you’ll see a newly registered web hook listening for the Code is pushed event.
	![Step 4.2](Images\vso-build-source-step4-2.png)

<hr/>

## Register a MyGet Service Hook within Visual Studio Team Services (if you have an existing MyGet Build Source)

Alternatively, you can also setup MyGet integration from within Visual Studio Team Services. This is particularly useful if you already have an existing MyGet Build Source pointing to your VSTS Git repository and triggered it manually up until now. You should be glad to hear you can now connect these and automate that.

1.	Go to your VSTS Team Project **Administration** dashboard and select the **Service Hooks** tab.

2.	Click the **Add Service Hook** button
	![Step 1](Images\vso-service-hook-step1.png)

3.	Within the New Service Hooks Subscription dialog, select the *MyGet* service and click the *Next* button.
	![Step 2](Images\vso-service-hook-step2.png)

4.	Select the **Code is pushed** event type and the desired **Repository** and **Branch** settings. Click **Next** to continue.
	![Step 3](Images\vso-service-hook-step3.png)

5.	Select the **Trigger a MyGet build** action and provide the target **Feed name** and **Build Source Identifier**.
	![Step 4](Images\vso-service-hook-step4.png)

	<p class="alert alert-info">
		<strong>Note:</strong> You can find the Build Source identifier in your MyGet feed settings > Build Sources, as highlighted below:<br/><br/>
		<img src="Images/vso-service-hook-step4-note.png" alt="Step 4 Note"/>
	</p>

6.	Click **Test** if you want to test this service hook first, or click **Finish** to complete the wizard. The new service hook is now listed as shown below:
	![Step 5](Images\vso-service-hook-step5.png)