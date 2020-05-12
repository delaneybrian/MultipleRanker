using System;
using System.Collections.Generic;
using System.Linq;
using MultipleRanker.Contracts;
using MultipleRanker.Definitions;

namespace MultipleRanker.Domain.Raters.Raters
{
    public class EloRater : IRater
    {
        private const int StartRating = 1500;

        //controls the volitiliy of swings caused by upsets
        //could increase with importance of the game
        private const int KValue = 32;

        private const int LogisticParameter = 1000;

        private Dictionary<Guid, double> CurrentRatingsByTeamId = new Dictionary<Guid, double>();

        public bool IsFor(RatingType ratingType)
        {
            return ratingType == RatingType.Elo;
        }

        public IEnumerable<ParticipantRating> Rate(RatingListModel ratingBoardModel)
        {
            foreach (var participantRatingModels in ratingBoardModel.ParticipantRatingModels)
            {
                CurrentRatingsByTeamId.Add(participantRatingModels.Id, StartRating);
            }

            foreach (var appliedResult in ratingBoardModel.AppliedResults
                .OrderBy(x => x.ResultTimeUtc))
            {
                var participant1OldRating = CurrentRatingsByTeamId[appliedResult.Participant1Id];

                var participant2OldRating = CurrentRatingsByTeamId[appliedResult.Participant2Id];

                var participant1EloScore = (appliedResult.Participant1Score + 1) / 
                                           (appliedResult.Participant1Score + appliedResult.Participant2Score + 2);

                var participant2EloScore = (appliedResult.Participant2Score + 1) /
                                           (appliedResult.Participant2Score + appliedResult.Participant1Score + 2);

                var differenceParticipant1V2 = participant1OldRating - participant2OldRating;

                var differenceParticipant2V1 = participant2OldRating - participant1OldRating;

                var participant1Mu = 1 / (1 + Math.Pow(10, ((-1 * (double) differenceParticipant1V2)) / LogisticParameter));

                var participant2Mu = 1 / (1 + Math.Pow(10, ((-1 * (double) differenceParticipant2V1)) / LogisticParameter));

                var participant1NewRating = participant1OldRating + KValue * (participant1EloScore - participant1Mu);

                var participant2NewRating = participant2OldRating + KValue * (participant2EloScore - participant2Mu);

                CurrentRatingsByTeamId[appliedResult.Participant1Id] = participant1NewRating;
                CurrentRatingsByTeamId[appliedResult.Participant2Id] = participant2NewRating;
            }

            var generatedRatings = new List<ParticipantRating>();

            foreach (var ratingByTeamId in CurrentRatingsByTeamId)
            {
                generatedRatings.Add(new ParticipantRating
                {
                    ParticipantId = ratingByTeamId.Key,
                    Rating = ratingByTeamId.Value
                });
            }

            return generatedRatings;
        }
    }
}
