# MyGet 1.9.5 Release Notes

MyGet 1.9.5 was released on November 18, 2014.

## Major Features
It's been a while since we tagged another MyGet release, but here we are, 9 months after tagging v1.9.0.
We constantly ship and deploy improvements to our service so this v1.9.5 basically aggregates everything we've done since then, combined into a single milestone.
This release contains many exciting new features. We'd love to hear from you so [please send us your feedback][9]!

* [Visual Studio Online integration][4]
* [MyGet now offered through the Microsoft Azure store!][16] (use your MSDN!)
* [Package mirroring is now enabled by default][0]
* [Build Status Badges][1]
* [MyGet Web Hooks][7] (see how we've built [a custom package retention rules sample][18])
* [Package License Analysis][8]
* [Package Update Notifications][10]
* [Redesigned MyGet Documentation site][12]
* [MyGet Feeds now support Target Framework Filtering][15]

### MyGet
* Improved status/error message when contributor fails pushing to a private feed on expired subscription plan
* [Add package from feed dialog now allows specifying Lowest/Highest/HighestPatch setting for dependency resolution][5]
* Support pre-authenticated feeds for clients that don't support basic authentication
* Support Visual Studio Online as a package source
* [Improved RSS feed-endpoint][11] allowing you to receive feed activity notifications. Also enabled RSS auto-discovery on feed pages on the MyGet web site to facilitate detection of the RSS endpoint in the browser.
* [Configure a feed's Report Abuse URL][19]

### MyGet Enterprise
* [Administrators can now configure password policies and account lockout settings][6]
* [Administrators can now configure IP white-listing for the Enterprise tenant][20]
* Administrators can now create users directly from within the administrative dashboard
* Administrators are now able to join feeds even when feed contributor quota is reached
* VSO, GitHub, CodePlex and BitBucket integrations are now also available for Enterprise tenants

### MyGet Build Services
* [Support for user-defined variables][17]
* [Built-in Support for GitVersion][2]
* [Add ability to push packages upstream directly from build artifacts][3]
* [Allow specifying which projects get built][13]
* [Configurable default upstream package source to push built artifacts to][14]
* Added support for XUnit 1.9.2
* Add ```.fsproj``` support for building and packaging F# projects
* Add warning in build log when detecting NuGet manifests named ```projectName.ext.nuspec``` (where ```ext``` is a known file extension such as ```csproj```, ```dll```, etc). This breaks nuspec-project correlation during builds resulting in not picking up the nuspec during packaging of a project.
* Build time is now measured for each step and available in the build log
* Made build status API reporting optional

## Minor Tweaks and Bug Fixes
* Build Services: Builds now fail immediately for feeds that return a 4XX or 5XX status code
* Build Services: No longer ignore ```*Test*.nuspec``` files when packaging
* Build Services: Assembly Version Patching no longer fails to process files that contain Visual Studio regions
* Build Services: Delete web hook at GitHub / VSO when build source is deleted
* API: ```nuget delete``` is no longer case-sensitive on package ID
* Cloning gallery feeds now unpublishes the cloned feed from the gallery
* Allow updating the description of access tokens
* Support page - Ability to select the (own) feed related to the support request, which helps us provide even faster support! ;)

In addition, this release also contains a lot of performance fixes, and we continue to work hard on improving your overall MyGet experience.
If you feel strong about a missing feature or have an idea for further improvement, [please let us know][9]! We build this for you!

_Happy packaging!_

[0]: http://blog.myget.org/post/2014/05/19/package-mirroring-is-now-enabled-by-default.aspx
[1]: http://blog.myget.org/post/2014/01/15/Build-Status-Badges.aspx
[2]: http://docs.myget.org/docs/reference/build-services#GitVersion_and_Semantic_Versioning
[3]: http://blog.myget.org/post/2014/06/26/Promoting-packages-generated-during-build.aspx
[4]: http://blog.myget.org/post/2014/05/12/Announcing-Visual-Studio-Online-integration.aspx
[5]: http://blog.myget.org/post/2014/05/05/Picking-the-right-dependency-version-adding-packages-from-NuGet.aspx
[6]: http://blog.myget.org/post/2014/04/25/Configuring-password-policies-and-account-lockout-using-MyGet-Enterprise.aspx
[7]: http://blog.myget.org/post/2014/09/10/Introducing-MyGet-webhooks.aspx
[8]: http://blog.myget.org/post/2014/06/03/Creating-a-license-report-for-your-NuGet-packages.aspx
[9]: http://myget.uservoice.com/
[10]: http://blog.myget.org/post/2014/09/23/Notifications-let-you-know-when-a-package-is-updated.aspx
[11]: http://blog.myget.org/post/2014/04/07/get-notified-of-feed-activity-through-rss.aspx
[12]: http://blog.myget.org/post/2014/03/03/MyGet-Documentation-site-redesigned.aspx
[13]: http://blog.myget.org/post/2014/03/10/Specifying-which-projects-get-built-with-MyGet-Build-Services.aspx
[14]: http://blog.myget.org/post/2014/03/25/Setting-default-package-sources-during-build.aspx
[15]: http://blog.myget.org/post/2014/10/08/myget-feeds-now-support-target-framework-filtering.aspx
[16]: http://blog.myget.org/post/2014/08/05/MyGet-now-offered-through-the-Microsoft-Azure-Store.aspx
[17]: http://blog.myget.org/post/2014/10/14/User-defined-environment-variables-in-MyGet-builds.aspx
[18]: http://blog.myget.org/post/2014/10/16/Implementing-custom-package-retention-using-webhooks.aspx
[19]: http://blog.myget.org/post/2014/08/18/Configure-a-feeds-Report-Abuse-URL.aspx
[20]: http://blog.myget.org/post/2014/11/13/IP-whitelisting-for-MyGet-Enterprise-customers.aspx
