using Microsoft.Maui.Controls.Shapes;
using PARCEL.Interfaces;

namespace PARCEL.Controls.Renderers;

public abstract class ButtonRenderer : IDrawable
{
    #region Fields
    protected const float defaultOffset = 0.5f;

    #endregion
    #region Constructors
    public ButtonRenderer()
        => GeometryConverter = new();

    public ButtonRenderer(IButtonPARCEL control) : this()
        => Parent = control;

    #endregion

    #region Properties
    public IButtonPARCEL? Parent { get; set; }
    
    protected PathGeometryConverter GeometryConverter { get; private set;}

    #endregion

    #region Methods
    public abstract void Draw(ICanvas canvas, RectF rect);

    /// <summary>
    /// Returns a new RectF whose measurements have been reduced by the value of offset.
    /// This was built primarily as a workaround for this issue: https://github.com/dotnet/maui/issues/8629
    /// Presumably, if MS fixes the bug so that canvas edges no longer clip on Windows/iOS, this won't be needed anymore.
    /// </summary>
    /// <param name="rect">The RectF that will act as a base for the new RectF the method returns.</param>
    /// <param name="offset">The value by which to reduce the perimeter of the new RectF the method returns.</param>
    /// <returns></returns>
    public RectF GetSafeMargins(RectF rect, float offset)
    {
        return new RectF()
        {
            Left = rect.Left + offset,
            Top = rect.Top + offset,
            Width = rect.Width - (offset * 2),
            Height = rect.Height - (offset * 2)

        };

    }

    #endregion

}