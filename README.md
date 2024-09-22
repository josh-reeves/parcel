## PARCEL
<br>

<p align="center"><img style="width: 25%" src="https://github.com/josh-reeves/parcel/raw/main/misc/iconography/logo_parcel_002.svg"/></p>

<br>

PARCEL is a libary of (relatively) platform-agnostic controls for .NET MAUI rendered using the framework's graphics system. These controls are meant to extend and, in some cases replace, those provided with MAUI by default in order to provide additional functionality and ease of use. 

Unlike the standard views provided with MAUI by default, which map to underlying controls provided by the OS, PARCELs contain both the code required to render the control and the members required to track its state and impelement its behavior. As a result, the difference's between each control's appearance and behavior from platform to platform should be minimized.

## About PARCELs

PARCELs are a series of controls and interfaces based on IContentView that provide, at the very least, an IDrawable property. These include, but are not limited to, the following:
- A touch-responsive indicator control for displaying shapes or images of various sizes [IndicatorPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/IndicatorPARCEL.cs) and its associated interface: [IIndicatorPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Interfaces/IIndicatorPARCEL.cs).
- A gauge control for displaying data in the form of a variety of graphics including linear and radial [GaugePARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/GaugePARCEL.cs) and its associated interface: [IGaugePARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Interfaces/IGaugePARCEL.cs).
    - This can be combined with the indicator control to enable slider functionality. 

<br>

In addition to the above, the library also provides an abstract base class ([ControlPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/ControlPARCEL.cs)) which inherits from ContentView and provides the requisite IDrawable property in the form of a Bindable Property along with a GraphicsView for easy consumption and a variety of methods for updating the controls' renderers.

The implementations for each of the controls provide a default renderer in the form of a nested class which implements IDrawable. The interfaces of these classes are limited to their constructors, which makes them easy to substitute with custom implementations via the renderer property.
