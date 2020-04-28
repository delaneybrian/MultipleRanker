using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MultipleRanker.Contracts;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Domain;
using MultipleRanker.Domain.Raters.Generators;
using NUnit.Framework;

namespace MultipleRanker.Tests.Unit.GeneratorTests
{
    [TestFixture]
    public class PointsDifferenceGeneratorTests
    {
        public TestContext _context;

        [OneTimeSetUp]
        public void SetUp() =>
            _context = new TestContext()
                .LoadFixtures()
                .LoadTeams()
                .CreateRankingBoard()
                .AddTeams()
                .AddGames();

        [Test]
        public void Total_Score_Matrix_Generated_Correctly() =>
            _context
                .GenerateTotalScoreMatrix()
                .SetExpectedResults()
                .AssertIsCorrect();

        public class TestContext
        {
            private readonly List<TempMatchUp> _tempMatchUps
                = new List<TempMatchUp>();

            private readonly HashSet<TempParticipant> _partitipcants
                = new HashSet<TempParticipant>();

            private PointsDifferentGenerator _sut;

            private readonly Guid _ratingBoardId;

            private RatingBoardModel _ratingBoardModel;

            private Matrix<double> _totalScoreMatrix;

            private int[][] _expectedResultsArray = new int[5][];

            public TestContext()
            {
                _ratingBoardModel = new RatingBoardModel();

                _sut = new PointsDifferentGenerator();

                _ratingBoardId = Guid.NewGuid();
            }

            public TestContext SetExpectedResults()
            {
                _expectedResultsArray[0] = new[] { 0, 45, 3, 31, 45 };
                _expectedResultsArray[1] = new[] { 0, 0, 0, 0, 0 };
                _expectedResultsArray[2] = new[] { 0, 18, 0, 0, 27 };
                _expectedResultsArray[3] = new[] { 0, 8, 2, 0, 38 };
                _expectedResultsArray[4] = new[] { 0, 20, 0, 0, 0 };

                return this;
            }

            public TestContext GenerateTotalScoreMatrix()
            {
                _totalScoreMatrix = _sut.Generate(_ratingBoardModel);

                return this;
            }

            public TestContext AssertIsCorrect()
            {
                var i = 0;
                foreach (var expectedTeamResults in _expectedResultsArray)
                {
                    var j = 0;
                    foreach (var result in expectedTeamResults)
                    {
                        Assert.AreEqual(_totalScoreMatrix[i, j], result);
                        j++;
                    }

                    i++;
                }

                return this;
            }

            public TestContext AddTeams()
            {
                foreach (var participant in _partitipcants)
                {
                    var addParticipantToRatingBoard = new AddParticipantToRatingBoard
                    {
                        ParticipantId = participant.Id,
                        ParticipantName = participant.Name,
                        RankingBoardId = _ratingBoardId
                    };

                    _ratingBoardModel.Apply(addParticipantToRatingBoard);
                }

                return this;
            }

            public TestContext AddGames()
            {
                foreach (var matchUp in _tempMatchUps)
                {
                    var matchUpCompletedCommand = new MatchUpCompleted
                    {
                        RatingBoardId = _ratingBoardId,
                        ParticipantScores = new List<MatchUpParticipantScore>
                        {
                            new MatchUpParticipantScore
                            {
                                ParticipantId = matchUp.HomeParticipantId,
                                PointsScored = matchUp.HomeParticipantScore
                            },
                            new MatchUpParticipantScore
                            {
                                ParticipantId = matchUp.AwayParticipantId,
                                PointsScored = matchUp.AwayParticipantScore
                            }
                        }
                    };

                    _ratingBoardModel.Apply(matchUpCompletedCommand);
                }

                return this;
            }

            public TestContext CreateRankingBoard()
            {
                var createRankingBoard = new CreateRatingBoard
                {
                    Id = _ratingBoardId,
                    Name = "Test Rating Board"
                };

                _ratingBoardModel.Apply(createRankingBoard);

                return this;
            }

            public TestContext LoadFixtures()
            {
                var directory = Directory.GetCurrentDirectory();

                var path = Path.Combine(directory, @"Files\NCAAResults.csv");

                var lines = File.ReadAllLines(path);

                foreach (var line in lines.Skip(1))
                {
                    var items = line.Split(',');

                    _tempMatchUps.Add(new TempMatchUp
                    {
                        HomeParticipantName = items[1],
                        HomeParticipantId = new Guid(items[2]),
                        HomeParticipantScore = int.Parse(items[3]),
                        AwayParticipantName = items[4],
                        AwayParticipantId = new Guid(items[5]),
                        AwayParticipantScore = int.Parse(items[6])
                    });
                }

                return this;
            }

            public TestContext LoadTeams()
            {
                var homeParticipants = _tempMatchUps
                    .Select(x => new TempParticipant
                    {
                        Id = x.HomeParticipantId,
                        Name = x.HomeParticipantName
                    });

                var awayParticipants = _tempMatchUps
                    .Select(x => new TempParticipant
                    {
                        Id = x.AwayParticipantId,
                        Name = x.AwayParticipantName
                    });

                var allParticipantIds = homeParticipants
                    .Concat(awayParticipants);

                foreach (var participantId in allParticipantIds)
                {
                    _partitipcants.Add(participantId);
                }

                return this;
            }
        }

        private class TempParticipant : IEquatable<TempParticipant>
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public bool Equals(TempParticipant other)
            {
                if (other is null)
                    return false;

                if (ReferenceEquals(this, other))
                    return true;

                return this.Id == other.Id;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Id.GetHashCode();
                    return hashCode;
                }
            }
        }

        private class TempMatchUp
        {
            public Guid HomeParticipantId { get; set; }

            public string HomeParticipantName { get; set; }

            public int HomeParticipantScore { get; set; }

            public Guid AwayParticipantId { get; set; }

            public string AwayParticipantName { get; set; }

            public int AwayParticipantScore { get; set; }
        }
    }
}
