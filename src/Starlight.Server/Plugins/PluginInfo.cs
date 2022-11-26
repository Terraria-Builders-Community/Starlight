using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.Server.Plugins
{
    /// <summary>
    ///     Represents information about the plugin.
    /// </summary>
    public class PluginInfo
    {
        /// <summary>
        ///     The plugin version.
        /// </summary>
        public Version Version { get; } = new(1, 0, 0, 0);

        /// <summary>
        ///     The plugin name.
        /// </summary>
        public string Name { get; } = string.Empty;

        /// <summary>
        ///     The plugin description.
        /// </summary>
        public string Description { get; } = string.Empty;

        /// <summary>
        ///     The plugin author.
        /// </summary>
        public string Author { get; } = string.Empty; 

        public PluginInfo(Action<PluginInfo> action)
        {
            action(this);
        }
    }
}
