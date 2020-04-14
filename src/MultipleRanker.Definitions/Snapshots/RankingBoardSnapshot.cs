using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MultipleRanker.Definitions.Snapshots
{
    [DataContract]
    public class RankingBoardSnapshot
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public ICollection<RankingBoardParticipantSnapshot> RankingBoardParticipants { get; set; } 
            = new List<RankingBoardParticipantSnapshot>();

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
