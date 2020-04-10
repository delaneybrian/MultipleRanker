using System;
using System.Collections.Generic;
using Autofac;
using MediatR;
using MultipleRanker.Definitions;
using MultipleRanker.Definitions.Commands;

namespace MultipleRanker.Host.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = Bootstrapper.Bootstrap())
            {
                var mediator = container.Resolve<IMediator>();

                var rankingBoardId = Guid.NewGuid();

                mediator.Send(new CreateRankingBoardCommand()
                {
                    Id = rankingBoardId,
                    Name = "Test Board"
                }).Wait();

                var participantId = Guid.NewGuid();
                var opponentId = Guid.NewGuid();

                mediator.Send(new AddParticipantToRankingBoardCommand
                {
                    ParticipantId = participantId,
                    ParticipantName = "Team1",
                    RankingBoardId = rankingBoardId
                }).Wait();

                mediator.Send(new AddParticipantToRankingBoardCommand
                {
                    ParticipantId = opponentId,
                    ParticipantName = "Team2",
                    RankingBoardId = rankingBoardId
                }).Wait();

                mediator.Send(new MatchUpCompletedCommand
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
