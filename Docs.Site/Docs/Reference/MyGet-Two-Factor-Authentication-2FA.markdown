# MyGet Two-Factor Authentication (2FA)

With our most recent release, MyGet introduced two-factor authentication (2FA) for MyGet.org and MyGet Enterprise accounts. 2FA adds a second layer of security to your MyGet account, so that the only way someone can log into your MyGet account is if they know both your password and have access to the authentication code on your phone.

MyGet 2FA supports authentication via one-time password (OTP) mobile apps like Google Authenticator or Authy. For security and accessibility reasons, we do not support SMS-based authentication. We strongly encourage you to enable 2FA for your account in MyGet.org, or, if you are a MyGet Enterprise customer, to enforce 2FA for users across your Enterprise MyGet instance.

## Enable 2FA for your MyGet.org Account

You can find a link to enable 2FA in your [account settings][1]. 

1. Download a time-based OTP app (such as <a href="https://support.google.com/accounts/answer/1066447?co=GENIE.Platform%3DAndroid&hl=en" target="_blank" rel="noopener">Google Authenticator</a> or <a href="https://authy.com/" target="_blank" rel="noopener">Authy</a>) to your mobile device.

2. Click on your login name in upper-right corner of any screen and select “View profile” from the drop-down menu.

3. Select TWO-FACTOR AUTHENTICATION from the sidebar menu:

![Enable 2FA.](/docs/reference/Images/2fabasic.png)

4. Type in your password and press OK. (This makes sure that it’s you who actually wants to modify the account.)

5. On the Two-Factor Authentication page, check the box to enable or disable 2FA for your account.

6. Using your authenticator app, scan the QR code that appears on your screen and enter the six-digit code generated your app.

![Set 2FA.](/docs/reference/Images/set2fa.png)

7. Click “Save.” 2FA is now active for your account!

8. Copy your recovery codes and save them to a secure location. They can help you log into your MyGet account if you lose access and are unable to use with your 2FA app to authenticate.

![Show recovery codes.](/docs/reference/Images/recovery-codes-open.png)

## Signing in with 2FA

Once you have activated 2FA, you will need to use it when signing into your MyGet account online.
Click "Sign In" on MyGet.org, and enter your login name and password. In the next screen you will see a prompt asking you to provide a code from your authenticator app.

![Show recovery codes.](/docs/reference/Images/2fa-auth-code-sign-in-full.png)

Now, type the code, Sign in and… You’re signed into MyGet.


## Recovery codes

In the (hopefully, rare) situation when you cannot use your authenticator app and are locked out of MyGet, MyGet provides recovery codes. Each code can be used one time to authenticate during sign-in without having to use your 2FA app.

The codes can be seen under Two-Factor Authentication tab in your profile. You can retrieve them by clicking “Show your recovery codes” and re-entering your MyGet password. Be sure to save your recovery codes in a secure location. Keep track of which codes have been used, as you will not be able to distinguish used and unused codes from within your MyGet settings.

1. If you cannot use your 2FA app to sign in, “Need to use your recovery code?” when prompted for an authentication code.

![Sign in with 2FA.](/docs/reference/Images/2fa-auth-code-sign-in.png)

2. Then enter one of your ten recovery codes in the next screen.

![Sign in with Recovery Codes.](/docs/reference/Images/userecoverycodetosignin.png)

3. If you enter a correct code, you will be logged in to MyGet. You will not be able to use the same recovery code to sign in again.
If you lose track your original recovery codes, they can be regenerated. However, to maintain the security of your account, please use these codes for recovery in emergency situations only.

To reset your list of recovery codes, access your <a href="https://www.myget.org/profile/Me#!/TwoFA" target="_blank" rel="noopener">Two-Factor Authentication</a> settings, click “Show recovery codes”, and press the “GET NEW CODES” button.

Because this will erase your previous set of recovery codes, you will be prompted to accept the changes to your settings before proceeding.

![Generate new codes.](/docs/reference/Images/generate-new-codes.png)

If you press OK, the new codes will be generated and the old ones will not work anymore. Please use that with caution!

## Enforce 2FA for your organization with MyGet Enterprise

With MyGet Enterprise, you can enforce 2FA for anyone with access to your organization’s MyGet feed. To enable organization-level 2FA, you need to have Administrator rights for a MyGet Enterprise subscription (so that the Enterprise Admin page is visible to you from your profile menu).

1. Log into your MyGet Enterprise account from your web browser, and click on your profile icon in the upper-right corner.

2. From the dropdown menu, select Enterprise Admin, and then click the Accounts tab on the sidebar menu. Scroll down to the Two-Factor Authentication section to enable 2FA for all users of your enterprise tenant.

![Enforce 2FA on MyGet Enterprise.](/docs/reference/Images/Enterprise-Admin-Account-set-2fa-private-tenant.png)

If you enable this option and click "Save", users belonging to your organization will be prompted to configure 2FA for their account the first time they log into your MyGet Enterprise space.

![Oblige all users with access to your MyGet Enterprise instance to configure 2FA the first time they log in.](/docs/reference/Images/tenant-obligatory-2fa-for-account-without-2fa-set.png)

When they try to sign in, they will automatically be presented a QR code that they must use with a 2FA app to configure 2FA for their account. This will allow them to sign in. Any time they attempt to sign in after that, they will need to use their 2FA app to enter a one-time authentication code.

[1]: https://www.myget.org/profile/Me#!/TwoFA
