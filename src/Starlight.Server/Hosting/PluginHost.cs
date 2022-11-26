using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Starlight.Server.Plugins;
using Starlight.Server.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.Server.Hosting
{
    public abstract class PluginHost<T> : IHostedService
    {
        public abstract PluginInfo PluginInfo { get; }

        public ILogger<T> Logger { get; }

        public PluginHost(ILogger<T> logger)
        {
            Logger = logger;
        }

        public abstract Task StartAsync(CancellationToken cancellationToken);

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public virtual Task<HandleResult> OnChatAsync(ChatEventArgs args)
        {
            return Task.FromResult(HandleResult.FromUnhandled());
        }
    }
}
