# Feed Security

Permissions on a MyGet feed can be granted to other users.

## Available Feed Privileges
* **Has no access to this feed** - The user will no longer have access to the current feed.
* **Can consume this feed** - The user can search and consume packages but pushing packages is not allowed.
* **Can contribute own packages** to this feed - The user can search, consume and push packages to the feed.
* **Can manage all packages for this feed** - The user can search, consume and push packages to the feed as well as use the MyGet website to manage packages.
* **Can manage users and all packages for this feed** - The user can search, consume and push packages to the feed as well as use the MyGet website to manage packages and users. It is as good as being a feed owner except that deleting a feed isn't allowed.
* **Owner of this feed** - The user owns the feed and can performall operations on it.