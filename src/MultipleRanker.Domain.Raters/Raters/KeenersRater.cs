﻿using System;
using System.Collections.Generic;
using MultipleRanker.Contracts;

namespace MultipleRanker.Domain.Raters.Raters
{
    public class KeenersRater : IRater
    {
        public bool IsFor(RaterType raterType)
        {
            return raterType == RaterType.KeenerMethod;
        }

        public IEnumerable<ParticipantRating> Rate(RatingListModel ratingListModel)
        {
            throw new NotImplementedException();
        }
    }
}
