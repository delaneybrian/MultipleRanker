using System;
using System.Collections.Generic;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Domain
{
    public class ParticipantRatingModel
    {
        public Guid Id { get; private set; }

        public int Index { get; private set; }

        public double TotalScoreFor { get; private set; }
        public double TotalScoreAgainst { get; private set; }
        public int TotalGamesPlayed { get; private set; }

        public IDictionary<Guid, double> TotalScoreByOpponentId { get; private set; } = new Dictionary<Guid, double>();

        public IDictionary<Guid, int> TotalWinsByOpponentId { get; private set; } = new Dictionary<Guid, int>();

        public IDictionary<Guid, double> TotalScoreConcededByOpponentId { get; private set; } = new Dictionary<Guid, double>();

        public IDictionary<Guid, int> TotalLosesByOpponentId { get; private set; } = new Dictionary<Guid, int>();

        public ParticipantRatingModel(Guid id, int index)
        {
            Id = id;
            Index = index;
        }

        public static ParticipantRatingModel For(RatingListParticipantSnapshot snapshot)
        {
            return new ParticipantRatingModel(snapshot);
        }

        public void AddResultVersus(Guid opponentId, double score, double opponentScore)
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
            Index = snapshot.Index;
            TotalScoreFor = snapshot.TotalScoreFor;
            TotalScoreAgainst = snapshot.TotalScoreAgainst;
            TotalScoreByOpponentId = snapshot.TotalScoreByOpponentId;
            TotalScoreConcededByOpponentId = snapshot.TotalScoreConcededByOpponentId;
            TotalLosesByOpponentId = snapshot.TotalLosesByOpponentId;
            TotalWinsByOpponentId = snapshot.TotalWinsByOpponentId;
        }

        private void AddOrUpdateDictionary<T>(
            IDictionary<Guid, T> toUpdate,
            Guid opponentId,
            Func<T, T> updateFunc,
            T addValue)
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
