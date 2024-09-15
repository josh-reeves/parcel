using Microsoft.Maui.Controls.Shapes;
using PARCEL.Interfaces;
using PARCEL.Helpers;

using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace PARCEL.Controls;

public class IndicatorPARCEL : ControlPARCEL, IIndicatorPARCEL
{
    #region Fields
    private readonly PointerGestureRecognizer? pointerRecognizer;
    private readonly Grid? controlContainer;

    public static readonly BindableProperty IndicatorGaugeProperty = BindableProperty.Create(nameof(IndicatorGauge), typeof(IGaugePARCEL), typeof(IndicatorPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(IndicatorPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty IndicatorShapeProperty = BindableProperty.Create(nameof(IndicatorShape), typeof(Shape), typeof(IndicatorPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty IndicatorIconProperty = BindableProperty.Create(nameof(IndicatorIcon), typeof(Image), typeof(IndicatorPARCEL), propertyChanged: RefreshView);

    #endregion

    #region Constructors
    public IndicatorPARCEL()
	{
        try
        {
            pointerRecognizer = new()
            {
                PointerPressedCommand = new Command(OnPointerPressed),
                PointerReleasedCommand = new Command(OnPointerReleased)

            };

            ControlCanvas = new()
            {
                Drawable = new IndicatorPARCELRenderer(this)

            };

            controlContainer = new()
            {
                Children = { ControlCanvas },
                GestureRecognizers = { pointerRecognizer }

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

    protected override void LayoutChildren(double x, double y, double width, double height)
    {
        try
        {
            if (IndicatorIcon != null && (!controlContainer?.Contains(IndicatorIcon) ?? false))
                controlContainer?.Add(IndicatorIcon);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

        base.LayoutChildren(x, y, width, height);

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
                        (parent.IndicatorShape as RoundRectangle ?? new RoundRectangle()).CornerRadius.TopLeft, 
                        (parent.IndicatorShape as RoundRectangle ?? new RoundRectangle()).CornerRadius.TopRight,
                        (parent.IndicatorShape as RoundRectangle ?? new RoundRectangle()).CornerRadius.BottomLeft,
                        (parent.IndicatorShape as RoundRectangle ?? new RoundRectangle()).CornerRadius.BottomRight);

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

                default:

                    canvas.ResetState();

                    break;

            }

        }

        #endregion

    }

    #endregion

}