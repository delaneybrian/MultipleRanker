using System;
using System.Collections.Generic;

namespace MultipleRanker.Definitions.Snapshots
{
    public class RankingBoardSnapshot
    {
        public Guid Id { get; set; }

        public ICollection<RankingBoardParticipantSnapshot> RankingBoardParticipants { get; set; } 
            = new List<RankingBoardParticipantSnapshot>();
    }
}
