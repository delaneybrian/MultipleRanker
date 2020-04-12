using MultipleRanker.Definitions;
using MultipleRanker.Definitions.Commands;

namespace MultipleRanker.Application
{
    public static class RatingResultEx
    {
        public static RatingsGeneratedCommand ToCommand(this RatingResults ratingResults)
        {
            return new RatingsGeneratedCommand
            {
                RatingBoardId = ratingResults.RankingBoardId,
                ParticipantRatings = ratingResults.ParticipantRatings,
                RatingType = ratingResults.RatingType
            };
        }
    }
}
