using PARCEL.Interfaces;

namespace PARCEL.Controls.Renderers;

public class VerticalGaugeRenderer : GaugeRenderer
{
    #region Constructors
    public VerticalGaugeRenderer() {}

    public VerticalGaugeRenderer(IGaugePARCEL control) : base(control) {}

    #endregion

    #region Methods
    public override void Draw(ICanvas canvas, RectF rect)
    {
        if (Parent is not GaugePARCEL)
            return;

        throw new NotImplementedException();

    }

    #endregion

} 