namespace PARCEL.Interfaces;

public interface IControlFacade
{
    #region Properties
    public IDrawable Renderer { get; }

    public IGaugePARCEL? Control { get; set; }

    #endregion

}
