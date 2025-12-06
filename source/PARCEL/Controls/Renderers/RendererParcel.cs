using System;
using Microsoft.Maui.Controls.Shapes;
using PARCEL.Interfaces;

namespace PARCEL.Controls.Renderers;

public abstract class RendererPARCEL : IRendererPARCEL
{
    protected const float defaultOffset = 0.5f;

    public RendererPARCEL() 
    {
        GeometryConverter = new();

    }

    public RendererPARCEL(IControlPARCEL parent) 
    {
        Parent = parent;
        GeometryConverter = new();

    }

    public IControlPARCEL? Parent { get; set; }
  
    protected PathGeometryConverter GeometryConverter { get; private set;}

    public abstract void Draw(ICanvas canvas, RectF rect);

    protected RectF GetSafeMargins(RectF rect, float offset)
    {
        return new RectF()
        {
            Left = rect.Left + offset,
            Top = rect.Top + offset,
            Width = rect.Width - (offset * 2),
            Height = rect.Height - (offset * 2)

        };

    }

}
