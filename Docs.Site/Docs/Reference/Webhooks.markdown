# Webhooks

Every MyGet feed provides the option to communicate with external services, such as a web server, whenever a specific action occurs on the feed. These webhooks can be used to perform actions based on such event, for example sending out a Tweet when a package is pushed or updating an issue tracker when a build succeeds.

Only feed owners and co-owners can manage webhooks for a feed. Each webhook can be triggered for one or more event types, depending on the implementation. Webhook deliveries can be inspected, including full logs, as well as redelivered in case this is needed. 

The following events can be subscribed to:

* **Ping** - a communications test sending a simple message to the webhook endpoint
* **Package added** - a package has been added to a feed
* **Package deleted** - a package has been deleted from a feed
* **Package listed/unlisted** - a package has been listed/unlisted
* **Package pinned/unpinned** - a package has been pinned/unpinned
* **Package pushed** - a package has been pushed to an external feed
* **Build finished** - a build has finished

<p class="alert alert-info">
	<strong>Note:</strong> Webhooks are currently a preview feature.
</p>

## HTTP Post webhook

The most basic type of webhook implementation is our HTTP Post webhook. It sends raw event data, formatted as a JSON document, as an HTTP POST to a configured URL.

MyGet will make every HTTP request with the following headers:

<table class="table table-condensed">
	<thead>
    	<tr>
    	    <th>Header</th> 
			<th>Description</th>
    	</tr>
	</thead>
	<tbody>
		<tr><th><strong>Content-type</strong></th><td><code>application/vnd.myget.webhooks.v1.preview+json</code></td>
		<tr><th><strong>User-Agent</strong></th><td>Will always have the prefix <code>MyGet Web Hook/</code></td>
		<tr><th><strong>X-MyGet-EventIdentifier</strong></th><td>A unique identifier for the event, which can be used to correlate diagnostics data and ensure a webhook payload is not handled more than once.</td>
	</tbody>
</table>

Let's go over some example payloads.

### Ping

Here's an example of a *ping* payload:

	{
	  "Identifier":"f83b6de3-9476-43b0-9f75-f9bf478539ca",
	  "Username":"maartenba",
	  "When":"2014-09-08T13:06:25.8446143Z",
	  "PayloadType":"PingWebHookEventPayloadV1",
	  "Payload":{
	  }
	}

### Package added

Here's an example of a *package added* payload. It includes package metadata.

It's important to know the ```PackageVersion``` property may not contain a valid version number but instead contains a string specifying the version constraint. It may contain characters like &ge;, &le;, &gt;, &lt;, = and so on.

	{
	  "Identifier":"7a48ce8c-728d-4350-8da8-e283d6fce277",
	  "Username":"maartenba",
	  "When":"2014-09-08T12:59:53.1148196Z",
	  "PayloadType":"PackageAddedWebHookEventPayloadV1",
	  "Payload":{
	    "PackageIdentifier":"GoogleAnalyticsTracker.WP8",
	    "PackageVersion":"1.0.0-CI00002",
	    "PackageDetailsUrl":"https://www.myget.org/feed/sample-feed/package/GoogleAnalyticsTracker.WP8/1.0.0-CI00002",
	    "PackageDownloadUrl":"https://www.myget.org/F/sample-feed/api/v2/package/GoogleAnalyticsTracker.WP8/1.0.0-CI00002",
	    "PackageMetadata":{
	      "IconUrl":"/Content/images/packageDefaultIcon.png",
	      "Size":6158,
	      "Authors":"Maarten Balliauw",
	      "Description":"GoogleAnalyticsTracker was created to have a means of tracking specific URL's directly from C#. For example, it enables you to log API calls to Google Analytics.",
	      "LicenseUrl":"http://github.com/maartenba/GoogleAnalyticsTracker/blob/master/LICENSE.md",
	      "LicenseNames":"MS-PL",
	      "ProjectUrl":"http://github.com/maartenba/GoogleAnalyticsTracker",
	      "Tags":"google analytics ga wp8 wp7 windows phone windowsphone api rest client tracker stats statistics mango",
	      "MinClientVersion":null,
	      "ReleaseNotes":null,
	      "Dependencies":[
	        {
	          "PackageIdentifier":"GoogleAnalyticsTracker.Core",
	          "PackageVersion":"(>= 2.0.5364.25176)",
	          "TargetFramework":".NETFramework,Version=v4.0.0.0"
	        }]
	    },
	    "FeedIdentifier":"sample-feed",
	    "FeedUrl":"https://www.myget.org/F/sample-feed/"
	  }
	}

### Package deleted

Here's an example of a *package deleted* payload.

	{
	  "Identifier":"de5358b5-2fea-4000-b59e-7345e14af0ca",
	  "Username":"maartenba",
	  "When":"2014-09-08T13:00:52.0300822Z",
	  "PayloadType":"PackageDeletedWebHookEventPayloadV1",
	  "Payload":{
	    "PackageIdentifier":"GoogleAnalyticsTracker.Core",
	    "PackageVersion":"1.0.0-CI00002",
	    "FeedIdentifier":"sample-feed",
	    "FeedUrl":"https://www.myget.org/F/sample-feed/"
	  }
	}

### Package listed/unlisted

Here's an example of a *package listed/unlisted* payload. The ```Action``` will contain ```listed``` or ```unlisted```.
	
	{
	  "Identifier":"76919dd9-ba4f-4f11-ab4d-d983146d2aae",
	  "Username":"maartenba",
	  "When":"2014-09-08T13:07:33.6121841Z",
	  "PayloadType":"PackageListedWebHookEventPayloadV1",
	  "Payload":{
	    "Action":"unlisted",
	    "PackageIdentifier":"GoogleAnalyticsTracker.Simple",
	    "PackageVersion":"1.0.0-CI00002",
	    "FeedIdentifier":"sample-feed",
	    "FeedUrl":"https://www.myget.org/F/sample-feed/"
	  }
	}

### Package pinned/unpinned

Here's an example of a *package pinned/unpinned* payload. The ```Action``` will contain ```pinned``` or ```unpinned```.
	
	{
	  "Identifier":"3374190e-33ed-4546-a1c1-d47e19b1980f",
	  "Username":"maartenba",
	  "When":"2014-09-08T13:07:43.1294196Z",
	  "PayloadType":"PackagePinnedWebHookEventPayloadV1",
	  "Payload":{
	    "Action":"pinned",
	    "PackageIdentifier":"GoogleAnalyticsTracker.Simple",
	    "PackageVersion":"1.0.0-CI00002",
	    "FeedIdentifier":"sample-feed",
	    "FeedUrl":"https://www.myget.org/F/sample-feed/"
	  }
	}

### Package pushed

Here's an example of a *package pushed* payload. It includes package metadata.
	
	{
	  "Identifier":"0ebf1e5d-2dc4-4576-8246-aacda950142d",
	  "Username":"maartenba",
	  "When":"2014-09-08T13:02:46.6949583Z",
	  "PayloadType":"PackagePushedWebHookEventPayloadV1",
	  "Payload":{
	    "PackageIdentifier":"GoogleAnalyticsTracker.Simple",
	    "PackageVersion":"1.0.0-CI00002",
	    "PackageDetailsUrl":"https://www.myget.org/feed/sample-feed/package/GoogleAnalyticsTracker.Simple/1.0.0-CI00002",
	    "PackageDownloadUrl":"https://www.myget.org/F/sample-feed/api/v2/package/GoogleAnalyticsTracker.Simple/1.0.0-CI00002",
	    "PackageMetadata":{
	      "IconUrl":"/Content/images/packageDefaultIcon.png",
	      "Size":5542,
	      "Authors":"Maarten Balliauw",
	      "Description":"GoogleAnalyticsTracker was created to have a means of tracking specific URL's directly from C#. For example, it enables you to log API calls to Google Analytics.",
	      "LicenseUrl":"http://github.com/maartenba/GoogleAnalyticsTracker/blob/master/LICENSE.md",
	      "LicenseNames":"MS-PL",
	      "ProjectUrl":"http://github.com/maartenba/GoogleAnalyticsTracker",
	      "Tags":"google analytics ga mvc api rest client tracker stats statistics",
	      "MinClientVersion":null,
	      "ReleaseNotes":null,
	      "Dependencies":[
	        {
	          "PackageIdentifier":"GoogleAnalyticsTracker.Core",
	          "PackageVersion":"(? 2.0.5364.25176)",
	          "TargetFramework":".NETFramework,Version=v4.0.0.0"
	        }]
	    },
	    "TargetPackageSourceName":"Other Feed",
	    "TargetPackageSourceUrl":"https://www.myget.org/F/other-feed/",
	    "FeedIdentifier":"sample-feed",
	    "FeedUrl":"https://www.myget.org/F/sample-feed/"
	  }
	}

### Build finished

Here's an example of a *build finished* payload. The ```Result``` will contain ```success``` or ```failed```. It includes the packages that were added to the feed.

	{
	  "Identifier":"82f9a300-2439-4ac6-a2bd-8da96bb26f75",
	  "Username":"maartenba",
	  "When":"2014-09-08T13:00:10.9006808Z",
	  "PayloadType":"BuildFinishedWebHookEventPayloadV1",
	  "Payload":{
	    "Result":"success",
	    "FeedIdentifier":"sample-feed",
	    "FeedUrl":"https://www.myget.org/F/sample-feed/",
	    "BuildLogUrl":"https://www.myget.org/BuildSource/List/sample-feed#d510be3d-7803-43cc-8d15-e327ba999ba7",
	    "Packages":[
	      {
	        "PackageIdentifier":"GoogleAnalyticsTracker.Core",
	        "PackageVersion":"1.0.0-CI00002",
	        "TargetFramework":null
	      },
	      {
	        "PackageIdentifier":"GoogleAnalyticsTracker.MVC4",
	        "PackageVersion":"1.0.0-CI00002",
	        "TargetFramework":null
	      }]
	  }
	}

## Service-specific webhooks

MyGet provides several service-specific webhooks:

* **Email** - sends the event as plain text or HTML to e-mail recipients
* **HipChat** - sends the event as a notification to a HipChat room
* **Twilio** - sends the event as a plain text SMS to a phone
* **Twitter** - sends the event as a plain text tweet
