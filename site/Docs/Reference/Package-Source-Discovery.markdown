# NuGet Package Source Discovery Specification

<p class="info">
    <strong>NOTE:</strong> You can contribute or comment on this specification on the GitHub repository at <a href="https://github.com/myget/PackageSourceDiscovery" target="_blank">https://github.com/myget/PackageSourceDiscovery</a>.
</p>

NuGet Package Source Discovery (PSD) allows for NuGet-based client tools to discover the feeds that are hosted by a user, or organization, by using, for instance a blog, any website URL.

NuGet Package Source Discovery is an attempt to remove friction from the following scenarios:

* An individual user may have several NuGet feeds spread across the Internet. Some may be on NuGet.org (including curated feeds), some on MyGet and maybe some on my corporate network. How do I easily point my Visual Studio to all my feeds accross different machines? And how do I maintain this configuration?
* An organization may have several feeds internally, as well as one on MyGet, and some CI packages on TeamCity. How can this organization tell a developer what feeds they can/should use?
* An organization may have a NuGet server containing multiple feeds. How will developers in this organization get a list of available feeds and services?

For all  scenarios, a simple feed discovery mechanism could facilitate this. Such feed discovery mechanism could be any URL out there (even multiple per host).

In order to make MyGet Gallery Feeds more discoverable, the MyGet Team have implemented the Package Source Discovery Specification on the MyGet Gallery page. To see this in action, open Visual Studio and open any solution.  Then, using the Package Manager Console, type the following commands:

    Install-Package DiscoverPackageSources
    Discover-PackageSources -Url "http://www.myget.org/gallery"

Close and re-open Visual Studio and check your package sources. The URL has been verified for a PSD manifest URL and the manifest has been parsed. Matching feeds have been installed into the NuGet.config file, in this case all feeds listed in the MyGet gallery.

## Request

A PSD request is an HTTP GET to a URL with optional authentication and an optional NuGet-ApiKey HTTP Header. There are no filtering or searching options at this time.

## Response

The response will be an XML document following the **Really Simple Discovery** (RSD) RFC as described on [https://github.com/danielberlinger/rsd](https://github.com/danielberlinger/rsd "RSD Specification"). Since not all required metadata can be obtained from the RSD format, the [Dublin Core schema](http://dublincore.org/documents/2012/06/14/dcmi-terms/?v=elements) is present in the PSD response as well.

Vendors and open source projects are allowed to add their own schema to the PSD discovery document however, the manifest described below should be respected at all times.

An example manifest could be:

    <?xml version="1.0" encoding="utf-8"?>
    <rsd version="1.0" xmlns:dc="http://purl.org/dc/elements/1.1/">
	    <service>
		    <engineName>MyGet</engineName>
		    <engineLink>http://www.myget.org</engineLink>
		    <dc:identifier>http://www.myget.org/F/googleanalyticstracker</dc:identifier>
		    <dc:creator>maartenba</dc:creator>
		    <dc:owner>maartenba</dc:owner>
		    <dc:title>Staging feed for GoogleAnalyticsTracker</dc:title>
		    <dc:description>Staging feed for GoogleAnalyticsTracker</dc:description> 
		    <homePageLink>http://www.myget.org/gallery/googleanalyticstracker</homePageLink>
		    <apis>
		    	<api name="nuget-v2-packages" preferred="true" apiLink="http://www.myget.org/F/googleanalyticstracker/api/v2" blogID="" />
		    	<api name="nuget-v2-push" preferred="true" apiLink="http://www.myget.org/F/googleanalyticstracker/api/v2/package" blogID="">
				    <settings>
				      <setting name="apiKey">abcdefghijkl</setting>
				    </settings>
		    	</api>
		    	<api name="nuget-v1-packages" preferred="false" apiLink="http://www.myget.org/F/googleanalyticstracker/api/v1" blogID="" />
		    </apis>
	    </service>
    </rsd>

The following table describes the elements in the data format:

<table class="reference">
<tr>
<th>Element</th>
<th>Description</th>
</tr>
<tr>
<td>engineName</td>
<td>Optional: the software on which the feed is running</td>
</tr>
<tr>
<td>engineLink</td>
<td>Optional: the URL to the website of the software on which the feed is running</td>
</tr>
<tr>
<td><strong>dc:identifier</strong></td>
<td><b>Required</b> - a globally unique URN/URL identifying he feed</td>
</tr>
<tr>
<td>dc:owner</td>
<td>Optional: the owner of the feed</td>
</tr>
<tr>
<td>dc:creator</td>
<td>Optional: the creator of the feed</td>
</tr>
<tr>
<td><strong>dc:title</strong></td>
<td><b>Required</b> - feed name</td>
</tr>
<tr>
<td>dc:description</td>
<td>Optional: feed description</td>
</tr>
<tr>
<td>homePageLink</td>
<td>Optional: link to an HTML representation of the feed</td>
</tr>
<tr>
<td><strong>apis</strong></td>
<td><b>Required</b> - a list of endpoints for the feed</td>
</tr>
<tr>
<td><strong>api name</strong></td>
<td>
<b>Required</b> - the feed protocol used<br/><br/>
Proposed values:<br/>
<ul>
<li>nuget-v1-packages (Orchard)</li>
<li>nuget-v2-packages (current NuGet protocol)</li>
<li>nuget-v2-push (package push URL)</li>
<li>symsrc-v2-push (SymbolSource package indexing URL)</li>
<li>symsrc-v1-symbols (SymbolSource debugging URL)</li>
</ul>
Note that this list can be extended at will (e.g. chocolatey-v1 or proget-v2). Clients can decide which versions they support.
</td>
</tr>
<tr>
<td><strong>api preferred</strong></td>
<td><b>Required</b> - is this the preferred endpoint to communicate with the feed?</td>
</tr>
<tr>
<td><strong>apiLink</strong></td>
<td><b>Required</b> - the GET URL</td>
</tr>
<tr>
<td><strong>blogID</strong></td>
<td><b>Required</b> - for compatibility with RSD spec, not used: leave BLANK</td>
</tr>
</table>

The `<api />` element can contain zero, one or more `<setting name="key">value</setting>` elements specifying settings such as `requireAuthentication`, `apiKey` and vendor-specific settings.

## Server Implementation Guidelines

### Feed Uniqueness

Feed uniqueness must be global. `urn:<guid>` or the feed URL can be valid identifiers.

All feeds should contain an Id.

### Feed Visibility / Access Control

All feeds that the requesting user has read access to should be returned. If the user is anonymous, feeds that require authentication should be omitted.

If the user is logged in, either controlled by basic authentication or using the `NuGet-ApiKey` HttpHeader, the server should return every feed the user has access to as well as feed specific settings such as API keys and so on. The PSD client can use these specifics to preconfigure the NuGet.config file on the user's machine.

## Client or Consumer Implementation Guidelines

The client should respect the following flow of discovering feeds:

* If no PSD is given, the client should assume `http://nuget.<currentdomain>` as the PSD server.
* The PSD URL is accessed and downloaded. It can contain:
  * HTML containing a tag such as 

	```<link rel="nuget" 
          type="application/rsd+xml" 
          title="GoogleAnalyticsTracker feed on MyGet" 
          href="http://myget.org/discovery/feed/googleanalyticstracker"/>```

	This URL should be followed and the PSD manifest parsed. Note that multiple tags may exist and should all be parsed.
	
  * HTML containing a tag such as

	```
    <link rel="nuget" 
    	  type="application/atom+xml" 
    	  title="WebMatrix Package Source" 
    	  href="http://nuget.org/api/v2/curated-feeds/WebMatrix/Packages"/>
	```

	This URL should be treated as a NuGet feed and added as-is, using the title attribute as the feed's title in NuGet package source list. No further metadata can be discovered for this feed. Note that multiple tags may exist.
	
* If the URL directly points to a NuGet Package Source Discovery Manifest, we can immediately parse it.

The client should support entry of a complete PSD URL `<protocol>://<host name>:<port>/<path>`, but require only that the host name be entered. When less than a full URL is entered, the client should verify if the host returns a PSD manifest or contains a `<link rel="nuget"/>` tag.

URLs specified in `<link rel="nuget"/>` tags can be absolute or relative.

Depending on security, consuming an PSD manifest using the `NuGet-ApiKey` header or using basic authentication may yield additional endpoints and API settings. For example, MyGet produces the following manifest on an anonymous call to https://www.myget.org/Discovery/Feed/googleanalyticstracker:

    <rsd version="1.0" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns="http://archipelago.phrasewise.com/rsd">
	    <service>
		    <engineName>MyGet</engineName>
		    <engineLink>http://www.myget.org/</engineLink>
		    <dc:identifier>http://www.myget.org/F/googleanalyticstracker/</dc:identifier>
		    <dc:owner>maartenba</dc:owner>
		    <dc:creator>maartenba</dc:creator>
		    <dc:title>Staging feed for GoogleAnalyticsTracker</dc:title>
		    <dc:description>Staging feed for GoogleAnalyticsTracker</dc:description>
		    <homePageLink>http://www.myget.org/Feed/Details/googleanalyticstracker/</homePageLink>
		    <apis>
		    	<api name="nuget-v2-packages" blogID="" preferred="true" apiLink="http://www.myget.org/F/googleanalyticstracker/" />
		    	<api name="nuget-v1-packages" blogID="" preferred="false" apiLink="http://www.myget.org/F/googleanalyticstracker/api/v1/" />
		    </apis>
	    </service>
    </rsd>

The authenticated version of https://www.myget.org/Discovery/Feed/googleanalyticstracker yields:

    <rsd version="1.0" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns="http://archipelago.phrasewise.com/rsd">
	    <service>
		    <engineName>MyGet</engineName>
		    <engineLink>http://www.myget.org/</engineLink>
		    <dc:identifier>http://www.myget.org/F/googleanalyticstracker/</dc:identifier>
		    <dc:owner>maartenba</dc:owner>
		    <dc:creator>maartenba</dc:creator>
		    <dc:title>Staging feed for GoogleAnalyticsTracker</dc:title>
		    <dc:description>Staging feed for GoogleAnalyticsTracker</dc:description>
		    <homePageLink>http://www.myget.org/Feed/Details/googleanalyticstracker/</homePageLink>
		    <apis>
		    	<api name="nuget-v2-packages" blogID="" preferred="true" apiLink="http://www.myget.org/F/googleanalyticstracker/" />
		    	<api name="nuget-v1-packages" blogID="" preferred="false" apiLink="http://www.myget.org/F/googleanalyticstracker/api/v1/" />
		    	<api name="nuget-v2-push" blogID="" preferred="true" apiLink="http://www.myget.org/F/googleanalyticstracker/">
				    <settings>
				      <setting name="apiKey">530c14a6-6ce5-47d0-8f14-9daab627aa38</setting>
				    </settings>
		    	</api>
			    <api name="symbols-v1-push" blogID="" preferred="true" apiLink="http://nuget.gw.symbolsource.org/MyGet/googleanalyticstracker">
				    <settings>
				      <setting name="apiKey">abcdefghijklmnop</setting>
				    </settings>
			    </api>
		    </apis>
	    </service>		
    </rsd>

## Example Client or Consumer implementation

Example Client / Consumer implementations can be found in the [PackageSourceDiscovery][2] repository.

* [A PowerShell CmdLet implementing the PSD spec][3]
* [A NuGet.exe extension implementing the PSD spec][4]

Note that these sample implementations support:

* Querying HTML containing `<link rel="nuget"/>`
* Querying RSD manifests
* Querying NuGet feeds
* Querying [NuGet Feed Discovery (NFD)][7] manifests

Also [check our Wiki][1] for details on clients implementing this spec already.

## Providers implementing the specification
* [MyGet][6] has several public PSD endpoints
* [Glimpse][8] supports PSD

## License
This repository, including the PSD spec and clients, are licensed under the [Apache v2.0 license][5].

## Comparing PSD with NuGet Feed Discovery (NFD)
Another package source discovery specification exists, [NuGet Feed Discovery (NFD)][7]. NFD differs from PSD in that both specs have a different intent.

* NFD is a convention-based API endpoint for listing feeds on a server
* PSD is a means of discovering feeds from any URL given

If you want more information, read up on [NuGet Feed Discovery (NFD)][7].


[1]: https://github.com/myget/PackageSourceDiscovery/wiki
[2]: https://github.com/myget/PackageSourceDiscovery
[3]: https://github.com/myget/PackageSourceDiscovery/tree/master/src/CmdLet
[4]: https://github.com/myget/PackageSourceDiscovery/tree/master/src/Extension
[5]: https://github.com/myget/PackageSourceDiscovery/blob/master/LICENSE.md
[6]: http://blog.myget.org/post/2013/03/18/Support-for-Package-Source-Discovery-draft.aspx
[7]: http://nugetext.org/nuget-feed-discovery
[8]: http://getglimpse.com
