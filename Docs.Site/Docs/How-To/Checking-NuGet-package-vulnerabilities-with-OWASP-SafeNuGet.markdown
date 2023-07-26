# Checking NuGet package vulnerabilities with OWASP SafeNuGet 

<p class="alert alert-warning">
    <strong>Note:</strong> This method of scanning vulnerabilities is outdated. Check out <a href="/docs/reference/vulnerability-report">our integrated vulnerability report</a> for a better way of analyzing potential vulnerabilities.
</p>

Use of libraries with known vulnerabilities can be an issue for software and components you create: check the excellent whitepaper ["The Unfortunate Reality of Insecure Libraries"](https://www.aspectsecurity.com/uploads/downloads/2012/03/Aspect-Security-The-Unfortunate-Reality-of-Insecure-Libraries.pdf). In the [OWASP Top 10 2013](https://www.owasp.org/index.php/Top_10#OWASP_Top_10_for_2013), consuming vulnerable packages is listed under [A9 Using Known Vulnerable Components](https://www.owasp.org/index.php/Top_10_2013-A9-Using_Components_with_Known_Vulnerabilities).

Automatic checking for known vulnerabilities can be done: [OWASP](https://www.owasp.org/) has released a [NuGet package](https://www.nuget.org/packages/SafeNuGet/) which is able to check known vulnerabilities in other NuGet packages. The [SafeNuGet package](https://github.com/OWASP/SafeNuGet) contains an MSBuild task which will warn you about consuming such packages.

## Installing SafeNuGet into a project

Installing SafeNuGet into a project is as easy as installing any other NuGet package:

	Install-Package SafeNuGet

This will add a *.targets* file to all projects in the open solution, adding a check for possibly vulnerable packages during build.

## How are potentially vulnerable packages shown?

A repository with vulnerable packages and the reason for that can be found on the [SafeNuGet GitHub project](https://github.com/OWASP/SafeNuGet). When running a build which references vulnerable NuGet packages, the warnings list will contain some information about this as well as a link with some explanation:

![OWASP SafeNuGet](Images/owasp-warning.png)

When a library referencing a potential unsafe package is built using MyGet Build Services, a warning will also be displayed in the build log:

![MyGet Build Services using OWASP SafeNuGet](Images/build-services-owasp.png)

## Does my build fail when such packages are consumed?

By default, the build will fail when such a package is found. If you only want warnings, find the *SafeNuGet.targets* file and change the setting `DontBreakBuild` to `true`.
