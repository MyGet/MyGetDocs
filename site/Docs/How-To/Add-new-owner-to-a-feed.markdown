# Add new owner to a feed
Within the MyGet User Interface it is possible to take full control of the permissions that are assigned to users that have access to one of your feeds.  A complete list of all the privileges that can be assigned to a user can be found on the [Feed Security Reference page](../reference/feed-security "Feed Security Reference Page").  This *how to* describes how to assign a new owner to your feed.

## Adding a new feed owner

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