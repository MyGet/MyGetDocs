# MyGet 2014.1 Release Notes

MyGet 2014.1 was released on February 27, 2014.

## Features

### MyGet
* Updated documentation website
* Support for NuGet 2.8
* Username is now added to password reset e-mails
* Packages mirrored from proxied package sources can be unlisted in the resulting feed
* [Support for multiple API keys](http://blog.myget.org/post/2013/10/23/Making-your-life-easier-with-multiple-access-tokens.aspx)
* [Visibility into which package source serves a specific package](http://blog.myget.org/post/2014/02/27/Where-does-this-package-come-from.aspx)
* Packages can be owned by more than one MyGet user
* Automatic updates of upstream packages
* [Labeling sources when pushing upstream](http://blog.myget.org/post/2013/12/18/Labeling-Sources-when-Pushing-to-NuGetorg.aspx)
* RSS feed of activity on your MyGet feed

### MyGet Enterprise
* [Enhancements to MyGet Gallery for Enterprise subscriptions](http://blog.myget.org/post/2014/02/17/Enhancements-to-MyGet-Gallery-for-Enterprise-subscriptions.aspx)
* Organization specific feeds are now visible to all users that are logged in

### MyGet Build Services
* Support for NuGet 2.8
* Support for MSBuild 2013
* Configure which project(s) should be built
* [Configure which packages are pushed to your feed after build](http://blog.myget.org/post/2014/02/24/Which-packages-are-added-to-a-feed-during-build.aspx)
* Configure build targets
* Specify default package source and default push source
* [Configure API keys for package sources as well](http://blog.myget.org/post/2014/01/13/Publishing-packages-to-NuGetorg-during-build.aspx)
* Current feed is added as a package source
* Build hooks only trigger build when branch matches
* Support for git submodules that require authentication
* [GitHub Commit Status API integration](http://blog.myget.org/post/2013/10/14/GitHub-Commit-Status-API-now-supported.aspx)
* [Labeling sources after build](http://blog.myget.org/post/2013/10/17/Labeling-Sources-after-Build.aspx)
* [Build status badges](http://blog.myget.org/post/2014/01/15/Build-Status-Badges.aspx)

## Bug Fixes
* Packages downloaded through the browser now have a .nupkg file extension
* NuGet Package Explorer 2.8 publishing works again
* Package restore with proxied feeds now works on feeds larger than 100 packages
* Load time of activity feeds has been improved
* Push upstream now works with private feeds

Next to all these, we have done a tremendous effort on our back-end: upgrade to the latest Windows Azure SDK and switch to JSON-based traffic to our storage accounts, a new queuing framework which increases back-end messaging throughput, ...

_Happy packaging!_
