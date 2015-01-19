# Feed State API Endpoint

Next to the [Feed Endpoints](/docs/reference/feed-endpoints) provided by MyGet, a feed state API endpoint is also available. 

The feed state API endpoint provides a description of:

* the current feed state (a complete list of all packages and their versions)
* a list of updates to the feed, returning an incremental view of changes since a specific timestamp

The feed state API endpoint is used by MyGet's Feed Sync. In this document, we'll go over the various characteristics of the feed state API endpoint.

## URL of the Feed State API

The feed state API endpoint URL is formatted as follows:

	https://www.myget.org/F/{feedname}/api/v2/feed-state

The endpoint is secured using the NuGet API key and its header:

	X-NuGet-ApiKey: {MyGetFeedApiKey}

See [access tokens](/docs/reference/security#Personal_security_access_tokens) for more information.

## Get complete feed state

To retrieve complete feed state, issue an HTTP `GET` to the feed state API endpoint URL.

	GET {feedstateurl}

The server will respond with the a very simple JSON document that describes the packages currently available on the feed. For example:

	Content-Type: application/json
	
	{
		_date: "401939109",
		packages: [{
			packagetype: "nuget",
			id: "my.package",
			versions: ["1.0","1.1"],
			dates: ["10008010","38192014"] }]
	}

The response is a single JSON object with a `packages` property containing an array of `package` objects. Each `package` object has a `packagetype` property, an `id` property and two arrays, `versions` and `dates`. The above sample response lists the following packages are available on the feed:

* my.package 1.0, updated 10008010 (NuGet package)
* my.package 1.1, updated 38192014 (NuGet package)

The timestamps are .NET `DateTime` ticks in UTC. Note date values can be null.

Possible HTTP status codes returned:

* A `200` will be returned when the request succeeded and a response is returned.
* A `404` will be returned when no feed with the name in the base URL exists.
* A `403` will be returned if the api key used has no write access to the feed.
* A `409` will be returned if the feed's subscription has expired.

## Get updated feed state

To retrieve updated feed state, issue an HTTP `GET` to the feed state API endpoint URL including the `since` URL parameter:

	GET {feedstateurl}?since={lastupdate}

The `lastupdate` value represents .NET `DateTime` ticks in UTC and ensures the response only includes package updates that have occurred after this date (packages added/changed/deleted). For example:

Content-Type: application/json

	{
		_date: "401939109",
		packages: [{
			packagetype: "nuget",
			id: "my.package",
			versions: ["1.0"],
			dates: ["10008010"] }],
		deleted: [{
			packagetype: "nuget",
			id: "my.deleted.package",
			versions: ["1.0"]
		}]
	}

The response is a single JSON object with a `packages` property containing an array of `package` objects. Each `package` object has a `packagetype` property, an `id` property and two arrays, `versions` and `dates`. Next to the `packages` property, a `deleted` property contains an array of `package` objects that have a `packagetype` property, an `id` property and a `dates` array.

The above sample response lists the following packages are available on the feed:

* my.package 1.0, added or updated 10008010 (NuGet package)
* my.deleted.package 1.0, deleted (NuGet package)

The timestamps are .NET `DateTime` ticks in UTC. Note date values can be null.

Possible HTTP status codes returned:

* A `200` will be returned when the request succeeded and a response is returned.
* A `404` will be returned when no feed with the name in the base URL exists.
* A `403` will be returned if the api key used has no write access to the feed.
* A `409` will be returned if the feed's subscription has expired.
* A `412` will be returned when since date is older than one month.