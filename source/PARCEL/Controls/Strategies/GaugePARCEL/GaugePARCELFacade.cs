using Microsoft.Maui.Graphics;
using PARCEL.Interfaces;

namespace PARCEL.Controls.Strategies.GaugePARCEL;

public abstract class GaugePARCELStrategy : IGaugePARCELStrategy
{
    public GaugePARCELStrategy(IGaugePARCEL parentControl)
        => Control = parentControl;

    public abstract void HandleInput(TouchEventArgs e);

    protected IGaugePARCEL Control { get; set; }

    protected RectF WorkingCanvas { get; set; }

    public RectF IndicatorBounds { get; set; }

    public abstract IDrawable Renderer { get; }

    public abstract class GaugePARCELStrategyRenderer : IDrawable
    {
        #region Constructors
        public GaugePARCELStrategyRenderer(IGaugePARCELStrategy parentStrategy)
            => Parent = parentStrategy;

        #endregion

        #region Properties
        protected IGaugePARCELStrategy Parent { get; private set; }

        #endregion

        #region Methods
        public abstract void Draw(ICanvas canvas, RectF rect);

        protected void DrawIndicator(RectF rect)
        {
            if (Parent is not GaugePARCELStrategy parent)
                return;

            parent.Control.Indicator.TranslationX = parent.IndicatorBounds.X - (rect.Width / 2 - (parent.IndicatorBounds.Width / 2));
            parent.Control.Indicator.TranslationY = parent.IndicatorBounds.Y - (rect.Height / 2 - (parent.IndicatorBounds.Height / 2));
        
        }

        #endregion

    }

}
