using System;
using System.Collections.Generic;
using MultipleRanker.Contracts;
using MultipleRanker.Definitions;

namespace MultipleRanker.Domain.Raters.Raters
{
    public class EloRater : IRater
    {
        public bool IsFor(RatingType ratingType)
        {
            return ratingType == RatingType.Elo;
        }

        public IEnumerable<ParticipantRating> Rate(RatingListModel ratingBoardModel)
        {
            throw new NotImplementedException();
        }
    }
}
