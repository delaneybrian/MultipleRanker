using MultipleRanker.Contracts;
using MultipleRanker.Contracts.Messages;


namespace MultipleRanker.Application
{
    public static class RatingResultEx
    {
        public static RatingsGenerated ToCommand(this RatingResults ratingResults)
        {
            return new RatingsGenerated
            {
                RatingBoardId = ratingResults.RankingBoardId,
                ParticipantRatings = ratingResults.ParticipantRatings,
                RatingType = ratingResults.RatingType
            };
        }
    }
}
