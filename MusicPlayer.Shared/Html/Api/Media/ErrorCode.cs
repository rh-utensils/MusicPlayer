namespace MusicPlayer.Shared.Html.Api.Media
{
    public enum ErrorCode
    {
        /// <summary>
        ///     The fetching of the associated resource was aborted by the user's request.
        /// </summary>
        MEDIA_ERR_ABORTED = 1,

        /// <summary>
        ///     Some kind of network error occurred which prevented the media from being successfully fetched,
        ///     despite having previously been available.
        /// </summary>
        MEDIA_ERR_NETWORK = 2,

        /// <summary>
        ///     Despite having previously been determined to be usable,
        ///     an error occurred while trying to decode the media resource,
        ///     resulting in an error.
        /// </summary>
        MEDIA_ERR_DECODE = 3,

        /// <summary>
        ///     The associated resource or media provider object has been found to be unsuitable.
        /// </summary>
        MEDIA_ERR_SRC_NOT_SUPPORTED = 4
    }
}