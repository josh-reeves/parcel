# PARCEL
<br>

<p align="center"><img style="width: 25%" src="https://github.com/josh-reeves/parcel/raw/main/misc/iconography/logo_parcel_002.svg"/></p>

<br>

PARCEL is a libary of (relatively) platform-agnostic controls for .NET MAUI rendered using Microsoft.Maui.Graphics. These controls are meant to extend and, in some cases replace, those provided with MAUI by default in order to provide additional functionality, uniformity and ease of use. Currently, the PARCEL libray includes the following controls:
- [IndicatorPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/IndicatorPARCEL.cs) - A touch-responsive indicator control for displaying shapes or images of various sizes.

- [GaugePARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/GaugePARCEL.cs) - A gauge control for displaying data in the form of a variety of graphs including both linear and radial variations. Adding an IndicatorPARCEL to the gauge via the provided property enables user input, turning the graph into a slider.

- [ButtonPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/ButtonPARCEL.cs) - A combo button with a variety of display styles that takes a stack of views as it's content. Place an image, text and more on a single button. Could you put a button on a button? I probably wouldn't reccommend it, but sure. You do you!

<br>

## Flexible
Interfaces are included for each control. Additionally, all controls are sub classes of the abstract base class ([ControlPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/ControlPARCEL.cs)), which inherits from ContentView and provides a publicly available IDrawable property in the form of a Bindable Property along with an internally available GraphicsView for easy consumption and a variety of methods for updating the controls' renderers. 

The default control renderers are implemented in the form of nested classes for convenience, but care has been taken to limit their interfaces entirely to their constructors. This all means that PARCELs are flexible: While each control includes a default implementation and renderer, no control is locked to a specific implementation or renderer.

## Reliable
Due to the way MAUI controls work, it's not possible to entirely decouple a control, especially a content view, from it's platform implementation. Steps can be taken, however, to minimize the differences in appearance and behavior. For this reason, PARCELs are rendered using Microsoft.Maui.Graphics. While they do include some conventional views and other controls, these are largely limited to smaller, more predictable elements such as images or layouts, and in some cases these merely serve to simplify the public facing interface, with all rendering still being done separately.

Aside from this, all PARCELs contain the values needed to manage their own state. This helps to minimize the differences between one platform and the next (i.e. A slider built off of GaugePARCEL should look and behave the same on iOS and Android).