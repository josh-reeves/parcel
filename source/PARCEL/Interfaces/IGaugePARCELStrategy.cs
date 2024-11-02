using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARCEL.Interfaces;

public interface IGaugePARCELStrategy : IControlPARCELStrategy
{
    #region Properties
    public RectF IndicatorBounds { get; set; }

    #endregion

    #region Methods
    public void HandleInput(TouchEventArgs e);

    #endregion
}
