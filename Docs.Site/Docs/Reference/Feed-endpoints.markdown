# Feed endpoints

This page explains the different feed endpoints available on MyGet. Depending on the client you are using (NuGet.exe, Orchard CMS, ...), these endpoints can be configured as the package source or as the publish endpoint.

MyGet has the following feed endpoints available:

* /F/&lt;your-feed-name&gt; - the NuGet v2 API endpoint
* /F/&lt;your-feed-name&gt;/api/v2 - the NuGet v2 API endpoint for consuming packages
* /F/&lt;your-feed-name&gt;/api/v2/package - the NuGet v2 API endpoint for pushing packages
* /F/&lt;your-feed-name&gt;/api/v1 - the NuGet v1 API endpoint for consuming and pushing packages (still in use by Orchard CMS and some others)

The following table lists which endpoint can be used with which client:

<table class="table table-condensed">
    <thead>
        <tr>
            <td><strong>Endpoint</strong></td>
            <td><strong>NuGet &lt; v1.6</strong></td>
            <td><strong>NuGet &gt; v1.6</strong></td>
            <td><strong>NuGet Package Explorer</strong></td>
            <td><strong>Orchard CMS</strong></td>
            <td><strong>Web browser</strong></td>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>/F/&lt;your-feed-name&gt;</td>
            <td>no</td>
            <td>yes</td>
            <td>yes</td>
            <td>no</td>
            <td>yes</td>
		</tr>
        <tr>
            <td>/F/&lt;your-feed-name&gt;/api/v2</td>
            <td>no</td>
            <td>yes</td>
            <td>yes</td>
            <td>no</td>
            <td>yes</td>
		</tr>
        <tr>
            <td>/F/&lt;your-feed-name&gt;/api/v2/package</td>
            <td>no</td>
            <td>yes</td>
            <td>yes</td>
            <td>no</td>
            <td>no</td>
		</tr>
        <tr>
            <td>/F/&lt;your-feed-name&gt;/api/v1</td>
            <td>yes</td>
            <td>yes (no push)</td>
            <td>yes</td>
            <td>yes</td>
            <td>yes</td>
		</tr>
        <tr>
            <td>/RSS/&lt;your-feed-name&gt;</td>
            <td>no</td>
            <td>no</td>
            <td>no</td>
            <td>no</td>
            <td>yes</td>
		</tr>
    </tbody>
</table>

## Private feed endpoints and authentication

When accessing *private* feeds, these endpoints all require basic authentication. If the NuGet client that is being used does not support basic authentication, a pre-authenticated feed URL can be used.

<p class="alert alert-warning">
    <strong>Warning!</strong> As pre-authenticated feed URLs contain sensitive information, it is recommended to never use pre-authenticated feed URLs unless absolutely needed.
</p>

MyGet has the following pre-authenticated feed endpoints available:

* /F/&lt;your-feed-name&gt;/auth/&lt;your-api-key&gt; - the NuGet v2 API endpoint
* /F/&lt;your-feed-name&gt;/auth/&lt;your-api-key&gt;/api/v2 - the NuGet v2 API endpoint for consuming packages
* /F/&lt;your-feed-name&gt;/auth/&lt;your-api-key&gt;/api/v2/package - the NuGet v2 API endpoint for pushing packages

<p class="alert alert-warning">
    <strong>Warning!</strong> Pre-authenticated feed URLs contain your API key in the URL. It is recommended to not share pre-authenticated feed URLs with others as they expose your credentials.<br /><br />
    We recommend using a <a href="https://www.myget.org/profile/Me#!/AccessTokens">separate access token</a> generated specifically for this purpose.
</p>