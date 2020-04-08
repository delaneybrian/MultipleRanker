using System;
using System.Runtime.Serialization;

namespace MultipleRanker.Definitions.Snapshots
{
    [DataContract]
    public class ParticipantSnapshot
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
