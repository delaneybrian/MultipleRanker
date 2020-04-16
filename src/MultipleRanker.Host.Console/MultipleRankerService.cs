using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Hosting;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Host
{
    public class MultipleRankerService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var container = Bootstrapper.Bootstrap();

            using (var scope = container.BeginLifetimeScope())
            {
                var messageSubscriber = scope.Resolve<IMessageSubscriber>();

                Task.Run(() => messageSubscriber.Start(cancellationToken));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
