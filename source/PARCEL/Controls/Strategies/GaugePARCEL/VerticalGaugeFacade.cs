using PARCEL.Interfaces;

namespace PARCEL.Controls.Strategies.GaugePARCEL;

public class VerticalStrategy : GaugePARCELStrategy
{
    #region Fields
    private IDrawable? renderer;

    #endregion

    #region Constructors
    public VerticalStrategy(IGaugePARCEL parentControl) : base(parentControl) { }

    #endregion

    public override IDrawable Renderer
    {
        get => renderer ??= new VerticalRenderer(this);

    }

    public override void HandleInput(TouchEventArgs e)
        => throw new NotImplementedException();

    public class VerticalRenderer : GaugePARCELStrategyRenderer
    {
        public VerticalRenderer(IGaugePARCELStrategy parentStrategy) : base(parentStrategy) { }

        public override void Draw(ICanvas canvas, RectF rect)
        {


        }

    }

}
