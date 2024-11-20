using System;

namespace PARCEL.Interfaces;

public interface IStrategizedControl
{
    public IInputStrategy InputStrategy { get; set; }

}
