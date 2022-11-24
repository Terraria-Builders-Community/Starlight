using Starlight.Structures;

namespace Starlight.Entities.Mutes
{
    public interface IMute : IModel
    {
        public UUId UUID { get; set; }

        public DateTimeOffset? TimeUntilRaised { get; set; }
    }
}
