using System;
using System.Runtime.Serialization;
using MediatR;

namespace MultipleRanker.Contracts.Messages
{
    [DataContract]
    public class CreateRatingBoard : IRequest
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
