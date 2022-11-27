namespace Starlight
{
    /// <summary>
    ///     Represents information about the plugin.
    /// </summary>
    public class PluginInfo
    {
        /// <summary>
        ///     The order in which the <see cref="Plugin.LoadAsync"/> call is made across all plugins.
        /// </summary>
        public int Order { get; set; } = 0;

        /// <summary>
        ///     The plugin version.
        /// </summary>
        public Version Version { get; set; } = new(1, 0, 0, 0);

        /// <summary>
        ///     The plugin name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     The plugin description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     The plugin author.
        /// </summary>
        public string Author { get; set; } = string.Empty;

        public PluginInfo(Action<PluginInfo> action)
        {
            action(this);
        }
    }
}
