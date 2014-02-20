# MyGet Security

MyGet features a rich security model around your feeds. You, as a feed owner, always have the richest set of permissions possible. You can assign privileges to specific users on MyGet using their email address or username.

## Available Feed Types

MyGet offers 3 standard feed types supporting various scenarios. You can change feed type at any time (given you did not exceed any subscription, feed or user quota).

* **Public** feeds - Everyone can search and download packages from this feed. Only users with sufficient privileges will be able to push packages to this feed. Public feeds listed in the Gallery have the *ReadOnly* tag. Public feeds are free (quota may apply based on your subscription plan).
* **Community** feeds - Everyone can search and download packages from this feed. Additionally, any user can push and manage their **own** packages on this feed. Community feeds listed in the Gallery have the *Community* tag. Community feeds are free (quota may apply based on your subscription plan).
* **Private** feeds - Nobody except the feed owner has access by default. The feed owner will invite people to this feed and assign feed privileges (see below). Private feeds are available on all paid subscription plans (quota may apply based on your subscription plan).

If you are a MyGet Enterprise user, a fourth feed type is available:

* **Enterprise** feeds - Everyone in your organization gets read access to this feed unless other privileges are given to specific users.

## Personal security: access tokens

There are several credentials linked to your MyGet profile. Every user gets at least one the primary API key, which can be used when publishing packages with NuGet.exe or NuGet Package Explorer and a username and password for consuming private feeds from Visual Studio or a build server.

Additional access tokens can be generated [from your profile page](https://www.myget.org/profile/Me#!/AccessTokens). The primary API key can be regenerated and new tokens can be easily created or revoked. Access tokens can be given a short description: this will help keeping track of where you used the access token and revoke it if necessary.

Access tokens can be used for all authentication purposes. They can be used when pushing to your MyGet feed or as an alternate password when authenticating against a private feed.

![Managing access tokens](Images/access_token_management.png)

## Inviting other users to your feed

MyGet features a rich security model around your feeds. You, as a feed owner, always have the richest set of permissions possible. You can assign privileges to specific users on MyGet using their email address or username.

In order to give other users a certain privilege on your feed, they have to be invited to your MyGet feed. This can be done in the *Feed security* tab for your feed. This tab lists all users that currently have access to your feed as well as a list of &quot;pending&quot; invitations, that is: users that have been invited to your feed but haven’t confirmed yet.

![The Feed security tab which enables you to assign specific privileges to other users](Images/myget_feed_security_tab.png)

The *Add feed privileges...* button will open a dialog and allows you to invite a user to your feed by entering his e-mail address. You can immediately assign the correct privilege to this user to ensure the correct privilege will be assigned once the user confirms the invitation.

Below you can see an example invitation for a user to whom, once the invitation is confirmed, the <i>&quot;can consume this feed&quot;</i> privilege will be assigned.

![Inviting other users to a feed and assigning them a specific privilege](Images/myget_feed_security_popup.png)

Once you’ve clicked the *Add user* button, an e-mail will be sent to the e-mail address provided. The user being added to your feed will receive this e-mail and can choose to claim the privileges you’ve assigned or to simply ignore the invitation.

Once the user confirms this e-mail by clicking the link provided in the e-mail body, the user will be granted access to your feed with the privileges chosen in the *Add feed privileges* dialog.

## Managing User Permissions

After inviting a user to your feed, you can change the privileges previously assigned. For example, a user who could previously only consume packages may now be granted the privilege of contributing packages to your feed. Also, a user who could previously manage all packages on the feed can be locked down into a privilege where he can only consume packages and no longer manage them.

The *Feed security* tab for your feed lists all users that currently have access to your feed as well as a list of users that have been invited to your feed but haven’t confirmed their privileges yet. The dropdown next to a user’s name allows you to modify the currently assigned privilege.

**Note:** When assigning the <i>&quot;Has no access to this feed&quot;</i> privilege to a certain user, the user will be removed from the list of users. If afterwards you want to assign a dfferent privilege to this user, the user should be sent a new invitation using the *Add feed privileges...* button.

## Available Feed Privileges

Permissions on a MyGet feed can be granted to other users.
The table below lists all possible permissions and their meaning:

<table class="feedprivileges">
	<thead>
        <tr>
            <th>Permission</th>
            <th>Description</th>
            <th>MyGet account required?</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Has no access to the feed</td>
            <td>The user is denied access to the feed and cannot perform any operations on it.</td>
            <td style="text-align:center;vertical-align: middle">no</td>
        </tr
        <tr>
            <td>Can consume this feed</td>
            <td>The user can search and consume packages but pushing packages is not allowed.</td>
            <td style="text-align:center;vertical-align: middle">no</td>
        </tr>
        <tr>
            <td>Can contribute own packages to this feed</td>
            <td>The user can search, consume and push packages to the feed.<br />Users with this privilege will only be able to manage their own packages. This security setting is identical to the security settings on the official NuGet package source, and the default security setting for a MyGet Community feed.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
        </tr>
        <tr>
            <td>Can manage all packages for this feed</td>
            <td>The user can search, consume and push packages to the feed as well as use the MyGet website to manage packages.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
        </tr>
        <tr>
            <td>Can manage users and all packages for this feed</td>
            <td>The user can search, consume and push packages to the feed as well as use the MyGet web site to manage packages and users. It is as good as being a feed owner except that deleting a feed isn't allowed.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
        </tr>
        <tr>
            <td>Owns the feed</td>
            <td>The owns the feed and can perform all operations on it. The user manage feed settings, packages and user privileges.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
        </tr>
    </tbody>
</table>

## Adding a new feed owner

This *how to* describes how to assign a new owner to your feed.

1. Log into MyGet and navigate to the feed that you want to add a new owner to
2. Select **Feed Security**, located down the left hand side of the page  
![The MyGet Feed Security Window](Images/feed_security_main_page.png)  
3. Click the **Add feed privileges...** button
4. In the **Add Feed Privileges** window, enter the MyGet Username, or the email address of the user, that you would like to assign owner permission of the feed to  
![Add Feed Privileges Window](Images/feed_security_main_dialog.png)  
5. In the **Privileges** drop down list select **Owner of this feed**  
![Available Feed Security Options](Images/feed_security_options.png)  
6. You will receive the following warning  
![Add owner warning dialog](Images/feed_security_add_warning.png)  
This will make the chosen user the owner of the feed, and make you a co-owner.  If you are happy to proceed, click OK  
7. The new permissions take immediate effect  
![Successful change of feed owner](Images/feed_security_add_success.png)  
The new owner will receive an email informing them of the change.  At this point the new owner can choose to reduce your permissions to the feed, or revoke them completely.