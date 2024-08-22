-----

<div align="center">
<img alt="Logo" height="50" src="https://raw.githubusercontent.com/grocy/grocy/master/public/img/logo.svg?sanitize=true" />
<h2>Grocy Desktop</h2>
<h3>A (Windows) desktop application wrapper for <a href="https://github.com/grocy/grocy">Grocy</a></h3>
<em><h4>This is a hobby project by <a href="https://github.com/berrnd">@berrnd</a></h4></em>
</div>

-----

## Questions / Help / Bug Reports / Feature Requests

- General help and usage questions &rarr;  [r/grocy subreddit](https://www.reddit.com/r/grocy)
- Bug Reports and Feature Requests &rarr; [Issue Tracker](https://github.com/grocy/grocy-desktop/issues/new/choose)

_Please don't send me private messages or call me regarding anything Grocy. I check the issue tracker and the subreddit pretty much daily, but don't provide any support beyond that._

## How to install

- Classic installer
  - Just download and execute the [latest release setup](https://releases.grocy.info/latest-desktop), afterwards you will have a "Grocy" shortcut on your desktop.
- Microsoft Store  
<a href="https://apps.microsoft.com/detail/9NWB1TRNNKSF"><img src="https://github.com/grocy/grocy-desktop/raw/master/.github/publication_assets/microsoft-store-badge-en.png" alt="Get it from Microsoft" width="150px" /></a>

Please note that user data is not automatically transfered when switching between the classic installer and the Microsoft Store version, please use the [backup/restore functionality](#how-to-backuprestore) to transfer your data.

## How to update

Just download and execute the [latest release installer](https://releases.grocy.info/latest-desktop). When using the Microsoft Store version, updates happen automatically as usual.

## How to backup/restore

All user data can be exported and restored as a ZIP file (see the `Grocy` and `Barcode Buddy` (if enabled) menu in the top menu bar).

## Localization

Grocy Desktop is fully localizable - the default language is English (integrated into code), a German localization is always maintained by me.

You can easily help translating Grocy on [Transifex](https://www.transifex.com/grocy/grocy-desktop/dashboard/) if your language is incomplete or not available yet.

Any translation which once reached a completion level of 70 % will be included in releases.

Grocy Desktop and Grocy will automatically use the localization based on your system language, if available.

## Barcode Buddy integration

[Barcode Buddy](https://github.com/Forceu/barcodebuddy) is a community contributed barcode helper tool for Grocy and can be activated via `File -> Enable Barcode Buddy`.

## External access

Both, Grocy and Barcode Buddy (if enabled), can be optionally accessed from external machines, external access can be enabled via `File -> Enable external access` (please accept the native Windows firewall question accordingly).
See the status bar for information about the URLs.

_This should only be used in trusted (local) networks._

## User data synchronization

If you want to have Grocy Desktop on more than one machine, you can enable synchronization of all user data via `File -> Enable user data synchronization`.
All user data will be exported to the selected directory an closing the application and restored on application start (e. g. use any cloud-synced directory for that).

## Motivation

Grocy is a selfhosted PHP web application, so normally runs on webservers. If you are not so familiar with the technical things regarding webservers, but just want to have Grocy running like a normal (Windows) desktop application, this is what you need.

## Things worth to know

### How this works technically

Grocy Desktop is a .Net Windows Forms application. It uses [CefSharp](https://github.com/cefsharp/CefSharp) as an integrated browser and utilizes [nginx](https://nginx.org) and [PHP](https://www.php.net/) (FastCGI)  to host Grocy. The UWP app (`.appx` package to be distributed through the Microsoft Store) is built using [Desktop Bridge](https://techcommunity.microsoft.com/t5/modern-work-app-consult-blog/desktop-bridge-8211-the-bridge-between-desktop-apps-and-the/ba-p/316488), all needed dependencies/manifests are located in the `appx_dependencies` folder.

### What the installer does

The installer has bundled, beside the application itself and the CefSharp dependencies, a for Grocy configured PHP and nginx version (in `embedded_dependencies/php.zip` / `embedded_dependencies/nginx.zip`) and the current Grocy and Barcode Buddy release.

Everything will be unpacked to `%programfiles%\grocy-desktop` by default, the path can also be changed during the installation process. (This does not apply when running/installing the UWP app, normally from the Microsoft Store - UWP apps have their own default package locations.)

### What happens on start

Grocy Desktop will do the following things and then opens the locally hosted instance in the integrated browser:
- Unpacking the dependency ZIP files, if needed, to `%appdata%\grocy-desktop\runtime-dependencies`
  - Grocy to `%appdata%\grocy-desktop\grocy`
  - Barcode Buddy (if enabled) to `%appdata%\grocy-desktop\barcodebuddy`
  - When running the UWP app (normally installed from the Microsoft Store) the used paths are
    - `%userprofile%\.grocy-desktop\runtime-dependencies`
    - `%userprofile%\.grocy-desktop\grocy`
    - `%userprofile%\.grocy-desktop\barcodebuddy`
- Configuring Grocy and Barcode Buddy (if enabled) in embedded mode (user data will be saved to `%appdata%\grocy-desktop\grocy-data` / `%appdata%\grocy-desktop\barcodebuddy-data`, these paths can be changed (see the `Grocy` and `Barcode Buddy` (if enabled) menu in the top menu bar)
  - When running the UWP app (normally installed from the Microsoft Store), the default path used is `%userprofile%\.grocy-desktop\grocy-data` / `%userprofile%\.grocy-desktop\barcodebuddy-data`
  - The default ports used are `4010` for Grocy and `4011` for Barcode Buddy, if they're already used, a random free port is used instead
- Starting nginx, bound to `localhost` if external access is disabled, otherwise bound to all network interfaces
- Starting PHP FastCGI, bound to `localhost` on a random free port

## Contributing / Say Thanks

Any help is welcome, feel free to contribute anything which comes to your mind or see <https://grocy.info/#say-thanks> if you just want to say thanks.

## Roadmap

There is none, this is a hobby project. The progress of a specific bug/enhancement is always tracked in the corresponding issue, at least by commit comment references.

## Screenshots

![Grocy Desktop](https://github.com/berrnd/grocy-desktop/raw/master/.github/publication_assets/grocy-desktop.png "Grocy Desktop")

## How to build

You will need Visual Studio 2022. All dependencies are included, available via NuGet or will be downloaded at compile time (see build events).
The setup is built using [WiX Toolset](https://wixtoolset.org), which should be installed along with the [Wix Toolset Visual Studio 2022 Extension](https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2022Extension).

To build the `.appx` package (UWP app) you'll need the [Windows SDK 10.0.22621.0](https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/) (this is done in the Post-build event of the `grocy-desktop-setup` project).

## License

The MIT License (MIT)
