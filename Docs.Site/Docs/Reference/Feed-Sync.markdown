# Feed Sync

How can you keep multiple local NuGet servers synchronized? How can developers consume the same packages when each office branch has its own local NuGet server? How can two servers be synchronized when bandwidth is insufficient for a cloud-only solution?

That's where feed sync comes in. Feed Sync, jointly developed by MyGet and [Inedo ProGet](http://inedo.com/proget), allows you to synchronize packages on multiple package servers with MyGet.

![Feed Sync](Images/feed-sync.png)

Packages added on a linked ProGet instance will be replicated to MyGet and any other linked instance. When [using MyGet Build Services to build packages from GitHub, BitBucket or Visual Studio Online](/docs/reference/build-services), packages that are created will be replicated as well.

<p class="alert alert-info">
    <strong>Note:</strong> After release, Feed Sync will be available available for all [paid MyGet plans](https://www.myget.org/plans) and users of ProGet Enterprise 3.5 and up. During preview, Feed Sync is available for all MyGet plans and Inedo ProGet free users.
</p>

Feed Sync makes use of the [Feed State API](/docs/reference/feed-state-api-endpoint).

## Getting Feed Sync details

In order to setup a sync relationship between ProGet and MyGet, the feed sync URL and a MyGet API key will be required. From the feed's _Feed Sync_ tab, this information can be obtained.

![Feed Sync details](Images/feed-sync-details.png)

<p class="alert alert-info">
    <strong>Note:</strong> We recommend using <a href="/docs/reference/security#Personal_security_access_tokens">alternate API keys</a> for every sync relation. This makes it easy to distinguish various local servers that may participate in synchronization and manage to the central MyGet feed.
</p>

## Configuring ProGet Feed Sync with MyGet

In ProGet, add a new feed of the type _MyGet Sync feed_. This is a special type of NuGet/Chocolatey feed that has no connectors and is linked to a single MyGet feed. Packages added to any ProGet instance linked to that MyGet feed will replicate through MyGet and into every other ProGet instance.

Packages added on MyGet, for example by third parties that you grant access or [using MyGet Build Services](/docs/reference/build-services), will be replicated to local ProGet instances as well. Team members could consume replicated packages from ProGet, while partners or road warriors can consume packages directly from MyGet.

![ProGet MyGet feed](Images/feed-sync-proget.png)

<p class="alert alert-info">
    <strong>Note:</strong> The initial sync can take some time if there are a lot of packages to transfer. Click the _view status_ link in the Feed Sync section of the Feed Settings page to view the status.
</p>