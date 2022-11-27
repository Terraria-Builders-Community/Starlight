using Starlight.Structures;
using System.Net;

namespace Starlight.Entities.Accounts
{
    /// <summary>
    ///     Represents an account.
    /// </summary>
    public interface IAccount : IModel
    {
        /// <summary>
        ///     The account name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The groups this account has access to.
        /// </summary>
        public IList<string> Groups { get; set; }

        /// <summary>
        ///     The account-only permissions of this account.
        /// </summary>
        public IList<Permission> Permissions { get; set; }

        /// <summary>
        ///     The device history of this account.
        /// </summary>
        public IList<UUId> DeviceHistory { get; set; }

        /// <summary>
        ///     The IP history of this account. This inherits from the device history.
        /// </summary>
        public IList<IPAddress> IPHistory { get; set; }

        /// <summary>
        ///     The selected prefix for this account.
        /// </summary>
        public string? Prefix { get; set; }

        /// <summary>
        ///     The selected suffix for this account.
        /// </summary>
        public string? Suffix { get; set; }
    }
}
