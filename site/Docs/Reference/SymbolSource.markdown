# SymbolSource

MyGet integrates with [SymbolSource](http//www.SymbolSource.org) to host debugging symbols for your NuGet packages. Let consumers of your NuGet packages step through the source code and integrate with Visual Studio. 

## Pushing packages to SymbolSource

With [NuGet.org](http://www.nuget.org), the NuGet client automatically recognizes symbols packages and pushes them to the default SymbolSource feed. MyGet uses a different feed on SymbolSource, making it possible to securely host your symbols packages. This does imply that pushing symbols to the MyGet symbol server consists of two steps instead of one.

The publish workflow to publish the SamplePackage.1.0.0.nupkg to a MyGet feed, including symbols, would be issuing the following two commands from the console:

```
nuget push SamplePackage.1.0.0.nupkg 00000000-0000-0000-0000-00000000000 -Source http://www.myget.org/F/somefeed/api/v1 
nuget push SamplePackage.1.0.0.Symbols.nupkg 00000000-0000-0000-0000-00000000000 -Source http://nuget.gw.SymbolSource.org/MyGet/somefeed
```

<p class="info">
    <strong>Note:</strong> MyGet and SymbolSource share the feed's API key which can be found on the feed's details page. The same is true for passwords. If you modify your [MyGet profile](https://www.myget.org/profile/Me#!/Edit) and update the password, you'll be able to [log in to SymbolSource](https://www.SymbolSource.org/MyGet/Account/LogIn) as well.
</p>

## Consuming symbol packages in Visual Studio

When logging in to MyGet, you can find the symbols URL compatible with Visual Studio under the Feed Details tab for your MyGet feed. This URL will be the same for all feeds you are allowed to consume, so no need to configure 10+ symbol servers in Visual Studio. Here’s how to configure it.

First of all, Visual Studio typically will only debug your own source code, the source code of the project or projects that are currently opened in Visual Studio. To disable this behavior and to instruct Visual Studio to also try to debug code other than the projects that are currently opened, open the Options dialog (under the menu Tools > Options). Find the Debugging node on the left and click the General node underneath. Turn off the option Enable Just My Code. Also turn on the option Enable source server support. This usually triggers a warning message but it is safe to just click Yes and continue with the settings specified.

![Visual Studio symbol server settings](Images/debug-options.png)

Keep the Options dialog opened and find the Symbols node under the Debugging node on the left. In the dialog shown in Figure 4-14, add the symbol server URL for your MyGet feed: http://srv.SymbolSource.org/pdb/MyGet/username/11111111-1111-1111-1111-11111111111. After that, click OK to confirm configuration changes and consume symbols for NuGet packages.

<p class="info">
    <strong>Note:</strong> While the API key and user password for MyGet and SymbolSource are shared, it is not possible to trigger authentication for a symbols URL inside Visual Studio as it has no support for authentication. Hence it is recommended to keep the symbols URL to yourself at all time: it's a personal URL in which security information is embedded inthe form of a guid. If for some reason this gets compromised, please [contact support](https://www.myget.org/Support) and ask for a SymbolSource URL reset.
</p>

## Security

Each MyGet user and feed automatically gets its counterpart on SymbolSource, so you don't need to do anything apart from checking out the feed details page to discover what your push URL for symbol packages is and how to configure Visual Studio to download PDBs from your repositories. Your account's API key will enable you to push to both MyGet and SymbolSource, just as with NuGet.

MyGet keeps the following in sync with SymbolSource:
* Users
* User API keys
* User passwords
* Feeds
* Feed permissions

This means that if you give a user access to a feed you own, the same persmission set will apply to SymbolSource. If a user gets read-only access to a feed, he will also get read-only access to the symbols. When permissions for a user are revoked on MyGet, they will also be revoked on SymbolSource.

## Quick command cheatsheet

Here's a quick cheatsheet of the commands related to symbol feeds:

* Storing your nuget.org key, which also enables pushing to symbolsource.org:
```nuget.exe setapikey <nuget-key>```

* Storing your myget.org key:
```nuget.exe setapikey <myget-key> -Source https://www.myget.org/F/<feed-name>```

* Storing your myget.org key for symbolsource.org (this one you need to do explicitly):
```nuget.exe setapikey <myget-key> -Source https://nuget.gw.symbolsource.org/MyGet/<feed-name>```

* Pushing a package to nuget.org (a symbol package will be detected and pushed to symbolsource.org automatically):
``nuget.exe push <package-file>```

* Pushing a symbol package to symbolsource.org explicitly (if you want to test it first):
```nuget.exe push <package-file> -Source https://nuget.gw.symbolsource.org/Public/NuGet```

* Pushing a package to myget.org:
```nuget.exe push <package-file> -Source https://nuget.gw.symbolsource.org/MyGet/<feed-name>```

* Pushing a symbol package to symbolsource.org:
```nuget.exe push <package-file> -Source https://nuget.gw.symbolsource.org/MyGet/<feed-name>```