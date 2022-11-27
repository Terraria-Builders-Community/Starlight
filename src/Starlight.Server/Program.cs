using CSF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Starlight;

var context = new ServerBuilderContext();

AppDomain.CurrentDomain.UnhandledException += (_, e)
    => Console.WriteLine(e);
AppDomain.CurrentDomain.AssemblyResolve += (_, e)
    => AssemblyResolver.Resolve(e, context);

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

host.ConfigurePlugins(serverContext =>
{
    serverContext.LoadedAssemblies = context.LoadedAssemblies;
});

host.ConfigureDatabase((context, database) =>
{
    database.ConnectionString = context.Configuration.GetConnectionString("MongoDB")!;
    database.DatabaseName = context.Configuration.GetSection("Database")["Name"]!;
});

host.ConfigureCommands((context, commands) =>
{
    if (!Enum.TryParse<CSF.LogLevel>(context.Configuration.GetSection("Logging").GetSection("LogLevel")["Commands"], out var logLevel))
        logLevel = CSF.LogLevel.Debug;
    commands.DefaultLogLevel = logLevel;
    commands.DoAsynchronousExecution = false;
    commands.Prefixes = new PrefixProvider()
        .Include(new CharPrefix('/'))
        .Include(new CharPrefix('.'));
});

host.ConfigureServices((context, services) =>
{
    services.AddHostedService<Server>();
    services.AddSingleton<HookCaller>();
});

var app = host.Build();

await app.Services.GetRequiredService<HookCaller>()
    .AttachAsync();

await app.RunAsync();