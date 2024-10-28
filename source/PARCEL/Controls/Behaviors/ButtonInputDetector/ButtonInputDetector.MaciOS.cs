using Foundation;
using PARCEL.Helpers;
using UIKit;

namespace PARCEL.Controls.Behaviors;

public partial class ButtonInputDetector : PlatformBehavior<View, UIView>
{
    #region Fields
    private bool canceled;

    private UIView? view;
    private PlatformGestureRecognizer gestureRecognizer;

    #endregion

    public ButtonInputDetector()
    {
        gestureRecognizer = new(this);

    }

    #region Methods
    protected override void OnAttachedTo(View bindable, UIView platformView)
    {
        base.OnAttachedTo(bindable, platformView);

        view = platformView;

        view.AddGestureRecognizer(gestureRecognizer);

        view.UserInteractionEnabled = true;
#if DEBUG
        DebugLogger.Log($"Behavior attached to {platformView}.");
#endif
    }

    protected override void OnDetachedFrom(View bindable, UIView platformView)
    {
        base.OnDetachedFrom(bindable, platformView);

        if (gestureRecognizer is not null && (platformView.GestureRecognizers?.Contains(gestureRecognizer) ?? false))
            platformView.RemoveGestureRecognizer(gestureRecognizer);

        platformView.UserInteractionEnabled = false;
        
        gestureRecognizer?.Dispose();
#if DEBUG
        DebugLogger.Log($"Behavior removed from {platformView}");
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
            DebugLogger.Log(evt);
#endif
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (!parent.canceled)
                parent.SendReleased();

            parent.canceled = false;
#if DEBUG
            DebugLogger.Log(evt);
#endif
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            if (touches.Last() is not UITouch touch || parent.view is null)
                return;

            parent.bounds = RectF.FromLTRB(
                0,
                0,
                (float)parent.view.Frame.Width,
                (float)parent.view.Frame.Height);

            if (!parent.bounds.Contains((float)touch.LocationInView(parent.view).X, (float)touch.LocationInView(parent.view).Y))
            {
                parent.SendExited();

                parent.canceled = true;

            }
#if DEBUG
                DebugLogger.Log($"touchPos: {touch.LocationInView(parent.view)}");
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
