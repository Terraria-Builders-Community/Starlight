namespace Starlight.Entities.Bans.Object
{
    /// <summary>
    ///     Represents an object ban.
    /// </summary>
    public interface IObjectBan : IBan
    {
        /// <summary>
        ///     The ID of the object that is banned.
        /// </summary>
        public uint Id { get; set; }
    }
}
