using System;
using System.Collections.Generic;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Domain
{
    public class ParticipantRankingModel
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public long TotalScoreFor { get; private set; }
        public long TotalScoreAgainst { get; private set; }
        public int TotalGamesPlayed { get; private set; }

        public IDictionary<Guid, int> TotalScoreByOpponentId { get; private set; } = new Dictionary<Guid, int>();
        public IDictionary<Guid, int> TotalWinsByOpponentId { get; private set; } = new Dictionary<Guid, int>();
        public IDictionary<Guid, int> TotalScoreConcededByOpponentId { get; private set; } = new Dictionary<Guid, int>();
        public IDictionary<Guid, int> TotalLosesByOpponentId { get; private set; } = new Dictionary<Guid, int>();

        public ParticipantRankingModel(Guid id, string name)
        {
            Id = id;
           Name = name;
        }

        public static ParticipantRankingModel For(RankingBoardParticipantSnapshot snapshot)
        {
            return new ParticipantRankingModel(snapshot);
        }

        public void AddResultVersus(Guid opponentId, int score, int opponentScore)
        {
            TotalGamesPlayed += 1;

            TotalScoreFor += score;
            TotalScoreAgainst += opponentScore;

            if (score > opponentScore)
                AddOrUpdateDictionary(TotalWinsByOpponentId, opponentId, (x) => x += 1, 1);

            if (score < opponentScore)
                AddOrUpdateDictionary(TotalLosesByOpponentId, opponentId, (x) => x += 1, 1);

            if(score == opponentScore)
                throw new NotImplementedException("Draws not yet supported");

            AddOrUpdateDictionary(TotalScoreByOpponentId, opponentId, (x) => x += score, score);

            AddOrUpdateDictionary(TotalScoreConcededByOpponentId, opponentId, (x) => x += opponentScore, opponentScore);

        }

        public RankingBoardParticipantSnapshot ToSnapshot()
        {
            return new RankingBoardParticipantSnapshot
            {
                Id = Id,
                Name = Name,
                TotalGamesPlayed = TotalGamesPlayed,
                TotalScoreFor = TotalScoreFor,
                TotalScoreAgainst = TotalScoreAgainst,
                TotalWinsByOpponentId = TotalWinsByOpponentId,
                TotalLosesByOpponentId = TotalLosesByOpponentId,
                TotalScoreByOpponentId = TotalScoreByOpponentId,
                TotalScoreConcededByOpponentId = TotalScoreConcededByOpponentId
            };
        }

        private ParticipantRankingModel(RankingBoardParticipantSnapshot snapshot)
        {
            Id = snapshot.Id;
            Name = snapshot.Name;
            TotalScoreFor = snapshot.TotalScoreFor;
            TotalScoreAgainst = snapshot.TotalScoreAgainst;

            TotalScoreByOpponentId = snapshot.TotalScoreByOpponentId;
            TotalScoreConcededByOpponentId = snapshot.TotalScoreConcededByOpponentId;
            TotalLosesByOpponentId = snapshot.TotalLosesByOpponentId;
            TotalWinsByOpponentId = snapshot.TotalWinsByOpponentId;
        }

        private void AddOrUpdateDictionary(
            IDictionary<Guid, int> toUpdate, 
            Guid opponentId, 
            Func<int, int> updateFunc, 
            int addValue)
        {
            if (toUpdate.TryGetValue(opponentId, out var value))
            {
                var newValue = updateFunc(value);
                toUpdate[opponentId] = newValue;
            }
            else
            {
                toUpdate.Add(opponentId, addValue);
            }
        }
    }
}
