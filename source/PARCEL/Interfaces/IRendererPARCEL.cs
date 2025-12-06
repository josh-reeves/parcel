using System;
using Microsoft.Maui.Controls.Shapes;

namespace PARCEL.Interfaces;

public interface IRendererPARCEL : IDrawable
{
    public IControlPARCEL? Parent { get; set; }

}