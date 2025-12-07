using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics.Platform;
using System.Reflection;
using System.Runtime.CompilerServices;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace PARCEL.Helpers;

public static class Designer
{
    #region Constructors
    static Designer() { }

    #endregion

    #region Methods
    public static void OutlineShape(ICanvas canvas, RectF rect, IShape shape)
    {
        switch (shape)
        {
            case Rectangle:
                canvas.DrawRectangle(rect);

                break;

            case RoundRectangle:
                canvas.DrawRoundedRectangle(
                    rect,
                    (shape as RoundRectangle)?.CornerRadius.TopLeft ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.TopRight ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.BottomLeft ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.BottomRight ?? 0);

                break;

            case Ellipse:
                canvas.DrawEllipse(rect);

                break;

            case Polygon:
                canvas.DrawPath((shape as Shape)?.GetPath());

                break;

            case Path:
                canvas.DrawPath((shape as Shape)?.GetPath());

                break;

            default:
                throw new NotImplementedException();

        }

    }

    public static void OutlineShape(ICanvas canvas, RectF rect, IShape shape, Color strokeColor, float strokeWidth)
    {
        canvas.SaveState();

        canvas.StrokeColor = strokeColor;
        canvas.StrokeSize = strokeWidth;

        switch (shape)
        {
            case Rectangle:
                canvas.DrawRectangle(rect);

                break;

            case RoundRectangle:
                canvas.DrawRoundedRectangle(
                    rect,
                    (shape as RoundRectangle)?.CornerRadius.TopLeft ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.TopRight ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.BottomLeft ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.BottomRight ?? 0);

                break;

            case Ellipse:
                canvas.DrawEllipse(rect);

                break;

            case Polygon:
                canvas.DrawPath((shape as Shape)?.GetPath());

                break;

            case Path:
                canvas.DrawPath((shape as Shape)?.GetPath());

                break;

            default:
                break;

        }

        canvas.RestoreState();

    }

    public static void FillShape(ICanvas canvas, RectF rect, IShape shape)
    {
        switch (shape)
        {
            case Rectangle:
                canvas.FillRectangle(rect);

                break;

            case RoundRectangle:
                canvas.FillRoundedRectangle(
                    rect,
                    (shape as RoundRectangle)?.CornerRadius.TopLeft ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.TopRight ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.BottomLeft ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.BottomRight ?? 0);

                break;

            case Ellipse:
                canvas.FillEllipse(rect);

                break;

            case Polygon:
                canvas.FillPath((shape as Shape)?.GetPath());

                break;

            case Path:
                canvas.FillPath((shape as Shape)?.GetPath());

                break;

            default:
                throw new NotImplementedException();

        }

    }

    public static void FillShape(ICanvas canvas, RectF rect, IShape shape, Brush fill)
    {
        canvas.SaveState();

        canvas.SetFillPaint(fill, rect);

        switch (shape)
        {
            case Rectangle:
                canvas.FillRectangle(rect);

                break;

            case RoundRectangle:
                canvas.FillRoundedRectangle(
                    rect,
                    (shape as RoundRectangle)?.CornerRadius.TopLeft ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.TopRight ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.BottomLeft ?? 0,
                    (shape as RoundRectangle)?.CornerRadius.BottomRight ?? 0);

                break;

            case Ellipse:
                canvas.FillEllipse(rect);

                break;

            case Polygon:
                canvas.FillPath((shape as Shape)?.GetPath());

                break;

            case Path:
                canvas.FillPath((shape as Shape)?.GetPath());

                break;

            default:
                break;

        }

        canvas.RestoreState();

    }

    /// <summary>
    /// Returns a PathF from the provided shape regardless of whether or not it is visible, has defined dimensions, etc.
    /// </summary>
    /// <param name="shape">The shape from which to create the path.</param>
    /// <param name="rect">Provides the dimensions for the returned path.</param>
    /// <returns>A new PathF object projected (that is, drawn with the bounds/dimensions of) the provided RectF.</returns>
    public static PathF GetProjectedPath(Shape shape, RectF rect)
    {
        PathF path = new();

        switch (shape)
        {
            case Ellipse:
                path.AppendEllipse(0, 0, rect.Width, rect.Height);
                break;

            case RoundRectangle roundRectShape:
                path.AppendRoundedRectangle(rect, 
                                            (float)roundRectShape.CornerRadius.TopLeft,
                                            (float)roundRectShape.CornerRadius.TopRight,
                                            (float)roundRectShape.CornerRadius.BottomRight,
                                            (float)roundRectShape.CornerRadius.BottomLeft);
                break;

            case Rectangle:
                path.AppendRectangle(0, 0, rect.Width, rect.Height);
                break;

            default:
                break;

        }

        return path;
    }

    public static void ConvertImage(ICanvas canvas, Image image)
        => throw new NotImplementedException();

    public static void DrawLine(this ICanvas canvas, LineCap lineCap, PointF startPoint, PointF endPoint)
        => throw new NotImplementedException();

    #endregion

}
