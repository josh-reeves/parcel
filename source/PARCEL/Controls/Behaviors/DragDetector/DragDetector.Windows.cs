using Microsoft.Maui.Platform;
using FrameworkElement = Microsoft.UI.Xaml.FrameworkElement;

namespace PARCEL.Controls.Behaviors;

public partial class DragDetector : PlatformBehavior<View, FrameworkElement>
{
    #region Fields
    private FrameworkElement? view;

    #endregion

    #region Methods
    protected override void OnAttachedTo(View bindable, FrameworkElement platformView)
    {
        base.OnAttachedTo(bindable, platformView);

        view = platformView;

        view.PointerPressed += OnDragStarted;
        view.PointerMoved += OnDrag;
        view.PointerReleased += OnDragEnded;

        dragEventArgs ??= new();

    }

    protected override void OnDetachedFrom(View bindable, FrameworkElement platformView)
    {
        base.OnDetachedFrom(bindable, platformView);

        if (view is not FrameworkElement)
            return;

        view.PointerPressed -= OnDragStarted;
        view.PointerMoved -= OnDrag;
        view.PointerReleased -= OnDragEnded;

        view = null;

    }

    private void OnDragStarted(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        dragEventArgs.Points.Clear();
        dragEventArgs.Points.Add(e.GetCurrentPoint(view).Position.ToPoint());

        SendDragStarted();

    }

    private void OnDrag(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        if (!dragStarted)
            return;

        dragEventArgs.Points.Add(e.GetCurrentPoint(view).Position.ToPoint());

        SendDrag();

    }

    private void OnDragEnded(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        if (!dragStarted)
            return;

        dragEventArgs.Points.Add(e.GetCurrentPoint(view).Position.ToPoint());

        SendDragEnded();

    }

    #endregion

}
