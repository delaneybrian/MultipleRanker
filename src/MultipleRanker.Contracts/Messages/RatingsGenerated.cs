using System;
using System.Collections.Generic;

namespace MultipleRanker.Contracts.Messages
{
    public class RatingsGenerated
    {
        public Guid RatingListId { get; set; }

        public Guid RatingId { get; set; }

        public ICollection<ParticipantRating> ParticipantRatings { get; set; }

        public DateTime CalculatedAtUtc { get; set; }
    }
}
