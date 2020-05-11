using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MultipleRanker.Infrastructure.Repositories.Mongo.Entities
{
    [DataContract]
    internal class RatingListSnapshotEntity
    {
        [BsonId]
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public ICollection<RatingListParticipantSnapshotEntity> RatingListParticipants { get; set; }
            = new List<RatingListParticipantSnapshotEntity>();

        [DataMember]
        public long MatchUpsCompleted { get; set; }

        [DataMember]
        public long NumberOfRatingsPerformed { get; set; }

        [DataMember]
        public DateTime LastRatingCalculatedAt { get; set; }

        [DataMember]
        public int MaxParticipantIndex { get; set; }
    }
}
