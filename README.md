# grocy-desktop
A (windows) desktop application wrapper for [https://github.com/berrnd/grocy](grocy)

## Motivation
grocy is a selfhosted PHP web application, so normally runs on webservers. If you are not so familiar with the technical things, but just want to have grocy running like a normal desktop application, this is what you need.

## How to install
Just download and execute the [https://releases.grocy.info/latest-desktop](latest release), afterwards you will have a shortcut on your desktop which starts grocy.

## How to update
Just download and execute the [https://releases.grocy.info/latest-desktop](latest release). grocy itself can also be updated separately (top emnu bar `grocy/Update`).

## Localization
grocy-desktop itself is not localized, but grocy is and will use automatically the localization based on your system language, if available.

## Things worth to know

### How this works technically
grocy-desktop is a .Net Windows Forms application. It uses [https://github.com/cefsharp/CefSharp](CefSharp) as an integrated browser and utilizes the in PHP integrated development server to host grocy.

### What the installer does
The installer has bundled, beside the application itself and the CefSharp dependencies, a for grocy configured PHP version (in `[https://github.com/berrnd/grocy-desktop/embedded_dependencies](embedded_dependencies/php.zip)`) and the current grocy release. grocy itself can also be updated separately, see above. Everything will be unpacked to `%localappdata%\grocy-desktop`.

### What happens on start
grocy-desktop will to the following things and then opens the locally hosted instance in the integrated browser:
- Unpacking the dependency ZIP files, if needed
- Configuring grocy in embedded mode (it will save its data in `%appdata%\grocy-desktop\grocy-data`)
- Starting a PHP development server on a free random port, bound to localhost

### How to build
You will need Visual Studio 2017. All dependencies are included, available via NuGet or will be downloaded at compile time (see build events).
The setup is built using [WiX Toolset](http://wixtoolset.org).

## License
The MIT License (MIT)
