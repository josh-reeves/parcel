#if IOS || MACCATALYST
using PlatformElement = PARCEL.Platforms.MaciOS.ButtonInputRecognizerHandler;

#elif ANDROID
using PlatformElement = PARCEL.Platforms.Android.ButtonInputRecognizerHandler;

#elif WINDOWS
using PlatformElement = PARCEL.Platforms.Windows.ButtonInputRecognizerHandler;

#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformElement = System.Object;

#endif

using Microsoft.Maui.Handlers;

namespace PARCEL.Controls.Handlers;

public partial class ButtonInputRecognizerHandler
{
    #region Constructors
    public ButtonInputRecognizerHandler() { }

    #endregion

}
