# Feed endpoints

This page explains the different feed endpoints available on MyGet. Depending on the client you are using (NuGet.exe, Orchard CMS, npm, ...), these endpoints can be configured as the package source or as the publish endpoint.

## Feed endpoints that can be used

Various endpoints are available for MyGet feeds.

### NuGet-compatible feed endpoints

MyGet has the following feed endpoints available for NuGet:

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

### Npm-compatible feed endpoints

MyGet has the following feed endpoints available for npm:

* /F/&lt;your-feed-name&gt;/npm - the npm API endpoint

## Private feed endpoints and authentication

When accessing *private* feeds, these endpoints all require basic authentication. If the NuGet client that is being used does not support basic authentication, a pre-authenticated feed URL can be used.

<p class="alert alert-danger">
    <strong>Warning!</strong> As pre-authenticated feed URLs contain sensitive information, it is recommended to never use pre-authenticated feed URLs unless absolutely needed. Sharing the pre-authenticated feed URL can potentially give others read and write access to the feed!
</p>

Since pre-authenticated feed URLs contain your API key in the URL, we advise to use a <a href="https://www.myget.org/profile/Me#!/AccessTokens">separate access token</a> generated specifically for the purpose of using a pre-authenticated feed endpoint so that it can be revoked at any time.