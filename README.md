## PARCEL

<img style="display: block; 
            margin-left: auto;
            margin-right: auto;
            width: 30%;"
     src="https://github.com/josh-reeves/parcel/blob/main/PARCEL_LOGO.svg"/>

PARCEL is a libary of (relatively) platform-agnostic controls for .NET MAUI rendered using the framework's graphics system. Thexe controls are meant to extend and, in some cases replace, those provided with MAUI by default in order to provide additional functionality and ease of use. 

Unlike the standard views provided with MAUI by default, which map to underlying controls provided by the OS, PARCELs contain both the code required to render the control and the members required to track its state and impelement its behavior. As a result, the difference's between each control's appearance and behavior from platform to platform should be minimized.

## About PARCELs

PARCELs are a series of interfaces based on IContentView that provide, at the very least, a GraphicsView. These include, but are not limited to, the following:
- A touch-responsive indicator control for displaying shapes or images of various sizes [IIndicatorPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Interfaces/IIndicatorPARCEL.cs)
- A gauge control for displaying data in the form of a variety of graphics including linear and radial [IGaugePARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Interfaces/IGaugePARCEL.cs).
    - This can be combined with the indicator control to enable slider functionality. 

<br>

In addition to the interfaces mentioned above, the library also provides a series of default implementations built off of a [ControlPARCEL](https://github.com/josh-reeves/parcel/blob/main/SOURCE/PARCEL/Controls/ControlPARCEL.cs) class, which inherits from ContentView and provides a GraphicsView for simplicity. This class also provides a variety of methods useful for updating and interacting with the controls' renderers.

The implementation classes for each of the individual controls provide the rendering logic via a private nested class that implements IDrawable.

