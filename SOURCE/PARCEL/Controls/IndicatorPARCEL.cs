using Microsoft.Maui.Controls.Shapes;
using PARCEL.Interfaces;
using PARCEL.Converters;
using Path = Microsoft.Maui.Controls.Shapes.Path;
using PARCEL.Helpers;

namespace PARCEL.Controls;

public class IndicatorPARCEL : ControlPARCEL, IIndicatorPARCEL
{
    #region Fields
    private readonly Grid? controlContainer;

    public static readonly BindableProperty IndicatorGaugeProperty = BindableProperty.Create(nameof(IndicatorGauge), typeof(IGaugePARCEL), typeof(IndicatorPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(IndicatorPARCEL), propertyChanged: RefreshView);
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
                                    Value = new IndicatorPARCELRenderer(this)

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
                GestureRecognizers =
                {
                    new PointerGestureRecognizer()
                    {
                        PointerPressedCommand = new Command(OnPointerPressed),
                        PointerReleasedCommand = new Command(OnPointerReleased)

                    }

                }

            };

            Content = controlContainer;

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
    public IGaugePARCEL IndicatorGauge
    {
        get => (IGaugePARCEL)GetValue(IndicatorGaugeProperty); 
        set => SetValue(IndicatorGaugeProperty, value);

    }

    public Color IndicatorColor
    {
        get => (Color)GetValue(IndicatorColorProperty); 
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

    #endregion

    #region Classes
    private class IndicatorPARCELRenderer : IDrawable
	{
        #region Fields
        private const float offset = 1.0f;

        private readonly IndicatorPARCEL parent;

        #endregion

        #region Constructors
        public IndicatorPARCELRenderer(IndicatorPARCEL parentControl)
		{
			parent = parentControl;

		}

        #endregion

        #region Methods
		public void Draw(ICanvas canvas, RectF rect)
		{
			parent.IndicatorColor ??= parent.IndicatorShape.BackgroundColor; // Set IndicatorColor to IndicatorShape.BackgroundColor if it is currently null;

            canvas.FillColor = parent.IndicatorColor;

			switch (parent.IndicatorShape)
			{
				case Rectangle:
                    canvas.FillRectangle(rect.Left + offset, rect.Top + offset, rect.Width - (offset * 2), rect.Height - (offset *2));

                    break;

                case RoundRectangle:
                    canvas.FillRoundedRectangle(
                        new Rect()
                        {
                            Left = rect.Left + offset,
                            Top = rect.Top + offset,
                            Width = rect.Width - (offset * 2),
                            Height = rect.Height - (offset * 2)

                        },
                        (parent.IndicatorShape as RoundRectangle)?.CornerRadius.TopLeft ?? 0, 
                        (parent.IndicatorShape as RoundRectangle)?.CornerRadius.TopRight ?? 0,
                        (parent.IndicatorShape as RoundRectangle)?.CornerRadius.BottomLeft ?? 0,
                        (parent.IndicatorShape as RoundRectangle)?.CornerRadius.BottomRight ?? 0);

                    break;

                case Ellipse:
                    canvas.FillEllipse(rect.Left + offset, rect.Top + offset, rect.Width - (offset * 2), rect.Height - (offset * 2));

                    break;

                case Polygon:
                    canvas.FillPath((parent.IndicatorShape as Polygon)?.GetPath());

                    break;

                case Path:
                    canvas.FillPath((parent.IndicatorShape as Path)?.GetPath());

                    break;

            }

        }

        #endregion

    }

    #endregion

}