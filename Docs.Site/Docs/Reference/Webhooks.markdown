# Webhooks

Every MyGet feed provides the option to communicate with external services, such as a web server, whenever a specific action occurs on the feed. These webhooks can be used to perform actions based on such event, for example sending out a tweet when a package is pushed or updating an issue tracker when a build succeeds.

Only feed owners and co-owners can manage webhooks for a feed. Each webhook can be triggered for one or more event types, depending on the implementation. Webhook deliveries can be inspected, including full logs, as well as redelivered in case this is needed. 

The following events can be subscribed to:

* **Ping** - a communications test sending a simple message to the webhook endpoint
* **Package added** - a package has been added to a feed
* **Package deleted** - a package has been deleted from a feed
* **Package listed/unlisted** - a package has been listed/unlisted
* **Package pinned/unpinned** - a package has been pinned/unpinned
* **Package pushed** - a package has been pushed to an external feed
* **Build started** - a build has started
* **Build finished** - a build has finished

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
		<tr><th><strong>Content-type</strong></th><td><code>application/vnd.myget.webhooks.v1+json</code></td>
		<tr><th><strong>User-Agent</strong></th><td>Will always have the prefix <code>MyGet Web Hook/</code></td>
		<tr><th><strong>X-MyGet-EventIdentifier</strong></th><td>A unique identifier for the event, which can be used to correlate diagnostics data and ensure a webhook payload is not handled more than once.</td>
	</tbody>
</table>

Let's go over some example payloads.

<p class="alert alert-success">
	<strong>Tip:</strong> Webhooks can be tested and debugged using a variety of tools. John Sheehan has an extensive list of them available <a href="http://john-sheehan.com/blog/ultimate-api-webhook-backend-service-debugging-testing-monitoring-and-discovery-tools-list">on his blog</a>.
</p>

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

### Build started

Here's an example of a *build started* payload.

	{
	  "Identifier":"82f9a300-2439-4ac6-a2bd-8da96bb26f75",
	  "Username":"maartenba",
	  "When":"2014-09-08T13:00:10.9006808Z",
	  "PayloadType":"BuildStartedWebHookEventPayloadV1",
	  "Payload":{
	    "FeedIdentifier":"sample-feed",
	    "FeedUrl":"https://www.myget.org/F/sample-feed/",
        "Name":"SampleBuild",
        "Branch":"master"
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
        "Name":"SampleBuild",
        "Branch":"master",
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

Not all event types may be supported by these service-specific webhooks. Some payloads may be optional as well.

### Email webhook

The email webhook sends the event as plain text or HTML to e-mail recipients. It requires a few configuration values:

* **Host** - the SMTP server hostname
* **Port** - the SMTP server port
* **Use TLS?** - whether to use TLS or not
* **SMTP Username** - SMTP server username
* **SMTP password** - SMTP server password
* **From** - sender of the message
* **To** - recipients of the message (separated by semicolon)
* **Body** - whether to send the e-mail containing *raw JSON*, *plain text* or *HTML*

### HipChat webhook

The HipChat webhook sends the event as a notification to a HipChat room. The following configuration options must be provided:

* **AuthToken** - auth token ([Room token](https://hipchat.com/rooms) or [Personal token](https://hipchat.com/account/api))
* **Room** - room to which to send the notifaction
* **Server** - HipChat server URL, defaults to api.hipchat.com
* **Color** - background color for the notification (yellow, green, red, purple, gray or random)
* **Color build events based on status** - will color the message gray when a build is started, green when a build succeeds and red when a build fails
* **Notify participants** - determines if room participants should get a notification for the event or not

### Twilio

The Twilio webhook sends the event as a plain text SMS to a phone. The following configuration options must be provided:

* **AccountSid** - the Twilio account [SID](https://www.twilio.com/user/account)
* **AuthenticationToken** - the Twilio account [authentication token](https://www.twilio.com/user/account)
* **From** - the number sending the message (must be a Twilio number enabled for SMS)
* **To** - the number to which the message will be sent

### Twitter

The Twitter webhook sends the event as a plain text tweet using OAuth credentials.

Before using this webhook, the following steps must be followed:

1. Create a [Twitter application](https://apps.twitter.com/). For the website, please use a URL of your own.
2. Modify the application permissions to *read & write*
3. Copy the API key and API secret and configure the MyGet webhook with it
4. Create an access token and configure the MyGet webhook with it

The following configuration options must be provided:

* **OAuth consumer key** - the OAuth consumer key (API key) obtained from [dev.twitter.com](http://dev.twitter.com)
* **OAuth consumer secret** - the OAuth consumer secret (API secret) obtained from [dev.twitter.com](http://dev.twitter.com)
* **OAuth token** - the OAuth token obtained from [dev.twitter.com](http://dev.twitter.com)
* **OAuth token secret** - the OAuth token secret obtained from [dev.twitter.com](http://dev.twitter.com)