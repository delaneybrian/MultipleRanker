using System;
using System.Collections.Generic;
using MultipleRanker.Contracts;
using MultipleRanker.Definitions;

namespace MultipleRanker.Domain.Raters.Raters
{
    public class KeenersRater : IRater
    {
        public bool IsFor(RatingType ratingType)
        {
            return ratingType == RatingType.Keeners;
        }

        public IEnumerable<ParticipantRating> Rate(RatingListModel ratingListModel)
        {
            throw new NotImplementedException();
        }
    }
}
