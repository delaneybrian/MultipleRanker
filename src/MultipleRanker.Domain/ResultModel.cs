using System;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Domain
{
    public class ResultModel
    {
        public Guid ResultId { get; private set; }

        public Guid Participant1Id { get; private set; }

        public Guid Participant2Id { get; private set; }

        public double Participant1Score { get; private set; }

        public double Participant2Score { get; private set; }

        public DateTime ResultTimeUtc { get; private set; }

        public void AddResult(
            Guid resultId,
            DateTime resultTimeUtc,
            Guid participant1Id,
            double participant1Score,
            Guid participant2Id,
            double participant2Score)
        {
            ResultId = resultId;
            ResultTimeUtc = resultTimeUtc;
            Participant1Id = participant1Id;
            Participant1Score = participant1Score;
            Participant2Id = participant2Id;
            Participant2Score = participant2Score;
        }

        private ResultModel(
            Guid resultId,
            Guid participant1Id,
            double participant1Score,
            Guid participant2Id,
            double participant2Score,
            DateTime resultTimeUtc)
        {
            ResultId = resultId;
            Participant1Id = participant1Id;
            Participant1Score = participant1Score;
            Participant2Id = participant2Id;
            Participant2Score = participant2Score;
            ResultTimeUtc = resultTimeUtc;
        }

        public static ResultModel For(
            Guid resultId,
            Guid participant1Id,
            double participant1Score,
            Guid participant2Id,
            double participant2Score,
            DateTime resultTimeUtc)
        {
            return new ResultModel(
                resultId,
                participant1Id,
                participant1Score,
                participant2Id,
                participant2Score,
                resultTimeUtc);
        }

        public static ResultModel For(RatingListResultSnapshot snapshot)
        {
            return new ResultModel(
                snapshot.ResultId,
                snapshot.Participant1Id,
                snapshot.Participant1Score,
                snapshot.Participant2Id,
                snapshot.Participant2Score,
                snapshot.ResultTimeUtc);
        }

        public RatingListResultSnapshot ToSnapshot()
        {
            return new RatingListResultSnapshot
            {
                ResultId = ResultId,
                Participant1Id = Participant1Id,
                Participant1Score = Participant1Score,
                Participant2Id = Participant2Id,
                Participant2Score = Participant2Score,
                ResultTimeUtc = ResultTimeUtc
            };
        }
    }
}
