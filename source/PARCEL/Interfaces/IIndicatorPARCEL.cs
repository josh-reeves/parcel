﻿using Microsoft.Maui.Controls.Shapes;

namespace PARCEL.Interfaces;

public interface IIndicatorPARCEL : IControlPARCEL
{
    public Brush IndicatorColor { get; set; }
    public Shape IndicatorShape { get; set; }
    public Image IndicatorIcon { get; set; }

}
