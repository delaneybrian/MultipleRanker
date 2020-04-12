using System;
using System.Runtime.Serialization;

namespace MultipleRanker.Definitions
{
    [DataContract]
    public class ParticipantRating
    {
        [DataMember]
        public Guid ParticipantId { get; set; }

        [DataMember]
        public double Rating { get; set; }
    }
}
