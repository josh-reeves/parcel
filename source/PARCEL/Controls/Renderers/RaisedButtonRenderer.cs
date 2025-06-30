using PARCEL.Interfaces;
using PARCEL.Helpers;

namespace PARCEL.Controls.Renderers;

public class RaisedButtonRenderer : ButtonRenderer
{
    #region Constructors
    public RaisedButtonRenderer() {}

    public RaisedButtonRenderer(IButtonPARCEL control) : base(control) { }

    #endregion

    #region Methods
    public override void Draw(ICanvas canvas, RectF rect)
    {
        if (Parent is not ButtonPARCEL)
            return;

        VisualElement? content = Parent.ButtonContent as VisualElement;

        if (Parent.IsPressed)
        {
            Designer.FillShape(canvas,
                new RectF()
                {
                    Top = rect.Top + offset + (float)Parent.Offset,
                    Left = rect.Left + offset,
                    Width = rect.Width - (offset * 2),
                    Bottom = rect.Bottom - offset

                },
                Parent.ButtonShape,
                Parent.PressedColor);

            Designer.OutlineShape(
                canvas,
                new RectF()
                {
                    Top = rect.Top + offset + (float)Parent.Offset,
                    Left = rect.Left + offset,
                    Width = rect.Width - (offset * 2),
                    Bottom = rect.Bottom - offset

                },
                Parent.ButtonShape,
                Parent.StrokeColor.Color);

            if (content != null)
                content.TranslationY = 0 + (Parent.Offset / 2);

        }
        else
        {
            Designer.FillShape(canvas,
                               GetSafeMargins(rect, offset),
                               Parent.ButtonShape,
                               Parent.OffsetColor);

            Designer.FillShape(
                canvas,
                new RectF()
                {
                    Top = rect.Top + offset,
                    Left = rect.Left + offset,
                    Width = rect.Width - (offset * 2),
                    Bottom = rect.Bottom - offset - (float)Parent.Offset

                },
                Parent.ButtonShape,
                Parent.ButtonColor);

            Designer.OutlineShape(canvas,
                                  GetSafeMargins(rect, offset),
                                  Parent.ButtonShape,
                                  Parent.StrokeColor.Color);

            if (content != null)
                content.TranslationY = 0 - (Parent.Offset / 2);

        }

    }

    #endregion

}