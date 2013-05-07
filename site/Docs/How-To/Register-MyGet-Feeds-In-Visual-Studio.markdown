# How can I register a MyGet feed in Visual Studio?

You can register a MyGet feed the same way you register any NuGet package source by using the _Package Manager Settings_ dialog.
You can find it under _Tools > Library Package Manager > Package Manager Settings_ in the Visual Studio menu.

![Register MyGet Feed](Images/faq_register_myget_feed.png)

To store your MyGet feed credentials in your account's roaming profile, you can use the following Gist in combination with the NuGet Commandline tool (<a href="https://nuget.org/nuget.exe" title="Click here to download the NuGet commandline tool">NuGet.exe</a>):

<script src="https://gist.github.com/3205826.js"> </script>

Also check out our blog post dedicated to this topic: <a href="http://blog.myget.org/post/2012/12/12/NuGet-package-restore-from-a-secured-feed.aspx" target="_blank">NuGet package restore from a secured feed</a>.