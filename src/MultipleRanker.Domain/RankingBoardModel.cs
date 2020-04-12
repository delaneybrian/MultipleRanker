using System;
using System.Collections.Generic;
using System.Linq;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Definitions.Commands;

namespace MultipleRanker.Domain
{
    public class RankingBoardModel : AggregateBase
    {
        public Guid Id { get; private set; }
        public List<ParticipantRankingModel> ParticipantRankingModels { get; private set; } = new List<ParticipantRankingModel>();
        private long _matchUpsCompleted;
        private long _numberOfRatingsPerformed;
        private DateTime _lastRatingCalculatedAt;

        public RankingBoardModel()
        {
            
        }

        private RankingBoardModel(RankingBoardSnapshot snapshot)
        {
            Id = snapshot.Id;
            ParticipantRankingModels = snapshot.RankingBoardParticipants
                .Select(participantSnapshot => ParticipantRankingModel.For(participantSnapshot))
                .ToList();
            _matchUpsCompleted = snapshot.MatchUpsCompleted;
            _lastRatingCalculatedAt = snapshot.LastRatingCalculatedAt;
            _numberOfRatingsPerformed = snapshot.NumberOfRatingsPerformed;
        }

        public static RankingBoardModel For(RankingBoardSnapshot snapshot)
        {
            return new RankingBoardModel(snapshot);
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
                var participantRankingModel = ParticipantRankingModels.Single(x => x.Id == matchUpParticipantScore.ParticipantId);

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
            var participantRankingModel = new ParticipantRankingModel(cmd.ParticipantId, cmd.ParticipantName);

            ParticipantRankingModels.Add(participantRankingModel);
        }

        public RankingBoardSnapshot ToSnapshot()
        {
            return new RankingBoardSnapshot
            {
                Id = Id,
                RankingBoardParticipants = ParticipantRankingModels
                    .Select(rankingModel => rankingModel.ToSnapshot())
                    .ToList(),
                MatchUpsCompleted = _matchUpsCompleted,
                NumberOfRatingsPerformed = _numberOfRatingsPerformed,
                LastRatingCalculatedAt = _lastRatingCalculatedAt
            };
        }
    }
}
