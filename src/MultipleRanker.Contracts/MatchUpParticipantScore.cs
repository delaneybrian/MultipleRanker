using System;
using System.Runtime.Serialization;

namespace MultipleRanker.Contracts
{
    [DataContract]
    public class MatchUpParticipantScore
    {
        [DataMember]
        public Guid ParticipantId { get; set; }

        [DataMember]
        public int PointsScored { get; set; }
    }
}
