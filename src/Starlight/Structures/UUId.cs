using System.Diagnostics.CodeAnalysis;

namespace Starlight.Structures
{
    public readonly struct UUId
    {
        public string Id { get; }

        public UUId(string id)
        {
            Id = id;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
