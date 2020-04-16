using System.Collections.Generic;
using MultipleRanker.Contracts;

namespace MultipleRanker.Domain.Raters
{
    public interface IRater
    {
        bool IsFor(RaterType rankerType);

        IEnumerable<ParticipantRating> Rate(RatingBoardModel rankingBoardModel);
    }
}
