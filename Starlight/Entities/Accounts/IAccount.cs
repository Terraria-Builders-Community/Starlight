using Starlight.Structures;
using System.Net;

namespace Starlight.Entities.Accounts
{
    public interface IAccount
    {
        public string Name { get; set; }

        public IList<string> Groups { get; set; }

        public IList<Permission> Permissions { get; set; }

        public IList<UUId> UserHistory { get; set; }

        public IList<IPAddress> IPHistory { get; set; }
    }
}
