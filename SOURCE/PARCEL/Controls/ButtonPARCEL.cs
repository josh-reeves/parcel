using PARCEL.Converters;
using PARCEL.Helpers;
using PARCEL.Interfaces;
using System.Windows.Input;

namespace PARCEL.Controls;

public class ButtonPARCEL : ControlPARCEL, IButtonPARCEL
{
    #region Fields
    private readonly Grid? controlContainer;

    public static readonly BindableProperty IsPressedProperty = BindableProperty.Create(nameof(IsPressed), typeof(bool), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty OffsetProperty = BindableProperty.Create(nameof(Offset), typeof(double), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create(nameof(StrokeWidth), typeof(double), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty AppearanceProperty = BindableProperty.Create(nameof(Appearance), typeof(IButtonPARCEL.ButtonStyle), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ReleasedCommandProperty = BindableProperty.Create(nameof(ReleasedCommand), typeof(ICommand), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty PressedCommandProperty = BindableProperty.Create(nameof(PressedCommand), typeof(ICommand), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ButtonContentProperty = BindableProperty.Create(nameof(ButtonContent), typeof(IStackLayout), typeof(ButtonPARCEL), propertyChanged: AddContent);
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ButtonColorProperty = BindableProperty.Create(nameof(ButtonColor), typeof(Brush), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty OffsetColorProperty = BindableProperty.Create(nameof(OffsetColor), typeof(Brush), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty PressedColorProperty = BindableProperty.Create(nameof(PressedColor), typeof(Brush), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    public static readonly BindableProperty ButtonShapeProperty = BindableProperty.Create(nameof(ButtonShape), typeof(IShape), typeof(ButtonPARCEL), propertyChanged: RefreshView);
    #endregion

    #region Constructors
    public ButtonPARCEL()
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
                            Binding = new Binding(nameof(Renderer), converter: new IsNullConverter()),
                            Value = true,
                            Setters =
                            {
                                new()
                                {
                                    Property = GraphicsView.DrawableProperty,
                                    Value = new ButtonPARCELRenderer(this)

                                }

                            }

                        }

                    }

                },
                [
                    new ViewBuilder<GraphicsView>.BindingPair(GraphicsView.DrawableProperty, nameof(Renderer))

                ]);

            controlContainer = new()
            {
                BindingContext = this,
                Children = { ControlCanvas },
                GestureRecognizers =
                {
                    new PointerGestureRecognizer()
                    {
                        PointerPressedCommand = new Command(OnPressed),
                        PointerReleasedCommand = new Command(OnReleased),
                        PointerExitedCommand = new Command(() => IsPressed = false)

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
    public bool IsPressed
    {
        get => (bool)GetValue(IsPressedProperty);
        set => SetValue(IsPressedProperty, value);

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

    public IButtonPARCEL.ButtonStyle Appearance
    {
        get => (IButtonPARCEL.ButtonStyle)GetValue(AppearanceProperty);
        set => SetValue(AppearanceProperty, value);

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

    public IStackLayout ButtonContent
    {
        get => (IStackLayout)GetValue(ButtonContentProperty);
        set => SetValue(ButtonContentProperty, value);

    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);

    }

    public Brush ButtonColor
    {
        get => (Brush)GetValue(ButtonColorProperty);
        set => SetValue(ButtonColorProperty, value);

    }

    public Brush OffsetColor
    {
        get => (Brush)GetValue(OffsetColorProperty);
        set => SetValue(OffsetColorProperty, value);

    }

    public Brush PressedColor
    {
        get => (Brush)GetValue(PressedColorProperty);
        set => SetValue(PressedColorProperty, value);

    }

    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);

    }

    public IShape ButtonShape
    {
        get => (IShape)GetValue(ButtonShapeProperty);
        set => SetValue(ButtonShapeProperty, value);

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
        if (bindable is ButtonPARCEL)
        {
            ButtonPARCEL instance = (ButtonPARCEL)bindable;

            if (!instance?.controlContainer?.Contains(instance.ButtonContent) ?? false)
                instance?.controlContainer?.Add(instance.ButtonContent);

        }

    }

    #endregion

    #region Classes
    private class ButtonPARCELRenderer : IDrawable
    {
        #region Fields
        private const float offset = 1.0f;
        private readonly ButtonPARCEL parent;

        #endregion

        #region Constructors
        public ButtonPARCELRenderer(ButtonPARCEL parentControl)
        {
            parent = parentControl;

        }

        #endregion

        public void Draw(ICanvas canvas, RectF rect)
        {
            canvas.StrokeSize = (float)parent.StrokeWidth;
    
            parent.StrokeColor ??= Colors.Transparent;

            switch (parent.Appearance)
            {
                case IButtonPARCEL.ButtonStyle.Flat:
                    if (!parent.IsPressed)
                        Designer.FillShape(canvas, GetSafeMargins(rect, offset), parent.ButtonShape, parent.ButtonColor);
                    else
                        Designer.FillShape(canvas, GetSafeMargins(rect, offset), parent.ButtonShape, parent.PressedColor);

                    Designer.OutlineShape(canvas, GetSafeMargins(rect, offset), parent.ButtonShape, parent.StrokeColor);
                        
                    break;

                case IButtonPARCEL.ButtonStyle.Raised:

                    if (!parent.IsPressed)
                    {
                        Designer.FillShape(canvas, GetSafeMargins(rect, offset), parent.ButtonShape, parent.OffsetColor);

                        Designer.FillShape(
                            canvas,
                            new RectF()
                            {
                                Top = rect.Top + offset,
                                Left = rect.Left + offset,
                                Width = rect.Width - (offset * 2),
                                Bottom = rect.Bottom - offset - (float)parent.Offset

                            },
                            parent.ButtonShape,
                            parent.ButtonColor);

                        Designer.OutlineShape(canvas, GetSafeMargins(rect, offset), parent.ButtonShape, parent.StrokeColor);

                    }
                    else
                    {
                        Designer.FillShape(canvas,
                            new RectF()
                            {
                                Top = rect.Top + offset + (float)parent.Offset,
                                Left = rect.Left + offset,
                                Width = rect.Width - (offset * 2),
                                Bottom = rect.Bottom - offset

                            },
                            parent.ButtonShape,
                            parent.PressedColor);

                        Designer.OutlineShape(
                            canvas,
                            new RectF()
                            {
                                Top = rect.Top + offset + (float)parent.Offset,
                                Left = rect.Left + offset,
                                Width = rect.Width - (offset * 2),
                                Bottom = rect.Bottom - offset

                            },
                            parent.ButtonShape, 
                            parent.StrokeColor);

                    }


                    break;

                default:
                    throw new NotImplementedException();

            }

        }

    }

    #endregion

}