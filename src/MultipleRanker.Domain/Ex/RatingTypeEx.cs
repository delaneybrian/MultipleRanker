using System;
using MultipleRanker.Definitions;

namespace MultipleRanker.Domain
{
    internal static class RatingTypeEx
    {
        public static RatingType ToRatingType(this RankerApi.Contracts.RatingType ratingType) =>
            ratingType switch
            {
                RankerApi.Contracts.RatingType.Collies => RatingType.Collies,
                RankerApi.Contracts.RatingType.Keeners => RatingType.Keeners,
                RankerApi.Contracts.RatingType.Elo => RatingType.Elo,
                RankerApi.Contracts.RatingType.Markov => RatingType.Markov,
                RankerApi.Contracts.RatingType.OffensiveDefensive => RatingType.OffensiveDefensive,
                RankerApi.Contracts.RatingType.Masseys=> RatingType.Masseys,
                _ => throw new ArgumentException($"{nameof(RankerApi.Contracts.RatingType)} {ratingType} is not a valid {nameof(RatingType)}")
            };
    }
}
