namespace MusicPlayer.Shared.Html.Api.Media
{
    public enum PreloadState
    {
        /// <summary>
        ///     Indicates that the audio should not be preloaded.
        /// </summary>
        None,

        /// <summary>
        ///     Indicates that only audio metadata (e.g. length) is fetched.
        /// </summary>
        MetaData,

        /// <summary>
        ///     Indicates that the whole audio file can be downloaded,
        ///     even if the user is not expected to use it.
        /// </summary>
        Auto
    }
}