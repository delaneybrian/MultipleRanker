using System;
using System.Runtime.Serialization;
using MediatR;

namespace MultipleRanker.Definitions.Commands
{
    [DataContract]
    public class CreateRankingBoardCommand : IRequest
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
