using CSF;

namespace Starlight
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        ///     Fetches the defined default prefix from the configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>The fetched prefix.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static CharPrefix GetDefaultPrefix(this IConfiguration configuration)
        {
            var prefix = configuration.GetSection("Prefixes")["Default"]!;

            if (prefix.Length > 1)
                throw new ArgumentOutOfRangeException(nameof(prefix), "Prefix cannot be more than 1 character long.");

            return new(prefix.ToCharArray()[0]);
        }

        /// <summary>
        ///     Fetches the defined silent prefix from the configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>The fetched prefix.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static CharPrefix GetSilentPrefix(this IConfiguration configuration)
        {
            var prefix = configuration.GetSection("Prefixes")["Silent"]!;

            if (prefix.Length > 1)
                throw new ArgumentOutOfRangeException(nameof(prefix), "Prefix cannot be more than 1 character long.");

            return new(prefix.ToCharArray()[0]);
        }

        /// <summary>
        ///     Fetches the loglevel of the specified <paramref name="section"/>.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="section"></param>
        /// <returns>The specified value.</returns>
        public static Microsoft.Extensions.Logging.LogLevel GetLogLevel(this IConfiguration configuration, string section)
        {
            var level = configuration.GetSection("Logging").GetSection("LogLevel")[section]!;

            if (Enum.TryParse<Microsoft.Extensions.Logging.LogLevel>(level, out var @enum))
                return @enum;

            else
                throw new ArgumentException("The provided section cannot be interpreted as loglevel.", nameof(section));
        }
    }
}
