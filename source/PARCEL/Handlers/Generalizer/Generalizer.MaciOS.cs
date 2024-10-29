using CoreGraphics;
using UIKit;

namespace PARCEL.Handlers;

internal static partial class Generalizer
{
    #region Methods
    public static partial void UpdateHandlers()
    {
        Microsoft.Maui.Handlers.GraphicsViewHandler.Mapper.AppendToMapping("IncludeLinecapsInWidth", (handler, view) =>
        {
            handler.PlatformView.ClipsToBounds = false;
          
        });

    }

    #endregion
}
