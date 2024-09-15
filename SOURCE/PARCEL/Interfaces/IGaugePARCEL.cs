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
    public bool Reverse { get; set; }

    public float StartPos { get; set; }

    public float EndPos { get; set; }

    public double Value { get; set; }

    public double ValueMin { get; set; }

    public double ValueMax { get; set; }

    public MeterStyle Appearance { get; set; }

    public IIndicatorPARCEL Indicator { get; set; }

    public Color EmptyColor { get; set; }

    public Color FillColor { get; set; }

    public Color StrokeColor { get; set; }

    #endregion

}
