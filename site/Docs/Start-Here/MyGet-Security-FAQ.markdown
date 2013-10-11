# MyGet Security - Frequently Asked Questions

MyGet features a rich security model around your feeds. You, as a feed owner, always have the richest set of permissions possible. You can assign privileges to specific users on MyGet using their email address or username.

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

## Managing user permissions

After inviting a user to your feed, you can change the privileges previously assigned. For example, a user who could previously only consume packages may now be granted the privilege of contributing packages to your feed. Also, a user who could previously manage all packages on the feed can be locked down into a privilege where he can only consume packages and no longer manage them.

The *Feed security* tab for your feed lists all users that currently have access to your feed as well as a list of users that have been invited to your feed but haven’t confirmed their privileges yet. The dropdown next to a user’s name allows you to modify the currently assigned privilege.

**Note:** When assigning the <i>&quot;Has no access to this feed&quot;</i> privilege to a certain user, the user will be removed from the list of users. If afterwards you want to assign a dfferent privilege to this user, the user should be sent a new invitation using the *Add feed privileges...* button.

## User Permissions reference

The table below lists all possible permissions and their meaning:

<table>
	<thead>
        <tr>
            <th>Permission</th>
            <th>Description</th>
            <th>MyGet account required?</th>
            <th>Requires username/password to consume the feed?</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td style="vertical-align: top;">Owns the feed</td>
            <td>Can only be assigned to the feed owner.<br />Can manage this feed, its users and its packages.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
            <td style="text-align:center;vertical-align: middle">no</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Can manage users and all packages for this feed</td>
            <td>Allows the user to manage all packages and all user permissions on the feed. Similar to "Owns the feed" except that deleting the feed is not permitted.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
            <td style="text-align:center;vertical-align: middle">no</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Can manage all packages for this feed</td>
            <td>Allows the user to manage all packages on the feed.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
            <td style="text-align:center;vertical-align: middle">no</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Can contribute own packages to this feed</td>
            <td>Allows the user to publish packages on the feed.<br /><br />Users with this privilege will only be able to manage their own packages. This security setting is identical to the security settings on the official NuGet package source.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
            <td style="text-align:center;vertical-align: middle">no</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Can consume this feed</td>
            <td>Allows the user to consume packages.</td>
            <td style="text-align:center;vertical-align: middle">no</td>
            <td style="text-align:center;vertical-align: middle">no</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Has no access to the feed</td>
            <td>Denies access to the feed.</td>
            <td style="text-align:center;vertical-align: middle">N/A</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
        </tr>
    </tbody>
</table>