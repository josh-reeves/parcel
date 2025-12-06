using PARCEL.Interfaces;
using PARCEL.Helpers;
using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;

using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace PARCEL.Controls.Renderers;

public class RaisedButtonRenderer : RendererPARCEL
{
    #region Constructors
    public RaisedButtonRenderer() : base() {}

    public RaisedButtonRenderer(IButtonPARCEL parent) : base(parent) { }

    #endregion

    #region Methods
    public override void Draw(ICanvas canvas, RectF rect)
    {
        if (Parent is not IButtonPARCEL parent)
        {
            return;
            
        }

        rect = GetSafeMargins(rect, (float)parent.StrokeWidth + defaultOffset);

        if (parent.IsPressed)
        {
            DrawPressedState(canvas, rect, parent);

            return;

        }

        DrawDefaultState(canvas, rect, parent);

    }

    /// <summary>
    /// Draws the button in its pressed state.
    /// </summary>
    /// <param name="canvas">The canvas onto which the image will be drawn.</param>
    /// <param name="rect">The bounds for the button.</param>
    /// <param name="parent">An object implementing IButtonPARCEL that contains the shape, color, etc. to render.</param>
    private void DrawPressedState(ICanvas canvas, RectF rect, IButtonPARCEL parent)
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

    /// <summary>
    /// Draws the button in its default (unpressed) state.
    /// </summary>
    /// <param name="canvas">The canvas onto which the image will be drawn.</param>
    /// <param name="rect">The bounds for the button.</param>
    /// <param name="parent">An object implementing IButtonPARCEL that contains the shape, color, etc. to render.</param>
    private void DrawDefaultState(ICanvas canvas, RectF rect, IButtonPARCEL parent)
    {
        Shape offset = parent.ButtonShape;

        RectF buttonFace = new RectF()
        {
            Top = rect.Top,
            Left = rect.Left,
            Width = rect.Width,
            Bottom = rect.Bottom - (float)parent.Offset

        };

        /* Checks to see if ButtonShape is an Ellipse to create a custom shape for the offset and outline.
         * This is necessary because adjusting the RectF used to draw the ellipse will distort the shape.*/
        if (parent.ButtonShape.GetType() == typeof(Ellipse))
        {
            double ellipseOffsetTopY = (rect.Top + buttonFace.Bottom) / 2;

            offset = new Path();

            ((Path)offset).Data = GeometryConverter.ConvertFromString(
                "M " + rect.Left + ',' + ellipseOffsetTopY + 
                "L " + rect.Left + ',' + (ellipseOffsetTopY + parent.Offset) + 
                "A " + rect.Width / 2 + ',' + (buttonFace.Bottom / 2) + " 0 0,0 " + rect.Right + ',' + (ellipseOffsetTopY + parent.Offset) +
                "L " + rect.Right + ',' + ellipseOffsetTopY +
                "A " + rect.Width / 2 + ',' + (buttonFace.Bottom / 2)  + " 0 0,0 " + rect.Left + ',' + ellipseOffsetTopY + " z") as Geometry;

        }

        Designer.FillShape(canvas, rect, offset, parent.OffsetColor);

        Designer.FillShape(canvas, buttonFace, parent.ButtonShape, parent.ButtonColor);

        Designer.OutlineShape(canvas, rect, offset, parent.StrokeColor.Color, (float)parent.StrokeWidth);

        if (parent.ButtonContent is VisualElement)
        {
            ((VisualElement)parent.ButtonContent).TranslationY = 0 - (parent.Offset / 2);
          
        }

    }

    #endregion

}