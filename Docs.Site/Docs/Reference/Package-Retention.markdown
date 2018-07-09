# Package retention

As you produce more and more packages, it may become harder to manage all of those assets.

**By default, we keep all package versions available on your feed.** Over time though, you may find your feeds to become larger and larger, perhaps even growing beyond the *storage quota* included in your MyGet subscription plan.
Package retention policies can be configured to overcome this growth and perform some automatic housekeeping.

## "Sticky" packages

Before diving into configuring any package retention policies, you may want to consider which packages you wish to retain. **Certain packages should never be deleted automatically.**

Defining which packages you wish to retain can easily be done by *pinning* packages, so they stick around, **even when package retention policies apply**.

Package retention rules will ignore any pinned packages, to avoid they accidentally get removed during housekeeping.

From the package details page, you can define which package versions should be retained using the **Pin** button next to the package. Alternatively, you can pin all versions at once, all but latest, or only the latest version using the button at the top of the *Package History* list.

Unpinning packages can be done using the same approach.

<img src="Images/package-pinning.png" alt="Pinning and unpinning packages" style="border: 1px solid darkgray;" />

## Defining package retention rules

**By default, we keep all package versions available on your feed.** Package retention rules allow you to do some automated housekeeping. These rules are **defined per feed**, so you can define different retention policies for different feeds.

<img src="Images/package-retention-rules.png" alt="Defining Package Retention Rule" style="border: 1px solid darkgray;" />

Retention options currently consist of any combination of the below:

* Maximum number of stable versions to keep
  * newest package versions for every package id
  * newest package versions for every package id and major version
  * newest package versions for every package id and major.minor version
  * most recently published packages for every package id
  * most recently published packages for every package id and major version
  * most recently published packages for every package id and major.minor version
* Maximum number of prerelease versions to keep
  * newest package versions for every package id
  * newest package versions for every package id and major version
  * newest package versions for every package id and major.minor version
  * most recently published packages for every package id
  * most recently published packages for every package id and major version
  * most recently published packages for every package id and major.minor version
* Keep depended packages
* Allow removal of packages that have downloads

### Retention Rule for Stable Versions

By defining the maximum number of stable versions to keep, the retention policy will keep the configured number of stable versions and remove other stable versions from the feed.

### Retention Rule for Prerelease Versions

By defining the maximum number of prerelease versions to keep, the retention policy will keep the configured number of prerelease versions and remove other prerelease versions from the feed.

### Retention Rule for Package Dependencies

By default, MyGet will never delete packages that are depended on (within the same feed).

When this retention rule is enabled, we will blindly delete packages that are older than version *X* (as defined in the first two rules), even if there are still packages depending on it. This can put you into trouble and cause [package not found](../How-To/package-not-found-during-package-restore) messages during package restore. For this very reason, the option is **disabled** by default.

### Retention Rule for Package Downloads

By default, MyGet's retention policies will remove all packages that match any of the above conditions. Sometimes you may want to keep package versions that have at least one download: it means these packages are being used and should remain available on the feed. Enabling the this retention rule will prevent retention policies from removing packages that have at least one download.

## Frequently Asked Questions

### Stable vs. Prerelease versions

A stable version is any version that does not have a `-` in the version. These versions are considered *stable* versions:

* `1.0.0`
* `1.1.0`
* `1.2.1.123`
* `2.0.0`
* `2.1.0.1`

These versions are considered *prerelease* versions:

* `1.0.0-alpha`
* `1.1.0-beta`
* `1.2.1-dev`
* `2.0.0-ci`

### Newest package versions vs. most recently published

Retention policies can be configured based on either *newest package version* or *most recently published*.

* When configuring *newest package version*, retention policies **order packages by version** and keep the specified number of packages from the bottom of that list.
* When configuring *most recently published*, retention policies **order packages by publish date** and keep the specified number of packages from the bottom of that list.

Let's provide some examples of which packages the various retention policies will keep around when running. Our feed contains the following packages:

* `Foo.Bar 1.0.0`, published `2018-07-01T00:00:00`
* `Foo.Bar 1.0.1`, published `2018-07-02T00:00:00`
* `Foo.Bar 1.0.2`, published `2018-07-04T00:00:00`
* `Foo.Bar 1.1.0`, published `2018-07-03T00:00:00`
* `Foo.Bar 1.1.1`, published `2018-07-04T00:00:00`
* `Foo.Bar 2.0.0`, published `2018-07-02T00:00:01`
* `Foo.Bar 2.1.0`, published `2018-07-02T00:00:02`

With retention set to **Keep 2 newest package versions for every package id**, the following packages will be **kept** on our feed:

* `Foo.Bar 2.0.0`, published `2018-07-02T00:00:01`
* `Foo.Bar 2.1.0`, published `2018-07-02T00:00:02`

With retention set to **Keep 2 newest package versions for every package id and major version**, the following packages will be **kept** on our feed:

* `Foo.Bar 1.1.0`, published `2018-07-03T00:00:00`
* `Foo.Bar 1.1.1`, published `2018-07-04T00:00:00`
* `Foo.Bar 2.0.0`, published `2018-07-02T00:00:01`
* `Foo.Bar 2.1.0`, published `2018-07-02T00:00:02`

With retention set to **Keep 2 newest package versions for every package id and major.minor version**, the following packages will be **kept** on our feed:

* `Foo.Bar 1.0.1`, published `2018-07-02T00:00:00`
* `Foo.Bar 1.0.2`, published `2018-07-04T00:00:00`
* `Foo.Bar 1.1.0`, published `2018-07-03T00:00:00`
* `Foo.Bar 1.1.1`, published `2018-07-04T00:00:00`
* `Foo.Bar 2.0.0`, published `2018-07-02T00:00:01`
* `Foo.Bar 2.1.0`, published `2018-07-02T00:00:02`

With retention set to **Keep 2 most recently published packages for every package id**, the following packages will be **kept** on our feed:

* `Foo.Bar 1.0.2`, published `2018-07-04T00:00:00`
* `Foo.Bar 1.1.1`, published `2018-07-04T00:00:00`

With retention set to **Keep 2 most recently published packages for every package id and major version**, the following packages will be **kept** on our feed:

* `Foo.Bar 1.0.2`, published `2018-07-04T00:00:00`
* `Foo.Bar 1.1.1`, published `2018-07-04T00:00:00`
* `Foo.Bar 2.0.0`, published `2018-07-02T00:00:01`
* `Foo.Bar 2.1.0`, published `2018-07-02T00:00:02`

With retention set to **Keep 2 most recently published packages for every package id and major.minor version**, the following packages will be **kept** on our feed:

* `Foo.Bar 1.0.1`, published `2018-07-02T00:00:00`
* `Foo.Bar 1.0.2`, published `2018-07-04T00:00:00`
* `Foo.Bar 1.1.0`, published `2018-07-03T00:00:00`
* `Foo.Bar 1.1.1`, published `2018-07-04T00:00:00`
* `Foo.Bar 2.0.0`, published `2018-07-02T00:00:01`
* `Foo.Bar 2.1.0`, published `2018-07-02T00:00:02`