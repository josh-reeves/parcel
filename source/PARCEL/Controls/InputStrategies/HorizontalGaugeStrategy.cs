using System.Diagnostics;
using PARCEL.Controls.Behaviors;
using PARCEL.Interfaces;

namespace PARCEL.Controls.InputStrategies;

public class HorizontalGaugeStrategy : IInputStrategy
{
    public HorizontalGaugeStrategy() { }

    public void HandleInput(IControlPARCEL control, object? args)
    {
        if (control is not GaugePARCEL parent || args is not DragDetector.DragEventArgs e)
            return;

        parent.Value = Math.Round(parent.ValueMin + ((e.Points.Last().X - (parent.Thickness / 2)) / parent.WorkingCanvas.Width * (parent.ValueMax - parent.ValueMin)), parent.Precision);

    }

}