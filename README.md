<div align="center">
<img alt="Logo" height="50" src="https://raw.githubusercontent.com/grocy/grocy/master/public/img/grocy_logo.svg?sanitize=true" />
<h3>grocy-desktop</h3>
<h4>A (Windows) desktop application wrapper for <a href="https://github.com/grocy/grocy">grocy</a><br>Created by <a href="https://github.com/berrnd">@berrnd</a></h4>
</div>

-----

## Questions / Help / Bug reporting / Feature requests
There is the [r/grocy subreddit](https://www.reddit.com/r/grocy) to connect with other grocy users and getting help.

If you've found something that does not work or if you have an idea for an improvement or new things which you would find useful, feel free to open a request on the [issue tracker](https://github.com/grocy/grocy-desktop/issues/new/choose) here (please remember: grocy-desktop is only the desktop application wrapper for [grocy](https://github.com/grocy/grocy), which has [its own issue tracker](https://github.com/grocy/grocy/issues/new/choose) for bug reports and feature requests).

Please don't send me private messages regarding grocy help. I check the issue tracker and the subreddit pretty much daily, but don't provide grocy support beyond that.

## Motivation
grocy is a selfhosted PHP web application, so normally runs on webservers. If you are not so familiar with the technical things regarding webservers, but just want to have grocy running like a normal (Windows) desktop application, this is what you need.

## How to install
- Classic installer
  - Just download and execute the [latest release setup](https://releases.grocy.info/latest-desktop), afterwards you will have a shortcut on your desktop which starts grocy.
- Microsoft Store  
<a href="//www.microsoft.com/store/apps/9nwb1trnnksf?cid=storebadge&ocid=badge"><img src="https://github.com/grocy/grocy-desktop/raw/master/.github/publication_assets/microsoft-store-badge-en.png" alt="Get it from Microsoft" width="150px" /></a>

Please note that user data is not automatically transfered when switching between the classic installer and the Microsoft Store version, please use the [backup/restore functionality](#how-to-backuprestore) to transfer your data.

## How to update
Just download and execute the [latest release installer](https://releases.grocy.info/latest-desktop). grocy and Barcode Buddy (if enabled) can also be updated separately (see the `grocy` and `Barcode Buddy` menu in the top menu bar).

## How to backup/restore
All user data can be exported and restored as a ZIP file (see the `grocy` and `Barcode Buddy` (if enabled) menu in the top menu bar).

## Localization
grocy-desktop is fully localizable - the default language is English (integrated into code), a German localization is always maintained by me.

You can easily help translating grocy-desktop on [Transfifex](https://www.transifex.com/grocy/grocy-desktop/dashboard/) if your language is incomplete or not available yet.

grocy-desktop and grocy will use automatically the localization based on your system language, if available.

## Barcode Buddy integration
[Barcode Buddy](https://github.com/Forceu/barcodebuddy) is a community contributed barcode helper tool for grocy and can be activated via `File -> Enable Barcode Buddy`.

## External access
Both, grocy and Barcode Buddy (if enabled), can be optionally accessed from external machines, external access can be enabled via `File -> Enable external access` (please accept the native Windows firewall question accordingly).
See the status bar for information about the URLs.

_This should only be used in trusted (local) networks._

## User data synchronization
If you want to have grocy-desktop on more than one machine, you can enable synchronization of all user data via `File -> Enable user data synchronization`.
All user data will be exported to the selected directory an closing the application and restored on application start (e. g. use any cloud-synced directory for that).

## Things worth to know

### How this works technically
grocy-desktop is a .Net Windows Forms application. It uses [CefSharp](https://github.com/cefsharp/CefSharp) as an integrated browser and utilizes [nginx](https://nginx.org) and [PHP](https://www.php.net/) (FastCGI)  to host grocy. The UWP app (`.appx` package to be distributed through the Microsoft Store) is built using [Desktop Bridge](https://docs.microsoft.com/en-us/windows/msix/desktop/source-code-overview), all needed dependencies/manifests are located in the `appx_dependencies` folder.

### What the installer does
The installer has bundled, beside the application itself and the CefSharp dependencies, a for grocy configured PHP and nginx version (in `embedded_dependencies/php.zip` / `embedded_dependencies/nginx.zip`) and the current grocy and Barcode Buddy release.

Everything will be unpacked to `%localappdata%\grocy-desktop` by default, the path can also be changed during the installation process. (This does not apply when running/installing the UWP app, normally from the Microsoft Store - UWP apps have their own default package locations.)

### What happens on start
grocy-desktop will do the following things and then opens the locally hosted instance in the integrated browser:
- Unpacking the dependency ZIP files, if needed, to `%appdata%\grocy-desktop\runtime-dependencies`
  - grocy to `%appdata%\grocy-desktop\grocy`
  - Barcode Buddy (if enabled) to `%appdata%\grocy-desktop\barcodebuddy`
  - When running the UWP app (normally installed from the Microsoft Store) the used paths are
    - `%userprofile%\.grocy-desktop\runtime-dependencies`
    - `%userprofile%\.grocy-desktop\grocy`
    - `%userprofile%\.grocy-desktop\barcodebuddy`
- Configuring grocy and Barcode Buddy (if enabled) in embedded mode (user data will be saved to `%appdata%\grocy-desktop\grocy-data` / `%appdata%\grocy-desktop\barcodebuddy-data`, these paths can be changed (see the `grocy` and `Barcode Buddy` (if enabled) menu in the top menu bar)
  - When running the UWP app (normally installed from the Microsoft Store), the default path used is `%userprofile%\.grocy-desktop\grocy-data` / `%userprofile%\.grocy-desktop\barcodebuddy-data`
  - The default ports used are `4010` for grocy and `4011` for Barcode Buddy, if they're already used, a random free port is used instead
- Starting nginx, bound to `localhost` if external access is disabled, otherwise bound to all network interfaces
- Starting PHP FastCGI, bound to `localhost` on a random free port

## Contributing / Say thanks
Any help is more than appreciated. Feel free to pick any open unassigned issue and submit a pull request, but please leave a short comment or assign the issue yourself, to avoid working on the same thing.

See https://grocy.info/#say-thanks for more ideas if you just want to say thanks.

## Roadmap
There is none. The progress of a specific bug/enhancement is always tracked in the corresponding issue, at least by commit comment references.

## Screenshots
![grocy-desktop](https://github.com/berrnd/grocy-desktop/raw/master/.github/publication_assets/grocy-desktop.png "grocy-desktop")

### How to build
You will need Visual Studio 2019. All dependencies are included, available via NuGet or will be downloaded at compile time (see build events).
The setup is built using [WiX Toolset](https://wixtoolset.org), which should be installed along with the [Wix Toolset Visual Studio 2019 Extension](https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2019Extension).

To build the `.appx` package (UWP app) you'll need the [Windows 10 SDK 10.0.18362.0](https://developer.microsoft.com/en-US/windows/downloads/windows-10-sdk) (this is done in the Post-build event of the `grocy-desktop-setup` project).

## License
The MIT License (MIT)
