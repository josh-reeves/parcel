using PARCEL.Helpers;

namespace PARCEL.Controls.Behaviors;

public partial class DragDetector
{
    private DragEventArgs dragEventArgs;
    private bool dragStarted;

    #region Constructors
    public DragDetector()
    {
        dragEventArgs = new();

    }

    #endregion

    #region Events
    public event EventHandler<DragEventArgs>? DragStarted;
    public event EventHandler<DragEventArgs>? Drag;
    public event EventHandler<DragEventArgs>? DragEnded;

    #endregion

    #region Properties

    #endregion

    #region Methods
    private void SendDragStarted()
    {
        dragStarted = true;

        DragStarted?.Invoke(this, dragEventArgs);
#if DEBUG
        DebugLogger.Log($"Drag started : {dragEventArgs.Points.Last()}");
#endif
    }

    private void SendDrag()
    {
        Drag?.Invoke(this, dragEventArgs);
#if DEBUG
        DebugLogger.Log($"Drag sent: {dragEventArgs.Points.Last()}, First: {dragEventArgs.Points.First()}");
#endif
    }

    private void SendDragEnded()
    {
        dragStarted = false;

        DragEnded?.Invoke(this, dragEventArgs);
#if DEBUG
        DebugLogger.Log($"Drag ended: {dragEventArgs.Points.Last()}");
#endif
    }

    #endregion

    #region Classes
    public class DragEventArgs
    {
        public DragEventArgs()
        {
            Points = new List<PointF>();

        }

        public IList<PointF> Points { get; set; }

    }
    
    #endregion
}
