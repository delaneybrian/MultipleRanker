using System.Collections.Generic;
using MultipleRanker.Contracts;
using MultipleRanker.Definitions;

namespace MultipleRanker.Domain.Raters
{
    public interface IRater

    {
        bool IsFor(RatingType ratingType);

        IEnumerable<ParticipantRating> Rate(RatingListModel ratingBoardModel);
    }
}
