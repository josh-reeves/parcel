using PARCEL.Controls;
using PARCEL.Controls.Behaviors;

namespace PARCEL.Interfaces;

public interface IGaugeStrategy
{
    public void HandleInput(IGaugePARCEL control, DragDetector.DragEventArgs args);
    
}