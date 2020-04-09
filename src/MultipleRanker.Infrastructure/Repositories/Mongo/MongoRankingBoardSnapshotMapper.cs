using AutoMapper;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Infrastructure.Repositories.Mongo.Entities;

namespace MultipleRanker.Infrastructure.Repositories.Mongo
{
    internal static class MongoRankingBoardSnapshotMapper
    {
        internal static IMapper _mapper;

        static MongoRankingBoardSnapshotMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RankingBoardSnapshot, RankingBoardSnapshotEntity>();
                cfg.CreateMap<RankingBoardParticipantSnapshotEntity, RankingBoardSnapshot>();
            });

            _mapper = config.CreateMapper();

        }

        internal static RankingBoardSnapshotEntity ToRankingBoardEntity(this RankingBoardSnapshot rankingBoardSnapshot)
        {
            return _mapper.Map<RankingBoardSnapshotEntity>(rankingBoardSnapshot);
        }

        internal static RankingBoardSnapshot ToRankingBoardSnapshot(this RankingBoardSnapshotEntity rankingBoardSnapshotEntity)
        {
            return _mapper.Map<RankingBoardSnapshot>(rankingBoardSnapshotEntity);
        }
    }
}
