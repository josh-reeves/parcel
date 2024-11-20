using System;

namespace PARCEL.Interfaces;

public interface IInputStrategy
{
    public void HandleInput(IControlPARCEL control, object? args);

}
