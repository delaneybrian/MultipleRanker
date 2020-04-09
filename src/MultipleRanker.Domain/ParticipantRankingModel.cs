using System;
using System.Collections.Generic;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Domain
{
    public class ParticipantRankingModel
    {
        private Guid _id;
        private string _name;

        private long _totalScoreFor;
        private long _totalScoreAgainst;

        private int _totalGamesPlayed;

        private IDictionary<Guid, int> _totalScoreByOpponentId = new Dictionary<Guid, int>();
        private IDictionary<Guid, int> _totalWinsByOpponentId = new Dictionary<Guid, int>();
        private IDictionary<Guid, int> _totalScoreConcededByOpponentId = new Dictionary<Guid, int>();
        private IDictionary<Guid, int> _totalLosesByOpponentId = new Dictionary<Guid, int>();

        public ParticipantRankingModel()
        {
            
        }

        public static ParticipantRankingModel For(RankingBoardParticipantSnapshot snapshot)
        {
            return new ParticipantRankingModel(snapshot);
        }

        public void AddResultVersus(Guid opponentId, int score, int opponentScore)
        {
            _totalGamesPlayed += 1;

            _totalScoreFor += score;
            _totalScoreAgainst += score;

            if (score > opponentScore)
                AddOrUpdateDictionary(_totalWinsByOpponentId, opponentId, (x) => x += 1, 1);

            if (score < opponentScore)
                AddOrUpdateDictionary(_totalLosesByOpponentId, opponentId, (x) => x += 1, 1);

            if(score == opponentScore)
                throw new NotImplementedException("Draws not yet supported");

            AddOrUpdateDictionary(_totalScoreByOpponentId, opponentId, (x) => x += score, score);

            AddOrUpdateDictionary(_totalScoreConcededByOpponentId, opponentId, (x) => x += opponentScore, opponentScore);

        }

        public RankingBoardParticipantSnapshot ToSnapshot()
        {
            return new RankingBoardParticipantSnapshot
            {
                Id = _id,
                Name = _name,
                TotalGamesPlayed = _totalGamesPlayed,
                TotalScoreFor = _totalScoreFor,
                TotalScoreAgainst = _totalScoreAgainst,
                TotalWinsByOpponentId = _totalWinsByOpponentId,
                TotalLosesByOpponentId = _totalLosesByOpponentId,
                TotalScoreByOpponentId = _totalScoreByOpponentId,
                TotalScoreConcededByOpponentId = _totalScoreConcededByOpponentId
            };
        }

        private ParticipantRankingModel(RankingBoardParticipantSnapshot snapshot)
        {
            _id = snapshot.Id;
            _name = snapshot.Name;
            _totalScoreFor = snapshot.TotalScoreFor;
            _totalScoreAgainst = snapshot.TotalScoreAgainst;

            _totalScoreByOpponentId = snapshot.TotalScoreByOpponentId;
            _totalScoreConcededByOpponentId = snapshot.TotalScoreConcededByOpponentId;
            _totalLosesByOpponentId = snapshot.TotalLosesByOpponentId;
            _totalWinsByOpponentId = snapshot.TotalWinsByOpponentId;
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
