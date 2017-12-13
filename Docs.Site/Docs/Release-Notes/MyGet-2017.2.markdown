# MyGet 2017.2 Release Notes

MyGet 2017.2 was released on December 13, 2017.

## Highlights

Next to some new [features](#Features) and many [fixes](#Bug_Fixes_amp_Other_Improvements), this 2017.2 release of MyGet again adds some new functionality to the service.

Major highlights of this release are: 

* We added **PHP Composer** support, and welcome PHP developers to the MyGet family! ([Announcement](https://blog.myget.org/post/2017/09/11/php-composer-packages-on-myget.html) | [Docs](docs/walkthrough/getting-started-with-php-composer))

  In fact, this also resulted in a bug fix on Composer itself (which is now merged, yay!) - [composer/composer#6717](https://github.com/composer/composer/pull/6717) will be part of Composer 1.5. Happy to contribute back!

<div style="text-align:center; display:block; margin-bottom:10px;">
<a href="docs/walkthrough/getting-started-with-php-composer">
	<img src="Images/myget-php-composer.svg" alt="Getting started with PHP Composer on MyGet!" width="90px"/>
</a>
</div>

* MyGet is now also available in the **GitHub Marketplace**! ([Announcement](https://github.com/blog/2469-build-on-your-workflow-with-four-new-marketplace-apps))
Full details, and the ability to set up a *free trial* from within GitHub, are available at [https://github.com/marketplace/myget](https://github.com/marketplace/myget).

<div style="text-align:center; display:block; margin-bottom:10px;">
<a href="https://github.com/marketplace/myget">
	<img src="Images/gh-marketplace.png" alt="MyGet package management now available in the GitHub Marketplace!" width="400px"/>
</a>
</div>

* In light of upcoming **EU General Data Protection Regulation** ([EU GDPR](https://www.eugdpr.org/)), which will be enforced in May 2018, MyGet is taking proactive action to verify we are compliant, and take corrective measures if we spotted anything that is not compliant, or questionable (typical grey area in legislation).
Being a EU-based company, we take privacy and security very seriously! As such, we focused in this release to ensure that:

	* user sign-ups by default opt-out of marketing communications or newsletters; unless the user explicitly takes positive action by ticking the checkbox to opt-in (which of course we do recommend, as we try to keep you informed about evolutions and guidance in the package management space)
	* first-time visitors see a proper cookie consent banner, requiring so-called *double consent* from the user (in other words: we ask you to explicitly accept/deny non-essential cookies instead of the automatic consent with cookies you see elsewhere on the Internet)
	* we verified our usage of essential and non-essential cookies and ensure we comply to GDPR in the way we handle these
	* we don't retain any personally identifiable information (PII) data longer than necessary and only use it for the purposes intended (full details in our [Terms of Service](https://myget.org/policies/terms) and [Privacy Policy](https://www.myget.org/Content/docs/legal/Privacy%20Policy.pdf))
  
  As data protection is critical, MyGet can help organizations in protecting them against potential vulnerabilities imposed by third-party or open source dependencies using the built-in [*vulnerability report*](docs/reference/vulnerability-report) on each feed.
  More GDPR-specific changes are coming to MyGet as we continuously deploy our services, and will be aggregated in the 2018.1 release notes.

<div style="text-align:center; display:block; margin-bottom:10px;">
<a href="/docs/reference/vulnerability-report">
	<img src="../Reference/Images/vulnerability-report.png" alt="MyGet vulnerability report for packages" width="400px"/>
</a>
</div>

* Another big theme we focused on lately is **auditing**. 
  Similar to our activity streams, we made security related events accessible in an easy to use audit log to MyGet Enterprise administrators. ([Announcement](https://blog.myget.org/post/2017/11/16/myget-enterprise-auditing.html))
  In addition, we allow an export of these audit logs into a downloadable `.csv` file containing up to 25.000 entries.

<div style="text-align:center; display:block; margin-bottom:10px;">
<a href="https://blog.myget.org/post/2017/11/16/myget-enterprise-auditing.html">
	<img src="Images/audit-entries.png" alt="Inspecting audit logs in MyGet Enterprise" width="400px"/>
</a>
</div>

## Features

### MyGet (all plans)

The following applies to **all** MyGet plans:

* NuGet: added support for `NetStandard2.0` and `NetCoreApp2.0`
* NuGet: added support for NuGet secure push protocol
* NuGet: improved v3 feed SemVer2 build metadata support
* Maven: added support for Maven classifiers
* Maven: added support for Maven version numbers `x.x.x.y.z` and `x.y.z.pre`
* Maven: added support for POM with dependencies that have a version range
* Maven: added support for Spring boot JAR files
* Maven: display classifier artifacts on package details page so they can be downloaded
* Maven: added support for inheriting `pom.xml` *groupId*, *artifactId* and *version* from parent
* Maven: now support POM files to exceed 64 KB in size
* NPM: added support for npm `whoami`
* Performance: upstream package sources that fail or timeout repeatedly will now be auto-disabled
* Usability: improved the dropdown menu in the main navigation
* Usability: improved look and feel of home page for newly registered users
* Usability: improved gallery details page ([Announcement(https://blog.myget.org/post/2017/08/01/new-and-improved-gallery-details-page.html)])
* Usability: added auto-focus on pages with search box so that you can simply start typing to search/filter
* Usability: when a feed clone operation fails, we now send an e-mail notification to inform the feed owner

### MyGet Enterprise

The enterprise plan has all functionality from the paid subscription plans, and more!
The following applies only to the MyGet Enterprise plan:

* Usability: MyGet Enterprise have an additional filter on the home page to show/filter Enterprise feeds
* Usability: improved user profile page in Enterprise administrative dashboard
* Usability: switched payment provider for MyGet Enterprise to Stripe to streamline the billing experience

### MyGet Build Services

* Added option to hide environment variables in build output
* Added PHP Composer support!

## Bug Fixes & Other Improvements

* NPM: fixed a bug that caused npm dist-tags not always to be populated correctly
* NPM: fixed a bug in version normalization when mirroring upstream npm packages
* NPM: fixed a bug that caused an API key validation error in npm upstream source configurations
* Maven: fixed a bug that caused package size to be overwritten when storing package classifier artifacts
* Maven: fixed a version parsing error that caused an unhandled exception during package upload
* Maven: fixed a bug that caused SNAPSHOT packages to be write-protected
* Maven: fixed a bug that caused failures mirroring specific Maven packages
* Maven: fixed a bug that caused POM overwrite to succeed when package overwriting is forbidden
* Maven: fixed a bug that caused SNAPSHOT POM overwrite to fail when package overwriting is forbidden
* Maven: fixed a bug that caused package POM proxying to fail when the upstream package POM contained malformed XML
* Maven: normalize line endings when proxying upstream package
* NuGet: cosmetic change to use updated NuGet logo
* Symbols: fixed a bug that caused VSTS to not automatically pick up symbols packages from your feed
* Symbols: provide a better error message to NuGet clients that incorrectly configured the symbols endpoint to be used as NuGet endpoint
* Usability: improved upgrade notification on MyGet Professional plan
* Usability: improved documentation for MyGet's [upstream sources](https://docs.myget.org/docs/reference/upstream-sources) feature
* Usability: our error page may now provide you with an error ID that you may attach to your technical support tickets. This helps our support team in correlating the issue with server-side diagnostics and telemetry.
* Usability: we separated technical support from billing support, to streamline the support experience, and show contextual help based on Knowledge Base articles.
* Usability: fixed a JS error on the *Manage permissions for feed* page
* Upstream sources now have improved timeout handling to fail faster if an upstream source is unreachable or not responding
* Upstream sources package mirroring now gracefully handles failures when attempting to mirror non-SemVer2 packages onto a SemVer2-only restricted feed
* Build services: fixed a bug that caused warning messages in build log to be incorrectly interpreted as error

We love hearing from you, so keep that [feedback](https://myget.uservoice.com/) coming! MyGet is built for you!

_Happy packaging!_

<a href="https://www.myget.org">
	<img src="Images/host your packages on myget.gif" alt="Host your packages on MyGet!" />
</a>
