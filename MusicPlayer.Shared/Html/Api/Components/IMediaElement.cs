using System;
using System.Threading.Tasks;
using MusicPlayer.Shared.Html.Api.Media;

namespace MusicPlayer.Shared.Html.Api.Components
{
    /// <summary>
    ///     The <see cref="IMediaElement" /> interface adds the properties and methods needed
    ///     to support basic media-related capabilities that are common to audio and video. The
    ///     <see cref="Audio" /> and <see cref="Video" /> both inherit this interface.
    /// </summary>
    public interface IMediaElement
    {
        // TODO: Customize Media Notifications and Handle Playlists
        // https://developers.google.com/web/updates/2017/02/media-session

        /// <summary>
        ///     The <see cref="IMediaElement" /> method <see cref="Load" /> resets the media element to its initial state and
        ///     begins the process of selecting a media source and loading the media in preparation for
        ///     playback to begin at the beginning. The amount of media data that is prefetched is determined
        ///     by the value of the element's <see cref="Preload" /> attribute.
        ///     <para />
        ///     This method is generally only useful when you've made dynamic changes to the set of sources
        ///     available for the media element, either by changing the element's <see cref="Source" /> attribute.
        ///     <see cref="Load" /> will reset the element and rescan the available sources, thereby causing
        ///     the changes to take effect.
        /// </summary>
        void Load();

        /// <summary>
        ///     The <see cref="Play" /> method attempts to begin playback of the media.
        /// </summary>
        void Play();

        /// <summary>
        ///     The <see cref="PlayAsync" /> method attempts to begin playback of the media async. It returns a
        ///     <see cref="bool" /> which is true when playback has been successfully started. Failure to begin
        ///     playback for any reason, such as permission issues, result is false.
        /// </summary>
        /// <returns>
        ///     A <see cref="bool" /> which returns true when playback has been started, or false if for any reason playback cannot
        ///     be started.
        /// </returns>
        Task<bool> PlayAsync();


        /// <summary>
        ///     The <see cref="Pause" /> method will pause playback of the media, if the media is
        ///     already in a paused state this method will have no effect.
        /// </summary>
        void Pause();

        /// <summary>
        ///     The <see cref="IMediaElement" /> method <see cref="CanPlayType" /> reports how likely it is that the current
        ///     browser will be able to play media of a given MIME type.
        /// </summary>
        /// <param name="mediaType">A <see cref="string" /> containing the MIME type of the media.</param>
        /// <returns>A <see cref="CanPlayResponse" /> indicating how likely it is that the media can be played.</returns>
        CanPlayResponse CanPlayType(string mediaType);

        #region Properties

        /// <summary>
        ///     The <see cref="Source" /> property sets the data of the media to embed.
        /// </summary>
        /// <value>A class which implements the <see cref="IFile" /> interface.</value>
        // TODO: Create IFile interface
        //IFile Source { get; set; }

        /// <summary>
        ///     The <see cref="Volume" /> property sets the volume at which the media will be played.
        /// </summary>
        /// <value>
        ///     A <see cref="double" /> values must fall between 0 and 1, where 0 is effectively muted and 1 is the loudest
        ///     possible value.
        /// </value>
        double Volume { get; set; }

        /// <summary>
        ///     The <see cref="IsMuted" /> indicates whether the media element muted.
        /// </summary>
        /// <value>A <see cref="bool" />. true means muted and false means not muted.</value>
        bool IsMuted { get; set; }

        /// <summary>
        ///     The <see cref="IsDefaultMuted" /> property reflects the <see cref="IsMuted" /> attribute, which
        ///     indicates whether the media element's audio output should be muted by default. This property
        ///     has no dynamic effect. To mute and unmute the audio output, use the <see cref="IsMuted" /> property.
        /// </summary>
        /// <value>A <see cref="bool" />. A value of true means that the audio output will be muted by default.</value>
        bool IsDefaultMuted { get; set; }

        /// <summary>
        ///     The <see cref="IsLooped" /> property controls whether the media element should start over when it reaches the end.
        /// </summary>
        /// <value>A <see cref="bool" />.</value>
        bool IsLooped { get; set; }

        /// <summary>
        ///     The <see cref="IsAutoPlayed" /> property indicates whether playback should automatically
        ///     begin as soon as enough media is available to do so without interruption.
        /// </summary>
        /// <value>
        ///     A <see cref="bool" /> whose value is true if the media element will begin playback
        ///     as soon as enough content has loaded to allow it to do so without interruption.
        ///     <para />
        ///     <strong>Note:</strong> Some browsers offer users the ability to override <see cref="IsAutoPlayed" /> in order to
        ///     prevent
        ///     disruptive audio or video from playing without permission or in the background. Do not rely
        ///     on <see cref="IsAutoPlayed" /> actually starting playback and instead use <see cref="Play" /> event.
        /// </value>
        bool IsAutoPlayed { get; set; }

        /// <summary>
        ///     The <see cref="IsRemotePlaybackDisabled" /> property determines whether the media
        ///     element is allowed to have a remote playback UI.
        /// </summary>
        /// <value>
        ///     A <see cref="bool" /> indicating whether the media element may have a remote playback UI.
        ///     (false means "not disabled", which means "enabled")
        /// </value>
        bool IsRemotePlaybackDisabled { get; set; }

        /// <summary>
        ///     The <see cref="Preload" /> property is intended to provide a hint to the browser about what the author
        ///     thinks will lead to the best user experience with regards to what content is loaded before the video is played.
        /// </summary>
        /// <value>
        ///     The default value is different for each browser. The spec advises it to be set to metadata.
        ///     <strong>Note:</strong>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>
        ///                 The <see cref="IsAutoPlayed" /> property has precedence over <see cref="Preload" />. If
        ///                 <see cref="IsAutoPlayed" /> is specified,
        ///                 the browser would obviously need to start downloading the media for playback.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 The specification does not force the browser to follow the value of this attribute; it is a mere hint.
        ///             </description>
        ///         </item>
        ///     </list>
        /// </value>
        PreloadState Preload { get; set; }

        /// <summary>
        ///     The <see cref="PlaybackRate" /> property sets the rate at which the media is being
        ///     played back. This is used to implement user controls for fast forward, slow motion, and so forth.
        ///     The normal playback rate is multiplied by this value to obtain the current rate, so a value of 1.0
        ///     indicates normal speed.
        ///     <para />
        ///     If <see cref="PlaybackRate" /> is negative, the media is not played backwards.
        ///     <para />
        ///     The audio is muted when the fast forward or slow motion is outside a useful range (for
        ///     example, Gecko mutes the sound outside the range 0.25 to 5.0).
        /// </summary>
        /// <value>
        ///     A <see cref="double" />. 1.0 is "normal speed," values lower than 1.0 make the media play slower than
        ///     normal, higher values make it play faster. (Default: 1.0)
        /// </value>
        double PlaybackRate { get; set; }

        /// <summary>
        ///     The <see cref="DefaultPlaybackRate" /> property indicates the default playback rate for the media.
        /// </summary>
        /// <value>
        ///     A <see cref="double" />. 1.0 is "normal speed," values lower than 1.0 make the media play slower than
        ///     normal, higher values make it play faster. The value 0.0 is invalid and throws an exception.
        /// </value>
        double DefaultPlaybackRate { get; set; }

        /// <summary>
        ///     The <see cref="Buffered" /> read-only property returns a new <see cref="TimeRanges" /> object
        ///     that indicates the ranges of the media source that the browser has buffered (if any) at the moment the
        ///     buffered property is accessed.
        /// </summary>
        /// <value>
        ///     A <see cref="TimeRanges" /> object. This object is normalized, which means that ranges are ordered, don't
        ///     overlap, aren't empty, and don't touch (adjacent ranges are folded into one bigger range).
        /// </value>
        ITimeRanges Buffered { get; }

        /// <summary>
        ///     The <see cref="Seekable" /> read-only property of the <see cref="IMediaElement" /> returns a
        ///     <see cref="TimeRanges" /> object
        ///     that contains the time ranges that the user is able to seek to, if any.
        /// </summary>
        /// <value>A <see cref="TimeRanges" /> object.</value>
        ITimeRanges Seekable { get; }

        /// <summary>
        ///     The read-only <see cref="IMediaElement" /> property <see cref="Duration" /> indicates the length of the element's
        ///     media in seconds.
        /// </summary>
        /// <value>
        ///     A double-precision floating-point value indicating the duration of the media in seconds. If no
        ///     media data is available, the value <see cref="double.NaN" /> is returned. If the element's media doesn't have a
        ///     known
        ///     duration—such as for live media streams—the value of <see cref="Duration" /> is
        ///     <see cref="double.PositiveInfinity" />.
        /// </value>
        double Duration { get; }

        /// <summary>
        ///     The <see cref="IMediaElement" /> interface's <see cref="CurrentPosition" /> property specifies the current playback
        ///     time
        ///     in seconds. Changing the value of <see cref="CurrentPosition" /> seeks the media to the new time.
        /// </summary>
        /// <value>
        ///     A double-precision floating-point value indicating the current playback time in seconds.
        ///     <para />
        ///     If the media is not yet playing, the value of <see cref="CurrentPosition" /> indicates the time position within the
        ///     media at which playback will begin once the <see cref="Play" /> method is called.
        ///     <para />
        ///     Setting <see cref="CurrentPosition" /> to a new value seeks the media to the given time, if the media is available.
        ///     <para />
        ///     For media without a known duration—such as media being streamed live—it's possible that the
        ///     browser may not be able to obtain parts of the media that have expired from the media buffer.
        ///     Also, media whose timeline doesn't begin at 0 seconds cannot be seeked to a time before its
        ///     timeline's earliest time.
        /// </value>
        double CurrentPosition { get; set; }

        /// <summary>
        ///     The <see cref="IsEnded" /> property indicates whether the media element has ended playback.
        /// </summary>
        /// <value>
        ///     A <see cref="bool" /> which is true if the media contained in the element has finished playing.
        ///     <para />
        ///     If the source of the media is a stream, this value is true if the value of the stream's active property is false.
        /// </value>
        bool IsEnded { get; }

        /// <summary>
        ///     The read-only <see cref="IsPaused" /> property tells whether the media element is paused.
        /// </summary>
        /// <value>A <see cref="bool" />. true is paused and false is not paused.</value>
        bool IsPaused { get; }

        /// <summary></summary>
        /// <value>Returns a <see cref="bool" /> that indicates whether the media is in the process of seeking to a new position.</value>
        bool IsSeeking { get; }

        /// <summary>
        ///     The <see cref="Error" /> is the <see cref="MediaError" /> object for the most recent error, or null if
        ///     there has not been an error. When an <see cref="OnError" /> event is received by the element, you can
        ///     determine details about what happened by examining this object.
        /// </summary>
        /// <value>
        ///     A <see cref="MediaError" /> object describing the most recent error to occur on the media element or null
        ///     if no errors have occurred.
        /// </value>
        IMediaError Error { get; }

        /// <summary>
        ///     The <see cref="NetworkState" /> property indicates the current state of the fetching of media over the network.
        /// </summary>
        /// <value></value>
        NetworkState NetworkState { get; }

        /// <summary>
        ///     The <see cref="ReadyState" /> property indicates the readiness state of the media.
        /// </summary>
        /// <value></value>
        ReadyState ReadyState { get; }

        # endregion

        #region Events

        /// <summary>
        ///     The <see cref="OnAbort" /> event is fired when the resource was not fully loaded,
        ///     but not as the result of an error
        /// </summary>
        event EventHandler OnAbort;

        /// <summary>
        ///     The <see cref="OnCanPlay" /> event is fired when the user agent can play the media,
        ///     but estimates that not enough data has been loaded to play the media up to its end
        ///     without having to stop for further buffering of content.
        /// </summary>
        event EventHandler OnCanPlay;

        /// <summary>
        ///     The <see cref="OnCanPlayThrough" /> event is fired when the user agent can play the media,
        ///     and estimates that enough data has been loaded to play the media up to its end
        ///     without having to stop for further buffering of content.
        /// </summary>
        event EventHandler OnCanPlayThrough;

        /// <summary>
        ///     The <see cref="OnComplete" /> event is fired when the rendering of an
        ///     offline audio context is complete.
        /// </summary>
        event EventHandler OnComplete;

        /// <summary>
        ///     The <see cref="OnDurationChanged" /> event is fired when the duration attribute has been updated.
        /// </summary>
        event EventHandler OnDurationChanged;

        /// <summary>
        ///     The <see cref="OnEmptied" /> event is fired when the media has become empty;
        ///     for example, this event is sent if the media has already been loaded (or partially loaded),
        ///     and the <see cref="Load()" /> method is called to reload it.
        /// </summary>
        event EventHandler OnEmptied;

        /// <summary>
        ///     The <see cref="OnEnded" /> event is fired when playback or streaming has stopped because the end of the media was
        ///     reached
        ///     or because no further data is available.
        /// </summary>
        event EventHandler OnEnded;

        /// <summary>
        ///     The <see cref="OnError" /> event is fired when the resource could not be loaded due to an error
        ///     (for example, a network connectivity problem).
        /// </summary>
        event EventHandler OnError;

        /// <summary>
        ///     The <see cref="OnLoadedData" /> event is fired when the frame at the current playback position of the media has
        ///     finished loading;
        ///     often the first frame.
        /// </summary>
        event EventHandler OnLoadedData;

        /// <summary>
        ///     The <see cref="OnLoadedMetaData" /> event is fired when the metadata has been loaded.
        /// </summary>
        event EventHandler OnLoadedMetaData;

        /// <summary>
        ///     The <see cref="OnStartLoading" /> event is fired when the browser has started to load a resource.
        /// </summary>
        event EventHandler OnStartLoading;

        /// <summary>
        ///     The <see cref="OnPause" /> event is sent when a request to pause an activity is handled and the activity has
        ///     entered its paused state,
        ///     most commonly after the media has been paused through a call to the element's <see cref="Pause()" /> method.
        /// </summary>
        event EventHandler OnPause;

        /// <summary>
        ///     The <see cref="OnPlay" /> event is fired when the <see cref="IsPaused" /> property is changed from <c>true</c> to
        ///     <c>false</c>,
        ///     as a result of the <see cref="Play()" /> method, or the <see cref="IsAutoPlayed" /> attribute.
        /// </summary>
        event EventHandler OnPlay;

        /// <summary>
        ///     The <see cref="OnProgress" /> event is fired periodically as the browser loads a resource.
        /// </summary>
        event EventHandler OnProgress;

        /// <summary>
        ///     The <see cref="OnRateChanged" /> event is fired when the playback rate has changed.
        /// </summary>
        event EventHandler OnRateChanged;

        /// <summary>
        ///     The <see cref="OnSeeked" /> event is fired when a seek operation completed,
        ///     the current playback position has changed,
        ///     and the Boolean seeking attribute is changed to <c>false</c>.
        /// </summary>
        event EventHandler OnSeeked;

        /// <summary>
        ///     The <see cref="OnSeeking" /> event is fired when a seek operation starts,
        ///     meaning the Boolean <see cref="IsSeeking" /> attribute has changed to <c>true</c> and the media is seeking a new
        ///     position.
        /// </summary>
        event EventHandler OnSeeking;

        /// <summary>
        ///     The <see cref="OnStalled" /> event is fired when the user agent is trying to fetch media data,
        ///     but data is unexpectedly not forthcoming.
        /// </summary>
        event EventHandler OnStalled;

        /// <summary>
        ///     The <see cref="OnSuspend" /> event is fired when media data loading has been suspended.
        /// </summary>
        event EventHandler OnSuspend;

        /// <summary>
        ///     The <see cref="OnPositionChanged" /> event is fired when the time indicated by the currentTime attribute has been
        ///     updated.
        /// </summary>
        event EventHandler OnPositionChanged;

        /// <summary>
        ///     The <see cref="OnVolumeChanged" /> event is fired when the volume has changed.
        /// </summary>
        event EventHandler OnVolumeChanged;

        /// <summary>
        ///     The <see cref="OnWaiting" /> event is fired when playback has stopped because of a temporary lack of data.
        /// </summary>
        event EventHandler OnWaiting;

        #endregion
    }
}