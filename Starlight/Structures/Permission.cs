namespace Starlight.Structures
{
    public readonly struct Permission
    {
        public string Node { get; }

        public Permission(string node)
        {
            Node = node;
        }
    }
}
