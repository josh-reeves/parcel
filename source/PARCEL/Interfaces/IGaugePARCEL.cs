using System.Windows.Input;

namespace PARCEL.Interfaces;

public interface IGaugePARCEL : IControlPARCEL
{
    #region Enums
    public enum MeterStyle
    {
        Horizontal,
        Vertical,
        Radial

    }

    #endregion

    #region Properties
    public bool TouchEnabled { get; set; }

    public bool DisplayValue { get; set; }

    public bool Reverse { get; set; }

    public int Precision { get; set; }

    public float Thickness { get; set; }

    public float StrokeThickness { get; set; }

    public float StartPos { get; set; }

    public float EndPos { get; set; }

    public double Value { get; set; }

    public double ValueMin { get; set; }

    public double ValueMax { get; set; }
    
    public double FontSize { get; set; }

    public string FontFamily { get; set; }

    public LineCap LineCap { get; set; }

    public MeterStyle Appearance { get; set; }

    public ICommand ValueChangedCommand { get; set; }

    public IIndicatorPARCEL Indicator { get; set; }

    public Color EmptyColor { get; set; }

    public Color FillColor { get; set; }

    public Color StrokeColor { get; set; }

    public Color FontColor { get; set; }

    #endregion

}