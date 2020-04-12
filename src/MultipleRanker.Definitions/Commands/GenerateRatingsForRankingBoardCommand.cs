using System;
using System.Runtime.Serialization;
using MediatR;

namespace MultipleRanker.Definitions.Commands
{
    [DataContract]
    public class GenerateRatingsForRankingBoardCommand : IRequest
    {
        [DataMember]
        public Guid RankingBoardId { get; set; }

        [DataMember]
        public RatingType RankerType { get; set; }
    }
}
