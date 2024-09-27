using PARCEL.Interfaces;

namespace PARCEL.Controls;

public abstract class ControlPARCEL : ContentView, IControlPARCEL
{
    #region Fields
    public static readonly BindableProperty RendererProperty = BindableProperty.Create(nameof(Renderer), typeof(IDrawable), typeof(ControlPARCEL), propertyChanged: RefreshView);

    #endregion

    #region Constructors
    public ControlPARCEL() { }

    #endregion

    #region Properties
    protected GraphicsView? ControlCanvas { get; set; }

    public IDrawable Renderer
    {
        get => (IDrawable)GetValue(RendererProperty);
        set => SetValue(RendererProperty, value);

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

    /// <summary>
    /// Returns a new RectF whose measurements have been reduced by the value of offset.
    /// This was built primarily as a workaround for this issue: https://github.com/dotnet/maui/issues/8629
    /// Presumably, if MS fixes the bug so that canvas edges no longer clip on Windows/iOS, this won't be needed anymore.
    /// </summary>
    /// <param name="rect">The RectF that will act as a base for the new RectF the method returns.</param>
    /// <param name="offset">The value by which to reduce the perimeter of the new RectF the method returns.</param>
    /// <returns></returns>
    protected static RectF GetSafeMargins(RectF rect, float offset)
    {
        return new RectF()
        {
            Left = rect.Left + offset,
            Top = rect.Top + offset,
            Width = rect.Width - (offset * 2),
            Height = rect.Height - (offset * 2)

        };

    }

    #endregion

}