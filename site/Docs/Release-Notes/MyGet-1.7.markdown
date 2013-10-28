# MyGet 1.7 Release Notes

MyGet 1.7 was released on May 17, 2013.

## Features

### MyGet Docs (new!)
* New MyGet Documentation site at http://docs.myget.org

### MyGet
* Support for NuGet 2.5
* Support for NuGet 2.5 new search syntax to query feeds
* Support for cross-domain calls to MyGet feeds from within Silverlight
* Support for ReSharper Extensions Gallery as package source preset
* Support for filtering & searching feeds on home page when authenticated
* Support for paging in the MyGet Gallery
* Support for traditional username/password registration on the MyGet Web site
* Support for user invitation by username to claim access to a feed
* Support for Package Source Discovery
* Support for SymbolSource in Package Source Discovery
* Redesign of home page when authenticated
* Activity streams on home page when authenticated
* Activity stream on own user profile when authenticated
* Links to the FAQ page are now pointing to http://docs.myget.org/docs/Reference/myget-faq
* New feed setting: auto-publish symbols
* Added username and subscription ID to Subscription Reminder emails
* Added link on the plans page to request a free trial of one of the paid plans

### MyGet Enterprise
* All fixes and features mentioned on this page also apply to MyGet Enterprise

### MyGet Build Services
* AssemblyInfo patching as-a-service
* Show number of packages produced during build
* Link to packages produced during build
* Support for build.cmd
* Support for build.ps1
* Improved custom build script support by providing various settings as process-specific environment variables

## Bug Fixes
* Fixed an issue with the feed activity page
* Fixed an issue with NuGet package restore over HTTPS
* Fixed an issue with pushing packages to nuget.org caused by enforced SSL
* Fixed an issue with pushing pre-release packages upstream
* Fixed an issue with build sources page scrolling to top upon every refresh
* Fixed an issue with validation of build source branch name
* Fixed an issue with uploading packages as a feed contributor
* Fixed an issue with Git repository cloning
* Numerous performance improvements

_Happy packaging!_