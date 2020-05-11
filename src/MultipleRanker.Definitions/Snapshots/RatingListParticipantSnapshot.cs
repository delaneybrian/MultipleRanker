using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MultipleRanker.Definitions.Snapshots
{
    [DataContract]
    public class RatingListParticipantSnapshot
    {
        [DataMember]
        public Guid Id { get; set; }


        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public double TotalScoreFor { get; set; }

        [DataMember]
        public double TotalScoreAgainst { get; set; }

        [DataMember]
        public int TotalGamesPlayed { get; set; }

        [DataMember]
        public IDictionary<Guid, double> TotalScoreByOpponentId =
            new Dictionary<Guid, double>();

        [DataMember]
        public IDictionary<Guid, int> TotalWinsByOpponentId =
            new Dictionary<Guid, int>();

        [DataMember]
        public IDictionary<Guid, double> TotalScoreConcededByOpponentId =
            new Dictionary<Guid, double>();

        [DataMember]
        public IDictionary<Guid, int> TotalLosesByOpponentId =
            new Dictionary<Guid, int>();
    }
}
