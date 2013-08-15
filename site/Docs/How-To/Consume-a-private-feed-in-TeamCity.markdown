# Consume a private feed in TeamCity

When consuming NuGet packages from an authenticated feed during a build on TeamCity, the last thing you want to do is add credentials for connecting to that authenticated feed to source control. TeamCity provides a build feature which enables you to consume packages from feeds that require authentication.

When editing the build configuration, you can add the NuGet Feed Credentials build feature from the build steps administration. In the dialog that is opened, the feed URL whould be specified as well as credentials to connect to the feed.

![TeamCity NuGet Feed Credentialsl](Images/teamcity-private-feed.png)