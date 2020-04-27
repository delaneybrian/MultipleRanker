using System;
using System.Runtime.Serialization;

namespace MultipleRanker.Contracts.Messages
{
    [DataContract]
    public class CreateRatingBoard
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
