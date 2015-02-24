# Okta integration

Many companies make use of single sign-on providers like [Okta](http://www.okta.com). Using Okta, users in your organization can easily login to company approved apps, of which MyGet could be one. This document describes the required configuration steps on your Okta administraton page.

## 1. Add a new Template App

From the Okta administration, use the top menu and navigate to _Applications_. Use the search box to create a new app and search for `Template App`. We will use this template to configure MyGet as an application with Okta.

![Add template app](Images/okta-add-template-app.png)

## 2. Configure the MyGet application

The application should be configured with the following parameters:

* **Application label**: Enter a descriptive label, like `MyGet`.
* **URL**: Enter the following URL: `https://www.myget.org/Account/SsoLogin`
	* _Note that if you're using MyGet Enterprise, you will have to change this URL to a URL that resembles your MyGet Enterprise login, e.g. `https://acmecorp.myget.org/Account/SsoLogin`._
* **Username parameter**: Enter `Username`.
* **Password parameter**: Enter `Password`.

![Okta application configuration](Images/okta-app-config.png)

## 3. Assign users

In Okta, assign users to MyGet. All users assigned to MyGet will get single sign-on capabilities using Okta.

## 4. Sign in using Okta

Using the Okta browser plugin or Okta's unique application URL for MyGet, sign in. The first login, Okta will ask for MyGet credentials which should be used for the user. The user can either enter existing MyGet credentials, or enter a username/password combination of choice to create a new user.

![kta first login](Images/okta-first.png)

<p class="alert alert-info">
    <strong>Note:</strong> The first login to MyGet will ask for additional profile details such as e-mail address and so forth. Every user will have to provide these details once.
</p>
