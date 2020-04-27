using System.Collections.Generic;
using MultipleRanker.Contracts;

namespace MultipleRanker.Domain.Raters
{
    public interface IRater
    {
        bool IsFor(RaterType raterType);

        IEnumerable<ParticipantRating> Rate(RatingBoardModel ratingBoardModel);
    }
}
