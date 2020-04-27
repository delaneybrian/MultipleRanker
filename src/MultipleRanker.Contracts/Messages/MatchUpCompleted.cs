using System;
using System.Collections.Generic;

namespace MultipleRanker.Contracts.Messages
{
    public class MatchUpCompleted
    {
        public Guid RatingBoardId { get; set; }

        public ICollection<MatchUpParticipantScore> ParticipantScores { get; set; } =
            new List<MatchUpParticipantScore>();
    }
}
