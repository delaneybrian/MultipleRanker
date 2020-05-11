using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MultipleRanker.Infrastructure.Repositories.Mongo.Entities
{
    [DataContract]
    internal class RatingListResultSnapshotEntity
    {
        [DataMember]
        [BsonId]
        public string ResultId { get; set; }

        [DataMember]
        public string Participant1Id { get; set; }

        [DataMember]
        public string Participant2Id { get; set; }

        [DataMember]
        public double Participant1Score { get; set; }

        [DataMember]
        public double Participant2Score { get; set; }

        [DataMember]
        public DateTime ResultTimeUtc { get; set; }
    }
}
