using System;
using System.Collections.Generic;
using MultipleRanker.Contracts;

namespace MultipleRanker.Domain.Raters.Raters
{
    public class MarkovMethod : IRater
    {
        public bool IsFor(RaterType raterType)
        {
            return raterType == RaterType.MarkovMethod;
        }

        public IEnumerable<ParticipantRating> Rate(RatingBoardModel ratingBoardModel)
        {
            throw new NotImplementedException();
        }
    }
}
