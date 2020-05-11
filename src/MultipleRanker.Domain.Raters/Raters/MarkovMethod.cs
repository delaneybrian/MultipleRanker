using System;
using System.Collections.Generic;
using MultipleRanker.Contracts;
using MultipleRanker.Definitions;

namespace MultipleRanker.Domain.Raters.Raters
{
    public class MarkovMethod : IRater
    {
        public bool IsFor(RatingType ratingType)
        {
            return ratingType == RatingType.Markov;
        }

        public IEnumerable<ParticipantRating> Rate(RatingListModel ratingListModel)
        {
            throw new NotImplementedException();
        }
    }
}
