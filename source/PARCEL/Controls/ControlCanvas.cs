using System;

namespace PARCEL.Controls;

public class ControlCanvas : ContentView
{
    public static readonly BindableProperty DrawableProperty = BindableProperty.Create(nameof(Drawable), typeof(IDrawable), typeof(ControlCanvas), propertyChanged: OnDrawableChanged);

    private GraphicsView graphicsView;

    public ControlCanvas()
    {
        graphicsView = new GraphicsView();

        Content = graphicsView;

    }

    public IDrawable Drawable
    {
        get => (IDrawable)GetValue(DrawableProperty);
        set => SetValue(DrawableProperty, value);

    }

    private static void OnDrawableChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ControlCanvas instance)
        {
            return;
        }

        instance.graphicsView.Drawable = instance.Drawable;;
        instance.graphicsView.Invalidate();

    }

    public void Invalidate()
    {
        graphicsView.Invalidate();

    } 
    
}
