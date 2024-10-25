using PARCEL.Handlers;
using PARCEL.Helpers;
using System.Windows.Input;

namespace PARCEL.Controls.GestureRecognizers;

public class ButtonInputDetector : View, IGestureRecognizer
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
        DebugLogger.Log("Test");
        Handler = new ButtonInputHandler();

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

    public object PressedCommandParamter
    {
        get => GetValue(PressedCommandParameterProperty);
        set => SetValue(PressedCommandParameterProperty, value);

    }

    public object ReleasedCommandParamter
    {
        get => GetValue(ReleasedCommandParameterProperty);
        set => SetValue(ReleasedCommandParameterProperty, value);

    }

    #endregion

    #region Methods
    internal void SendPressed()
    {

    }

    internal void SendReleased()
    {

    }

    #endregion

}
