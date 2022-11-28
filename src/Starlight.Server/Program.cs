using CSF;
using Starlight;

var configuration = new ServerConfiguration();

AppDomain.CurrentDomain.UnhandledException += (_, e)
    => Console.WriteLine(e);
AppDomain.CurrentDomain.AssemblyResolve += (_, e)
    => AssemblyResolver.Resolve(e, configuration);

var host = Host.CreateDefaultBuilder(args);

host.ConfigureHostConfiguration(configure =>
{
    configure.AddEnvironmentVariables("TSERVER_");
    configure.AddJsonFile("appsettings.json");
});

host.ConfigureLogging(logging =>
{
    logging.AddSimpleConsole();
});

AssemblyResolver.ResolvePlugins(configuration);

host.ConfigurePlugins(server =>
{
    server.LoadedAssemblies = configuration.LoadedAssemblies;
});

host.ConfigureDatabase((context, database) =>
{
    database.ConnectionString = context.Configuration.GetConnectionString("Database")!;
    database.DatabaseName = context.Configuration.GetSection("Database")["Name"]!;
});

host.ConfigureCommands((context, commands) =>
{
    commands.DefaultLogLevel = (CSF.LogLevel)context.Configuration.GetLogLevel("Commands");
    commands.DoAsynchronousExecution = true;

    commands.Prefixes = new PrefixProvider()
        .Include(context.Configuration.GetSilentPrefix())
        .Include(context.Configuration.GetDefaultPrefix());
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