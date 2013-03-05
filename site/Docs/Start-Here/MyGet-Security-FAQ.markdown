# MyGet Security - Frequently Asked Questions

MyGet supports a lot of scenarios based on a set of configurable privileges. We have a few sections explaining you the most common scenarios, but feel free to configure your own custom ones, or [contact us](http://www.myget.org/Support) if you need further assistance.

## User Permissions

MyGet features a rich security model around your feeds. You, as a feed owner, always have the richest set of permissions possible. You can assign privileges to specific users also known on MyGet as well as to the anonymous “Everyone” user.

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
            <td style="text-align:center;vertical-align: middle">no*</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Can manage users and all packages for this feed</td>
            <td>Can be assigned to any registered MyGet user.<br />Allows the user to manage all packages and all user permissions on the feed.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
            <td style="text-align:center;vertical-align: middle">no*</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Can manage all packages for this feed</td>
            <td>Can be assigned to any registered MyGet user.<br />Allows the user to manage all packages on the feed.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
            <td style="text-align:center;vertical-align: middle">no*</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Can contribute own packages to this feed</td>
            <td>Can be assigned to any registered MyGet user.<br />Allows the user to publish packages on the feed.<br /><br />Users with this privilege will only be able to manage their own packages. This security setting is identical to the security settings on the official NuGet package source.</td>
            <td style="text-align:center;vertical-align: middle">yes</td>
            <td style="text-align:center;vertical-align: middle">no*</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Can consume this feed</td>
            <td>Can be assigned to any registered MyGet user as well as anonymous users.<br />Allows the user to consume packages.</td>
            <td style="text-align:center;vertical-align: middle">no</td>
            <td style="text-align:center;vertical-align: middle">no*</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Has no access to the feed</td>
            <td>Can be assigned to any registered MyGet user as well as anonymous users.<br />Denies access to the feed.</td>
            <td style="text-align:center;vertical-align: middle">N/A</td>
            <td style="text-align:center;vertical-align: middle">yes*</td>
        </tr>
    </tbody>
    <tfoot>
        <tr>
            <td colspan="4" style="font-size: 0.9em;">* Unless <i>&quot;Everyone&quot;</i> is assigned the <i>&quot;Has no access to the feed&quot;</i> privilege</td>
        </tr>
    </tfoot>
</table>

## Private Feeds

All of these privileges can be combined at will to create your unique security configuration.

A common scenario is the so-called <i>&quot;private feed&quot;</i> scenario, where you want to create a private feed to which only you can add and consume packages.

Configuring this one is easy and consists of the following list of privileges configured:

* **You** –  Owns this feed (this can not be changed)
* **Everyone** – Has no access to this feed

Now imagine you want to keep these settings and want to give some colleagues the privilege to consume packages *after authenticating* using their MyGet username and password. 
 
The following configuration should do the trick:

* **You** – Owns this feed (this can not be changed)
* **Colleague X** – Can consume this feed
* **Colleague Y** – Can consume this feed
* **Everyone** - Has no access to this feed

If you want to allow &quot;Colleague X&quot; to manage all packages on this feed, you can assign him. 
Optionally, the <i>&quot;Can manage users and all packages for this feed&quot;</i> privilege will enable him to manage user permisisons as well.

## Simulate the official NuGet.org feed

Another interesting scenario may be simulating the official NuGet package source: everyone can publish and manage their own packages on the feed while everyone can consume every other user’s packages.

This can be easily configured using the following permission set:

* **You** –  Owns this feed (this can not be changed)
* **Everyone** – Can contribute own packages to this feed

## Inviting other users to your feed

In order to give other users a certain privilege, they have to be invited to your MyGet feed. This can be done in the *Feed security* tab for your feed. This tab lists all users that currently have access to your feed as well as a list of &quot;pending&quot; invitations, that is: users that have been invited to your feed but haven’t confirmed yet.

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