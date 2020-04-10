using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MultipleRanker.Infrastructure.Repositories.Mongo.Entities
{
    [DataContract]
    internal class RankingBoardParticipantSnapshotEntity
    {
        [BsonId]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public long TotalScoreFor { get; set; }

        [DataMember]
        public long TotalScoreAgainst { get; set; }

        [DataMember]
        public int TotalGamesPlayed { get; set; }

        [DataMember]
        public ICollection<ValueByOpponentIdEntity> TotalScoreByOpponentId =
            new List<ValueByOpponentIdEntity>();

        [DataMember]
        public ICollection<ValueByOpponentIdEntity> TotalWinsByOpponentId =
            new List<ValueByOpponentIdEntity>();

        [DataMember]
        public ICollection<ValueByOpponentIdEntity> TotalScoreConcededByOpponentId =
            new List<ValueByOpponentIdEntity>();

        [DataMember]
        public ICollection<ValueByOpponentIdEntity> TotalLosesByOpponentId =
            new List<ValueByOpponentIdEntity>();
    }
}
