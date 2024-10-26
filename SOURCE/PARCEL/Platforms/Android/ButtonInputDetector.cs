using Android.Views;
using PARCEL.Helpers;
using PlatformView = Android.Views.View;
using VirtualView = Microsoft.Maui.Controls.View;

namespace PARCEL.Controls.Behaviors;

public partial class ButtonInputDetector : PlatformBehavior<VirtualView, PlatformView>
{
    #region Methods
    protected override void OnAttachedTo(VirtualView bindable, PlatformView platformView)
    {
        try
        {
            base.OnAttachedTo(bindable, platformView);

            platformView.Touch += OnTouch;

            platformView.Clickable = true;
#if DEBUG
            DebugLogger.Log("Behavior attached.");
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
#if DEBUG
                DebugLogger.Log(e.Event.Action);
#endif
                break;

            case MotionEventActions.Up:
#if DEBUG
                DebugLogger.Log(e.Event.Action);
#endif
                break;

        }

    }

    #endregion

}
