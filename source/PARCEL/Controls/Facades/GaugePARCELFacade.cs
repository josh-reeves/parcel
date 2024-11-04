using Microsoft.Maui.Graphics;
using PARCEL.Interfaces;

namespace PARCEL.Controls.Facades;

public abstract class GaugePARCELFacade : IGaugePARCELStrategy
{
    #region Constructors
    public GaugePARCELFacade() { }

    public GaugePARCELFacade(IGaugePARCEL parentControl)
        => Control = parentControl;

    #endregion

    #region Properties
    public abstract IDrawable Renderer { get; }

    public RectF WorkingCanvas { get; set; }

    public IGaugePARCEL? Control { get; set; }

    public RectF IndicatorBounds { get; set; }

    #endregion

    #region Methods
    public abstract void HandleInput(TouchEventArgs e);

    #endregion

    #region Classes
    public abstract class GaugeFacadeRenderer : IDrawable
    {
        #region Constructors
        public GaugeFacadeRenderer(IGaugePARCELStrategy parentFacade)
            => Parent = parentFacade;

        #endregion

        #region Properties
        protected IGaugePARCELStrategy Parent { get; private set; }

        #endregion

        #region Methods
        public abstract void Draw(ICanvas canvas, RectF rect);

        protected void DrawIndicator(RectF rect)
        {
            if (Parent.Control is null)
                return;

            Parent.Control.Indicator.TranslationX = Parent.IndicatorBounds.X - (rect.Width / 2 - (Parent.IndicatorBounds.Width / 2));
            Parent.Control.Indicator.TranslationY = Parent.IndicatorBounds.Y - (rect.Height / 2 - (Parent.IndicatorBounds.Height / 2));
        
        }

        #endregion

    }

    #endregion

}
