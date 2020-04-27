using System;
using System.Runtime.Serialization;

namespace MultipleRanker.Contracts.Messages
{
    [DataContract]
    public class GenerateRatingsForRatingBoard
    {
        [DataMember]
        public Guid RatingBoardId { get; set; }

        [DataMember]
        public RatingType RatingType { get; set; }
    }
}
