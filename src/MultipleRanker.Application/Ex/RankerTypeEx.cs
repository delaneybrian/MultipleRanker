using System;
using MultipleRanker.Domain.Raters;

namespace MultipleRanker.Application
{
    internal static class RankerTypeEx
    {
        public static RaterType ToRankerType(this Definitions.RatingType rankerType)
        {
            switch (rankerType)
            {
                case (Definitions.RatingType.ColleyMethod):
                    return RaterType.ColleyMethod;
                case (Definitions.RatingType.MarkovMethod):
                    return RaterType.MarkovMethod;
                case (Definitions.RatingType.MasseyMethod):
                    return RaterType.MasseyMethod;
                case (Definitions.RatingType.KeenerMethod):
                    return RaterType.KeenerMethod;
                case (Definitions.RatingType.OffensiveDefensive):
                    return RaterType.OffensiveDefensive;
                default:
                    throw new ArgumentException($"{rankerType.ToString()} is not a valid RankerType");
            }
        }
    }
}
