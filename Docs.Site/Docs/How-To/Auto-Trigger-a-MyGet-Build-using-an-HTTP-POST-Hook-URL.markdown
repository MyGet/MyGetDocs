# Auto Trigger a MyGet Build using an HTTP POST Hook URL

In addition to manually triggering a Build within the MyGet User Interface, it is also possible to automatically trigger a Build every time code is pushed to your Source Control Repository, by making use of an HTTP POST Hook URL.

<p class="alert alert-info">
    <strong>Note:</strong> When adding a build source from GitHub, BitBucket or Visual Studio Team Services, we automatically create a service hook trigger for you. Except if you disable this when linking a source code repository to MyGet, the hook has already been created and there is no need to do this again.
</p>

## What is an HTTP POST Hook URL?

Once you have fully configured a Build Source for your MyGet Feed, you will be able to manually trigger a build whenever you like.  However, if you are trying to adopt the [Continuous Integration Software Development Practice](http://martinfowler.com/articles/continuousIntegration.html "Martin Fowler talks about the Continuous Integration Software Development Practice"), then automatically triggering a MyGet Build whenever you push some code changes to your Source Code Repository is one of the first steps in doing this.

The HTTP POST Hook URL is a mechanism to allow your Source Code Repository to notify the MyGet Build Service (via an HTTP POST to the given URL) when a commit has been pushed.  As soon as this has happened, a MyGet Build will be added to the Build Queue, which will then go and grab the latest code from the Source Code Repository, and execute the Build.

<p class="alert alert-info">
    <strong>Note:</strong> MyGet Build Services has a 5 minute cooldown period between builds during which you can't trigger a build, manually or otherwise. Please contact MyGet support for more information about our dedicated Build Services offering to avoid this cooldown period.
</p>

## Where can I find the HTTP POST Hook URL?

When you are logged into MyGet, browse to the Feed that you would like to configure, and select Build Services (you can find the link on the left hand side of the screen).  This will take you to a page which looks similar to this:

![The MyGet Build Services Window](Images/myget_build_services_window.png)

The HTTP POST Hook URL is clearly visible on this page (the GUID at the end of the URL has been obscured simply because this is unique to each Feed).  Click the copy button (indicated by the arrow) to grab the URL ready for adding into your Source Code Repository.

## Setting up GitHub

<p class="alert alert-info">
    <strong>Note:</strong> When adding a build source from GitHub, we automatically create a service hook trigger for you. Except if you disable this when linking a souce code repository to MyGet, the hook has already been created and there is no need to do this again.
</p>

We recommend using <a href="https://www.myget.org/profile/me#!/AccessTokens">alternate API keys</a> for every sync relation.

The following 6 steps provide information about how to use the MyGet HTTP POST Hook URL within a GitHub Repository.

1. Log into GitHub and navigate to the repository that is to be configured
2. In the top right hand corner of the page, click the __Settings__ button
![The Project Settings button within your GitHub Repository](Images/github_hook_settings_button.png)
3. Within the Settings page, click on the __Service Hooks__ button, located down the left hand side of the page
![The Service Hooks button within your GitHub Project Settings](Images/github_hook_service_hooks_button.png)
4. Within the Service Hooks page, click on the __WebHook URLs__ link
![The WebHook URLs link within Service Hooks for your GitHub Project](Images/github_hook_webhook_url_link.png)
5. Using the form that appears, paste in the HTTP POST Hook URL that was copied from MyGet above into the __URL__ field and click the __Update settings__ button. __Make sure to select the application/vnd.github.v3+json hook type!__
![Adding a new WebHook URL to your GitHub Project](Images/github_hook_add_webhook_url.png)
6. To verify that this has been set up correctly, you can then click the __Test Hook__ button.  Doing this should trigger a Build straight away within MyGet.
![Testing the new WebHook URL that was added to your GitHub Project](Images/github_hook_test_webhook.png)

## Setting up Bitbucket

<p class="alert alert-info">
    <strong>Note:</strong> When adding a build source from  BitBucket, we automatically create a service hook trigger for you. Except if you disable this when linking a source code repository to MyGet, the hook has already been created and there is no need to do this again.
</p>

The following 6 steps provide information about how to use the MyGet HTTP POST Hook URL within a Bitbucket Repository.

1. Log into Bitbucket and navigate to the repository that is to be configured
2. In the right hand corner of the page, click the __Settings__ button
![The Project Settings button within your BitBucket Repository](Images/bitbucket_hook_settings_button.png)
3. Within the Settings page, click on the __Services__ link, located down the left hand side of the page
![The Service Hooks button within your BitBucket Project Settings](Images/bitbucket_hook_service_hooks_button.png)
4. Within the Services page, find the POST option within the __Select a service...__ drop down list, and click the __Add Service__ button
![The WebHook URLs link within Service Hooks for your BitBucket Project](Images/bitbucket_hook_webhook_dropdown.png)
5. Using the form that appears, paste in the HTTP POST Hook URL that was copied from MyGet above into the __URL__ field and click the __Save__ button
![Adding a new WebHook URL to your BitBucket Project](Images/bitbucket_hook_add_webhook_url.png)
6. At this point in time, there is no automatic way of testing to ensure that the URL that you have entered works correctly.  In order to achieve this though, you can follow the [Troubleshooting Bitbucket Service](https://confluence.atlassian.com/display/BITBUCKET/Troubleshooting+Bitbucket+Services "Troubleshooting Bitbucket Services") page on the Atlassian Wiki.  In addition, make sure to add comments on the [issue that has been raised](https://bitbucket.org/site/master/issue/4667/add-ability-to-test-services-bb-5436 "Bitbucket Issue for adding ability to test Services") to add this feature to Bitbucket to show that it is popular.

## Setting up CodePlex

The following 6 steps provide information about how to use the MyGet HTTP POST Hook URL within a CodePlex project.

1. Log into CodePlex and navigate to the project that is to be configured
2. In the right hand corner of the page, click the __Settings__ link
![The Project Settings button within your CodePlex project](Images/codeplex_hook_settings_button.png)
3. Within the Settings page, click on the __Services__ link, located at the top of the page
![The Service Hooks button within your CodePlex Project Settings](Images/codeplex_hook_service_hooks_button.png)
4. Within the Services page, click the __AppHarbor__ option on the left (yes, this is a trick)
![The AppHarbor service for your CodePlex Project](Images/codeplex_appharbor_hook.png)
5. Using the form that appears, check the __Enable code change events__ option and paste in the HTTP POST Hook URL that was copied from MyGet above into the __URL__ field and click the __Save__ button
![Adding a new WebHook URL to your CodePlex Project](Images/codeplex_hook_add_webhook_url.png)
6. At this point in time, there is no automatic way of testing to ensure that the URL that you have entered works correctly other than commiting code to your CodePlex repository.
