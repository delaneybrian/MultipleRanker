using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MultipleRanker.Infrastructure.Repositories.Mongo.Entities
{
    [DataContract]
    internal class RatingBoardSnapshotEntity
    {
        [BsonId]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public ICollection<RatingBoardParticipantSnapshotEntity> RatingBoardParticipants { get; set; }
            = new List<RatingBoardParticipantSnapshotEntity>();

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
