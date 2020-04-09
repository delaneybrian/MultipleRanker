using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MultipleRanker.Infrastructure.Repositories.Mongo.Entities
{
    [DataContract]
    internal class RankingBoardSnapshotEntity
    {
        [BsonId]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public ICollection<RankingBoardParticipantSnapshotEntity> RankingBoardParticipants { get; set; }
            = new List<RankingBoardParticipantSnapshotEntity>();
    }
}
