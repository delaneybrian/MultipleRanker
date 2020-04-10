using System;
using System.Collections.Generic;
using MediatR;

namespace MultipleRanker.Definitions.Commands
{
    public class MatchUpCompletedCommand : IRequest
    {
        public Guid RankingBoardId { get; set; }

        public ICollection<MatchUpParticipantScore> ParticipantScores { get; set; } =
            new List<MatchUpParticipantScore>();
    }
}
