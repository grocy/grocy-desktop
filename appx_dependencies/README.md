### grocy-desktop_developer_cert.pfx:

This is a self signed certificate used to sign the Appx package during the Post-build event of the `grocy-desktop-setup` project. The PFX password is `123456`. Import this certificate into the `LocalMachine\Trusted People` certificate store to be able to sideload the created Appx package. This certificate is not needed or used when distributing grocy-desktop through the Microsoft Store.
