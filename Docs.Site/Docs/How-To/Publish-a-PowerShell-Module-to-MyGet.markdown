# Publish a PowerShell Module to MyGet

With PowerShell 5.0 and PowerShellGet, private feeds on MyGet can be used to store private PowerShell Modules. The following script demonstrates how to push a PowerShell Module to a MyGet feed.

	Import-Module PowerShellGet
	$PSGalleryPublishUri = 'https://www.myget.org/F/<feed-name-goes-here>/api/v2/package'
	$PSGallerySourceUri = 'https://www.myget.org/F/<feed-name-goes-here>/api/v2'
	$APIKey = '<api-key-for-MyGet-goes-here'
	
	Register-PSRepository -Name MyGetFeed -SourceLocation $PSGallerySourceUri -PublishLocation $PSGalleryPublishUri
	Publish-Module -Path <path-to-module> -NuGetApiKey $APIKey -Repository MyGetFeed -Verbose

The `Register-PSRepository` [registers](https://technet.microsoft.com/en-us/library/dn807168.aspx) the MyGet feed as a PowerShellGet repository. The `Publish-Module` [publishes](https://technet.microsoft.com/en-us/library/dn807163.aspx) the module to the previously registered PowerShellGet module.

<p class="alert alert-info">
    <strong>Note:</strong> If you want to update the PowerShellGet repository, use the <a href="https://technet.microsoft.com/en-us/library/dn807165.aspx"><code>Set-PSRepository</code></a> CmdLet.
</p>