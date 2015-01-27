# MyGet Enterprise

[MyGet Enterprise](http://www.myget.org/enterprise) contains everything MyGet has to offer, hosted on a dedicated URL for your team. It comes with an additional management dashboard that can be used to manage everything related to the team such as feeds, users, quota, account policies and so forth.

This page describes the available management options that are available to administrators of the Enterprise account.

## Statistics

The statistics page provides an overview of common metrics related to the Enterprise account:

* Feed statistics, e.g. number of feeds
* Package statistics, e.g. number of packages hosted
* Storage statistics, e.g. how much storages consumed by the Enterprise account and feeds therein
* Usage statistics, e.g. how many people are working with feeds
* Build staticstics, covering usage of [MyGet Build Services](Build-Services)

## Accounts

The accounts page allows setting account options, such as whitelisted/blacklisted domains, IP security, password policies and so forth.

![Managing accounts for MyGet Enterprise](Images/enterprise_account_management.png)

### Registration

The registration section can be used to whitelist/blacklist e-mail domains. For example, a whitelist can be set to _acmecompany.com_ to only allow users from that domain to register an account.

### IP security

MyGet Enterprise makes it possible to whitelist IP addresses (or IP ranges) so only clients can only access MyGet if they are coming from the configured address. The whitelist will be applied for accessing the website, as well as for consuming hosted NuGet feeds.

IP addresses can be entered on separate lines. If an entire subnet has to have access, the IP address can be suffixed with a CIDR suffix (e.g. /24) or a subnet mask (e.g. /255.255.255.0).

### Login page

The login page section provides a means of customizing the text that is shown on the login page (above username/password). For example, a notice could be added on who to contact to get an account.

### Passwords

The passwords section enables specifying minimum password length as well as policies such as how many lowercase, uppercase, numeric and special characters should be in a password.

### Lockout

The lockout section provides options to lock a user account after a number of login attempts. For example if 3 login attempts fail over a course of 10 minutes, the account can be locked out for a given duration.

### Sessions

By default, MyGet closes sessions after 2 hours. If it is required to close a session after a shorter time period, it can be specified here. Many teams use this feature to logout users after, for example, 15 minutes of inactivity.

## Users

The users section allows for user management. It is possible to create new users, as well as manage roles, manage quota for a user, send a password reset e-mail or delete a user.

### Creating a user

Users can be created by clicking the _Add a new user_ button, after which the display name, user name, password and e-mail can be entered. Optionally, a welcome e-mail can be sent to the user as well.

![Creating a MyGet Enterprise user](Images/add_user.png)

### Deleting a user

When deleting users, it is possible to transfer the user's feeds to a different user. For example when a user leaves the team or company, their account can be deleted and feeds can be transfered to other users within the MyGet Enterprise account.

### Managing user quota

Default quota can be overriden per user. It is possible to specify:

* Allowed # of feeds for the user
* Allowed # of private feeds for the user
* Allowed package size
* Allowed storage per user (over all feeds owned by the user)

## Feeds

The feeds page allows for basic feed management. It is possible for an administrator to join a feed team as either the owner or as a co-owner.

## Analytics

MyGet Enterprise allows tracking activity using [Google Analytics](http://www.google.com/analytics). By entering your Google Analytics account details, MyGet will send information about packages downloaded, features that are being used and so on to Google Analytics.

## Gallery

It is possible to enable or disable MyGet Gallery. When enabled, users can enlist their feeds in the gallery and make them discoverable for other users and teams. Feeds that are public will also be discoverable to any user outside the team, while feeds that are visible only to users in the Enterprise account will only show up after login.

## SymbolSource

The SymbolSource page allows configuring integration with [SymbolSource.org](http://www.symbolsource.org). Note that You will need a SymbolSource.org [private company](https://www.symbolsource.org/Public/Account/RegisterCompany) in order to be able to link MyGet to SymbolSource.

<p class="alert alert-info">
    <strong>Note:</strong> User accounts will be synchronized with SymbolSource by leveraging the <i>admin</i> account configured on this page. Please do not change the password for this account as it will break synchronization of users and privileges. 
</p>

We have a dedicated page available on [working with symbols and SymbolSource](SymbolSource) for your users.

## SMTP settings

MyGet comes with a preconfigured SMTP server for sending out system messages to users and administrators. If it is desired to use a custom SMTP server, it can be configured from this page.

## Default quota

Quota for users can be managed individually from the _Users_ page. However, it is possible to configure defaults on the quota page.

Configurable quota are:

* Allowed # of contributors per feed
* Allowed # of feeds per user
* Allowed # of private feeds per user
* Allowed package size
* Allowed storage per user (over all feeds owned by the user)
* Allowed storage per feed