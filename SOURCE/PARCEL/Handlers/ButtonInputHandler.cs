using Microsoft.Maui.Handlers;
using PARCEL.Controls.GestureRecognizers;
using PARCEL.Helpers;

#if IOS || MACCATALYST
using PlatformView = PARCEL.Platforms.MaciOS.ButtonInput;

#elif ANDROID
using PlatformView = PARCEL.Platforms.Android.ButtonInput;

#elif WINDOWS
using PlatformView = PARCEL.Platforms.Windows.ButtonInput;

#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;

#endif

namespace PARCEL.Handlers;

public partial class ButtonInputHandler
{
    #region Fields
    public static IPropertyMapper<ButtonInputDetector, ButtonInputHandler> PropertyMapper = new PropertyMapper<ButtonInputDetector, ButtonInputHandler>(ViewMapper);
    public static CommandMapper<ButtonInputDetector, ButtonInputHandler> CommandMapper = new(ElementCommandMapper);

    #endregion

    #region Constructors
    public ButtonInputHandler() : base(PropertyMapper, CommandMapper)
    {
        DebugLogger.Log("Test");

    }

    #endregion

}
