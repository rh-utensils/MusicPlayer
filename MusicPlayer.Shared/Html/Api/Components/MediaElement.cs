using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using MusicPlayer.Shared.Html.Api.Media;
using MusicPlayer.Shared.Html.Api.Services;

namespace MusicPlayer.Shared.Html.Api.Components
{
    [ContentProperty(Name = "Source")]
    public abstract class MediaElement : Control, IMediaElement
    {
        /// <summary>
        ///     Creates a new instance of an <see cref="MediaElement" />.
        /// </summary>
        protected MediaElement()
        {
            this.SetHtmlAttribute("preload", "none");
            this.SetHtmlAttribute("crossOrigin", "anonymous");

            var events = new[]
            {
                ("abort", OnAbort), ("canplay", OnCanPlay), ("canplaythrough", OnCanPlayThrough),
                ("complete", OnComplete),
                ("durationchange", OnDurationChanged), ("emptied", OnEmptied), ("ended", OnEnded), ("error", OnError),
                ("loadeddata", OnLoadedData), ("loadedmetadata", OnLoadedMetaData), ("loadstart", OnStartLoading),
                ("pause", OnPause), ("play", OnPlay), ("progress", OnProgress), ("ratechange", OnRateChanged),
                ("seeked", OnSeeked), ("seeking", OnSeeking), ("stalled", OnStalled), ("suspend", OnSuspend),
                ("timeupdate", OnPositionChanged), ("volumechange", OnVolumeChanged), ("waiting", OnWaiting)
            };

            HtmlEventHandler.RegisterEvents(this, events);

            this.ExecuteJavascript("element.context = new (window.AudioContext || window.webkitAudioContext);");
            this.ExecuteJavascript("element.context.source = element.context.createMediaElementSource(element);");
            this.ExecuteJavascript("element.context.analyser = element.context.createAnalyser();");

            this.ExecuteJavascript("element.context.source.connect(element.context.analyser);");
            this.ExecuteJavascript("element.context.analyser.connect(element.context.destination);");

            this.ExecuteJavascript("element.context.analyser.fftSize = 4096;");

            this.ExecuteJavascript(
                "document.addEventListener('click', () => element.context.resume(), { once: true });");
        }

        /// <inheritdoc />
        public void Load()
        {
            this.ExecuteJavascript("element.load();");
        }

        /// <inheritdoc />
        public void Play()
        {
            this.ExecuteJavascript("element.play();");
        }

        /// <summary>
        ///     The <see cref="PlayAsync" /> method attempts to begin playback of the media. It returns a
        ///     <see cref="Task" /> which returns true when playback has been successfully started. Failure to begin
        ///     playback for any reason, such as permission issues, result being false.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task" /> which returns true when playback has been started, or returns false if for any reason
        ///     playback cannot be started.
        /// </returns>
        public async Task<bool> PlayAsync()
        {
            var isFulfilled = await this.ExecuteJavascriptAsync(@"
                (async () => {
                    var promise = await element.play();
                    return promise.isFulfilled();
                })();");

            return bool.Parse(isFulfilled);
        }

        /// <inheritdoc />
        public void Pause()
        {
            this.ExecuteJavascript("element.pause();");
        }

        /// <inheritdoc />
        public CanPlayResponse CanPlayType(string mediaType)
        {
            return CanPlayResponse.Not;
        }

        #region Properties

        // TODO: Only allow play existing/valid files and valid media links

        #region SourceProperty

        // TODO: Create IFile interface
        /* public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source",
            typeof(IFile),
            typeof(MediaElement),
            new PropertyMetadata(default(IFile), SourceChanged));

        /// <inheritdoc />
        public IFile Source
        {
            get => (IFile) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        private static void SourceChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as MediaElement;
            // TODO: Set file as Source
            //element?.SetHtmlAttribute("src", (IFile) args.NewValue);
        } */

        #endregion

        #region VolumeProperty

        public static readonly DependencyProperty VolumeProperty = DependencyProperty.Register(
            "Volume",
            typeof(double),
            typeof(MediaElement),
            new PropertyMetadata(1.0, VolumeChanged));

        /// <inheritdoc />
        public double Volume
        {
            get => (double) GetValue(VolumeProperty);
            set
            {
                if (value > 1.0) value = 1.0;
                if (value < 0.0) value = 0.0;

                SetValue(VolumeProperty, value);
            }
        }

        private static void VolumeChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as MediaElement;
            var volume = ((double) args.NewValue).ToString(CultureInfo.InvariantCulture);
            element?.ExecuteJavascript($"element.volume = {volume};");
        }

        #endregion

        #region MuteProperty

        public static readonly DependencyProperty MuteProperty = DependencyProperty.Register(
            "IsMuted",
            typeof(bool), typeof(MediaElement),
            new PropertyMetadata(default(bool), MuteChanged));

        /// <inheritdoc />
        public bool IsMuted
        {
            get => (bool) GetValue(MuteProperty);
            set => SetValue(MuteProperty, value);
        }

        private static void MuteChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as MediaElement;
            var muted = ((bool) args.NewValue).ToString().ToLower();
            element?.ExecuteJavascript($"element.muted = {muted};");
        }

        #endregion

        #region DefaultMuteProperty

        public static readonly DependencyProperty DefaultMuteProperty = DependencyProperty.Register(
            "IsDefaultMuted",
            typeof(bool), typeof(MediaElement),
            new PropertyMetadata(default(bool), DefaultMuteChanged));

        /// <inheritdoc />
        public bool IsDefaultMuted
        {
            get => (bool) GetValue(DefaultMuteProperty);
            set => SetValue(DefaultMuteProperty, value);
        }

        private static void DefaultMuteChanged(DependencyObject dependencyobject,
            DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as MediaElement;
            var muted = ((bool) args.NewValue).ToString().ToLower();
            element?.ExecuteJavascript($"element.defaultMuted = {muted};");
        }

        #endregion

        #region LoopProperty

        public static readonly DependencyProperty LoopProperty = DependencyProperty.Register(
            "IsLooped",
            typeof(bool), typeof(MediaElement),
            new PropertyMetadata(default(bool), LoopChanged));

        /// <inheritdoc />
        public bool IsLooped
        {
            get => (bool) GetValue(LoopProperty);
            set => SetValue(LoopProperty, value);
        }

        private static void LoopChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as MediaElement;
            var loop = ((bool) args.NewValue).ToString().ToLower();
            element?.ExecuteJavascript($"element.loop = {loop};");
        }

        #endregion

        #region AutoPlayProperty

        public static readonly DependencyProperty AutoPlayProperty = DependencyProperty.Register(
            "IsAutoPlayed",
            typeof(bool), typeof(MediaElement),
            new PropertyMetadata(default(bool), AutoPlayChanged));

        /// <inheritdoc />
        public bool IsAutoPlayed
        {
            get => (bool) GetValue(AutoPlayProperty);
            set => SetValue(AutoPlayProperty, value);
        }

        private static void AutoPlayChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as MediaElement;
            var autoplay = ((bool) args.NewValue).ToString().ToLower();
            element?.ExecuteJavascript($"element.autoplay = {autoplay};");
        }

        #endregion

        #region DisableRemotePlaybackProperty

        public static readonly DependencyProperty DisableRemotePlaybackProperty = DependencyProperty.Register(
            "IsRemotePlaybackDisabled",
            typeof(bool), typeof(MediaElement),
            new PropertyMetadata(default(bool), DisableRemotePlaybackChanged));

        /// <inheritdoc />
        public bool IsRemotePlaybackDisabled
        {
            get => (bool) GetValue(DisableRemotePlaybackProperty);
            set => SetValue(DisableRemotePlaybackProperty, value);
        }

        private static void DisableRemotePlaybackChanged(DependencyObject dependencyobject,
            DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as MediaElement;
            var disableRemotePlayback = ((bool) args.NewValue).ToString().ToLower();
            element?.ExecuteJavascript($"element.disableRemotePlayback = {disableRemotePlayback};");
        }

        #endregion

        #region PreloadProperty

        public static readonly DependencyProperty PreloadProperty = DependencyProperty.Register(
            "Preload",
            typeof(PreloadState), typeof(MediaElement),
            new PropertyMetadata(default(PreloadState), PreloadChanged));

        /// <inheritdoc />
        public PreloadState Preload
        {
            get => (PreloadState) GetValue(PreloadProperty);
            set => SetValue(PreloadProperty, value);
        }

        private static void PreloadChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as MediaElement;
            var preload = ((PreloadState) args.NewValue).ToString().ToLower();
            element?.SetHtmlAttribute("preload", preload);
        }

        #endregion

        #region PlaybackRateProperty

        public static readonly DependencyProperty PlaybackRateProperty = DependencyProperty.Register(
            "PlaybackRate",
            typeof(double),
            typeof(MediaElement),
            new PropertyMetadata(1.0, PlaybackRateChanged));

        /// <inheritdoc />
        public double PlaybackRate
        {
            get => (double) GetValue(PlaybackRateProperty);
            set
            {
                if (value > 5.0) value = 0.5;
                if (value < 0.25) value = 0.25;

                SetValue(PlaybackRateProperty, value);
            }
        }

        private static void PlaybackRateChanged(DependencyObject dependencyobject,
            DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as MediaElement;
            var playbackRate = ((double) args.NewValue).ToString(CultureInfo.InvariantCulture);
            element?.ExecuteJavascript($"element.playbackRate = {playbackRate};");
        }

        #endregion

        #region DefaultPlaybackRateProperty

        public static readonly DependencyProperty DefaultPlaybackRateProperty = DependencyProperty.Register(
            "DefaultPlaybackRate",
            typeof(double),
            typeof(MediaElement),
            new PropertyMetadata(1.0, DefaultPlaybackRateChanged));

        /// <inheritdoc />
        public double DefaultPlaybackRate
        {
            get => (double) GetValue(DefaultPlaybackRateProperty);
            set
            {
                if (value > 5.0) value = 0.5;
                if (value < 0.25) value = 0.25;

                SetValue(DefaultPlaybackRateProperty, value);
            }
        }

        private static void DefaultPlaybackRateChanged(DependencyObject dependencyobject,
            DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as MediaElement;
            var playbackRate = ((double) args.NewValue).ToString(CultureInfo.InvariantCulture);
            element?.ExecuteJavascript($"element.defaultPlaybackRate = {playbackRate};");
        }

        #endregion

        /// <inheritdoc />
        public ITimeRanges Buffered
        {
            get
            {
                var bufferedList = new List<(double start, double end)>();
                var bufferedLength = int.Parse(this.ExecuteJavascript("element.buffered.length"));

                for (var timeRange = 0; timeRange < bufferedLength; timeRange++)
                {
                    var start = this.ExecuteJavascript($"element.buffered.start({timeRange});");
                    var end = this.ExecuteJavascript($"element.buffered.end({timeRange});");

                    bufferedList.Add((double.Parse(start, CultureInfo.InvariantCulture),
                        double.Parse(end, CultureInfo.InvariantCulture)));
                }

                return new TimeRanges(bufferedList);
            }
        }

        /// <inheritdoc />
        public ITimeRanges Seekable
        {
            get
            {
                var seekableList = new List<(double start, double end)>();
                var seekableLength = int.Parse(this.ExecuteJavascript("element.seekable.length"));

                for (var timeRange = 0; timeRange < seekableLength; timeRange++)
                {
                    var start = this.ExecuteJavascript($"element.seekable.start({timeRange});");
                    var end = this.ExecuteJavascript($"element.seekable.end({timeRange});");

                    seekableList.Add((double.Parse(start, CultureInfo.InvariantCulture),
                        double.Parse(end, CultureInfo.InvariantCulture)));
                }

                return new TimeRanges(seekableList);
            }
        }

        /// <inheritdoc />
        public double Duration
        {
            get
            {
                var duration = this.ExecuteJavascript("element.duration;");
                return double.Parse(duration, CultureInfo.InvariantCulture);
            }
        }

        /// <inheritdoc />
        public double CurrentPosition
        {
            get
            {
                var currentTime = this.ExecuteJavascript("element.currentTime;");
                return double.Parse(currentTime, CultureInfo.InvariantCulture);
            }
            set
            {
                var currentTime = value.ToString(CultureInfo.InvariantCulture);
                this.ExecuteJavascript($"element.currentTime = {currentTime};");
            }
        }

        /// <inheritdoc />
        public bool IsEnded
        {
            get
            {
                var isEnded = this.ExecuteJavascript("element.ended;");
                return bool.Parse(isEnded);
            }
        }

        /// <inheritdoc />
        public bool IsPaused
        {
            get
            {
                var isPaused = this.ExecuteJavascript("element.paused;");
                return bool.Parse(isPaused);
            }
        }

        /// <inheritdoc />
        public bool IsSeeking
        {
            get
            {
                var isSeeking = this.ExecuteJavascript("element.seeking;");
                return bool.Parse(isSeeking);
            }
        }

        /// <inheritdoc />
        public IMediaError Error
        {
            get
            {
                var error = this.ExecuteJavascript("element.error;");
                if (error.Equals("null")) return null;

                var message = this.ExecuteJavascript("element.error.message;");
                var code = this.ExecuteJavascript("element.error.code;");

                return new MediaError(message, int.Parse(code));
            }
        }

        /// <inheritdoc />
        public NetworkState NetworkState
        {
            get
            {
                var networkState = this.ExecuteJavascript("element.networkState;");
                return (NetworkState) int.Parse(networkState);
            }
        }

        /// <inheritdoc />
        public ReadyState ReadyState
        {
            get
            {
                var readyState = this.ExecuteJavascript("element.readyState;");
                return (ReadyState) int.Parse(readyState);
            }
        }

        #endregion

        #region Events

        /// <inheritdoc />
        public event EventHandler OnAbort;

        /// <inheritdoc />
        public event EventHandler OnCanPlay;

        /// <inheritdoc />
        public event EventHandler OnCanPlayThrough;

        /// <inheritdoc />
        public event EventHandler OnComplete;

        /// <inheritdoc />
        public event EventHandler OnDurationChanged;

        /// <inheritdoc />
        public event EventHandler OnEmptied;

        /// <inheritdoc />
        public event EventHandler OnEnded;

        /// <inheritdoc />
        public event EventHandler OnError;

        /// <inheritdoc />
        public event EventHandler OnLoadedData;

        /// <inheritdoc />
        public event EventHandler OnLoadedMetaData;

        /// <inheritdoc />
        public event EventHandler OnStartLoading;

        /// <inheritdoc />
        public event EventHandler OnPause;

        /// <inheritdoc />
        public event EventHandler OnPlay;

        /// <inheritdoc />
        public event EventHandler OnProgress;

        /// <inheritdoc />
        public event EventHandler OnRateChanged;

        /// <inheritdoc />
        public event EventHandler OnSeeked;

        /// <inheritdoc />
        public event EventHandler OnSeeking;

        /// <inheritdoc />
        public event EventHandler OnStalled;

        /// <inheritdoc />
        public event EventHandler OnSuspend;

        /// <inheritdoc />
        public event EventHandler OnPositionChanged;

        /// <inheritdoc />
        public event EventHandler OnVolumeChanged;

        /// <inheritdoc />
        public event EventHandler OnWaiting;

        #endregion
    }
}