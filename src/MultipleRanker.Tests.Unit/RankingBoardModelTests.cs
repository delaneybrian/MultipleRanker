using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MultipleRanker.Domain;
using NUnit.Framework;
using MultipleRanker.Definitions;
using MultipleRanker.Definitions.Commands;
using MultipleRanker.Definitions.Snapshots;

namespace MultipleRanker.Tests.Integration
{
    [TestFixture]
    public class RankingBoardModelTests
    {
        public TestContext _context;

        [OneTimeSetUp]
        public void SetUp() => 
            _context = new TestContext()
                .LoadFixtures()
                .LoadTeams()
                .CreateRankingBoard()
                .AddTeams()
                .AddGames()
                .ToSnapshot();

        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", 4)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 4)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 4)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 4)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", 4)]
        public void Test_Games_Played_By_Participant(string participantId, int gamesPlayed) =>
            _context
                .AssertGamesPlayed(participantId, gamesPlayed);

        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", 35)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 138)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 50)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 74)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", 134)]
        public void Test_Total_Score_By_Participant(string participantId, int scoreFor) =>
            _context
                .AssertTotalScoreFor(participantId, scoreFor);

        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", 159)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 47)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 90)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 91)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", 44)]
        public void Test_Total_Score_Against_By_Participant(string participantId, int scoreAgainst) =>
            _context
                .AssertTotalScoreAgainst(participantId, scoreAgainst);

        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 0)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 0)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 0)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 0)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 1)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 1)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 1)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 1)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 1)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 0)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 1)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 0)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 1)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 0)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 0)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 0)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 1)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 0)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 1)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 1)]
        public void Test_Wins_Vs_Opponent(string participantId, string opponentId, int wins) =>
            _context
                .AssertWinsByOpponentId(participantId, opponentId, wins);

        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 1)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 1)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 1)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 1)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 0)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 0)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 0)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 0)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 0)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 1)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 0)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 1)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 0)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 1)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 1)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 1)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 0)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 1)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 0)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 0)]
        public void Test_Loses_Vs_Opponent(string participantId, string opponentId, int loses) =>
           _context
               .AssertLossesByOpponentId(participantId, opponentId, loses);

        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 7)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 21)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 7)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 0)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 52)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 34)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 25)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 27)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 24)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 16)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 7)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 3)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 38)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 17)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 5)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 14)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 45)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 7)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 30)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 52)]
        public void Test_Scored_Vs_Opponent(string participantId, string opponentId, int scored) =>
          _context
              .AssertTotalScoreByOpponentId(participantId, opponentId, scored);

        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 52)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 24)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 38)]
        [TestCase("FB052DE0-C9E3-4559-B370-A352EF4E3234", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 45)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 7)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 16)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 17)]
        [TestCase("6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 7)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 21)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 34)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 5)]
        [TestCase("1549972F-2CFE-4F27-A23C-E54DFE6F85E5", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 30)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 7)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 25)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 7)]
        [TestCase("DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", "BCE0C4D0-385F-47D6-9E37-820C30AC2147", 52)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "FB052DE0-C9E3-4559-B370-A352EF4E3234", 0)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "6AA6FBD6-B0CB-4B2B-B9C2-DB87895DC181", 27)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "1549972F-2CFE-4F27-A23C-E54DFE6F85E5", 3)]
        [TestCase("BCE0C4D0-385F-47D6-9E37-820C30AC2147", "DE6F9FC2-24C2-40CF-B012-5657FB2CEC9F", 14)]
        public void Test_Score_Conceded_Vs_Opponent(string participantId, string opponentId, int scoreConceeded) =>
        _context
            .AssertTotalScoreConcededByOpponentId(participantId, opponentId, scoreConceeded);

        public class TestContext
        {
            private readonly List<TempMatchUp> _tempMatchUps
                = new List<TempMatchUp>();

            private readonly HashSet<Guid> _participantIds
                = new HashSet<Guid>();

            private RankingBoardModel _sut;

            private readonly Guid _rankingBoardId;

            private RankingBoardSnapshot _rankingBoardSnapshot;
            
            public TestContext()
            {
                _sut = new RankingBoardModel();

                _rankingBoardId = Guid.NewGuid();
            }

            public TestContext ToSnapshot()
            {
                _rankingBoardSnapshot = _sut.ToSnapshot();

                return this;
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
                                ParticipantId = matchUp.AwayParticipantId,
                                PointsScored = matchUp.AwayParticipantScore
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
                var homeParticipantIds = _tempMatchUps
                    .Select(x => x.HomeParticipantId);

                var awayParticipantIds = _tempMatchUps
                    .Select(x => x.AwayParticipantId);

                var allParticipantIds = homeParticipantIds
                    .Concat(awayParticipantIds);

                foreach (var participantId in allParticipantIds)
                {
                    _participantIds.Add(participantId);
                }

                return this;
            }

            public TestContext AssertLossesByOpponentId(string participantId, string opponentId, int expectedLosses)
            {
                var participantIdToCheck = Guid.Parse(participantId);

                var opponentIdToCheck = Guid.Parse(opponentId);

                var rankingBoardParticipant = _rankingBoardSnapshot.RankingBoardParticipants
                    .Single(p => p.Id == participantIdToCheck);

                var actualLosses = rankingBoardParticipant.TotalLosesByOpponentId[opponentIdToCheck];

                Assert.AreEqual(expectedLosses, actualLosses);

                return this;
            }

            public TestContext AssertWinsByOpponentId(string participantId, string opponentId, int expectedWins)
            {
                var participantIdToCheck = Guid.Parse(participantId);

                var opponentIdToCheck = Guid.Parse(opponentId);

                var rankingBoardParticipant = _rankingBoardSnapshot.RankingBoardParticipants
                    .Single(p => p.Id == participantIdToCheck);

                var actualWins = rankingBoardParticipant.TotalWinsByOpponentId[opponentIdToCheck];

                Assert.AreEqual(expectedWins, actualWins);

                return this;
            }

            public TestContext AssertTotalScoreConcededByOpponentId(string participantId, string opponentId, int expectedConcededScore)
            {
                var participantIdToCheck = Guid.Parse(participantId);

                var opponentIdToCheck = Guid.Parse(opponentId);

                var rankingBoardParticipant = _rankingBoardSnapshot.RankingBoardParticipants
                    .Single(p => p.Id == participantIdToCheck);

                var actualScoreConceeded = rankingBoardParticipant.TotalScoreConcededByOpponentId[opponentIdToCheck];

                Assert.AreEqual(expectedConcededScore, actualScoreConceeded);

                return this;
            }

            public TestContext AssertTotalScoreByOpponentId(string participantId, string opponentId, int expectedTotalScore)
            {
                var participantIdToCheck = Guid.Parse(participantId);

                var opponentIdToCheck = Guid.Parse(opponentId);

                var rankingBoardParticipant = _rankingBoardSnapshot.RankingBoardParticipants
                    .Single(p => p.Id == participantIdToCheck);

                var totalScored = rankingBoardParticipant.TotalScoreByOpponentId[opponentIdToCheck];

                Assert.AreEqual(expectedTotalScore, totalScored);

                return this;
            }

            public TestContext AssertGamesPlayed(string participantId, int expectedGamesPlayed)
            {
                var participantIdToCheck = Guid.Parse(participantId);

                var rankingBoardParticipant = _rankingBoardSnapshot.RankingBoardParticipants
                    .Single(p => p.Id == participantIdToCheck);

                var totalGamesPlayed = rankingBoardParticipant.TotalGamesPlayed;

                Assert.AreEqual(expectedGamesPlayed, totalGamesPlayed);

                return this;
            }

            public TestContext AssertTotalScoreAgainst(string participantId, int expectedScoreAgainst)
            {
                var participantIdToCheck = Guid.Parse(participantId);

                var rankingBoardParticipant = _rankingBoardSnapshot.RankingBoardParticipants
                    .Single(p => p.Id == participantIdToCheck);

                var totalScoreAgainst = rankingBoardParticipant.TotalScoreAgainst;

                Assert.AreEqual(expectedScoreAgainst, totalScoreAgainst);

                return this;
            }

            public TestContext AssertTotalScoreFor(string participantId, int expectedScoreFor)
            {
                var participantIdToCheck = Guid.Parse(participantId);

                var rankingBoardParticipant = _rankingBoardSnapshot.RankingBoardParticipants
                    .Single(p => p.Id == participantIdToCheck);

                var totalScoreFor = rankingBoardParticipant.TotalScoreFor;

                Assert.AreEqual(expectedScoreFor, totalScoreFor);

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
