using Starlight.Structures;
using System.Net;

namespace Starlight.Entities.Devices
{
    public interface IDevice : IModel
    {
        /// <summary>
        ///     The UUID of the user.
        /// </summary>
        public UUId UUID { get; set; }

        /// <summary>
        ///     The IP addresses this user has so far used to connect with.
        /// </summary>
        public IList<IPAddress> IPHistory { get; set; }
    }
}
