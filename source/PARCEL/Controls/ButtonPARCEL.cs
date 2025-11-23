using PARCEL.Controls.Behaviors;
using PARCEL.Controls.Renderers;
using PARCEL.Converters;
using PARCEL.Helpers;
using PARCEL.Interfaces;
using System.Windows.Input;

namespace PARCEL.Controls;

public class ButtonPARCEL : ControlPARCEL, IButtonPARCEL
{
    #region Fields
    public static readonly BindableProperty IsPressedProperty = BindableProperty.Create(nameof(IsPressed), typeof(bool), typeof(ButtonPARCEL), defaultValue: false, propertyChanged: RefreshView);
    public static readonly BindableProperty OffsetProperty = BindableProperty.Create(nameof(Offset), typeof(double), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create(nameof(StrokeWidth), typeof(double), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ButtonShapeProperty = BindableProperty.Create(nameof(ButtonShape), typeof(IShape), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ButtonContentProperty = BindableProperty.Create(nameof(ButtonContent), typeof(IStackLayout), typeof(ButtonPARCEL), propertyChanged: AddContent);
    public static readonly BindableProperty ReleasedCommandProperty = BindableProperty.Create(nameof(ReleasedCommand), typeof(ICommand), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty PressedCommandProperty = BindableProperty.Create(nameof(PressedCommand), typeof(ICommand), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(SolidColorBrush), typeof(ButtonPARCEL), propertyChanging: RefreshView, propertyChanged: RefreshView);
    public static readonly BindableProperty ButtonColorProperty = BindableProperty.Create(nameof(ButtonColor), typeof(Brush), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty PressedColorProperty = BindableProperty.Create(nameof(PressedColor), typeof(Brush), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty OffsetColorProperty = BindableProperty.Create(nameof(OffsetColor), typeof(Brush), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty IsParentPressedProperty = BindableProperty.CreateAttached("IsParentPressed", typeof(bool), typeof(ButtonPARCEL), false);

    #endregion

    #region Constructors
    public ButtonPARCEL()
    {
        try
        {
            ControlCanvas = ViewBuilder<GraphicsView>.BuildView(
                new GraphicsView()
                {
                    InputTransparent = true,
                    Triggers =
                    {
                        new DataTrigger(typeof(GraphicsView))
                        {
                            Binding = new Binding(nameof(Renderer), converter: new IsNullConverter()),
                            Value = true,
                            Setters =
                            {
                                new()
                                {
                                    Property = GraphicsView.DrawableProperty,
                                    Value = GetDefaultRenderer()

                                }

                            }

                        }

                    }

                },
                [
                    new ViewBuilder<GraphicsView>.BindingPair(GraphicsView.DrawableProperty, nameof(Renderer))

                ]);

            ControlContainer = new Grid()
            {
                BindingContext = this,
                InputTransparent = false,
                Children = { ControlCanvas },
                Behaviors =
                {
                    new ButtonInputDetector()
                    {
                        PressedCommand = new Command(OnPressed),
                        ReleasedCommand = new Command(OnReleased),
                        ExitedCommand = new Command(() => IsPressed = false)

                    }

                }

            };

            Content = ControlContainer;

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
    public bool IsPressed
    {
        get
        {
            if (ButtonContent is Layout layout)
            {
                foreach (View child in layout.Children)
                {
                    if (GetIsParentPressed(child))
                    {
                        IsPressed = GetIsParentPressed(child);
                        
                    }
                    
                }

            }
            
            return (bool)GetValue(IsPressedProperty);

        }
        set
        {
            if (ButtonContent is Layout layout)
            {
                SetIsParentPressed(layout, value);

                foreach (View child in layout.Children)
                    SetIsParentPressed(child, value);

            }

            SetValue(IsPressedProperty, value);

        }

    }

    public double Offset
    {
        get => (double)GetValue(OffsetProperty);
        set => SetValue(OffsetProperty, value);

    }

    public double StrokeWidth
    {
        get => (double)GetValue(StrokeWidthProperty);
        set => SetValue(StrokeWidthProperty, value);

    }

    public IShape ButtonShape
    {
        get => (IShape)GetValue(ButtonShapeProperty);
        set => SetValue(ButtonShapeProperty, value);

    }

    public IView ButtonContent
    {
        get => (IView)GetValue(ButtonContentProperty);
        set => SetValue(ButtonContentProperty, value);

    }

    public ICommand ReleasedCommand
    {
        get => (ICommand)GetValue(ReleasedCommandProperty);
        set => SetValue(ReleasedCommandProperty, value);

    }

    public ICommand PressedCommand
    {
        get => (ICommand)GetValue(PressedCommandProperty);
        set => SetValue(PressedCommandProperty, value);

    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);

    }

    public SolidColorBrush StrokeColor
    {
        get => (SolidColorBrush)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);

    }

    public Brush ButtonColor
    {
        get => (Brush)GetValue(ButtonColorProperty);
        set => SetValue(ButtonColorProperty, value);

    }

    public Brush PressedColor
    {
        get => (Brush)GetValue(PressedColorProperty);
        set => SetValue(PressedColorProperty, value);

    }

    public Brush OffsetColor
    {
        get => (Brush)GetValue(OffsetColorProperty);
        set => SetValue(OffsetColorProperty, value);

    }

    #endregion

    #region Methods
    private void OnPressed()
    {
        IsPressed = true;

        if (PressedCommand?.CanExecute(this) ?? false)
            PressedCommand.Execute(this);

        Pressed?.Invoke(this, EventArgs.Empty);

    }

    private void OnReleased()
    {
        IsPressed = false;

        if (ReleasedCommand?.CanExecute(this) ?? false)
            ReleasedCommand.Execute(this);

        Released?.Invoke(this, EventArgs.Empty);

    }

    private static void AddContent(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ButtonPARCEL instance)
        {
            instance.ControlContainer?.Remove(oldValue as IView);
            instance.ControlContainer?.Add(instance.ButtonContent);

        }

        RefreshView(bindable, oldValue, newValue);

    }

    public static bool GetIsParentPressed(BindableObject view)
        => (bool)view.GetValue(IsParentPressedProperty);

    public static void SetIsParentPressed(BindableObject view, bool value)
        => view.SetValue(IsParentPressedProperty, value);

    protected override IDrawable GetDefaultRenderer()
    {
        return Offset switch
        {
            >= 0 => new RaisedButtonRenderer(this),
            < 0 => new RecessedButtonRenderer(this),
            _ => throw new NotImplementedException()

        };

    }

    #endregion

}