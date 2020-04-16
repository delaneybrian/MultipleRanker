using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MultipleRanker.Contracts
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public ICollection<byte> Content { get; set; }

        [DataMember]
        public string RoutingKey { get; set; }

        [DataMember]
        public Guid CorrelationId { get; set; }

        [DataMember]
        public string AssemblyQualifiedName { get; set; }
    }
}
