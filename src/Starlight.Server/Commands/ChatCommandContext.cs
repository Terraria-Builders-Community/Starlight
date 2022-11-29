using CSF;

namespace Starlight
{
    public class ChatCommandContext : CommandContext
    {
        public ChatCommandContext(string rawInput, IPrefix? prefix = null)
            : base(rawInput, prefix)
        {

        }
    }
}
