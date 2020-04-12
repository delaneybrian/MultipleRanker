using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MultipleRanker.Definitions
{
    [DataContract]
    public class RatingResults
    {
        [DataMember]
        public Guid RankingBoardId { get; set; }

        [DataMember]
        public DateTime RatedAtUtc { get; set; }

        [DataMember]
        public ICollection<ParticipantRating> ParticipantRatings { get; set; }

        [DataMember]
        public RatingType RatingType { get; set; }
    }
}
