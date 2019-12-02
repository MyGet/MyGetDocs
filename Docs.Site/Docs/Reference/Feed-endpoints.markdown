# Feed endpoints

This page explains the different feed endpoints available on MyGet. Depending on the client you are using (NuGet.exe, Orchard CMS, npm, pip, rubygems, ...), these endpoints can be configured as the package source or as the publish endpoint.

## Feed endpoint basic structure

MyGet feed endpoints all share the same basic structure, regardless of what package management tool you are using with MyGet. You will need specify your MyGet domain, the name of your feed, and the correct endpoint for the package manager you are using (i.e. NuGet, npm, Python, etc.).

* **Determining your MyGet domain.** If are using a non-enterprise MyGet.org account, your MyGet domain is simply `myget.org`. However, for users on the MyGet Enterprise plan, you will need to specify the custom subdomain used by your company for your MyGet Enterprise instance (i.e. `mycompany.myget.org`). You can find your company's MyGet Enterprise subdomain by copying the URL used to log into your MyGet Enterprise instance from the browser.

### Public or Community feeds

If your MyGet feed is set to *Public* or *Community* access, the feed endpoint structure for your feed will follow the basic format below:

     https://<your_myget_domain>/F/<your-feed-name>/<feed_endpoint>

### Private feeds

If you have set your MyGet feed security settings to *Private*, then you will need to authenticate with MyGet using your username/password or API key to access any feed endpoints.

**Username/password.** the following structure will allow you to access MyGet endpoints to work with your packages from your local machine or CI/CD system using username and password:

     https://<username>:<password>@<your_myget_domain>/F/<your-feed-name>/<feed_endpoint>

**API key.** In the event that you do not use a username/password to authenticate with your MyGet account, you can use a pre-authenticated endpoint URL with an API access key. See the section "Private feed endpoints and authentication" below.
    

## Feed endpoints that can be used

Various endpoints are available for MyGet feeds.

### NuGet-compatible feed endpoints

MyGet has the following feed endpoints available for NuGet:

* /F/&lt;your-feed-name&gt;/api/v3/index.json - the NuGet v3 API endpoint
* /F/&lt;your-feed-name&gt;/api/v2 - the NuGet v2 API endpoint for consuming packages
* /F/&lt;your-feed-name&gt;/api/v2/package - the NuGet v2 API endpoint for pushing packages
* /F/&lt;your-feed-name&gt;/api/v1 - the NuGet v1 API endpoint for consuming and pushing packages (still in use by Orchard CMS and some others)

The following table lists which endpoint can be used with which client:

<table class="table table-condensed">
    <thead>
        <tr>
            <td><strong>Endpoint</strong></td>
            <td><strong>NuGet &lt; v1.6</strong><br />(VS2010)</td>
            <td><strong>NuGet &gt; v1.6</strong><br />(VS2013)</td>
            <td><strong>NuGet &gt; v3.0</strong><br />(VS2015+)</td>
            <td><strong>NuGet Package Explorer</strong></td>
            <td><strong>Orchard CMS</strong></td>
            <td><strong>Chocolatey / OneGet</strong></td>
            <td><strong>Web browser</strong></td>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>/F/&lt;your-feed-name&gt;/api/v3/index.json</td>
            <td>no</td>
            <td>no</td>
            <td>yes</td>
            <td>no</td>
            <td>no</td>
            <td>no</td>
            <td>yes</td>
		</tr>
        <tr>
            <td>/F/&lt;your-feed-name&gt;/api/v2</td>
            <td>no</td>
            <td>yes</td>
            <td>yes</td>
            <td>yes</td>
            <td>no</td>
            <td>yes</td>
            <td>yes</td>
		</tr>
        <tr>
            <td>/F/&lt;your-feed-name&gt;/api/v2/package (push)</td>
            <td>no</td>
            <td>yes</td>
            <td>yes</td>
            <td>yes</td>
            <td>no</td>
            <td>yes</td>
            <td>no</td>
		</tr>
        <tr>
            <td>/F/&lt;your-feed-name&gt;/api/v1</td>
            <td>yes</td>
            <td>yes (no push)</td>
            <td>yes</td>
            <td>yes</td>
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
            <td>no</td>
            <td>no</td>
            <td>yes</td>
		</tr>
    </tbody>
</table>

### Symbol server endpoints

MyGet has the following endpoints available for symbol server (debugging in Visual Studio and WinDbg):

* /F/&lt;your-feed-name&gt;/api/v2/package - the symbols package publish endpoint
* /F/&lt;your-feed-name&gt;/symbols - the symbol server endpoint

### Npm-compatible feed endpoints

MyGet has the following feed endpoints available for npm:

* /F/&lt;your-feed-name&gt;/npm - the npm API endpoint

### Bower-compatible feed endpoints

MyGet has the following feed endpoints available for Bower:

* /F/&lt;your-feed-name&gt;/bower - the Bower API endpoint

### Vsix-compatible feed endpoints

MyGet has the following feed endpoints available for Vsix (Visual Studio extensions):

* /F/&lt;your-feed-name&gt;/vsix - the VSIX Atom feed API endpoint
* /F/&lt;your-feed-name&gt;/vsix/package - the VSIX upload endpoint

### PHP Composer-compatible feed endpoints

MyGet has the following feed endpoints available for PHP Composer packages:

* /F/&lt;your-feed-name&gt;/composer - the PHP Composer registry API endpoint
* /F/&lt;your-feed-name&gt;/composer/dist/lt;packageid&gt;/lt;packageversion&gt;.zip - the PHP Composer registry API endpoint to which a binary can be uploaded using `HTTP POST`

### Python (PyPI)-compatible feed endpoints

MyGet supports the following feed endpoints for interacting with Python PyPI and .whl packages on MyGet:

* /F/&lt;your-feed-name&gt;/python - the MyGet API endpoint to install Python packages using tools like `pip`
* /F/&lt;your_feed_name&gt;/python/upload - the MyGet API endpoint to upload binary .whl packages using `HTTP POST` or Twine 
	
### RubyGems-compatible feed endpoints

MyGet supports the following feed endpoints for interacting with Ruby gems on MyGet:

* /F/&lt;your_feed_name&gt;/geminstall - the MyGet feeds endpoint to install gems using tools like `rubygems`
* /F/&lt;your_feed_name&gt;/gem/upload - the MyGet feeds endpoint to upload gems from the command line or your build system using `bundler` or `HTTP POST`

## Private feed endpoints and authentication

When accessing *private* feeds, these endpoints all require basic authentication. If the client that is being used does not support basic authentication, a pre-authenticated feed URL can be used. The pre-authenticated feed URL is available from the feed's *feed details* page.

<p class="alert alert-danger">
    <strong>Warning!</strong> As pre-authenticated feed URLs contain sensitive information, it is recommended to never use pre-authenticated feed URLs unless absolutely needed. Sharing the pre-authenticated feed URL can potentially give others read and write access to the feed!
</p>

Since pre-authenticated feed URLs contain your API key in the URL, we advise to use a <a href="https://www.myget.org/profile/Me#!/AccessTokens">separate access token</a> generated specifically for the purpose of using a pre-authenticated feed endpoint so that it can be revoked at any time.
