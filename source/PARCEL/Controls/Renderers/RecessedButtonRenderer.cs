using PARCEL.Interfaces;
using PARCEL.Helpers;
using System.Diagnostics;

namespace PARCEL.Controls.Renderers;

public class RecessedButtonRenderer : RendererPARCEL    
{
    #region Constructors
    public RecessedButtonRenderer() { }

    public RecessedButtonRenderer(IControlPARCEL parent) : base(parent) { }

    #endregion

    #region Methods
    public override void Draw(ICanvas canvas, RectF rect)
    {
        if (Parent is not IButtonPARCEL parent)
            return;

        VisualElement? content = parent.ButtonContent as VisualElement;

        rect = GetSafeMargins(rect, (float)(parent.StrokeWidth * 5 + defaultOffset));

        // Designer.FillShape(canvas, rect, Parent.ButtonShape, Parent.OffsetColor);

        if (parent.IsPressed)
        {

            Designer.FillShape(
                canvas,
                new RectF()
                {
                    Top = rect.Top + Math.Abs((float)parent.Offset),
                    Left = rect.Left ,
                    Width = rect.Width ,
                    Bottom = rect.Bottom 

                },
                parent.ButtonShape,
                parent.PressedColor);

            if (content != null)
                content.TranslationY = 0 + (Math.Abs(parent.Offset) / 2);

        }
        else
        {
            Designer.FillShape(canvas, rect, parent.ButtonShape, parent.ButtonColor);

            if (content != null)
                content.TranslationY = 0;

        }

        Designer.OutlineShape(canvas, rect, parent.ButtonShape, parent.StrokeColor.Color, (float)parent.StrokeWidth);

    }

    #endregion

}