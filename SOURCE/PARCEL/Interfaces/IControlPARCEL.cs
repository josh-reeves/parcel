namespace PARCEL.Interfaces;

public interface IControlPARCEL : IContentView
{
    public GraphicsView? ControlCanvas { get; set; }

    // The following prevent the need to create multiple base classes and inheritance in favor of interfaces and composition:
    public new bool InputTransparent { get; set; }

    public new double TranslationX { get; set; }

    public new double TranslationY { get; set; }

    public new Microsoft.Maui.Controls.View Content { get; set; }

}


