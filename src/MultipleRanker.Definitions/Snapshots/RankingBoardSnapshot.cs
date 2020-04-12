﻿using System;
using System.Collections.Generic;

namespace MultipleRanker.Definitions.Snapshots
{
    public class RankingBoardSnapshot
    {
        public Guid Id { get; set; }

        public ICollection<RankingBoardParticipantSnapshot> RankingBoardParticipants { get; set; } 
            = new List<RankingBoardParticipantSnapshot>();

        public long MatchUpsCompleted { get; set; }

        public long NumberOfRatingsPerformed { get; set; }

        public DateTime LastRatingCalculatedAt { get; set; }
    }
}
