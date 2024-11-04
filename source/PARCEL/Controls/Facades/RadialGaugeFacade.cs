using PARCEL.Interfaces;
using PARCEL.Helpers;
using System.Runtime.CompilerServices;

namespace PARCEL.Controls.Facades;

public class RadialGaugeFacade : GaugePARCELFacade
{
    #region Fields
    private IDrawable? renderer;

    #endregion

    #region Constructors
    public RadialGaugeFacade() { }

    public RadialGaugeFacade(IGaugePARCEL parentControl) : base(parentControl) { }

    #endregion

    public override IDrawable Renderer
    {
        get => renderer ??= new RadialRenderer(this);

    }

    public override void HandleInput(TouchEventArgs e)
    {

        if (Control is null)
            return;

        double inputThreshold = 20;

        PointF startPosPoint = GeometryUtil.EllipseAngleToPoint(WorkingCanvas.Left, WorkingCanvas.Top, WorkingCanvas.Width, WorkingCanvas.Height, Control.StartPos);

        if (Mathematician.GetAngle(WorkingCanvas.Center, e.Touches.Last(), startPosPoint) <= (360f - (Math.Abs(Control.StartPos) - Math.Abs(Control.EndPos))) + inputThreshold)
            Control.Value = Math.Round(Control.ValueMin + (Mathematician.GetAngle(WorkingCanvas.Center, e.Touches.Last(), startPosPoint) / (360f - (Math.Abs(Control.StartPos) - Math.Abs(Control.EndPos))) * (Control.ValueMax - Control.ValueMin)), Control.Precision);

    }

    public class RadialRenderer : GaugeFacadeRenderer
    {
        #region Constructors
        public RadialRenderer(IGaugePARCELStrategy parentStrategy) : base(parentStrategy) { }

        #endregion

        #region Methods
        public override void Draw(ICanvas canvas, RectF rect)
        {
            float valuePos;

            if (Parent is not RadialGaugeFacade parent || parent.Control is null)
                return;

            parent.WorkingCanvas = new()
            {
                Width = rect.Width - (parent.Control.Thickness + parent.Control.StrokeThickness),
                Height = rect.Height - (parent.Control.Thickness + parent.Control.StrokeThickness),
                Left = rect.Left + (float)((parent.Control.Thickness + parent.Control.StrokeThickness) / 2),
                Top = rect.Top + (float)((parent.Control.Thickness + parent.Control.StrokeThickness) / 2)

            };

            valuePos = parent.Control.StartPos - (float)((parent.Control.Value - parent.Control.ValueMin) / (parent.Control.ValueMax - parent.Control.ValueMin) * (360f - (Math.Abs(parent.Control.StartPos) - Math.Abs(parent.Control.EndPos))));

            canvas.StrokeLineCap = parent.Control.LineCap;
            canvas.StrokeSize = parent.Control.Thickness + parent.Control.StrokeThickness;
            canvas.StrokeColor = parent.Control.StrokeColor;

            if (parent.Control.EmptyColor.Alpha > 0)
                canvas.DrawArc(
                    parent.WorkingCanvas.Left,
                    parent.WorkingCanvas.Top,
                    parent.WorkingCanvas.Width,
                    parent.WorkingCanvas.Height,
                    parent.Control.StartPos,
                    parent.Control.EndPos,
                    clockwise: true,
                    closed: false);

            if (parent.Control.EmptyColor.Alpha == 0)
                canvas.DrawArc(
                    parent.WorkingCanvas.Left,
                    parent.WorkingCanvas.Top,
                    parent.WorkingCanvas.Width,
                    parent.WorkingCanvas.Height,
                    parent.Control.StartPos,
                    valuePos,
                    clockwise: true,
                    closed: false);

            canvas.StrokeSize = parent.Control.Thickness;
            canvas.StrokeColor = parent.Control.EmptyColor;

            canvas.DrawArc(
                parent.WorkingCanvas.Left,
                parent.WorkingCanvas.Top,
                parent.WorkingCanvas.Width,
                parent.WorkingCanvas.Height,
                parent.Control.StartPos,
                parent.Control.EndPos,
                clockwise: true,
                closed: false);

            canvas.StrokeColor = parent.Control.FillColor;

            canvas.DrawArc(
                parent.WorkingCanvas.Left,
                parent.WorkingCanvas.Top,
                parent.WorkingCanvas.Width,
                parent.WorkingCanvas.Height,
                parent.Control.StartPos,
                valuePos,
                clockwise: true,
                closed: false);

            PointF indicatorPos = GeometryUtil.EllipseAngleToPoint(
                parent.WorkingCanvas.Left,
                parent.WorkingCanvas.Top,
                parent.WorkingCanvas.Width,
                parent.WorkingCanvas.Height,
                Math.Abs(valuePos));

            parent.IndicatorBounds = new(
                (float)(indicatorPos.X - ((parent.Control.Indicator?.Width ?? parent.Control.Thickness) / 2)),
                (float)(indicatorPos.Y - ((parent.Control.Indicator?.Height ?? parent.Control.Thickness) / 2)),
                (float)(parent.Control.Indicator?.Width ?? parent.Control.Thickness),
                (float)(parent.Control.Indicator?.Height ?? parent.Control.Thickness));

            if (parent.Control.Indicator is not null)
                DrawIndicator(rect);

        }

        #endregion

    }

}
