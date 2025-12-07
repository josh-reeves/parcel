using PARCEL.Interfaces;
using PARCEL.Helpers;
using System.Diagnostics;
using Microsoft.Maui.Controls.Shapes;

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

        canvas.ClipPath(Designer.GetProjectedPath(parent.ButtonShape, rect));

        rect = GetSafeMargins(rect, (float)parent.StrokeWidth / 2 + defaultOffset);

        Designer.FillShape(canvas, rect, parent.ButtonShape, parent.OffsetColor);

        if (parent.IsPressed)
        {

            Designer.FillShape(
                canvas,
                new RectF()
                {
                    Top = rect.Top + Math.Abs((float)parent.Offset),
                    Left = rect.Left ,
                    Width = rect.Width ,
                    Bottom = rect.Bottom + Math.Abs((float)parent.Offset)

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