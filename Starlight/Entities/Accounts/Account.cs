using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Starlight.Structures;
using System.Net;

namespace Starlight.Entities.Accounts
{
    public record class Account : IAccount
    {
        /// <inheritdoc/>
        [BsonId]
        public ObjectId ObjectId { get; set; }

        /// <inheritdoc/>
        public ModelState State { get; set; } = ModelState.Deserializing;

        private string _name = string.Empty;
        /// <inheritdoc/>
        public string Name
        {
            get
                => _name;
            set
            {
                _ = this.ModifyAsync(x => x.Name, value);
                _name = value;
            }
        }

        private UUId _uuid;
        /// <summary>
        ///     The UUID of the user that was banned.
        /// </summary>
        public UUId UUID
        {
            get
                => _uuid;
            set
            {
                _ = this.ModifyAsync(x => x.UUID, value);
                _uuid = value;
            }
        }

        private string? _prefix;
        /// <inheritdoc/>
        public string? Prefix
        {
            get
                => _prefix;
            set
            {
                _ = this.ModifyAsync(x => x.Name, value);
                _prefix = value;
            }
        }

        private string? _suffix;
        /// <inheritdoc/>
        public string? Suffix
        {
            get
                => _suffix;
            set
            {
                _ = this.ModifyAsync(x => x.Name, value);
                _suffix = value;
            }
        }

        private IList<UUId> _userHistory = new List<UUId>();
        /// <inheritdoc/>
        public IList<UUId> DeviceHistory
        {
            get
                => _userHistory;
            set
            {
                _ = this.ModifyAsync(x => x.DeviceHistory, value);
                _userHistory = value;
            }
        }

        private IList<IPAddress> _ipHistory = new List<IPAddress>();
        /// <inheritdoc/>
        public IList<IPAddress> IPHistory
        {
            get
                => _ipHistory;
            set
            {
                _ = this.ModifyAsync(x => x.IPHistory, value);
                _ipHistory = value;
            }
        }

        private IList<Permission> _permissions = new List<Permission>();
        /// <inheritdoc/>
        public IList<Permission> Permissions
        {
            get
                => _permissions;
            set
            {
                _ = this.ModifyAsync(x => x.Permissions, value);
                _permissions = value;
            }
        }

        private IList<string> _groups = new List<string>();
        /// <inheritdoc/>
        public IList<string> Groups
        {
            get
                => _groups;
            set
            {
                _ = this.ModifyAsync(x => x.Groups, value);
                _groups = value;
            }
        }
    }
}
