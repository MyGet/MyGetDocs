# Debugging, source code and symbols for NuGet packages

MyGet symbols support lets consumers of our NuGet packages step through the source code and integrate with Visual Studio and tools like WinDbg.

MyGet comes with its own symbol server but also provides integration with [SymbolSource.org](/docs/reference/symbolsource).

## Pushing symbols packages to MyGet

With [NuGet.org](http://www.nuget.org), the NuGet client automatically recognizes symbols packages and pushes them to the default SymbolSource feed. Since we want to ensure our packages end up on our own feed and securely host debugger symbols, we must explicitly push symbols to the MyGet symbol server.

The publish workflow to publish the SamplePackage.1.0.0.nupkg to a MyGet feed, including symbols, would be issuing the following two commands from the console (replace the GUID with your MyGet API key):

	nuget push SamplePackage.1.0.0.nupkg 00000000-0000-0000-0000-00000000000 -Source http://www.myget.org/F/somefeed/api/v2/package
	nuget push SamplePackage.1.0.0.Symbols.nupkg 00000000-0000-0000-0000-00000000000 -Source http://www.myget.org/F/somefeed/api/v2/package

## Consuming symbol packages in Visual Studio

To debug a NuGet package for which symbols are available, we will need the symbols URL for use in Visual Studio. After logging in to MyGet, we can find this URL under the *Feed Details* tab of our feed.

![SymbolServer URL in MyGet feed settings](Images/feedsettings_symbols_url.png)

<p class="alert alert-success">
    <strong>Tip:</strong> MyGet provides two symbol server URLs: one that requires authentication and one that contains an authentication token in the URL. The first will prompt for credentials when used (Visual Studio 2015 and beyond). The latter one will not, as it contains an authentication token. It is recommended to keep the symbols URL to yourself at all time: it's a personal URL in which security information is embedded in the form of a guid. If for some reason this gets compromised, please <a href="https://www.myget.org/Support">contact support</a> and ask for an API key reset.
</p>

Visual Studio typically will only debug our own source code, the source code of the project or projects that are currently opened in Visual Studio. To disable this behavior and to instruct Visual Studio to also try to debug code other than the projects that are currently opened, use the ***Tools | Options*** menu and find the *Debugging* node. COnfigure the following options:

* *Enable Just My Code* should be disabled.
* *Enable source server support* should be enabled. This may trigger a warning message but it is safe to just click *Yes* and continue with the settings specified.

Keep the Options dialog open and find the ***Debugging | Symbols*** node on the left. In the dialog shown, add the symbol server URL for your MyGet feed, for example `http://www.myget.org/F/somefeed/auth/11111111-1111-1111-1111-11111111111/symbols`.

![Visual Studio symbol server settings](Images/debug-options-2015.png)

## Browsing symbols in MyGet

The package details page for a package that has symbols available comes with a nice utility that enables us to browse the source code embedded in a symbols package.

![Browse NuGet source code on MyGet](Images/browse-sources.png)

## Quick command cheatsheet

Here's a quick cheatsheet of the commands related to symbol feeds:

* Create package and symbols

	```nuget.exe pack <path_to_project_or_nuspec> -symbols```

* Pushing a package to MyGet:

	```nuget.exe push <package-file> <myget-key> -Source https://www.myget.org/F/<feed-name>/api/v2/package```

* Pushing a symbols package to MyGet:

	```nuget.exe push <package-file> <myget-key> -Source https://www.myget.org/F/<feed-name>/api/v2/package```

## Troubleshooting

The following list of tips might be useful to you if you hit any issues when configuring the debugger. If you have some other tips to share, contact MyGet support or submit a pull request for this page.

<p class="alert alert-info">
    <strong>Note:</strong> MyGet does not index any binaries found in the package's <code>\tools</code> folder.
</p>

### A symbols package was pushed but does not provide source stepping

The way Visual Studio and other debugging tools match an assembly and PDB file is by using the assembly hash. This hash is stored in the `.dll` and `.pdb` file and must match for debugging and source steping to work.

On the package details page on MyGet, we can verify if for a given assembly debugging and source stepping is possible. MyGet shows the assembly name, the assembly hash and whether source stepping will be available for it or not.

![Does the assembly hash match](Images/assembly-pdb-match.png)

<p class="alert alert-info">
    <strong>Note:</strong> To verify this match when creating packages on our system, we can use the <a href="http://www.debuginfo.com/tools/chkmatch.html">ChkMatch</a> tool. The author of this tool also provides <a href="http://www.debuginfo.com/articles/debuginfomatch.html">a comprehensive article about matching assemblies and symbols</a>.
</p>

### Veryifying symbols package contents

There is a useful plug-in for [NuGet Package Explorer](http://npe.codeplex.com) which allows us to validate our symbols packages.

To install the plug-in, open NuGet Package Explorer and use the ***Tools | Plugin Manager...*** menu. In the dialog that opens, click the ***Add Feed Plugin...*** button double-click the SymbolSource plug-in from the MyGet feed.

![Installing the SymbolSource Plugin in NuGet Package Explorer](Images/npe_plugins_symbolsource.png)

This plugin enhances the package analysis tools with additional rules that should help detect 99% of the problems with symbols packages.

Once installed, open a symbols package and validate its contents before pushing it to SymbolSource by selecting `Tools > Analyze Package` or hit `CTRL-Q`.

A common root cause for symbols missing in the symbols package originates from a too restrictive `.nuspec` file. The one below will filter out all non-DLL files from the package.

```<file src="C:\src\AwesomeLib\bin\Release\AwesomeLib.dll" target="lib\net45" />```

If you have a nuspec file which contains a similar line as the one above, you might want to change it to the following:

```<file src="C:\src\AwesomeLib\bin\Release\AwesomeLib.*" target="lib\net45" />```

The NuGet client tools are smart enough to filter out PDB files from non-symbols packages (unless you explicitly include them).
