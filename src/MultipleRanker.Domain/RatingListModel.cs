using System;
using System.Collections.Generic;
using System.Linq;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Definitions;
using MultipleRanker.RankerApi.Contracts.Events;

namespace MultipleRanker.Domain
{
    public class RatingListModel
    {
        public Guid Id { get; private set; }
        public List<ParticipantRatingModel> ParticipantRatingModels { get; private set; } = new List<ParticipantRatingModel>();

        public List<ResultModel> AppliedResults { get; private set;  }

        private long _numberOfResults;
        private long _numberOfRatingsPerformed;
        private DateTime _lastRatingCalculatedAt;
        private int _maxParticipantIndex;
        private RatingType _ratingType;
        private RatingAggregationType _ratingAggregationType;

        public RatingListModel()
        {
            
        }

        private RatingListModel(RatingListSnapshot snapshot)
        {
            Id = snapshot.Id;
            ParticipantRatingModels = snapshot.RatingListParticipants
                .Select(participantSnapshot => ParticipantRatingModel.For(participantSnapshot))
                .ToList();
            _numberOfResults = snapshot.NumberOfResults;
            _lastRatingCalculatedAt = snapshot.LastRatingCalculatedAt;
            _numberOfRatingsPerformed = snapshot.NumberOfRatingsPerformed;
            _maxParticipantIndex = snapshot.MaxParticipantIndex;
            _ratingType = snapshot.RatingType;
            _ratingAggregationType = snapshot.RatingAggregationType;
            AppliedResults = snapshot.RatingListResults
                .Select(x => )
        }

        public static RatingListModel For(RatingListSnapshot snapshot)
        {
            return new RatingListModel(snapshot);
        }

        public void Apply(RatingListCreated evt)
        {
            Id = evt.RatingListId;

            _ratingType = evt.RatingType.ToRatingType();
            _ratingAggregationType = evt.RatingAggregation.ToRatingAggregationType();
        }

        public void Apply(GenerateRatings evt)
        {
            //todo remove dependency on DateTime here :-(
            _lastRatingCalculatedAt = DateTime.UtcNow;
            _numberOfRatingsPerformed++;
        }

        public void Apply(ResultAdded evt)
        {
            _numberOfResults++;

            foreach (var participantResult in evt.ParticipantResults)
            {
                var participantRatingModel = ParticipantRatingModels.Single(x => x.Id == participantResult.ParticipantId);

                foreach (var opponentResult in evt.ParticipantResults
                    .Where(x => x.ParticipantId != participantResult.ParticipantId))
                {
                    participantRatingModel.AddResultVersus(
                        opponentResult.ParticipantId,
                        participantResult.Score,
                        opponentResult.Score);
                }
            }
        }

        public void Apply(ParticipantAddedToRatingList evt)
        {
            var participantRatingModel = new ParticipantRatingModel(evt.ParticipantId, _maxParticipantIndex);

            _maxParticipantIndex++;

            ParticipantRatingModels.Add(participantRatingModel);
        }

        public RatingListSnapshot ToSnapshot()
        {
            return new RatingListSnapshot
            {
                Id = Id,
                RatingListParticipants = ParticipantRatingModels
                    .Select(rankingModel => rankingModel.ToSnapshot())
                    .ToList(),
                NumberOfResults = _numberOfResults,
                NumberOfRatingsPerformed = _numberOfRatingsPerformed,
                LastRatingCalculatedAt = _lastRatingCalculatedAt,
                MaxParticipantIndex = _maxParticipantIndex,
                RatingType = _ratingType,
                RatingAggregationType = _ratingAggregationType
            };
        }
    }
}
