namespace MusicPlayer.Shared.Html.Api.Media
{
    public enum CanPlayResponse
    {
        /// <summary>
        ///     Media of the type indicated by the mediaType parameter is probably playable on this device.
        /// </summary>
        Probably,

        /// <summary>
        ///     Not enough information is available to determine for sure whether or not the media will play
        ///     until playback is actually attempted.
        /// </summary>
        Maybe,

        /// <summary>
        ///     Media of the given type definitely can't be played on the current device.
        /// </summary>
        Not
    }
}