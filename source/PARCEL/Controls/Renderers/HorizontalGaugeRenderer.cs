using PARCEL.Helpers;
using PARCEL.Interfaces;

namespace PARCEL.Controls.Renderers;

public class HorizontalGaugeRenderer : GaugeRenderer
{
    #region Constructors
    public HorizontalGaugeRenderer()
    {


    }

    public HorizontalGaugeRenderer(IGaugePARCEL control) : base(control) { }
 
    #endregion

    public override void Draw(ICanvas canvas, RectF rect)
    {
        if (Parent is not GaugePARCEL parent)
            return;

        float offset = 2,
              inputMargin = (float)(parent.Indicator?.Width ?? parent.Thickness) / 2,
              valuePos;
    
        if (parent.Reverse)
            parent.WorkingCanvas = RectF.FromLTRB(
                rect.Right - offset - inputMargin,
                rect.Top + offset,
                rect.Left + offset + inputMargin,
                rect.Bottom - offset);
        else
            parent.WorkingCanvas = RectF.FromLTRB(
                rect.Left + offset + inputMargin,
                rect.Top + offset,
                rect.Right - offset - inputMargin,
                rect.Bottom - offset);

        valuePos = parent.WorkingCanvas.Left + (float)((parent.Value - parent.ValueMin) / (parent.ValueMax - parent.ValueMin)) * parent.WorkingCanvas.Width;
        
        parent.IndicatorBounds = new()
        {
            X = valuePos - inputMargin,
            Y = parent.WorkingCanvas.Center.Y - inputMargin,
            Width = (float)(parent.Indicator?.Width ?? parent.Thickness),
            Height = (float)(parent.Indicator?.Height ?? parent.Thickness)

        };

        canvas.StrokeLineCap = parent.LineCap;
        canvas.StrokeSize = parent.Thickness + parent.StrokeThickness;
        canvas.StrokeColor = parent.StrokeColor;

        canvas.DrawLine(parent.WorkingCanvas.Left,
            parent.WorkingCanvas.Center.Y,
            parent.WorkingCanvas.Right,
            parent.WorkingCanvas.Center.Y);

        canvas.StrokeSize = parent.Thickness;
        canvas.StrokeColor = parent.EmptyColor;

        canvas.DrawLine(parent.WorkingCanvas.Left,
            parent.WorkingCanvas.Center.Y,
            parent.WorkingCanvas.Right,
            parent.WorkingCanvas.Center.Y);

        canvas.StrokeColor = parent.FillColor;

        canvas.DrawLine(parent.WorkingCanvas.Left,
            parent.WorkingCanvas.Center.Y,
            valuePos,
            parent.WorkingCanvas.Center.Y);

        if (parent.Indicator is not null)
            DrawIndicator(rect);
#if DEBUG
        DebugLogger.Log("Rendering complete.");
#endif
    }

}