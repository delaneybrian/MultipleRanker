using System;
using System.Collections.Generic;
using System.Linq;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Domain
{
    public class RatingBoardModel
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

        private RatingBoardModel(RatingBoardSnapshot snapshot)
        {
            Id = snapshot.Id;
            ParticipantRatingModels = snapshot.RatingBoardParticipants
                .Select(participantSnapshot => ParticipantRatingModel.For(participantSnapshot))
                .ToList();
            _matchUpsCompleted = snapshot.MatchUpsCompleted;
            _lastRatingCalculatedAt = snapshot.LastRatingCalculatedAt;
            _numberOfRatingsPerformed = snapshot.NumberOfRatingsPerformed;
            _maxParticipantIndex = snapshot.MaxParticipantIndex;
        }

        public static RatingBoardModel For(RatingBoardSnapshot snapshot)
        {
            return new RatingBoardModel(snapshot);
        }

        public void Apply(CreateRatingBoard evt)
        {
            Id = evt.Id;
        }

        public void Apply(GenerateRatingsForRatingBoard evt)
        {
            //todo remove dependency on DateTime here :-(
            _lastRatingCalculatedAt = DateTime.UtcNow;
            _numberOfRatingsPerformed++;
        }

        public void Apply(MatchUpCompleted evt)
        {
            _matchUpsCompleted++;

            foreach (var matchUpParticipantScore in evt.ParticipantScores)
            {
                var participantRankingModel = ParticipantRatingModels.Single(x => x.Id == matchUpParticipantScore.ParticipantId);

                foreach (var opponentMatchUpParticipantScore in evt.ParticipantScores
                    .Where(x => x.ParticipantId != matchUpParticipantScore.ParticipantId))
                {
                    participantRankingModel.AddResultVersus(
                        opponentMatchUpParticipantScore.ParticipantId, 
                        matchUpParticipantScore.PointsScored, 
                        opponentMatchUpParticipantScore.PointsScored);
                }
            }
        }

        public void Apply(AddParticipantToRatingBoard evt)
        {
            var participantRankingModel = new ParticipantRatingModel(evt.ParticipantId, evt.ParticipantName, _maxParticipantIndex);

            _maxParticipantIndex++;

            ParticipantRatingModels.Add(participantRankingModel);
        }

        public RatingBoardSnapshot ToSnapshot()
        {
            return new RatingBoardSnapshot
            {
                Id = Id,
                RatingBoardParticipants = ParticipantRatingModels
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
