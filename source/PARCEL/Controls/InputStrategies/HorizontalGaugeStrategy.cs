using System.Diagnostics;
using PARCEL.Controls.Behaviors;
using PARCEL.Helpers;
using PARCEL.Interfaces;

namespace PARCEL.Controls.InputStrategies;

public class HorizontalGaugeStrategy : IInputStrategy
{
    public HorizontalGaugeStrategy() { }

    public void HandleInput(IControlPARCEL control, object? args)
    {
        if (control is not GaugePARCEL parent || args is not DragDetector.DragEventArgs e)
            return;

        float width = Math.Abs((float)(parent.ControlCanvas?.Bounds.Width - (float)(parent.Indicator?.Width ?? parent.Thickness) ?? 0.0f)),
              point;
        double touchPos;

        if (parent.Reverse)
        {
            point = width - e.Points.Last().X;
            touchPos = (point + ((parent.Indicator?.Width ?? parent.Thickness) / 2)) >= 0 ? (point + ((parent.Indicator?.Width ?? parent.Thickness) / 2)) : 0;

        }
        else
        {
            point = e.Points.Last().X;
            touchPos = (point - ((parent.Indicator?.Width ?? parent.Thickness) / 2)) >= 0 ? (point - ((parent.Indicator?.Width ?? parent.Thickness) / 2)) : 0;

        }

        parent.Value = Math.Round(
        parent.ValueMin + (touchPos / width * (parent.ValueMax - parent.ValueMin)),
        parent.Precision);

    }

}