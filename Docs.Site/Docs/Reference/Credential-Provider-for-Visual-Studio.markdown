# MyGet Credential Provider for Visual Studio 2017

The MyGet Credential Provider for Visual Studio provides an easy way to connect to secured NuGet package sources hosted on MyGet.

It is a Visual Studio extension that relies on [NuGet's credential provider extensibility](http://docs.nuget.org/ndocs/api/credential-providers), so it requires the NuGet Package Manager to be available in your Visual Studio workload.

## Benefits
The benefit of using the MyGet Credential Provider for Visual Studio is three-fold:

* No need to know or learn about NuGet.Config files or nuget.exe commands to modify them
* No need to use API keys (or access tokens) when working within Visual Studio
* You can authenticate with your MyGet profile (using the identity provider of your choice, or as configured by your MyGet administrator), and remain authenticated for the duration of your Visual Studio session

## Installation
The MyGet Credential Provider for Visual Studio can be installed from within Visual Studio using the Visual Studio Extension Manager, available under *Tools > Extensions and Updates...*.

Alternatively, you can [download the VSIX from the Visual Studio Gallery](https://marketplace.visualstudio.com/vsgallery/79609fc1-58d5-4a31-a171-124b952ca9e0).
