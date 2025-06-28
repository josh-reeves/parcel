using PARCEL.Interfaces;

namespace PARCEL.Controls.Renderers;

public abstract class ButtonRenderer : IDrawable
{
    #region Fields
    protected const float offset = 1.0f;

    #endregion
    #region Constructors
    public ButtonRenderer() { }

    public ButtonRenderer(IButtonPARCEL control)
        => Parent = control;

    #endregion

    #region Properties
    public IButtonPARCEL? Parent;

    #endregion

    #region Methods
    public abstract void Draw(ICanvas canvas, RectF rect);

    #endregion

}