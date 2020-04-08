using Autofac;
using MediatR;
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

                mediator.Send(new CreateRankingBoardCommand()).Wait();
            }

            System.Console.WriteLine("Finished");
            System.Console.ReadKey();
        }
    }
}
