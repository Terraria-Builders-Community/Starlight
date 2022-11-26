using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.Server.Hosting
{
    public class ServerHost : IHostedService
    {
        public ServerHost()
        {

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Terraria.WindowsLaunch.Main(Environment.GetCommandLineArgs());

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
