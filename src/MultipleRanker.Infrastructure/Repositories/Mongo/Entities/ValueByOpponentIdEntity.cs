using System;
using System.Runtime.Serialization;

namespace MultipleRanker.Infrastructure.Repositories.Mongo.Entities
{
    [DataContract]
    internal class ValueByOpponentIdEntity
    {
        [DataMember]
        public Guid OpponentId { get; set; }

        [DataMember]
        public long Value { get; set; }
    }
}
