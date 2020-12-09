using System;
using MusicPlayer.Shared.Html.Api.Media;

namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     A <see cref="MediaError" /> object describes the error in general terms using a numeric code categorizing
    ///     the kind of error, and a <see cref="Message" />, which provides specific diagnostics about what went wrong.
    /// </summary>
    public class MediaError : IMediaError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MediaError" /> class.
        /// </summary>
        /// <param name="message">A <see cref="string" /> containing the error message.</param>
        /// <param name="error">A <see cref="Media.ErrorCode" /> representing the error type</param>
        public MediaError(string message, ErrorCode error)
        {
            Code = error;
            Message = message;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediaError" /> class.
        /// </summary>
        /// <param name="message">A <see cref="string" /> containing the error message.</param>
        /// <param name="code">A <see cref="int" /> representing the error type</param>
        public MediaError(string message, int code)
        {
            if (Enum.IsDefined(typeof(ErrorCode), code)) Code = (ErrorCode) code;
            Message = message;
        }

        /// <inheritdoc />
        public ErrorCode Code { get; }

        /// <inheritdoc />
        public string Message { get; }
    }
}