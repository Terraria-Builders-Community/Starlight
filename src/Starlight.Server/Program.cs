using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Starlight.Server.Hosting;

AppDomain.CurrentDomain.UnhandledException += (_, e) =>
{
    Console.WriteLine(e);
};

var host = Host.CreateDefaultBuilder(args);

host.ConfigureHostConfiguration(configure =>
{
    configure.AddEnvironmentVariables("STARLIGHT_");
    configure.AddJsonFile("appsettings.json");
});

host.ConfigureLogging(logging =>
{
    logging.AddSimpleConsole();
});

host.ConfigureServices((context, services) =>
{
    services.AddHostedService<ServerHost>();
});

var app = host.Build();

await app.RunAsync();