using CSF;

namespace Starlight
{
    public sealed class OnPostCommandArgs
    {
        public IResult Result { get; }

        public ChatCommandContext Context { get; }

        public OnPostCommandArgs(IResult result, ChatCommandContext context)
        {
            Result = result;
            Context = context;
        }
    }
}
