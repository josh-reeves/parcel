using PARCEL.Interfaces;
using PARCEL.Helpers;

namespace PARCEL.Controls.Renderers;

public class RecessedButtonRenderer : ButtonRenderer
{
    #region Constructors
    public RecessedButtonRenderer() { }

    public RecessedButtonRenderer(IButtonPARCEL control) : base(control) { }

    #endregion

    #region Methods
    public override void Draw(ICanvas canvas, RectF rect)
    {
        if (Parent is not IButtonPARCEL)
            return;

        VisualElement? content = Parent.ButtonContent as VisualElement;

        Designer.FillShape(canvas, GetSafeMargins(rect, offset), Parent.ButtonShape, Parent.OffsetColor);

        if (Parent.IsPressed)
        {

            Designer.FillShape(
                canvas,
                new RectF()
                {
                    Top = rect.Top + offset + Math.Abs((float)Parent.Offset),
                    Left = rect.Left + offset,
                    Width = rect.Width - (offset * 2),
                    Bottom = rect.Bottom - offset

                },
                Parent.ButtonShape,
                Parent.PressedColor);

            if (content != null)
                content.TranslationY = 0 + (Math.Abs(Parent.Offset) / 2);

        }
        else
        {
            Designer.FillShape(canvas, GetSafeMargins(rect, offset), Parent.ButtonShape, Parent.ButtonColor);

            if (content != null)
                content.TranslationY = 0;

        }

        Designer.OutlineShape(canvas, GetSafeMargins(rect, offset), Parent.ButtonShape, Parent.StrokeColor.Color);

    }

    #endregion

}