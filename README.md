# PARCEL (Platform Agnostic Rendered Control Library)
<br>

<p align="center"><img align="center" style="width: 50%" src="https://github.com/josh-reeves/parcel/raw/main/misc/iconography/logo_parcel_003_007.svg"/></p>

<br>

PARCEL is a library of (relatively) platform-agnostic controls for .NET MAUI rendered using Microsoft.Maui.Graphics. These controls are meant to extend and, in some cases replace, those provided with MAUI by default in order to provide additional functionality, uniformity and ease of use. Currently, the PARCEL library includes the following controls:
- [IndicatorPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/IndicatorPARCEL.cs) - A touch-responsive indicator control for displaying shapes or images of various sizes. An IndicatorPARCEL can even display a GaugePARCEL.<br>

- [GaugePARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/GaugePARCEL.cs) - A gauge for displaying data in the form of a variety of graphs including both linear and radial variations. Touch input can be enabled via a bindable property, turning any gauge into a slider. An IndicatorPARCEL can be added to any gauge to visually highlight data.

    *Radial GaugePARCEL with a circular IndicatorPARCEL:*
    <p align="center"><img align="center" style="width: 25%" src="https://github.com/josh-reeves/parcel/raw/main/misc/screenshots/gaugeparcel-radial.png"/></p><br>

- [ButtonPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/ButtonPARCEL.cs) - A combo button that takes a stack of views as it's content. Place an image, text and more on a single button. Could you put a button on a button? I probably wouldn't recommend it, but sure: You do you! Each ButtonPARCEL also provides an "Offset" property, which provides an intuitive way to alter the control's appearance, allowing for flat, raised and recessed buttons.

    *Flat ButtonPARCEL:*
    <p align="center"><img align="center" style="width: 25%" src="https://github.com/josh-reeves/parcel/raw/main/misc/screenshots/buttonparcel-flat.png"/></p>
    
    *Raised ButtonPARCEL:*
    <p align="center"><img align="center" style="width: 25%" src="https://github.com/josh-reeves/parcel/raw/main/misc/screenshots/buttonparcel-raised.png"/></p>

    *Recessed ButtonPARCEL:*
    <p align="center"><img align="center" style="width: 25%" src="https://github.com/josh-reeves/parcel/raw/main/misc/screenshots/buttonparcel-recessed.png"/></p><br>
    
    Finally, the "IsPressed" property of a button parcel is publicly settable. This partially decouples a ButtonPARCEL's pressed visual state from its behavior and allows it to be invoked separately, which means that a button's visual state can be overridden via triggers, effectively turning any ButtonPARCEL into a toggle.

### Flexible
Interfaces are included for each control. Additionally, all controls are sub classes of the abstract base class ([ControlPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/ControlPARCEL.cs)), which inherits from ContentView and provides a publicly available IDrawable property in the form of a Bindable Property along with an internally available GraphicsView for easy consumption and a variety of methods for updating the controls' renderers. 

The default control renderers are implemented in the form of nested classes for convenience, but care has been taken to limit their interfaces entirely to their constructors. This all means that PARCELs are flexible: While each control includes a default implementation and renderer, no control is locked to a specific implementation or renderer.

### Reliable
Due to the way MAUI controls work, it's not possible to entirely decouple a control, especially a content view, from it's platform implementation. Steps can be taken, however, to minimize the differences in appearance and behavior. For this reason, PARCELs are rendered using Microsoft.Maui.Graphics. While they do include some conventional views and other controls, these are largely limited to smaller, more predictable elements such as images or layouts, and in some cases these merely serve to simplify the public facing interface, with all rendering still being done separately.

Aside from this, all PARCELs contain the values needed to manage their own state. This helps to minimize the differences between one platform and the next (i.e. A slider built off of GaugePARCEL should look and behave the same on iOS and Android).