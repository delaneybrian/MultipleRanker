using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MultipleRanker.Definitions.Snapshots
{
    [DataContract]
    public class RatingListSnapshot
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public ICollection<RatingListParticipantSnapshot> RatingListParticipants { get; set; } 
            = new List<RatingListParticipantSnapshot>();

        [DataMember]
        public long MatchUpsCompleted { get; set; }

        [DataMember]
        public long NumberOfRatingsPerformed { get; set; }

        [DataMember]
        public DateTime LastRatingCalculatedAt { get; set; }

        [DataMember]
        public int MaxParticipantIndex { get; set; }
    }
}
