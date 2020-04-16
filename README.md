# grocy-desktop
A (Windows) desktop application wrapper for [grocy](https://github.com/grocy/grocy)

## Motivation
grocy is a selfhosted PHP web application, so normally runs on webservers. If you are not so familiar with the technical things regarding webservers, but just want to have grocy running like a normal (Windows) desktop application, this is what you need.

## How to install
- Classic installer
  - Just download and execute the [latest release](https://releases.grocy.info/latest-desktop), afterwards you will have a shortcut on your desktop which starts grocy.
- Microsoft Store  
<a href="//www.microsoft.com/store/apps/9nwb1trnnksf?cid=storebadge&ocid=badge"><img src="https://assets.windowsphone.com/85864462-9c82-451e-9355-a3d5f874397a/English_get-it-from-MS_InvariantCulture_Default.png" alt="Get it from Microsoft" width="150px" /></a>

Please note that the user data is not automatically transfered when switching between the classic installer and the Microsoft Store version, please use the [backup/restore functionality](#how-to-backuprestore) to transfer your data.

## How to update
Just download and execute the [latest release](https://releases.grocy.info/latest-desktop). grocy and Barcode Buddy (if enabled) can also be updated separately (top menu bar `grocy` and `Barcode Buddy`).

## How to backup/restore
All user data can be exported and restored as a ZIP file (see the `grocy` and `Barcode Buddy` (if enabled) menu in the top menu bar).

## Localization
grocy-desktop is fully localizable - the default language is English (integrated into code), a German localization is always maintained by me.
You can easily help translating grocy at https://www.transifex.com/grocy/grocy-desktop, if your language is incomplete or not available yet.
(grocy-desktop and grocy will use automatically the localization based on your system language, if available.)

## Barcode Buddy integration
Barcode Buddy (https://github.com/Forceu/barcodebuddy) is a barcode helper tool for grocy and can be activated via `File -> Enable Barcode Buddy`.

## External access
Both grocy and Barcode Buddy (if enabled) can be optionally accessed from external machines, external access can be enabled via `File -> Enable external access` (please accept the native Windows firewall question accordingly).
See the status bar for information about the URLs.

## User data synchronization
If you want to have grocy-desktop on more than one machine, you can enable the synchronization of all user data via `File -> Enable user data synchronization`.
All user data will be exported to the selected directory an closing the application and restored on application start (e. g. use any cloud-synced directory for that).

## Things worth to know

### How this works technically
grocy-desktop is a .Net Windows Forms application. It uses [CefSharp](https://github.com/cefsharp/CefSharp) as an integrated browser and utilizes [nginx](https://nginx.org) to host grocy. The UWP app (Appx package to be distributed through the Microsoft Store) is built using the [Desktop Bridge](https://developer.microsoft.com/en-us/windows/bridges/desktop), all needed dependencies/manifests are located in the `appx_dependencies` folder.

### What the installer does
The installer has bundled, beside the application itself and the CefSharp dependencies, a for grocy configured PHP and nginx version (in `embedded_dependencies/php.zip`) and the current grocy and Barcode Buddy release. grocy and Barcode Buddy (if enabled) can also be updated separately, see above. Everything will be unpacked to `%localappdata%\grocy-desktop` by default, the path can also be changed during the installation process. (This does not apply when running/installing the UWP app, normally from the Microsoft Store - UWP apps have their own default package locations.)

### What happens on start
grocy-desktop will do the following things and then opens the locally hosted instance in the integrated browser:
- Unpacking the dependency ZIP files, if needed, to `%appdata%\grocy-desktop\runtime-dependencies` and grocy itself to `%appdata%\grocy-desktop\grocy`
  - When running the UWP app (normally installed from the Microsoft Store), the used paths are `%userprofile%\.grocy-desktop\runtime-dependencies` and `%userprofile%\.grocy-desktop\grocy`
- Configuring grocy in embedded mode (it will save its data in `%appdata%\grocy-desktop\grocy-data`, this path can be changed (top menu bar `File -> Configure/change data location`)
  - When running the UWP app (normally installed from the Microsoft Store), the default path is `%userprofile%\.grocy-desktop\grocy-data`
- Starting nginx, bound to `localhost` if external access is disabled, otherwise bound to all network interfaces
  - The default ports used are `4010` for grocy and `4011`, if they're already used, a random free port is used instead

## Screenshots
![grocy-desktop](https://github.com/berrnd/grocy-desktop/raw/master/publication_assets/grocy-desktop.png "grocy-desktop")

### How to build
You will need Visual Studio 2019. All dependencies are included, available via NuGet or will be downloaded at compile time (see build events).
The setup is built using [WiX Toolset](http://wixtoolset.org), which should be installed along with the [Wix Toolset Visual Studio 2019 Extension](https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2019Extension).
To build the Appx package (UWP app) you will need the [Windows 10 SDK 10.0.18362.0](https://developer.microsoft.com/en-US/windows/downloads/windows-10-sdk) (this is done in the Post-build event of the `grocy-desktop-setup` project).

## License
The MIT License (MIT)
