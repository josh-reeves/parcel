using Microsoft.Maui.Controls.Shapes;
using System.Windows.Input;

namespace PARCEL.Interfaces;

public interface IButtonPARCEL : IControlPARCEL
{
    #region Events
    public event EventHandler Pressed;
    public event EventHandler Released;

    #endregion

    #region Enums
    public enum ButtonStyle
    {
        Flat,
        Raised,
        Recessed

    }

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

    public double FontSize
    {
        get;
        set;

    }

    public double TextOpacity
    {
        get;
        set;

    }

    public string Text
    {
        get;
        set;

    }

    public string Font
    {
        get;
        set;

    }

    public ButtonStyle Appearance
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

    public IStackLayout ButtonContents
    {
        get;
        set;

    }

    public IIndicatorPARCEL ButtonIcon
    {
        get;
        set;

    }

    public object CommandParameter
    {
        get;
        set;

    }

    public Color TextColor
    {
        get;
        set;

    }

    public Brush ButtonColor
    {
        get;
        set;

    }

    public Brush OffsetColor
    {
        get;
        set;

    }

    public Brush PressedColor
    {
        get;
        set;

    }

    public Brush StrokeColor
    {
        get;
        set;

    }

    public Shape ButtonShape
    {
        get;
        set;

    }

    #endregion

}
