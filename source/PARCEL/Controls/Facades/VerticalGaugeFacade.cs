using PARCEL.Controls.Behaviors;
using PARCEL.Interfaces;

namespace PARCEL.Controls.Facades;

public class VerticalGaugeFacade : GaugeFacade
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
    public override void HandleInput(DragDetector.DragEventArgs e)
        => throw new NotImplementedException();

    public class VerticalRenderer : GaugeFacadeRenderer
    {
        public VerticalRenderer(IGaugeFacade parentStrategy) : base(parentStrategy) { }

        public override void Draw(ICanvas canvas, RectF rect)
        {


        }

    }

}
