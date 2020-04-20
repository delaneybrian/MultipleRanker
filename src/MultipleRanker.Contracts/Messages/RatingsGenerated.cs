using System;
using System.Collections.Generic;
using MediatR;

namespace MultipleRanker.Contracts.Messages
{
    public class RatingsGenerated : IRequest
    {
        public Guid RatingBoardId { get; set; }

        public ICollection<ParticipantRating> ParticipantRatings { get; set; }

        public RatingType RatingType { get; set; }

        public DateTime CalculatedAtUtc { get; set; }
    }
}
