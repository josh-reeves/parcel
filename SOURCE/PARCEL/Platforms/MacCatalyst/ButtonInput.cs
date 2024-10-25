﻿using Foundation;
using PARCEL.Helpers;
using UIKit;

namespace PARCEL.Platforms.MaciOS;

public class ButtonInput : UIView
{

    public override void TouchesBegan(NSSet touches, UIEvent? evt)
    {
        DebugLogger.Log(touches);

        base.TouchesBegan(touches, evt);
    }

    public struct GestureMap
    {
        Action Pressed;
        Action Released;

    }

}
