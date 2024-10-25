using System.Windows.Input;

namespace PARCEL.Controls.GestureRecognizers;

public class ButtonInputDetector : GestureRecognizer
{
    #region Fields
    public static BindableProperty PressedCommandProperty = BindableProperty.Create(nameof(PressedCommand), typeof(ICommand), typeof(ButtonInputDetector));
    public static BindableProperty ReleasedCommandProperty = BindableProperty.Create(nameof(ReleasedCommand), typeof(ICommand), typeof(ButtonInputDetector));
    public static BindableProperty PressedCommandParameterProperty = BindableProperty.Create(nameof(PressedCommandParamter), typeof(object), typeof(ButtonInputDetector));
    public static BindableProperty ReleasedCommandParameterProperty = BindableProperty.Create(nameof(ReleasedCommandParamter), typeof(object), typeof(ButtonInputDetector));


    #endregion

    #region Constructors
    public ButtonInputDetector()
    {

    }

    #endregion

    #region Events
    public event EventHandler? Pressed;
    public event EventHandler? Released;

    #endregion

    #region Properties
    public ICommand PressedCommand
    {
        get => (ICommand)GetValue(PressedCommandProperty);
        set => SetValue(PressedCommandProperty, value);

    }
    public ICommand ReleasedCommand
    {
        get => (ICommand)GetValue(ReleasedCommandProperty);
        set => SetValue(ReleasedCommandProperty, value);

    }

    public object PressedCommandParamter { get; set; } 

    public object ReleasedCommandParamter { get; set; }

    #endregion

}
