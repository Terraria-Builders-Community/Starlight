namespace Starlight
{
    public sealed class OnPreCommandArgs
    {
        public ChatCommandContext Context { get; }

        public OnPreCommandArgs(ChatCommandContext context)
        {
            Context = context;
        }
    }
}
