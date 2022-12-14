using NodaTime;
using System.Text.Json.Serialization;
using TED.Enums;
using TED.Extensions;
using TED.Utility;

namespace TED.Models.MetaData
{
    public class Release : MetaDataBase
    {
        public Release()
            :this(null, null)
        {
        }

        public Release(IRandomNumber? randomNumber, IClock? clock)
            : base(randomNumber, clock)
        {
        }

        public override string ToString()
        {
            return $"Status [{Status}] Directory [{ Directory }]";
        }

        public DataToken? Artist { get; set; }

        public Image? ArtistThumbnail { get; set; }

        public Image? CoverImage { get; set; }

        public double? Duration { get; set; }

        public string? Directory { get; set; }

        public string DurationTime
        {
            get
            {
                if (!Duration.HasValue)
                {
                    return "--:--";
                }

                return new TimeInfo(SafeParser.ToNumber<decimal>(Duration.Value)).ToFullFormattedString();
            }
        }

        public string FormattedMediaSize
        {
            get
            {
                return SafeParser.ToNumber<long>(Media?.SelectMany(x => x.Tracks).Sum(x => x.FileSize) ?? 0).FormatFileSize();
            }
        }

        public DataToken? Genre { get; set; }

        public bool IsValid => Id != Guid.Empty &&
                       !string.IsNullOrEmpty(Artist?.Text) &&
                       !string.IsNullOrEmpty(ReleaseData?.Text) &&
                       CoverImage?.Bytes?.Length > 0 &&
                       (Status != Statuses.Incomplete && Status != Statuses.NeedsAttention && Status != Statuses.Incomplete);

        public IEnumerable<ReleaseMedia>? Media { get; set; }

        public int? MediaCount { get; set; }

        public DataToken? ReleaseData { get; set; }

        string? _releaseDate;
        public string? ReleaseDate
        {
            get
            {
                if (!string.IsNullOrEmpty(_releaseDate))
                {
                    return _releaseDate;
                }
                return ReleaseDateDateTime.HasValue
                        ? ReleaseDateDateTime.Value.ToUniversalTime().ToString("yyyy-MM-dd")
                        : null;
            }
            set => _releaseDate = value;
        }

        public int? Year { get; set; }

        [JsonIgnore]
        public DateTime? ReleaseDateDateTime { get; set; } = DateTime.MinValue;

        public int? TrackCount { get; set; }

        public Statuses Status { get; set; } = Statuses.Incomplete;

        [JsonIgnore]
        public List<ProcessMessage> ProcessingMessages { get; set; } = new List<ProcessMessage>();
    }
}