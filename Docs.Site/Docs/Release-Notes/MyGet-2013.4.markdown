# MyGet 2013.4 Release Notes

MyGet 2013.4 was released on September 10, 2013.

## Features

### MyGet
* Support for NuGet 2.7
* Metadata for packages is auto-updated from upstream feeds
* Retention policies: pin packages so they don't get deleted
* Retention policies: packages that are depended on will no longer be deleted (unless explicitly enabled)
* Push upstream: package source code repositories can be labeled when pushing packages upstream
* Send e-mail when feed permissions change
* Users can revoke their own access from a feed
* Automatic mirroring for packages from upstream feeds when feed proxy is enabled

### MyGet Enterprise
* Administrators can now join a feed. Feed owners are notified of this action.

### MyGet Build Services
* Repositories from GitHub organizations are now shown
* The latest build version is shown in the UI
* Package sources added at the feed level are available on the build server
* Automatic support for NuGet package restore even if it's not enabled for the solution
* Support for NuGet 2.7 package restore - see [https://docs.myget.org/docs/reference/build-services#Package_Restore](https://docs.myget.org/docs/reference/build-services#Package_Restore)
* Package sources added at the feed level are available on the build server for package restore
* Build labeling: on succesful or failed builds, a label can be added to the sources. This is compatible with GitHub Releases. - see [https://docs.myget.org/docs/reference/build-services#Source_labeling_(tagging)](https://docs.myget.org/docs/reference/build-services#Source_labeling_(tagging))
* Support for MyGet.cmd, MyGet.bat, MyGet.ps1

## Bug Fixes
* Fixed an issue with copy to clipboard
* Fixed an issue when pushing packages upstream to authenticated feeds
* Support page is no longer behind an authentication wall
* Fixed an issue when build services used @ in the username
* Various performance improvements

_Happy packaging!_
