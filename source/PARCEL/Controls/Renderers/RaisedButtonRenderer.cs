using PARCEL.Interfaces;
using PARCEL.Helpers;
using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;

using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace PARCEL.Controls.Renderers;

public class RaisedButtonRenderer : ButtonRenderer
{
    #region Constructors
    public RaisedButtonRenderer() : base() {}

    public RaisedButtonRenderer(IButtonPARCEL control) : base(control) { }

    #endregion

    #region Methods
    public override void Draw(ICanvas canvas, RectF rect)
    {
        if (Parent is not ButtonPARCEL)
        {
            return;
            
        }

        rect = GetSafeMargins(rect, (float)Parent.StrokeWidth + defaultOffset);

        if (Parent.IsPressed)
        {
            DrawPressed(canvas, rect, Parent);

            return;

        }

        DrawReady(canvas, rect, Parent);

    }

    private void DrawPressed(ICanvas canvas, RectF rect, IButtonPARCEL parent)
    {
        Designer.FillShape(canvas,
            new RectF()
            {
                Top = rect.Top + (float)parent.Offset,
                Left = rect.Left,
                Width = rect.Width,
                Bottom = rect.Bottom

            },
            parent.ButtonShape,
            parent.PressedColor);

        Designer.OutlineShape(
            canvas,
            new RectF()
            {
                Top = rect.Top  + (float)parent.Offset,
                Left = rect.Left,
                Width = rect.Width,
                Bottom = rect.Bottom

            },
            parent.ButtonShape,
            parent.StrokeColor.Color,
            (float)parent.StrokeWidth);

        if (parent.ButtonContent is VisualElement)
        {
            ((VisualElement)parent.ButtonContent).TranslationY = 0 + (parent.Offset / 2);
            
        }

    }

    private void DrawReady(ICanvas canvas, RectF rect, IButtonPARCEL parent)
    {
        RectF buttonFace = new RectF()
        {
            Top = rect.Top,
            Left = rect.Left,
            Width = rect.Width,
            Bottom = rect.Bottom - (float)parent.Offset

        };

        double ellipseOffsetTopY = (rect.Top + rect.Bottom - parent.Offset) / 2;

        if (parent.ButtonShape.GetType() == typeof(Ellipse))
        {
            Path background = new();

            background.Data = GeometryConverter.ConvertFromString(
                "M " + rect.Left + ',' + ellipseOffsetTopY + 
                "L " + rect.Left + ',' + (ellipseOffsetTopY + parent.Offset) + 
                "A " + rect.Width / 2 + ',' + (rect.Bottom - parent.Offset) / 2 + " 0 0,0 " + rect.Right + ',' + (ellipseOffsetTopY + parent.Offset) +
                "L " + rect.Right + ',' + ellipseOffsetTopY + " Z") as Geometry;

            Designer.FillShape(canvas, rect, background, parent.OffsetColor);

        }
        else
        {
            Designer.FillShape(canvas, rect, parent.ButtonShape, parent.OffsetColor);

        }

        Designer.FillShape(canvas, buttonFace, parent.ButtonShape, parent.ButtonColor);

        if (parent.ButtonShape.GetType() == typeof(Ellipse))
        {
            Path outline = new();

            outline.Data = GeometryConverter.ConvertFromString(
                "M " + rect.Left + ',' + ellipseOffsetTopY + 
                "L " + rect.Left + ',' + (ellipseOffsetTopY + parent.Offset) + 
                "A " + rect.Width / 2 + ',' + (rect.Bottom - parent.Offset) / 2 + " 0 0,0 " + rect.Right + ',' + (ellipseOffsetTopY + parent.Offset) +
                "L " + rect.Right + ',' + ellipseOffsetTopY +
                "A " + rect.Width / 2 + ',' + ((rect.Bottom - parent.Offset) / 2 )  + " 0 0,0 " + rect.Left + ',' + ellipseOffsetTopY + " z") as Geometry;

            Designer.OutlineShape(canvas, rect, outline, parent.StrokeColor.Color, (float)parent.StrokeWidth);

        }
        else
        {
            Designer.OutlineShape(canvas, rect, parent.ButtonShape, parent.StrokeColor.Color, (float)parent.StrokeWidth);

        }

        if (parent.ButtonContent is VisualElement)
        {
            ((VisualElement)parent.ButtonContent).TranslationY = 0 - (parent.Offset / 2);
          
        }

    }

    #endregion

}