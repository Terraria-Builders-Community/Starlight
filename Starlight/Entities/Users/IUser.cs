using Starlight.Structures;
using System.Net;

namespace Starlight.Entities.Users
{
    public interface IUser : IModel
    {
        public string Name { get; set; }

        public UUId UUID { get; set; }

        public IList<IPAddress> IPHistory { get; set; }
    }
}
