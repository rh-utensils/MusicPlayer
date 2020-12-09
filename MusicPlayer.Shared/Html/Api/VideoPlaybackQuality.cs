using Windows.UI.Xaml;
using MusicPlayer.Shared.Html.Api.Components;

namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     A <see cref="VideoPlaybackQuality" /> object is returned by the
    ///     <see cref="Components.Video.GetVideoPlaybackQuality" />
    ///     method and contains metrics that can be used to determine the playback quality of a video.
    /// </summary>
    public class VideoPlaybackQuality : IVideoPlaybackQuality
    {
        private readonly Video _videoElement;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VideoPlaybackQuality" /> class.
        /// </summary>
        /// <param name="videoElement">
        ///     A <see cref="Video" /> element to get the <see cref="IVideoPlaybackQuality" /> properties
        ///     from.
        /// </param>
        public VideoPlaybackQuality(Video videoElement)
        {
            _videoElement = videoElement;
        }

        /// <inheritdoc />
        public double CreationTime =>
            double.Parse(_videoElement.ExecuteJavascript("element.getVideoPlaybackQuality().creationTime;"));

        /// <inheritdoc />
        public ulong DroppedVideoFrames =>
            ulong.Parse(_videoElement.ExecuteJavascript("element.getVideoPlaybackQuality().droppedVideoFrames;"));

        /// <inheritdoc />
        public ulong TotalVideoFrames =>
            ulong.Parse(_videoElement.ExecuteJavascript("element.getVideoPlaybackQuality().totalVideoFrames;"));
    }
}