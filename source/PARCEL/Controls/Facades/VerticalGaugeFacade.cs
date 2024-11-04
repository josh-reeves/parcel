using PARCEL.Interfaces;

namespace PARCEL.Controls.Facades;

public class VerticalGaugeFacade : GaugePARCELFacade
{
    #region Fields
    private IDrawable? renderer;

    #endregion

    #region Constructors
    public VerticalGaugeFacade() { }

    public VerticalGaugeFacade(IGaugePARCEL parentControl) : base(parentControl) { }

    #endregion

    public override IDrawable Renderer
    {
        get => renderer ??= new VerticalRenderer(this);

    }

    public override void HandleInput(TouchEventArgs e)
        => throw new NotImplementedException();

    public class VerticalRenderer : GaugeFacadeRenderer
    {
        public VerticalRenderer(IGaugePARCELStrategy parentStrategy) : base(parentStrategy) { }

        public override void Draw(ICanvas canvas, RectF rect)
        {


        }

    }

}
