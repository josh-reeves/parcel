namespace PARCEL.Interfaces;



public interface IControlPARCEL : IView
{
    public IDrawable Renderer { get; set; }

    /* The following prevent the need to create multiple base classes and inheritance in favor of interfaces and composition.
     *  the first three are kind of gross, but they tend to be given write access by their implementations anyway.
     *  The last one is, basically, fine.
     */
    public new bool InputTransparent { get; set; }

    public new double TranslationX { get; set; }

    public new double TranslationY { get; set; }

    public new View Content { get; set; }

}


