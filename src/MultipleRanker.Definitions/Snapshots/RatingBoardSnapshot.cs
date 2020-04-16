using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MultipleRanker.Definitions.Snapshots
{
    [DataContract]
    public class RatingBoardSnapshot
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public ICollection<RatingBoardParticipantSnapshot> RatingBoardParticipants { get; set; } 
            = new List<RatingBoardParticipantSnapshot>();

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
