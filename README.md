XboxCtrlrInput for Unity
========================

__*Note: This is a work in progress! It's not ready for production use.*__
Feel free to contribute. You'll need at least Unity 4.2 or greater.

### Decription

`XboxCtrlrInput` is a C# class for Unity3D that handles Xbox 360 controller input. It's used in the same way as Unity's `Input` class. It's not a Unity script and thus does not need to be attached to any GameObject. Unity's `Input` class works in a similar way.

This repo includes `XboxCtrlrInput.cs` and an example project using Unity 4.2 to demonstrate Xbox controller input. 


### Goals

The goals of `XboxCtrlrInput` are:

1.   To be able to make simple calls to Xbox controller input that works on Mac, Windows, and Linux (Ubuntu),
2.   And to be able to handle multiple Xbox controllers that works as you expect.


### How To Use

Download the [XboxCtrlrInput class file](https://github.com/JISyed/Unity-XboxCtrlrInput/blob/master/XboxCtrlrInput/Assets/XboxCtrlrInputPackage/XboxCtrlrInput.cs) and then put it into your Unity project under the `Assets/` folder. It can be anywhere in that folder.

Next, take the [copy of the Unity Input Manager](https://github.com/JISyed/Unity-XboxCtrlrInput/blob/master/XboxCtrlrInput/Assets/XboxCtrlrInputPackage/InputManagerCopy.txt) and replace the contents of your project's `/ProjectSettings/InputManager.asset` with the contents of the copy.

It's not a `MonoBehavior`, so no need to attach it like a script.

**Note for Mac users:** Be sure to install [Tattie Bogle drivers](http://tattiebogle.net/index.php/ProjectRoot/Xbox360Controller/OsxDriver).  


### Documentation

For documentation, refer to the [wiki](https://github.com/JISyed/Unity-XboxCtrlrInput/wiki#reference "XboxCtrlrInput Wiki").

### Issues

Refer to the [Issues page](https://github.com/JISyed/Unity-XboxCtrlrInput/issues).
