namespace PARCEL.Interfaces;

public interface IControlPARCELStrategy
{
    #region Properties
    public IDrawable Renderer { get; }
    public IGaugePARCEL? Control { get; set; }

    #endregion

}
