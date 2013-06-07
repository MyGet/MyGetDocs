# Use TFS Online Git repositories with MyGet Build Services

MyGet Build Services supports many repositories from which it can fetch sources and build NuGet packages. When using [TFS Online](http://tfs.visualstudio.com) Git repositories, basic authentication should be enabled on your service to be able to use it with MyGet Build Services.

The following steps are required to enable basic authentication:

1. Go to your team project's home page and open your profile.
![TFS Online profile](Images/tfsonline-profile.jpg)
2. Allow alternate credentials for this account.
![Allow alternate credentials](Images/tfsonline-allowalternate.jpg)

You can now use the Git URL for your TFS Online project with MyGet Build Services.