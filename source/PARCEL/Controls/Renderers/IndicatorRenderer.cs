using PARCEL.Interfaces;
using PARCEL.Helpers;

namespace PARCEL.Controls.Renderers;

public class IndicatorRenderer : IDrawable
{
    #region Fields
    private const float offset = 1.0f;

    #endregion

    #region Constructors
    public IndicatorRenderer() { }

    public IndicatorRenderer(IndicatorPARCEL control)
        => Parent = control;

    #endregion

    #region Properties
    public IIndicatorPARCEL? Parent { get; set; }

    #endregion

    #region Methods
    public void Draw(ICanvas canvas, RectF rect)
    {
        if (Parent is not IndicatorPARCEL)
            return;

        Parent.IndicatorColor ??= Parent.IndicatorShape.BackgroundColor; // Set IndicatorColor to IndicatorShape.BackgroundColor if it is currently null;

        canvas.SetFillPaint(Parent.IndicatorColor, rect);

        Designer.FillShape(
            canvas,
            new RectF()
            {
                Left = rect.Left + offset,
                Top = rect.Top + offset,
                Width = rect.Width - (offset * 2),
                Height = rect.Height - (offset * 2)

            },
            Parent.IndicatorShape);

    }

    #endregion

}