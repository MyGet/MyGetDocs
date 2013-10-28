# Feed Security

## Available Feed Types

MyGet offers 3 standard feed types supporting various scenarios. You can change feed type at any time (given you did not exceed any subscription, feed or user quota).

* **Public** feeds - Everyone can search and download packages from this feed. Only users with sufficient privileges will be able to push packages to this feed. Public feeds listed in the Gallery have the *ReadOnly* tag. Public feeds are free (quota may apply based on your subscription plan).
* **Community** feeds - Everyone can search and download packages from this feed. Additionally, any user can push and manage their **own** packages on this feed. Community feeds listed in the Gallery have the *Community* tag. Community feeds are free (quota may apply based on your subscription plan).
* **Private** feeds - Nobody except the feed owner has access by default. The feed owner will invite people to this feed and assign feed privileges (see below). Private feeds are available on all paid subscription plans (quota may apply based on your subscription plan).

## Available Feed Privileges

Permissions on a MyGet feed can be granted to other users.

* **Has no access to this feed** - The user will no longer have access to the current feed.
* **Can consume this feed** - The user can search and consume packages but pushing packages is not allowed.
* **Can contribute own packages** to this feed - The user can search, consume and push packages to the feed.
* **Can manage all packages for this feed** - The user can search, consume and push packages to the feed as well as use the MyGet website to manage packages.
* **Can manage users and all packages for this feed** - The user can search, consume and push packages to the feed as well as use the MyGet website to manage packages and users. It is as good as being a feed owner except that deleting a feed isn't allowed.
* **Owner of this feed** - The user owns the feed and can performall operations on it.
