using Microsoft.Maui.Controls.Shapes;
using PARCEL.Interfaces;
using PARCEL.Converters;
using PARCEL.Helpers;
using PARCEL.Controls.Behaviors;
using PARCEL.Controls.Renderers;

namespace PARCEL.Controls;

public class IndicatorPARCEL : ControlPARCEL, IIndicatorPARCEL
{
    #region Fields
    private readonly Grid? controlContainer;

    public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Brush), typeof(IndicatorPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty IndicatorShapeProperty = BindableProperty.Create(nameof(IndicatorShape), typeof(Shape), typeof(IndicatorPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty IndicatorIconProperty = BindableProperty.Create(nameof(IndicatorIcon), typeof(Image), typeof(IndicatorPARCEL), propertyChanged: AddIcon);

    #endregion

    #region Constructors
    public IndicatorPARCEL()
	{
        try
        {
            ControlCanvas = ViewBuilder<GraphicsView>.BuildView(
                new()
                {
                    Triggers =
                    {
                        new DataTrigger(typeof(GraphicsView))
                        {
                            Binding =  new Binding(nameof(Renderer), converter: new IsNullConverter()),
                            Value = true,
                            Setters =
                            {
                                new()
                                {
                                    Property = GraphicsView.DrawableProperty,
                                    Value = GetDefaultRenderer()

                                }

                            }

                        },

                    }

                },
                [
                    new(GraphicsView.DrawableProperty, nameof(Renderer)),
                    
                ]);

            controlContainer = new()
            {
                BindingContext = this,
                Children = { ControlCanvas },
                Behaviors = 
                {
                    new ButtonInputDetector()
                    {
                        PressedCommand = new Command(OnPointerPressed),
                        ReleasedCommand = new Command(OnPointerReleased)

                    }

                }

            };

            Content = controlContainer;

            BackgroundColor = Colors.Transparent;

        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex);

        }

	}

    #endregion

    #region Events
    public event EventHandler? Pressed;
    public event EventHandler? Released;

    #endregion

    #region Properties
    public Brush IndicatorColor
    {
        get => (Brush)GetValue(IndicatorColorProperty); 
        set => SetValue(IndicatorColorProperty, value);

    }

	public Shape IndicatorShape
    {
        get => (Shape)GetValue(IndicatorShapeProperty);
        set => SetValue(IndicatorShapeProperty, value);

    }

	public Image IndicatorIcon
    {
        get => (Image)GetValue(IndicatorIconProperty);
        set => SetValue(IndicatorIconProperty, value);

    }

    #endregion

    #region Methods
    private void OnPointerPressed()
    {
        try
        {
            Pressed?.Invoke(this, EventArgs.Empty);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

    }

    private void OnPointerReleased()
    {
        try
        {
            Released?.Invoke(this, EventArgs.Empty);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

    }

    private static void AddIcon(BindableObject bindable, object oldValue, object newValue)
    {
        IndicatorPARCEL instance = (IndicatorPARCEL)bindable;

        try
        {
            if (instance.IndicatorIcon != null && (!instance.controlContainer?.Contains(instance.IndicatorIcon) ?? false))
                instance.controlContainer?.Add(instance.IndicatorIcon);

            RefreshView(bindable, oldValue, newValue);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

    }

    protected override IDrawable GetDefaultRenderer()
        => new IndicatorRenderer(this);
        
    #endregion

}