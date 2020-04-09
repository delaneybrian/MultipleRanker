﻿using System;
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
        public IDictionary<Guid, int> TotalScoreByOpponentId =
            new Dictionary<Guid, int>();

        [DataMember]
        public IDictionary<Guid, int> TotalWinsByOpponentId =
            new Dictionary<Guid, int>();

        [DataMember]
        public IDictionary<Guid, int> TotalScoreConcededByOpponentId =
            new Dictionary<Guid, int>();

        [DataMember]
        public IDictionary<Guid, int> TotalLosesByOpponentId =
            new Dictionary<Guid, int>();
    }
}
