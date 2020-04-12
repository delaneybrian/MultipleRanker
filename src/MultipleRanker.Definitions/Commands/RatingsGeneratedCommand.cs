using System;
using System.Collections.Generic;
using MediatR;

namespace MultipleRanker.Definitions.Commands
{
    public class RatingsGeneratedCommand : IRequest
    {
        public Guid RatingBoardId { get; set; }

        public ICollection<ParticipantRating> ParticipantRatings { get; set; }

        public RatingType RatingType { get; set; }
    }
}
