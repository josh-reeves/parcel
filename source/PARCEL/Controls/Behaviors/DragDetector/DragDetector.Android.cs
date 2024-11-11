using Android.Views;
using PARCEL.Helpers;
using PlatformView = Android.Views.View;
using VirtualView = Microsoft.Maui.Controls.View;

namespace PARCEL.Controls.Behaviors;

public partial class DragDetector : PlatformBehavior<VirtualView, PlatformView>
{
    #region Fields
    private RectF bounds;

    #endregion

    #region Methods
    protected override void OnAttachedTo(VirtualView bindable, PlatformView platformView)
    {
        try
        {
            base.OnAttachedTo(bindable, platformView);

            platformView.Touch += OnTouch;
#if DEBUG
            DebugLogger.Log($"Behavior attached to {platformView}.");
#endif
        }
        catch (Exception ex)
        {
            DebugLogger.Log(ex);

        }

    }

    protected override void OnDetachedFrom(VirtualView bindable, PlatformView platformView)
    {
        try
        {
            base.OnDetachedFrom(bindable, platformView);

            platformView.Touch -= OnTouch;
#if DEBUG
            DebugLogger.Log($"Behavior detatched from {platformView}.");
#endif
        }
        catch (Exception ex)
        {
            DebugLogger.Log(ex);

        }

    }

    private void OnTouch(object? sender, PlatformView.TouchEventArgs e)
    {
        if (e.Event is null || sender is not PlatformView platformView)
            return;

        /* Get raw input X and Y coordinates relative to view and display density. 
         * If any object in the chain required to access display density is null, set display density to 1:*/
        float inputX = e.Event.GetX(),
              inputY = e.Event.GetY(),
              density = platformView.Context?.Resources?.DisplayMetrics?.Density ?? 1f;
        
        /* Account for the offset/innacuracy in the values returned by GetX() and GetY() relative to MAUI view when display density is greater than 1.
         * If the display density is 1, there is no change:*/
        inputX /= density;
        inputY /= density;

        bounds = RectF.FromLTRB(
            platformView.Left,
            platformView.Top,
            platformView.Right / density,
            platformView.Bottom / density);

        // Handle input:
        switch (e.Event.ActionMasked)
        {
            case MotionEventActions.Down:
                dragEventArgs.Points.Clear();
                dragEventArgs.Points.Add(new(inputX, inputY));

                SendDragStarted();

                break;

            case MotionEventActions.Move:
                if (!dragStarted)
                    return;

                dragEventArgs.Points.Add(new(inputX, inputY));

                SendDrag();

                break;

            case MotionEventActions.Up:
                if (!dragStarted)
                    return;

                dragEventArgs.Points.Add(new(inputX, inputY));

                SendDragEnded();

                break;

        }
#if DEBUG
        DebugLogger.Log(e.Event.Action);
#endif
    }

    #endregion

}