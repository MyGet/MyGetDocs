# License Analysis

When managing your dependencies using MyGet, it may be important to have a view on which licenses are used on your feeds and in your software projects. Many teams would love to know which (open source) licenses are being used by their teams, so they can be inspected and managed.
From your feed, a _Licenses_ tab displays a report of all licenses used by packages on your feed.

![License report for NuGet packages](Images/license-analysis.png)

The licenses overview provices a chart that provides a quick view on licenses in use. The list underneath shows all different licenses used per package identifier. If a package changed license over time, it will be listed twice. To quickly filter the detailed list, click one of the colors in the chart: this will show just the packages that have the selected license applied.

## Where does license information come from?

Whenever a package is uploaded to your feed, whether from an upstream package source or by using ```nuget push```, MyGet will perform a license analysis on the package. The license is determined as follows:

* If we've seen the package's license URL before, we will assign the same license to the package that is being added.
* If your feed contains a package with the same identifier, we'll take that package's license.
* If we haven't, we'll download the license URL result and run a full-text analysis on it. We've been working on a unique scoring algorithm which compares the text with known [OSI licenses](https://opensource.org/licenses/) out there.
* If the score is too low, we assign the license ```Unknown``` to the package.

## How can I change the license for a package?

Whenever you have a package where the license analysis was incorrect or you want to assign a proprietary license name to a package, it is possible to override the license. From the package details page, you can inspect the package license as well as override it.

![License report for an individual NuGet package](Images/package-details-license.png)

Editing the license will open a dialog in which you can specify a new license. MyGet provides autocompletion on known [OSI licenses](https://opensource.org/licenses/), but a proprietary license name can be entered here as well. Once a license has been overriden, new versions of the package will be assigned this overriden license.

![Edit NuGet package license information](Images/edit-license.png)
+
