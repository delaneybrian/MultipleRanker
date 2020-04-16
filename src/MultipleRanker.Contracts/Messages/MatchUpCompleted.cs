using System;
using System.Collections.Generic;
using MediatR;

namespace MultipleRanker.Contracts.Messages
{
    public class MatchUpCompleted : IRequest
    {
        public Guid RatingBoardId { get; set; }

        public ICollection<MatchUpParticipantScore> ParticipantScores { get; set; } =
            new List<MatchUpParticipantScore>();
    }
}
