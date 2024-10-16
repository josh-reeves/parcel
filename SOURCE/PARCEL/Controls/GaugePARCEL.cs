using System.Windows.Input;
using PARCEL.Interfaces;
using PARCEL.Helpers;
using PARCEL.Converters;
using Microsoft.Maui.Graphics;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel;

namespace PARCEL.Controls;

public class GaugePARCEL : ControlPARCEL, IGaugePARCEL
{
    #region Fields
    private readonly Grid? controlContainer;
    private readonly Label? valueLabel;
    
    private bool dragEnabled;

    private PointF firstTouch;
    private RectF workingCanvas,
                  indicatorBounds;

    public static readonly BindableProperty TouchEnabledProperty = BindableProperty.Create(nameof(TouchEnabled), typeof(bool), typeof(GaugePARCEL), defaultValue: false, propertyChanged: EnableTouch);
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
    public static readonly BindableProperty IndicatorProperty = BindableProperty.Create(nameof(Indicator), typeof(IIndicatorPARCEL), typeof(GaugePARCEL), propertyChanged: AddIndicator);
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
            ControlCanvas = ViewBuilder<GraphicsView>.BuildView(
                new GraphicsView()
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
                                    Value = new GaugePARCELRenderer(this)

                                }

                            }

                        }

                    }

                },
                [
                    new ViewBuilder<GraphicsView>.BindingPair(GraphicsView.DrawableProperty, nameof(Renderer))

                ]);

            valueLabel = ViewBuilder<Label>.BuildView(
                new()
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    IsVisible = false,
                    InputTransparent = true,
                    Triggers =
                    {
                        new DataTrigger(typeof(Label))
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

                        },
                        new DataTrigger(typeof(Label))
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

                        }

                    }
                },
                [
                    new(Label.TextColorProperty, nameof(FontColor)),
                    new(Label.FontFamilyProperty, nameof(FontFamily)),
                    new(Label.TextProperty, nameof(Value)),

                ]);

            controlContainer = new()
            {
                BindingContext = this,
                Children = 
                { 
                    valueLabel,
                    ControlCanvas
                
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
    public bool TouchEnabled
    {
        get => (bool)GetValue(TouchEnabledProperty);
        set => SetValue(TouchEnabledProperty, value);

    }

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
            if (firstTouch.IsEmpty)
                firstTouch = e.Touches.First();

            if (indicatorBounds.Contains(firstTouch))
                dragEnabled = true;

            if (dragEnabled)
                TranslateInput(e);
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

    private void TranslateInput(TouchEventArgs e)
    {
        try
        {
            double previous = Value,
                   inputThreshold = 20;

            PointF startPosPoint = GeometryUtil.EllipseAngleToPoint(workingCanvas.Left, workingCanvas.Top, workingCanvas.Width, workingCanvas.Height, StartPos); ;

            switch (Appearance)
            {
                case IGaugePARCEL.MeterStyle.Horizontal:
                    throw new NotImplementedException();

                case IGaugePARCEL.MeterStyle.Vertical:
                    throw new NotImplementedException();

                case IGaugePARCEL.MeterStyle.Radial:
                    if (Mathematician.GetAngle(workingCanvas.Center, e.Touches.Last(), startPosPoint) <= (360f - (Math.Abs(StartPos) - Math.Abs(EndPos))) + inputThreshold)
                        Value = Math.Round(Mathematician.GetAngle(workingCanvas.Center, e.Touches.Last(), startPosPoint) / (360f - (Math.Abs(StartPos) - Math.Abs(EndPos))) * ValueMax, Precision);

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

    private static void AddIndicator(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            GaugePARCEL instance = (GaugePARCEL)bindable;

            if (instance.Indicator != null && !(instance.controlContainer?.Contains(instance.Indicator) ?? true))
            {
                instance.Indicator.InputTransparent = true;

                instance.controlContainer?.Add(instance.Indicator);

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

        RefreshView(bindable, oldValue, newValue);

    }

    private static void EnableTouch(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            GaugePARCEL instance = (GaugePARCEL)bindable;

            if (instance.ControlCanvas != null)
            {
                instance.ControlCanvas.DragInteraction -= instance.ControlCanvasDragInteraction;
                instance.ControlCanvas.EndInteraction -= instance.ControlCanvasEndInteraction;

            }

            if ((bool)newValue && instance.ControlCanvas != null)
            {                
                instance.ControlCanvas.DragInteraction += instance.ControlCanvasDragInteraction;
                instance.ControlCanvas.EndInteraction += instance.ControlCanvasEndInteraction;

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

        RefreshView(bindable, oldValue, newValue);

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

        #region Methods
        private void DrawIndicator(RectF rect)
        {
            PointF indicatorPos = GeometryUtil.EllipseAngleToPoint(
                parent.workingCanvas.Left,
                parent.workingCanvas.Top,
                parent.workingCanvas.Width,
                parent.workingCanvas.Height,
                Math.Abs(valuePos));

            parent.indicatorBounds = new((float)(indicatorPos.X - (parent.Indicator.Width / 2)), (float)(indicatorPos.Y - (parent.Indicator.Content.Height / 2)), (float)parent.Indicator.Content.Width, (float)parent.Indicator.Content.Height);

            parent.Indicator.TranslationX = parent.indicatorBounds.X - (rect.Width / 2 - (parent.indicatorBounds.Width / 2));
            parent.Indicator.TranslationY = parent.indicatorBounds.Y - (rect.Height / 2 - (parent.indicatorBounds.Height / 2));

        }

        public void Draw(ICanvas canvas, RectF rect)
        {
            canvas.StrokeLineCap = parent.LineCap;
            canvas.StrokeSize = parent.Thickness + parent.StrokeThickness;
            canvas.StrokeColor = parent.StrokeColor;

            switch (parent.Appearance)
            {
                case IGaugePARCEL.MeterStyle.Horizontal:
                    parent.workingCanvas = GetSafeMargins(rect, 1);

                    canvas.DrawLine(parent.workingCanvas.Left, parent.StartPos, parent.workingCanvas.Right, parent.EndPos); 
                    
                    canvas.StrokeSize = parent.Thickness;
                    canvas.StrokeColor = parent.EmptyColor;

                    canvas.DrawLine(parent.workingCanvas.Left, parent.StartPos, parent.workingCanvas.Right, parent.EndPos);

                    canvas.StrokeColor = parent.FillColor;

                    break;

                case IGaugePARCEL.MeterStyle.Vertical:
                    throw new NotImplementedException();

                case IGaugePARCEL.MeterStyle.Radial:
                    parent.workingCanvas = new()
                    {
                        Width = rect.Width - (parent.Thickness + parent.StrokeThickness),
                        Height = rect.Height - (parent.Thickness + parent.StrokeThickness),
                        Left = rect.Left + (float)((parent.Thickness + parent.StrokeThickness) / 2),
                        Top = rect.Top + (float)((parent.Thickness + parent.StrokeThickness) / 2)

                    };

                    valuePos = parent.StartPos - (float)((parent.Value - parent.ValueMin) / (parent.ValueMax - parent.ValueMin) * (360f - (Math.Abs(parent.StartPos) - Math.Abs(parent.EndPos))));
                    
                    if (parent.EmptyColor.Alpha > 0)
                        canvas.DrawArc(
                            parent.workingCanvas.Left,
                            parent.workingCanvas.Top,
                            parent.workingCanvas.Width,
                            parent.workingCanvas.Height,
                            parent.StartPos,
                            parent.EndPos,
                            clockwise: true,
                            closed: false);

                    if (parent.EmptyColor.Alpha == 0)
                        canvas.DrawArc(
                            parent.workingCanvas.Left,
                            parent.workingCanvas.Top,
                            parent.workingCanvas.Width,
                            parent.workingCanvas.Height,
                            parent.StartPos,
                            valuePos,
                            clockwise: true,
                            closed: false);

                    canvas.StrokeSize = parent.Thickness;
                    canvas.StrokeColor = parent.EmptyColor;

                    canvas.DrawArc(
                        parent.workingCanvas.Left, 
                        parent.workingCanvas.Top, 
                        parent.workingCanvas.Width, 
                        parent.workingCanvas.Height, 
                        parent.StartPos, 
                        parent.EndPos, 
                        clockwise: true, 
                        closed: false);

                    canvas.StrokeColor = parent.FillColor;

                    canvas.DrawArc(
                        parent.workingCanvas.Left, 
                        parent.workingCanvas.Top, 
                        parent.workingCanvas.Width, 
                        parent.workingCanvas.Height, 
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