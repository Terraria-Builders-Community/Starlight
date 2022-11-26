using Starlight.Structures;

namespace Starlight.Entities.Groups
{
    public interface IGroup : IModel
    {
        public string Name { get; set; }

        public IList<Permission> Permissions { get; set; }

        public string Parent { get; set; }

        public string Prefix { get; set; }

        public string Suffix { get; set; }
    }
}
