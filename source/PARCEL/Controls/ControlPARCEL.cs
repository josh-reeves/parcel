using PARCEL.Helpers;
using PARCEL.Interfaces;

namespace PARCEL.Controls;

public abstract class ControlPARCEL : ContentView, IControlPARCEL
{
    #region Fields
    public static readonly BindableProperty RendererProperty = BindableProperty.Create(nameof(Renderer), typeof(IDrawable), typeof(ControlPARCEL), propertyChanged: SetRenderer);

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
    private static void SetRenderer(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ControlPARCEL instance || instance.ControlCanvas is null)
            return;

        instance.ControlCanvas.Drawable = instance.Renderer;

        RefreshView(bindable, oldValue, newValue);

    }

    /// <summary>
    /// Refreshes the ControlCanvas of bindable. Invocable via BindableProperty delegates and accessible by sub-classes.
    /// </summary>
    /// <param name="bindable">ControlPARCEL</param>
    /// <param name="oldValue">Old value of ControlCanvas.</param>
    /// <param name="newValue">New value of ControlCanvas.</param>
    protected static void RefreshView(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ControlPARCEL instance)
            return;

        instance.ControlCanvas?.Invalidate();
#if DEBUG
        DebugLogger.Log($"{bindable} refreshed");
#endif
    }

    protected abstract IDrawable GetDefaultRenderer();

    #endregion

}