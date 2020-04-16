using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MultipleRanker.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder();

            builder.ConfigureServices(s => s.AddSingleton<IHostedService, MultipleRankerService>());

            await builder.RunConsoleAsync();
        }
    }
}
