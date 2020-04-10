using System;
using System.Collections.Generic;
using System.Linq;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Definitions.Commands;

namespace MultipleRanker.Domain
{
    public class RankingBoardModel : AggregateBase
    {
        private Guid _id;
        private List<ParticipantRankingModel> _participantRankingModels = new List<ParticipantRankingModel>();
        private long _matchUpsCompleted;

        public RankingBoardModel()
        {
            
        }

        private RankingBoardModel(RankingBoardSnapshot snapshot)
        {
            _id = snapshot.Id;
            _participantRankingModels = snapshot.RankingBoardParticipants
                .Select(participantSnapshot => ParticipantRankingModel.For(participantSnapshot))
                .ToList();
            _matchUpsCompleted = snapshot.MatchUpsCompleted;
        }

        public static RankingBoardModel For(RankingBoardSnapshot snapshot)
        {
            return new RankingBoardModel(snapshot);
        }

        public void Apply(CreateRankingBoardCommand cmd)
        {
            _id = cmd.Id;
        }

        public void Apply(MatchUpCompletedCommand cmd)
        {
            _matchUpsCompleted++;

            foreach (var matchUpParticipantScore in cmd.ParticipantScores)
            {
                var participantRankingModel = _participantRankingModels.Single(x => x.Id == matchUpParticipantScore.ParticipantId);

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

            _participantRankingModels.Add(participantRankingModel);
        }

        public RankingBoardSnapshot ToSnapshot()
        {
            return new RankingBoardSnapshot
            {
                Id = _id,
                RankingBoardParticipants = _participantRankingModels
                    .Select(rankingModel => rankingModel.ToSnapshot())
                    .ToList(),
                MatchUpsCompleted = _matchUpsCompleted
            };
        }
    }
}
