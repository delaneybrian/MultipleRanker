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
        private List<ParticipantSnapshot> _participantSnapshots;

        public RankingBoardModel()
        {
            
        }

        private RankingBoardModel(RankingBoardSnapshot snapshot)
        {
            _id = snapshot.Id;
            _participantSnapshots = snapshot.Participants.ToList();
        }

        public static RankingBoardModel For(RankingBoardSnapshot snapshot)
        {
            return new RankingBoardModel(snapshot);
        }

        public void Apply(CreateRankingBoardCommand cmd)
        {
            _id = cmd.Id;
        }

        public void Apply(AddParticipantToRankingBoardCommand cmd)
        {
            _participantSnapshots.Add(new ParticipantSnapshot
            {
                Id = cmd.ParticipantId,
                Name = cmd.ParticipantName
            });
        }

        public RankingBoardSnapshot ToSnapshot()
        {
            return new RankingBoardSnapshot
            {
                Id = _id,
                Participants = _participantSnapshots
            };
        }
    }
}
