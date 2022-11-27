using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Starlight
{
    public class Server : IHostedService
    {
        private readonly ILogger<Server> _logger;
        private readonly IReadOnlyList<Plugin> _plugins;

        public Server(ILogger<Server> logger, IEnumerable<Plugin> plugins)
        {
            _plugins = plugins
                .OrderBy(x => x.PluginInfo.Order)
                .OrderBy(x => x.GetType().Name)
                .ToList();

            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var args = Environment.GetCommandLineArgs();

            _logger.LogInformation("Starting server with arguments: {}.", string.Join(", ", args));

            foreach (var plugin in _plugins)
            {
                try
                {
                    await plugin.LoadAsync();
                    _logger.LogInformation("Succesfully loaded plugin {} ({}).",
                        plugin.PluginInfo.Name, plugin.PluginInfo.Version);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Critical exception occurred while initializing plugin {} ({}).", plugin.PluginInfo.Name, plugin.PluginInfo.Version);
                }
            }

            Terraria.WindowsLaunch.Main(Environment.GetCommandLineArgs());
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var plugin in _plugins)
            {
                await plugin.UnloadAsync();

                _logger.LogInformation("Succesfully unloaded plugin {} ({}).",
                    plugin.PluginInfo.Name, plugin.PluginInfo.Version);

                plugin.Dispose();
            }
        }
    }
}
