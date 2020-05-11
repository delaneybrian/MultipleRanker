using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MultipleRanker.Infrastructure.Repositories.Mongo.Entities
{
    [DataContract]
    internal class RatingListParticipantSnapshotEntity
    {
        [BsonId]
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public long TotalScoreFor { get; set; }

        [DataMember]
        public long TotalScoreAgainst { get; set; }

        [DataMember]
        public int TotalGamesPlayed { get; set; }

        [DataMember]
        public ICollection<ValueByOpponentIdEntity<double>> TotalScoreByOpponentId =
            new List<ValueByOpponentIdEntity<double>>();

        [DataMember]
        public ICollection<ValueByOpponentIdEntity<int>> TotalWinsByOpponentId =
            new List<ValueByOpponentIdEntity<int>>();

        [DataMember]
        public ICollection<ValueByOpponentIdEntity<double>> TotalScoreConcededByOpponentId =
            new List<ValueByOpponentIdEntity<double>>();

        [DataMember]
        public ICollection<ValueByOpponentIdEntity<int>> TotalLosesByOpponentId =
            new List<ValueByOpponentIdEntity<int>>();
    }
}
