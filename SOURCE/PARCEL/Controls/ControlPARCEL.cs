using PARCEL.Interfaces;

namespace PARCEL.Controls;

public abstract class ControlPARCEL : ContentView, IControlPARCEL
{
    #region Fields
    public static readonly BindableProperty ControlCanvasProperty = BindableProperty.Create(nameof(ControlCanvas), typeof(GraphicsView), typeof(ControlPARCEL), propertyChanged: RefreshView);

    #endregion

    #region Constructors
    public ControlPARCEL() { }

    #endregion

    #region Properties
    public GraphicsView ControlCanvas
    {
        get => (GraphicsView)GetValue(ControlCanvasProperty);
        set => SetValue(ControlCanvasProperty, value);

    }

    #endregion

    #region Methods
    protected static void RefreshView(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ControlPARCEL)
        {
            (bindable as ControlPARCEL)?.ControlCanvas?.Invalidate();

        }

    }

    protected static void AdjustMargins(ref RectF rect, float offset)
    {
        rect.Left += offset;
        rect.Top += offset;
        rect.Width -= offset * 2;
        rect.Height -= offset * 2;

    }

    #endregion

}