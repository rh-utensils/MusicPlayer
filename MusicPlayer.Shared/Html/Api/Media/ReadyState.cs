namespace MusicPlayer.Shared.Html.Api.Media
{
    public enum ReadyState
    {
        /// <summary>
        ///     No information is available about the media resource.
        /// </summary>
        HAVE_NOTHING = 0,

        /// <summary>
        ///     Enough of the media resource has been retrieved that the metadata attributes are initialized.
        ///     Seeking will no longer raise an exception.
        /// </summary>
        HAVE_METADATA = 1,

        /// <summary>
        ///     Data is available for the current playback position, but not enough to actually play more than one frame.
        /// </summary>
        HAVE_CURRENT_DATA = 2,

        /// <summary>
        ///     Data for the current playback position as well as for at least a little bit of time into the future is available
        ///     (in other words, at least two frames of video, for example).
        /// </summary>
        HAVE_FUTURE_DATA = 3,

        /// <summary>
        ///     Enough data is available - and the download rate is high enough -
        ///     that the media can be played through to the end without interruption.
        /// </summary>
        HAVE_ENOUGH_DATA = 4
    }
}