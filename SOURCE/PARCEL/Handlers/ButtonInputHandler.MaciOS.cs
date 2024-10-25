using Microsoft.Maui.Handlers;
using PARCEL.Controls.GestureRecognizers;
using PARCEL.Helpers;
using PARCEL.Platforms.MaciOS;

namespace PARCEL.Handlers;

public partial class ButtonInputHandler : ViewHandler<ButtonInputDetector, ButtonInput>
{
    #region Methods
    protected override ButtonInput CreatePlatformView()
    {
        DebugLogger.Log("Test");

        return new ButtonInput();

    }

    protected override void ConnectHandler(ButtonInput platformElement)
        => base.ConnectHandler(platformElement);

    protected override void DisconnectHandler(ButtonInput platformElement)
    {
        platformElement.Dispose();
        base.DisconnectHandler(platformElement);

    }

    #endregion

}
