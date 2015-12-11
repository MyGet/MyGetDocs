# Use VSTS Git repositories with MyGet Build Services

MyGet Build Services supports many repositories from which it can fetch sources and build NuGet packages. When using [Visual Studio Team Services - VSTS](http://www.visualstudio.com) Git repositories, basic authentication should be enabled on your service to be able to use it with MyGet Build Services.

The following steps are required to enable basic authentication:

* Go to your team project's home page and open your profile.

![TFS Online profile](Images/tfsonline-profile.jpg)

* Allow alternate credentials for this account.

![Allow alternate credentials](Images/tfsonline-allowalternate.jpg)

You can now use the Git URL for your TFS Online project with MyGet Build Services.