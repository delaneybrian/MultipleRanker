﻿using System.Linq;
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

                cfg.CreateMap<RankingBoardSnapshotEntity, RankingBoardSnapshot>();

                cfg.CreateMap<RankingBoardParticipantSnapshot, RankingBoardParticipantSnapshotEntity>()
                    .ForMember(x => x.TotalLosesByOpponentId, opt
                        => opt.MapFrom(x => x.TotalLosesByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key, Value = y.Value })))
                    .ForMember(x => x.TotalScoreByOpponentId, opt
                        => opt.MapFrom(x => x.TotalScoreByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key, Value = y.Value })))
                    .ForMember(x => x.TotalScoreConcededByOpponentId, opt
                        => opt.MapFrom(x => x.TotalScoreConcededByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key, Value = y.Value })))
                    .ForMember(x => x.TotalWinsByOpponentId, opt
                        => opt.MapFrom(x => x.TotalWinsByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key, Value = y.Value })));

                cfg.CreateMap<RankingBoardParticipantSnapshotEntity, RankingBoardParticipantSnapshot>()
                    .ForMember(x => x.TotalLosesByOpponentId, opt
                        => opt.MapFrom(x => x.TotalLosesByOpponentId.ToDictionary(k => k.OpponentId, v => v.Value)))
                    .ForMember(x => x.TotalScoreByOpponentId, opt
                        => opt.MapFrom(x => x.TotalScoreByOpponentId.ToDictionary(k => k.OpponentId, v => v.Value)))
                    .ForMember(x => x.TotalScoreConcededByOpponentId, opt
                        => opt.MapFrom(x => x.TotalScoreConcededByOpponentId.ToDictionary(k => k.OpponentId, v => v.Value)))
                    .ForMember(x => x.TotalWinsByOpponentId, opt
                        => opt.MapFrom(x => x.TotalWinsByOpponentId.ToDictionary(k => k.OpponentId, v => v.Value)));
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
