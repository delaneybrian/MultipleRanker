using System;
using System.Runtime.Serialization;
using MediatR;

namespace MultipleRanker.Contracts.Messages
{
    [DataContract]
    public class AddParticipantToRatingBoard : IRequest
    {
        [DataMember]
        public Guid RankingBoardId { get; set; }

        [DataMember]
        public Guid ParticipantId { get; set; }

        [DataMember]
        public string ParticipantName { get; set; }
    }
}
