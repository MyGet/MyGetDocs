# ADFS integration

The [MyGet Enterprise plan](https://www.myget.org/enterprise) provides support for external authentication modules to sign in to the web application. 

Have a look at the [authentication modules documentation](/docs/reference/authentication-modules) for more information about setting up integration with ADFS, Azure Active Directory, Google, GitHub, Microsoft Account, ...

## FAQ

**When disabling a user account in ADFS, will that also disable that user account's access to MyGet using API keys (access tokens)?**
  
**>** *No. ADFS is a federated identity service, so ADFS integration does not provide MyGet with access to user accounts. MyGet credentials, including username and access tokens, remain on MyGet and therefore are not disabled when disabling the user account in ADFS.*
