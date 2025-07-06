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

        // Creates a float storing the x coordinate of the last touched point if its value is greater than or equal to zero. Otherwise, set to 0: 
        float width = Math.Abs(parent.WorkingCanvas.Width),
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

        /* Add the minimum value to the X coordinate of the last touched point.
         * Subtract half of thickness to account for endcaps.
         * Divide by width of working canvas to convert to a decimal representation of X value as compared to canvas:*/
        parent.Value = Math.Round(
        parent.ValueMin + (touchPos / width * (parent.ValueMax - parent.ValueMin)),
        parent.Precision);

    }

}