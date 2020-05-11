using System.Runtime.Serialization;

namespace MultipleRanker.Infrastructure.Repositories.Mongo.Entities
{
    [DataContract]
    internal class ValueByOpponentIdEntity<T>
    {
        [DataMember]
        public string OpponentId { get; set; }

        [DataMember]
        public T Value { get; set; }
    }
}
