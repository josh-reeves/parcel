using System.Windows.Input;
using PARCEL.Interfaces;
using PARCEL.Helpers;
using PARCEL.Converters;
using PARCEL.Controls.Behaviors;
using PARCEL.Controls.Renderers;
using PARCEL.Controls.InputStrategies;

namespace PARCEL.Controls;

public class GaugePARCEL : ControlPARCEL, IGaugePARCEL, IStrategizedControl
{
    #region Fields
    private readonly Label? valueLabel;

    private bool touchActive;

    private IInputStrategy? defaultStrategy;

    private DragDetector? dragDetector;
    
    public static readonly BindableProperty ValueChangedParameterProperty = BindableProperty.Create(nameof(ValueChangedCommandParameter), typeof(object), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty TouchEnabledProperty = BindableProperty.Create(nameof(TouchEnabled), typeof(bool), typeof(GaugePARCEL), defaultValue: false, propertyChanged: UpdateTouch);
    public static readonly BindableProperty DisplayValueProperty = BindableProperty.Create(nameof(DisplayValue), typeof(bool), typeof(GaugePARCEL), defaultValue: false, propertyChanged: RefreshView);
    public static readonly BindableProperty ReverseProperty = BindableProperty.Create(nameof(Reverse), typeof(bool), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(float), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(float), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty StartPosProperty = BindableProperty.Create(nameof(StartPos), typeof(float), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty EndPosProperty = BindableProperty.Create(nameof(EndPos), typeof(float), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(GaugePARCEL), propertyChanged: SendValueChanged);
    public static readonly BindableProperty ValueMinProperty = BindableProperty.Create(nameof(ValueMin), typeof(double), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ValueMaxProperty = BindableProperty.Create(nameof(ValueMax), typeof(double), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty PrecisionProperty = BindableProperty.Create(nameof(Precision), typeof(int), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty LineCapProperty = BindableProperty.Create(nameof(LineCap), typeof(LineCap), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty GaugeStyleProperty = BindableProperty.Create(nameof(Appearance), typeof(IGaugePARCEL.MeterStyle), typeof(GaugePARCEL), propertyChanged: UpdateAppearance);
    public static readonly BindableProperty ValueChangedCommandProperty = BindableProperty.Create(nameof(ValueChangedCommand), typeof(ICommand), typeof(GaugePARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty IndicatorProperty = BindableProperty.Create(nameof(Indicator), typeof(IIndicatorPARCEL), typeof(GaugePARCEL), propertyChanged: AddIndicator);
    public static readonly BindableProperty InputStrategyProperty = BindableProperty.Create(nameof(InputStrategy), typeof(IInputStrategy), typeof(GaugePARCEL));
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
            dragDetector = new();

            ControlCanvas = new()
            {
                Drawable = GetDefaultRenderer(),
                InputTransparent = true

            };

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

            ControlContainer = new Grid()
            {
                BindingContext = this,
                Children =
                {
                    ControlCanvas,
                    valueLabel

                },
                Behaviors =
                {
                    dragDetector

                }

            };

            Content = ControlContainer;

            ControlContainer.SetBinding(MarginProperty, nameof(Margin));
            ControlCanvas.SetBinding(MarginProperty, nameof(ControlContainer.Margin));

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
    internal RectF IndicatorBounds { get; set; }

    internal RectF WorkingCanvas { get; set; }

    public object ValueChangedCommandParameter
    {
        get => GetValue(ValueChangedParameterProperty);
        set => SetValue(ValueChangedParameterProperty, value);
           
    }

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

    public int Precision
    {
        get => (int)GetValue(PrecisionProperty);
        set => SetValue(PrecisionProperty, value);

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

    public IInputStrategy InputStrategy
    {
        get
        {   
            if ((IInputStrategy)GetValue(InputStrategyProperty) is not null)
                return (IInputStrategy)GetValue(InputStrategyProperty);

            return Appearance switch
            {
                IGaugePARCEL.MeterStyle.Horizontal => defaultStrategy ??= new HorizontalGaugeStrategy(),
                IGaugePARCEL.MeterStyle.Vertical => defaultStrategy ??= new VerticalGaugeStrategy(),
                IGaugePARCEL.MeterStyle.Radial => defaultStrategy ??= new RadialGaugeStrategy(),
                _=> throw new NotImplementedException()

            };

        }

        set => SetValue(InputStrategyProperty, value);

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
    private static void AddIndicator(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            GaugePARCEL instance = (GaugePARCEL)bindable;

            if (instance.Indicator != null && !(instance.ControlContainer?.Contains(instance.Indicator) ?? true))
            {
                int labelIndex = Convert.ToInt32(instance.ControlContainer?.IndexOf(instance.valueLabel));
                instance.ControlContainer?.Insert(labelIndex, instance.Indicator);

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

        RefreshView(bindable, oldValue, newValue);

    }

    private static void UpdateTouch(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            if (bindable is not GaugePARCEL instance || instance.dragDetector is null)
                return;
            
            instance.dragDetector.Drag -= instance.OnDrag;
            instance.dragDetector.DragEnded -= instance.OnDragEnded;

            if ((bool)newValue)
            {
                instance.dragDetector.Drag += instance.OnDrag;
                instance.dragDetector.DragEnded += instance.OnDragEnded;

            }

        }
        catch (Exception ex)
        {
            DebugLogger.Log(ex);

        }

        RefreshView(bindable, oldValue, newValue);

    }

    private static void SendValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue == oldValue || bindable is not GaugePARCEL instance)
            return; 
        
        instance.ValueChanged?.Invoke(instance, EventArgs.Empty);

        if (instance.ValueChangedCommand?.CanExecute(instance.ValueChangedCommandParameter) ?? false)
            instance.ValueChangedCommand.Execute(instance.ValueChangedCommandParameter);
#if DEBUG
        DebugLogger.Log($"Value changed to {instance.Value}.");
#endif
        RefreshView(bindable, oldValue, newValue);

    }
    
    private static void UpdateAppearance(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not GaugePARCEL instance)
            return;

        instance.defaultStrategy = null;

        if (instance.Renderer is not null || instance.ControlCanvas is null)
            return;

        instance.ControlCanvas.Drawable = instance.GetDefaultRenderer();
        
        RefreshView(bindable, oldValue, newValue);
#if DEBUG
    DebugLogger.Log($"Renderer updated by {nameof(instance.Appearance)}.");
#endif
    }

    private void OnDrag(object? sender, DragDetector.DragEventArgs args)
    {
        try
        {
            if (IndicatorBounds.Contains(args.Points.First()) || Indicator.Frame.Contains(args.Points.First()))
            {
                touchActive = true;                
            }

            if (touchActive)
            {
                InputStrategy?.HandleInput(this, args);

            }

        }
        catch (Exception ex)
        {
            DebugLogger.Log(ex);

        }
#if DEBUG
        DebugLogger.Log($"GaugePARCEL drag event. Location: {args.Points.Last()}, Indicator:{IndicatorBounds}.");
#endif
    }

    private void OnDragEnded(object? sender, DragDetector.DragEventArgs e)
        => touchActive = false;
    
    protected override IDrawable GetDefaultRenderer()
    {
        return Appearance switch
        {
            IGaugePARCEL.MeterStyle.Horizontal => new HorizontalGaugeRenderer(this),
            IGaugePARCEL.MeterStyle.Vertical => new VerticalGaugeRenderer(this),
            IGaugePARCEL.MeterStyle.Radial => new RadialGaugeRenderer(this),
            _ => throw new NotImplementedException()

        };

    }
    
    #endregion

}