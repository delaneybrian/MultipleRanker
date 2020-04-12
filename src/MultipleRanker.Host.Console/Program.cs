using System;
using System.Collections.Generic;
using Autofac;
using MultipleRanker.Definitions;
using MultipleRanker.Definitions.Commands;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Host.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = Bootstrapper.Bootstrap())
            {
                var commandPublisher = container.Resolve<ICommandPublisher>();

                var rankingBoardId = Guid.NewGuid();

                commandPublisher.Publish(new CreateRankingBoardCommand()
                {
                    Id = rankingBoardId,
                    Name = "Test Board"
                }).Wait();

                var participantId = Guid.NewGuid();
                var opponentId = Guid.NewGuid();

                commandPublisher.Publish(new AddParticipantToRankingBoardCommand
                {
                    ParticipantId = participantId,
                    ParticipantName = "Team1",
                    RankingBoardId = rankingBoardId
                }).Wait();

                commandPublisher.Publish(new AddParticipantToRankingBoardCommand
                {
                    ParticipantId = opponentId,
                    ParticipantName = "Team2",
                    RankingBoardId = rankingBoardId
                }).Wait();

                commandPublisher.Publish(new MatchUpCompletedCommand
                {
                    ParticipantScores = new List<MatchUpParticipantScore>
                    {
                        new MatchUpParticipantScore
                        {
                            ParticipantId = participantId,
                            PointsScored = 20
                        },
                        new MatchUpParticipantScore
                        {
                            ParticipantId = opponentId,
                            PointsScored = 31
                        }
                    },
                    RankingBoardId = rankingBoardId
                }).Wait();
            }

            System.Console.WriteLine("Finished");
            System.Console.ReadKey();
        }
    }
}
