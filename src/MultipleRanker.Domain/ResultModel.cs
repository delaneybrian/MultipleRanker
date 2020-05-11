using System;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Domain
{
    public class ResultModel
    {
        private Guid _resultId;

        private Guid _participant1Id;

        private Guid _participant2Id;

        private double _participant1Score;

        private double _participant2Score;

        private DateTime _resultTimeUtc;

        public void AddResult(
            Guid resultId,
            DateTime resultTimeUtc,
            Guid participant1Id,
            double participant1Score,
            Guid participant2Id,
            double participant2Score)
        {
            _resultId = resultId;
            _resultTimeUtc = resultTimeUtc;
            _participant1Id = participant1Id;
            _participant1Score = participant1Score;
            _participant2Id = participant2Id;
            _participant2Score = participant2Score;
        }
     
        public static ResultModel For(RatingListResultSnapshot snapshot)
        {
            _resultId = snapshot.ResultId;
            _participant1Id = snapshot.Participant1Id;
            _participant1Score = snapshot.Participant1Score;
            _participant2Id = snapshot.Participant2Id;
            _participant2Score = snapshot.Participant2Score;
            _resultTimeUtc = snapshot.ResultTimeUtc;
        }

        public RatingListResultSnapshot ToSnapshot()
        {
            return new RatingListResultSnapshot
            {
                ResultId = _resultId,
                Participant1Id = _participant1Id,
                Participant1Score = _participant1Score,
                Participant2Id = _participant2Id,
                Participant2Score = _participant2Score,
                ResultTimeUtc = _resultTimeUtc
            };
        }
    }
}
