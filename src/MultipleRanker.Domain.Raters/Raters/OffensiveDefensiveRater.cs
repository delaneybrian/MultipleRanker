using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MultipleRanker.Contracts;

namespace MultipleRanker.Domain.Raters
{
    public class OffensiveDefensiveRaters : IRater
    {
        private Vector<double> _defensiveRatings;

        private Vector<double> _offensiveRatings;

        private Vector<double> _previousDefensiveRatings;

        private Vector<double> _previousOffensiveRatings;

        private Matrix<double> _results;

        public bool IsFor(RaterType rankerType)
        {
            return rankerType == RaterType.OffensiveDefensive;
        }

        public IEnumerable<ParticipantRating> Rate(RatingBoardModel ratingBoardModel)
        {
            var numParticipants = ratingBoardModel.ParticipantRatingModels.Count();

            _results = GenerateTotalScoreMatrix(ratingBoardModel);

            _defensiveRatings = Vector<double>.Build.Dense(_results.ColumnCount, 1);

            _offensiveRatings = Vector<double>.Build.Dense(_results.ColumnCount, 1);

            _previousDefensiveRatings = Vector<double>.Build.Dense(_results.ColumnCount, 1);

            _previousOffensiveRatings = Vector<double>.Build.Dense(_results.ColumnCount, 1);

            Solve();

            var finalRatings = CombineRatings(numParticipants);

            return CreateRatingResults(ratingBoardModel, finalRatings);
        }

        private IEnumerable<ParticipantRating> CreateRatingResults(RatingBoardModel rankingBoardModel, Vector<double> finalRatings)
        {
            var i = 0;
            foreach(var participant in rankingBoardModel.ParticipantRatingModels)
            {
                yield return new ParticipantRating
                {
                    ParticipantId = participant.Id,
                    Rating = finalRatings[i]
                };

                i++;
            }
        }

        private Vector<double> CombineRatings(int numParticipants)
        {
            var finalRatings = Vector<double>.Build.Dense(_defensiveRatings.Count);

            for (var i = 0; i < numParticipants; i++)
            {
                finalRatings[i] = _offensiveRatings[i] / _defensiveRatings[i];
            }

            return finalRatings;
        }

        private void Solve()
        {
            var iterationNumber = 0;

            do
            {
                StoreLast();

                //offensive calculations
                for (var j = 0; j < _results.RowCount; j++)
                {
                    var newOffensiveRating = 0D;

                    for (var i = 0; i < _results.ColumnCount; i++)
                    {
                        var score = _results[i, j];

                        var defensiveRating = _defensiveRatings[i];

                        newOffensiveRating += score / defensiveRating;
                    }

                    _offensiveRatings[j] = newOffensiveRating;
                }

                //defensive calculations
                for (var i = 0; i < _results.ColumnCount; i++)
                {
                    var newDefensiveRating = 0D;

                    for (var j = 0; j < _results.RowCount; j++)
                    {
                        var score = _results[i, j];

                        var offensiveRating = _offensiveRatings[j];

                        newDefensiveRating += score / offensiveRating;
                    }

                    _defensiveRatings[i] = newDefensiveRating;
                }

                iterationNumber++;

            } while (!HasConverged());



        }

        private void StoreLast()
        {
            for (var i = 0; i < _offensiveRatings.Count; i++)
            {
                _previousDefensiveRatings[i] = _defensiveRatings[i];
                _previousOffensiveRatings[i] = _offensiveRatings[i];
            }
        }

        private bool HasConverged()
        {
            //defensive convergance check
            for (var i = 0; i < _defensiveRatings.Count; i++)
            {
                if (Math.Abs(_defensiveRatings[i] - _previousDefensiveRatings[i]) > 0.001)
                    return false;
            }

            ////offensive convergance check
            for (var i = 0; i < _offensiveRatings.Count; i++)
            {
                if (Math.Abs(_offensiveRatings[i] - _previousOffensiveRatings[i]) > 0.001)
                    return false;
            }

            return true;
        }

        private Matrix<double> GenerateTotalScoreMatrix(RatingBoardModel ratingRankingBoardModel)
        {
            var numberOfParticipants = ratingRankingBoardModel.ParticipantRatingModels.Count;

            var scoreMatrix = Matrix<double>.Build.Dense(numberOfParticipants, numberOfParticipants);

            int i = 0;
            foreach (var participantRankingModel in ratingRankingBoardModel
                .ParticipantRatingModels
                .OrderBy(x => x.Index))
            {
                int j = 0;
                foreach (var opponentParticipantRankingModel in ratingRankingBoardModel
                    .ParticipantRatingModels
                    .OrderBy(x => x.Index))
                {
                    if (participantRankingModel.Id == opponentParticipantRankingModel.Id)
                    {
                        scoreMatrix[j, i] = 0;
                    }
                    else
                    {
                        var score = participantRankingModel.TotalScoreByOpponentId[opponentParticipantRankingModel.Id];
                        scoreMatrix[j, i] = score;
                    }

                    j++;
                }

                i++;
            }

            return scoreMatrix;
        }
    }
}
