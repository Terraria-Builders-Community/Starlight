using Starlight.Structures;

namespace Starlight.Entities.Regions
{
    public interface IRegion : IModel
    {
        public string Name { get; set; }

        public Polygon Polygon { get; set; }
    }
}
