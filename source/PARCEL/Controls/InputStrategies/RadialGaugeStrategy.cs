using PARCEL.Controls.Behaviors;
using PARCEL.Helpers;
using PARCEL.Interfaces;

namespace PARCEL.Controls.InputStrategies;

public class RadialGaugeStrategy : IInputStrategy
{
    public RadialGaugeStrategy() {} 

    public void HandleInput(IControlPARCEL control, object? args)
    {
        if (control is not GaugePARCEL parent || args is not DragDetector.DragEventArgs e)
        {
            return;            
        
        }

        double inputThreshold = 20;

        PointF startPosPoint = GeometryUtil.EllipseAngleToPoint((float)(parent.ControlCanvas?.Bounds.Left ?? 0.0f), (float)(parent.ControlCanvas?.Frame.Top ?? 0.0f), (float)(parent.ControlCanvas?.Frame.Width ?? 0.0f), (float)(parent.ControlCanvas?.Frame.Height ?? 0.0f), parent.StartPos);

        if (Mathematician.GetAngle((PointF)(parent.ControlCanvas?.Bounds.Center ?? new(0.0, 0.0)), e.Points.Last(), startPosPoint) <= 360f - (Math.Abs(parent.StartPos) - Math.Abs(parent.EndPos)) + inputThreshold)
        {
            parent.Value = Math.Round(parent.ValueMin + (Mathematician.GetAngle((PointF)(parent.ControlCanvas?.Bounds.Center ?? new(0.0, 0.0)), e.Points.Last(), startPosPoint) / (360f - (Math.Abs(parent.StartPos) - Math.Abs(parent.EndPos))) * (parent.ValueMax - parent.ValueMin)), parent.Precision);            
        
        }

/*
        PointF startPosPoint = GeometryUtil.EllipseAngleToPoint(parent.WorkingCanvas.Left, parent.WorkingCanvas.Top, parent.WorkingCanvas.Width, parent.WorkingCanvas.Height, parent.StartPos);

        if (Mathematician.GetAngle(parent.WorkingCanvas.Center, e.Points.Last(), startPosPoint) <= 360f - (Math.Abs(parent.StartPos) - Math.Abs(parent.EndPos)) + inputThreshold)
        {
            parent.Value = Math.Round(parent.ValueMin + (Mathematician.GetAngle(parent.WorkingCanvas.Center, e.Points.Last(), startPosPoint) / (360f - (Math.Abs(parent.StartPos) - Math.Abs(parent.EndPos))) * (parent.ValueMax - parent.ValueMin)), parent.Precision);            
        
        }
*/

    }

}