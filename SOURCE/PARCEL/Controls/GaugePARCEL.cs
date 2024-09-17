using System.Windows.Input;
using PARCEL.Interfaces;
using PARCEL.Helpers;
using PARCEL.Converters;

namespace PARCEL.Controls;

public class GaugePARCEL : ControlPARCEL, IGaugePARCEL
{
    #region Fields
    private readonly Grid? controlContainer;
    private readonly Label? valueLabel;
    private readonly DataTrigger? labelFontSizeDataTrigger,
                                  labelDisplayDataTrigger;

    private bool dragEnabled;

    private PointF firstTouch;

    public static readonly BindableProperty DisplayValueProperty = BindableProperty.Create(nameof(DisplayValue), typeof(bool), typeof(GaugePARCEL), defaultValue: false, propertyChanged: RefreshView);
    public static readonly BindableProperty ReverseProperty = BindableProperty.Create(nameof(Reverse), typeof(bool), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(float), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(float), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty StartPosProperty = BindableProperty.Create(nameof(StartPos), typeof(float), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty EndPosProperty = BindableProperty.Create(nameof(EndPos), typeof(float), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ValueMinProperty = BindableProperty.Create(nameof(ValueMin), typeof(double), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ValueMaxProperty = BindableProperty.Create(nameof(ValueMax), typeof(double), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty PrecisionProperty = BindableProperty.Create(nameof(Precision), typeof(int), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty LineCapProperty = BindableProperty.Create(nameof(LineCap), typeof(LineCap), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty GaugeStyleProperty = BindableProperty.Create(nameof(Appearance), typeof(IGaugePARCEL.MeterStyle), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ValueChangedCommandProperty = BindableProperty.Create(nameof(ValueChangedCommand), typeof(ICommand), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty IndicatorProperty = BindableProperty.Create(nameof(Indicator), typeof(IIndicatorPARCEL), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty EmptyColorProperty = BindableProperty.Create(nameof(EmptyColor), typeof(Color), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty FillColorProperty = BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty FontColorProperty = BindableProperty.Create(nameof(FontColor), typeof(Color), typeof(GaugePARCEL), propertyChanged: RefreshView);

    #endregion

    #region Constructors
    public GaugePARCEL()
    {
        try
        {
            firstTouch = new();

            ControlCanvas = new()
            {
                Drawable = new GaugePARCELRenderer(this)

            };

            labelFontSizeDataTrigger = new(typeof(Label))
            {
                Binding = new Binding(nameof(FontSize), converter: new IsNullConverter()),
                Value = false,
                Setters =
            {
                new()
                {
                    Property = Label.FontSizeProperty,
                    Value = new Binding(nameof(FontSize))

                }

            }

            };

            labelDisplayDataTrigger = new(typeof(Label))
            {
                Binding = new Binding(nameof(DisplayValue)),
                Value = true,
                Setters =
            {
                new()
                {
                    Property = IsVisibleProperty,
                    Value = true

                }

            }

            };

            valueLabel = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                IsVisible = false,
                InputTransparent = true,
                Triggers =
                {
                    labelFontSizeDataTrigger,
                    labelDisplayDataTrigger

                }

            };

            valueLabel.SetBinding(Label.TextColorProperty, nameof(FontColor));
            valueLabel.SetBinding(Label.FontFamilyProperty, nameof(FontFamily));
            valueLabel.SetBinding(Label.TextProperty, nameof(Value));

            controlContainer = new()
            {
                Children = 
                { 
                    ControlCanvas,
                    valueLabel 
                
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
    public event EventHandler? ValueChanged;

    #endregion

    #region Properties
    public bool DisplayValue
    {
        get => (bool)GetValue(DisplayValueProperty);
        set => SetValue(DisplayValueProperty, value);

    }

    public bool Reverse
    {
        get => (bool)GetValue(ReverseProperty);
        set => SetValue(ReverseProperty, value);

    }

    public float Thickness
    {
        get => (float)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);

    }

    public float StrokeThickness
    {
        get => (float)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);

    }

    public float StartPos
    {
        get => (float)GetValue(StartPosProperty);
        set => SetValue(StartPosProperty, value);

    }

    public float EndPos
    {
        get => (float)GetValue(EndPosProperty);
        set => SetValue(EndPosProperty, value);

    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set
        {
            if (value < ValueMin)
                value = ValueMin;

            if (value > ValueMax)
                value = ValueMax;

            SetValue(ValueProperty, value);

        }

    }

    public double ValueMin
    {
        get => (double)GetValue(ValueMinProperty);
        set => SetValue(ValueMinProperty, value);

    }

    public double ValueMax
    {
        get => (double)GetValue(ValueMaxProperty);
        set
        {
            if (value < ValueMin)
                value = ValueMin;

            SetValue(ValueMaxProperty, value);

        }

    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);

    }

    public int Precision
    {
        get => (int)GetValue(PrecisionProperty);
        set => SetValue(PrecisionProperty, value);

    }

    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);

    }

    public LineCap LineCap
    {
        get => (LineCap)GetValue(LineCapProperty);
        set => SetValue(LineCapProperty, value);

    }

    public IGaugePARCEL.MeterStyle Appearance
    {
        get => (IGaugePARCEL.MeterStyle)GetValue(GaugeStyleProperty);
        set => SetValue(GaugeStyleProperty, value);

    }

    public ICommand ValueChangedCommand
    {
        get => (ICommand)GetValue(ValueChangedCommandProperty);
        set => SetValue(ValueChangedCommandProperty, value);

    }

    public IIndicatorPARCEL Indicator
    {
        get => (IIndicatorPARCEL)GetValue(IndicatorProperty);
        set => SetValue(IndicatorProperty, value);

    }

    public Color EmptyColor
    {
        get => (Color)GetValue(EmptyColorProperty);
        set => SetValue(EmptyColorProperty, value);

    }

    public Color FillColor
    {
        get => (Color)GetValue(FillColorProperty); 
        set => SetValue(FillColorProperty, value);

    }

    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);

    }

    public Color FontColor
    {
        get => (Color)GetValue(FontColorProperty);
        set => SetValue(FontColorProperty, value);

    }

    #endregion

    #region Methods
    private void ControlCanvasDragInteraction(object? sender, TouchEventArgs e)
    {
        try
        {
            GaugePARCELRenderer? renderer = ControlCanvas?.Drawable as GaugePARCELRenderer;

            if (firstTouch.IsEmpty)
                firstTouch = e.Touches.First();

            if ((renderer ?? new GaugePARCELRenderer(this)).IndicatorBounds.Contains(firstTouch))
                dragEnabled = true;

            if (dragEnabled)
                TranslateInput(renderer ?? new GaugePARCELRenderer(this), e);
#if DEBUG
            Console.WriteLine($"Console Drag Event");
#endif
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

    }

    private void ControlCanvasEndInteraction(object? sender, TouchEventArgs e)
    {
        try
        {
            dragEnabled = false;

            firstTouch = new();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

    }

    private void TranslateInput(GaugePARCELRenderer renderer, TouchEventArgs e)
    {
        try
        {
            double previous = Value,
                   inputThreshold = 20;

            switch (Appearance)
            {
                case IGaugePARCEL.MeterStyle.Horizontal:

                    break;

                case IGaugePARCEL.MeterStyle.Vertical:

                    break;

                case IGaugePARCEL.MeterStyle.Radial:
                    if (Mathematician.GetAngle(renderer.WorkingCanvas.Center, e.Touches.Last(), renderer.StartPosAsPoint) <= (360f - (Math.Abs(StartPos) - Math.Abs(EndPos))) + inputThreshold)
                    {
                        Value = Math.Round(Mathematician.GetAngle(renderer.WorkingCanvas.Center, e.Touches.Last(), renderer.StartPosAsPoint) / (360f - (Math.Abs(StartPos) - Math.Abs(EndPos))) * ValueMax, Precision);

                    }

                    break;

            }

            if (Value != previous)
            {
                ValueChanged?.Invoke(this, EventArgs.Empty);

                if (ValueChangedCommand?.CanExecute(null) ?? false)
                    ValueChangedCommand.Execute(null);

            }

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
            if (!(Indicator == null || (controlContainer ?? []).Contains(Indicator)))
            {
                Indicator.InputTransparent = true;

                controlContainer?.Add(Indicator);

                if (ControlCanvas != null)
                {
                    ControlCanvas.DragInteraction += ControlCanvasDragInteraction;
                    ControlCanvas.EndInteraction += ControlCanvasEndInteraction;

                }

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

        base.LayoutChildren(x, y, width, height);

    }

    #endregion

    #region Classes
    private class GaugePARCELRenderer : IDrawable
    {
        #region Fields
        private float valuePos;

        private readonly GaugePARCEL parent;

        #endregion

        #region Constructors
        public GaugePARCELRenderer(GaugePARCEL parentControl)
        {
            parent = parentControl;

        }

        #endregion

        #region Properties        
        internal RectF WorkingCanvas { get; private set; }
        
        internal RectF IndicatorBounds { get; private set; }

        internal PointF StartPosAsPoint
        {
            get
            {
                return GeometryUtil.EllipseAngleToPoint(WorkingCanvas.Left, WorkingCanvas.Top, WorkingCanvas.Width, WorkingCanvas.Height, parent.StartPos);

            }

        }

        #endregion

        #region Methods
        private void DrawIndicator(RectF rect)
        {
            PointF indicatorPos = GeometryUtil.EllipseAngleToPoint(
                WorkingCanvas.Left,
                WorkingCanvas.Top,
                WorkingCanvas.Width,
                WorkingCanvas.Height,
                Math.Abs(valuePos));

            IndicatorBounds = new((float)(indicatorPos.X - (parent.Indicator.Content.Width / 2)), (float)(indicatorPos.Y - (parent.Indicator.Content.Height / 2)), (float)parent.Indicator.Content.Width, (float)parent.Indicator.Content.Height);

            parent.Indicator.TranslationX = IndicatorBounds.X - (rect.Width / 2 - (IndicatorBounds.Width / 2));
            parent.Indicator.TranslationY = IndicatorBounds.Y - (rect.Height / 2 - (IndicatorBounds.Height / 2));

        }

        public void Draw(ICanvas canvas, RectF rect)
        {
            WorkingCanvas = new()
            { 
                Width = rect.Width - (parent.Thickness + parent.StrokeThickness),
                Height = rect.Height - (parent.Thickness + parent.StrokeThickness),
                Left = rect.Left + (float)((parent.Thickness + parent.StrokeThickness) / 2),
                Top = rect.Top + (float)((parent.Thickness + parent.StrokeThickness) / 2)

            };

            canvas.StrokeLineCap = parent.LineCap;
            canvas.StrokeSize = parent.Thickness + parent.StrokeThickness;
            canvas.StrokeColor = parent.StrokeColor;

            switch (parent.Appearance)
            {
                case IGaugePARCEL.MeterStyle.Horizontal:

                    break;

                case IGaugePARCEL.MeterStyle.Vertical:

                    break;

                case IGaugePARCEL.MeterStyle.Radial:
                    valuePos = parent.StartPos - (float)((parent.Value - parent.ValueMin) / (parent.ValueMax - parent.ValueMin) * (360f - (Math.Abs(parent.StartPos) - Math.Abs(parent.EndPos))));
                    
                    if (parent.EmptyColor.Alpha > 0)
                        canvas.DrawArc(
                            WorkingCanvas.Left,
                            WorkingCanvas.Top,
                            WorkingCanvas.Width,
                            WorkingCanvas.Height,
                            parent.StartPos,
                            parent.EndPos,
                            clockwise: true,
                            closed: false);

                    if (parent.EmptyColor.Alpha == 0)
                        canvas.DrawArc(
                            WorkingCanvas.Left,
                            WorkingCanvas.Top,
                            WorkingCanvas.Width,
                            WorkingCanvas.Height,
                            parent.StartPos,
                            valuePos,
                            clockwise: true,
                            closed: false);

                    canvas.StrokeSize = parent.Thickness;
                    canvas.StrokeColor = parent.EmptyColor;

                    canvas.DrawArc(
                        WorkingCanvas.Left, 
                        WorkingCanvas.Top, 
                        WorkingCanvas.Width, 
                        WorkingCanvas.Height, 
                        parent.StartPos, 
                        parent.EndPos, 
                        clockwise: true, 
                        closed: false);

                    canvas.StrokeColor = parent.FillColor;

                    canvas.DrawArc(
                        WorkingCanvas.Left, 
                        WorkingCanvas.Top, 
                        WorkingCanvas.Width, 
                        WorkingCanvas.Height, 
                        parent.StartPos, 
                        valuePos, 
                        clockwise: true, 
                        closed: false);

                    if (parent.Indicator != null)
                    {
                        DrawIndicator(rect);

                    }

                    break;

                default:

                    break;

            }

        }

        #endregion

    }

    #endregion

}