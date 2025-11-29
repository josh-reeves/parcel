using PARCEL.Interfaces;

namespace PARCEL.Controls.Renderers;

public class RadialGaugeRenderer : GaugeRenderer
{
    #region Constructors
    public RadialGaugeRenderer() {}

    public RadialGaugeRenderer(IGaugePARCEL control) : base(control) {}

    #endregion

    #region Methods
    public override void Draw(ICanvas canvas, RectF rect)
    {
        float valuePos;

        if (Parent is not GaugePARCEL parent)
            return;

        RectF WorkingCanvas = new()
        {
            Width = rect.Width - (parent.Thickness + parent.StrokeThickness),
            Height = rect.Height - (parent.Thickness + parent.StrokeThickness),
            Left = rect.Left + (float)((parent.Thickness + parent.StrokeThickness) / 2),
            Top = rect.Top + (float)((parent.Thickness + parent.StrokeThickness) / 2)

        };

        valuePos = parent.StartPos - (float)((parent.Value - parent.ValueMin) / (parent.ValueMax - parent.ValueMin) * (360f - (Math.Abs(parent.StartPos) - Math.Abs(parent.EndPos))));

        canvas.StrokeLineCap = parent.LineCap;
        canvas.StrokeSize = parent.Thickness + parent.StrokeThickness;
        canvas.StrokeColor = parent.StrokeColor;

        if (parent.EmptyColor.Alpha > 0)
            canvas.DrawArc(
                WorkingCanvas.Left,
                WorkingCanvas.Top,
                WorkingCanvas.Width,
                WorkingCanvas.Height,
                parent.StartPos,
                parent.EndPos,
                clockwise: true,
                closed: false);

        if (parent.EmptyColor.Alpha == 0)
            canvas.DrawArc(
                WorkingCanvas.Left,
                WorkingCanvas.Top,
                WorkingCanvas.Width,
                WorkingCanvas.Height,
                parent.StartPos,
                valuePos,
                clockwise: true,
                closed: false);

        canvas.StrokeSize = parent.Thickness;
        canvas.StrokeColor = parent.EmptyColor;

        canvas.DrawArc(
            WorkingCanvas.Left,
            WorkingCanvas.Top,
            WorkingCanvas.Width,
            WorkingCanvas.Height,
            parent.StartPos,
            parent.EndPos,
            clockwise: true,
            closed: false);

        canvas.StrokeColor = parent.FillColor;

        canvas.DrawArc(
            WorkingCanvas.Left,
            WorkingCanvas.Top,
            WorkingCanvas.Width,
            WorkingCanvas.Height,
            parent.StartPos,
            valuePos,
            clockwise: true,
            closed: false);

        PointF indicatorPos = GeometryUtil.EllipseAngleToPoint(
            WorkingCanvas.Left,
            WorkingCanvas.Top,
            WorkingCanvas.Width,
            WorkingCanvas.Height,
            Math.Abs(valuePos));

        parent.IndicatorBounds = new(
            (float)(indicatorPos.X - ((parent.Indicator?.Width ?? parent.Thickness) / 2)),
            (float)(indicatorPos.Y - ((parent.Indicator?.Height ?? parent.Thickness) / 2)),
            (float)(parent.Indicator?.Width ?? parent.Thickness),
            (float)(parent.Indicator?.Height ?? parent.Thickness));

        if (parent.Indicator is not null)
            DrawIndicator(rect);

    }

    #endregion

}