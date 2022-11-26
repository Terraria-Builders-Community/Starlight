using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Starlight.Structures;

namespace Starlight.Entities.Groups
{
    public record class Group : IGroup
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

        private string _prefix = string.Empty;
        /// <inheritdoc/>
        public string Prefix
        {
            get
                => _prefix;
            set
            {
                _ = this.ModifyAsync(x => x.Prefix, value);
                _prefix = value;
            }
        }

        private string _suffix = string.Empty;
        /// <inheritdoc/>
        public string Suffix
        {
            get
                => _suffix;
            set
            {
                _ = this.ModifyAsync(x => x.Suffix, value);
                _suffix = value;
            }
        }

        private string _parent = string.Empty;
        /// <inheritdoc/>
        public string Parent
        {
            get
                => _parent;
            set
            {
                _ = this.ModifyAsync(x => x.Parent, value);
                _parent = value;
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
    }
}
