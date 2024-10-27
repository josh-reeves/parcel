using Android.Views;
using PARCEL.Helpers;
using PlatformView = Android.Views.View;
using VirtualView = Microsoft.Maui.Controls.View;
using RectF = Android.Graphics.RectF;

namespace PARCEL.Controls.Behaviors;

public partial class ButtonInputDetector : PlatformBehavior<VirtualView, PlatformView>
{
    #region Fields
    private RectF bounds;

    #endregion

    #region Constructors
    public ButtonInputDetector()
    {
        bounds = new();

    }

    #endregion

    #region Methods
    protected override void OnAttachedTo(VirtualView bindable, PlatformView platformView)
    {
        try
        {
            base.OnAttachedTo(bindable, platformView);
            
            platformView.Touch += OnTouch;
#if DEBUG
            DebugLogger.Log($"Behavior attached to {platformView}");
#endif
        }
        catch (Exception ex)
        {
            DebugLogger.Log(ex);
        }

    }

    protected override void OnDetachedFrom(VirtualView bindable, PlatformView platformView)
    {
        base.OnDetachedFrom(bindable, platformView);

        platformView.Touch -= OnTouch;

        bounds.Dispose();
#if DEBUG
        DebugLogger.Log("Behavior detatched.");
#endif
    }

    private void OnTouch(object? sender, PlatformView.TouchEventArgs e)
    {
        if (sender is null)
            return;

        switch (e.Event?.ActionMasked)
        {
            case MotionEventActions.Down:
                SendPressed();

                break;

            case MotionEventActions.Up:
                SendReleased();

                break;

            case MotionEventActions.Move:
                if (sender is not PlatformView platformView)
                    return;

                bounds.Set(
                    platformView.Left,
                    platformView.Top,
                    platformView.Right,
                    platformView.Bottom);

                float inputX = bounds.Left + e.Event.GetX(),
                      inputY = bounds.Top + e.Event.GetY();

                if (!bounds.Contains(inputX, inputY))
                    SendExited();
#if DEBUG
                DebugLogger.Log($"inputCoords: {inputX}, {inputY} | preCalc: {e.Event.GetX()}, {e.Event.GetY()}");
                DebugLogger.Log($"bounds: {bounds.Left}, {bounds.Top}, {bounds.Right}, {bounds.Bottom} | preCalc: {platformView.Left}, {platformView.Top}, {platformView.Right}, {platformView.Bottom}");
#endif
                break;

            default:

                return;

        }
#if DEBUG
        if (e.Event is not null)
            DebugLogger.Log(e.Event.Action);
#endif
    }

    #endregion

}
