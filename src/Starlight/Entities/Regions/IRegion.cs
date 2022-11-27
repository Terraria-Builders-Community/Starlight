using Starlight.Structures;

namespace Starlight.Entities.Regions
{
    /// <summary>
    ///     Represents a world region.
    /// </summary>
    public interface IRegion : IModel
    {
        /// <summary>
        ///     The name of this region.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The ID of the world this region is available in.
        /// </summary>
        public Guid WorldId { get; set; }

        /// <summary>
        ///     The depth layer of this region.
        /// </summary>
        public byte ZLayer { get; set; }

        /// <summary>
        ///     The definition of this region.
        /// </summary>
        public Polygon Definition { get; set; }

        /// <summary>
        ///     The event flags of this region.
        /// </summary>
        public RegionFlags Flags { get; set; }
    }
}
