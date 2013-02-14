# Why Should I Use MyGet?

There are quite a few usage scenarios out there where MyGet fits in very well:

* A feed containing only the packages you or your company often use
* A feed containing only your (open-source?) project and its dependencies
* A feed containing just a few packages that you want to use for a certain project: tell your developers to just install them all
* A feed creating and pushing packages on itself by watching your GitHub/Codeplex/Bitbucket repository and triggering our Build Services

## What's the difference between MyGet and NuGet.Server, Orchard Gallery or NuGet Gallery?

Tough one! If you look at MyGet for the first minute, you would think it's an identical solution to NuGet.Server, Orchard Gallery or NuGet Gallery. If you create an account, you will notice some differences though:

* MyGet can set up a feed for you in less than 5 seconds
* You don't have to set up or maintain your own infrastructure
* There's support for multiple feeds per account in MyGet. NuGet.Server, Orchard Gallery and NuGet Gallery only support one
* MyGet allows you to delegate privileges to other users without sharing an API key: everyone has its own, secure access to a MyGet feed
* And much more is coming!