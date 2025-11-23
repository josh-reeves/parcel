using System.Windows.Input;

namespace PARCEL.Interfaces;

public interface IButtonPARCEL : IControlPARCEL
{
    #region Events
    public event EventHandler Pressed;
    public event EventHandler Released;

    #endregion

    #region Properties
    public bool IsPressed 
    {
        get; set;
    
    }

    public double Offset
    {
        get; set;

    }

    public double StrokeWidth
    {
        get;
        set;

    }

    public IShape ButtonShape
    {
        get;
        set;

    }

    public IView ButtonContent
    {
        get;
        set;

    }

    public ICommand ReleasedCommand
    {
        get;
        set;

    }

    public ICommand PressedCommand
    {
        get;
        set;

    }

    public object CommandParameter
    {
        get;
        set;

    }

    public SolidColorBrush StrokeColor
    {
        get;
        set;

    }

    public Brush ButtonColor
    {
        get;
        set;

    }

    public Brush PressedColor
    {
        get;
        set;

    }

    public Brush OffsetColor
    {
        get;
        set;

    }

    #endregion

}
