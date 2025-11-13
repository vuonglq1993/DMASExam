using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BattleGameFunction
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices((ctx, services) =>
                {
                    var conn = Environment.GetEnvironmentVariable("SqlConnectionString") 
                               ?? throw new InvalidOperationException("Missing connection string");
                    services.AddDbContext<BattleGameContext>(options =>
                        options.UseSqlServer(conn));
                })
                .Build();

            host.Run();
        }
    }
}