Unity-XboxCtrlrInput
====================

__*Note: This is a work in progress! It's not ready for production use.*__

`XboxCtrlrInput` is a C# class for Unity3D that handles Xbox 360 controller input. It's used in the same way as Unity's `Input` class. The goals of `XboxCtrlrInput` are:

1.   To be able to make simple calls to Xbox controller input that works on Mac, Windows, and Linux (Ubuntu),
2.   And to be able to handle multiple Xbox controllers that works as you expect.

This repo includes `XboxCtrlrInput.cs` and an example project using Unity 4.2 to demonstrate Xbox controller input. 

`XboxCtrlrInput.cs` itself is not a Unity script and thus does not need to be attached to any GameObject. Unity's `Input` class works in a similar way. Also like `Input`, you cannot inherit from `XboxCtrlrInput` because it's a sealed class. `XboxCtrlrInput` should work on older versions of Unity that support joystick input.

For documentation, refer to the [wiki](https://github.com/JISyed/Unity-XboxCtrlrInput/wiki "XboxCtrlrInput Wiki") ( *also a work in progress* ).
