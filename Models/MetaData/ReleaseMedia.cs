using NodaTime;
using TED.Utility;

namespace TED.Models.MetaData
{
    [Serializable]
    public sealed class ReleaseMedia : MetaDataBase
    {
        public ReleaseMedia()
            : base(null, null)
        {
        }

        public ReleaseMedia(IRandomNumber randomNumber, IClock clock)
            : base(randomNumber, clock)
        {
        }

        public short? MediaNumber { get; set; }

        public string? SubTitle { get; set; }

        public int? TrackCount { get; set; }

        public IEnumerable<Track> Tracks { get; set; } = Enumerable.Empty<Track>();
    }
}