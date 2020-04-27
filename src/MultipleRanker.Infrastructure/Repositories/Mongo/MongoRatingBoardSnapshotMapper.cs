using System.Linq;
using AutoMapper;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Infrastructure.Repositories.Mongo.Entities;

namespace MultipleRanker.Infrastructure.Repositories.Mongo
{
    internal static class MongoRatingBoardSnapshotMapper
    {
        internal static IMapper _mapper;

        static MongoRatingBoardSnapshotMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RatingBoardSnapshot, RatingBoardSnapshotEntity>();

                cfg.CreateMap<RatingBoardSnapshotEntity, RatingBoardSnapshot>();

                cfg.CreateMap<RatingBoardParticipantSnapshot, RatingBoardParticipantSnapshotEntity>()
                    .ForMember(x => x.TotalLosesByOpponentId, opt
                        => opt.MapFrom(x => x.TotalLosesByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key.ToString(), Value = y.Value })))
                    .ForMember(x => x.TotalScoreByOpponentId, opt
                        => opt.MapFrom(x => x.TotalScoreByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key.ToString(), Value = y.Value })))
                    .ForMember(x => x.TotalScoreConcededByOpponentId, opt
                        => opt.MapFrom(x => x.TotalScoreConcededByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key.ToString(), Value = y.Value })))
                    .ForMember(x => x.TotalWinsByOpponentId, opt
                        => opt.MapFrom(x => x.TotalWinsByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key.ToString(), Value = y.Value })));

                cfg.CreateMap<RatingBoardParticipantSnapshotEntity, RatingBoardParticipantSnapshot>()
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

        internal static RatingBoardSnapshotEntity ToRatingBoardEntity(this RatingBoardSnapshot ratingBoardSnapshot)
        {
            return _mapper.Map<RatingBoardSnapshotEntity>(ratingBoardSnapshot);
        }

        internal static RatingBoardSnapshot ToRatingBoardSnapshot(this RatingBoardSnapshotEntity ratingBoardSnapshotEntity)
        {
            return _mapper.Map<RatingBoardSnapshot>(ratingBoardSnapshotEntity);
        }
    }
}
