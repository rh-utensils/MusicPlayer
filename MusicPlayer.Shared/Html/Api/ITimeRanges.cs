namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     The <see cref="ITimeRanges" /> interface is used to represent a set of time ranges, primarily for the purpose
    ///     of tracking which portions of media have been buffered when loading it for use by the
    ///     <see cref="Components.Audio" />
    ///     and <see cref="Components.Video" /> elements.
    /// </summary>
    public interface ITimeRanges
    {
        /// <summary>
        ///     The <see cref="Length" /> read-only property returns the number of ranges in the object.
        /// </summary>
        /// <value>Returns an <see cref="int" /> representing the number of time ranges represented by the time range object.</value>
        int Length { get; }

        /// <param name="index">The range number to return the starting time for.</param>
        /// <returns>Returns the time offset at which a specified time range begins.</returns>
        double Start(int index);

        /// <param name="index">The range number to return the ending time for.</param>
        /// <returns>Returns the time offset at which a specified time range ends.</returns>
        double End(int index);
    }
}