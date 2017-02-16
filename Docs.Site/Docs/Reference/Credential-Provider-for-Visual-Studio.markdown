# MyGet Credential Provider for Visual Studio 2017

The MyGet Credential Provider for Visual Studio provides an easy way to connect to secured NuGet package sources hosted on MyGet.

It is a Visual Studio extension that relies on [NuGet's credential provider extensibility (VS2017)](http://docs.nuget.org/ndocs/api/credential-providers), so it requires the NuGet Package Manager to be available in your Visual Studio workload.

## Benefits

The benefit of using the MyGet Credential Provider for Visual Studio is three-fold:

* No need to know or learn about `NuGet.Config` files or `nuget.exe` commands to modify them
* No need to use API keys (or access tokens) when working within Visual Studio
* You can authenticate with your MyGet profile (using the identity provider of your choice, or as configured by your MyGet administrator), and remain authenticated for the duration of your Visual Studio session

## Installation

The MyGet Credential Provider for Visual Studio can be installed from within Visual Studio using the Visual Studio Extension Manager, available under *Tools | Extensions and Updates...*.

Alternatively, you can [download the VSIX from the Visual Studio Gallery](https://marketplace.visualstudio.com/vsgallery/79609fc1-58d5-4a31-a171-124b952ca9e0).

<img src="Images/install-VSIX.png" alt="installation" />

<div class="alert alert-block">
  <strong>Note:</strong> The credential provider for Visual Studio 2017 is prerelease and is provided as-is. Please reach out to us if you are having issues with it.
</div>

Note that the credential provider is under development.

## Screenshot

The below screenshot shows the credential provider in action: when NuGet tries to connect to a secured MyGet package source, an OAuth flow is initiated and MyGet will show you a prompt to authenticate against the package source. You can choose to authenticate using any identity provider available (as configured in the target MyGet tenant), or simply provide a username and password for your MyGet account.

<img src="Images/credprovider-screenshot.png" alt="screenshot" />

# MyGet Credential Provider for Visual Studio 2015 ("Experimental")

Why experimental? Because we had to create a custom build version of the NuGet Package Manager extension for Visual Studio 2015.

This custom build is just a few commits newer than the latest official release of the NuGet VSIX and includes the Visual Studio Credential Provider extensibility as documented [here](https://docs.nuget.org/ndocs/api/nuget-credential-providers-for-visual-studio). It would be great to see this commit released as an official build of the NuGet extension, unfortunately Microsoft has not yet shipped an update of NuGet 3.x for Visual Studio 2015.

To install the MyGet Credential Provider for Visual Studio 2015:

* Uninstall the NuGet Package Manager extension from Visual Studio 2015 (*Tools > Extensions and Updates...*, look for *NuGet Package Manager* and hit *Uninstall*)
* Install this custom build of the NuGet Package Manager, available for download on [MyGet](https://www.myget.org/F/credentialproviders/vsix/NuGet.0d421874-a3b2-4f67-b53a-ecfce878063b-3.6.0.2289.vsix) 
* Install the MyGet credential provider for Visual Studio 2015, available for download on [MyGet](https://www.myget.org/F/credentialproviders/vsix/MyGet.CredentialProvider.VS2015.MyGet.13817c70-1be0-4971-8cd7-6a11fb6f4502-1.0.2.54.vsix).

<div class="alert alert-block">
  <strong>Note:</strong> The credential provider for Visual Studio 2015 as well as the custom build of the NuGet Package Manager extension are prerelease and are provided as-is. Please reach out to us if you are having issues with it.
</div>
