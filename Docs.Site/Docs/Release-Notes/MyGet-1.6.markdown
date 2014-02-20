# MyGet 1.6 Release Notes

MyGet 1.6 was released on February 25, 2013.

## Features

### MyGet
* Minimum length for usernames has been decreased to 3 characters (previously 6). Shorter usernames are now possible.
* A new menu item under feed: "Feed Settings" will contain settings specific to how MyGet handles packages for a given feed.
* Feed settings contains an option to enable/disable overwriting of packages on the feed
* Feed settings contains an option to enable/disable validation on the package version number with regards to Semantic Versioning
* Dashes and underscores in feed names are supported. Feeds can be named _foo-prod_ for example.
* Download feed as ZIP.
* Package Sources are out of beta.
* API key in package source configuration is now masked.

### MyGet Enterprise
* Block e-mail addresses not belonging to the organization.
* Users can be made administrator.
* User removal (with the option of transferring their resources to another user).

### MyGet Build Services
* Copy build log to clipboard.
* Specify build configuration and platform (Release/Debug and Any CPU/Mixed Platforms/...)
* Support incremental build numbers.
* Support configuring the build number using a template. Register version number as an environment variable.
* Support building Windows Phone 8 projects.
* Hotlink commit on GitHub/BitBucket from the build list.
* Refresh build status automatically.
* Support creating tools packages.
* Hanging build detection.
* Install psake on the build servers.

## Bug Fixes
* Copy to clipboard on feed details page did not work in Chrome version 24.0.1312.57 and up.
* Feed statistics are not updated in some situations.
* NuGet Package Explorer always shows prerelease packages in the feed list.
* Build Services: building from protected SVN repositories isn't always working.
* Build Services: GitHub API only returns 30 repositories.

_Happy packaging!_
