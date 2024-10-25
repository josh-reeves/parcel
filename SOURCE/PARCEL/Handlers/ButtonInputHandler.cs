#if IOS || MACCATALYST
using PlatformElement = PARCEL.Platforms.MaciOS.ButtonInput;

#elif ANDROID
using PlatformElement = PARCEL.Platforms.Android.ButtonInput;

#elif WINDOWS
using PlatformElement = PARCEL.Platforms.Windows.ButtonInput;

#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformElement = System.Object;

#endif

using Microsoft.Maui.Handlers;
using PARCEL.Controls.GestureRecognizers;

namespace PARCEL.Handlers;

public partial class ButtonInputHandler
{
    #region Fields
    public static IPropertyMapper<ButtonInputDetector, ButtonInputHandler> PropertyMapper = new PropertyMapper<ButtonInputDetector, ButtonInputHandler>(ElementMapper);
    public static CommandMapper<ButtonInputDetector, ButtonInputHandler> CommandMapper = new(ElementCommandMapper);

    #endregion

    #region Constructors
    public ButtonInputHandler() : base(PropertyMapper, CommandMapper)
    {

    }

    #endregion

}
