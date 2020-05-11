using System;
using System.Collections.Generic;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Domain
{
    public class ParticipantRatingModel
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int Index { get; private set; }

        public long TotalScoreFor { get; private set; }
        public long TotalScoreAgainst { get; private set; }
        public int TotalGamesPlayed { get; private set; }

        public IDictionary<Guid, int> TotalScoreByOpponentId { get; private set; } = new Dictionary<Guid, int>();

        public IDictionary<Guid, int> TotalWinsByOpponentId { get; private set; } = new Dictionary<Guid, int>();

        public IDictionary<Guid, int> TotalScoreConcededByOpponentId { get; private set; } = new Dictionary<Guid, int>();

        public IDictionary<Guid, int> TotalLosesByOpponentId { get; private set; } = new Dictionary<Guid, int>();

        public ParticipantRatingModel(Guid id, string name, int index)
        {
            Id = id;
            Name = name;
            Index = index;
        }

        public static ParticipantRatingModel For(RatingListParticipantSnapshot snapshot)
        {
            return new ParticipantRatingModel(snapshot);
        }

        public void AddResultVersus(Guid opponentId, int score, int opponentScore)
        {
            TotalGamesPlayed += 1;

            TotalScoreFor += score;
            TotalScoreAgainst += opponentScore;

            AddOrUpdateDictionary(
                TotalWinsByOpponentId,
                opponentId,
                (x) => x += score > opponentScore ? 1 : 0,
                score > opponentScore ? 1 : 0);

            AddOrUpdateDictionary(
                TotalLosesByOpponentId,
                opponentId,
                (x) => x += score < opponentScore ? 1 : 0,
                score < opponentScore ? 1 : 0);

            if (score == opponentScore)
                throw new NotImplementedException("Draws not yet supported");

            AddOrUpdateDictionary(TotalScoreByOpponentId, opponentId, (x) => x += score, score);

            AddOrUpdateDictionary(TotalScoreConcededByOpponentId, opponentId, (x) => x += opponentScore, opponentScore);
        }

        public RatingListParticipantSnapshot ToSnapshot()
        {
            return new RatingListParticipantSnapshot
            {
                Id = Id,
                Name = Name,
                Index = Index,
                TotalGamesPlayed = TotalGamesPlayed,
                TotalScoreFor = TotalScoreFor,
                TotalScoreAgainst = TotalScoreAgainst,
                TotalWinsByOpponentId = TotalWinsByOpponentId,
                TotalLosesByOpponentId = TotalLosesByOpponentId,
                TotalScoreByOpponentId = TotalScoreByOpponentId,
                TotalScoreConcededByOpponentId = TotalScoreConcededByOpponentId
            };
        }

        private ParticipantRatingModel(RatingListParticipantSnapshot snapshot)
        {
            Id = snapshot.Id;
            Name = snapshot.Name;
            Index = snapshot.Index;
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
