using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MultipleRanker.Domain;
using MultipleRanker.Domain.Raters;
using MultipleRanker.Domain.Raters.Raters;
using MultipleRanker.RankerApi.Contracts;
using MultipleRanker.RankerApi.Contracts.Events;
using NUnit.Framework;
using ParticipantRating = MultipleRanker.Contracts.ParticipantRating;

namespace MultipleRanker.Tests.Unit
{
    public class EloRankerTests
    {
        private TestContext _context;

        [OneTimeSetUp]
        public void SetUp() =>
            _context = new TestContext()
                .LoadFixtures()
                .LoadTeams()
                .CreateRatingList()
                .AddParticipants()
                .AddGames();

        [Test]
        public void Elo_Rates_In_Correct_Order() =>
            _context
                .Rate()
                .AssertRatingOrderCorrect(
                    new Dictionary<Guid, int>
                    {
                        { Guid.Parse("FB052DE0-C9E3-4559-B370-A352EF4E3234"), 5 },
                        { Guid.Parse("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181"), 1 },
                        { Guid.Parse("1549972F-2CFE-4F27-A23C-E54DFE6F85E5"), 4 },
                        { Guid.Parse("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F"), 3 },
                        { Guid.Parse("BCE0C4D0-385F-47D6-9E37-820C30AC2147"), 2 },
                    })
                .AssertRatingRangeCorrect();

        public class TestContext
        {
            private readonly List<TempMatchUp> _tempMatchUps
                = new List<TempMatchUp>();

            private readonly HashSet<TempParticipant> _partitipcants
                = new HashSet<TempParticipant>();

            private IRater _sut;

            private readonly Guid _ratingListId;

            private RatingListModel _ratingListModel;

            private IEnumerable<ParticipantRating> _ratingResults;

            public TestContext()
            {
                _ratingListModel = new RatingListModel();

                _sut = new EloRater();

                _ratingListId = Guid.NewGuid();
            }

            public TestContext Rate()
            {
                _ratingResults = _sut
                    .Rate(_ratingListModel);

                return this;
            }

            public TestContext CreateRatingList()
            {
                var ratingListCreated = new RatingListCreated
                {
                    RatingListId = _ratingListId,
                    RatingBoardId = Guid.NewGuid(),
                    RatingAggregation = RankerApi.Contracts.RatingAggregationType.TotalWins,
                    RatingType = RankerApi.Contracts.RatingType.OffensiveDefensive,
                    CreatedOnUtc = DateTime.UtcNow
                };

                _ratingListModel.Apply(ratingListCreated);

                return this;
            }

            public TestContext AddParticipants()
            {
                foreach (var participant in _partitipcants)
                {
                    var addParticipantToRatingBoard = new ParticipantAddedToRatingList
                    {
                        ParticipantId = participant.Id,
                        RatingListId = _ratingListId
                    };

                    _ratingListModel.Apply(addParticipantToRatingBoard);
                }

                return this;
            }

            public TestContext AddGames()
            {
                foreach (var matchUp in _tempMatchUps)
                {
                    var resultAdded = new ResultAdded
                    {
                        RatingListId = _ratingListId,
                        ParticipantResults = new List<ParticipantResult>
                        {
                            new ParticipantResult
                            {
                                ParticipantId = matchUp.HomeParticipantId,
                                Score = matchUp.HomeParticipantScore
                            },
                            new ParticipantResult
                            {
                                ParticipantId = matchUp.AwayParticipantId,
                                Score = matchUp.AwayParticipantScore
                            }
                        }
                    };

                    _ratingListModel.Apply(resultAdded);
                }

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

            public TestContext AssertRatingOrderCorrect(
                IDictionary<Guid, int> expectedRatingOrderByTeamId)
            {
                var ratingPosition = 1;
                foreach (var ratingResult in _ratingResults
                    .OrderByDescending(x => x.Rating))
                {
                    var expectedRatingOrder = expectedRatingOrderByTeamId[ratingResult.ParticipantId];

                    Assert.AreEqual(expectedRatingOrder, ratingPosition);

                    ratingPosition++;
                }

                return this;
            }

            public TestContext AssertRatingRangeCorrect()
            {
                foreach (var ratingResult in _ratingResults)
                {
                    Assert.Greater(ratingResult.Rating, 1000);
                    Assert.Less(ratingResult.Rating, 2000);
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
