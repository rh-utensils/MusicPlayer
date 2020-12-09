using System;
using Uno.Extensions;

namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     The <see cref="PictureInPictureWindow" /> interface represents an object able to programmatically obtain
    ///     the <see cref="Width" /> and <see cref="Height" /> and <see cref="Resize" /> event of the floating video window.
    ///     <para />
    ///     An object with this interface is obtained using the <see cref="Components.Video.RequestPictureInPictureAsync" />
    ///     task return value.
    /// </summary>
    public interface IPictureInPictureWindow
    {
        /// <summary>
        ///     The read-only <see cref="PictureInPictureWindow" /> property <see cref="Width" /> returns the width of the floating
        ///     video window in pixels.
        /// </summary>
        /// <value>
        ///     An <see cref="int" /> value indicating the width of the floating video window in pixels. This property
        ///     is read-only, and has no default value.
        /// </value>
        int Width { get; }

        /// <summary>
        ///     The read-only <see cref="PictureInPictureWindow" /> property <see cref="Height" /> returns the height of the
        ///     floating
        ///     video window in pixels.
        /// </summary>
        /// <value>
        ///     An <see cref="int" /> value indicating the height of the floating video window in pixels. This property
        ///     is read-only, and has no default value.
        /// </value>
        int Height { get; }

        /// <summary>
        ///     The <see cref="OnResize" /> event fires when the floating video window has been resized.
        /// </summary>
        event EventHandler<HtmlCustomEventArgs> OnResize;
    }
}