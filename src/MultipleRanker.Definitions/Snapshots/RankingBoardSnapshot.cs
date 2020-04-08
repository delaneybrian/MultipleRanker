using System;
using System.Collections.Generic;

namespace MultipleRanker.Definitions.Snapshots
{
    public class RankingBoardSnapshot
    {
        public Guid Id { get; set; }

        public ICollection<ParticipantSnapshot> Participants { get; set; } = new List<ParticipantSnapshot>();
    }
}
