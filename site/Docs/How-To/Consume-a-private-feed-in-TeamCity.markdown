# Consume a private feed in TeamCity

When consuming NuGet packages from an authenticated feed during a build on TeamCity, the last thing you want to do is add credentials for connecting to that authenticated feed to source control. Depending on your version of TeamCity, there are different approaches to consuming private feeds.

## TeamCity 8

TeamCity 8 provides a build feature which enables you to consume packages from feeds that require authentication.

When editing the build configuration, you can add the NuGet Feed Credentials build feature from the build steps administration. In the dialog that is opened, the feed URL whould be specified as well as credentials to connect to the feed.

![TeamCity NuGet Feed Credentialsl](Images/teamcity-private-feed.png)

## TeamCity 7

In older versions of TeamCity, two additional buildsteps can be added. One right before consuming the feed, one right after.

The following command line should be run before consuming the feed:

	nuget sources add -Name <some name for the feed> -Url <feed URL> -Username <username> -Password <password>

After consuming the build: (do note that you want to run this step regardless of the build status of previous steps)

	nuget sources remove -Name <some name for the feed>
	
This will add cached credentials to the build agent's NuGet configuraion before the build and remove them again afterwards.