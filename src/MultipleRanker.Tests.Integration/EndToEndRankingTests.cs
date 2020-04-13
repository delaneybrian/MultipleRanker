using MultipleRanker.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MultipleRanker.Definitions;
using MultipleRanker.Definitions.Commands;

namespace MultipleRanker.Tests.Integration
{
    [TestFixture]
    public class EndToEndRankingTests
    {
        public TestContext _context;

        [SetUp]
        public void SetUp() => _context = new TestContext();

        public class TestContext
        {
            private readonly List<TempMatchUp> _tempMatchUps
                = new List<TempMatchUp>();

            private readonly HashSet<Guid> _participantIds
                = new HashSet<Guid>();

            private RankingBoardModel _sut;

            private readonly Guid _rankingBoardId;
            
            public TestContext()
            {
                _sut = new RankingBoardModel();

                _rankingBoardId = Guid.NewGuid();
            }

            public TestContext CreateRankingBoard()
            {
                var createRankingBoardCommand = new CreateRankingBoardCommand
                {
                    Id = _rankingBoardId,
                    Name = "Test Ranking Board"
                };

                _sut.Apply(createRankingBoardCommand);

                return this;
            }

            public TestContext AddTeams()
            {
                foreach (var participantId in _participantIds)
                {
                    var addParticipantToRankingBoard = new AddParticipantToRankingBoardCommand
                    {
                        ParticipantId = participantId,
                        ParticipantName = "Test",
                        RankingBoardId = _rankingBoardId
                    };

                    _sut.Apply(addParticipantToRankingBoard);
                }

                return this;
            }

            public TestContext AddGames()
            {
                foreach (var matchUp in _tempMatchUps)
                {
                    var matchUpCompletedCommand = new MatchUpCompletedCommand
                    {
                        RankingBoardId = _rankingBoardId,
                        ParticipantScores = new List<MatchUpParticipantScore>
                        {
                            new MatchUpParticipantScore
                            {
                                ParticipantId = matchUp.HomeParticipantId,
                                PointsScored = matchUp.HomeParticipantScore
                            },
                            new MatchUpParticipantScore
                            {
                                ParticipantId = matchUp.HomeParticipantId,
                                PointsScored = matchUp.HomeParticipantScore
                            }
                        }
                    };

                    _sut.Apply(matchUpCompletedCommand);
                }

                return this;
            }

            public TestContext LoadFixtures()
            {
                var directory = Directory.GetCurrentDirectory();

                var path = Path.Combine(directory, "/Files/NCAAResults.csv");

                var lines = File.ReadAllLines(path);

                foreach (var line in lines)
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
                _tempMatchUps
                    .Select(x => _participantIds
                        .Add(x.HomeParticipantId));

                _tempMatchUps
                    .Select(x => _participantIds
                        .Add(x.AwayParticipantId));

                return this;
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
