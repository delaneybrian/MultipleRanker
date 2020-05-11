using System;
using System.Runtime.Serialization;

namespace MultipleRanker.Definitions.Snapshots
{
    [DataContract]
    public class RatingListResultSnapshot
    {
        [DataMember]
        public Guid ResultId { get; set; }

        [DataMember]
        public Guid Participant1Id { get; set; }

        [DataMember]
        public Guid Participant2Id { get; set; }

        [DataMember]
        public double Participant1Score { get; set; }

        [DataMember]
        public double Participant2Score { get; set; }

        [DataMember]
        public DateTime ResultTimeUtc { get; set; }
    }
}
