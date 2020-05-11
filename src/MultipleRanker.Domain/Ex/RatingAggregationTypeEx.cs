using System;
using MultipleRanker.Definitions;

namespace MultipleRanker.Domain
{
    internal static class RatingAggregationTypeEx
    {
        public static RatingAggregationType ToRatingAggregationType(this RankerApi.Contracts.RatingAggregationType ratingAggregationType) =>
            ratingAggregationType switch
            {
                RankerApi.Contracts.RatingAggregationType.ScoreDifference =>RatingAggregationType.ScoreDifference,
                RankerApi.Contracts.RatingAggregationType.TotalLosses => RatingAggregationType.TotalLosses,
                RankerApi.Contracts.RatingAggregationType.TotalScoreAgainst =>RatingAggregationType.TotalScoreAgainst,
                RankerApi.Contracts.RatingAggregationType.TotalScoreFor => RatingAggregationType.TotalScoreFor,
                RankerApi.Contracts.RatingAggregationType.TotalWins => RatingAggregationType.TotalWins,
                _ => throw new ArgumentException($"{nameof(RankerApi.Contracts.RatingAggregationType)} {ratingAggregationType} is not a valid {nameof(RatingAggregationType)}")
            };
    }
}
