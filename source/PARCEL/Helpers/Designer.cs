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
                throw new NotImplementedException();

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
                throw new NotImplementedException();

        }

    }

    public static void ConvertImage(ICanvas canvas, Image image)
        => throw new NotImplementedException();

    public static void DrawLine(this ICanvas canvas, LineCap lineCap, PointF startPoint, PointF endPoint)
        => throw new NotImplementedException();

    #endregion

}
