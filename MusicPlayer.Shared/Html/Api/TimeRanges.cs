using System.Collections.Generic;

namespace MusicPlayer.Shared.Html.Api
{
    /// <summary>
    ///     A <see cref="TimeRanges" /> object includes one or more ranges of time, each specified by a starting and
    ///     ending time offset. You reference each time range by using the <see cref="Start" /> and <see cref="End" /> methods,
    ///     passing the index number of the time range you want to retrieve.
    /// </summary>
    public class TimeRanges : ITimeRanges
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TimeRanges" /> class.
        /// </summary>
        /// <param name="timeRanges">An Array containing the time for the start and end of the ranges.</param>
        public TimeRanges((double start, double end)[] timeRanges)
        {
            _timeRanges = timeRanges;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimeRanges" /> class.
        /// </summary>
        /// <param name="timeRanges">A List containing the time for the start and end of the ranges.</param>
        public TimeRanges(List<(double start, double end)> timeRanges)
        {
            _timeRanges = timeRanges.ToArray();
        }

        private (double start, double end)[] _timeRanges { get; }

        /// <inheritdoc />
        public int Length => _timeRanges.Length;

        /// <inheritdoc />
        public double Start(int index)
        {
            return _timeRanges[index].start;
        }

        /// <inheritdoc />
        public double End(int index)
        {
            return _timeRanges[index].start;
        }
    }
}