using PARCEL.Helpers;
using System.Windows.Input;

namespace PARCEL.Controls.Behaviors;

public partial class ButtonInputDetector 
{
    #region Fields
    private RectF bounds;

    public static BindableProperty PressedCommandProperty = BindableProperty.Create(nameof(PressedCommand), typeof(ICommand), typeof(ButtonInputDetector));
    public static BindableProperty ReleasedCommandProperty = BindableProperty.Create(nameof(ReleasedCommand), typeof(ICommand), typeof(ButtonInputDetector));
    public static BindableProperty ExitedCommandProperty = BindableProperty.Create(nameof(ExitedCommand), typeof(ICommand), typeof(ButtonInputDetector));
    public static BindableProperty PressedCommandParameterProperty = BindableProperty.Create(nameof(PressedCommandParamter), typeof(object), typeof(ButtonInputDetector));
    public static BindableProperty ReleasedCommandParameterProperty = BindableProperty.Create(nameof(ReleasedCommandParamter), typeof(object), typeof(ButtonInputDetector));
    public static BindableProperty ExitedCommandParameterProperty = BindableProperty.Create(nameof(ExitedCommandParameter), typeof(object), typeof(ButtonInputDetector));

    #endregion

    #region Events
    public event EventHandler? Pressed;
    public event EventHandler? Released;
    public event EventHandler? Exited;

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

    public ICommand ExitedCommand
    {
        get => (ICommand)GetValue(ExitedCommandProperty);
        set => SetValue(ExitedCommandProperty, value);

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

    public object ExitedCommandParameter
    {
        get => GetValue(ExitedCommandParameterProperty);
        set => SetValue(ExitedCommandParameterProperty, value);

    }

    #endregion

    #region Methods
    private void SendPressed()
    {
        Pressed?.Invoke(this, EventArgs.Empty);

        if (PressedCommand?.CanExecute(PressedCommandParamter) ?? false)
            PressedCommand.Execute(PressedCommandParamter);
#if DEBUG
        DebugLogger.Log("Pressed sent");
#endif
    }

    private void SendReleased()
    {
        Released?.Invoke(this, EventArgs.Empty);

        if (ReleasedCommand?.CanExecute(ReleasedCommandParamter) ?? false)
            ReleasedCommand.Execute(ReleasedCommandParamter);
#if DEBUG
        DebugLogger.Log("Released sent");
#endif
    }

    private void SendExited()
    {
        Exited?.Invoke(this, EventArgs.Empty);

        if (ExitedCommand?.CanExecute(ExitedCommandParameter) ?? false)
            ExitedCommand.Execute(ExitedCommandParameter);
#if DEBUG
        DebugLogger.Log("Exited sent");
#endif
    }

    #endregion

}
