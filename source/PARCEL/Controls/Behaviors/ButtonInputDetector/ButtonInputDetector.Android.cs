using Android.Views;
using PARCEL.Helpers;
using PlatformView = Android.Views.View;
using VirtualView = Microsoft.Maui.Controls.View;


namespace PARCEL.Controls.Behaviors;

public partial class ButtonInputDetector : PlatformBehavior<VirtualView, PlatformView>
{
    #region Fields
    private bool canceled;

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

#if DEBUG
        DebugLogger.Log($"Behavior detatched from {platformView}.");
#endif
    }

    private void OnTouch(object? sender, PlatformView.TouchEventArgs e)
    {
        if (e.Event is null || sender is not PlatformView platformView)
            return;

        bounds = RectF.FromLTRB(
            platformView.Left,
            platformView.Top,
            platformView.Right,
            platformView.Bottom);

        float inputX = bounds.Left + e.Event.GetX(),
              inputY = bounds.Top + e.Event.GetY();

        if (!canceled && !bounds.Contains(inputX, inputY))
        {
            SendExited();

            canceled = true;
            
        }

        switch (e.Event.ActionMasked)
        {
            case MotionEventActions.Down:
                SendPressed();

                break;

            case MotionEventActions.Up:
                if (!canceled)
                    SendReleased();

                canceled = false;

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
