using MultipleRanker.Definitions;
using System.Collections.Generic;

namespace MultipleRanker.Domain.Raters
{
    public interface IRater
    {
        bool IsFor(RaterType rankerType);

        IEnumerable<ParticipantRating> Rate(RatingBoardModel rankingBoardModel);
    }
}
