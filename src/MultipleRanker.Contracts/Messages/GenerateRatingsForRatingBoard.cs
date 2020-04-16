using System;
using System.Runtime.Serialization;
using MediatR;

namespace MultipleRanker.Contracts.Messages
{
    [DataContract]
    public class GenerateRatingsForRatingBoard : IRequest
    {
        [DataMember]
        public Guid RatingBoardId { get; set; }

        [DataMember]
        public RatingType RatingType { get; set; }
    }
}
