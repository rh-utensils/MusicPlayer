using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using MusicPlayer.Shared.Html.Api.Services;
using Uno.UI.Runtime.WebAssembly;

namespace MusicPlayer.Shared.Html.Api.Components
{
    /// <summary>
    ///     The <see cref="Video" /> element embeds a media player which supports video playback
    ///     into the document. You can use <see cref="Video" /> for audio content as well, but
    ///     the <see cref="Audio" /> element may provide a more appropriate user experience.
    /// </summary>
    [HtmlElement("video")]
    public class Video : MediaElement
    {
        /// <summary>
        ///     Creates a new instance of an <see cref="Video" />.
        /// </summary>
        public Video()
        {
            var events = new[]
            {
                ("enterpictureinpicture", OnEnterPictureInPicture), ("leavepictureinpicture", OnLeavePictureInPicture)
            };

            HtmlEventHandler.RegisterEvents(this, events);
        }

        /// <summary>
        ///     The <see cref="Video" /> method <see cref="GetVideoPlaybackQuality" /> creates and returns a
        ///     <see cref="VideoPlaybackQuality" /> object containing metrics including how many frames have been lost.
        ///     <para />
        ///     The data returned can be used to evaluate the quality of the video stream.
        /// </summary>
        /// <returns>
        ///     A <see cref="VideoPlaybackQuality" /> object providing information about the video element's current playback
        ///     quality.
        /// </returns>
        public VideoPlaybackQuality GetVideoPlaybackQuality()
        {
            return new VideoPlaybackQuality(this);
        }

        /// <summary>
        ///     The <see cref="Video" /> method <see cref="RequestPictureInPicture" /> issues an
        ///     request to display the video in picture-in-picture mode.
        /// </summary>
        public void RequestPictureInPicture()
        {
            this.ExecuteJavascript("element.requestPictureInPicture();");
        }

        /// <summary>
        ///     The <see cref="Video" /> method <see cref="RequestPictureInPicture" /> issues an asynchronous
        ///     request to display the video in picture-in-picture mode.
        ///     <para />
        ///     It's not guaranteed that the video will be put into picture-in-picture. If permission to enter
        ///     that mode is granted, the returned <see cref="bool" /> will be true and the video will receive a
        ///     <see cref="OnEnterPictureInPicture" /> event to let it know that it's now in picture-in-picture.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task" /> that will return to a <see cref="PictureInPictureWindow" /> object. that can be used to
        ///     listen
        ///     when a user will resize that floating window.
        /// </returns>
        public async Task<PictureInPictureWindow> RequestPictureInPictureAsync()
        {
            return await PictureInPictureWindow.RequestPictureInPictureAsync(this);
        }

        #region Properties

        #region AutoPictureInPictureProperty

        public static readonly DependencyProperty AutoPictureInPictureProperty = DependencyProperty.Register(
            "IsAutoPictureIn",
            typeof(bool),
            typeof(Video),
            new PropertyMetadata(default(bool), AutoPictureInPictureChanged));

        /// <summary>
        ///     The <see cref="IsAutoPictureIn" /> property indicate whether the video should
        ///     enter or leave picture-in-picture mode automatically.
        /// </summary>
        /// <value>
        ///     A <see cref="bool" /> whose value is true if the video should enter or leave picture-in-picture mode
        ///     automatically when changing tab and/or application.
        /// </value>
        public bool IsAutoPictureIn
        {
            get => (bool) GetValue(AutoPictureInPictureProperty);
            set => SetValue(AutoPictureInPictureProperty, value);
        }

        private static void AutoPictureInPictureChanged(DependencyObject dependencyobject,
            DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as Video;
            var autoPictureInPicture = ((bool) args.NewValue).ToString().ToLower();
            element?.ExecuteJavascript($"element.autoPictureInPicture = {autoPictureInPicture};");
        }

        #endregion

        #region DisablePictureInPictureProperty

        public static readonly DependencyProperty DisablePictureInPictureProperty = DependencyProperty.Register(
            "IsPictureInDisabled",
            typeof(bool),
            typeof(Video),
            new PropertyMetadata(default(bool), DisablePictureInPictureChanged));

        /// <summary>
        ///     The <see cref="IsPictureInDisabled" /> property indicate whether the user agent should suggest
        ///     the picture-in-picture feature to users, or request it automatically.
        /// </summary>
        /// <value>
        ///     A <see cref="bool" /> whose value is true if the user agent should suggest that feature to users.
        /// </value>
        public bool IsPictureInDisabled
        {
            get => (bool) GetValue(DisablePictureInPictureProperty);
            set => SetValue(DisablePictureInPictureProperty, value);
        }

        private static void DisablePictureInPictureChanged(DependencyObject dependencyobject,
            DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as Video;
            var disablePictureInPicture = ((bool) args.NewValue).ToString().ToLower();
            element?.ExecuteJavascript($"element.disablePictureInPicture = {disablePictureInPicture};");
        }

        #endregion

        #region PosterProperty

        public static readonly DependencyProperty PosterProperty = DependencyProperty.Register(
            "Poster",
            typeof(Size),
            typeof(Uri),
            new PropertyMetadata(default(Uri), PosterChanged));

        /// <summary></summary>
        /// <value>
        ///     A <see cref="Uri" /> for an image to be shown while the video is downloading. If this attribute isn't
        ///     specified, nothing is displayed until the first frame is available, then the first frame is shown
        ///     as the poster frame.
        /// </value>
        public Uri Poster
        {
            get => (Uri) GetValue(PosterProperty);
            set => SetValue(PosterProperty, value);
        }

        private static void PosterChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as Video;
            var poster = (Uri) args.NewValue;
            element?.ExecuteJavascript($"element.poster = '{poster}';");
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        ///     The <see cref="OnEnterPictureInPicture" /> event is fired when the <see cref="Video" /> enters
        ///     picture-in-picture mode successfully.
        /// </summary>
        public event EventHandler OnEnterPictureInPicture;

        /// <summary>
        ///     The <see cref="OnLeavePictureInPicture" /> event is fired when the <see cref="Video" /> leaves
        ///     picture-in-picture mode successfully.
        /// </summary>
        public event EventHandler OnLeavePictureInPicture;

        #endregion
    }
}