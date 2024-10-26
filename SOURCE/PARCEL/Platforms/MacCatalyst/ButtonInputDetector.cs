using Foundation;
using PARCEL.Helpers;
using UIKit;

namespace PARCEL.Controls.Behaviors;

public partial class ButtonInputDetector : PlatformBehavior<View, UIView>
{
    #region Fields
    private PlatformGestureRecognizer? gestureRecognizer;

    #endregion

    #region Methods
    protected override void OnAttachedTo(View bindable, UIView platformView)
    {
        base.OnAttachedTo(bindable, platformView);

        gestureRecognizer = new PlatformGestureRecognizer(this);

        platformView.AddGestureRecognizer(gestureRecognizer);

        platformView.UserInteractionEnabled = true;
#if DEBUG
        DebugLogger.Log("Behavior attached.");
#endif
    }

    protected override void OnDetachedFrom(View bindable, UIView platformView)
    {
        base.OnDetachedFrom(bindable, platformView);

        if (gestureRecognizer is not null && (platformView.GestureRecognizers?.Contains(gestureRecognizer) ?? false))
            platformView.RemoveGestureRecognizer(gestureRecognizer);

        platformView.UserInteractionEnabled = false;

        if (gestureRecognizer is not null)
            gestureRecognizer?.Dispose();
#if DEBUG
        DebugLogger.Log("Behavior removed.");
#endif
    }

    #endregion

    #region Classes
    private class PlatformGestureRecognizer : UIGestureRecognizer
    {
        ButtonInputDetector parent;

        public PlatformGestureRecognizer(ButtonInputDetector parentBehavior)
        {
            parent = parentBehavior;

        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            parent.SendPressed();
#if DEBUG
            DebugLogger.Log(touches);
#endif
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            parent.SendReleased();
#if DEBUG
            DebugLogger.Log("touches");
#endif
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
#if DEBUG
            DebugLogger.Log(evt);
#endif
        }
        
    }

    #endregion

}
