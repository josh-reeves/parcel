using CoreGraphics;
using Foundation;
using PARCEL.Helpers;
using UIKit;

namespace PARCEL.Controls.Behaviors;

public partial class DragDetector : PlatformBehavior<View, UIView>
{
    #region Fields
    private UIView? view;
    private PlatformGestureRecognizer? gestureRecognizer;

    #endregion

    #region Methods
    protected override void OnAttachedTo(View bindable, UIView platformView)
    {
        base.OnAttachedTo(bindable, platformView);

        gestureRecognizer = new(this);

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

        if (view is null)
            return;

        view.UserInteractionEnabled = false;

        if (gestureRecognizer is null)
            return;
        
        if (platformView.GestureRecognizers?.Contains(gestureRecognizer) ?? false)
            platformView.RemoveGestureRecognizer(gestureRecognizer);

        gestureRecognizer.Dispose();
#if DEBUG
        DebugLogger.Log($"Behavior detatched from {platformView}.");
#endif
    }

    #endregion

    #region Classes
    private class PlatformGestureRecognizer : UIGestureRecognizer
    {
        private readonly DragDetector parent;

        private CGPoint point;

        public PlatformGestureRecognizer(DragDetector parentBehavior)
            => parent = parentBehavior;

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            parent.dragEventArgs.Points.Clear();

            point = (touches.Last() as UITouch ?? new()).LocationInView(parent.view);       
            parent.dragEventArgs.Points.Add(new((float)point.X, (float)point.Y));

            parent.SendDragStarted();

        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            if (!parent.dragStarted)
                return;

            point = (touches.Last() as UITouch ?? new()).LocationInView(parent.view);
            parent.dragEventArgs.Points.Add(new((float)point.X, (float)point.Y));

            parent.SendDrag();

        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (!parent.dragStarted)
                return;

            point = (touches.Last() as UITouch ?? new()).LocationInView(parent.view);
            parent.dragEventArgs.Points.Add(new((float)point.X, (float)point.Y));

            parent.SendDragEnded();

        }

    }

    #endregion

}
