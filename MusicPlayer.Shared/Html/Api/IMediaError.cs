using MusicPlayer.Shared.Html.Api.Media;

namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     The <see cref="IMediaError" /> interface represents an error which occurred while handling media in an
    ///     <see cref="IMediaElement" /> based element, such as <see cref="Components.Audio" /> or
    ///     <see cref="Components.Video" />.
    /// </summary>
    public interface IMediaError
    {
        /// <value>A number which represents the general type of error that occurred.</value>
        ErrorCode Code { get; }

        /// <value>
        ///     A <see cref="string" /> containing a human-readable string which provides specific diagnostic
        ///     information to help the reader understand the error condition which occurred; specifically, it
        ///     isn't simply a summary of what the error code means, but actual diagnostic information to
        ///     help in understanding what exactly went wrong. This text and its format is not defined by the
        ///     specification and will vary from one user agent to another. If no diagnostics are available, or
        ///     no explanation can be provided, this value is an empty string ("").
        /// </value>
        string Message { get; }
    }
}