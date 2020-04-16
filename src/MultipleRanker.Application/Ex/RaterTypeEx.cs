using System;
using MultipleRanker.Domain.Raters;

namespace MultipleRanker.Application
{
    internal static class RaterTypeEx
    {
        public static RaterType ToRankerType(this Contracts.RatingType ratingType)
        {
            switch (ratingType)
            {
                case (Contracts.RatingType.ColleyMethod):
                    return RaterType.ColleyMethod;
                case (Contracts.RatingType.MarkovMethod):
                    return RaterType.MarkovMethod;
                case (Contracts.RatingType.MasseyMethod):
                    return RaterType.MasseyMethod;
                case (Contracts.RatingType.KeenerMethod):
                    return RaterType.KeenerMethod;
                case (Contracts.RatingType.OffensiveDefensive):
                    return RaterType.OffensiveDefensive;
                default:
                    throw new ArgumentException($"{ratingType.ToString()} is not a valid RaterType");
            }
        }
    }
}
