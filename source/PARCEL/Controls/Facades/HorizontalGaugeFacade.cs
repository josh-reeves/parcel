using PARCEL.Interfaces;

namespace PARCEL.Controls.Facades;

public class HorizontalGaugeFacade : GaugePARCELFacade
{
    #region Fields
    private IDrawable? renderer;

    #endregion

    #region Constructors
    public HorizontalGaugeFacade() { }

    public HorizontalGaugeFacade(IGaugePARCEL parentControl) : base(parentControl) { }

    #endregion

    public override IDrawable Renderer
    {
        get => renderer ??= new HorizontalRenderer(this);

    }

    public override void HandleInput(TouchEventArgs e)
        => throw new NotImplementedException();

    public class HorizontalRenderer : GaugeFacadeRenderer
    {
        public HorizontalRenderer(IGaugePARCELStrategy parentFacade) : base(parentFacade) { }

        public override void Draw(ICanvas canvas, RectF rect)
        {

            if (Parent is not GaugePARCELFacade parent || parent.Control is null)
                return;

            float offset = 2,
                  valuePos;

            parent.WorkingCanvas = RectF.FromLTRB(
                rect.Left + offset + (parent.Control.Thickness / 2),
                rect.Top + offset,
                rect.Right - offset - (parent.Control.Thickness / 2),
                rect.Bottom - offset);

            valuePos = parent.WorkingCanvas.Left + (float)((parent.Control.Value - parent.Control.ValueMin) / (parent.Control.ValueMax - parent.Control.ValueMin)) * (parent.WorkingCanvas.Width);
            
            parent.IndicatorBounds = new()
            {
                X = valuePos - (float)((parent.Control.Indicator?.Width ?? parent.Control.Thickness) / 2),
                Y = parent.WorkingCanvas.Center.Y - (float)((parent.Control.Indicator?.Height ?? parent.Control.Thickness) / 2),
                Width = (float)(parent.Control.Indicator?.Width ?? parent.Control.Thickness),
                Height = (float)(parent.Control.Indicator?.Height ?? parent.Control.Thickness)

            };

            canvas.StrokeLineCap = parent.Control.LineCap;
            canvas.StrokeSize = parent.Control.Thickness + parent.Control.StrokeThickness;
            canvas.StrokeColor = parent.Control.StrokeColor;

            canvas.DrawLine(parent.WorkingCanvas.Left,
                parent.WorkingCanvas.Center.Y,
                parent.WorkingCanvas.Right,
                parent.WorkingCanvas.Center.Y);

            canvas.StrokeSize = parent.Control.Thickness;
            canvas.StrokeColor = parent.Control.EmptyColor;

            canvas.DrawLine(parent.WorkingCanvas.Left,
                parent.WorkingCanvas.Center.Y,
                parent.WorkingCanvas.Right,
                parent.WorkingCanvas.Center.Y);

            canvas.StrokeColor = parent.Control.FillColor;

            canvas.DrawLine(parent.WorkingCanvas.Left,
                parent.WorkingCanvas.Center.Y,
                valuePos,
                parent.WorkingCanvas.Center.Y);

            if (parent.Control.Indicator is not null)
                DrawIndicator(rect);

        }

    }

}
