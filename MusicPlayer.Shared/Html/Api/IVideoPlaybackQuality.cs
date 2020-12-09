namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     The <see cref="IVideoPlaybackQuality" /> interface contains metrics that can be used to determine the playback
    ///     quality of a video.
    /// </summary>
    public interface IVideoPlaybackQuality
    {
        /// <summary>
        ///     The read-only <see cref="CreationTime" /> property on the <see cref="IVideoPlaybackQuality" /> interface reports
        ///     the
        ///     number of milliseconds since the browsing context was created this quality sample was recorded.
        /// </summary>
        /// <value>
        ///     A <see cref="double" /> which indicates the number of milliseconds that elapsed
        ///     between the time the browsing context was created and the time at which this sample of the
        ///     video quality was obtained.
        /// </value>
        double CreationTime { get; }

        /// <summary>
        ///     The read-only <see cref="DroppedVideoFrames" /> property of the <see cref="IVideoPlaybackQuality" /> interface
        ///     returns the
        ///     number of video frames which have been dropped rather than being displayed since the last time the media was loaded
        ///     into
        ///     the <see cref="Components.Video" /> element.
        /// </summary>
        /// <value>
        ///     An unsigned 64-bit value indicating the number of frames that have been dropped since the last
        ///     time the media in the <see cref="Components.Video" /> element was loaded or reloaded. This information can be used
        ///     to determine whether or not to downgrade the video stream to avoid dropping frames.
        ///     <para />
        ///     Frames are typically dropped either before or after decoding them, when it's determined that it
        ///     will not be possible to draw them to the screen at the correct time.
        /// </value>
        ulong DroppedVideoFrames { get; }

        /// <summary>
        ///     The <see cref="IVideoPlaybackQuality" /> interface's <see cref="TotalVideoFrames" /> read-only property returns
        ///     the total number of video frames that have been displayed or dropped since the media was loaded.
        /// </summary>
        /// <value>
        ///     The total number of frames that the <see cref="Components.Video" /> element has displayed or dropped since the
        ///     media was loaded into it. Essentially, this is the number of frames the element would have
        ///     presented had no problems occurred.
        ///     <para />
        ///     This value is reset when the media is reloaded or replaced.
        /// </value>
        ulong TotalVideoFrames { get; }
    }
}