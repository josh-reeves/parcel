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

        VisualElement? content = Parent.ButtonContent as VisualElement;

        if (Parent.IsPressed)
        {
            Designer.FillShape(canvas,
                new RectF()
                {
                    Top = rect.Top + (float)Parent.Offset,
                    Left = rect.Left,
                    Width = rect.Width,
                    Bottom = rect.Bottom

                },
                Parent.ButtonShape,
                Parent.PressedColor);

            Designer.OutlineShape(
                canvas,
                new RectF()
                {
                    Top = rect.Top  + (float)Parent.Offset,
                    Left = rect.Left,
                    Width = rect.Width,
                    Bottom = rect.Bottom

                },
                Parent.ButtonShape,
                Parent.StrokeColor.Color,
                (float)Parent.StrokeWidth);

            if (content != null)
            {
                content.TranslationY = 0 + (Parent.Offset / 2);
               
            }

            return;

        }

        if (Parent.ButtonShape.GetType() == typeof(Ellipse))
        {
            DrawEllipseBackground(canvas, rect, Parent);

        }
        else
        {
            Designer.FillShape(canvas, rect, Parent.ButtonShape, Parent.OffsetColor);

        }

        Designer.FillShape(
            canvas,
            new RectF()
            {
                Top = rect.Top,
                Left = rect.Left,
                Width = rect.Width,
                Bottom = rect.Bottom - (float)Parent.Offset

            },
            Parent.ButtonShape,
            Parent.ButtonColor);

        if (Parent.ButtonShape.GetType() == typeof(Ellipse))
        {
            DrawEllipseOutline(canvas, rect, Parent);

        }
        else
        {
            Designer.OutlineShape(canvas, rect, Parent.ButtonShape, Parent.StrokeColor.Color, (float)Parent.StrokeWidth);

        }

        if (content != null)
        {
            content.TranslationY = 0 - (Parent.Offset / 2);
          
        }

    }

    private void DrawEllipseBackground(ICanvas canvas, RectF rect, IButtonPARCEL parent)
    {
        Path background = new();

        double offsetYPos = (rect.Top + rect.Bottom - parent.Offset) / 2;

        background.Data = GeometryConverter.ConvertFromString(
            "M " + rect.Left + ',' + offsetYPos + 
            "L " + rect.Left + ',' + (offsetYPos + parent.Offset) + 
            "A " + rect.Width / 2 + ',' + (rect.Bottom - parent.Offset) / 2 + " 0 0,0 " + rect.Right + ',' + (offsetYPos + parent.Offset) +
            "L " + rect.Right + ',' + offsetYPos + " Z") as Geometry;

        Designer.FillShape(canvas,
            rect,
            background,
            parent.OffsetColor);
    
    }

    private void DrawEllipseOutline(ICanvas canvas, RectF rect, IButtonPARCEL parent)
    {
        Path outline = new();

        double offsetYPos = (rect.Top + rect.Bottom - parent.Offset) / 2;

        outline.Data = GeometryConverter.ConvertFromString(
            "M " + rect.Left + ',' + offsetYPos + 
            "L " + rect.Left + ',' + (offsetYPos + parent.Offset) + 
            "A " + rect.Width / 2 + ',' + (rect.Bottom - parent.Offset) / 2 + " 0 0,0 " + rect.Right + ',' + (offsetYPos + parent.Offset) +
            "L " + rect.Right + ',' + offsetYPos +
            "A " + rect.Width / 2 + ',' + ((rect.Bottom - parent.Offset) / 2 )  + " 0 0,0 " + rect.Left + ',' + offsetYPos + " z") as Geometry;

        Designer.OutlineShape(canvas,
            rect,
            outline,
            parent.StrokeColor.Color,
            (float)parent.StrokeWidth);
            
    }

    #endregion

}