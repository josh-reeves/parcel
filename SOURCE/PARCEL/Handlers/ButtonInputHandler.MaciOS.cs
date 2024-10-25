using Microsoft.Maui.Handlers;
using PARCEL.Controls.GestureRecognizers;
using PARCEL.Platforms.MaciOS;

namespace PARCEL.Handlers;

public partial class ButtonInputHandler : ElementHandler<ButtonInputDetector, ButtonInput>
{
    #region Methods
    protected override ButtonInput CreatePlatformElement()
        => new ButtonInput();

    protected override void ConnectHandler(ButtonInput platformElement)
        => base.ConnectHandler(platformElement);

    protected override void DisconnectHandler(ButtonInput platformElement)
    {
        base.DisconnectHandler(platformElement);

    }

    #endregion

}
