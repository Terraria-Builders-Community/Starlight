namespace Starlight.Entities.Bans.Entity
{
    public interface IEntityBan : IBan
    {
        public DateTimeOffset? TimeUntilRaised { get; set; }

        public EntityBanFlags Flags { get; set; }

        public string Name { get; set; }
    }
}
