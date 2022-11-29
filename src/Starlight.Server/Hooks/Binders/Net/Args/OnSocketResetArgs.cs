using Terraria;

namespace Starlight
{
    public sealed class OnSocketResetArgs
    {
        public RemoteClient Socket { get; set; }

        public OnSocketResetArgs(RemoteClient socket)
        {
            Socket = socket;
        }
    }
}
