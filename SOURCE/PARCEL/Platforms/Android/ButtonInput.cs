using View = Android.Views.View;
using Android.Views;
using AndroidX.Core.View;
using PARCEL.Helpers;
using Application = Android.App.Application;

namespace PARCEL.Platforms.Android;

public class ButtonInput : View
{
    public ButtonInput()
    {

    }

    public override bool OnDown(MotionEvent e)
    {
        DebugLogger.Log(e);

        return base.OnSingleTapUp(e);

    }

    public override bool OnSingleTapUp(MotionEvent e)
    {
        DebugLogger.Log(e);

        return base.OnSingleTapUp(e);

    }

    public struct GestureMap
    {
        Action Pressed;
        Action Released;

    }

}
