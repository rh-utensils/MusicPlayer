namespace MusicPlayer.Shared.Html.Api.Media
{
    public enum NetworkState
    {
        /// <summary>
        ///     There is no data yet. Also, <see cref="ReadyState" /> is <see cref="ReadyState.HAVE_NOTHING" /> .
        /// </summary>
        NETWORK_EMPTY = 0,

        /// <summary>
        ///     The <see cref="Html.Media" /> Element is active and has selected a resource, but is not using the network.
        /// </summary>
        NETWORK_IDLE = 1,

        /// <summary>
        ///     The browser is downloading <see cref="Html.Media" /> Element data.
        /// </summary>
        NETWORK_LOADING = 2,

        /// <summary>
        ///     No <see cref="Html.Media" /> Element Source found.
        /// </summary>
        NETWORK_NO_SOURCE = 3
    }
}