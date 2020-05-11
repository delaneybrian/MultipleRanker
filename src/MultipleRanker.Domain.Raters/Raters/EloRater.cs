using System;
using System.Collections.Generic;
using MultipleRanker.Contracts;

namespace MultipleRanker.Domain.Raters.Raters
{
    public class EloRater : IRater
    {
        public bool IsFor(RaterType raterType)
        {
            return raterType == RatingType.Elo ;
        }

        public IEnumerable<ParticipantRating> Rate(RatingListModel ratingBoardModel)
        {
            throw new NotImplementedException();
        }
    }
}
