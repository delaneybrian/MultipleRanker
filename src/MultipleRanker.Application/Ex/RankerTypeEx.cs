using System;
using MultipleRanker.Domain.Rankers;

namespace MultipleRanker.Application
{
    internal static class RankerTypeEx
    {
        public static RankerType ToRankerType(this Definitions.RatingType rankerType)
        {
            switch (rankerType)
            {
                case (Definitions.RatingType.ColleyMethod):
                    return RankerType.ColleyMethod;
                case (Definitions.RatingType.MarkovMethod):
                    return RankerType.MarkovMethod;
                case (Definitions.RatingType.MasseyMethod):
                    return RankerType.MasseyMethod;
                case (Definitions.RatingType.KeenerMethod):
                    return RankerType.KeenerMethod;
                case (Definitions.RatingType.OffensiveDefensive):
                    return RankerType.OffensiveDefensive;
                default:
                    throw new ArgumentException($"{rankerType.ToString()} is not a valid RankerType");
            }
        }
    }
}
