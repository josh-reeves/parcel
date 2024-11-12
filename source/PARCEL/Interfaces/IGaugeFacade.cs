using PARCEL.Controls.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARCEL.Interfaces;

public interface IGaugeFacade : IControlFacade
{
    #region Properties
    public RectF WorkingCanvas { get; }
    public RectF IndicatorBounds { get; set; }

    #endregion

    #region Methods
    public void HandleInput(DragDetector.DragEventArgs e);

    #endregion
}
