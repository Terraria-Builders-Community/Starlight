using CSF;
using System.ComponentModel;
using System.Reflection;

namespace Starlight
{
    public static class HostExtensions
    {
        public static IHostBuilder ConfigureDatabase(this IHostBuilder builder, Action<HostBuilderContext, DatabaseConfiguration> action)
        {
            builder.ConfigureServices((context, services) =>
            {
                var config = new DatabaseConfiguration();

                action(context, config);

                services.AddSingleton(config);
                services.AddSingleton<DatabaseConfiguration>();
            });
            return builder;
        }

        public static IHostBuilder ConfigureCommands(this IHostBuilder builder, Action<HostBuilderContext, CommandConfiguration> action)
        {
            builder.ConfigureServices((context, services) =>
            {
                var config = new CommandConfiguration();

                action(context, config);

                services.AddSingleton(config);
                services.AddSingleton<CommandFramework>();
            });

            return builder;
        }

        public static IHostBuilder ConfigurePlugins(this IHostBuilder builder, Action<ServerConfiguration> action)
        {
            var serverContext = new ServerConfiguration();

            action(serverContext);

            foreach (var asm in serverContext.LoadedAssemblies)
                builder.ConfigurePlugin(asm.Value);

            return builder;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IHostBuilder ConfigurePlugin(this IHostBuilder builder, Assembly assembly)
        {
            Type? pluginType = null;
            var eventTypes = new List<Type>();

            foreach (var type in assembly.GetExportedTypes())
            {
                if (type.IsSubclassOf(typeof(Plugin)) && !type.IsAbstract && type.IsPublic)
                {
                    if (pluginType is not null)
                        continue;

                    pluginType = type;
                }

                if (type.IsAssignableTo(typeof(IResolver)) && !type.IsAbstract && type.IsPublic)
                {
                    eventTypes.Add(type);
                }
            }

            if (pluginType is null)
                return builder;

            var obj = (Activator.CreateInstance(pluginType) as Plugin)!;

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(typeof(Plugin), obj);
                obj.ConfigureServices(services);

                foreach (var eventType in eventTypes)
                    services.AddSingleton(typeof(IResolver), eventType);
            });

            return builder;
        }
    }
}
