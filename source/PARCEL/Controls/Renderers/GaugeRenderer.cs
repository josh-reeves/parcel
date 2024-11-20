using PARCEL.Interfaces;

namespace PARCEL.Controls.Renderers;

public abstract class GaugeRenderer : IDrawable
{
    #region Constructors
    public GaugeRenderer() {}

    public GaugeRenderer(IGaugePARCEL control)
        => Parent = control;

    #endregion

    public IGaugePARCEL? Parent { get; set; }

    #region Methods
    public abstract void Draw(ICanvas canvas, RectF rect);

    protected void DrawIndicator(RectF rect)
    {
        if (Parent is not GaugePARCEL parent)
            return;

        parent.Indicator.TranslationX = parent.IndicatorBounds.X - (rect.Width / 2 - (parent.IndicatorBounds.Width / 2));
        parent.Indicator.TranslationY = parent.IndicatorBounds.Y - (rect.Height / 2 - (parent.IndicatorBounds.Height / 2));
    
    }

    #endregion

}