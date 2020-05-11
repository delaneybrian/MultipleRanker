using System.Linq;
using AutoMapper;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Infrastructure.Repositories.Mongo.Entities;

namespace MultipleRanker.Infrastructure.Repositories.Mongo
{
    internal static class MongoRatingListSnapshotMapper
    {
        internal static IMapper _mapper;

        static MongoRatingListSnapshotMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RatingListSnapshot, RatingListSnapshotEntity>();

                cfg.CreateMap<RatingListSnapshotEntity, RatingListSnapshot>();

                cfg.CreateMap<RatingListParticipantSnapshot, RatingListParticipantSnapshotEntity>()
                    .ForMember(x => x.TotalLosesByOpponentId, opt
                        => opt.MapFrom(x => x.TotalLosesByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key.ToString(), Value = y.Value })))
                    .ForMember(x => x.TotalScoreByOpponentId, opt
                        => opt.MapFrom(x => x.TotalScoreByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key.ToString(), Value = y.Value })))
                    .ForMember(x => x.TotalScoreConcededByOpponentId, opt
                        => opt.MapFrom(x => x.TotalScoreConcededByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key.ToString(), Value = y.Value })))
                    .ForMember(x => x.TotalWinsByOpponentId, opt
                        => opt.MapFrom(x => x.TotalWinsByOpponentId.Select(y => new ValueByOpponentIdEntity { OpponentId = y.Key.ToString(), Value = y.Value })));

                cfg.CreateMap<RatingListParticipantSnapshotEntity, RatingListParticipantSnapshot>()
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

        internal static RatingListSnapshotEntity ToRatingListEntity(this RatingListSnapshot ratingListSnapshot)
        {
            return _mapper.Map<RatingListSnapshotEntity>(ratingListSnapshot);
        }

        internal static RatingListSnapshot ToRatingListSnapshot(this RatingListSnapshotEntity ratingListSnapshotEntity)
        {
            return _mapper.Map<RatingListSnapshot>(ratingListSnapshotEntity);
        }
    }
}
