using System.Reflection;

namespace Starlight
{
    public sealed class ServerConfiguration
    {
        /// <summary>
        ///     Represents the default path to load plugins from.
        /// </summary>
        public string PluginPath { get; set; }

        /// <summary>
        ///     Represents the assemblies loaded by default.
        /// </summary>
        public Dictionary<string, Assembly> LoadedAssemblies { get; set; }

        public ServerConfiguration()
        {
            LoadedAssemblies = new();
            PluginPath = "plugins";
        }
    }
}
