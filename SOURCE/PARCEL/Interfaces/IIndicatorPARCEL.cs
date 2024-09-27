using Microsoft.Maui.Controls.Shapes;

namespace PARCEL.Interfaces;

public interface IIndicatorPARCEL : IControlPARCEL
{
    public IGaugePARCEL IndicatorGauge { get; set; }
    public Brush IndicatorColor { get; set; }
    public Shape IndicatorShape { get; set; }
    public Image IndicatorIcon { get; set; }

}
