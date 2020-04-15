using System;
using System.Collections.Generic;
using System.Linq;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Definitions.Commands;

namespace MultipleRanker.Domain
{
    public class RatingBoardModel : AggregateBase
    {
        public Guid Id { get; private set; }
        public List<ParticipantRatingModel> ParticipantRatingModels { get; private set; } = new List<ParticipantRatingModel>();
        private long _matchUpsCompleted;
        private long _numberOfRatingsPerformed;
        private DateTime _lastRatingCalculatedAt;
        private int _maxParticipantIndex;

        public RatingBoardModel()
        {
            
        }

        private RatingBoardModel(RankingBoardSnapshot snapshot)
        {
            Id = snapshot.Id;
            ParticipantRankingModels = snapshot.RankingBoardParticipants
                .Select(participantSnapshot => ParticipantRatingModel.For(participantSnapshot))
                .ToList();
            _matchUpsCompleted = snapshot.MatchUpsCompleted;
            _lastRatingCalculatedAt = snapshot.LastRatingCalculatedAt;
            _numberOfRatingsPerformed = snapshot.NumberOfRatingsPerformed;
            _maxParticipantIndex = snapshot.MaxParticipantIndex;
        }

        public static RatingBoardModel For(RankingBoardSnapshot snapshot)
        {
            return new RatingBoardModel(snapshot);
        }

        public void Apply(CreateRankingBoardCommand cmd)
        {
            Id = cmd.Id;
        }

        public void Apply(GenerateRatingsForRankingBoardCommand cmd)
        {
            //todo remove dependency on DateTime here :-(
            _lastRatingCalculatedAt = DateTime.UtcNow;
            _numberOfRatingsPerformed++;
        }

        public void Apply(MatchUpCompletedCommand cmd)
        {
            _matchUpsCompleted++;

            foreach (var matchUpParticipantScore in cmd.ParticipantScores)
            {
                var participantRankingModel = ParticipantRatingModels.Single(x => x.Id == matchUpParticipantScore.ParticipantId);

                foreach (var opponentMatchUpParticipantScore in cmd.ParticipantScores
                    .Where(x => x.ParticipantId != matchUpParticipantScore.ParticipantId))
                {
                    participantRankingModel.AddResultVersus(
                        opponentMatchUpParticipantScore.ParticipantId, 
                        matchUpParticipantScore.PointsScored, 
                        opponentMatchUpParticipantScore.PointsScored);
                }
            }
        }

        public void Apply(AddParticipantToRankingBoardCommand cmd)
        {
            var participantRankingModel = new ParticipantRatingModel(cmd.ParticipantId, cmd.ParticipantName, _maxParticipantIndex);

            _maxParticipantIndex++;

            ParticipantRatingModels.Add(participantRankingModel);
        }

        public RankingBoardSnapshot ToSnapshot()
        {
            return new RankingBoardSnapshot
            {
                Id = Id,
                RankingBoardParticipants = ParticipantRatingModels
                    .Select(rankingModel => rankingModel.ToSnapshot())
                    .ToList(),
                MatchUpsCompleted = _matchUpsCompleted,
                NumberOfRatingsPerformed = _numberOfRatingsPerformed,
                LastRatingCalculatedAt = _lastRatingCalculatedAt,
                MaxParticipantIndex = _maxParticipantIndex
            };
        }
    }
}
