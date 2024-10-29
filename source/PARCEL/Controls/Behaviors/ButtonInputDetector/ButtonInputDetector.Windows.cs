using PARCEL.Helpers;
using FrameworkElement = Microsoft.UI.Xaml.FrameworkElement;

namespace PARCEL.Controls.Behaviors;

public partial class ButtonInputDetector : PlatformBehavior<View, FrameworkElement>
{
    #region Constructors
    public ButtonInputDetector() { }

    #endregion

    #region Methods
    protected override void OnAttachedTo(View bindable, FrameworkElement platformView)
    {
        base.OnAttachedTo(bindable, platformView);

        platformView.PointerPressed += OnPointerPressed;
        platformView.PointerReleased += OnPointerReleased;
        platformView.PointerExited += OnPointerExited;
#if DEBUG
        DebugLogger.Log($"Behavior attacdhed to {platformView}");
#endif
    }

    protected override void OnDetachedFrom(View bindable, FrameworkElement platformView)
    {
        base.OnDetachedFrom(bindable, platformView);
    
        platformView.PointerPressed -= OnPointerPressed;
        platformView.PointerReleased -= OnPointerReleased;
        platformView.PointerExited -= OnPointerExited;
#if DEBUG
        DebugLogger.Log($"Behavior attacdhed to {platformView}");
#endif
    }

    private void OnPointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        SendPressed();

        canExecute = true;

    }

    private void OnPointerReleased(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        if (canExecute)
            SendReleased();
        
    }

    private void OnPointerExited(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        if (canExecute)
            SendExited();

        canExecute = false;
    
    }

    #endregion

}
