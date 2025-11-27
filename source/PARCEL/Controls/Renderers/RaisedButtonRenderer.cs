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

        rect = GetSafeMargins(rect, offset);

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
                Parent.StrokeColor.Color);

            if (content != null)
            {
                content.TranslationY = 0 + (Parent.Offset / 2);
               
            }

            return;

        }

        if (Parent.ButtonShape.GetType() == typeof(Ellipse))
        {
            DrawEllipseBackground(canvas, rect);

        }
        else
        {
            Designer.FillShape(canvas,
                rect,
                Parent.ButtonShape,
                Parent.OffsetColor);

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
            DrawEllipseOutline(canvas, rect);

        }
        else
        {
            Designer.OutlineShape(canvas,
                rect,
                Parent.ButtonShape,
                Parent.StrokeColor.Color);

        }

        if (content != null)
        {
            content.TranslationY = 0 - (Parent.Offset / 2);
          
        }

    }

    private void DrawEllipseBackground(ICanvas canvas, RectF rect)
    {
        if (Parent is not ButtonPARCEL)
        {
            return;

        }

        Path background = new();

        background.Data = GeometryConverter.ConvertFromString(
            "M " + rect.Left + ',' + (rect.Bottom - Parent.Offset) / 2 + 
            "L " + rect.Left + ',' + (((rect.Bottom - Parent.Offset) / 2) + Parent.Offset) + 
            "A " + rect.Width / 2 + ',' + (rect.Bottom - Parent.Offset) / 2 + " 0 0,0 " + rect.Right + ',' + (((rect.Bottom - Parent.Offset) / 2) + Parent.Offset) +
            "L " + rect.Right + ',' + ((rect.Bottom - Parent.Offset) / 2) + " z") as Geometry;

            Designer.FillShape(canvas,
                rect,
                background,
                Parent.OffsetColor);
        
    }

    private void DrawEllipseOutline(ICanvas canvas, RectF rect)
    {
        if (Parent is not ButtonPARCEL)
        {
            return;

        }

        Path outline = new();

        outline.Data = GeometryConverter.ConvertFromString(
            "M " + rect.Left + ',' + (rect.Bottom - Parent.Offset) / 2 + 
            "L " + rect.Left + ',' + ((rect.Bottom - Parent.Offset) / 2 + Parent.Offset) + 
            "A " + rect.Width / 2 + ',' + (rect.Bottom - Parent.Offset) / 2 + " 180 0,0 " + rect.Right + ',' + (((rect.Bottom - Parent.Offset) / 2) + Parent.Offset) +
            "L " + rect.Right + ',' + ((rect.Bottom - Parent.Offset) / 2) +
            "A " + (rect.Width / 2) + ',' + (rect.Bottom - Parent.Offset) / 2  + " 180 0,0 " + rect.Left + ',' + (rect.Bottom - Parent.Offset) / 2 + " z") as Geometry;

        Designer.OutlineShape(canvas,
            rect,
            outline,
            Parent.StrokeColor.Color);

        
    }

    #endregion

}