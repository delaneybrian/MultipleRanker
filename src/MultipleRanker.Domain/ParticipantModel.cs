using System;
using System.Collections.Generic;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Domain
{
    public class ParticipantModel
    {
        private Guid _id;
        private string _name;

        private long _totalScoreFor;
        private long _totalScoreAgainst;

        private int _gamesPlayed;

        private int _averageScoreFor;
        private int _averageScoreAgainst;

        private IDictionary<Guid, int> _totalScoreByOpponentId = new Dictionary<Guid, int>();
        private IDictionary<Guid, int> _totalWinsByOpponentId = new Dictionary<Guid, int>();
        private IDictionary<Guid, int> _totalScoreConcededByOpponentId = new Dictionary<Guid, int>();
        private IDictionary<Guid, int> _totalLosesByOpponentId = new Dictionary<Guid, int>();

        public ParticipantModel()
        {
            
        }

        private ParticipantModel(ParticipantSnapshot snapshot)
        {
            
        }

        public static ParticipantModel For(ParticipantSnapshot snapshot)
        {
            return new ParticipantModel(snapshot);
        }

        public void AddResultVersus(Guid opponentId, int score, int opponentScore)
        {
            _gamesPlayed += 1;

            _totalScoreFor += score;
            _totalScoreAgainst += score;

            UpdateDict(_totalLosesByOpponentId, opponentId, (x) => x += 1, 1);


            if (_totalLosesByOpponentId.TryGetValue(opponentId, out var totalLosses))
            {
                totalLosses += 1;
            }
        }

        public ParticipantSnapshot ToSnapshot()
        {
            return new ParticipantSnapshot();
        }

        private void UpdateDict(
            IDictionary<Guid, int> toUpdate, 
            Guid opponentId, 
            Func<int, int> updateAction, 
            int addValue)
        {
            if (toUpdate.TryGetValue(opponentId, out var value))
            {
                var newValue = updateAction(value);
                toUpdate[opponentId] = newValue;
            }
            else
            {
                toUpdate.Add(opponentId, addValue);
            }
        }
    }
}
