using PARCEL.Interfaces;
using PARCEL.Helpers;
using System.Diagnostics;

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

        rect = GetSafeMargins(rect, (float)(Parent.StrokeWidth * 5 + defaultOffset));

        // Designer.FillShape(canvas, rect, Parent.ButtonShape, Parent.OffsetColor);

        if (Parent.IsPressed)
        {

            Designer.FillShape(
                canvas,
                new RectF()
                {
                    Top = rect.Top + Math.Abs((float)Parent.Offset),
                    Left = rect.Left ,
                    Width = rect.Width ,
                    Bottom = rect.Bottom 

                },
                Parent.ButtonShape,
                Parent.PressedColor);

            if (content != null)
                content.TranslationY = 0 + (Math.Abs(Parent.Offset) / 2);

        }
        else
        {
            Designer.FillShape(canvas, rect, Parent.ButtonShape, Parent.ButtonColor);

            if (content != null)
                content.TranslationY = 0;

        }

        Designer.OutlineShape(canvas, rect, Parent.ButtonShape, Parent.StrokeColor.Color, (float)Parent.StrokeWidth);

    }

    #endregion

}