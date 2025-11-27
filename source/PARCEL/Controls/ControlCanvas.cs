namespace PARCEL.Controls;

/// <summary>For now, this is largely a direct copy of Microsoft.Maui.Controls.GraphicsView. 
/// The purpose of this class is to provide an alternative container for IDrawable objects
/// that addresses some of the apparent deficiencies of the existing implementation.
/// 
/// The class is implemented directly, as opposed to merely wrapping a GraphicsView with a 
/// ContentView, to improve malleability and allow further deviation from the original
/// implementation as needed over time.</summary>*/
public class ControlCanvas : View, IGraphicsView
{

#region Fields
    /// <summary>Bindable property for <see cref="Drawable"/>.</summary>
    public static readonly BindableProperty DrawableProperty =
        BindableProperty.Create(nameof(Drawable), typeof(IDrawable), typeof(GraphicsView), null);

#endregion

#region Constructor(s)
    public IDrawable Drawable
    {
        set { SetValue(DrawableProperty, value); }
        get { return (IDrawable)GetValue(DrawableProperty); }
    }

#endregion

#region Events
    public event EventHandler<TouchEventArgs>? StartHoverInteraction;
    public event EventHandler<TouchEventArgs>? MoveHoverInteraction;
    public event EventHandler? EndHoverInteraction;
    public event EventHandler<TouchEventArgs>? StartInteraction;
    public event EventHandler<TouchEventArgs>? DragInteraction;
    public event EventHandler<TouchEventArgs>?EndInteraction;
    public event EventHandler? CancelInteraction;

#endregion

#region Methods
    private static void UpdateRenderer()
    {
        
    }
    
    public void Invalidate()
    {
        Handler?.Invoke(nameof(IGraphicsView.Invalidate));

    }

    void IGraphicsView.CancelInteraction() => 
        CancelInteraction?.Invoke(this, EventArgs.Empty);

    void IGraphicsView.DragInteraction(PointF[] points) => 
        DragInteraction?.Invoke(this, new TouchEventArgs(points, true));

    void IGraphicsView.EndHoverInteraction() => 
        EndHoverInteraction?.Invoke(this, EventArgs.Empty);

    void IGraphicsView.EndInteraction(PointF[] points, bool isInsideBounds) => 
        EndInteraction?.Invoke(this, new TouchEventArgs(points, isInsideBounds));

    void IGraphicsView.StartHoverInteraction(PointF[] points) => 
        StartHoverInteraction?.Invoke(this, new TouchEventArgs(points, true));

    void IGraphicsView.MoveHoverInteraction(PointF[] points) => 
        MoveHoverInteraction?.Invoke(this, new TouchEventArgs(points, true));

    void IGraphicsView.StartInteraction(PointF[] points) => 
        StartInteraction?.Invoke(this, new TouchEventArgs(points, true));

#endregion

	public class TouchEventArgs : EventArgs
	{
		public TouchEventArgs()
		{

		}

		public TouchEventArgs(PointF[] points, bool isInsideBounds)
		{
			Touches = points;
			IsInsideBounds = isInsideBounds;
		}

		/// <summary>
		/// This is only used for EndInteraction;
		/// </summary>
		public bool IsInsideBounds { get; private set; }

		public PointF[]? Touches { get; private set; }

	}

}