![XboxControllerSVG](https://upload.wikimedia.org/wikipedia/commons/thumb/1/1b/Xbox_Controller.svg/200px-Xbox_Controller.svg.png)

XboxCtrlrInput for Unity
========================

Feel free to contribute. Requires at least Unity 5.3 (64-bit editor only) or greater. Unfortunately older versions of Unity are no longer supported (it may work or not, but your miliage will vary). 32-bit editor is no longer supported (but 32-bit game builds are still supported).

### Decription

`XboxCtrlrInput` is a wrapper written in C# for Unity3D that handles Xbox 360 controller input. It's used in the same way as Unity's [`Input`](http://docs.unity3d.com/Documentation/ScriptReference/Input.html) class. My reasons for starting this project can be read [here](http://jibransyed.wordpress.com/2013/09/07/the-motivation-behind-xboxctrlrinput/).

[`XboxCtrlrInput.cs`](https://github.com/JISyed/Unity-XboxCtrlrInput/blob/master/XboxCtrlrInput/Assets/Plugins/XboxCtrlrInput.cs) itself is not a Unity script and thus does not need to be attached to any GameObject, since it contains no [`MonoBehavior`](http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.html) derived classes in it.

`XboxCtrlrInput.cs` includes a C# namespace called `XboxCtrlrInput`. In that namespace there are three enumerations, [`XboxButton`](https://github.com/JISyed/Unity-XboxCtrlrInput/wiki/XboxButton), [`XboxDPad`](https://github.com/JISyed/Unity-XboxCtrlrInput/wiki/XboxDPad), and [`XboxAxis`](https://github.com/JISyed/Unity-XboxCtrlrInput/wiki/XboxAxis). Most importantly, there is a static class called [`XCI`](https://github.com/JISyed/Unity-XboxCtrlrInput/wiki/Coding-Reference#the-xci-class) that is used to get Xbox input.


### Goals

The goals of `XboxCtrlrInput` are:

1.   To be able to make simple calls to Xbox controller input that works on Mac, Windows, and Linux (Ubuntu),
2.   And to be able to handle multiple Xbox controllers that works as you expect.


### Installation

1. Download the latest .unitypackage [release](https://github.com/JISyed/Unity-XboxCtrlrInput/releases).
2. Import the package:  
   `Assets ▶ Import Package ▶ Custom Package...`
3. Update InputManager.asset file (to configure Xbox 360 input axis and buttons):  
   `Window ▶ XboxCtrlrInput ▶ Replace InputManager.asset...`


### How to Use

1. For any C# script where you want to use Xbox input, place `using XboxCtrlrInput;` at the top of the script under `using UnityEngine;`.

2. The `XboxCtrlrInput` namespace includes the class [`XCI`](https://github.com/JISyed/Unity-XboxCtrlrInput/wiki/Coding-Reference#the-xci-class), which you will use to get Xbox input, such as:
```csharp
bool didPressA = XCI.GetButton(XboxButton.A);
```

**Note for macOS users:** Be sure to install the latest stable version of the [360Controller drivers](https://github.com/360Controller/360Controller/releases).

**Note for Linux users:** All of my Linux testing was done on Ubuntu 13.04 64-bit. To test 32-bit Unity builds on a 64-bit OS, I ran `sudo apt-get install ia32-libs` in a terminal. I am using the default Xbox controller driver that came with Ubuntu, which is known as [xpad](http://lxr.free-electrons.com/source/drivers/input/joystick/xpad.c). I could not get Unity builds to cooperate with [xboxdrv](http://pingus.seul.org/~grumbel/xboxdrv/). Your milage may vary. For best results, make sure all your Xbox controllers are connected before testing anything.


### What Works?

If you want to find out what currently works (such as button mappings), refer to the [What Works](https://github.com/JISyed/Unity-XboxCtrlrInput/wiki/What-Works) page on the wiki. Compatability information can also be found there.


### Documentation

For documentation, including information about the included enumerations and methods, refer to the [Coding References](https://github.com/JISyed/Unity-XboxCtrlrInput/wiki/Coding-Reference) page on the wiki. A diagram showing all the labeled Xbox inputs can also be found there.

### Issues

To see the latest bugs and limitations, refer to the repo's [Issues](https://github.com/JISyed/Unity-XboxCtrlrInput/issues) section.

### License

Everything in this repository is public domain, including the code and documentation art. I encourage everyone to use and even modify the code to your own specifications, and of course to contribute to this repo by forking the project. See [`UNLICENSE.md`](https://github.com/JISyed/Unity-XboxCtrlrInput/blob/master/UNLICENSE.md).

### About the Example Project

`XboxCtrlrInput` includes a demo Unity project that requires Unity 4.3 or above. For more information about the demo, refer to the [Example Unity Project](https://github.com/JISyed/Unity-XboxCtrlrInput/wiki/The-Example-Unity-Project) page on the wiki.

![demoScreenshot](http://jibransyed.files.wordpress.com/2013/09/screen-shot-2013-09-09-at-3-24-24-pm.png?w=600)
